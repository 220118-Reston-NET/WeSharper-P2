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
            CreateMap<Message, MessageDTO>()
                .ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(src =>
                    src.SenderUser.Profiles.FirstOrDefault(x => x.UserId.Equals(src.SenderUserId)).ProfilePictureUrl))
                .ForMember(dest => dest.RecipientPhotoUrl, opt => opt.MapFrom(src =>
                    src.RecipientUser.Profiles.FirstOrDefault(x => x.UserId.Equals(src.SenderUserId)).ProfilePictureUrl));
        }
    }
}