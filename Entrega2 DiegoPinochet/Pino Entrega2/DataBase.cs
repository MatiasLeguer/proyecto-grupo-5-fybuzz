using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pino_Entrega2
{
    class DataBase: IOperationMult
    {
        protected List<Song> listSongsGlobal;
        protected List<Video> listVideoGlobal; //get y un set
        protected List<Playlist> listPlaylistGlobal;
        protected List<String> gender;

        private Dictionary<int, List<string>> userDataBase;
    }
}
