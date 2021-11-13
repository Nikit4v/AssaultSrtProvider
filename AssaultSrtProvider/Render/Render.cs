using System;
using System.IO;
using Renders;
using SkiaSharp;

namespace AssaultSrtProvider.Render
{
    /// <summary>
    /// Используй этот класс как шаблон (сохраняя имена и реализуя хотя-бы представленные варианты функций)
    /// </summary>
    abstract class Renderer
    {
        private readonly (int, int) _resolution;

        /// <summary>
        /// Рендеринг картинки на поверх другой картинки. Пока будем байты гонять, но если найдём адекватную либу, то перейдём на неё
        /// </summary>
        /// <param name="inputFrame">Байты фрейма. Размер не указан, но всегда можно прочитать из `_resolution`</param>
        /// <param name="snapshot">собственно сам снапшот для рендера</param>
        /// <returns>Новый фрейм в виде байтов</returns>
        /// <comment>
        /// Рекомендую (для разделения кода + для лёгкости перестройки в случае чего) сразу написать RenderTag.
        /// Также стоит учесть, что перетаскивание байтов из подконтрольной памяти в свободную процесс сложный, и его тоже лучше
        /// прописать отдельным методом. (подробнее: https://stackoverflow.com/questions/48017310/how-to-convert-a-byte-array-to-skbitmap-in-skiasharp)
        /// </comment>
        public abstract byte[] RenderSnapshot(byte[] inputFrame, Snapshot snapshot);

        protected Renderer((int, int) resolution)
        {
            _resolution = resolution;
        }
    }

    class Render /* : Renderer */
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