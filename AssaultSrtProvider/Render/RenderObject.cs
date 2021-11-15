using System;
using AssaultSrtProvider.Representation;

namespace AssaultSrtProvider.Render
{
    /// <summary>
    /// По сути упрощение (ну или слой абстракции, как хочешь)
    /// </summary>
    public struct RenderObject
    {
        /// <summary>
        /// Bitmap уже отрисованного текста (скорее всего, но наверно можно попроавить если нужно)
        /// </summary>
        public readonly byte[] Content;
        
        /// <summary>
        /// Обычный стиль. Используй как сичтаешь нужным
        /// </summary>
        public readonly Style Style;

        public RenderObject(byte[] content, Style style)
        {
            Content = content;
            Style = style;
        }

        /// <summary>
        /// Получение набора объектов-слоёв из тега
        /// </summary>
        /// <param name="tag">Тег для парсинга</param>
        /// <returns>Набор объектов-слоёв</returns>
        public static RenderObject[] FromTag(Tag tag)
        {
            throw new NotImplementedException(); // Это значит, что тебе нужно его сделать!!!
        }
    }
}