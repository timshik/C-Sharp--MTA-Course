﻿namespace ReversiSharp
{
    using Reversi;

    public partial class WelcomeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WelcomeForm));
            this.buttonAgainstComputer = new System.Windows.Forms.Button();
            this.buttonAgainstPlayer = new System.Windows.Forms.Button();
            this.buttonBoardSize = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonAgainstComputer
            // 
            this.buttonAgainstComputer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAgainstComputer.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonAgainstComputer.Location = new System.Drawing.Point(12, 63);
            this.buttonAgainstComputer.Name = "buttonAgainstComputer";
            this.buttonAgainstComputer.Size = new System.Drawing.Size(185, 40);
            this.buttonAgainstComputer.TabIndex = 300;
            this.buttonAgainstComputer.Text = "Play against the computer";
            this.buttonAgainstComputer.UseVisualStyleBackColor = true;
            this.buttonAgainstComputer.Click += new System.EventHandler(this.ButtonAgainstComputer_Click);
            // 
            // buttonAgainstPlayer
            // 
            this.buttonAgainstPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAgainstPlayer.Location = new System.Drawing.Point(207, 63);
            this.buttonAgainstPlayer.Name = "buttonAgainstPlayer";
            this.buttonAgainstPlayer.Size = new System.Drawing.Size(185, 40);
            this.buttonAgainstPlayer.TabIndex = 200;
            this.buttonAgainstPlayer.Text = "Play against your friend";
            this.buttonAgainstPlayer.UseVisualStyleBackColor = true;
            this.buttonAgainstPlayer.Click += new System.EventHandler(this.ButtonAgainstPlayer_Click);
            // 
            // buttonBoardSize
            // 
            this.buttonBoardSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBoardSize.Location = new System.Drawing.Point(12, 12);
            this.buttonBoardSize.Name = "buttonBoardSize";
            this.buttonBoardSize.Size = new System.Drawing.Size(380, 40);
            this.buttonBoardSize.TabIndex = 100;
            this.buttonBoardSize.Text = "Board Size: 6x6 (Click to increase)";
            this.buttonBoardSize.UseVisualStyleBackColor = true;
            this.buttonBoardSize.Click += new System.EventHandler(this.ButtonBoardSize_Click);
            // 
            // WelcomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 115);
            this.Controls.Add(this.buttonBoardSize);
            this.Controls.Add(this.buttonAgainstPlayer);
            this.Controls.Add(this.buttonAgainstComputer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WelcomeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Othello - Game Settings";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonAgainstComputer;
        private System.Windows.Forms.Button buttonAgainstPlayer;
        private System.Windows.Forms.Button buttonBoardSize;
        public static int m_BoardSize = 6;
    }
}