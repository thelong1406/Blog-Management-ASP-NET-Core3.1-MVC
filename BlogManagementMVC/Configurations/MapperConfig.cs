using AutoMapper;
using DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using MapModels.ViewModels;
using MapModels.ViewModels.PostVM;
using BlogManagementMVC.Configurations;

namespace BlogManagementMVC.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Tag, TagVM>().ReverseMap();
            CreateMap<Category, CategoryVM>().ReverseMap();
            CreateMap<Post, PostVM>().ReverseMap();
            CreateMap<Post, ListPostsVM>().ReverseMap();
            CreateMap<Post, PostDetailVM>().ReverseMap();
            CreateMap<Post, PostEditVM>().ReverseMap();
            CreateMap<PostTag, PostTagVM>().ReverseMap();
            CreateMap<PostMeta, PostMetaVM>().ReverseMap();
            CreateMap<PostCategory, PostCategoryVM>().ReverseMap();
            CreateMap<Vote, VoteVM>().ReverseMap();
            CreateMap<PostComment, PostCommentVM>().ReverseMap();
            CreateMap<User, UserVM>().ReverseMap();
            
        }
    }
}
