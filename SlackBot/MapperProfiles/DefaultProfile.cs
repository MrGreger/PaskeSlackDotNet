using AutoMapper;
using HttpSlackBot.Commands;
using SlackBot.Database;
using SlackBot.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlackBot.MapperProfiles
{
    public class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            this.CreateMap<SlackCommandDTO, SlackCommand>();
            this.CreateMap<SlackMessageTemplate, HttpSlackBot.Templates.SlackMessageTemplate>();
        }
    }
}
