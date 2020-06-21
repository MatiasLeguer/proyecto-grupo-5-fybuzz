using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfaz_FyBuzz_MOD1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Diseno_Oculto();
        }

        private void Diseno_Oculto()
        {
            MultimediaIOptionsPanel.Visible = false;
            PlayListsOptionsPanel.Visible = false;
            CreateOptionsPanel.Visible = false;
            AccountSettingsPanel.Visible = false;
        }

        private void OcultarSubMenus()
        {
            if(MultimediaIOptionsPanel.Visible == true)
                MultimediaIOptionsPanel.Visible = false;
            if (PlayListsOptionsPanel.Visible == true)
                PlayListsOptionsPanel.Visible = false;
            if (CreateOptionsPanel.Visible == true)
                CreateOptionsPanel.Visible = false;
            if (AccountSettingsPanel.Visible == true)
                AccountSettingsPanel.Visible = false;
        }

        private void MostrarSubMenus(Panel SubMenu)
        {
            if (SubMenu.Visible == false)
            {
                OcultarSubMenus();
                SubMenu.Visible = true;
            }
            else
                SubMenu.Visible = false;
        }

        private void MultimediaButton_Click(object sender, EventArgs e)
        {
            MostrarSubMenus(MultimediaIOptionsPanel);
        }

        private void PlayListsButton_Click(object sender, EventArgs e)
        {
            //Codigo de Los Cabros....
            MostrarSubMenus(PlayListsOptionsPanel);
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            MostrarSubMenus(CreateOptionsPanel);
        }

     

        private void SongsButton_Click(object sender, EventArgs e)
        {
            //Codigo de Los Cabros....
            
            OcultarSubMenus();
        }

        private void VideosButton_Click(object sender, EventArgs e)
        {
            //Codigo de Los Cabros....
            
            OcultarSubMenus();
        }

        private void PrivatePlsButton_Click(object sender, EventArgs e)
        {
            //Codigo de Los Cabros....
            
            OcultarSubMenus();
        }

        private void GlobalPlsButton_Click(object sender, EventArgs e)
        {
            //Codigo de Los Cabros....
            
            OcultarSubMenus();
        }

        private void FavoritePlsButton_Click(object sender, EventArgs e)
        {
            //Codigo de Los Cabros....
            OcultarSubMenus();
        }

        private void CreateSongsButton_Click(object sender, EventArgs e)
        {
            //Codigo de Los Cabros....
            OcultarSubMenus();
        }

        private void CreateVideosButton_Click(object sender, EventArgs e)
        {
            //Codigo de Los Cabros....
            OcultarSubMenus();
        }

        private void CreatePlayListsButton_Click(object sender, EventArgs e)
        {
            //Codigo de Los Cabros....
            OcultarSubMenus();
        }

        private void CreateProfilesButton_Click(object sender, EventArgs e)
        {
            //Codigo de Los Cabros....
            OcultarSubMenus();
        }

        private void AccountSettingsButton_Click(object sender, EventArgs e)
        {
            //Codigo de Los Cabros....
            MostrarSubMenus(AccountSettingsPanel);
            
            
        }

        private void GeneralPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BackWardMediaButton_Click(object sender, EventArgs e)
        {

        }

        private void UserInfoButton_Click(object sender, EventArgs e)
        {
            OcultarSubMenus();
        }

        private void ProfilesInfoButton_Click(object sender, EventArgs e)
        {
            OcultarSubMenus();
        }

        private void AboutUsButton_Click(object sender, EventArgs e)
        {

        }

        private void CreateSongNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void CreateSongPanel_Paint(object sender, PaintEventArgs e)
        {

        }

<<<<<<< HEAD
        private void iconButton1_Click(object sender, EventArgs e)
=======
        private void PlayMediaButton_Click(object sender, EventArgs e)
>>>>>>> origin/master
        {

        }
    }
}
