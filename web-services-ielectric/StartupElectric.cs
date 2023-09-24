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
using web_services_ielectric.Security.Authorization.Handlers.Implementations;
using web_services_ielectric.Security.Authorization.Handlers.Interfaces;
using web_services_ielectric.Security.Authorization.Middleware;
using web_services_ielectric.Security.Authorization.Settings;
using web_services_ielectric.Security.Domain.Repositories;
using web_services_ielectric.Security.Domain.Services;
using web_services_ielectric.Security.Persistence.Repositories;
using web_services_ielectric.Security.Services;
using web_services_ielectric.Shared.Domain.Repositories;
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

namespace web_services_ielectric;
public class StartupElectric{
    public StartupElectric(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Add CORS
            services.AddCors();

            services.AddControllers();

            //Database
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("AzureDbConnection"), new MySqlServerVersion(new Version(8, 0, 26)));
            });

            //Repositories
            services.AddScoped<IAdministratorRepository, AdministratorRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ITechnicianRepository, TechnicianRepository>();
            services.AddScoped<IApplianceModelRepository, ApplianceModelRepository>();
            services.AddScoped<IApplianceBrandRepository, ApplianceBrandRepository>();
            services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<ISpareRequestRepository, SpareRequestRepository>();
            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IApplianceRepository, ApplianceRepository>();

            //Unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Services
            services.AddScoped<IAdministratorService, AdministratorService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<ITechnicianService, TechnicianService>();
            services.AddScoped<IApplianceModelService, ApplianceModelService>();
            services.AddScoped<IApplianceBrandService, ApplianceBrandService>();
            services.AddScoped<IAnnouncementService, AnnouncementService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<ISpareRequestService, SpareRequestService>();
            services.AddScoped<IPlanService, PlanService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IApplianceService, ApplianceService>();

            //Utilities
            services.AddScoped<IJwtHandler, JwtHandler>();

            //Endpoint Naming Conventions
            services.AddRouting(options => options.LowercaseUrls = true);

            //Configure AppSettings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            //AutoMapper Setup
            //services.AddAutoMapper(typeof(Startup));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "web_services_ielectric", Version = "v1" });
                c.EnableAnnotations();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger(c => c.SerializeAsV2 = true);
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "web_services_ielectric v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            // CORS Configuration
            app.UseCors(x => x.SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseAuthorization();

            // Integrate Error Handling Middleware
            app.UseMiddleware<ErrorHandlerMiddleware>();

            //Integrate JWT Authorization Middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
}