using Microsoft.EntityFrameworkCore;
using web_services_ielectric.Administrators.Domain.Models;
using web_services_ielectric.Announcements.Domain.Models;
using web_services_ielectric.ApplianceBrands.Domain.Models;
using web_services_ielectric.ApplianceModels.Domain.Models;
using web_services_ielectric.Appliances.Domain.Models;
using web_services_ielectric.Appointments.Domain.Models;
using web_services_ielectric.Clients.Domain.Models;
using web_services_ielectric.Plans.Domain.Models;
using web_services_ielectric.Reports.Domain.Models;
using web_services_ielectric.Shared.Domain.Models;
using web_services_ielectric.Shared.Extensions;
using web_services_ielectric.SpareRequests.Domain.Models;
using web_services_ielectric.Technicians.Domain.Models;

namespace web_services_ielectric.Shared.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Administrator> Administrators { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Technician> Technicians { get; set; }
    public DbSet<Announcement> Announcements { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<SpareRequest> SpareRequests { get; set; }

    public DbSet<ApplianceModel> ApplianceModels { get; set; }
    public DbSet<ApplianceBrand> ApplianceBrands { get; set; }
    public DbSet<Appliance> Appliances { get; set; }
    public DbSet<Plan> Plans { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // START USER //
        

        // FINISH USER //

        // START PERSON //
        // Constraints
        builder.Entity<Person>().ToTable("Persons");
        builder.Entity<Person>().HasKey(p => p.Id);
        builder.Entity<Person>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Person>().Property(p => p.Names).IsRequired().HasMaxLength(30);
        builder.Entity<Person>().Property(p => p.LastNames).IsRequired().HasMaxLength(30);
        builder.Entity<Person>().Property(p => p.CellphoneNumber).IsRequired();
        builder.Entity<Person>().Property(p => p.Address).IsRequired().HasMaxLength(50);
        builder.Entity<Person>().Property(p => p.UserId).IsRequired();
        // END PERSON //

        // START ADMINISTRATOR //
        // Constraints
        builder.Entity<Administrator>().ToTable("administrators");
        //Example data
        /*builder.Entity<Administrator>().HasData(
            new Administrator
            {
                Id = 3,
                Names = "Carlos",
                LastNames = "Leon",
                Address = "San Martin",
                CellphoneNumber = 940596111,
                Email = "carlos.leon@gmail.com",
                Password = "carlos123"
            }
        );*/
        // END ADMINISTRATOR //

        // START CLIENT //
        // Constraints
        builder.Entity<Client>().ToTable("Clients");
        builder.Entity<Client>().Property(p => p.PlanId);

        //Relationships
        builder.Entity<Client>().HasMany(p => p.Appointments)
            .WithOne(p => p.Client)
            .HasForeignKey(p => p.ClientId);

        //Example data
        /*builder.Entity<Client>().HasData(
            new Client
            {
                Id = 1,
                Names = "Jose",
                LastNames = "Feliciano",
                Address = "North Avenue",
                CellphoneNumber = 987879219
            }
        );*/
        // END CLIENT //

        // START TECHNICIAN //
        //Constraints
        builder.Entity<Technician>().ToTable("Technicians");

        //Relationships
        builder.Entity<Technician>().HasMany(p => p.Appointments)
            .WithOne(p => p.Technician)
            .HasForeignKey(p => p.TechnicianId);

        builder.Entity<Technician>().HasMany(p => p.Reports)
            .WithOne(p => p.Technician)
            .HasForeignKey(p => p.TechnicianId);

        builder.Entity<Technician>().HasMany(p => p.SpareRequests)
            .WithOne(p => p.Technician)
            .HasForeignKey(p => p.TechnicianId);

        //Example
        /*builder.Entity<Technician>().HasData(
            new Technician
            {
                Id = 2,
                Names = "Estefano Sebastian",
                LastNames = "Bran Zapata",
                Address = "Los Angeles",
                CellphoneNumber = 987899219,
                //Email = "sebas@gmail.com",
                //Password = "Sebas123"
            }
        );*/
        // END TECHNICIAN //

        // START ANNOUNCEMENT //
        // Constraints
        builder.Entity<Announcement>().ToTable("Announcements");
        builder.Entity<Announcement>().HasKey(p => p.Id);
        builder.Entity<Announcement>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Announcement>().Property(p => p.Title).IsRequired().HasMaxLength(30);
        builder.Entity<Announcement>().Property(p => p.Description).IsRequired();
        builder.Entity<Announcement>().Property(p => p.Content).IsRequired();
        builder.Entity<Announcement>().Property(p => p.UrlToImage).IsRequired();
        builder.Entity<Announcement>().Property(p => p.TypeOfAnnouncement).IsRequired();
        builder.Entity<Announcement>().Property(p => p.Visible).IsRequired();
        // END ANNOUNCEMENT //

        //Plan Entity
        builder.Entity<Plan>().ToTable("Plans");

        // START APPOINTMENT //
        // Constraints
        builder.Entity<Appointment>().ToTable("Appointments");
        builder.Entity<Appointment>().HasKey(p => p.Id);
        builder.Entity<Appointment>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Appointment>().Property(p => p.DateReserve).IsRequired();
        builder.Entity<Appointment>().Property(p => p.Hour).IsRequired();
        builder.Entity<Appointment>().Property(p => p.Done).IsRequired();
        builder.Entity<Appointment>().Property(p => p.ClientId).IsRequired();
        builder.Entity<Appointment>().Property(p => p.TechnicianId).IsRequired();

        //Relationships
        builder.Entity<Appointment>().HasMany(p => p.Reports)
            .WithOne(p => p.Appointment)
            .HasForeignKey(p => p.AppointmentId);

        builder.Entity<Appointment>().HasMany(p => p.SpareRequests)
            .WithOne(p => p.Appointment)
            .HasForeignKey(p => p.AppointmentId);

        //Example
        /*builder.Entity<Appointment>().HasData(
            new Appointment {
                Id = 1,
                DateReserve = "10-12-2021",
                DateAttention = "10-12-2021",
                Hour = "15:00",
                ClientId = 1,
                TechnicianId = 2,
                Done = false
            }
        );*/
        // END APPOINTMENT //


        // START REPORT //
        // Constraints
        builder.Entity<Report>().ToTable("Reports");
        builder.Entity<Report>().HasKey(p => p.Id);
        builder.Entity<Report>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Report>().Property(p => p.Observation).IsRequired().HasMaxLength(100);
        builder.Entity<Report>().Property(p => p.Diagnosis).IsRequired().HasMaxLength(300);
        builder.Entity<Report>().Property(p => p.RepairDescription).IsRequired().HasMaxLength(300);
        builder.Entity<Report>().Property(p => p.ImagePath).IsRequired();
        builder.Entity<Report>().Property(p => p.Date).IsRequired();
        builder.Entity<Report>().Property(p => p.AppointmentId).IsRequired();
        builder.Entity<Report>().Property(p => p.TechnicianId).IsRequired();

        //Example
        /*builder.Entity<Report>().HasData(
            new Report()
            {
                Id = 1,
                Observation = "The microwave smell bad",
                Diagnosis = "A component in the microchip is burned",
                RepairDescription = "I replaced the microchip successfully",
                ImagePath = "https://google.com/images",
                Date = "10-12-24",
                TechnicianId = 2,
                AppointmentId = 1
            }
        );*/
        // END REPORT //

        // START SPARE REQUEST
        // Constraints
        builder.Entity<SpareRequest>().ToTable("SpareRequests");
        builder.Entity<SpareRequest>().HasKey(p => p.Id);
        builder.Entity<SpareRequest>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<SpareRequest>().Property(p => p.Description).IsRequired().HasMaxLength(300);
        
        builder.Entity<SpareRequest>().Property(p => p.Date).IsRequired();
        builder.Entity<SpareRequest>().Property(p => p.ImagePath).IsRequired();
        builder.Entity<SpareRequest>().Property(p => p.AppointmentId).IsRequired();
        builder.Entity<SpareRequest>().Property(p => p.TechnicianId).IsRequired();

        /*builder.Entity<SpareRequest>().HasData(
            new SpareRequest()
            {
                Id = 1,
                Description = "We need buy these pieces: #1, #2, #3...",
                Date = "10-12-24",
                ImagePath = "https://google.com/images",
                AppointmentId = 1,
                TechnicianId = 2
            }
        );*/
        // END SPARE REQUEST

        //Constraints
        builder.Entity<Plan>().HasKey(p => p.Id);
        builder.Entity<Plan>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Plan>().Property(p => p.Name).IsRequired().HasMaxLength(30);
        builder.Entity<Plan>().Property(p => p.Price).IsRequired();


        // START APPLIANCE MODELS //

        //Constraints
        builder.Entity<ApplianceModel>().ToTable("ApplianceModels");
        builder.Entity<ApplianceModel>().HasKey(a => a.Id);
        builder.Entity<ApplianceModel>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ApplianceModel>().Property(a => a.Name).IsRequired();
        builder.Entity<ApplianceModel>().Property(a => a.Model).IsRequired();
        builder.Entity<ApplianceModel>().Property(a => a.ImgPath).IsRequired();
        builder.Entity<ApplianceModel>().Property(a => a.ApplianceBrandId).IsRequired();

        /*builder.Entity<ApplianceModel>().HasData(
            new ApplianceModel {Id=1,Name = "",Model = "",ImgPath = "",PurchaseDate = "",ApplianceBrandId = 1 }
        );*/

        // END APPLIANCE MODELS //

        // START APPLIANCE BRAND //

        builder.Entity<ApplianceBrand>().ToTable("ApplianceBrands");
        builder.Entity<ApplianceBrand>().HasKey(a => a.Id);
        builder.Entity<ApplianceBrand>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ApplianceBrand>().Property(a => a.Name).IsRequired();
        builder.Entity<ApplianceBrand>().Property(a => a.ImgPath).IsRequired();

        builder.Entity<ApplianceBrand>().HasMany(a => a.ApplianceModels)
            .WithOne(a => a.ApplianceBrand)
            .HasForeignKey(a => a.ApplianceBrandId);

        // END APLIANCE BRAND//

        // START APPLIANCE //

        builder.Entity<Appliance>().ToTable("Appliances");
        builder.Entity<Appliance>().HasKey(p => p.Id);
        builder.Entity<Appliance>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Appliance>().Property(p => p.ClientId).IsRequired();
        builder.Entity<Appliance>().Property(p => p.ApplianceModelId).IsRequired();
        builder.Entity<Appliance>().Property(p => p.PurchaseDate).IsRequired();

        // END APPLIANCE //

        // Apply Snake Case Naming Convention to All Objects
        builder.UseSnakeCaseNamingConvention();
    }
    
}