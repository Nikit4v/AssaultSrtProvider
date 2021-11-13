


namespace Renders
{
    class Tag
    {
        public Style Style;
        public string Text;
        public int[] Position;
        public Tag(string Text,Style Style,int[] Position)
        {
            this.Style = Style;
            this.Text = Text;
            this.Position = Position;
        }
    }
}