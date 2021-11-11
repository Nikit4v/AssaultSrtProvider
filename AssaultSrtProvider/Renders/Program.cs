using System;
using System.IO;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Design;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace Renders
{
    class Program
    {
        public static Test Test = new Test();
        public static RSw RSw = new RSw();
        static void Main()
        {
            Test.rendtest();
        }
        
    }
    class Test
    {
        
        public Snapshot test = new Snapshot();
        public static Style Style = new Style(@"C:\\Users\\inovi\\AppData\\Local\\Microsoft\\Windows\\Fonts\\19838.ttf");
        public static Style AnyStyle = new Style(@"C:\\Users\\inovi\\AppData\\Local\\Microsoft\\Windows\\Fonts\\shanghai_rus.otf") {FontSize = 60,BorderSize = new int[] {10,1} };
        public Tag tt = new Tag("text", Style, new int[] { 200, 200 });
        public Tag ttt = new Tag("AnyText", AnyStyle, new int[] { 100, 600 });
        public Render Render = new Render(@"D:\\user\\Pictures\\video_2021-11-07_05-42-17.mp4", @"H:\\Assault[0.1]\\AssaultRenders\\Renders");
        public void rendtest()
        {
             test.Tags = new Tag[] { tt,ttt };
             test.Start = 1;
             test.End = 100;
             Render.Save_Frame(Render.Rend_Frame(new Snapshot[] { test }, 300));

        }
    }
}
