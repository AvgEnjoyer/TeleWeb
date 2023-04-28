using AutoMapper;
using TeleWeb.Application.DTOs;
using TeleWeb.Domain.Models;

namespace TeleWeb.Presentation.AppStartExtensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // UserDTO to User
            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // User to UserDTO
            CreateMap<User, UserDTO>();

            // AdminDTO to Admin
            CreateMap<AdminDTO, Admin>()
                .IncludeBase<UserDTO, User>()
                .ForMember(dest => dest.OwnedChannels, opt => opt.Ignore())
                .ForMember(dest => dest.AdministratingChannels, opt => opt.Ignore())
                .ForMember(dest => dest.Posts, opt => opt.Ignore());

            // Admin to AdminDTO
            CreateMap<Admin, AdminDTO>()
                .IncludeBase<User, UserDTO>();

            // PostDTO to Post
            CreateMap<PostDTO, Post>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Channel, opt => opt.MapFrom(src => src.Channel))
                .ForMember(dest => dest.AdminWhoPosted, opt => opt.MapFrom(src => src.AdminWhoPosted))
                .ForMember(dest =>dest.MediaFiles, opt=>opt.Ignore());

            // Post to PostDTO
            CreateMap<Post, PostDTO>()
                .ForMember(dest => dest.Channel, opt => opt.MapFrom(src => src.Channel))
                .ForMember(dest => dest.AdminWhoPosted, opt => opt.MapFrom(src => src.AdminWhoPosted));

            // ChannelDTO to Channel
            CreateMap<ChannelDTO, Channel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PrimaryAdmin, opt => opt.MapFrom(src => src.PrimaryAdmin))
                .ForMember(dest => dest.Subscribers, opt => opt.Ignore())
                .ForMember(dest => dest.Admins, opt => opt.Ignore())
                .ForMember(dest => dest.Posts, opt => opt.Ignore());
            
            // Channel to ChannelDTO
            CreateMap<Channel, ChannelDTO>()
                .ForMember(dest => dest.PrimaryAdmin, opt => opt.MapFrom(src => src.PrimaryAdmin));

            // TelegramChannelDTO to TelegramChannel
            CreateMap<TelegramChannelDTO, TelegramChannel>()
                .IncludeBase<ChannelDTO,Channel>();

            // TelegramChannel to TelegramChannelDTO
            CreateMap<TelegramChannel, TelegramChannelDTO>()
                .IncludeBase<Channel, ChannelDTO>();

            //MediaFileDTO to MediaFile
            CreateMap<MediaFileDTO, MediaFile>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Post, opt => opt.MapFrom(src => src.Post));

            //MediaFile to MediaFileDTO
            CreateMap<MediaFile, MediaFileDTO>()
                .ForMember(dest => dest.Post, opt => opt.MapFrom(src => src.Post));

        }
    }
}