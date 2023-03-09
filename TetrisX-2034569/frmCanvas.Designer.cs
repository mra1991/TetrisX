
namespace TetrisX_2034569
{
    partial class frmCanvas
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
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.lblScore = new System.Windows.Forms.Label();
            this.lblPause = new System.Windows.Forms.Label();
            this.lblHint = new System.Windows.Forms.Label();
            this.btnRestart = new System.Windows.Forms.Button();
            this.lblGameOver = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Enabled = true;
            this.tmrUpdate.Interval = 10;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.Location = new System.Drawing.Point(12, 9);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(152, 29);
            this.lblScore.TabIndex = 1;
            this.lblScore.Text = "Score: 0000";
            // 
            // lblPause
            // 
            this.lblPause.AutoSize = true;
            this.lblPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPause.Location = new System.Drawing.Point(27, 70);
            this.lblPause.Name = "lblPause";
            this.lblPause.Size = new System.Drawing.Size(137, 29);
            this.lblPause.TabIndex = 2;
            this.lblPause.Text = "PAUSED ||";
            this.lblPause.Visible = false;
            // 
            // lblHint
            // 
            this.lblHint.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHint.Location = new System.Drawing.Point(12, 609);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(268, 248);
            this.lblHint.TabIndex = 3;
            this.lblHint.Text = "- Use [ARROW] keys to move and [ENTER] key to rotate your tetrimino.\r\n\r\n- Use [SP" +
    "ACE] key to Pause/Play the game.\r\n";
            // 
            // btnRestart
            // 
            this.btnRestart.AutoSize = true;
            this.btnRestart.Enabled = false;
            this.btnRestart.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestart.Location = new System.Drawing.Point(17, 114);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(163, 42);
            this.btnRestart.TabIndex = 4;
            this.btnRestart.Text = "RESTART";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Visible = false;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // lblGameOver
            // 
            this.lblGameOver.AutoSize = true;
            this.lblGameOver.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGameOver.ForeColor = System.Drawing.Color.Red;
            this.lblGameOver.Location = new System.Drawing.Point(16, 70);
            this.lblGameOver.Name = "lblGameOver";
            this.lblGameOver.Size = new System.Drawing.Size(164, 29);
            this.lblGameOver.TabIndex = 5;
            this.lblGameOver.Text = "GAME OVER";
            this.lblGameOver.Visible = false;
            // 
            // frmCanvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1030, 674);
            this.Controls.Add(this.lblGameOver);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.lblPause);
            this.Controls.Add(this.lblScore);
            this.Name = "frmCanvas";
            this.Text = "Tetris - 2034569";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmCanvas_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmCanvas_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCanvas_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer tmrUpdate;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblPause;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Label lblGameOver;
    }
}

