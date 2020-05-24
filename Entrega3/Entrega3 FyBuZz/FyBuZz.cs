using Entrega3_FyBuZz.CustomArgs;
using Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Entrega3_FyBuZz
{
    public partial class FyBuZz : Form
    {
        public delegate bool LogInEventHandler(object soruce, LogInEventArgs args);
        public event LogInEventHandler LogInLogInButton_Clicked;
        public delegate bool RegisterEventHandler(object soruce, RegisterEventArgs args);
        public event RegisterEventHandler RegisterRegisterButton_Clicked;

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
            string username = UserLogInTextBox.Text;
            string pass = PasswordLogInTextBox.Text;
            OnLoginButtonClicked(username, pass);
        }

        //Metodos internos
        public void OnLoginButtonClicked(string username, string password)
        {
            if(LogInLogInButton_Clicked != null)
            {
                bool result = LogInLogInButton_Clicked(this, new LogInEventArgs() { UsernameText = username, PasswrodText = password });
                if (!result) //Resultado es falso
                {
                    LogInInvalidCredentialsTetxbox.AppendText("Incorrect Username or Password");
                    LogInInvalidCredentialsTetxbox.Visible = true;
                }
                else
                {
                    LogInInvalidCredentialsTetxbox.AppendText("Log-In Succesfull");
                    LogInInvalidCredentialsTetxbox.Visible = true;
                }
            }
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
    }
}
