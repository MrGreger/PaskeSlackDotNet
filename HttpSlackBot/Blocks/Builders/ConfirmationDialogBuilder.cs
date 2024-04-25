using HttpSlackBot.Blocks.Dialog;

namespace HttpSlackBot.Blocks.Builders
{
    public class ConfirmationDialogBuilder
    {
        private ConfirmationDialog _confirmationDialog = new ConfirmationDialog();

        public ConfirmationDialogBuilder WithTitle(string title, bool emoji = true)
        {
            _confirmationDialog.Title = new PlainText(emoji)
            {
                Value = title
            };
            return this;
        }

        public ConfirmationDialogBuilder WithText(string text, bool emoji = true)
        {
            _confirmationDialog.Text = new PlainText(emoji)
            {
                Value = text
            };
            return this;
        }

        public ConfirmationDialogBuilder WithConfirm(string confirmText, bool emoji = true)
        {
            _confirmationDialog.Confirm = new PlainText(emoji)
            {
                Value = confirmText
            };
            return this;
        }

        public ConfirmationDialogBuilder WithDeny(string denyText, bool emoji = true)
        {
            _confirmationDialog.Deny = new PlainText(emoji)
            {
                Value = denyText
            };
            return this;
        }

        public ConfirmationDialogBuilder WithStyle(string style)
        {
            _confirmationDialog.Style = style;
            return this;
        }

        public ConfirmationDialog Build()
        {
            return _confirmationDialog;
        }
    }
}