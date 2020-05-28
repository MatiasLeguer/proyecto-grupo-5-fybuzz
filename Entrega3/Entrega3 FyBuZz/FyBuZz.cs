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

namespace Entrega3_FyBuZz
{
    public partial class FyBuZz : Form
    {
        WindowsMediaPlayer windowsMediaPlayer = new WindowsMediaPlayer();


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
        public event ListSongEventHandler SearchSearchButton_Clicked;

        public delegate List<User> ListUserEventHandler(object source, RegisterEventArgs args);
        public event ListUserEventHandler SearchUserButton_Clicked;

        private string ProfileName { get; set; }

        public FyBuZz()
        {
            InitializeComponent();
        }

        private void WelcomeLogInButton_Click(object sender, EventArgs e)
        {
            LogInPanel.BringToFront();
        }
        private void WelcomeRegisterButton_Click(object sender, EventArgs e)
        {
            RegisterPanel.BringToFront();
        }

        private void WelcomeCloseFyBuZz_Click(object sender, EventArgs e)
        {
            Close();
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
            
            /*
            foreach (string followers in user.FollowerList)
            {
                AccountSettingsFollowerListDomainUp.Items.Add(followers);
            }*/         
            


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

        private void DisplayStartCloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SearchGoBackButton_Click(object sender, EventArgs e)
        {
            DisplayStartPanel.BringToFront();
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
        //<<CREATE SONG PANEL>>
        private void DisplayStartShowAddButton_Click(object sender, EventArgs e)
        {
            AddShowPanel.BringToFront();
        }
        private void AddShowAddSongButton_Click(object sender, EventArgs e)
        {
            CreateSongPanel.BringToFront();
        }
        private void CreateSongGoBackButton_Click(object sender, EventArgs e)
        {
            AddShowPanel.BringToFront();
        }
        //<<Search Panel>>
        private void SearchSearchButton_Click(object sender, EventArgs e)
        {
            string search = SearchSearchTextBox.Text; //Bad Bunny and Trap and ... and ...

            //string[] newSearchAnd = search.Split(new string[] { " and " }, StringSplitOptions.None);

            List<string> listSearch = new List<string>();
            //listSearch.Add(search);
            List<Song> songDataBase = new List<Song>();
            songDataBase = OnSearchSearchButton_Click();
            List<User> userDataBase = new List<User>();
            userDataBase = OnSearchUserButton_Click();

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

        }
        private void SearchSelectMultButton_Click(object sender, EventArgs e)
        {
            List<Song> songDataBase = new List<Song>();
            songDataBase = OnSearchSearchButton_Click();
            List<User> userDataBase = new List<User>();     
            userDataBase = OnSearchUserButton_Click();
            foreach (Song song in songDataBase)
            {
                if (song.Format == ".mp3")
                {
                    string result = SearchSearchResultsDomainUp.Text;
                    if (result == song.SearchedInfoSong() + " Format: " + song.Format)
                    {
                        //No se como reiniciar la barra de progreso ni el timer
                        SearchProgressBar.Value = 0;
                        SearchTimerDisplayLabel.ResetText();
                        windowsMediaPlayer.URL = song.SongFile;
                        DurationTimer.Interval = 1000;
                        SearchProgressBar.Maximum = (int)(song.Duration * 60);
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

            if (SearchSearchResultsDomainUp.Text.Contains("User:"))
            {
                foreach (User user in userDataBase)
                {
                    string result = SearchSearchResultsDomainUp.Text;
                    List<string> listuser = user.FollowingList;
                    if (result == "User: " + user.SearchedInfoUser())
                    {
                        //user es el usuario que quiero seguir....
                        //Todo esto dentro de un método:user.Followers = user.Followers + 1;
                        DisplayStartPanel.BringToFront();
                        //Hacer un evento en user que revise si el usuario ya sigue 
                        //al que se esta buscando, si no lo sigue... etc, mismo metodo
                        //que en menú.
                    }
                }
            }
        }
        private void DurationTimer_Tick(object sender, EventArgs e)
        {
            SearchProgressBar.Increment(1);
            //Falta que se muestre bien el tiempo...
            SearchTimerDisplayLabel.Text =  SearchProgressBar.Value.ToString();
        }
        private void SearchPlayButton_Click(object sender, EventArgs e)
        {
            List<Song> songDataBase = new List<Song>();
            songDataBase = OnSearchSearchButton_Click();
            foreach (Song song in songDataBase)
            {
                if (SearchSearchResultsDomainUp.Text.Contains("Song:") &&  song.Format == ".mp3")
                {
                    windowsMediaPlayer.controls.play();
                    DurationTimer.Start();
                    break;   
                }
            }
        }
        private void SearchPauseButton_Click(object sender, EventArgs e)
        {
            List<Song> songDataBase = new List<Song>();
            songDataBase = OnSearchSearchButton_Click();
            foreach (Song song in songDataBase)
            {
                if (song.Format == ".mp3")
                {
                    windowsMediaPlayer.controls.pause();
                    DurationTimer.Stop();
                    break;
                }
            }
        }
        private void SearchPreviousButton_Click(object sender, EventArgs e)
        {
            List<Song> songDataBase = new List<Song>();
            songDataBase = OnSearchSearchButton_Click();
            foreach (Song song in songDataBase)
            {
                if (song.Format == ".mp3")
                {
                    windowsMediaPlayer.controls.previous();
                    break;
                }
            }
        }
        private void SearchSkipButton_Click(object sender, EventArgs e)
        {
            List<Song> songDataBase = new List<Song>();
            songDataBase = OnSearchSearchButton_Click();
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
                string songFileDest = Directory.GetCurrentDirectory();
                string songFile = songFileSource.Split('\\')[songFileSource.Split('\\').Length-1];
                File.Copy(songFileSource, songFile);
                OnCreateSongCreateSongButton_Click(songName, songArtist, songAlbum, songDiscography, songGender, songPublishDate, songStudio, songDuration, songFormat, songLyrics, songFile);
            }
            else
            {
                AddShowInvalidCredentialsLabel.Text = "You profile type is viewer, you don't have permision to create multimedia";
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
                }
                else
                {
                    RegisterInvalidCredencialsTextBox.AppendText("Registered Succesfull");
                    RegisterInvalidCredencialsTextBox.Visible = true;
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
        public void OnCreateSongCreateSongButton_Click(string sName, string sArtist, string sAlbum, string sDiscography, string sGender, DateTime sPublishDate, string sStudio, double sDuration, string sFormat, string sLyrics, string songFile)
        {
            if (CreateSongCreateSongButton_Clicked != null)
            {
                bool result = CreateSongCreateSongButton_Clicked(this, new SongEventArgs() { NameText = sName, AlbumText = sAlbum, ArtistText = sArtist, DateText = sPublishDate, DiscographyText = sDiscography, DurationText = sDuration, FormatText = sFormat, GenderText = sGender, LyricsText = sLyrics, StudioText = sStudio, FileNameText = songFile});
                if (!result)
                {
                    CreateSongInvalidSongLabel.Text = "An Error has ocurred please try again";
                    Thread.Sleep(2000);
                }
                else
                {
                    CreateSongInvalidSongLabel.Text = "Song Added to the system";
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

                    DisplayStartPanel.BringToFront();
                }
            }
        }
        public List<Song> OnSearchSearchButton_Click()
        {
            if(SearchSearchButton_Clicked != null)
            {
                List<Song> songDataBase = SearchSearchButton_Clicked(this, new SongEventArgs());
                return songDataBase;
            }
            return null;
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

        //<<ADD/SHOW MULTIMEDIA PANEL>>
        private void AddShowGoBackButton_Click(object sender, EventArgs e)
        {
            DisplayStartLabel.BringToFront();
        }
    }
}
