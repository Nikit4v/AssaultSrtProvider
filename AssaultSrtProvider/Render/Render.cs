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
            foreach(var tag in snapshot.Tags)
            {
                foreach(var layer in RenderObject.FromTag(tag))
                    // Вместо старого
                    // foreach(var layer in tag.styles)
                {
                    if (layer.BorderInfo != null)
                    {
                        canvas.DrawImage(SKImage.FromBitmap(SKBitmap.Decode(layer.BorderInfo.image)),layer.BorderInfo.position[0], layer.BorderInfo.position[0]);
                    }
                    var paint = new SKPaint(SKTypeface.FromFile(layer.FontFile).ToFont());
                    paint.Style = (SKPaintStyle) layer.TextType;
                    paint.TextSize = layer.FontSize;
                    var color = layer.Color;
                    paint.Color = new SKColor(color[0], color[1], color[2], color[3]);
                    paint.StrokeWidth = layer.BorderSize;
                    paint.MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Inner, layer.blur, false);
                    canvas.DrawText(tag.Text, tag.Position[0]+layer.position[0], tag.Position[1]+layer.position[1], paint);
                }
            }
            return (SKBitmap.FromImage(surface.Snapshot()).Bytes);
        }

        protected Renderer((int, int) resolution)
        {
            _resolution = resolution;
        }


    }
}