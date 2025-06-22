using ContactsManager.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsManager.Core.Database;

/// <summary>
/// The EF DB context
/// </summary>
public class ContactDbContext(DbContextOptions<ContactDbContext> options) : DbContext(options)
{
    public virtual DbSet<Contact> Contacts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Contact>().HasData(
            new Contact { Id = 1, FirstName = "Neil", LastName = "Armstrong", CompanyName = "Apollo Ventures", Mobile = "0451 001 001", Email = "neil@apollo11.space" },
            new Contact { Id = 2, FirstName = "Buzz", LastName = "Aldrin", CompanyName = "Lunar Logistics", Mobile = "0452 002 002", Email = "buzz@moonmail.io" },
            new Contact { Id = 3, FirstName = "Yuri", LastName = "Gagarin", CompanyName = "Vostok Explorations", Mobile = "0453 003 003", Email = "yuri@cosmos.ru" },
            new Contact { Id = 4, FirstName = "Sally", LastName = "Ride", CompanyName = "Challenger Research", Mobile = "0454 004 004", Email = "sally@orbitgirls.org" },
            new Contact { Id = 5, FirstName = "Chris", LastName = "Hadfield", CompanyName = "ISS Harmony Studios", Mobile = "0455 005 005", Email = "chris@spaceguitar.ca" },
            new Contact { Id = 6, FirstName = "Mae", LastName = "Jemison", CompanyName = "Endeavour BioSystems", Mobile = "0456 006 006", Email = "mae@orbitalhealth.com" },
            new Contact { Id = 7, FirstName = "John", LastName = "Glenn", CompanyName = "Mercury Dynamics", Mobile = "0457 007 007", Email = "john@friendship7.gov" },
            new Contact { Id = 8, FirstName = "Valentina", LastName = "Tereshkova", CompanyName = "Vostok Voyagers", Mobile = "0458 008 008", Email = "valentina@cccp.space" },
            new Contact { Id = 9, FirstName = "Peggy", LastName = "Whitson", CompanyName = "ISS Command Core", Mobile = "0459 009 009", Email = "peggy@spacetime360.com" },
            new Contact { Id = 10, FirstName = "Tim", LastName = "Peake", CompanyName = "ESA Pathfinder Group", Mobile = "0460 010 010", Email = "tim@europass.uk" },
            new Contact { Id = 11, FirstName = "Alan", LastName = "Shepard", CompanyName = "Freedom 7 Consulting", Mobile = "0461 111 111", Email = "alan@mercuryflight.com" },
            new Contact { Id = 12, FirstName = "Guion", LastName = "Bluford", CompanyName = "Orbital Dynamics Inc.", Mobile = "0462 222 222", Email = "guion@blufordlabs.org" },
            new Contact { Id = 13, FirstName = "Eileen", LastName = "Collins", CompanyName = "Shuttle Command Systems", Mobile = "0463 333 333", Email = "eileen@stslead.com" },
            new Contact { Id = 14, FirstName = "Michael", LastName = "Collins", CompanyName = "Lunar Orbit Logistics", Mobile = "0464 444 444", Email = "michael@apollocommand.net" },
            new Contact { Id = 15, FirstName = "Kalpana", LastName = "Chawla", CompanyName = "Microgravity Research Group", Mobile = "0465 555 555", Email = "kalpana@columbialabs.org" },
            new Contact { Id = 16, FirstName = "Luca", LastName = "Parmitano", CompanyName = "ESA Aquanauts", Mobile = "0466 666 666", Email = "luca@spaceitalia.eu" },
            new Contact { Id = 17, FirstName = "Jean-François", LastName = "Clervoy", CompanyName = "Orbital Engineering Europe", Mobile = "0467 777 777", Email = "jf@esaorbit.fr" },
            new Contact { Id = 18, FirstName = "Chiaki", LastName = "Mukait", CompanyName = "JAXA Life Sciences", Mobile = "0468 888 888", Email = "chiaki@jaxa.jp" },
            new Contact { Id = 19, FirstName = "Victor", LastName = "Glover", CompanyName = "Crew Dragon Ops", Mobile = "0469 999 999", Email = "victor@spacexflight.us" },
            new Contact { Id = 20, FirstName = "Jessica", LastName = "Watkins", CompanyName = "Mars Geology Group", Mobile = "0470 000 000", Email = "jessica@redrockresearch.com" }
        );
    }
}