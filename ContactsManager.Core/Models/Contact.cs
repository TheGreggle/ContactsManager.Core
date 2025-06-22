using System.ComponentModel.DataAnnotations;

namespace ContactsManager.Core.Models;

/// <summary>
/// Contact model
/// </summary>
public class Contact
{
    public int? Id { get; set; }

    [Required]
    [StringLength(255)]
    public string? FirstName { get; set; }

    [Required]
    [StringLength(255)]
    public string? LastName { get; set; }

    [StringLength(255)]
    public string? CompanyName { get; set; }

    [Phone]
    public string? Mobile { get; set; }

    [EmailAddress]
    public string? Email { get; set; }
}