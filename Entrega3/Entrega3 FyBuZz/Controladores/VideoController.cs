using Entrega3_FyBuZz.CustomArgs;
using Modelos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Entrega3_FyBuZz.Controladores
{
    public class VideoController
    {
        FyBuZz fyBuzz;
        List<Video> videoDataBase = new List<Video>() { new Video("Top 10 N Words", "Barack Obama", "Barack Obama", "31/05/2020", "16:9", "720", "16", "nibba", 0.22, "y", ".mp4", "Top 10 N Words.mp4", true),
                                                        new Video("crash_bandicoot_gameplay", "crash", "Crash bandicoot", "31/05/2020", "16:9", "720", "0", "crash woah", 2.59, "y", ".mov", "crash_bandicoot_gameplay.mov", true),
                                                        new Video("wii-sports-remix", "Wii", "Wii sports", "31/05/2020", "16:9", "720", "0", "wii remix yo", 2.10, "n", ".avi", "wii-sports-remix.avi", true)};
        DataBase database = new DataBase();

        public VideoController(Form fyBuzz)
        {
            Initialize();
            this.fyBuzz = fyBuzz as FyBuZz;
            this.fyBuzz.CreateVideoSaveButton_Clicked += OnCreateVideoSaveButton_Clicked;
            this.fyBuzz.SearchVideoButton_Clicked += OnSearchVideoButton_Clicked;
            this.fyBuzz.GetAllVideosInformation += ReturnAllVideosInfo;
            this.fyBuzz.GetVideoInformation += ReturnVideoInfo;
            this.fyBuzz.PlaysVideoRateButton_Clicked += RateVideo;
            this.fyBuzz.SkipOrPreviousVideoButton_Clicked += OnSkipOrPreviousVideoButton_Clicked;
        }

        public void Initialize()
        {
            if(File.Exists("AllVideos.bin") != true)
            {
                database.Save_Videos(videoDataBase);
            }
            else
            {
                videoDataBase = database.Load_Videos();
            }
        }

        public bool OnCreateVideoSaveButton_Clicked(object sender, VideoEventArgs e)
        {
            File.Copy(e.FileDestText, e.FileNameText);
            List<string> infoMult = new List<string>() {e.NameText, e.ActorsText, e.DirectorsText, e.ReleaseDateText, e.DimensionText, e.QualityText, e.Categorytext, e.DescriptionText, e.DurationText, e.SubtitlesText, e.FormatText, e.FileNameText, e.VideoImage};
            string description = database.AddMult(1, infoMult, null, null, videoDataBase, null, null, null, null);
            if(description == null)
            {
                database.Save_Videos(videoDataBase);
                return true;
            }
            else
            {
                File.Delete(e.FileNameText);
                return false;
            }
        }

        public List<Video> OnSearchVideoButton_Clicked(object sender, VideoEventArgs e)
        {
            return videoDataBase;
        }
        private List<string> ReturnVideoInfo(object sender, VideoEventArgs e)
        {
            List<string> videoInfo = new List<string>();
            foreach (Video video in videoDataBase)
            {

                if (video.Name == e.NameText && video.Actors == e.ActorsText && video.Directors == e.DirectorsText)
                {
                    videoInfo = video.InfoVideo();
                }
            }
            return videoInfo;
        }
        private List<List<string>> ReturnAllVideosInfo(object sender, VideoEventArgs e)
        {
            List<List<string>> allVideosInfo = new List<List<string>>();
            foreach (Video video in videoDataBase)
            {
                allVideosInfo.Add(video.InfoVideo());
            }
            return allVideosInfo;
        }
        private string RateVideo(object sender, VideoEventArgs e)
        {
            string result = null;
            foreach (Video video in videoDataBase)
            {
                string name = e.NameText;
                string actors = e.ActorsText;
                string dir = e.DirectorsText;
                if(e.NameText.Contains(video.Name) == true && e.ActorsText.Contains(video.Actors) == true && e.DirectorsText.Contains(video.Directors))
                {
                    video.CantRated = video.CantRated + 1;
                    video.AccumulativeRated = video.AccumulativeRated + e.RankingText;
                    video.Ranking = video.AccumulativeRated / video.CantRated;
                    result = "Ranking summited.";
                }
            }
            database.Save_Videos(videoDataBase);
            return result;
        }


        public Video OnSkipOrPreviousVideoButton_Clicked(object sender, VideoEventArgs e)
        {
            if (e.previousOrSkip == 0)
            {
                if (e.OnQueue.Count == 0)
                {
                    if (e.playlistVideo == null)
                    {
                        for (int i = 0; i < videoDataBase.Count(); i++)
                        {
                            if (((e.NameText.Contains(videoDataBase[i].InfoVideo()[0])) && (e.ActorsText.Contains(videoDataBase[i].InfoVideo()[1]))) && (i != (videoDataBase.Count() - 1))) return videoDataBase[i + 1];
                            else if (((e.NameText.Contains(videoDataBase[i].InfoVideo()[0])) && (e.ActorsText.Contains(videoDataBase[i].InfoVideo()[1]))) && (i == (videoDataBase.Count() - 1))) return videoDataBase[0];

                        }
                    }
                    else
                    {
                        for (int i = 0; i < e.playlistVideo.Videos.Count(); i++)
                        {
                            if (((e.NameText.Contains(e.playlistVideo.Videos[i].InfoVideo()[0])) && (e.ActorsText.Contains(e.playlistVideo.Videos[i].InfoVideo()[1]))) && (i != (e.playlistVideo.Videos.Count() - 1))) return e.playlistVideo.Videos[i + 1];
                            else if (((e.NameText.Contains(e.playlistVideo.Videos[i].InfoVideo()[0])) && (e.ActorsText.Contains(e.playlistVideo.Videos[i].InfoVideo()[1]))) && (i == (e.playlistVideo.Videos.Count() - 1))) return e.playlistVideo.Videos[0];

                        }
                    }
                }
                else
                {
                    int n = 0;
                    foreach (string videoFile in e.OnQueue)
                    {
                        if (videoFile.Contains(".mp3") == false && videoFile.Contains(".wav") == false)
                        {
                            foreach (Video video in videoDataBase)
                            {
                                if (videoFile.Contains(video.FileName))
                                {
                                    e.OnQueue.RemoveAt(n);
                                    return video;
                                }
                            }
                            n++;
                        }
                    }

                }
            }
            else
            {
                if (e.playlistVideo == null)
                {
                    for (int i = 0; i < videoDataBase.Count(); i++)
                    {
                        if (((e.NameText.Contains(videoDataBase[i].InfoVideo()[0])) && (e.ActorsText.Contains(videoDataBase[i].InfoVideo()[1]))) && (i != 0)) return videoDataBase[i - 1];
                        else if (((e.NameText.Contains(videoDataBase[i].InfoVideo()[0])) && (e.ActorsText.Contains(videoDataBase[i].InfoVideo()[1]))) && (i == 0)) return videoDataBase[(videoDataBase.Count() - 1)];

                    }
                }

                else
                {
                    for (int i = 0; i < e.playlistVideo.Videos.Count(); i++)
                    {
                        if (((e.NameText.Contains(e.playlistVideo.Videos[i].InfoVideo()[0])) && (e.ActorsText.Contains(e.playlistVideo.Videos[i].InfoVideo()[1]))) && (i != 0)) return e.playlistVideo.Videos[i - 1];
                        else if (((e.NameText.Contains(e.playlistVideo.Videos[i].InfoVideo()[0])) && (e.ActorsText.Contains(e.playlistVideo.Videos[i].InfoVideo()[1]))) && (i == 0)) return e.playlistVideo.Videos[(e.playlistVideo.Videos.Count() - 1)];
                    }
                }
            }
            return null;
        }
    }
}
