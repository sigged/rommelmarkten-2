using Rommelmarkten.Api.Common.Application.Mappings;
using Rommelmarkten.Api.Common.Application.Models;
using Rommelmarkten.Api.Common.Domain;
using Rommelmarkten.Api.Features.Users.Domain;

namespace Rommelmarkten.Api.Features.Users.Application.Models;

public class UserProfileDto
{
    //public UserProfileDto(IUser user, UserProfile userProfile)
    public UserProfileDto()
    {
        //Id = user.Id;
        //Email = user.Email;
        //UserName = user.UserName;

        //Consented = userProfile.Consented;
        //IsBanned = userProfile.IsBanned;
        //Name = userProfile.Name;
        //Address = userProfile.Address;
        //PostalCode = userProfile.PostalCode;
        //City = userProfile.City;
        //Country = userProfile.Country;
        //VAT = userProfile.VAT;
        //ActivationRemindersSent = userProfile.ActivationRemindersSent;
        //LastActivationMailSendDate = userProfile.LastActivationMailSendDate;
        //ActivationDate = userProfile.ActivationDate;
        //LastActivityDate = userProfile.LastActivityDate;
        //LastPasswordResetDate = userProfile.LastPasswordResetDate;
        //Created = userProfile.Created;
        //CreatedBy = userProfile.CreatedBy;
        //LastModified = userProfile.LastModified;
        //LastModifiedBy = userProfile.LastModifiedBy;


    }

    public static UserProfileDto FromEntities(IUser user, UserProfile userProfile)
    {
        return new UserProfileDto
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            Consented = userProfile.Consented,
            IsBanned = userProfile.IsBanned,
            Name = userProfile.Name,
            Address = userProfile.Address,
            PostalCode = userProfile.PostalCode,
            City = userProfile.City,
            Country = userProfile.Country,
            VAT = userProfile.VAT,
            ActivationRemindersSent = userProfile.ActivationRemindersSent,
            LastActivationMailSendDate = userProfile.LastActivationMailSendDate,
            ActivationDate = userProfile.ActivationDate,
            LastActivityDate = userProfile.LastActivityDate,
            LastPasswordResetDate = userProfile.LastPasswordResetDate,
            Created = userProfile.Created,
            CreatedBy = userProfile.CreatedBy,
            LastModified = userProfile.LastModified,
            LastModifiedBy = userProfile.LastModifiedBy
        };
    }

    public required string Id { get; set; }
    public required string? Email { get; set; }
    public required string? UserName { get; set; }
    public required bool Consented { get; set; }
    public required bool IsBanned { get; set; }
    public required string Name { get; set; }
    public required string? Address { get; set; }
    public required string? PostalCode { get; set; }
    public required string? City { get; set; }
    public required string? Country { get; set; }
    public required string? VAT { get; set; }
    public required int ActivationRemindersSent { get; set; }
    public required DateTimeOffset? LastActivationMailSendDate { get; set; }
    public required  DateTimeOffset? ActivationDate { get; set; }
    public required DateTimeOffset? LastActivityDate { get; set; }
    public required DateTimeOffset? LastPasswordResetDate { get; set; }
    public required DateTime Created { get; set; }
    public required string? CreatedBy { get; set; }
    public required DateTime? LastModified { get; set; }
    public required string? LastModifiedBy { get; set; }

}
