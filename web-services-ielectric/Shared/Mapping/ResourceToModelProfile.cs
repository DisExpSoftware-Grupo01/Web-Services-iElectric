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
using web_services_ielectric.Shared.Resources;
using web_services_ielectric.SpareRequests.Domain.Models;
using web_services_ielectric.SpareRequests.Resources;
using web_services_ielectric.Technicians.Domain.Models;
using web_services_ielectric.Technicians.Resources;

namespace web_services_ielectric.Shared.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SavePersonResource, Person>();
        CreateMap<SaveAdministratorResource, Administrator>();
        CreateMap<SaveClientResource, Client>();
        CreateMap<SaveTechnicianResource, Technician>();
        CreateMap<SaveApplianceModelResource, ApplianceModel>();
        CreateMap<SaveApplianceBrandResource, ApplianceBrand>();
        CreateMap<SaveApplianceResource, Appliance>();
        CreateMap<SaveAnnouncementResource, Announcement>()
            .ForMember(target => target.TypeOfAnnouncement,
                opt => opt.MapFrom(source => (ETypeOfAnnouncement)source.TypeOfAnnouncement));
        CreateMap<SaveAppointmentResource, Appointment>();
        CreateMap<SaveReportResource, Report>();
        CreateMap<SaveSpareRequestResource, SpareRequest>();
        CreateMap<SavePlanResource, Plan>();
        CreateMap<RegisterRequest, User>();
        CreateMap<AuthenticateRequest, User>();
        CreateMap<UpdateRequest, User>();
    }
}