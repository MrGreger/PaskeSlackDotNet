using HttpSlackBot.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HttpSlackBot.CommandHandlers
{
    public interface ICommandHandler
    {
        Task HandleCommand(SlackCommand payload);
    }
}
