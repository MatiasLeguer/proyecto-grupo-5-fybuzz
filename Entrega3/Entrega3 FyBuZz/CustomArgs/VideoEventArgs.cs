﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entrega3_FyBuZz.CustomArgs
{
    public class VideoEventArgs : EventArgs
    {
        public string NameText { get; set; }
        public string ActorsText { get; set; }
        public string DirectorsText { get; set; }
        public string ReleaseDateText { get; set; }
        public string DimensionText { get; set; }
        public string QualityText { get; set; }
        public string Categorytext { get; set; }
        public string DescriptionText { get; set; }
        public string VideoImage { get; set; }  /* Falta pasar bien la imagen */
        public string DurationText { get; set; }
        public string FormatText { get; set; }
        public string SubtitlesText { get; set; }
        public string FileDestText { get; set; }
        public string FileNameText { get; set; }

    }
}