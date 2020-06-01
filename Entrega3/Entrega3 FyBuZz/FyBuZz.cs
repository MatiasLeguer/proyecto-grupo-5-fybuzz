using Entrega3_FyBuZz.CustomArgs;
using Modelos;
using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;
using System.Media;

namespace Entrega3_FyBuZz
{
    public partial class FyBuZz : Form
    {


        //PUBLIC DELEGATES
        //--------------------------------------------------------------------------------

        WindowsMediaPlayer windowsMediaPlayer = new WindowsMediaPlayer();
        SoundPlayer soundPlayer;
        

        public delegate User LogInEventHandler(object soruce, LogInEventArgs args);
        public event LogInEventHandler LogInLogInButton_Clicked;

        public delegate bool RegisterEventHandler(object soruce, RegisterEventArgs args);
        public event RegisterEventHandler RegisterRegisterButton_Clicked;

        public delegate Profile ProfileEventHandler(object source, ProfileEventArgs args);
        public event ProfileEventHandler CreateProfileCreateProfileButton_Clicked;

        public delegate Profile ChooseProfileEventHandler(object source, ProfileEventArgs args);
        public event ChooseProfileEventHandler ProfilesChooseProfile_Clicked;

        public delegate List<PlayList> ChoosePlaylistEventHandler(object source, PlaylistEventArgs args);
        public event ChoosePlaylistEventHandler DisplayPlaylistsGlobalPlaylist_Clicked;

        public delegate bool SongEventHandler(object source, SongEventArgs args);
        public event SongEventHandler CreateSongCreateSongButton_Clicked;

        public delegate List<Song> ListSongEventHandler(object source, SongEventArgs args);
        public event ListSongEventHandler SearchSongButton_Clicked;


        public delegate List<User> ListUserEventHandler(object source, RegisterEventArgs args);
        public event ListUserEventHandler SearchUserButton_Clicked;

        public delegate string SearchedUserEventHandler(object source, UserEventArgs args);
        public event SearchedUserEventHandler SearchFollowButton_Clicked;

        public delegate string PlaylistEventHandler(object source, PlaylistEventArgs args);
        public event PlaylistEventHandler CreatePlaylistCreatePlaylistButton_Clicked;

        public delegate bool CreateVideoEventHandler(object source, VideoEventArgs args);
        public event CreateVideoEventHandler CreateVideoSaveButton_Clicked;

        public delegate List<Video> ListVideoEventHandler(object source, VideoEventArgs args);
        public event ListVideoEventHandler SearchVideoButton_Clicked;
        //--------------------------------------------------------------------------------


        //ATRIBUTOS
        //--------------------------------------------------------------------------------
        private string ProfileName { get; set; }
        //--------------------------------------------------------------------------------


        //CONSTRUCTOR
        //--------------------------------------------------------------------------------
        public FyBuZz()
        {
            InitializeComponent();
        }
        private void FyBuZz_Load(object sender, EventArgs e)
        {
            
        }
        //--------------------------------------------------------------------------------


        //LOG-IN
        //--------------------------------------------------------------------------------
        private void WelcomeLogInButton_Click(object sender, EventArgs e)
        {
            LogInPanel.BringToFront();
        }
        private void WelcomeRegisterButton_Click(object sender, EventArgs e)
        {
            RegisterPanel.BringToFront();
        }



        private void GobackRegisterButton_Click(object sender, EventArgs e)
        {
            WelcomePanel.BringToFront();
        }

        private void GoBackLoginButton_Click(object sender, EventArgs e)
        {
            WelcomePanel.BringToFront();
        }

        private void RegisterRegisterButton_Click(object sender, EventArgs e)
        {
            string username = UsernameRegisterTextBox.Text;
            string email = EmailRegisterTextBox.Text;
            string pswd = PasswordRegisterTextBox.Text;
            string subs = SubscriptionRegisterDomainUp.Text;
            bool privacy = PrivacyRegisterCheckBox.Checked; // true = privado
            string gender = GenderRegisterDomainUp.Text;
            DateTime birthday = AgeRegisterDateTimePicker.Value;
            string profileType = ProfileTypeRegisterDomainUp.Text;
            OnRegisterRegisterButtonClicked(username, email, pswd, subs, privacy, gender, birthday, profileType);
            WelcomePanel.BringToFront();
        }

        private void LogInLogInButton_Click(object sender, EventArgs e)
        {
            User user = new User();
            string username = UserLogInTextBox.Text;
            string pass = PasswordLogInTextBox.Text;
            user = OnLoginButtonClicked(username, pass);
            if(user != null)
            {
                ProfilePanel.BringToFront();
            }
        }
        private void ProfilePanel_Paint(object sender, EventArgs e)
        {

        }
        private void ProfileGoBack_Click(object sender, EventArgs e)
        {
            LogInPanel.BringToFront();
        }

