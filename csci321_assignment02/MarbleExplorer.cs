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
        private string exportDir = "";
        private bool isFile = false;
        private string currentlySelectedItemName = "";
        private string currentlySelectedItemPath = "";

        public MarbleExplorer(string startingPath, string exportDir = null)
        {
            InitializeComponent();
            this.exportDir = exportDir ?? startingPath;
            filePath = PathText.Text = startingPath;
        }

        private void PathText_TextChanged(object sender, EventArgs e)
        {
            // This will trigger the view to refresh with new folder contents
            filePath = PathText.Text;
            LoadFilesAndDirectories();
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            // This will open the selected file to begin extraction and usage
            filePath = currentlySelectedItemPath;
            LoadFilesAndDirectories();
        }

        private void ExploreButton_Click(object sender, EventArgs e)
        {
            // This will open a file browser to navigate to a new path, then execute the OpenNewPath method
            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Select target path" })
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    isFile = false;
                    PathText.Text = fbd.SelectedPath;
                }
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            // Go to Parent Directory, if possible
            isFile = false;
            PathText.Text = Directory.GetParent(PathText.Text).FullName;
            LoadFilesAndDirectories();
        }

        private void MarbleExplorer_Load(object sender, EventArgs e)
        {
            LoadFilesAndDirectories();
        }

        public void LoadFilesAndDirectories()
        {
            DirectoryInfo fileList;
            FileAttributes fileAttr;
            try
            {

                if (isFile)
                {
                    //string tempFilePath = filePath + "/" + currentlySelectedItemName;
                    FileInfo fileDetails = new FileInfo(currentlySelectedItemPath);
                    fileAttr = File.GetAttributes(currentlySelectedItemPath);
                    Console.WriteLine("Determining Action for: " + currentlySelectedItemPath);
                    // TODO
                }
                else
                {
                    fileAttr = File.GetAttributes(filePath);
                    PathText.Text = filePath;
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
                            case ".MRB":
                                DataView.Items.Add(files[i].Name, 0);
                                break;

                            default:
                                //DataView.Items.Add(files[i].Name, Icon.ExtractAssociatedIcon(files[i].Name));
                                break;
                        }

                    }

                    for (int i = 0; i < dirs.Length; i++)
                    {
                        DataView.Items.Add(dirs[i].Name, 1);
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

            // Form Updates, if needed
            if (Directory.GetParent(filePath) == null)
            {
                BackButton.Enabled = false;
            }
        }

        private void DataView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            currentlySelectedItemName = e.Item.Text;
            currentlySelectedItemPath = filePath + "/" + currentlySelectedItemName;

            FileAttributes fileAttr = File.GetAttributes(currentlySelectedItemPath);
            if ((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                isFile = false;
                UpdatePreviewData();
            }
            else
            {
                isFile = true;
                // Start Extraction for .mrb, if complicit
                if (currentlySelectedItemPath.EndsWith(".mrb"))
                {
                    // Stage 1: Identify whether the cache directory for this file exists
                    // - If so, we remove this directory if it contains no files, but parse through it if it does
                    string stagedDir = exportDir + Path.DirectorySeparatorChar + currentlySelectedItemName.Replace(".mrb", "");
                    if (Directory.Exists(stagedDir))
                    {
                        if (Directory.GetFiles(stagedDir) == null)
                        {
                            Directory.Delete(stagedDir);
                        } else
                        {
                            UpdatePreviewData(stagedDir);
                        }
                    } else
                    {

                    }
                } else
                {
                    UpdatePreviewData();
                }
            }
            OpenButton.Enabled = isFile;
        }

        private void UpdatePreviewData(string stagingDirectory = null)
        {
            if (stagingDirectory == null)
            {
                FilePreviewBox.Image = null;
                WallsLabel.Text = "Walls: 0";
                BallsLabel.Text = "Balls: 0";
                SizeLabel.Text = "Size: 0";
            } else
            {
                // TODO
            }
        }

        private void DataView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            filePath = currentlySelectedItemPath;
            LoadFilesAndDirectories();
        }
    }
}
