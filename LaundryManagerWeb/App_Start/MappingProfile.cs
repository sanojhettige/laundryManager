using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LaundryManagerWeb.Models;
using LaundryManagerWeb.Dtos;


namespace LaundryManagerWeb.App_Start
{
    public class MappingProfile : Profile
    {
        /* create mapping convention*/
        public MappingProfile()
        {
            Mapper.CreateMap<ActivityDto, Activity>();
            Mapper.CreateMap<Activity, ActivityDto>();
            Mapper.CreateMap<CategoryDto, Category>();
            Mapper.CreateMap<Category, CategoryDto>();

        }
    }


}