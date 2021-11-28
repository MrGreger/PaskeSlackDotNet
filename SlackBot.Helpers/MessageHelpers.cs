using HttpSlackBot.Messaging;
using HttpSlackBot.Options;
using System;
using System.Threading.Tasks;

namespace SlackBot.Helpers
{
    public static class MessageHelpers
    {
        public static async Task<SlackResponseBase> FastSlackMessage<T>(this SlackMessageSender<T> messageSender, string text, string channelId, string threadId = null, string blocks = null) where T : class, IBotOptions
        {
            var message = new SlackMessage
            {
                Blocks = blocks,
                Text = text,
                Channel = channelId,
                ThreadTs = threadId
            };

            return await messageSender.SendSlackMessage(message);
        }

        public static async Task<SlackResponseBase> FastEphemeralMessage<T>(this SlackMessageSender<T> messageSender, string text, string channelId, string userId, string threadId = null, string blocks = null) where T : class, IBotOptions
        {
            var resultMessage = new EphemeralMessage
            {
                Text = text,
                Channel = channelId,
                User = userId,
                ThreadTs = threadId
            };

            return await messageSender.SendEphemeralMessage(resultMessage);
        }
    }
}
