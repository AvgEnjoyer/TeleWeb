using AutoMapper;
using TeleWeb.Application.DTOs;
using TeleWeb.Domain.Models;

namespace TeleWeb.Presentation.AppStartExtensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // DTO of User


            CreateMap<UpdateUserDTO, User>();
            CreateMap<User, UpdateUserDTO>();


            CreateMap<GetUserDTO, User>();
            CreateMap<User,GetUserDTO >();
            
            
            // DTO of Post
            CreateMap<GetPostDTO, Post>();
            CreateMap<Post, GetPostDTO>();

            CreateMap<UpdatePostDTO, Post>()
                .ForMember(dest => dest.MediaFiles, opt => opt.MapFrom(src => src.MediaFileDTOs));
            CreateMap<Post, GetPostDTO>()
                .ForMember(dest => dest.MediaFileDTOs, opt => opt.MapFrom(src => src.MediaFiles));

            
            // DTO of Channel
            CreateMap<GetChannelDTO, Channel>();
            CreateMap<Channel, GetChannelDTO>();


            // CreateMap<UpdateChannelDTO,Channel>()
            //     .ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) =>
            //         srcMember != null && destMember != null));
            CreateMap<UpdateChannelDTO, Channel>();
            CreateMap<Channel,UpdateChannelDTO>();

            //DTO of MediaFile
            CreateMap<MediaFileDTO, MediaFile>();
            CreateMap<MediaFile, MediaFileDTO>();
        }
    }
}