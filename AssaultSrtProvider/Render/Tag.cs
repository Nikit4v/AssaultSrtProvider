


namespace Renders
{
    class Tag
    {
        public Style[] Styles;
        public string Text;
        public int[] Position;
        public Tag(string Text,Style[] Styles,int[] Position)
        {
            this.Styles = Styles;
            this.Text = Text;
            this.Position = Position;
        }
    }
}