using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FyBuzz_E2
{
    [Serializable]
    public class Multimedia
    {
        protected string name;
        protected string date;
        protected string ranking;
        protected int likes;
        protected int generalRep;
        protected int profileRep;
        protected double duration;
        protected double presentTime;
        protected string format;

        public double Duration { get => duration; }
        public string Name { get => name; }

        public string AddMult()
        {
            throw new NotImplementedException();
        }

        public string DeleteMult()
        {
            throw new NotImplementedException();
        }

        public bool VerifyMult()
        {
            throw new NotImplementedException();
        }

        //InfoMult()
        //Todavia no sabemos como colocar un archivo srt o una lyric de una cancion, entonces no puedo hacer display (lyrics esta en bool)




    }
}