        private void ProfilesChooseProfile_Click(object sender, EventArgs e)
        {
            string username = UserLogInTextBox.Text;

            string password = PasswordLogInTextBox.Text;
            string profileProfileName = ProfileDomainUp.Text;
            Profile profile = OnProfilesChooseProfile_Click(profileProfileName, username, password);

            ProfileName = profileProfileName;
            DisplayStartPanel.BringToFront();
            

            //Creo que cada vez que necesite el perfil debo llamar a este método con el parametro
            //que venga del "ProfileDomainUp.Text"
        }

        private void ProfileCreateProfileButton_Click(object sender, EventArgs e)
        {
            CreateProfilePanel.BringToFront();
        }
        private void CreateProfileGoBackButton_Click(object sender, EventArgs e)
        {
            ProfilePanel.BringToFront();
        }

        private void CreateProfileCreateProfileButton_Click(object sender, EventArgs e)
        {
            string pName = CreateProfileProfileNameTextBox.Text;
            string pGender = CreateProfileProfileGenderDomainUp.Text;
            DateTime pBirth = CreateProfileProfileBirthdayTimePicker.Value;
            string pType = CreateProfileProfileTypeDomainUp.Text;
            string pEmail = EmailRegisterTextBox.Text;
            string username = UserLogInTextBox.Text;
            string psswd = PasswordLogInTextBox.Text;
            Image pPic = CreateProfilePic1.Image;
            if (CreateProfilePicCheckedListBox.SelectedIndex == 0) pPic = CreateProfilePic1.Image;
            else if (CreateProfilePicCheckedListBox.SelectedIndex == 1) pPic = CreateProfilePic2.Image;
            else if (CreateProfilePicCheckedListBox.SelectedIndex == 2) pPic = CreateProfilePic3.Image;
            else if (CreateProfilePicCheckedListBox.SelectedIndex == 3) pPic = CreateProfilePic4.Image;
            OnCreateProfileCreateProfileButton_Click(username, psswd, pName,pGender,pType, pEmail,pBirth,pPic);
        }

        private void DisplayStartSearchButton_Click(object sender, EventArgs e)
        {
            SearchPanel.BringToFront();
        }

        private void DisplayStartLogOutButton_Click(object sender, EventArgs e)
        {
            LogInPanel.BringToFront();
            UserLogInTextBox.ResetText();
            PasswordLogInTextBox.ResetText();
            LogInInvalidCredentialsTetxbox.Clear();
            ProfilesWelcomeTextBox.Clear();
            ProfilesInvalidCredentialTextBox.Clear();
            ProfileDomainUp.ResetText();
            ProfilesInvalidCredentialTextBox.Clear();
        }


        private void DisplayStartSettingsButton_Click(object sender, EventArgs e)
        {
            string username = UserLogInTextBox.Text;
            string password = PasswordLogInTextBox.Text;
            User user = OnLoginButtonClicked(username, password);

            AccountSettingsUsernameTextBox.AppendText(user.Username);
            AccountSettingsPasswordTextBox.AppendText(user.Password);
            AccountSettingsEmailTextBox.AppendText(user.Email);
            AccountSettingsAccountTypeTextBox.AppendText(user.Accountype);
            AccountSettingsFollowersTextBox.AppendText(user.Followers.ToString());
            AccountSettingsFollowingTextBox.AppendText(user.Following.ToString());
            
            foreach(string seguidor in user.FollowingList)
            {
                AccountSettingsFollowingListDomaiUp.Items.Add(seguidor);
            }
            
            
            foreach (string followers in user.FollowerList)
            {
                AccountSettingsFollowerListDomainUp.Items.Add(followers);
            }         
            


            Profile profile = OnProfilesChooseProfile_Click(ProfileName, username, password);

            ProfileSettingsNameTextBox.AppendText(profile.ProfileName);
            ProfileSettingsProfileTypeTextBox.AppendText(profile.ProfileType);
            ProfileSettingsGenderTextBox.AppendText(profile.Gender);
            ProfileSettingsBirthdayTextBox.AppendText(profile.Age.ToString());

            Image pPic = CreateProfilePic1.Image;
            if (CreateProfilePicCheckedListBox.SelectedIndex == 0) pPic = CreateProfilePic1.Image;
            else if (CreateProfilePicCheckedListBox.SelectedIndex == 1) pPic = CreateProfilePic2.Image;
            else if (CreateProfilePicCheckedListBox.SelectedIndex == 2) pPic = CreateProfilePic3.Image;
            else if (CreateProfilePicCheckedListBox.SelectedIndex == 3) pPic = CreateProfilePic4.Image;

            ProfileSettingsProfilePicImageBox.Image = pPic;

            AccountProfileSettingsPanel.BringToFront();
        }

