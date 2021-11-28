using FluentValidation.Results;
using HttpSlackBot.Blocks;
using HttpSlackBot.Interactions;
using HttpSlackBot.Interactions.Elements;
using HttpSlackBot.Messaging;
using HttpSlackBot.Models;
using Newtonsoft.Json;
using ShopList.Application.AppSettings;
using SlackBot.Common;
using SlackBot.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryBot.InteractionHandlers
{
    public class CreateRequestEvent
    {
        public PlainTextInputElement WorkTime { get; set; }
        public PlainTextInputElement PaymentReason { get; set; }
        public PlainTextInputElement ResultUrl { get; set; }
        public UsersMultiSelect WhoAccepted { get; set; }
        public PlainTextInputElement WherePay { get; set; }
        public DatePicker PayUntil { get; set; }
        public CheckboxesElement WorkTested { get; set; }
    }

    [InteractionHandler("request-payment")]
    public class CreateRequestInteractionHandler : InteractionHandler<CreateRequestEvent>
    {
        private SalaryBotMessageSender _messageSender;
        private IAppSettingsStorage _appSettingsStorage;

        public CreateRequestInteractionHandler(SalaryBotMessageSender messageSender, IAppSettingsStorage appSettingsStorage)
        {
            _messageSender = messageSender;
            _appSettingsStorage = appSettingsStorage;
        }

        protected override async Task OnEvent(InteractionEvent<CreateRequestEvent> payload)
        {
            var validator = new RequestPaymentModelValidator();

            var validationResult = validator.Validate(payload.State.Values);

            if (!validationResult.IsValid)
            {
                await SendErrorMessage(validationResult.Errors, payload.Channel.Id, payload.User.Id);

                return;
            }

            await SendToConfirmation(payload.State.Values, payload.User.Id, payload.Message.Ts, payload.Channel.Id, payload.Action.Value);

            var resultMessage = new EphemeralMessage
            {
                Text = $"Запрос отправлен",
                Channel = payload.Channel.Id,
                User = payload.User.Id

            };

            var response = await _messageSender.SendEphemeralMessage(resultMessage);
        }

        private async Task SendToConfirmation(CreateRequestEvent request, string requestSenderId, string messageTs, string channelId, string replyTs)
        {
            var userProfiles = await GetUserProfiles(request.WhoAccepted.SelectedUsers);

            var whoAccepted = string.Join(',', userProfiles.Select(x => x.RealName));

            var blocks = await BuildRequestSummary(request, requestSenderId, whoAccepted, messageTs);

            var requestReceiptEmail = await _appSettingsStorage.GetAppSettingByIdAsync(SalaryBotAppSettingIds.RequestRecieverEmail, new StringParser());

            var receipt = await _messageSender.GetUserByEmail(requestReceiptEmail);

            var channel = await _messageSender.SendRequestToSlack<ConversationsOpenResponse>("conversations.open", new { users = receipt.Content.Id });

            var requestConfirmationChannelId = channel.Content.Id;

            var message = new SlackMessage
            {
                Blocks = blocks.Serialize(),
                Text = "Please, check request",
                Channel = requestConfirmationChannelId,
                ThreadTs = replyTs
            };

            var messageResult = await _messageSender.SendSlackMessage(message);

            if (!messageResult.Success)
            {
                message = new SlackMessage
                {
                    Blocks = blocks.Serialize(),
                    Text = "Please, check request",
                    Channel = requestConfirmationChannelId
                };

                messageResult = await _messageSender.SendSlackMessage(message);
                await UpdateSendButton(messageResult.Content.Ts, channelId, messageTs);
            }
        }

        private async Task UpdateSendButton(string confirmationTs, string requestChannel, string requestMessageTs)
        {
            var replies = await _messageSender.GetReplies(requestChannel, requestMessageTs);
            var message = replies.Content.First();

            var blocks = JsonConvert.SerializeObject(message.Blocks);

            var newBlocks = blocks.Replace("[thread_placeholder]", confirmationTs);

            var result = await _messageSender.UpdateSlackMessage(new UpdateSlackMessageRequest
            {
                Blocks = newBlocks,
                Channel = requestChannel,
                Ts = requestMessageTs,
                Text = message.Text
            });
        }

        private async Task<BlocksContainer> BuildRequestSummary(CreateRequestEvent request, string requestSenderId, string whoAccepted, string messageTs)
        {
            var senderProfile = await _messageSender.GetUser(requestSenderId);

            var workTested = false;

            if (request.WorkTested.HasValue("WorkTested"))
            {
                workTested = request.WorkTested.GetValue<bool>("WorkTested");
            }

            var blocks = new BlocksContainer();

            blocks.Add(new Header($"Запрос на оплату от {senderProfile.Profile.RealName}"));

            blocks.Add(new Header("Часы работы"));
            blocks.Add(new PlainText(request.WorkTime.Value));
            blocks.Add(new Header("Что оплатить"));
            blocks.Add(new PlainText(request.PaymentReason.Value));
            blocks.Add(new Header("Ссылка на работу"));
            blocks.Add(new PlainText(request.ResultUrl.Value));
            blocks.Add(new Header("Кем принята"));
            blocks.Add(new PlainText(whoAccepted));
            blocks.Add(new Header("Работа протестирована"));
            blocks.Add(new PlainText(workTested ? "да" : "нет"));
            blocks.Add(new Header("Где оплатить"));
            blocks.Add(new PlainText(request.WherePay.Value));
            blocks.Add(new Header("Желаемая дата оплаты"));
            blocks.Add(new PlainText(request.PayUntil.SelectedDate.Value.ToShortDateString()));

            blocks.Add(new MultilinePlainTextInput("Ваш комментарий", "PaymentRequestComment"));

            var model = new PaymentRequestInfo { UserId = requestSenderId, RequestFormTs = messageTs };

            var serializedModel = JsonConvert.SerializeObject(model);

            var buttonsSection = new ActionsSection();
            buttonsSection.Elements.Add(new ActionButton("Принять", "accept-payment-request", value: serializedModel));
            buttonsSection.Elements.Add(new ActionButton("Отклонить", "deny-payment-request", value: serializedModel));

            blocks.Add(buttonsSection);
            return blocks;
        }

        private async Task<IEnumerable<Profile>> GetUserProfiles(string[] selectedUsers)
        {
            var profiles = new List<Profile>();

            foreach (var userId in selectedUsers)
            {
                var user = await _messageSender.GetUser(userId);
                profiles.Add(user.Profile);
            }

            return profiles;
        }

        private async Task SendErrorMessage(List<ValidationFailure> errors, string channel, string user)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var error in errors)
            {
                stringBuilder.Append("- ");
                stringBuilder.Append(error.ErrorMessage);
                stringBuilder.Append("\n");
            }

            var message = stringBuilder.ToString();

            var slackMessage = new EphemeralMessage
            {
                Attachments = new object[0],
                Blocks = null,
                Channel = channel,
                User = user,
                Text = message
            };

            await _messageSender.SendEphemeralMessage(slackMessage);
        }
    }
}
