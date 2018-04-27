[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebWCFHost.App_Start.AutoMapperConfig), "RegisterMappings")]

namespace WebWCFHost.App_Start
{
    using AutoMapper;
    using BLL.EntitesDTO;
    using Model.Entities;

    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Appointment, AppointmentDTO>();
                cfg.CreateMap<AppointmentDTO, Appointment>();

                cfg.CreateMap<Location, LocationDTO>();

                cfg.CreateMap<User, UserDTO>()
                    .ForMember(s => s.Groups, opt => opt.Ignore());
                cfg.CreateMap<UserDTO, User>();

                cfg.CreateMap<Group, GroupDTO>();
                cfg.CreateMap<GroupDTO, Group>();

                cfg.CreateMap<Role, RoleDTO>();
                cfg.CreateMap<RoleDTO, Role>();

                cfg.CreateMap<Log, LogDTO>();

                cfg.CreateMap<Notification, NotificationDTO>();
                cfg.CreateMap<NotificationDTO, Notification>();
                cfg.CreateMap<AppointmentDTO, Notification>();
            });
        }
    }
}