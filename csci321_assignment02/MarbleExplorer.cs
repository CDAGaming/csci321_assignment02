using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace csci321_assignment02
{
    public partial class MarbleExplorer : Form
    {
        private string filePath;
        private readonly string exportDir = "";
        private bool isFile = false;
        private string currentlySelectedItemName = "";
        public string currentlySelectedItemPath = "";

        public string returnDirectory { get; set; }

        public MarbleExplorer(string startingPath, string exportDir = null)
        {
            InitializeComponent();
            this.exportDir = exportDir ?? startingPath;
            filePath = PathText.Text = startingPath;
        }

        private void PathText_TextChanged(object sender, EventArgs e)
        {
            // This will trigger the view to refresh with new folder contents
            if (!PathText.Focused)
            {
                filePath = PathText.Text;
                LoadFilesAndDirectories();
                UpdatePreviewData();
            }
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            // This will open the selected file to begin extraction and usage
            if (!isFile)
            {
                filePath = currentlySelectedItemPath;
            }
            LoadFilesAndDirectories();

            if (isFile)
            {
                // Close after Resync
                DialogResult = DialogResult.OK;
                Close();
            }
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
                    FileInfo fileDetails = new FileInfo(currentlySelectedItemPath);
                    fileAttr = File.GetAttributes(currentlySelectedItemPath);
                    Console.WriteLine("Determining Action for: " + currentlySelectedItemPath);
                    if (currentlySelectedItemPath.EndsWith(".mrb"))
                    {
                        Console.WriteLine(".mrb File detected; Assume Extracted and using cached data");
                        Console.WriteLine("Cached Directory: " + returnDirectory);
                        // Close after Resync
                        DialogResult = DialogResult.OK;
                        Close();
                    }
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
                Console.WriteLine("An exception has occured: " + e.Message);
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
            if ((fileAttr & FileAttributes.Directory) == FileAttributes.Directory || !e.IsSelected)
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
                        }
                        else
                        {
                            UpdatePreviewData(stagedDir);
                        }
                    }
                    else
                    {
                        ZipFile.ExtractToDirectory(currentlySelectedItemPath, stagedDir);
                        UpdatePreviewData(stagedDir);
                    }
                }
                else
                {
                    UpdatePreviewData();
                }
            }
            OpenButton.Enabled = e.IsSelected;
        }

        private void UpdatePreviewData(string stagingDirectory = null)
        {
            if (stagingDirectory == null)
            {
                FilePreviewBox.Image = null;
                WallsLabel.Text = "Walls: 0";
                BallsLabel.Text = "Balls: 0";
                SizeLabel.Text = "Size: 0";
            }
            else
            {
                // Scaling Data
                int resWidth = 250;
                int resHeight = 250;
                Image returnImg = Image.FromFile(stagingDirectory + Path.DirectorySeparatorChar + "puzzle.jpg");

                float ratio;
                // Scaling ratio for gameboard
                if (returnImg.Width >= resWidth) // shrink original image
                {
                    if (returnImg.Width >= returnImg.Height) // shrink by width
                    {
                        ratio = returnImg.Width / (float)resWidth;
                    }
                    else // shrink by height
                    {
                        ratio = returnImg.Height / (float)resHeight;
                    }
                }
                else // enlarge original image
                {
                    if (returnImg.Width >= returnImg.Height) // enlarge by width
                    {
                        ratio = resWidth / (float)returnImg.Width;
                    }
                    else // enlarge by height
                    {
                        ratio = resHeight / (float)returnImg.Height;
                    }
                }
                FilePreviewBox.Image = returnImg;
                float gridWidth = returnImg.Width / ratio;
                float gridHeight = returnImg.Height / ratio;
                FilePreviewBox.Size = new Size((int)gridWidth, (int)gridHeight);
                FilePreviewBox.SizeMode = PictureBoxSizeMode.StretchImage;

                int size = 0;
                int balls = 0;
                int walls = 0;

                // Text Data Retrieval
                try
                {
                    string[] lines = File.ReadAllLines(stagingDirectory + Path.DirectorySeparatorChar + "puzzle.txt");
                    string[] counts = lines[0].Split(' ');
                    // Counts of components
                    size = Convert.ToInt32(counts[0]);
                    balls = Convert.ToInt32(counts[1]);
                    walls = Convert.ToInt32(counts[2]);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error: Unable to parse .mrb preview data, please verify archive condition.");
                }

                WallsLabel.Text = "Walls: " + walls;
                BallsLabel.Text = "Balls: " + balls;
                SizeLabel.Text = "Size: " + size;
            }
            returnDirectory = stagingDirectory;
        }

        private void DataView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!isFile)
            {
                filePath = currentlySelectedItemPath;
            }
            LoadFilesAndDirectories();
        }

        private void PathText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                filePath = PathText.Text;
                LoadFilesAndDirectories();
                UpdatePreviewData();
            }
        }

        public static void AddFilesToZip(string zipPath, string[] files)
        {
            if (files == null || files.Length == 0)
            {
                return;
            }

            using (var zipArchive = ZipFile.Open(zipPath, ZipArchiveMode.Update))
            {
                foreach (var file in files)
                {
                    var fileInfo = new FileInfo(file);
                    ZipArchiveEntry oldEntry = zipArchive.GetEntry(fileInfo.Name);
                    if (oldEntry != null) oldEntry.Delete();
                    zipArchive.CreateEntryFromFile(fileInfo.FullName, fileInfo.Name);
                }
            }
        }
    }
}
