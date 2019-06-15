namespace ReversiSharp
{
    using Reversi;

    public partial class Game
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game));
            this.m_BackgroundPicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_BackgroundPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // m_BackgroundPicture
            // 
            this.m_BackgroundPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_BackgroundPicture.Location = new System.Drawing.Point(0, 0);
            this.m_BackgroundPicture.Name = "m_BackgroundPicture";
            this.m_BackgroundPicture.Size = new System.Drawing.Size(782, 792);
            this.m_BackgroundPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_BackgroundPicture.TabIndex = 0;
            this.m_BackgroundPicture.TabStop = false;
            this.m_BackgroundPicture.MouseClick += new System.Windows.Forms.MouseEventHandler(this.backgroundPicture_MouseClick);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(782, 792);
            this.Controls.Add(this.m_BackgroundPicture);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 839);
            this.Name = "Game";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = Strings.game_basic_title;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.game_FormClosing);
            this.Load += new System.EventHandler(this.game_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_BackgroundPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox m_BackgroundPicture;
        private System.Windows.Forms.PictureBox[,] m_Squares;
        private System.Windows.Forms.PictureBox m_PlayerTurn = new System.Windows.Forms.PictureBox();
        private System.Windows.Forms.PictureBox m_BlackPlayerMark = new System.Windows.Forms.PictureBox();
        private System.Windows.Forms.PictureBox m_WhitePlayerMark = new System.Windows.Forms.PictureBox();
    }
}