using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FyBuzz_E2
{
    [Serializable]
    public class Video:Multimedia
    {
    //ATRIBUTOS:

        protected string[] videoDimension;
        protected string[] quality;
        protected int memorySize; //No se si colocarlo o no en el constructor ya que nadie pone el peso de su video youtube. Se pone solo
        protected string category;
        protected string description;
        protected int rated;
        protected bool image;
        protected string subtitles;
        protected string actors;
        protected string directors;
        //--------------------------------------------------------------------------------------------------

        //GETTERS Y SETTERS:
        //--------------------------------------------------------------------------------------------------
        public string Subtitles { get => subtitles; }
        public string Category { get => category; }
        //--------------------------------------------------------------------------------------------------

        //CONSTRUCTOR:
        //--------------------------------------------------------------------------------------------------
        public Video(string name, string actors, string directors ,string date, string videoDimension, string quality, string category, string description, bool image, double duration, string subtitles, string format)
        {
            this.name = name;
            this.date = date;
            this.videoDimension = videoDimension.Split(':');
            this.quality = quality.Split('x');
            this.category = category;
            this.description = description;
            this.rated = 0;
            this.image = image;
            this.ranking = 0;
            this.duration = duration;
            this.subtitles = subtitles;
            this.format = format;
            this.actors = actors;
            this.directors = directors;

        }
        //--------------------------------------------------------------------------------------------------

    //MÉTODOS:

        //MÉTODOS DE INFORMACIÓN
        //--------------------------------------------------------------------------------------------------
        public List<string> InfoVideo()                    //Entrega una lista de strings con la infromación de la clase video.
        {
            return new List<string>() { name, actors, directors, quality[0] + ":" + quality[1], category, rated.ToString(), ranking.ToString(), description}; //Agregar más atributos?
        }
        public string DisplayInfoVideo()                   //Entrega un string con la información de la clase video
        {
            return "Name: " + name + "\tActors: " + actors + "\nDirectors: " + directors + "\tQuality: " + quality + "\nCategory: " + rated + "\tRating: " + rated + "\nRanking: " + ranking;
        }
        public List<int> InfoRep()                         //Entrega una lista de int con la información de las reproducciones generales y del perfil.
        {
            return new List<int>() { generalRep, profileRep };
        }
        public string SearchedInfoVideo()                  //Entrega la información básica cuando se busca un video.
        {
            return "Video: " + name + "\tActors: " + actors + "\tDirectors: " + directors;
        }
        //--------------------------------------------------------------------------------------------------


        /* Ver que más métodos podemos poner? o que sea solo clase constructora. */
    }
}






//ESTABA EN LINEA 52
/*
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
*/
