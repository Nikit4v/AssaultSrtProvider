namespace AssaultSrtProvider.Representation
{
    public struct Tag
    {
        public string Text;
        public dynamic Position;
        public new string ToString()
        {
            return Text;
        }
        public Tag(string text,(int,int) position)
        {
            Text = text;
            Position = position;
        }
        public Tag(string text)
        {
            Text = text;
            Position = null;
        }
    }
}