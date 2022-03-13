using System;
using System.Linq;
using AutoMapper;
using WeSharper.APIPortal.DataTransferObjects;
using WeSharper.Models;

namespace WeSharper.APIPortal.Helpers
{
    public class AutoMapperProfiles : AutoMapper.Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<MessageDTO, Message>();
        }
    }
}