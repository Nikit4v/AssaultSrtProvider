using System;
using System.Collections.Generic;

namespace Renders
{
    class Style
    {
        /// <summary>
        /// �����, ���������� ��� ��������� ��� ���������� �����
        /// </summary>
        /// <param name="FontFile"> ���� �� ����� �� ������� </param>
        /// <param name="FontSize"> ������ ������ </param>
        /// <param name="position"> �������� ������������ ������� ���� </param>
        /// <param name="Color"> ����  </param>
        /// <param name="TextType"> ���� ������ � ������������ � SKPaintStyle </param>
        /// <param name="blur"> ���� </param>
        /// <param name="BorderSize"> ������ ������� ������ </param>
        /// <param name="BorderInfo"> ������ �������� ��������� ��� ��������� �������� � ���� ������ ������ </param>
        /// <returns> ������ ��� ���������� </returns>

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