using AutoMapper;
using Rommelmarkten.Api.Features.Users.Application.Models;

namespace Rommelmarkten.Api.Features.Users.Application.Queries
{
    public class UserProfileDtoProfile : Profile
    {
        public UserProfileDtoProfile()
        {
            CreateMap<UserProfileAndUser, UserProfileDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.Consented, opt => opt.MapFrom(src => src.Profile.Consented))
                .ForMember(dest => dest.IsBanned, opt => opt.MapFrom(src => src.Profile.IsBanned))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Profile.Name))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Profile.Address))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Profile.PostalCode))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Profile.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Profile.Country))
                .ForMember(dest => dest.VAT, opt => opt.MapFrom(src => src.Profile.VAT))
                .ForMember(dest => dest.ActivationRemindersSent, opt => opt.MapFrom(src => src.Profile.ActivationRemindersSent))
                .ForMember(dest => dest.LastActivationMailSendDate, opt => opt.MapFrom(src => src.Profile.LastActivationMailSendDate))
                .ForMember(dest => dest.ActivationDate, opt => opt.MapFrom(src => src.Profile.ActivationDate))
                .ForMember(dest => dest.LastActivityDate, opt => opt.MapFrom(src => src.Profile.LastActivityDate))
                .ForMember(dest => dest.LastPasswordResetDate, opt => opt.MapFrom(src => src.Profile.LastPasswordResetDate))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Profile.Created))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Profile.CreatedBy))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.Profile.LastModified))
                .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(src => src.Profile.LastModifiedBy));
        }
    }


}
