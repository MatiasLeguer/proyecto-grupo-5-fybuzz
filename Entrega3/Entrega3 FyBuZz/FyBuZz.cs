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
        WindowsMediaPlayer addsOnWMP = new WindowsMediaPlayer();
        SoundPlayer soundPlayer;

        public delegate List<string> UserGetterListEventHandler(object source, UserEventArgs args);
        public event UserGetterListEventHandler LogInLogInButton_Clicked2;
        public delegate List<string> UserProfilesNames(object source, UserEventArgs args);
        public event UserProfilesNames GetProfileNames;

        public delegate User LogInEventHandler(object soruce, LogInEventArgs args);
        public event LogInEventHandler LogInLogInButton_Clicked;

        public delegate bool RegisterEventHandler(object soruce, RegisterEventArgs args);
        public event RegisterEventHandler RegisterRegisterButton_Clicked;

        public delegate string ProfileEventHandler(object source, ProfileEventArgs args);
        public event ProfileEventHandler CreateProfileCreateProfileButton_Clicked;

        public delegate List<string> ChooseProfileEventHandlerMVC(object source, ProfileEventArgs args);
        public event ChooseProfileEventHandlerMVC ProfilesChooseProfile_Clicked2;

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

        public delegate string ChoosePLEventHanlder(object source, PlaylistEventArgs args);
        public event ChoosePLEventHanlder PlaySongChoosePlsButton_Clicked;
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
            UserLogInTextBox.Clear();
            PasswordLogInTextBox.Clear();
            LogInInvalidCredentialsTetxbox.Clear();
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
            List<string> userGetter = new List<string>();
            string username = UserLogInTextBox.Text;
            string pass = PasswordLogInTextBox.Text;
            userGetter = OnLogInLogInButton_Clicked2(username);
            //user = OnLoginButtonClicked(username, pass);
            if (userGetter != null)
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
            UserLogInTextBox.Clear();
            PasswordLogInTextBox.Clear();
            LogInInvalidCredentialsTetxbox.Clear();
        }

        private void ProfilesChooseProfile_Click(object sender, EventArgs e)
        {
            string username = UserLogInTextBox.Text;

            string password = PasswordLogInTextBox.Text;
            string profileProfileName = ProfileDomainUp.Text;
            List<string> profileGetterString = OnProfilesChooseProfile_Click2(profileProfileName, username, password);

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
            OnCreateProfileCreateProfileButton_Click2(username, psswd, pName,pGender,pType, pEmail,pBirth,pPic);
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
            User user = new User();
            user = OnLoginButtonClicked(username, password);
            List<string> userGetterString = new List<string>();
            userGetterString = OnLogInLogInButton_Clicked2(username);
            string profileProfileName = ProfileDomainUp.Text;
            List<string> profileGetterString = OnProfilesChooseProfile_Click2(profileProfileName, username, password);

            AccountSettingsUsernameTextBox.AppendText(userGetterString[0]);
            AccountSettingsPasswordTextBox.AppendText(userGetterString[1]);
            AccountSettingsEmailTextBox.AppendText(userGetterString[2]);
            AccountSettingsAccountTypeTextBox.AppendText(userGetterString[3]);
            AccountSettingsFollowersTextBox.AppendText(userGetterString[4]);
            AccountSettingsFollowingTextBox.AppendText(userGetterString[5]);
            
            foreach(string seguidor in user.FollowingList)
            {
                AccountSettingsFollowingListDomaiUp.Items.Add(seguidor);
            }


            foreach (string followers in user.FollowerList)
            {
                AccountSettingsFollowerListDomainUp.Items.Add(followers);
            }         

            ProfileSettingsNameTextBox.AppendText(profileGetterString[0]);
            ProfileSettingsProfileTypeTextBox.AppendText(profileGetterString[1]);
            ProfileSettingsGenderTextBox.AppendText(profileGetterString[2]);
            ProfileSettingsBirthdayTextBox.AppendText(profileGetterString[3]);

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
            AccountSettingsUsernameTextBox.Clear();
            AccountSettingsPasswordTextBox.Clear();
            AccountSettingsAccountTypeTextBox.Clear();
            AccountSettingsEmailTextBox.Clear();
            AccountSettingsFollowersTextBox.Clear();
            AccountSettingsFollowingTextBox.Clear();

            ProfileSettingsNameTextBox.Clear();
            ProfileSettingsProfileTypeTextBox.Clear();
            ProfileSettingsGenderTextBox.Clear();
            ProfileSettingsBirthdayTextBox.Clear();
            //ProfileSettingsProfilePicImageBox.Image.Dispose();

            ProfilesInvalidCredentialTextBox.Clear();
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
            Profile profile = OnProfilesChooseProfile_Click(ProfileDomainUp.Text, UserLogInTextBox.Text, PasswordLogInTextBox.Text);
            //<<Followed>>
            if (profile.FollowedPlayList.Count() == 1)
            {
                DisplayPlaylistsFollowedPlaylist1.Visible = true;
                DisplayPlaylistsFollowedPlaylist1.Image = CreateProfilePic1.Image;
            }
            else if (profile.FollowedPlayList.Count() == 2)
            {
                DisplayPlaylistsFollowedPlaylist1.Visible = true;
                DisplayPlaylistsFollowedPlaylist2.Visible = true;
            }
            else if (profile.FollowedPlayList.Count() == 3)
            {
                DisplayPlaylistsFollowedPlaylist1.Visible = true;
                DisplayPlaylistsFollowedPlaylist2.Visible = true;
                DisplayPlaylistsFollowedPlaylist3.Visible = true;
            }
            else if(profile.FollowedPlayList.Count() > 3)
            {
                DisplayPlaylistsFollowedPlaylist1.Visible = true;
                DisplayPlaylistsFollowedPlaylist2.Visible = true;
                DisplayPlaylistsFollowedPlaylist3.Visible = true;
                DisplayPlaylistsMoreFollowedPlaylistButton.Visible = true;
            }
            //<<Created>>
            if (profile.CreatedPlaylist.Count() == 1)
            {
                DisplayPlaylistCreatedPlaylistImage1.Visible = true;
                DisplayPlaylistCreatedPlaylistImage1.Image = CreateProfilePic1.Image;
            }
            else if (profile.FollowedPlayList.Count() == 2)
            {
                DisplayPlaylistCreatedPlaylistImage1.Visible = true;
                DisplayPlaylistCreatedPlaylistImage2.Visible = true;
            }
            else if (profile.FollowedPlayList.Count() == 3)
            {
                DisplayPlaylistCreatedPlaylistImage1.Visible = true;
                DisplayPlaylistCreatedPlaylistImage2.Visible = true;
                DisplayPlaylistCreatedPlaylistImage3.Visible = true;
            }
            else if (profile.FollowedPlayList.Count() > 3)
            {
                DisplayPlaylistCreatedPlaylistImage1.Visible = true;
                DisplayPlaylistCreatedPlaylistImage2.Visible = true;
                DisplayPlaylistCreatedPlaylistImage3.Visible = true;
                DisplayPlaylistCreatedPlaylistButton.Visible = true;
            }
            DisplayPlaylistPanel.BringToFront();
        }
        private void DisplayPlaylistsGlobalPlaylist1_Click(object sender, EventArgs e)
        {
            soundPlayer = new SoundPlayer();
            List<PlayList> playlistDataBase = new List<PlayList>();
            playlistDataBase = OnDisplayPlaylistsGlobalPlaylist_Click();

            string result = playlistDataBase[0].DisplayInfoPlayList();
            foreach (PlayList playList in playlistDataBase)
            {
                string ex = playList.DisplayInfoPlayList();
                if (result == ex)
                {
                    if (playList.Format == ".mp3" || playList.Format == ".wav")
                    {
                        foreach (Song song in playList.Songs)
                        {
                            PlayPlaylistShowMultimedia.Items.Add(song.SearchedInfoSong());
                        }
                    }
                }
            }
            PlayPlaylistPanel.BringToFront();

        }

        private void DisplayPlaylistsGlobalPlaylist2_Click(object sender, EventArgs e)
        {
            soundPlayer = new SoundPlayer();
            List<PlayList> playlistDataBase = new List<PlayList>();
            playlistDataBase = OnDisplayPlaylistsGlobalPlaylist_Click();

            string result = playlistDataBase[1].DisplayInfoPlayList();
            foreach (PlayList playList in playlistDataBase)
            {
                string ex = playList.DisplayInfoPlayList();
                if (result == ex)
                {
                    if (playList.Format == ".mp3" || playList.Format == ".wav")
                    {
                        foreach (Song song in playList.Songs)
                        {
                            PlayPlaylistShowMultimedia.Items.Add(song.SearchedInfoSong());
                        }
                    }
                }
            }
            PlayPlaylistPanel.BringToFront();
        }

        private void DisplayPlaylistsGlobalPlaylist3_Click(object sender, EventArgs e)
        {
            soundPlayer = new SoundPlayer();
            List<PlayList> playlistDataBase = new List<PlayList>();
            playlistDataBase = OnDisplayPlaylistsGlobalPlaylist_Click();

            string result = playlistDataBase[2].DisplayInfoPlayList();
            foreach (PlayList playList in playlistDataBase)
            {
                string ex = playList.DisplayInfoPlayList();
                if (result == ex)
                {
                    if (playList.Format == ".mp3" || playList.Format == ".wav")
                    {
                        foreach (Song song in playList.Songs)
                        {
                            PlayPlaylistShowMultimedia.Items.Add(song.SearchedInfoSong());
                        }
                    }
                }
            }
            PlayPlaylistPanel.BringToFront();
        }
        private void DisplayPlaylistsMoreGlobalPlaylistButton_Click(object sender, EventArgs e)
        {
            List<PlayList> playlistDataBase = new List<PlayList>();
            playlistDataBase = OnDisplayPlaylistsGlobalPlaylist_Click();
            if(playlistDataBase.Count > 3)
            {
                //Traer un panel que muestre las otras Pls Globales...
            }
        }
        private void DisplayPlaylistsFollowedPlaylist1_Click(object sender, EventArgs e)
        {
            Profile profile = OnProfilesChooseProfile_Click(ProfileDomainUp.Text, UserLogInTextBox.Text, PasswordLogInTextBox.Text);
            soundPlayer = new SoundPlayer();
            List<PlayList> playlistDataBase = new List<PlayList>();
            playlistDataBase = OnDisplayPlaylistsGlobalPlaylist_Click();

            string result = profile.FollowedPlayList[0].DisplayInfoPlayList();
            foreach (PlayList playList in playlistDataBase)
            {
                string ex = playList.DisplayInfoPlayList();
                if (result == ex)
                {
                    if (playList.Format == ".mp3" || playList.Format == ".wav")
                    {
                        foreach (Song song in playList.Songs)
                        {
                            PlayPlaylistShowMultimedia.Items.Add(song.SearchedInfoSong());
                        }
                    }
                }
            }
            PlayPlaylistPanel.BringToFront();
        }

        private void DisplayPlaylistsFollowedPlaylist2_Click(object sender, EventArgs e)
        {
            Profile profile = OnProfilesChooseProfile_Click(ProfileDomainUp.Text, UserLogInTextBox.Text, PasswordLogInTextBox.Text);
            soundPlayer = new SoundPlayer();
            List<PlayList> playlistDataBase = new List<PlayList>();
            playlistDataBase = OnDisplayPlaylistsGlobalPlaylist_Click();

            string result = profile.FollowedPlayList[1].DisplayInfoPlayList();
            foreach (PlayList playList in playlistDataBase)
            {
                string ex = playList.DisplayInfoPlayList();
                if (result == ex)
                {
                    if (playList.Format == ".mp3" || playList.Format == ".wav")
                    {
                        foreach (Song song in playList.Songs)
                        {
                            PlayPlaylistShowMultimedia.Items.Add(song.SearchedInfoSong());
                        }
                    }
                }
            }
            PlayPlaylistPanel.BringToFront();
        }

        private void DisplayPlaylistsFollowedPlaylist3_Click(object sender, EventArgs e)
        {
            Profile profile = OnProfilesChooseProfile_Click(ProfileDomainUp.Text, UserLogInTextBox.Text, PasswordLogInTextBox.Text);
            soundPlayer = new SoundPlayer();
            List<PlayList> playlistDataBase = new List<PlayList>();
            playlistDataBase = OnDisplayPlaylistsGlobalPlaylist_Click();

            string result = profile.FollowedPlayList[2].DisplayInfoPlayList();
            foreach (PlayList playList in playlistDataBase)
            {
                string ex = playList.DisplayInfoPlayList();
                if (result == ex)
                {
                    if (playList.Format == ".mp3" || playList.Format == ".wav")
                    {
                        foreach (Song song in playList.Songs)
                        {
                            PlayPlaylistShowMultimedia.Items.Add(song.SearchedInfoSong());
                        }
                    }
                }
            }
            PlayPlaylistPanel.BringToFront();
        }

        private void DisplayPlaylistsMoreFollowedPlaylistButton_Click(object sender, EventArgs e)
        {
            
        }
        private void DisplayPlaylistCreatedPlaylistImage1_Click(object sender, EventArgs e)
        {
            Profile profile = OnProfilesChooseProfile_Click(ProfileDomainUp.Text, UserLogInTextBox.Text, PasswordLogInTextBox.Text);
            soundPlayer = new SoundPlayer();
            List<PlayList> playlistDataBase = new List<PlayList>();
            playlistDataBase = OnDisplayPlaylistsGlobalPlaylist_Click();

            string result = profile.CreatedPlaylist[0].DisplayInfoPlayList();
            foreach (PlayList playList in playlistDataBase)
            {
                string ex = playList.DisplayInfoPlayList();
                if (result == ex)
                {
                    if (playList.Format == ".mp3" || playList.Format == ".wav")
                    {
                        foreach (Song song in playList.Songs)
                        {
                            PlayPlaylistShowMultimedia.Items.Add(song.SearchedInfoSong());
                        }
                    }
                }
            }
            PlayPlaylistPanel.BringToFront();
        }

        private void DisplayPlaylistCreatedPlaylistImage2_Click(object sender, EventArgs e)
        {
            Profile profile = OnProfilesChooseProfile_Click(ProfileDomainUp.Text, UserLogInTextBox.Text, PasswordLogInTextBox.Text);
            soundPlayer = new SoundPlayer();
            List<PlayList> playlistDataBase = new List<PlayList>();
            playlistDataBase = OnDisplayPlaylistsGlobalPlaylist_Click();

            string result = profile.CreatedPlaylist[1].DisplayInfoPlayList();
            foreach (PlayList playList in playlistDataBase)
            {
                string ex = playList.DisplayInfoPlayList();
                if (result == ex)
                {
                    if (playList.Format == ".mp3" || playList.Format == ".wav")
                    {
                        foreach (Song song in playList.Songs)
                        {
                            PlayPlaylistShowMultimedia.Items.Add(song.SearchedInfoSong());
                        }
                    }
                }
            }
            PlayPlaylistPanel.BringToFront();
        }

        private void DisplayPlaylistCreatedPlaylistImage3_Click(object sender, EventArgs e)
        {
            Profile profile = OnProfilesChooseProfile_Click(ProfileDomainUp.Text, UserLogInTextBox.Text, PasswordLogInTextBox.Text);
            soundPlayer = new SoundPlayer();
            List<PlayList> playlistDataBase = new List<PlayList>();
            playlistDataBase = OnDisplayPlaylistsGlobalPlaylist_Click();

            string result = profile.CreatedPlaylist[2].DisplayInfoPlayList();
            foreach (PlayList playList in playlistDataBase)
            {
                string ex = playList.DisplayInfoPlayList();
                if (result == ex)
                {
                    if (playList.Format == ".mp3" || playList.Format == ".wav")
                    {
                        foreach (Song song in playList.Songs)
                        {
                            PlayPlaylistShowMultimedia.Items.Add(song.SearchedInfoSong());
                        }
                    }
                }
            }
            PlayPlaylistPanel.BringToFront();
        }

        private void DisplayPlaylistCreatedPlaylistButton_Click(object sender, EventArgs e)
        {

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
        private void SearchPlayButton_Click(object sender, EventArgs e)
        {
            soundPlayer.Play();
            List<Song> songDataBase = new List<Song>();
            songDataBase = OnSearchSongButton_Click();
            foreach (Song song in songDataBase)
            {
                if (song.Format == ".mp3")
                {
                    windowsMediaPlayer.controls.play();
                    DurationTimer.Start();
                    break;
                }
                else if (song.Format == ".wav")
                {

                    DurationTimer.Start();
                }
            }
        }
        private void SearchPlayerToMultButton_Click(object sender, EventArgs e)
        {
            string mult = SearchPlayingLabel.Text;
            if(mult.Contains("Song") == true)
            {
                PlaySongPanel.BringToFront();
            }
            else if ((mult.Contains("Playlist") == true && mult.Contains(".mp3") == true) || (mult.Contains("Playlist") == true && mult.Contains(".wav") == true))
            {
                PlayPlaylistPanel.BringToFront();
            }      
        }

        private void SearchPauseBotton_Click(object sender, EventArgs e)
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
        private void SearchGoBackButton_Click(object sender, EventArgs e)
        {
            SearchSearchTextBox.Text = "Search Songs,Video, Playlists or Users";
            SearchSearchResultsDomainUp.Visible = false;
            //SearchSearchResultsDomainUp.Items.Clear();
            SearchSearchResultsDomainUp.Text = "Searched Results:";
            DisplayStartPanel.BringToFront();
            if (windowsMediaPlayer.URL != null)
            {
                PlayerPanel.BringToFront();
                PlayerPanel.Dock = DockStyle.Bottom;
            }
        }
        private void SearchSearchButton_Click(object sender, EventArgs e)
        {
            SearchSearchResultsDomainUp.ReadOnly = true;
            //SearchSearchResultsDomainUp.Items.Clear();
            SearchSearchResultsDomainUp.Text = "Searched Results:";
            
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
            List<PlayList> playlistDataBase = new List<PlayList>();
            playlistDataBase = OnDisplayPlaylistsGlobalPlaylist_Click();

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
            foreach(PlayList playlist in playlistDataBase)
            {
                if (playlist.InfoPlayList().Contains(search))
                {
                    SearchSearchResultsDomainUp.Visible = true;
                    SearchSearchResultsDomainUp.Items.Add(playlist.DisplayInfoPlayList());
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
            PlaySongChoosePlsDomainUp.Visible = false;
            PlaySongChoosePlsDomainUp.ResetText();
            PlaySongChoosePlsDomainUp.ReadOnly = true;
            PlaySongMessageTextBox.Clear();

        }

        private void SearchSelectMultButton_Click(object sender, EventArgs e)
        {
            soundPlayer = new SoundPlayer();
            List<Song> songDataBase = new List<Song>();
            songDataBase = OnSearchSongButton_Click();
            List<PlayList> playListsDataBase = new List<PlayList>();
            playListsDataBase = OnDisplayPlaylistsGlobalPlaylist_Click();
            List<Video> videoDataBase = OnSearchVideoButton_Click();


            string multimediaType = SearchSearchResultsDomainUp.Text;

            if (multimediaType.Contains("Song:") == true && multimediaType.Contains("Artist:") == true)
            {
                soundPlayer.Stop();
                windowsMediaPlayer.controls.pause();
                foreach (Song song in songDataBase)
                {
                    if (song.Format == ".mp3")
                    {
                        string result = SearchSearchResultsDomainUp.Text;
                        if (result == song.SearchedInfoSong())
                        {
                            PlayerPlayingLabel.Clear();
                            SearchPlayingLabel.Clear();
                            PlaySongProgressBar.Value = 0;
                            PlaySongTimerTextBox.ResetText();
                            windowsMediaPlayer.URL = song.SongFile;
                            DurationTimer.Interval = 1000;
                            PlaySongProgressBar.Maximum = (int)(song.Duration * 60);
                            SearchProgressBar.Maximum = (int)(song.Duration * 60);
                            PlayPlaylistProgressBarBox.Maximum = (int)(song.Duration * 60);
                            PlaySongPanel.BringToFront();
                            PlayerPlayingLabel.AppendText("Song playing: " + song.Name +song.Format);
                            SearchPlayingLabel.AppendText("Song playing: " + song.Name + song.Format);
                            DurationTimer.Start();
                            break;
                        }
                    }
                    else if (song.Format == ".wav")
                    {
                        string result = SearchSearchResultsDomainUp.Text;
                        if (result == song.SearchedInfoSong())
                        {

                            PlayerPlayingLabel.Clear();
                            SearchPlayingLabel.Clear();
                            PlaySongProgressBar.Value = 0;
                            PlaySongTimerTextBox.ResetText();
                            soundPlayer.SoundLocation = song.SongFile;
                            soundPlayer.Play();
                            DurationTimer.Interval = 1000;
                            PlaySongProgressBar.Maximum = (int)(song.Duration * 60);
                            SearchProgressBar.Maximum = (int)(song.Duration * 60);
                            PlayPlaylistProgressBarBox.Maximum = (int)(song.Duration * 60);
                            PlaySongPanel.BringToFront();
                            PlayerPlayingLabel.AppendText("Song playing: " + song.Name + song.Format);
                            SearchPlayingLabel.AppendText("Song playing: " + song.Name + song.Format);
                            DurationTimer.Start();
                            break;
                        }
                    }
                }
            }
            else if(multimediaType.Contains("PlayList Name:") == true)
            {
                string result = SearchSearchResultsDomainUp.Text;
                foreach (PlayList playList in playListsDataBase)
                {
                    string ex = playList.DisplayInfoPlayList();
                    if(result == ex)
                    {
                        if(playList.Format == ".mp3" || playList.Format == ".wav")
                        {
                            foreach(Song song in playList.Songs)
                            {
                                PlayPlaylistShowMultimedia.Items.Add(song.SearchedInfoSong());
                            }
                        }
                    }
                }
                PlayPlaylistPanel.BringToFront();
            }

            else if(multimediaType.Contains("Video:") == true)
            {
                foreach(Video video in videoDataBase)
                {
                    string result = SearchSearchResultsDomainUp.Text;
                    if (result == video.SearchedInfoVideo())
                    {
                        PlayVideoPanel.BringToFront();
                        wmpVideo.URL = video.FileName;
                    }
                }
            }

        }
     
        private void SearchFollowButton_Click(object sender, EventArgs e)
        {
            List<User> userDataBase = new List<User>();
            userDataBase = OnSearchUserButton_Click();
            Profile profile = OnProfilesChooseProfile_Click(ProfileDomainUp.Text, UserLogInTextBox.Text, PasswordLogInTextBox.Text);
            User logInUser = OnLoginButtonClicked(UserLogInTextBox.Text, PasswordLogInTextBox.Text);

            if (SearchSearchResultsDomainUp.Text.Contains("User:"))
            {
                foreach (User searchedUser in userDataBase)
                {
                    string result = SearchSearchResultsDomainUp.Text;
                    List<string> listuser = searchedUser.FollowingList;
                    if (result == "User: " + searchedUser.SearchedInfoUser())
                    {
                        OnSearchFollowButton_Click(logInUser, searchedUser,profile);
                    }
                }
            }
        }
        
        //<<PLAY SONG PANEL>>

        private void PlaySongGoBackButton_Click(object sender, EventArgs e)
        {
            SearchSearchTextBox.Text = "Search Songs,Video, Playlists or Users";
            SearchSearchResultsDomainUp.Visible = false;
            SearchPlayingPanel.Visible = true;
            //SearchSearchResultsDomainUp.Items.Clear();
            SearchSearchResultsDomainUp.Text = "Searched Results:";
            SearchPanel.BringToFront();
            SearchSearchResultsDomainUp.ResetText();

        }
        private void PlaySongAddToPlaylistButton_Click(object sender, EventArgs e)
        {
            PlaySongMessageTextBox.Clear();
            PlaySongChoosePlsDomainUp.ResetText();
            Profile profile = OnProfilesChooseProfile_Click(ProfileDomainUp.Text, UserLogInTextBox.Text, PasswordLogInTextBox.Text);
            if (profile.CreatedPlaylist.Count() != 0)
            {
                foreach(PlayList playList in profile.CreatedPlaylist)
                {
                    PlaySongChoosePlsDomainUp.Items.Add(playList.NamePlayList);
                }
                PlaySongChoosePlsButton.Visible = true;
                PlaySongChoosePlsDomainUp.Visible = true;
            }
            else
            {
                PlaySongMessageTextBox.AppendText("ERROR[!] You don´t have created Playlists");
            }
        }
        private void PlaySongChoosePlsButton_Click(object sender, EventArgs e)
        {
            PlaySongMessageTextBox.Clear();
            List<Song> songDataBase = new List<Song>();
            string result = SearchSearchResultsDomainUp.Text;
            string searchedPlaylistName = PlaySongChoosePlsDomainUp.Text;
            int choosenPl = PlaySongChoosePlsDomainUp.SelectedIndex;
            songDataBase = OnSearchSongButton_Click();
            Profile profile = OnProfilesChooseProfile_Click(ProfileDomainUp.Text, UserLogInTextBox.Text, PasswordLogInTextBox.Text);
            PlaySongChoosePlsButton_Click(songDataBase, profile, result, choosenPl, searchedPlaylistName);
            SearchSearchResultsDomainUp.ResetText();
        }
        

        private void PlaySongDownloadSongButton_Click(object sender, EventArgs e)
        {
            PlaySongMessageTextBox.Clear();
            List<string> listUser = OnLogInLogInButton_Clicked2(UserLogInTextBox.Text);
            if (listUser[3] != "standard")
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string destDirectory = desktopPath + "\\Downloaded-Songs-FyBuZz";
                if (System.IO.Directory.Exists(destDirectory) == false)
                {         
                    System.IO.Directory.CreateDirectory(destDirectory);
                    File.Create(destDirectory + "\\ FyBuZz.txt");
                    string songFile = windowsMediaPlayer.URL.Split('\\')[windowsMediaPlayer.URL.Split('\\').Length - 1];
                    string destFile = destDirectory + "\\" + songFile;
                    File.Copy(windowsMediaPlayer.URL, destFile);
                }
                else
                {
                    string songFile = windowsMediaPlayer.URL.Split('\\')[windowsMediaPlayer.URL.Split('\\').Length - 1];
                    string destFile = destDirectory + "\\" + songFile;
                    File.Copy(windowsMediaPlayer.URL, destFile);
                }        
                PlaySongMessageTextBox.AppendText("Song downloaded succesfully.");
            }
            else
            {
                PlaySongMessageTextBox.Visible = true;
                PlaySongMessageTextBox.AppendText("Standard users can't download songs.");
            }

        }
        private void DurationTimer_Tick(object sender, EventArgs e)
        {
            PlaySongProgressBar.Increment(1);
            PlayPlaylistProgressBarBox.Increment(1);
            SearchProgressBar.Increment(1);
            PlaySongTimerTextBox.Text = PlaySongProgressBar.Value.ToString();
            PlayPlaylistTimerBox.Text = PlayPlaylistProgressBarBox.Value.ToString();
            SearchTimerTextBox.Text = PlaySongProgressBar.Value.ToString();  
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
                if (song.Format == ".mp3")
                {
                    windowsMediaPlayer.controls.play();
                    DurationTimer.Start();
                    break;
                }
                else if (song.Format == ".wav")
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
            SearchSearchResultsDomainUp.ResetText();
        }

        private void CreatePlaylistCreatePlaylistButton_Click(object sender, EventArgs e)
        {
            string playlistName = CreatePlaylistNameTextBox.Text;
            string playlistFormat = CreatePlaylistFormatDomainUp.Text;
            User playlistCreator = OnLoginButtonClicked(UserLogInTextBox.Text,PasswordLogInTextBox.Text);
            Profile playlistProfileCreator = OnProfilesChooseProfile_Click(ProfileDomainUp.Text, UserLogInTextBox.Text, PasswordLogInTextBox.Text);
            bool playlistPrivacy = CreatePlaylistPrivacyCheckBox.Checked; //True si esta checked
            OnCreatePlaylistCreatePlaylistButton_Click(playlistName, playlistFormat, playlistPrivacy, playlistCreator, playlistProfileCreator);
            OnSearchUserButton_Click();
        }
        //<<PLAY PLAYLIST PANEL
        private void PlayPlaylistGoBackButton_Click(object sender, EventArgs e)
        {
            SearchSearchTextBox.Text = "Search Songs,Video, Playlists or Users";
            SearchSearchResultsDomainUp.Visible = false;
            SearchPlayingPanel.Visible = true;
            //SearchSearchResultsDomainUp.Items.Clear();
            SearchSearchResultsDomainUp.Text = "Searched Results:";
            SearchPanel.BringToFront();
            SearchSearchResultsDomainUp.ResetText();
        }

        private void PlayPlaylistChooseMultimediaButton_Click(object sender, EventArgs e)
        {
            PlayPlaylistProgressBarBox.Value = 0;
            PlayPlaylistTimerBox.Clear();
            soundPlayer.Stop();
            windowsMediaPlayer.controls.pause();
            PlayPlaylistMessageBox.Clear();
            PlayPlaylistPlayerPanel.Visible = true;
            List<Song> songDataBase = new List<Song>();
            songDataBase = OnSearchSongButton_Click();
            string multimediaType = PlayPlaylistShowMultimedia.Text;
            if (multimediaType.Contains("Song:") == true && multimediaType.Contains("Artist:") == true)
            {
                foreach (Song song in songDataBase)
                {
                    if (song.Format == ".mp3")
                    {
                        if (multimediaType == song.SearchedInfoSong())
                        {
                            PlayPlaylistMessageBox.Clear();
                            PlaySongProgressBar.Value = 0;
                            PlaySongTimerTextBox.Clear();
                            windowsMediaPlayer.URL = song.SongFile;
                            DurationTimer.Interval = 1000;
                            PlaySongProgressBar.Maximum = (int)(song.Duration * 60);
                            SearchProgressBar.Maximum = (int)(song.Duration * 60);

                            PlayPlaylistMessageBox.AppendText("Playlist playing: " + song.Name + song.Format);
                            SearchPlayingLabel.AppendText("Playlist playing: " + song.Name + song.Format);
                            DurationTimer.Start();
                            break;
                        }
                    }
                    else if (song.Format == ".wav")
                    {
                        if (multimediaType == song.SearchedInfoSong())
                        {

                            PlayPlaylistMessageBox.Clear();
                            PlaySongProgressBar.Value = 0;
                            PlaySongTimerTextBox.ResetText();
                            soundPlayer.SoundLocation = song.SongFile;
                            soundPlayer.Play();
                            DurationTimer.Interval = 1000;
                            PlaySongProgressBar.Maximum = (int)(song.Duration * 60);
                            SearchProgressBar.Maximum = (int)(song.Duration * 60);

                            PlayPlaylistMessageBox.AppendText("Playlist playing: " + song.Name + song.Format);
                            SearchPlayingLabel.AppendText("Playlist playing: " + song.Name + song.Format);
                            DurationTimer.Start();
                            break;
                        }
                    }
                }
            }
        }
        private void PlayPlaylistRandomButton_Click(object sender, EventArgs e)
        {

        }
        private void PlayPlaylistPlayButton_Click(object sender, EventArgs e)
        {
            soundPlayer.Play();
            List<Song> songDataBase = new List<Song>();
            songDataBase = OnSearchSongButton_Click();
            string ex = PlayPlaylistShowMultimedia.Text;
            foreach (Song song in songDataBase)
            {
                if (PlayPlaylistShowMultimedia.Text.Contains("Song:") && song.Format == ".mp3")
                {
                    windowsMediaPlayer.controls.play();
                    DurationTimer.Start();
                    break;
                }
                else if (PlayPlaylistShowMultimedia.Text.Contains("Song:") && song.Format == ".wav")
                {
                    DurationTimer.Start();
                }
            }
        }

        private void PlayPlaylistPauseButton_Click(object sender, EventArgs e)
        {
            soundPlayer.Stop();
            List<Song> songDataBase = new List<Song>();
            songDataBase = OnSearchSongButton_Click();
            string ex = PlayPlaylistShowMultimedia.Text;
            foreach (Song song in songDataBase)
            {
                if (PlayPlaylistShowMultimedia.Text.Contains("Song:") && song.Format == ".mp3")
                {
                    windowsMediaPlayer.controls.pause();
                    DurationTimer.Stop();
                    break;
                }
                else if (PlayPlaylistShowMultimedia.Text.Contains("Song:") && song.Format == ".wav")
                {
                    DurationTimer.Stop();
                }
            }
        }

        private void PlayPlaylistPreviousButton_Click(object sender, EventArgs e)
        {

        }

        private void PlayPlaylistSkipButton_Click(object sender, EventArgs e)
        {

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
            openFileDialog.Filter = "Media Files|*.mp4;*.avi;*.mov";

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filename = openFileDialog.FileName;
                CreateVideoLoadVideoTextBox.Text = filename;
            }
        }

        //MÉTODOS INTERNOS 

        public List<string> OnLogInLogInButton_Clicked2(string username)
        {
            List<string> userGetterStringList = new List<string>();
            List<string> userProfileNames = new List<string>();
            if(LogInLogInButton_Clicked2 != null)
            {
                userGetterStringList = LogInLogInButton_Clicked2(this, new UserEventArgs() { UsernameText = username });
                if (userGetterStringList != null)
                {
                    LogInInvalidCredentialsTetxbox.AppendText("Log-In Succesfull");
                    Thread.Sleep(2000);
                    LogInInvalidCredentialsTetxbox.Visible = true;

                    ProfilesWelcomeTextBox.AppendText("Welcome to FyBuZz " + userGetterStringList[0]);
                    userProfileNames = GetProfileNames(this, new UserEventArgs() { UsernameText = username });
                    foreach (string profilename in userProfileNames)
                    {
                        ProfileDomainUp.Items.Add(profilename);
                    }
                    return userGetterStringList;
                }
                else
                {
                    LogInInvalidCredentialsTetxbox.AppendText("Incorrect Username or Password");
                    Thread.Sleep(2000);
                    LogInInvalidCredentialsTetxbox.Visible = true;
                    return null;
                }
                
            }
            return null;
        }
        public void OnRegisterRegisterButtonClicked(string username, string email, string psswd, string subs, bool priv, string gender, DateTime birthday, string profileType)
        {
            if (RegisterRegisterButton_Clicked != null)
            {
                bool result = RegisterRegisterButton_Clicked(this, new RegisterEventArgs() { UsernameText = username, EmailText = email, PasswrodText = psswd, SubsText = subs, PrivacyText = priv, GenderText = gender, BirthdayText = birthday, ProfileTypeText = profileType });
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

        public void OnCreateProfileCreateProfileButton_Click2(string username, string pswd, string pName, string pGender, string pType, string pEmail, DateTime pBirth, Image pPic)
        {
            if (CreateProfileCreateProfileButton_Clicked != null)
            {
                string result = CreateProfileCreateProfileButton_Clicked(this, new ProfileEventArgs() { UsernameText = username, PasswordText = pswd, ProfileNameText = pName, EmailText = pEmail, GenderText = pGender, BirthdayText = pBirth, ProfileTypeText = pType, PicImage = pPic });
                if (result != null)
                {
                    ProfileDomainUp.Items.Add(result);
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
        public List<string> OnProfilesChooseProfile_Click2(string pName, string usr, string pass)
        {
            if (ProfilesChooseProfile_Clicked2 != null)
            {
                List<string> choosenProfile = ProfilesChooseProfile_Clicked2(this, new ProfileEventArgs() { ProfileNameText = pName, UsernameText = usr, PasswordText = pass });
                ProfilesInvalidCredentialTextBox.ResetText();
                ProfilesInvalidCredentialTextBox.AppendText("Entering FyBuZz with... " + choosenProfile[0]);

                Thread.Sleep(2000);

                return choosenProfile;
            }
            else
            {
                return null;
            }
        }
        //SIN MVC

        public User OnLoginButtonClicked(string username, string password)
            //Este metodo ya no deberia servir, no es MVC
        {
            User user = new User();
            if(LogInLogInButton_Clicked != null)
            {
                user = LogInLogInButton_Clicked(this, new LogInEventArgs() { UsernameText = username, PasswrodText = password });
                if (user == null) //Resultado es falso
                {
                    LogInInvalidCredentialsTetxbox.AppendText("Incorrect Username or Password");
                    Thread.Sleep(2000);
                    LogInInvalidCredentialsTetxbox.Visible = true;
                }
                else
                {
                    LogInInvalidCredentialsTetxbox.AppendText("Log-In Succesfull");
                    Thread.Sleep(2000);
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
            
        
        /*public void OnCreateProfileCreateProfileButton_Click(string username, string pswd, string pName,string pGender, string pType, string pEmail, DateTime pBirth, Image pPic)
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
        }*/
        private void PlaySongChoosePlsButton_Click(List<Song> songDataBase, Profile profile, string result, int choosenPl, string searchedPL)
        {
            if (PlaySongChoosePlsButton_Clicked != null)
            {
                string final = PlaySongChoosePlsButton_Clicked(this, new PlaylistEventArgs() { RestultText = result, ChoosenIndex = choosenPl, SongDataBaseText = songDataBase, ProfileCreatorText = profile,SearchedPlaylistNameText = searchedPL });
                if (final == null)
                {
                    PlaySongMessageTextBox.AppendText("Song added succesfully.");
                    OnSearchUserButton_Click();
                }
                else
                {
                    PlaySongMessageTextBox.AppendText("ERROR[!] couldn´t add song.");
                    Thread.Sleep(1000);
                    PlaySongMessageTextBox.Clear();

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
        public List<PlayList> OnDisplayPlaylistsGlobalPlaylist_Click()
        {
            if(DisplayPlaylistsGlobalPlaylist_Clicked != null)
            {
                List<PlayList> listPlaylist = DisplayPlaylistsGlobalPlaylist_Clicked(this, new PlaylistEventArgs()); //Nose si es necesario darle parametros
                return listPlaylist;
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
        public void OnSearchFollowButton_Click(User userLogIn, User userSearched, Profile profilesearched)
        {
            if(SearchFollowButton_Clicked != null)
            {
                string result = SearchFollowButton_Clicked(this, new UserEventArgs() {UserLogIn = userLogIn, UserSearched = userSearched, ProfileUserLogIn = profilesearched });
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


        //PlayVideoPanel
        //--------------------------------------


        private void PlayVideoPlayButton_Click(object sender, EventArgs e)
        {
            wmpVideo.Ctlcontrols.play();
        }

        private void PlayVideoPauseButton_Click(object sender, EventArgs e)
        {
            wmpVideo.Ctlcontrols.pause();
        }

        private void PlayVideoStopButton_Click(object sender, EventArgs e)
        {
            wmpVideo.Ctlcontrols.stop();
        }

        private void PlayVideoGoBackButton_Click(object sender, EventArgs e)
        {
            SearchPanel.BringToFront();
        }

        
    }
}
