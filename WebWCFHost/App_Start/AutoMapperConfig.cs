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
                //cfg.CreateMap<Appointment, AppointmentDTO>().ForMember(x => x.Users, opt => opt.Ignore());
                cfg.CreateMap<AppointmentDTO, Appointment>();

                cfg.CreateMap<Location, LocationDTO>();

                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, User>();
            });
        }
    }
}