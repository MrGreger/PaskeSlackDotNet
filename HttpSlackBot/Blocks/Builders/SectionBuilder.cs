using System;
using HttpSlackBot.Blocks.BaseBlocks;

namespace HttpSlackBot.Blocks.Builders
{
    public class SectionBuilder
    {
        private Section _section = new Section();

        public SectionBuilder AddButton(Action<ButtonBuilder> builder)
        {
            var buttonBuilder = new ButtonBuilder();

            builder.Invoke(buttonBuilder);

            _section.Acessory = buttonBuilder.Build();
            
            return this;
        }

        public SectionBuilder AddCheckBoxes()
        {
            return this;
        }
        
        public Section Build()
        {
            return _section;
        }
    }
}