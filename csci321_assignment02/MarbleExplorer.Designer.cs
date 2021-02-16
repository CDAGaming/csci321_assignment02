
namespace csci321_assignment02
{
    partial class MarbleExplorer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarbleExplorer));
            this.BackButton = new System.Windows.Forms.Button();
            this.ForwardButton = new System.Windows.Forms.Button();
            this.OpenButton = new System.Windows.Forms.Button();
            this.PathLabel = new System.Windows.Forms.Label();
            this.PathText = new System.Windows.Forms.TextBox();
            this.ExploreButton = new System.Windows.Forms.Button();
            this.FilePreviewBox = new System.Windows.Forms.PictureBox();
            this.DataView = new System.Windows.Forms.ListView();
            this.SizeLabel = new System.Windows.Forms.Label();
            this.BallsLabel = new System.Windows.Forms.Label();
            this.WallsLabel = new System.Windows.Forms.Label();
            this.iconList = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.FilePreviewBox)).BeginInit();
            this.SuspendLayout();
            // 
            // BackButton
            // 
            this.BackButton.Location = new System.Drawing.Point(12, 7);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(46, 23);
            this.BackButton.TabIndex = 0;
            this.BackButton.Text = "<<";
            this.BackButton.UseVisualStyleBackColor = true;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // ForwardButton
            // 
            this.ForwardButton.Location = new System.Drawing.Point(64, 7);
            this.ForwardButton.Name = "ForwardButton";
            this.ForwardButton.Size = new System.Drawing.Size(46, 23);
            this.ForwardButton.TabIndex = 1;
            this.ForwardButton.Text = ">>";
            this.ForwardButton.UseVisualStyleBackColor = true;
            this.ForwardButton.Click += new System.EventHandler(this.ForwardButton_Click);
            // 
            // OpenButton
            // 
            this.OpenButton.Location = new System.Drawing.Point(596, 7);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(75, 23);
            this.OpenButton.TabIndex = 2;
            this.OpenButton.Text = "Open";
            this.OpenButton.UseVisualStyleBackColor = true;
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // PathLabel
            // 
            this.PathLabel.AutoSize = true;
            this.PathLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.PathLabel.Location = new System.Drawing.Point(127, 12);
            this.PathLabel.Name = "PathLabel";
            this.PathLabel.Size = new System.Drawing.Size(32, 13);
            this.PathLabel.TabIndex = 3;
            this.PathLabel.Text = "Path:";
            // 
            // PathText
            // 
            this.PathText.Location = new System.Drawing.Point(165, 9);
            this.PathText.Name = "PathText";
            this.PathText.Size = new System.Drawing.Size(371, 20);
            this.PathText.TabIndex = 4;
            this.PathText.TextChanged += new System.EventHandler(this.PathText_TextChanged);
            // 
            // ExploreButton
            // 
            this.ExploreButton.Location = new System.Drawing.Point(542, 9);
            this.ExploreButton.Name = "ExploreButton";
            this.ExploreButton.Size = new System.Drawing.Size(27, 21);
            this.ExploreButton.TabIndex = 6;
            this.ExploreButton.Text = "...";
            this.ExploreButton.UseVisualStyleBackColor = true;
            this.ExploreButton.Click += new System.EventHandler(this.ExploreButton_Click);
            // 
            // FilePreviewBox
            // 
            this.FilePreviewBox.Location = new System.Drawing.Point(5, 35);
            this.FilePreviewBox.Name = "FilePreviewBox";
            this.FilePreviewBox.Size = new System.Drawing.Size(250, 250);
            this.FilePreviewBox.TabIndex = 7;
            this.FilePreviewBox.TabStop = false;
            // 
            // DataView
            // 
            this.DataView.HideSelection = false;
            this.DataView.Location = new System.Drawing.Point(262, 35);
            this.DataView.Name = "DataView";
            this.DataView.Size = new System.Drawing.Size(409, 250);
            this.DataView.SmallImageList = this.iconList;
            this.DataView.TabIndex = 8;
            this.DataView.UseCompatibleStateImageBehavior = false;
            // 
            // SizeLabel
            // 
            this.SizeLabel.AutoSize = true;
            this.SizeLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.SizeLabel.Location = new System.Drawing.Point(12, 310);
            this.SizeLabel.Name = "SizeLabel";
            this.SizeLabel.Size = new System.Drawing.Size(39, 13);
            this.SizeLabel.TabIndex = 9;
            this.SizeLabel.Text = "Size: 0";
            // 
            // BallsLabel
            // 
            this.BallsLabel.AutoSize = true;
            this.BallsLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.BallsLabel.Location = new System.Drawing.Point(71, 310);
            this.BallsLabel.Name = "BallsLabel";
            this.BallsLabel.Size = new System.Drawing.Size(41, 13);
            this.BallsLabel.TabIndex = 10;
            this.BallsLabel.Text = "Balls: 0";
            // 
            // WallsLabel
            // 
            this.WallsLabel.AutoSize = true;
            this.WallsLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.WallsLabel.Location = new System.Drawing.Point(127, 310);
            this.WallsLabel.Name = "WallsLabel";
            this.WallsLabel.Size = new System.Drawing.Size(45, 13);
            this.WallsLabel.TabIndex = 11;
            this.WallsLabel.Text = "Walls: 0";
            // 
            // iconList
            // 
            this.iconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iconList.ImageStream")));
            this.iconList.TransparentColor = System.Drawing.Color.Transparent;
            this.iconList.Images.SetKeyName(0, "folder.png");
            this.iconList.Images.SetKeyName(1, "marble.png");
            // 
            // MarbleExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(680, 332);
            this.Controls.Add(this.WallsLabel);
            this.Controls.Add(this.BallsLabel);
            this.Controls.Add(this.SizeLabel);
            this.Controls.Add(this.DataView);
            this.Controls.Add(this.FilePreviewBox);
            this.Controls.Add(this.ExploreButton);
            this.Controls.Add(this.PathText);
            this.Controls.Add(this.PathLabel);
            this.Controls.Add(this.OpenButton);
            this.Controls.Add(this.ForwardButton);
            this.Controls.Add(this.BackButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "MarbleExplorer";
            this.ShowIcon = false;
            this.Text = "MarbleExplorer";
            this.Load += new System.EventHandler(this.MarbleExplorer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FilePreviewBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.Button ForwardButton;
        private System.Windows.Forms.Button OpenButton;
        private System.Windows.Forms.Label PathLabel;
        private System.Windows.Forms.TextBox PathText;
        private System.Windows.Forms.Button ExploreButton;
        private System.Windows.Forms.PictureBox FilePreviewBox;
        private System.Windows.Forms.ListView DataView;
        private System.Windows.Forms.Label SizeLabel;
        private System.Windows.Forms.Label BallsLabel;
        private System.Windows.Forms.Label WallsLabel;
        private System.Windows.Forms.ImageList iconList;
    }
}