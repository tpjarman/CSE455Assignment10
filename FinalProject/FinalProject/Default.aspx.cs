using BusinessLoigc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //get the session counter variable and display it to the user on page load
            int sessionCounter = (int)Application["SessionCounter"];
            SessionCounterTextBox.Text = "Session counter is: " + sessionCounter;
        }

        protected void PasswordDemo_Click(object sender, EventArgs e)
        {
            //call the class library to demonstrate the encryption/decryption code
            string encrptedText = SecurityManager.Encrypt(PasswordTextBox.Text);
            string decryptedText = SecurityManager.Decrypt(encrptedText);
            PasswordDemoResult.Text = "Password Encrypted: " + encrptedText + " Password Decrypted: " + decryptedText;
        }
    }
}