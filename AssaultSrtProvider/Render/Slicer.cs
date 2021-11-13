/*using System;
using System.IO;
using System.Collections.Generic;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;
using MediaToolkit.Util;

namespace Renders
{
    class Slicer
    {
        private string TempFolderPath;
        public string VideoFile;
        public Engine MTkEngine;
        public Slicer(string videofile, string TempFolderPath)
        {
            this.TempFolderPath = TempFolderPath;
            this.VideoFile = videofile;
            this.MTkEngine = new Engine();
        }
        public string get_frame(double time)
        {
            var video = new MediaFile(VideoFile);
            var rawframe = new MediaFile(TempFolderPath + "rawframe.png");
            MTkEngine.GetMetadata(video);
            if (video.Metadata.Duration.TotalMilliseconds >= time)
            {
                MTkEngine.GetThumbnail(video, rawframe, new ConversionOptions() { Seek = TimeSpan.FromMilliseconds(time) }) ;
            }
            else
            {
                Console.WriteLine($"Time ({time}) is above of video lenth ({video.Metadata.Duration.TotalMilliseconds})");
                MTkEngine.GetThumbnail(video, rawframe, new ConversionOptions());
            }
            
            return (rawframe.Filename);
        }
    }
}*/