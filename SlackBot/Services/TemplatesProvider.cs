using AutoMapper;
using HttpSlackBot.Templates;
using Microsoft.EntityFrameworkCore;
using SlackBot.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlackBot.Services
{
    public class TemplatesProvider : ITemplatesProvider
    {
        private SlackContext _slackContext;
        private IMapper _mapper;

        public TemplatesProvider(SlackContext slackContext, IMapper mapper)
        {
            _slackContext = slackContext;
            _mapper = mapper;
        }

        public async Task<HttpSlackBot.Templates.SlackMessageTemplate> GetMessageTemplate(string templateName)
        {
            var template = await _slackContext.Templates.Where(x => EF.Functions.ILike(x.TemplateName, $"{templateName}")).FirstAsync();

            return _mapper.Map<HttpSlackBot.Templates.SlackMessageTemplate>(template);
        }
    }
}
