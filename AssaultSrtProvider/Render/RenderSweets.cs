using System.Collections.Generic;
using System.Drawing;

namespace AssaultSrtProvider.Render
{
    class RSw
    {
        public SizeF GetBackFrame(Tag tag)
        {
            var fontp = new PrivateFontCollection();
            fontp.AddFontFile(tag.Style.FontFile);
            var i = fontp.Families[0].Name;
            var fontf = new FontFamily(i, fontp);
            var font = new Font(fontf, 12);
            SizeF ts = Graphics.FromImage(new Bitmap(1, 1)).MeasureString(tag.Text, font);
            return (ts);
        }
        public List<float>[] GetyoutubeBackFrame(Tag tag)
        {
            SizeF size = GetBackFrame(tag);
            List<List<float>> points = new List<List<float>>();
            var tn = tag.Text.Split("\n");
            for(var i = 0;i < tn.Length; i++)
            {
                points.Add(new List<float> { tag.Style.Position[0] + size.Height * i, tag.Style.Position[1], tn[i].Length * size.Width, size.Height });
            }
            return (points.ToArray());
        }
    }
}