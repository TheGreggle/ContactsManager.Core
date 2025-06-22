using ContactsManager.Core.Models;

namespace ContactsManager.Core.Database;

public static class ContactDbHelper
{
    /// <summary>
    /// Applies a search filter to contact records
    /// </summary>
    /// <param name="contacts">The contacts query to filter</param>
    /// <param name="search">The search value</param>
    /// <returns>IQueryable Contact</returns>
    public static IQueryable<Contact> ApplySearchFilter(IQueryable<Contact> contacts, string search)
    {
        // Ensure we have records and a filter
        if (string.IsNullOrWhiteSpace(search) || !contacts.Any())
        {
            return contacts;
        }

        // Filter the records
        return contacts = contacts.Where(c =>
            (c.FirstName ?? "").Contains(search) ||
            (c.LastName ?? "").Contains(search) ||
            (c.CompanyName ?? "").Contains(search) ||
            (c.Email ?? "").Contains(search) ||
            (c.Mobile ?? "").Replace(" ", "").Contains(search.Replace(" ", ""))
        );
    }

    /// <summary>
    /// Applies and sorts contact records
    /// </summary>
    /// <param name="contacts">The contacts query to sort</param>
    /// <param name="sort">Property to sort by</param>
    /// <param name="descending">Set to true to reverse the list order</param>
    /// <returns>IQueryable Contact</returns>
    public static IQueryable<Contact> ApplySorting(IQueryable<Contact> contacts, string sort, bool descending)
    {
        // Ensure we have records
        if (!contacts.Any())
        {
            return contacts;
        }

        // Sort the records
        contacts = sort.ToLower() switch
        {
            "firstname" => descending ? contacts.OrderByDescending(e => e.FirstName) : contacts.OrderBy(c => c.FirstName),
            "companyname" => descending ? contacts.OrderByDescending(e => e.CompanyName) : contacts.OrderBy(c => c.CompanyName),
            "mobile" => descending ? contacts.OrderByDescending(e => e.Mobile) : contacts.OrderBy(c => c.Mobile),
            "email" => descending ? contacts.OrderByDescending(e => e.Email) : contacts.OrderBy(c => c.Email),
            _ => descending ? contacts.OrderByDescending(e => e.LastName) : contacts.OrderBy(c => c.LastName),
        };
        return contacts;
    }
}