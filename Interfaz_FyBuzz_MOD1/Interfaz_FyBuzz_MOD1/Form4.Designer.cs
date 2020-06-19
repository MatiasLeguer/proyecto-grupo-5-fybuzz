namespace Interfaz_FyBuzz_MOD1
{
    partial class Form4
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
            this.GoBackButton = new FontAwesome.Sharp.IconButton();
            this.SuspendLayout();
            // 
            // GoBackButton
            // 
            this.GoBackButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GoBackButton.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.GoBackButton.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GoBackButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.GoBackButton.IconChar = FontAwesome.Sharp.IconChar.Reply;
            this.GoBackButton.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(188)))), ((int)(((byte)(45)))));
            this.GoBackButton.IconSize = 40;
            this.GoBackButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GoBackButton.Location = new System.Drawing.Point(24, 25);
            this.GoBackButton.Name = "GoBackButton";
            this.GoBackButton.Rotation = 0D;
            this.GoBackButton.Size = new System.Drawing.Size(124, 52);
            this.GoBackButton.TabIndex = 0;
            this.GoBackButton.Text = "Return";
            this.GoBackButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GoBackButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.GoBackButton.UseVisualStyleBackColor = true;
            this.GoBackButton.Click += new System.EventHandler(this.GoBackButton_Click);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(20)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(684, 438);
            this.Controls.Add(this.GoBackButton);
            this.Name = "Form4";
            this.Text = "Form4";
            this.ResumeLayout(false);

        }

        #endregion

        private FontAwesome.Sharp.IconButton GoBackButton;
    }
}