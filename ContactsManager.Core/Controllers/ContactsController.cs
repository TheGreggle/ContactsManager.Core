using ContactsManager.Core.Database;
using ContactsManager.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsManager.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController(ContactDbContext contactDbContext) : Controller
{
    private const int PAGE_SIZE = 10;

    /// <summary>
    /// Return contacts
    /// </summary>
    /// <param name="sort">Property to sort by</param>
    /// <param name="page">Current page of the paginated list</param>
    /// <param name="search">Optional string to search for</param>
    /// <param name="descending">Set to true to reverse the list order</param>
    /// <returns>A list of contacts based on the provided parameters</returns>
    [HttpGet]
    [Route("")]
    [ProducesResponseType(typeof(List<Contact>), 200)]
    public ActionResult<List<Contact>> Contacts(
        string sort = "LastName", int page = 1, string search = "", bool descending = false
    )
    {
        // Start query
        IQueryable<Contact> contactsQuery = contactDbContext.Contacts.AsQueryable();

        // Sort by query
        contactsQuery = ContactDbHelper.ApplySorting(contactsQuery, sort, descending);

        // Filter by search text query
        contactsQuery = ContactDbHelper.ApplySearchFilter(contactsQuery, search);

        // Execute query
        List<Contact> contacts = [.. contactsQuery];

        // Paginate the response
        int total = contacts.Count;
        List<Contact> pagedContacts = [.. contacts.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE)];

        // Pass pagination state back to the UI
        Response.Headers["Sort"] = sort;
        Response.Headers["Descending"] = descending.ToString();
        Response.Headers["Page"] = page.ToString();
        Response.Headers["TotalPages"] = ((int)Math.Ceiling((double)total / PAGE_SIZE)).ToString();
        Response.Headers["Search"] = search;

        return pagedContacts;
    }

    /// <summary>
    /// Returns a single contact by id
    /// </summary>
    /// <param name="id">The contact id</param>
    /// <returns>A HTTP status code. 200 for success. 404 for not found.</returns>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(List<Contact>), 200)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Contact?> Get(int id)
    {
        Contact? contact = contactDbContext.Contacts.Find(id);

        if (contact == null)
        {
            return NotFound();
        }

        return (contact);
    }

    /// <summary>
    /// Update a contact
    /// </summary>
    /// <param name="contact">The contact to update</param>
    /// <returns>A HTTP status code. 200 for success. 400 for failure.</returns>
    [HttpPatch]
    [Route("")]
    [ProducesResponseType(typeof(Contact), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Update(Contact contact)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        contactDbContext.Entry(contact).State = EntityState.Modified;
        contactDbContext.SaveChanges();
        return Ok(contact);
    }

    /// <summary>
    /// Delete a contact by id
    /// </summary>
    /// <param name="id">The contact id to delete</param>
    /// <returns>A HTTP status code. 200 for success. 400 for failure.</returns>
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Delete(int id)
    {
        Contact? contact = contactDbContext.Contacts.Find(id);
        if (contact == null)
        {
            return BadRequest();
        }

        contactDbContext.Contacts.Remove(contact);
        contactDbContext.SaveChanges();
        return Ok();
    }

    /// <summary>
    /// Create a new contact
    /// </summary>
    /// <param name="contact">A contact object</param>
    /// <returns>A HTTP status code. 200 for success. 400 for failure.</returns>
    [HttpPost]
    [Route("")]
    [ProducesResponseType(typeof(Contact), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Create(Contact contact)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        contact.Id = null;
        contactDbContext.Contacts.Add(contact);
        contactDbContext.SaveChanges();
        return Ok(contact);
    }
}
