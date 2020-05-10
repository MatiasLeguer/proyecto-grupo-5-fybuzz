using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FyBuzz_E2
{
    public class Video:Multimedia
    {
        protected int videoDimension;
        protected string quality;
        protected int memorySize; //No se si colocarlo o no en el constructor ya que nadie pone el peso de su video youtube. Se pone solo
        protected string category;
        protected string description;
        protected string rated;
        protected string image;
        protected bool subtitles; // 
        protected string actors;
        protected string directors;


        public Video(string name,string actors, string directors string date, int videoDimension, string quality, string category, string description, string rated, string image, string ranking, double duration, bool subtitles, string format,)
        {
            this.name = name;
            this.date = date;
            this.videoDimension = videoDimension;
            this.quality = quality;
            this.category = category;
            this.description = description;
            this.rated = rated;
            this.image = image;
            this.ranking = ranking;
            this.duration = duration;
            this.subtitles = subtitles;
            this.format = format;
            this.actors = actors;
            this.directors = directors;

        }

        public delegate void SendVideoEventHandler(object source, Video v);
        public event SendVideoEventHandler VideoSent;

        //Verificar si es que funciona este evento, debido a que colocque en vez de EventArgs args, puse Song cancion y no se si sirve
        protected virtual void OnVideoSent(Video v)
        {
            if (VideoSent != null)
            {
                VideoSent(this, v);
            }
        }


        public List<string> InfoVideo()
        {
            return new List<string>() { name, quality, category, rated, ranking }; //Agregar más atributos?
        }
        public List<int> InfoRep()
        {
            return new List<int>() { generalRep, profileRep };
        }
        public string SearchedInfoVideo()
        {
            return "Video: " + name + "\tActors: " + actors + "\tDirectors: " + directors;
        }

        //Ver que más métodos podemos poner? o que sea solo clase constructora.
    }
}
