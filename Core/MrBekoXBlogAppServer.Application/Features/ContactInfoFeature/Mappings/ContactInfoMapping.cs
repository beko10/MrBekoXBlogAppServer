using AutoMapper;
using MrBekoXBlogAppServer.Application.Features.ContactInfoFeature.DTOs;
using MrBekoXBlogAppServer.Domain.Entities;

namespace MrBekoXBlogAppServer.Application.Features.ContactInfoFeature.Mappings;

public class ContactInfoMapping : Profile
{
    public ContactInfoMapping()
    {
        CreateMap<ContactInfo,CreateContactInfoCommandDto>().ReverseMap();
        CreateMap<ContactInfo,UpdateContactInfoCommandDto>().ReverseMap();
        CreateMap<ContactInfo,ResultContactInfoQueryDto>().ReverseMap();
    }
}
