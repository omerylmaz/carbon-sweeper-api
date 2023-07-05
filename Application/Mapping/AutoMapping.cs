using Application.Dto.Response;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<User, GetUserFootPrintResponse>()
                .ForMember(dest => dest.FullName, src => src.MapFrom(src => $"{src.FirstName} {src.LastName}") );
        }
    }
}
