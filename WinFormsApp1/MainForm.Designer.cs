namespace WinFormsApp1
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnTick = new Button();
            btnCreateWorld = new Button();
            SuspendLayout();
            // 
            // btnTick
            // 
            btnTick.Location = new Point(169, 137);
            btnTick.Name = "btnTick";
            btnTick.Size = new Size(229, 145);
            btnTick.TabIndex = 0;
            btnTick.Text = "Tick";
            btnTick.UseVisualStyleBackColor = true;
            btnTick.Click += btnTick_Click;
            // 
            // btnCreateWorld
            // 
            btnCreateWorld.Location = new Point(169, 31);
            btnCreateWorld.Name = "btnCreateWorld";
            btnCreateWorld.Size = new Size(229, 75);
            btnCreateWorld.TabIndex = 1;
            btnCreateWorld.Text = "CreateWorld";
            btnCreateWorld.UseVisualStyleBackColor = true;
            btnCreateWorld.Click += btnCreateWorld_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1298, 415);
            Controls.Add(btnCreateWorld);
            Controls.Add(btnTick);
            Margin = new Padding(6, 7, 6, 7);
            Name = "MainForm";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button btnTick;
        private Button btnCreateWorld;
    }
}
