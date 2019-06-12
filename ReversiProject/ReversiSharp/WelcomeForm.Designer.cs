namespace ReversiSharp
{
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
            this.buttonAgainstComputer.Location = new System.Drawing.Point(12, 57);
            this.buttonAgainstComputer.Name = "buttonAgainstComputer";
            this.buttonAgainstComputer.Size = new System.Drawing.Size(141, 33);
            this.buttonAgainstComputer.TabIndex = 300;
            this.buttonAgainstComputer.Text = "Against computer";
            this.buttonAgainstComputer.UseVisualStyleBackColor = true;
            this.buttonAgainstComputer.Click += new System.EventHandler(this.ButtonAgainstComputer_Click);
            // 
            // buttonAgainstPlayer
            // 
            this.buttonAgainstPlayer.Location = new System.Drawing.Point(180, 57);
            this.buttonAgainstPlayer.Name = "buttonAgainstPlayer";
            this.buttonAgainstPlayer.Size = new System.Drawing.Size(145, 33);
            this.buttonAgainstPlayer.TabIndex = 200;
            this.buttonAgainstPlayer.Text = "Against Player";
            this.buttonAgainstPlayer.UseVisualStyleBackColor = true;
            this.buttonAgainstPlayer.Click += new System.EventHandler(this.ButtonAgainstPlayer_Click);
            // 
            // buttonBoardSize
            // 
            this.buttonBoardSize.Location = new System.Drawing.Point(12, 13);
            this.buttonBoardSize.Name = "buttonBoardSize";
            this.buttonBoardSize.Size = new System.Drawing.Size(313, 38);
            this.buttonBoardSize.TabIndex = 100;
            this.buttonBoardSize.Text = "Board Size: 6x6 (Click to increase)";
            this.buttonBoardSize.UseVisualStyleBackColor = true;
            this.buttonBoardSize.Click += new System.EventHandler(this.ButtonBoardSize_Click);
            // 
            // WelcomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 102);
            this.Controls.Add(this.buttonBoardSize);
            this.Controls.Add(this.buttonAgainstPlayer);
            this.Controls.Add(this.buttonAgainstComputer);
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