using System;
using System.IO;
using SkiaSharp;
using AssaultSrtProvider.Representation;

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
        /// <param name="inputFrame">Байты фрейма</param>
        /// <param name="snapshot">Снапшот для рендеринга</param>
        /// <returns>Новый фрейм в виде байтов</returns>
        /// <comment>
        /// Рекомендую (для разделения кода + для лёгкости перестройки в случае чего) сразу написать RenderTag.
        /// Также стоит учесть, что перетаскивание байтов из подконтрольной памяти в свободную процесс сложный, и его тоже лучше
        /// прописать отдельным методом. (подробнее: https://stackoverflow.com/questions/48017310/how-to-convert-a-byte-array-to-skbitmap-in-skiasharp)
        /// </comment>
        public byte[] RenderSnapshot(byte[] inputFrame, Snapshot snapshot)
        {
            var surface = SKSurface.Create(new SKImageInfo(_resolution.Item1, _resolution.Item2));
            var canvas = surface.Canvas;
            var style = snapshot.Style;
            foreach(var tag in snapshot.Tags)
            {
                // Оно с твоими структурами не робит! <-!!!
                // Так и не должно!!! Я переписать просил!!!
                //if (style.BorderInfo != null)
                //{
                //    canvas.DrawImage(SKImage.FromBitmap(SKBitmap.Decode(style.BorderInfo.image)), layer.BorderInfo.position[0], layer.BorderInfo.position[0]);
                //}
                var paint = new SKPaint(SKTypeface.FromFile(style.FontFile).ToFont());
                paint.Style = (SKPaintStyle)style.TextType;
                paint.TextSize = style.FontSize;
                var color = style.Color;
                paint.Color = new SKColor(color[0], color[1], color[2], color[3]);
                paint.StrokeWidth = style.BorderSize;
                paint.MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Inner, style.Blur, false);
                ///Оно тоже нормально со структурами не робит {
                if (tag.Position != null)
                {
                    canvas.DrawText(tag.Text, tag.Position.Item1, tag.Position.Item2, paint);
                }
                else
                {
                    canvas.DrawText(tag.Text,_resolution.Item1-(tag.Text.Length*style.FontSize/4), _resolution.Item2-style.FontSize*4, paint);
                }
                ///}
            }
            return (SKBitmap.FromImage(surface.Snapshot()).Bytes);
        }

        protected Renderer((int, int) resolution)
        {
            _resolution = resolution;
        }


    }
}