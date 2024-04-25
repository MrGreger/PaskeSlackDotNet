namespace HttpSlackBot.Blocks
{
    public class PlainText : TextAttribute
    {
        public PlainText(bool emoji) : base(TextTypes.Plain, emoji)
        {
        }
    }
}