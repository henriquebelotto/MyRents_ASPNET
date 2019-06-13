using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MyRents.Dtos;
using MyRents.Models;

namespace MyRents.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Following one of the many variants that I saw about how to use Automapper

            // Creating a Map between Customer and CustomerDto
            // API -> Outbound
            CreateMap<Customer, CustomerDto>();

            // Creating a Map between CustomerDto and Customer an IGNORING the Id
            // API <- Inbound
            CreateMap<CustomerDto, Customer>()
                .ForMember(c => c.Id, opt => opt.Ignore());

            // Creating a map from MovieDto to Movie class
            // API -> Outbound
            CreateMap<Movie, MovieDto>();

            // Creating a map from Movie to MovieDto
            // API <- Inbound
            CreateMap<MovieDto, Movie>()
                .ForMember(m => m.Id, opt => opt.Ignore());
        }
    }
}