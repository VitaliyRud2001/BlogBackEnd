using Application.Dtos;
using AutoMapper;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.MapperProfile
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {


            CreateMap<Post, PostGetDto>()
                .ForMember(p => p.User, opt => opt.MapFrom(dto => dto.User))
                .ForMember(p => p.Tags, opt => opt.MapFrom(dto => dto.Tags.Select(p => p.Tag).ToList()));

            CreateMap<Tag, TagDto>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();

            CreateMap<TagDto, PostTag>()
                .ForMember(p => p.TagId, opt => opt.MapFrom(dto => dto.Id));


            CreateMap<PostCreateDto, Post>()
                .ForMember(entity => entity.Tags, opt => opt.MapFrom(x => x.Tags))
                .AfterMap((model, entity) =>
                {
                    foreach (var item in entity.Tags)
                    {
                        item.PostId = entity.Id;
                    }

                });

        }
    }
}
