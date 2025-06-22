using ContactsManager.Core.Controllers;
using ContactsManager.Core.Database;
using ContactsManager.Core.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ContactsManager.Core.Tests;

/// <summary>
/// Contacts Manager Controller Unit Tests
/// </summary>
[TestClass]
public sealed class ContactDbHelperTests
{
    private Mock<ContactDbContext>? mockContext;
    private Mock<DbSet<Contact>>? mockSet;
    private List<Contact>? contactList;

    [TestInitialize]
    public void Setup()
    {
        contactList =
        [
            new Contact { Id = 1, FirstName = "Neil", LastName = "Armstrong", Email = "neil@space.com", Mobile = "0400 111 222" },
            new Contact { Id = 2, FirstName = "Buzz", LastName = "Aldrin", Email = "buzz@apollo.com", Mobile = "04 00 333 444" }
        ];

        IQueryable<Contact> queryable = contactList.AsQueryable();

        mockSet = new Mock<DbSet<Contact>>();
        mockSet.As<IQueryable<Contact>>().Setup(m => m.Provider).Returns(queryable.Provider);
        mockSet.As<IQueryable<Contact>>().Setup(m => m.Expression).Returns(queryable.Expression);
        mockSet.As<IQueryable<Contact>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        mockSet.As<IQueryable<Contact>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
        mockSet.Setup(m => m.Find(It.IsAny<object[]>()))
            .Returns<object[]>(ids => contactList.FirstOrDefault(c => c.Id == (int)ids[0]));

        mockContext = new Mock<ContactDbContext>();
        mockContext.Setup(c => c.Contacts).Returns(mockSet.Object);
    }

    [TestMethod]
    public void ApplySearchFilter_Returns_Matching_Contacts()
    {
        // Arrange
        IQueryable<Contact> data = new List<Contact>
        {
            new() { FirstName = "Neil", Email = "neil@moon.com", Mobile = "0400987654" },
            new() { FirstName = "Buzz", Email = "buzz@apollo.com", Mobile = "0400 123 456" }
        }.AsQueryable();

        // Act
        List<Contact> result = [.. ContactDbHelper.ApplySearchFilter(data, "0400123456")];

        // Assert
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("Buzz", result[0].FirstName);
    }

    [TestMethod]
    public void ApplySearchFilter_Returns_No_Contacts()
    {
        // Arrange
        IQueryable<Contact> data = new List<Contact> { }.AsQueryable();

        // Act
        List<Contact> result = [.. ContactDbHelper.ApplySearchFilter(data, "0400123456")];

        // Assert
        Assert.AreEqual(0, result.Count);
    }

    [DataTestMethod]
    [DataRow("FirstName", false, new[] { "Ada", "Liam", "Zoe" })]
    [DataRow("FirstName", true, new[] { "Zoe", "Liam", "Ada" })]
    [DataRow("LastName", false, new[] { "Alpha", "Lambda", "Zeta" })]
    [DataRow("LastName", true, new[] { "Zeta", "Lambda", "Alpha" })]
    [DataRow("CompanyName", false, new[] { "A Corp", "L Corp", "Z Corp" })]
    [DataRow("Email", true, new[] { "zoe@site.com", "liam@site.com", "ada@site.com" })]
    public void ApplySorting_Returns_Correct_Order(string sortField, bool descending, string[] expectedFirstFields)
    {
        // Arrange
        IQueryable<Contact> contacts = new List<Contact>
        {
            new() { FirstName = "Zoe", LastName = "Zeta", CompanyName = "Z Corp", Email = "zoe@site.com" },
            new() { FirstName = "Ada", LastName = "Alpha", CompanyName = "A Corp", Email = "ada@site.com" },
            new() { FirstName = "Liam", LastName = "Lambda", CompanyName = "L Corp", Email = "liam@site.com" },
        }.AsQueryable();

        // Act
        List<Contact> sorted = [.. ContactDbHelper.ApplySorting(contacts, sortField, descending)];

        // Assert
        for (int i = 0; i < expectedFirstFields.Length; i++)
        {
            string expected = expectedFirstFields[i];
            string actual = sortField switch
            {
                "FirstName" => sorted[i]?.FirstName ?? "",
                "LastName" => sorted[i]?.LastName ?? "",
                "CompanyName" => sorted[i]?.CompanyName ?? "",
                "Email" => sorted[i]?.Email ?? "",
                _ => sorted[i]?.LastName ?? "",
            };
            Assert.AreEqual(expected, actual);
        }
    }

    [TestMethod]
    public void ApplySorting_Returns_Unchanged_When_Empty()
    {
        // Arrange
        IQueryable<Contact> emptyContact = new List<Contact>().AsQueryable();

        // Act
        IQueryable<Contact> result = ContactDbHelper.ApplySorting(emptyContact, "FirstName", false);

        // Assert
        Assert.AreEqual(0, result.Count());
    }
}