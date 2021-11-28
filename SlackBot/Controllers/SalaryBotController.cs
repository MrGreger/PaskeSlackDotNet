using AutoMapper;
using HttpSlackBot;
using HttpSlackBot.Commands;
using HttpSlackBot.Events;
using HttpSlackBot.Interactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SlackBot.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SlackBot.Controllers
{
    [Route("api/salary-bot")]
    [ApiController]
    public class SalaryBotController : ControllerBase
    {
        private SlackCallbackDispatcher _callbackDispatcher;
        private CommandDispatcher _commandDispatcher;
        private InteractionDispatcher _interactionDispatcher;
        private IMapper _mapper;

        public SalaryBotController(SlackCallbackDispatcher callbackDispatcher, IMapper mapper, CommandDispatcher commandDispatcher, InteractionDispatcher interactionDispatcher)
        {
            _callbackDispatcher = callbackDispatcher;
            _mapper = mapper;
            _commandDispatcher = commandDispatcher;
            _interactionDispatcher = interactionDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> Post(JObject payload)
        {
            var slackBaseEvent = payload.ToObject<SlackCallback>();

            if (slackBaseEvent == null)
            {
                return BadRequest();
            }

            if (CheckIsChallangeEvent(slackBaseEvent))
            {
                return Ok(slackBaseEvent.Challenge);
            }

            await _callbackDispatcher.DispatchCallaback(slackBaseEvent);

            return Ok();
        }

        [HttpPost("command")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<IActionResult> ProcessCommand([FromForm] SlackCommandDTO data)
        {
            var command = _mapper.Map<SlackCommand>(data);

            await _commandDispatcher.DispatchCommand(command);

            return Ok();
        }

        [HttpPost("interactivity")]
        public async Task<IActionResult> ProcessInteractivity([FromForm] string payload)
        {
            var interaction = JsonConvert.DeserializeObject<InteractionEvent>(payload);

            await _interactionDispatcher.DispatchCommand(interaction);

            return Ok();
        }

        private bool CheckIsChallangeEvent(SlackCallback @event)
        {
            return @event.Type.Equals("url_verification", StringComparison.OrdinalIgnoreCase);
        }
    }
}
