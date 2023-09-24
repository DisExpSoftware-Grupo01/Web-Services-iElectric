using AutoMapper;
using web_services_ielectric.Administrators.Domain.Models;
using web_services_ielectric.Administrators.Resources;
using web_services_ielectric.Announcements.Domain.Models;
using web_services_ielectric.Announcements.Resources;
using web_services_ielectric.ApplianceBrands.Domain.Models;
using web_services_ielectric.ApplianceBrands.Resources;
using web_services_ielectric.ApplianceModels.Domain.Models;
using web_services_ielectric.ApplianceModels.Resources;
using web_services_ielectric.Appliances.Domain.Models;
using web_services_ielectric.Appliances.Resources;
using web_services_ielectric.Appointments.Domain.Models;
using web_services_ielectric.Appointments.Resources;
using web_services_ielectric.Clients.Domain.Models;
using web_services_ielectric.Clients.Resources;
using web_services_ielectric.Plans.Domain.Models;
using web_services_ielectric.Plans.Resources;
using web_services_ielectric.Reports.Domain.Models;
using web_services_ielectric.Reports.Resources;
using web_services_ielectric.Security.Domain.Entities;
using web_services_ielectric.Security.Domain.Services.Communication;
using web_services_ielectric.Shared.Domain.Models;
using web_services_ielectric.Shared.Extensions;
using web_services_ielectric.Shared.Resources;
using web_services_ielectric.SpareRequests.Domain.Models;
using web_services_ielectric.SpareRequests.Resources;
using web_services_ielectric.Technicians.Domain.Models;
using web_services_ielectric.Technicians.Resources;

namespace web_services_ielectric.Shared.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Person, PersonResource>();
        CreateMap<Administrator, AdministratorResource>();
        CreateMap<Client, ClientResource>();
        CreateMap<Technician, TechnicianResource>();
        CreateMap<ApplianceModel, ApplianceModelResource>();
        CreateMap<ApplianceBrand, ApplianceBrandResource>();
        CreateMap<Appliance, ApplianceResource>();
        CreateMap<Announcement, AnnouncementResource>()
            .ForMember(target => target.TypeOfAnnouncement,
                opt => opt.MapFrom(source => source.TypeOfAnnouncement.ToDescriptionString()));
        CreateMap<Appointment, AppointmentResource>();
        CreateMap<Report, ReportResource>();
        CreateMap<SpareRequest, SpareRequestResource>();
        CreateMap<Plan, PlanResource>();
        CreateMap<User, RegisterRequest>();
        CreateMap<User, AuthenticateResponse>();
        CreateMap<User, RegisterResponse>();
    }
}