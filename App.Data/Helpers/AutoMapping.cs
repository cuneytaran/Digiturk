using AutoMapper;
using App.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using App.Data.Dtos;

namespace App.Data.Helpers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Articles, ArticleDto>().ReverseMap();
            CreateMap<Comments, CommentDto>().ReverseMap();
        }
    }
}
