using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SlackBot.Common.EventSending
{
    public interface IEventsSender
    {
        Task SendEvent<T>(T @event);
    }
}
