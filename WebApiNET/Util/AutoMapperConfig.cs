﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BLL.EntitesDTO;
using Model.Entities;

namespace WebApiNET.Util
{
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