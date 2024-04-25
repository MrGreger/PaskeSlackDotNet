using System;
using HttpSlackBot.Blocks.Dialog;

namespace HttpSlackBot.Blocks.Builders
{
    public class ButtonBuilder
    {
        private string _text = null;
        private string _value = null;
        private string _actionId = null;
        private string _url = null;
        private string _style = null;
        private ConfirmationDialog _confirmationDialog = null;
        private string _accessibilityLabel = null;
        private bool _emoji = false;
        private string _blockId;

        public ButtonBuilder WithText(string text)
        {
            _text = text;
            return this;
        }

        public ButtonBuilder WithPayload(string payload)
        {
            _value = payload;
            return this;
        }

        public ButtonBuilder WithActionId(string actionId)
        {
            _actionId = actionId;
            return this;
        }

        public ButtonBuilder WithUrl(string url)
        {
            _url = url;
            return this;
        }

        public ButtonBuilder WithStyle(string style)
        {
            _style = style;
            return this;
        }

        public ButtonBuilder WithAccessibilityLavel(string label)
        {
            _accessibilityLabel = label;
            return this;
        }

        public ButtonBuilder WithEmoji(bool withEmoji = true)
        {
            _emoji = withEmoji;
            return this;
        }

        public ButtonBuilder WithId(string blockId)
        {
            _blockId = blockId;
            return this;
        }

        public ButtonBuilder WithConfirmationDialog(Action<ConfirmationDialogBuilder> dialogBuilder)
        {
            var builder = new ConfirmationDialogBuilder();

            builder.WithTitle("Missing title")
                .WithText("Missing text")
                .WithConfirm("Ok")
                .WithDeny("Cancel");

            dialogBuilder.Invoke(builder);

            _confirmationDialog = builder.Build();

            return this;
        }

        public Button Build()
        {
            if (_text == null)
            {
                throw new Exception("Button text is null! Use WithText to assign text.");
            }

            if (_text.Length > 75)
            {
                throw new Exception("Button text max length is more than 75.");
            }

            return new Button()
            {
                ActionId = _actionId,
                Value = _value,
                Style = _style,
                AccessibilityLabel = _accessibilityLabel,
                Content = new PlainText(_emoji)
                {
                    Value = _text
                },
                Url = _url,
                BlockId = _blockId
            };
        }
    }
}