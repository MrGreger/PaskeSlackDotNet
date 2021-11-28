using HttpSlackBot.Blocks.BaseBlocks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Blocks
{
    public class PlainTextInput : InputBase
    {
        public PlainTextInput(string label, string blockName) : base(blockName)
        {
            Label.Value = label;
        }

        protected TextInput _text = new TextInput(false);

        public override BlockBase Element => _text;
    }
}