        private void DisplayStartLogOutProfileButton_Click(object sender, EventArgs e)
        {
            ProfilePanel.BringToFront();
        }

        private void DisplayStartAdminMenuButton_Click(object sender, EventArgs e)
        {
            AdminMenuPanel.BringToFront();
        }

        private void DisplayPlaylistGoBackButton_Click(object sender, EventArgs e)
        {
            DisplayStartPanel.BringToFront();
        }

        private void AccountProfileSettingsGoBackButton_Click(object sender, EventArgs e)
        {
            DisplayStartPanel.BringToFront();
        }

        private void AdminMenuGoBackButton_Click(object sender, EventArgs e)
        {
            DisplayStartPanel.BringToFront();
        }

        //<<PANEL DISPLAY PLAYLISTS>>

        private void DisplayStartDisplayPlaylistButton_Click(object sender, EventArgs e)
        {
            
            DisplayPlaylistPanel.BringToFront();
        }
        private void DisplayPlaylistsGlobalPlaylist1_Click(object sender, EventArgs e)
        {
            OnDisplayPlaylistsGlobalPlaylist_Click(0);
        }

        private void DisplayPlaylistsGlobalPlaylist2_Click(object sender, EventArgs e)
        {
            OnDisplayPlaylistsGlobalPlaylist_Click(1);
        }

        private void DisplayPlaylistsGlobalPlaylist3_Click(object sender, EventArgs e)
        {
            OnDisplayPlaylistsGlobalPlaylist_Click(2);
        }
        private void DisplayStartShowAddButton_Click(object sender, EventArgs e)
        {
            AddShowPanel.BringToFront();
        }

        //<<ADD/SHOW MULTIMEDIA PANEL>>
        private void AddShowGoBackButton_Click(object sender, EventArgs e)
        {
            DisplayStartPanel.BringToFront();
        }
        private void AddShowAddSongButton_Click(object sender, EventArgs e)
        {
            Profile profile = OnProfilesChooseProfile_Click(ProfileDomainUp.Text, UserLogInTextBox.Text, PasswordLogInTextBox.Text);
            if(profile.ProfileType != "viewer")
            {
                CreateSongPanel.BringToFront();
            }
            else
            {
                AddShowInvalidCredentialsLabel.Text = "You don´t have permission to create multimedia";
            }
            
        }
        private void AddShowAddPlaylistButton_Click(object sender, EventArgs e)
        {
            Profile profile = OnProfilesChooseProfile_Click(ProfileDomainUp.Text, UserLogInTextBox.Text, PasswordLogInTextBox.Text);
            if (profile.ProfileType != "viewer")
            {
                CreatePlaylistPanel.BringToFront();
            }
            else
            {
                AddShowInvalidCredentialsLabel.Text = "You don´t have permission to create multimedia";
            }
        }
        private void CreateSongGoBackButton_Click(object sender, EventArgs e)
        {
            AddShowPanel.BringToFront();
        }
        //<<Search Panel>>
        private void SearchGoBackButton_Click(object sender, EventArgs e)
        {
            DisplayStartPanel.BringToFront();
            if (windowsMediaPlayer.URL != null)
            {
                PlayerPanel.BringToFront();
                PlayerPanel.Dock = DockStyle.Bottom;
            }
        }
        private void SearchSearchButton_Click(object sender, EventArgs e)
        {
            string search = SearchSearchTextBox.Text; //Bad Bunny and Trap and ... and ...

            //string[] newSearchAnd = search.Split(new string[] { " and " }, StringSplitOptions.None);

            List<string> listSearch = new List<string>();
            //listSearch.Add(search);
            List<Song> songDataBase = new List<Song>();
            songDataBase = OnSearchSongButton_Click();
            List<User> userDataBase = new List<User>();
            userDataBase = OnSearchUserButton_Click();
            List<Video> videoDataBase = new List<Video>();
            videoDataBase = OnSearchVideoButton_Click();

            foreach (Song song in songDataBase)
            {

                if (song.InfoSong().Contains(search))
                {
                    SearchSearchResultsDomainUp.Visible = true;
                    SearchSearchResultsDomainUp.Items.Add(song.SearchedInfoSong());
                }
            }
            foreach(User user in userDataBase)
            {             
                if (user.infoUser().Contains(search))
                {
                    SearchSearchResultsDomainUp.Visible = true;
                    SearchSearchResultsDomainUp.Items.Add("User: " + user.SearchedInfoUser());
                }
            }

            foreach(Video video in videoDataBase)
            {
                if (video.InfoVideo().Contains(search))
                {
                    SearchSearchResultsDomainUp.Visible = true;
                    SearchSearchResultsDomainUp.Items.Add(video.SearchedInfoVideo());
                }
            }

        }

