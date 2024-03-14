using AutoMapper;
using Project.Service.Database.Models;
using Project.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MVC.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<VehicleMake, VehicleMakeViewModel>().ReverseMap();

            CreateMap<VehicleMake, VehicleMakeCreateViewModel>().ReverseMap();

            CreateMap<VehicleModel, VehicleModelViewModel>()
                .ForMember(dest => dest.MakeName, opt => opt.MapFrom(src => src.VehicleMake.Name))
                .ReverseMap();

            CreateMap<VehicleModel, VehicleModelCreateViewModel>().ReverseMap();
        }
    }
}
