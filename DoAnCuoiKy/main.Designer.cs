namespace DoAnCuoiKy
{
    partial class main
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
            this.btnGui = new System.Windows.Forms.Button();
            this.txtGui = new System.Windows.Forms.TextBox();
            this.rtbMess = new System.Windows.Forms.RichTextBox();
            this.btnGoi = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGui
            // 
            this.btnGui.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGui.Location = new System.Drawing.Point(841, 788);
            this.btnGui.Name = "btnGui";
            this.btnGui.Size = new System.Drawing.Size(224, 96);
            this.btnGui.TabIndex = 0;
            this.btnGui.Text = "Gửi";
            this.btnGui.UseVisualStyleBackColor = true;
            this.btnGui.Click += new System.EventHandler(this.btnGui_Click);
            // 
            // txtGui
            // 
            this.txtGui.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGui.Location = new System.Drawing.Point(75, 788);
            this.txtGui.Multiline = true;
            this.txtGui.Name = "txtGui";
            this.txtGui.Size = new System.Drawing.Size(713, 96);
            this.txtGui.TabIndex = 1;
            // 
            // rtbMess
            // 
            this.rtbMess.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbMess.Location = new System.Drawing.Point(75, 73);
            this.rtbMess.Name = "rtbMess";
            this.rtbMess.ReadOnly = true;
            this.rtbMess.Size = new System.Drawing.Size(1260, 649);
            this.rtbMess.TabIndex = 2;
            this.rtbMess.Text = "";
            // 
            // btnGoi
            // 
            this.btnGoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoi.Location = new System.Drawing.Point(1111, 788);
            this.btnGoi.Name = "btnGoi";
            this.btnGoi.Size = new System.Drawing.Size(224, 96);
            this.btnGoi.TabIndex = 3;
            this.btnGoi.Text = "Gọi";
            this.btnGoi.UseVisualStyleBackColor = true;
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1402, 929);
            this.Controls.Add(this.btnGoi);
            this.Controls.Add(this.rtbMess);
            this.Controls.Add(this.txtGui);
            this.Controls.Add(this.btnGui);
            this.Name = "main";
            this.Text = "main";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGui;
        private System.Windows.Forms.TextBox txtGui;
        private System.Windows.Forms.RichTextBox rtbMess;
        private System.Windows.Forms.Button btnGoi;
    }
}