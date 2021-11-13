using System.IO;
using Renders;
using SkiaSharp;

namespace AssaultSrtProvider.Render
{
    abstract class Renderer
    {
        // Используй этот класс как шаблон (сохраняя имена и реализуя хотя-бы представленные варианты функций)
        public abstract byte[] RenderSnapshot(byte[] inputFrame, Snapshot snapshot);
    }

    class Render
    {
        public string TempFolderPath;
        public Slicer Slicer;
        public Render(string VideoFilePath, string TempFolderPath)
        {
            this.Slicer = new Slicer(VideoFilePath, TempFolderPath);
            this.TempFolderPath = TempFolderPath;
        }
        public void Rend_Snapshot(Snapshot snapshot,SKCanvas canvas)
        {
            foreach(var tag in snapshot.Tags)
            {
                for(int i = 0; i<tag.Style.TextStyles.Length; i++)
                {
                    var paint = new SKPaint(SKTypeface.FromFile(tag.Style.FontFile).ToFont());
                    paint.Style = (SKPaintStyle)tag.Style.TextStyles[i];
                    paint.TextSize = tag.Style.FontSize;
                    var Color = tag.Style.Colors[i];
                    paint.Color = new SKColor(Color[0], Color[1], Color[2], Color[3]);
                    paint.StrokeWidth = tag.Style.BorderSize[i];
                    paint.MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Inner, tag.Style.blur[i], false);
                    canvas.DrawText(tag.Text, tag.Position[0], tag.Position[1], paint);
                }
            }
        }
        public void SaveFrame(SKSurface surface)
        {
            SKImage image = surface.Snapshot();
            var data = image.Encode(SKEncodedImageFormat.Png, 80);
            var stream = File.OpenWrite(Path.Combine(TempFolderPath, "frame.png"));
            data.SaveTo(stream);
        }
        
        public SKSurface RenderFrame(Snapshot[] snapshots, double time, float x = 0.0f, float y = 0.0f)
        {
            
            var frame = SKBitmap.Decode(Slicer.get_frame(time));
            //var frame = SKBitmap.Decode(@"D:\user\Pictures\cs.png");
            var info = new SKImageInfo(frame.Width, frame.Height);
            var surface = SKSurface.Create(info);
            var canvas = surface.Canvas;
            canvas.DrawBitmap(frame, x,y);
            foreach(var snapshot in snapshots)
            {
                Rend_Snapshot(snapshot, canvas);
            }
            return (surface);
        }
    }
}