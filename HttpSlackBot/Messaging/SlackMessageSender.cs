using HttpSlackBot.Models;
using HttpSlackBot.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpSlackBot.Messaging
{
    public class SlackMessageSender<TOptions> where TOptions : class, IBotOptions
    {
        private TOptions _botOptions;
        private HttpClient _httpClient;

        public SlackMessageSender(IOptions<TOptions> botOptions, IHttpClientFactory httpClient)
        {
            _botOptions = botOptions.Value;
            _httpClient = httpClient.CreateClient();
        }

        public async Task<TResponse> SendRequestToSlack<TResponse>(string method, object data)
        {
            var requestContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _botOptions.Token);

            var response = await _httpClient.PostAsync($"https://slack.com/api/{method}", requestContent);

            var content = await response.Content.ReadAsStringAsync();


            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }

            var result = JsonConvert.DeserializeObject<TResponse>(content);

            return result;
        }

        public async Task<SlackResponseBase> SendEphemeralMessage(EphemeralMessage message)
        {
            return await SendRequestToSlack<SlackResponseBase>("chat.postEphemeral", message);
        }

        public async Task<ChatPostMessageResponse> SendSlackMessage(SlackMessage message)
        {
            return await SendRequestToSlack<ChatPostMessageResponse>("chat.postMessage", message);
        }
        public async Task<ChatPostMessageResponse> UpdateSlackMessage(UpdateSlackMessageRequest message)
        {
            return await SendRequestToSlack<ChatPostMessageResponse>("chat.update", message);
        }

        public async Task<ConversationsRepliesResponse> GetReplies(string channel, string ts)
        {
            return await SendRequestToSlack<ConversationsRepliesResponse>($"conversations.replies?channel={channel}&ts={ts}", null);
        }

        public async Task<User> GetUser(string userId)
        {
            var getUserResponse = await SendRequestToSlack<UsersInfoResponse>($"users.info?user={userId}", null);
            return getUserResponse.Content;
        }

        public async Task<UsersLookupByEmailResponse> GetUserByEmail(string email)
        {
            return await SendRequestToSlack<UsersLookupByEmailResponse>($"users.lookupByEmail?email={email}", null);
        }

        public async Task<ConversationsOpenResponse> OpenConversation(string channelId = null, params string[] userIds)
        {
            if (string.IsNullOrWhiteSpace(channelId))
            {
                var usersList = string.Join(',', userIds);

                return await SendRequestToSlack<ConversationsOpenResponse>("conversations.open", new { users = usersList });
            }
            else
            {
                return await SendRequestToSlack<ConversationsOpenResponse>("conversations.open", new { channel = channelId });
            }
        }

        public async Task<IEnumerable<User>> GetUserProfiles(IEnumerable<string> userEmails)
        {
            var profiles = new List<User>();

            foreach (var userEmail in userEmails)
            {
                var user = await GetUserByEmail(userEmail);
                profiles.Add(user.Content);
            }

            return profiles;
        }
    }
}