        private void SearchSelectMultButton_Click(object sender, EventArgs e)
        {
            soundPlayer = new SoundPlayer();
            List<Song> songDataBase = new List<Song>();
            songDataBase = OnSearchSongButton_Click();

            foreach (Song song in songDataBase)
            {
                if (song.Format == ".mp3")
                {
                    string result = SearchSearchResultsDomainUp.Text;
                    if (result == song.SearchedInfoSong())
                    {
                        PlayerPlayingLabel.Clear();
                        PlaySongProgressBar.Value = 0;
                        PlaySongTimerTextBox.ResetText();
                        windowsMediaPlayer.URL = song.SongFile;
                        DurationTimer.Interval = 1000;
                        PlaySongProgressBar.Maximum = (int)(song.Duration * 60);
                        PlaySongPanel.BringToFront();
                        PlayerPlayingLabel.AppendText("Playing: " + song.Name);
                        DurationTimer.Start();
                        break;
                    }
                }
                else if(song.Format == ".wav")
                {
                    string result = SearchSearchResultsDomainUp.Text;
                    if (result == song.SearchedInfoSong())
                    {

                        PlayerPlayingLabel.Clear();
                        PlaySongProgressBar.Value = 0;
                        PlaySongTimerTextBox.ResetText();
                        soundPlayer.SoundLocation = song.SongFile;
                        soundPlayer.Play();
                        DurationTimer.Interval = 1000;
                        PlaySongProgressBar.Maximum = (int)(song.Duration * 60);
                        PlaySongPanel.BringToFront();
                        PlayerPlayingLabel.AppendText("Playing: " + song.Name);
                        DurationTimer.Start();
                        break;
                    }
                }
            }
        }
     
        private void SearchFollowButton_Click(object sender, EventArgs e)
        {
            List<User> userDataBase = new List<User>();
            userDataBase = OnSearchUserButton_Click();
            User logInUser = OnLoginButtonClicked(UserLogInTextBox.Text, PasswordLogInTextBox.Text);

            if (SearchSearchResultsDomainUp.Text.Contains("User:"))
            {
                foreach (User searchedUser in userDataBase)
                {
                    string result = SearchSearchResultsDomainUp.Text;
                    List<string> listuser = searchedUser.FollowingList;
                    if (result == "User: " + searchedUser.SearchedInfoUser())
                    {
                        OnSearchFollowButton_Click(logInUser, searchedUser);
                    }
                }
            }
        }
        

        //<<PLAY SONG MP3 PANEL>>

        private void DurationTimer_Tick(object sender, EventArgs e)
        {
            PlaySongProgressBar.Increment(1);      
            //Falta que se muestre bien el tiempo...
            PlaySongTimerTextBox.Text = PlaySongProgressBar.Value.ToString();
        }
        private void PlaySongGoBackButton_Click(object sender, EventArgs e)
        {
            SearchPanel.BringToFront();
            
            
        }
        private void PlaySongStopButton_Click(object sender, EventArgs e)
        {
            soundPlayer.Stop();
            List<Song> songDataBase = new List<Song>();
            songDataBase = OnSearchSongButton_Click();
            foreach (Song song in songDataBase)
            {
                if (song.Format == ".mp3")
                {
                    windowsMediaPlayer.controls.pause();
                    DurationTimer.Stop();
                    break;
                }
                else if (song.Format == ".wav")
                {
                    soundPlayer.Stop();
                    DurationTimer.Stop();
                    break;
                }
            }
        }

        private void PlaySongPlayButton_Click_1(object sender, EventArgs e)
        {
            soundPlayer.Play();
            List<Song> songDataBase = new List<Song>();
            songDataBase = OnSearchSongButton_Click();
            foreach (Song song in songDataBase)
            {
                if (SearchSearchResultsDomainUp.Text.Contains("Song:") && song.Format == ".mp3")
                {
                    windowsMediaPlayer.controls.play();
                    DurationTimer.Start();
                    break;
                }
                else if (SearchSearchResultsDomainUp.Text.Contains("Song:") && song.Format == ".wav")
                {

                    DurationTimer.Start();
                }
            }
        }

