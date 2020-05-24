using Entrega3_FyBuZz.CustomArgs;
using Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Entrega3_FyBuZz
{
    public partial class FyBuZz : Form
    {
        public delegate bool LogInEventHandler(object soruce, LogInEventArgs args);
        public event LogInEventHandler LogInLogInButton_Clicked;

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
            //Toma todo lo que se inscribieron en los textbox...
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
                    LogInInvalidCredentialsLabel.Text = "Incorrect Username or Password";
                    LogInInvalidCredentialsLabel.Visible = true;
                }
                else
                {
                    LogInInvalidCredentialsLabel.ResetText();
                    LogInInvalidCredentialsLabel.Visible = false;
                }
            }
        }
    }
}
