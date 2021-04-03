using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkCloudTest.Entities;
using WorkCloudTest.Enums;
using WorkCloudTest.Models;

namespace WorkCloudTest.MapperProfiles
{
    public class Profiles : Profile
	{
		public Profiles()
		{
			CreateMap<StudentModel, Student>()
               .ForMember(destination => destination.Casa, origin => origin.MapFrom(source => HouseType.FromName<HouseType>(source.Casa).Value));
                //  .ForMember(destination => destination.Casa, origin => origin.MapFrom(source => (HouseType)Enum.Parse(typeof(HouseType), source.Casa, true)));


		}
	}
}
