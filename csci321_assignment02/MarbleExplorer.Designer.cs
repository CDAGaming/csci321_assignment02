
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
            this.BackButton = new System.Windows.Forms.Button();
            this.ForwardButton = new System.Windows.Forms.Button();
            this.OpenButton = new System.Windows.Forms.Button();
            this.PathLabel = new System.Windows.Forms.Label();
            this.PathText = new System.Windows.Forms.TextBox();
            this.DataBrowser = new System.Windows.Forms.WebBrowser();
            this.ExploreButton = new System.Windows.Forms.Button();
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
            // DataBrowser
            // 
            this.DataBrowser.Location = new System.Drawing.Point(12, 35);
            this.DataBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.DataBrowser.Name = "DataBrowser";
            this.DataBrowser.Size = new System.Drawing.Size(659, 250);
            this.DataBrowser.TabIndex = 5;
            this.DataBrowser.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.DataBrowser_Navigated);
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
            // MarbleExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(680, 307);
            this.Controls.Add(this.ExploreButton);
            this.Controls.Add(this.DataBrowser);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.Button ForwardButton;
        private System.Windows.Forms.Button OpenButton;
        private System.Windows.Forms.Label PathLabel;
        private System.Windows.Forms.TextBox PathText;
        private System.Windows.Forms.WebBrowser DataBrowser;
        private System.Windows.Forms.Button ExploreButton;
    }
}