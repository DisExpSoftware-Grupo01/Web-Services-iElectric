using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using web_services_ielectric.Administrators.Domain.Repositories;
using web_services_ielectric.Administrators.Domain.Services;
using web_services_ielectric.Administrators.Persistence.Repositories;
using web_services_ielectric.Administrators.Services;
using web_services_ielectric.Announcements.Domain.Repositories;
using web_services_ielectric.Announcements.Domain.Services;
using web_services_ielectric.Announcements.Persistence.Repositories;
using web_services_ielectric.Announcements.Services;
using web_services_ielectric.ApplianceBrands.Domain.Repositories;
using web_services_ielectric.ApplianceBrands.Domain.Services;
using web_services_ielectric.ApplianceBrands.Persistence.Repositories;
using web_services_ielectric.ApplianceBrands.Services;
using web_services_ielectric.ApplianceModels.Domain.Repositories;
using web_services_ielectric.ApplianceModels.Domain.Services;
using web_services_ielectric.ApplianceModels.Persistence.Repositories;
using web_services_ielectric.ApplianceModels.Services;
using web_services_ielectric.Appliances.Domain.Repositories;
using web_services_ielectric.Appliances.Domain.Services;
using web_services_ielectric.Appliances.Persistence.Repositories;
using web_services_ielectric.Appliances.Services;
using web_services_ielectric.Appointments.Domain.Repositories;
using web_services_ielectric.Appointments.Domain.Services;
using web_services_ielectric.Appointments.Persistence.Repositories;
using web_services_ielectric.Appointments.Services;
using web_services_ielectric.Clients.Domain.Repositories;
using web_services_ielectric.Clients.Domain.Services.Communication;
using web_services_ielectric.Clients.Persistence.Repositories;
using web_services_ielectric.Clients.Services;
using web_services_ielectric.Plans.Domain.Repositories;
using web_services_ielectric.Plans.Domain.Services;
using web_services_ielectric.Plans.Persistence.Repositories;
using web_services_ielectric.Plans.Services;
using web_services_ielectric.Reports.Domain.Repositories;
using web_services_ielectric.Reports.Domain.Services;
using web_services_ielectric.Reports.Persistence.Repositories;
using web_services_ielectric.Reports.Services;
using web_services_ielectric.Shared.Domain.Repositories;
using web_services_ielectric.Shared.Mapping;
using web_services_ielectric.Shared.Persistence.Contexts;
using web_services_ielectric.Shared.Persistence.Repositories;
using web_services_ielectric.SpareRequests.Domain.Repositories;
using web_services_ielectric.SpareRequests.Domain.Services;
using web_services_ielectric.SpareRequests.Persistence.Repositories;
using web_services_ielectric.SpareRequests.Services;
using web_services_ielectric.Technicians.Domain.Repositories;
using web_services_ielectric.Technicians.Domain.Services;
using web_services_ielectric.Technicians.Persistence.Repositories;
using web_services_ielectric.Technicians.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo() { Title = "isa.io RESTful API", Version = "v1" });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseMySQL(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors());

// Add lowercase routes

builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Dependency Injection Configuration

//Repositories
builder.Services.AddScoped<IAdministratorRepository, AdministratorRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<ITechnicianRepository, TechnicianRepository>();
builder.Services.AddScoped<IApplianceModelRepository, ApplianceModelRepository>();
builder.Services.AddScoped<IApplianceBrandRepository, ApplianceBrandRepository>();
builder.Services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<ISpareRequestRepository, SpareRequestRepository>();
builder.Services.AddScoped<IPlanRepository, PlanRepository>();
builder.Services.AddScoped<IApplianceRepository, ApplianceRepository>();

//Unit of work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Services
builder.Services.AddScoped<IAdministratorService, AdministratorService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<ITechnicianService, TechnicianService>();
builder.Services.AddScoped<IApplianceModelService, ApplianceModelService>();
builder.Services.AddScoped<IApplianceBrandService, ApplianceBrandService>();
builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<ISpareRequestService, SpareRequestService>();
builder.Services.AddScoped<IPlanService, PlanService>();
builder.Services.AddScoped<IApplianceService, ApplianceService>();


// AutoMapper Configuration

builder.Services.AddAutoMapper(
    typeof(ModelToResourceProfile),
    typeof(ResourceToModelProfile));


var app = builder.Build();

// Validation for ensuring Database Objects are created

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "isa.io RESTful API");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();