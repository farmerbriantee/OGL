﻿namespace OGL
{
    partial class Form_OGL
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
            this.glWin = new OpenTK.GLControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblSegments = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // glWin
            // 
            this.glWin.BackColor = System.Drawing.Color.Black;
            this.glWin.ForeColor = System.Drawing.Color.Coral;
            this.glWin.Location = new System.Drawing.Point(10, 8);
            this.glWin.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.glWin.Name = "glWin";
            this.glWin.Size = new System.Drawing.Size(502, 447);
            this.glWin.TabIndex = 0;
            this.glWin.VSync = false;
            this.glWin.Load += new System.EventHandler(this.glWin_Load);
            this.glWin.Paint += new System.Windows.Forms.PaintEventHandler(this.glWin_Paint);
            this.glWin.Resize += new System.EventHandler(this.glWin_Resize);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblSegments
            // 
            this.lblSegments.AutoSize = true;
            this.lblSegments.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblSegments.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblSegments.Location = new System.Drawing.Point(14, 14);
            this.lblSegments.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSegments.Name = "lblSegments";
            this.lblSegments.Size = new System.Drawing.Size(68, 23);
            this.lblSegments.TabIndex = 1;
            this.lblSegments.Text = "label1";
            // 
            // Form_OGL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(519, 470);
            this.Controls.Add(this.lblSegments);
            this.Controls.Add(this.glWin);
            this.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "Form_OGL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Never Twice";
            this.Load += new System.EventHandler(this.Form_OGL_Load);
            this.ResizeEnd += new System.EventHandler(this.Form_OGL_ResizeEnd);
            this.ClientSizeChanged += new System.EventHandler(this.Form_OGL_ClientSizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenTK.GLControl glWin;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblSegments;
    }
}

