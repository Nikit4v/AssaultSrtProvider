using System;
using System.Collections.Generic;

namespace Renders
{
    class Style
    {
        /// <summary>
        /// Стиль, содержащий все параметры для рендеринга тегов
        /// </summary>
        /// <param name="FontFile"> Путь до файла со шрифтом </param>
        /// <param name="FontSize"> размер шрифта </param>
        /// <param name="position"> Смещение относительно позиции тега </param>
        /// <param name="Color"> Цвет  </param>
        /// <param name="TextType"> типы текста в соответствии с SKPaintStyle </param>
        /// <param name="blur"> Блюр </param>
        /// <param name="BorderSize"> Размер обводки текста </param>
        /// <param name="BorderInfo"> Объект хранящий параметры для рисования картинок и фори позади текста </param>
        /// <returns> Данные для рендеринга </returns>

        public string FontFile;
        public int FontSize;
        public int[] position;
        public byte[] Color;
        public int TextType;
        public float blur;
        public float BorderSize;
        public BorderInfo BorderInfo = null;
        public Style(string FontFile)
        {
            this.FontFile = FontFile;
            this.FontSize = 12;
            this.position = new int[] {0,0};
            this.Color = new byte[] { 225,225,225,225};
            this.TextType = 0;
            this.blur = 0.0f;
            this.BorderSize = 1;
        }
    }
    class BorderInfo
    {
        public byte[] image;
        public float[] position;

        public BorderInfo()
        {
            
        }
    }
}