using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Blocks
{
    public class MultilinePlainTextInput : PlainTextInput
    {
        public MultilinePlainTextInput(string label, string blockName) : base(label, blockName)
        {
            _text.Multiline = true;
        }
    }
}
