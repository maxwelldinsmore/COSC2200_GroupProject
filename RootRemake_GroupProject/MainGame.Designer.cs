namespace RootRemake_GroupProject
{
    partial class MainGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainGame));
            picBoxBoard = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picBoxBoard).BeginInit();
            SuspendLayout();
            // 
            // picBoxBoard
            // 
            picBoxBoard.Dock = DockStyle.Fill;
            picBoxBoard.Image = (Image)resources.GetObject("picBoxBoard.Image");
            picBoxBoard.Location = new Point(0, 0);
            picBoxBoard.Name = "picBoxBoard";
            picBoxBoard.Size = new Size(827, 758);
            picBoxBoard.SizeMode = PictureBoxSizeMode.Zoom;
            picBoxBoard.TabIndex = 0;
            picBoxBoard.TabStop = false;
            picBoxBoard.Click += picBoxBoard_Click;
            // 
            // MainGame
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(827, 758);
            Controls.Add(picBoxBoard);
            MaximumSize = new Size(845, 805);
            MinimumSize = new Size(845, 805);
            Name = "MainGame";
            Text = "MainGame";
            Load += MainGame_Load;
            MouseMove += MainGame_MouseMove;
            Resize += MainGame_Resize;
            ((System.ComponentModel.ISupportInitialize)picBoxBoard).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox picBoxBoard;
    }
}