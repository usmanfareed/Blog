using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Blog.Models;

namespace Blog.WebUI.App_Start
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            Mapper.Initialize(x=>x.CreateMap<Post,Post>());
            Mapper.Initialize(x=>x.CreateMap<User,User>());
            Mapper.Initialize(x=>x.CreateMap<Tag, Tag>());
        }
    }
}