namespace AssaultSrtProvider.Representation
{
    public struct Tag
    {
        public string Text;

        public new string ToString()
        {
            return Text;
        }
        public Tag(string text)
        {
            Text = text;
        }

    }
}