
namespace csci321_assignment02
{
    partial class App
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gameBox = new System.Windows.Forms.GroupBox();
            this.controlBox = new System.Windows.Forms.GroupBox();
            this.playerName = new System.Windows.Forms.TextBox();
            this.nameLbl = new System.Windows.Forms.Label();
            this.timerObj = new IPControl3.Clock();
            this.highScores = new System.Windows.Forms.TextBox();
            this.initButton = new System.Windows.Forms.Button();
            this.rightButton = new System.Windows.Forms.Button();
            this.leftButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.OpenFileButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.controlBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.17843F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.82157F));
            this.tableLayoutPanel1.Controls.Add(this.gameBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.controlBox, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(781, 575);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // gameBox
            // 
            this.gameBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameBox.ForeColor = System.Drawing.Color.LimeGreen;
            this.gameBox.Location = new System.Drawing.Point(3, 3);
            this.gameBox.Name = "gameBox";
            this.gameBox.Size = new System.Drawing.Size(510, 569);
            this.gameBox.TabIndex = 3;
            this.gameBox.TabStop = false;
            this.gameBox.Text = "Game Board";
            // 
            // controlBox
            // 
            this.controlBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlBox.AutoSize = true;
            this.controlBox.Controls.Add(this.playerName);
            this.controlBox.Controls.Add(this.nameLbl);
            this.controlBox.Controls.Add(this.timerObj);
            this.controlBox.Controls.Add(this.highScores);
            this.controlBox.Controls.Add(this.initButton);
            this.controlBox.Controls.Add(this.rightButton);
            this.controlBox.Controls.Add(this.leftButton);
            this.controlBox.Controls.Add(this.downButton);
            this.controlBox.Controls.Add(this.upButton);
            this.controlBox.Controls.Add(this.OpenFileButton);
            this.controlBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.controlBox.ForeColor = System.Drawing.Color.LimeGreen;
            this.controlBox.Location = new System.Drawing.Point(519, 3);
            this.controlBox.Name = "controlBox";
            this.controlBox.Size = new System.Drawing.Size(259, 569);
            this.controlBox.TabIndex = 2;
            this.controlBox.TabStop = false;
            this.controlBox.Text = "Controls";
            // 
            // playerName
            // 
            this.playerName.Location = new System.Drawing.Point(67, 412);
            this.playerName.Name = "playerName";
            this.playerName.Size = new System.Drawing.Size(183, 26);
            this.playerName.TabIndex = 8;
            // 
            // nameLbl
            // 
            this.nameLbl.AutoSize = true;
            this.nameLbl.Location = new System.Drawing.Point(6, 415);
            this.nameLbl.Name = "nameLbl";
            this.nameLbl.Size = new System.Drawing.Size(55, 20);
            this.nameLbl.TabIndex = 7;
            this.nameLbl.Text = "Name:";
            // 
            // timerObj
            // 
            this.timerObj.Location = new System.Drawing.Point(11, 269);
            this.timerObj.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.timerObj.Name = "timerObj";
            this.timerObj.Size = new System.Drawing.Size(241, 135);
            this.timerObj.TabIndex = 6;
            // 
            // highScores
            // 
            this.highScores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.highScores.ForeColor = System.Drawing.Color.White;
            this.highScores.Location = new System.Drawing.Point(9, 442);
            this.highScores.Multiline = true;
            this.highScores.Name = "highScores";
            this.highScores.ReadOnly = true;
            this.highScores.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.highScores.Size = new System.Drawing.Size(241, 118);
            this.highScores.TabIndex = 5;
            this.highScores.Text = "No Scores Available";
            this.highScores.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // initButton
            // 
            this.initButton.Enabled = false;
            this.initButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.initButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.initButton.Location = new System.Drawing.Point(9, 25);
            this.initButton.Name = "initButton";
            this.initButton.Size = new System.Drawing.Size(241, 40);
            this.initButton.TabIndex = 4;
            this.initButton.Text = "Start Game";
            this.initButton.UseVisualStyleBackColor = true;
            this.initButton.Click += new System.EventHandler(this.InitButton_Click);
            // 
            // rightButton
            // 
            this.rightButton.Enabled = false;
            this.rightButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rightButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.rightButton.Location = new System.Drawing.Point(156, 167);
            this.rightButton.Name = "rightButton";
            this.rightButton.Size = new System.Drawing.Size(50, 50);
            this.rightButton.TabIndex = 3;
            this.rightButton.Text = "🠖";
            this.rightButton.UseVisualStyleBackColor = true;
            this.rightButton.Click += new System.EventHandler(this.RightButton_Click);
            // 
            // leftButton
            // 
            this.leftButton.Enabled = false;
            this.leftButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.leftButton.Location = new System.Drawing.Point(56, 167);
            this.leftButton.Name = "leftButton";
            this.leftButton.Size = new System.Drawing.Size(50, 50);
            this.leftButton.TabIndex = 2;
            this.leftButton.Text = "🠔";
            this.leftButton.UseVisualStyleBackColor = true;
            this.leftButton.Click += new System.EventHandler(this.LeftButton_Click);
            // 
            // downButton
            // 
            this.downButton.Enabled = false;
            this.downButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.downButton.Location = new System.Drawing.Point(106, 217);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(50, 50);
            this.downButton.TabIndex = 1;
            this.downButton.Text = "🠗";
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.DownButton_Click);
            // 
            // upButton
            // 
            this.upButton.Enabled = false;
            this.upButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.upButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.upButton.Location = new System.Drawing.Point(106, 117);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(50, 50);
            this.upButton.TabIndex = 0;
            this.upButton.Text = "🠕";
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.UpButton_Click);
            // 
            // OpenFileButton
            // 
            this.OpenFileButton.Enabled = false;
            this.OpenFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OpenFileButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.OpenFileButton.Location = new System.Drawing.Point(9, 71);
            this.OpenFileButton.Name = "OpenFileButton";
            this.OpenFileButton.Size = new System.Drawing.Size(241, 40);
            this.OpenFileButton.TabIndex = 4;
            this.OpenFileButton.Text = "Open File";
            this.OpenFileButton.UseVisualStyleBackColor = true;
            this.OpenFileButton.Click += new System.EventHandler(this.OpenFileButton_Click);
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(781, 575);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "App";
            this.ShowIcon = false;
            this.Text = "CSCI321 Assignment 04 - Marble Game";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.App_FormClosing);
            this.Load += new System.EventHandler(this.App_Load);
            this.ResizeEnd += new System.EventHandler(this.App_ResizeEnd);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.controlBox.ResumeLayout(false);
            this.controlBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox controlBox;
        private System.Windows.Forms.Button initButton;
        private System.Windows.Forms.Button rightButton;
        private System.Windows.Forms.Button leftButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button OpenFileButton;
        private System.Windows.Forms.GroupBox gameBox;
        private System.Windows.Forms.TextBox highScores;
        private IPControl3.Clock timerObj;
        private System.Windows.Forms.TextBox playerName;
        private System.Windows.Forms.Label nameLbl;
    }
}

