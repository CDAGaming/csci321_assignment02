using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace csci321_assignment02
{
    public partial class MarbleExplorer : Form
    {
        public MarbleExplorer()
        {
            InitializeComponent();
            DataBrowser.Url = new Uri(Environment.CurrentDirectory);
        }

        private void PathText_TextChanged(object sender, EventArgs e)
        {
            // This will trigger the browser to refresh with new folder contents
            DataBrowser.Url = new Uri(PathText.Text);
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            // This will open the selected file to begin extraction and usage
        }

        private void ExploreButton_Click(object sender, EventArgs e)
        {
            // This will open a file browser to navigate to a new path, then execute the OpenNewPath method
            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Select target path" })
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    DataBrowser.Url = new Uri(fbd.SelectedPath);
                    PathText.Text = fbd.SelectedPath;
                }
            }
        }

        private void OpenTargetFile(string folderPath)
        {
            // TODO
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            if (DataBrowser.CanGoBack)
            {
                DataBrowser.GoBack();
            }
        }

        private void ForwardButton_Click(object sender, EventArgs e)
        {
            if (DataBrowser.CanGoForward)
            {
                DataBrowser.GoForward();
            }
        }

        private void DataBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            PathText.Text = DataBrowser.Url.LocalPath;
            BackButton.Enabled = DataBrowser.CanGoBack;
            ForwardButton.Enabled = DataBrowser.CanGoForward;
        }
    }
}
