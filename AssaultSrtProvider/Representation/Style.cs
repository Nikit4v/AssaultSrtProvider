namespace AssaultSrtProvider.Representation
{
    public struct BorderInfo
    {
        public byte[] Image;
        public float[] Position;

        public BorderInfo(byte[] image, float[] position)
        {
            Image = image;
            Position = position;
        }
    }
    
    public struct Style
    {
        public string FontFile;
        public int FontSize;
        public int[] Position;
        public byte[] Color;
        public int TextType;
        public float Blur;
        public float BorderSize;
        public BorderInfo BorderInfo;
        
        
        public Style(string fontFile, int fontSize, int[] position, byte[] color, int textType, float blur, float borderSize, BorderInfo borderInfo)
        {
            FontFile = fontFile;
            FontSize = fontSize;
            Position = position;
            Color = color;
            TextType = textType;
            Blur = blur;
            BorderSize = borderSize;
            BorderInfo = borderInfo;
        }
    }
}