        private void PlaySongPreviousButton_Click(object sender, EventArgs e)
        {
            List<Song> songDataBase = new List<Song>();
            songDataBase = OnSearchSongButton_Click();
            foreach (Song song in songDataBase)
            {
                if (song.Format == ".mp3")
                {
                    windowsMediaPlayer.controls.previous();
                    break;
                }
            }
        }

        private void PlaySongSkipButton_Click(object sender, EventArgs e)
        {
            List<Song> songDataBase = new List<Song>();
            songDataBase = OnSearchSongButton_Click();
            foreach (Song song in songDataBase)
            {
                if (song.Format == ".mp3")
                {
                    windowsMediaPlayer.controls.next();
                    break;
                }
            }
        }

        

        //<<PANEL DE CREACION SONG>>
        private void CreateSongCreateSongButton_Click(object sender, EventArgs e)
        {
            Profile profile = OnProfilesChooseProfile_Click(ProfileName, UserLogInTextBox.Text, PasswordLogInTextBox.Text);
            if (profile.ProfileType != "viewer")
            {
                string songName = CreateSongNameTextBox.Text;
                string songArtist = CreateSongArtistTextBox.Text;
                string songAlbum = CreateSongAlbumTextBox.Text;
                string songDiscography = CreateSongDiscographyTextBox.Text;
                string songGender = CreateSongGenderTextBox.Text;
                DateTime songPublishDate = CreateSongPublishDateTime.Value;
                string songStudio = CreateSongStudioTextBox.Text;
                double songDuration = double.Parse(CreateSongDurationTextBox.Text);
                string songFormat = CreateSongFormatTextBox.Text;
                string songLyrics = CreateSongLyricsTextBox.Text;
                string songFileSource = CreateSongSongFileTextBox.Text;
                string songFile = songFileSource.Split('\\')[songFileSource.Split('\\').Length-1];
                if(File.Exists(songFile) == false)
                {
                    OnCreateSongCreateSongButton_Click(songName, songArtist, songAlbum, songDiscography, songGender, songPublishDate, songStudio, songDuration, songFormat, songLyrics, songFileSource, songFile);
                }
                else
                {
                    CreateSongInvalidCredentialTextBox.AppendText("An Error has ocurred please try again");
                    Thread.Sleep(2000);
                    DisplayStartPanel.BringToFront();
                    CreateSongNameTextBox.Clear();
                    CreateSongArtistTextBox.Clear();
                    CreateSongAlbumTextBox.Clear();
                    CreateSongDiscographyTextBox.Clear();
                    CreateSongGenderTextBox.Clear();
                    CreateSongStudioTextBox.Clear();
                    CreateSongDurationTextBox.Clear();
                    CreateSongFormatTextBox.Clear();
                    CreateSongLyricsTextBox.Clear();
                    CreateSongSongFileTextBox.Clear();
                }
                
            }
        }
        private void CreateSongSongFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filename = openFileDialog.FileName;
                CreateSongSongFileTextBox.Text = filename;
            }
        }

        //<<CREATE PLAYLIST PANEL>>
        private void CreatePlaylistGoBack_Click(object sender, EventArgs e)
        {
            DisplayStartPanel.BringToFront();
        }

        private void CreatePlaylistCreatePlaylistButton_Click(object sender, EventArgs e)
        {
            string playlistName = CreatePlaylistNameTextBox.Text;
            string playlistFormat = CreatePlaylistFormatDomainUp.Text;
            User playlistCreator = OnLoginButtonClicked(UserLogInTextBox.Text,PasswordLogInTextBox.Text);
            Profile playlistProfileCreator = OnProfilesChooseProfile_Click(ProfileDomainUp.Text, UserLogInTextBox.Text, PasswordLogInTextBox.Text);
            bool playlistPrivacy = CreatePlaylistPrivacyCheckBox.Checked; //True si esta checked
            OnCreatePlaylistCreatePlaylistButton_Click(playlistName, playlistFormat, playlistPrivacy, playlistCreator, playlistProfileCreator);

        }

        //CREATE VIDEO PANEL -->AL APRETAR ADD VIDEO
        private void AddShowAddVideoButton_Click(object sender, EventArgs e)
        {
            CreateVideoPanel.BringToFront();
        }

        private void CreateVideoSaveButton_Click(object sender, EventArgs e)
        {
            string videoName = CreateVideoNameTextBox.Text;
            string actors = CreateVideoActorsTextBox.Text;
            string directors = CreateVideoDirectorsTextBox.Text;
            string releaseDate = CreateVideoReleaseDateDateTimePicker.Value.ToShortDateString();
            string videoDimension = CreateVideoDimensionTextBox.Text;
            string videoQuality = CreateVideoQualityTextBox.Text;
            string videoCategory = CreateVideoCategoryTextBox.Text;
            string videoDescription = CreateVideoDescriptionTextBox.Text;
            string videoDuration = CreateVideoDurationTextBox.Text;
            string videoFormat = CreateVideoFormatTextBox.Text;
            string videoSubtitles = CreateVideoSubtitlesTextBox.Text;
            string videoFileSource = CreateVideoLoadVideoTextBox.Text;
            string videoFileName = videoFileSource.Split('\\')[videoFileSource.Split('\\').Length - 1];
            

            if(File.Exists(videoFileName) == false)
            {
                OnCreateVideoSaveButton_Clicked(videoName, actors, directors, releaseDate, videoDimension, videoQuality, videoCategory, videoDescription, videoDuration, videoFormat, videoSubtitles, videoFileSource, videoFileName, "true");
            }
            else
            {
                CreateVideoMessageTextBox.AppendText("ERROR[!] Your file already exists");
                CreateVideoNameTextBox.Clear();
                CreateVideoActorsTextBox.Clear();
                CreateVideoDirectorsTextBox.Clear();
                CreateVideoDimensionTextBox.Clear();
                CreateVideoQualityTextBox.Clear();
                CreateVideoCategoryTextBox.Clear();
                CreateVideoDescriptionTextBox.Clear();
                CreateVideoDurationTextBox.Clear();
                CreateVideoFormatTextBox.Clear();
                CreateVideoSubtitlesTextBox.Clear();
                CreateVideoLoadVideoTextBox.Clear();
                Thread.Sleep(1500);
                CreateVideoMessageTextBox.Clear();
            }

        }

        private void CreateVideoLoadVideoButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filename = openFileDialog.FileName;
                CreateVideoLoadVideoTextBox.Text = filename;
            }
        }

        //MÉTODOS INTERNOS 
        public User OnLoginButtonClicked(string username, string password)
        {
            User user = new User();
            if(LogInLogInButton_Clicked != null)
            {
                user = LogInLogInButton_Clicked(this, new LogInEventArgs() { UsernameText = username, PasswrodText = password });
                if (user == null) //Resultado es falso
                {
                    LogInInvalidCredentialsTetxbox.AppendText("Incorrect Username or Password");
                    LogInInvalidCredentialsTetxbox.Visible = true;
                }
                else
                {
                    LogInInvalidCredentialsTetxbox.AppendText("Log-In Succesfull");
                    LogInInvalidCredentialsTetxbox.Visible = true;
                    
                    ProfilesWelcomeTextBox.AppendText("Welcome to FyBuZz " + user.Username);
                    foreach (Profile profile in user.Perfiles)
                    {
                        ProfileDomainUp.Items.Add(profile.ProfileName);
                    }
                }
            }
            return user;
        }
            
        public void OnRegisterRegisterButtonClicked(string username, string email,string psswd, string subs, bool priv, string gender, DateTime birthday, string profileType)
        {
            if(RegisterRegisterButton_Clicked != null)
            {
                bool result = RegisterRegisterButton_Clicked(this, new RegisterEventArgs() { UsernameText = username, EmailText = email, PasswrodText = psswd, SubsText = subs,PrivacyText = priv, GenderText = gender, BirthdayText = birthday, ProfileTypeText = profileType });
                if (!result) //Resultado es falso
                {
                    RegisterInvalidCredencialsTextBox.AppendText("User already exist");
                    RegisterInvalidCredencialsTextBox.Visible = true;
                    Thread.Sleep(2000);
                    UsernameRegisterTextBox.Clear();
                    EmailRegisterTextBox.Clear();
                    PasswordRegisterTextBox.Clear();
                    RegisterInvalidCredencialsTextBox.Clear();
                    
                }
                else
                {
                    RegisterInvalidCredencialsTextBox.AppendText("Registered Succesfull");
                    RegisterInvalidCredencialsTextBox.Visible = true;
                    UsernameRegisterTextBox.Clear();
                    EmailRegisterTextBox.Clear();
                    PasswordRegisterTextBox.Clear();
                    RegisterInvalidCredencialsTextBox.Clear();
                    Thread.Sleep(2000);
                }
            }
        }
        public void OnCreateProfileCreateProfileButton_Click(string username, string pswd, string pName,string pGender, string pType, string pEmail, DateTime pBirth, Image pPic)
        {
            if(CreateProfileCreateProfileButton_Clicked != null)
            {
                Profile result = CreateProfileCreateProfileButton_Clicked(this, new ProfileEventArgs() {UsernameText = username, PasswordText = pswd, ProfileNameText = pName, EmailText = pEmail, GenderText = pGender, BirthdayText = pBirth, ProfileTypeText = pType, PicImage = pPic });
                if(result != null)
                {
                    ProfileDomainUp.Items.Add(result.ProfileName);
                    ProfilePanel.BringToFront();
                    CreateProfileProfileNameTextBox.Clear();
                }
                else
                {
                    ProfilePanel.BringToFront();
                    ProfilesInvalidCredentialTextBox.AppendText("Only premium Users can create profiles");
                }
            }
        }

        public Profile OnProfilesChooseProfile_Click(string pName, string usr, string pass)
        {
            if (ProfilesChooseProfile_Clicked != null)
            {
                Profile choosenProfile = ProfilesChooseProfile_Clicked(this, new ProfileEventArgs() { ProfileNameText = pName , UsernameText = usr, PasswordText = pass});
                ProfilesInvalidCredentialTextBox.ResetText();
                ProfilesInvalidCredentialTextBox.AppendText("Entering FyBuZz with... " + choosenProfile.ProfileName);

                Thread.Sleep(2000);

                return choosenProfile;
            }
            else
            {
                return null;
            }
        }
        public PlayList OnDisplayPlaylistsGlobalPlaylist_Click(int playlistIndex)
        {
            if(DisplayPlaylistsGlobalPlaylist_Clicked != null)
            {
                List<PlayList> listPlaylist = DisplayPlaylistsGlobalPlaylist_Clicked(this, new PlaylistEventArgs()); //Nose si es necesario darle parametros
                return listPlaylist[playlistIndex];
            }
            return null;
        }
        public void OnCreateSongCreateSongButton_Click(string sName, string sArtist, string sAlbum, string sDiscography, string sGender, DateTime sPublishDate, string sStudio, double sDuration, string sFormat, string sLyrics, string sSource,string songFile)
        {
            if (CreateSongCreateSongButton_Clicked != null)
            {
                bool result = CreateSongCreateSongButton_Clicked(this, new SongEventArgs() { NameText = sName, AlbumText = sAlbum, ArtistText = sArtist, DateText = sPublishDate, DiscographyText = sDiscography, DurationText = sDuration, FormatText = sFormat, GenderText = sGender, LyricsText = sLyrics, StudioText = sStudio, FileDestName = sSource,FileNameText = songFile});
                if (!result)
                {
                    CreateSongInvalidCredentialTextBox.AppendText("An Error has ocurred please try again");
                    Thread.Sleep(2000);
                    DisplayStartPanel.BringToFront();
                    CreateSongNameTextBox.Clear();
                    CreateSongArtistTextBox.Clear();
                    CreateSongAlbumTextBox.Clear();
                    CreateSongDiscographyTextBox.Clear();
                    CreateSongGenderTextBox.Clear();
                    CreateSongStudioTextBox.Clear();
                    CreateSongDurationTextBox.Clear();
                    CreateSongFormatTextBox.Clear();
                    CreateSongLyricsTextBox.Clear();
                    CreateSongSongFileTextBox.Clear();
                    
                }
                else
                {
                    CreateSongInvalidCredentialTextBox.AppendText("Song Added to the system");
                    Thread.Sleep(2000);
                    CreateSongNameTextBox.Clear();
                    CreateSongArtistTextBox.Clear();
                    CreateSongAlbumTextBox.Clear();
                    CreateSongDiscographyTextBox.Clear();
                    CreateSongGenderTextBox.Clear();
                    CreateSongStudioTextBox.Clear();
                    CreateSongDurationTextBox.Clear();
                    CreateSongFormatTextBox.Clear();
                    CreateSongLyricsTextBox.Clear();
                    CreateSongSongFileTextBox.Clear();
                }
            }
        }
        public List<Song> OnSearchSongButton_Click()
        {
            if(SearchSongButton_Clicked != null)
            {
                List<Song> songDataBase = SearchSongButton_Clicked(this, new SongEventArgs());
                return songDataBase;
            }
            return null;
        }



        // CLOSE/GO BACK

        private void DisplayStartCloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void WelcomeCloseFyBuZz_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewGroup listViewUsers = new ListViewGroup("Users");


            //listView1.Items.Add();
        }
        //<<ADD/SHOW MULTIMEDIA PANEL>>


        private void WelcomePanel_Paint(object sender, PaintEventArgs e)
        {

        }
        public List<User> OnSearchUserButton_Click()
        {
            if (SearchUserButton_Clicked != null)
            {
                List<User> userDataBase = SearchUserButton_Clicked(this, new RegisterEventArgs());
                return userDataBase;
            }
            return null;

        }
        public void OnSearchFollowButton_Click(User userLogIn, User userSearched)
        {
            if(SearchFollowButton_Clicked != null)
            {
                string result = SearchFollowButton_Clicked(this, new UserEventArgs() {UserLogIn = userLogIn, UserSearched = userSearched });
                if (result != null)
                {
                    //Un label que appende el result...
                    SearchInvalidCredentialsTextBox.AppendText(result);
                    Thread.Sleep(2000);
                    DisplayStartPanel.BringToFront();
                    SearchInvalidCredentialsTextBox.Clear();
                }
                else
                {
                    //Un label que appende un error...
                    SearchInvalidCredentialsTextBox.AppendText("Error... couldn't follow " + userSearched.Username);
                    Thread.Sleep(2000);
                    DisplayStartPanel.BringToFront();
                    SearchInvalidCredentialsTextBox.Clear();
                }
            }
            else
            {
                SearchInvalidCredentialsTextBox.AppendText("Error... couldn't follow " + userSearched.Username);
                Thread.Sleep(2000);
                DisplayStartPanel.BringToFront();
                SearchInvalidCredentialsTextBox.Clear();
            }
        }
        public void OnCreatePlaylistCreatePlaylistButton_Click(string plName, string plFormat, bool plPrivacy, User plCreator, Profile plProfileCreator)
        {
            if(CreatePlaylistCreatePlaylistButton_Clicked != null)
            {
                string result = CreatePlaylistCreatePlaylistButton_Clicked(this, new PlaylistEventArgs() { NameText = plName, CreatorText = plCreator, FormatText = plFormat, PrivacyText = plPrivacy, ProfileCreatorText = plProfileCreator });

                if(result == null)
                {
                    CreatePlaylistInvalidCredentialstextBox.AppendText("Playlist created succesfully!!");
                    Thread.Sleep(2000);
                    DisplayStartPanel.BringToFront();
                }
                else
                {
                    CreatePlaylistInvalidCredentialstextBox.AppendText("Error[!] " + result);
                    Thread.Sleep(2000);
                    DisplayStartPanel.BringToFront();
                }
            }
        }

        public void OnCreateVideoSaveButton_Clicked(string name, string actors, string directors, string releaseDate, string dimension, string quality, string category, string description, string duration, string format, string subtitles, string fileDest, string fileName,  string image)
        {
            if(CreateVideoSaveButton_Clicked != null)
            {
                bool createVideo = CreateVideoSaveButton_Clicked(this, new VideoEventArgs() { NameText = name, ActorsText = actors, DirectorsText = directors, ReleaseDateText = releaseDate, DimensionText = dimension, Categorytext = category, DescriptionText = description, DurationText = duration, FormatText = format, SubtitlesText = subtitles, FileDestText = fileDest, FileNameText = fileName , QualityText = quality, VideoImage = image});
                if (createVideo)
                {
                    CreateVideoMessageTextBox.AppendText("Video Created succesfully!");
                    CreateVideoNameTextBox.Clear();
                    CreateVideoActorsTextBox.Clear();
                    CreateVideoDirectorsTextBox.Clear();
                    CreateVideoDimensionTextBox.Clear();
                    CreateVideoQualityTextBox.Clear();
                    CreateVideoCategoryTextBox.Clear();
                    CreateVideoDescriptionTextBox.Clear();
                    CreateVideoDurationTextBox.Clear();
                    CreateVideoFormatTextBox.Clear();
                    CreateVideoSubtitlesTextBox.Clear();
                    CreateVideoLoadVideoTextBox.Clear();
                    Thread.Sleep(2000);
                    CreateVideoMessageTextBox.Clear();
                    DisplayStartPanel.BringToFront();
                }
                else
                {
                    CreateVideoMessageTextBox.AppendText("ERROR[!] could not create video!");
                    CreateVideoNameTextBox.Clear();
                    CreateVideoActorsTextBox.Clear();
                    CreateVideoDirectorsTextBox.Clear();
                    CreateVideoDimensionTextBox.Clear();
                    CreateVideoQualityTextBox.Clear();
                    CreateVideoCategoryTextBox.Clear();
                    CreateVideoDescriptionTextBox.Clear();
                    CreateVideoDurationTextBox.Clear();
                    CreateVideoFormatTextBox.Clear();
                    CreateVideoSubtitlesTextBox.Clear();
                    CreateVideoLoadVideoTextBox.Clear();
                    Thread.Sleep(1500);
                    CreateVideoMessageTextBox.Clear();
                }
            }
        }

        public List<Video> OnSearchVideoButton_Click()
        {
            if (SearchVideoButton_Clicked != null)
            {
                List<Video> videoDataBase = SearchVideoButton_Clicked(this, new VideoEventArgs());
                return videoDataBase;
            }
            return null;
        }


    }
}
