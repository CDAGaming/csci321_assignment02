using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace csci321_assignment02
{
    public partial class MarbleExplorer : Form
    {
        private string filePath { get; set; }
        private bool isFile = false;
        private string currentlySelectedItemName = "";

        public MarbleExplorer(string startingPath)
        {
            InitializeComponent();
            filePath = PathText.Text = startingPath;
        }

        private void PathText_TextChanged(object sender, EventArgs e)
        {
            // This will trigger the browser to refresh with new folder contents
            filePath = PathText.Text;
            LoadFilesAndDirectories();
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            // This will open the selected file to begin extraction and usage
            //Console.WriteLine("Line: " + GetSelectedText());
        }

        private void ExploreButton_Click(object sender, EventArgs e)
        {
            // This will open a file browser to navigate to a new path, then execute the OpenNewPath method
            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Select target path" })
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    filePath = PathText.Text = fbd.SelectedPath;
                }
            }
        }

        private void OpenTargetFile(string folderPath)
        {
            // TODO
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            // TODO
        }

        private void ForwardButton_Click(object sender, EventArgs e)
        {
            // TODO
        }

        private void MarbleExplorer_Load(object sender, EventArgs e)
        {
            LoadFilesAndDirectories();
        }

        public void LoadFilesAndDirectories()
        {
            DirectoryInfo fileList;
            string tempFilePath = "";
            FileAttributes fileAttr;
            try
            {

                if (isFile)
                {
                    tempFilePath = filePath + "/" + currentlySelectedItemName;
                    FileInfo fileDetails = new FileInfo(tempFilePath);
                    //fileNameLabel.Text = fileDetails.Name;
                    //fileTypeLabel.Text = fileDetails.Extension;
                    fileAttr = File.GetAttributes(tempFilePath);
                    Process.Start(tempFilePath);
                }
                else
                {
                    fileAttr = File.GetAttributes(filePath);

                }

                if ((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    fileList = new DirectoryInfo(filePath);
                    FileInfo[] files = fileList.GetFiles(); // GET ALL THE FILES
                    DirectoryInfo[] dirs = fileList.GetDirectories(); // GET ALL THE DIRS
                    string fileExtension = "";
                    DataView.Items.Clear();

                    for (int i = 0; i < files.Length; i++)
                    {
                        fileExtension = files[i].Extension.ToUpper();
                        switch (fileExtension)
                        {
                            /*case ".PNG":
                            case ".JPG":
                            case ".JPEG":
                                DataView.Items.Add(files[i].Name, 9);
                                break;
                            case ".TXT":
                                DataView.Items.Add(files[i].Name, 2);
                                break;*/
                            case ".MRB":
                                DataView.Items.Add(files[i].Name, 1);
                                break;

                            default:
                                //DataView.Items.Add(files[i].Name, 8);
                                break;
                        }

                    }

                    for (int i = 0; i < dirs.Length; i++)
                    {
                        DataView.Items.Add(dirs[i].Name, 0);
                    }
                }
                else
                {
                    DataView.Text = this.currentlySelectedItemName;
                }
            }
            catch (Exception e)
            {
                // TODO
            }
        }
    }
}
