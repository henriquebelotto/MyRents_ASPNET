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
            // Following one of the many variants that I saw about how to use
            // Automapper

            // Creating a Map between Customer and CustomerDto
            CreateMap<Customer, CustomerDto>();

            // Creating a Map between CustomerDto and Customer
            CreateMap<CustomerDto, Customer>();

        }
    }
}