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

        public byte[] RendSnapshot(Snapshot snapshot, byte[] rawframe)
        {
            /// <summary>
            /// Функция рендеринга Snapshot, как понятно из названия, поверх фрейма видео
            /// </summary>
            /// <param name="snapshot">Внезапно Snapshot</param>
            /// <param name="snapshot">На удивление сырые байты Фрейма </param>
            /// <returns>byte[] Фрейма с отрендереными тегами</returns>
            var surface = SKSurface.Create(new SKImageInfo(_resolution.Item1, _resolution.Item2));
            var canvas = surface.Canvas;
            foreach(var tag in snapshot.Tags)
            {
                foreach(var lay in tag.Styles)
                {
                    if (lay.BorderInfo != null)
                    {
                        canvas.DrawImage(SKImage.FromBitmap(SKBitmap.Decode(lay.BorderInfo.image)),lay.BorderInfo.position[0], lay.BorderInfo.position[0]);
                    }
                    var paint = new SKPaint(SKTypeface.FromFile(lay.FontFile).ToFont());
                    paint.Style = (SKPaintStyle)lay.TextType;
                    paint.TextSize = lay.FontSize;
                    var Color = lay.Color;
                    paint.Color = new SKColor(Color[0], Color[1], Color[2], Color[3]);
                    paint.StrokeWidth = lay.BorderSize;
                    paint.MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Inner, lay.blur, false);
                    canvas.DrawText(tag.Text, tag.Position[0]+lay.position[0], tag.Position[1]+lay.position[1], paint);
                }
            }
            return (SKBitmap.FromImage(surface.Snapshot()).Bytes);
        }
    }
}