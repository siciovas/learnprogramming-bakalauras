﻿using AutoMapper;
using LearnProgramming.Core.Dto.DtoPost;
using LearnProgramming.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnProgramming.Infrastructure.AutoMapper
{
    public class ShippingInformationProfile : Profile
    {
        public ShippingInformationProfile()
        {
            CreateMap<ShippingInformation, ShippingInformationPostDto>();
        }
    }
}
