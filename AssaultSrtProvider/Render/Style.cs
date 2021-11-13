using System;
using System.Collections.Generic;

namespace Renders
{
    class Style
    {
        
        public string FontFile;
        public int FontSize = 100;
        public int[] Position;
        public float[] blur;
        public int[] TextStyles;
        public List<byte[]> Colors = new List<byte[]>();
        public int[] BorderSize;
        public List<float>[] ElementFrames;


        public Style(string FontFile)
        {
            this.TextStyles = new int[] { 0, 1 };
            this.FontFile = FontFile;
            this.Colors.Add(new byte[] { 225, 225, 225, 225 });
            this.Colors.Add(new byte[] { 0, 0, 225, 225 });
            this.Colors.Add(new byte[] { 0, 225, 0, 225 });
            this.BorderSize = new int[] { 1, 5 };
            this.blur = new float[] { 0.0f, 0.0f };
            this.Position = new int[] { 100, 100 };
        }
    }
    public class ImageStyle
    {
        public float[] position;
        public float[] resize = null;
        public string FilePath;

    }
}