using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Runtime.Serialization;

namespace FinalProject
{
    public partial class SiteCompareUserTool : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CompareSites_Click(object sender, EventArgs e)
        {
            //call the remote service to run the compare code and display to the user
            WordService.Service1Client client = new WordService.Service1Client();
            int score = client.SiteCompareTool(Website1TextBox.Text, Website2TextBox.Text);
            WebsiteResultLabel.Text = "Score is: " + score.ToString();
        }
    }
}