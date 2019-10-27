namespace Ex05_Othelo
{
    public partial class FormSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.buttonBoardSize = new System.Windows.Forms.Button();
            this.buttonAgainstFriend = new System.Windows.Forms.Button();
            this.buttonAgainstComputer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonBoardSize
            // 
            resources.ApplyResources(this.buttonBoardSize, "buttonBoardSize");
            this.buttonBoardSize.Name = "buttonBoardSize";
            this.buttonBoardSize.UseVisualStyleBackColor = true;
            this.buttonBoardSize.Click += new System.EventHandler(this.buttonBoardSize_Click);
            // 
            // buttonAgainstFriend
            // 
            resources.ApplyResources(this.buttonAgainstFriend, "buttonAgainstFriend");
            this.buttonAgainstFriend.Name = "buttonAgainstFriend";
            this.buttonAgainstFriend.UseVisualStyleBackColor = true;
            this.buttonAgainstFriend.Click += new System.EventHandler(this.buttonAgainstFriend_Click);
            // 
            // buttonAgainstComputer
            // 
            resources.ApplyResources(this.buttonAgainstComputer, "buttonAgainstComputer");
            this.buttonAgainstComputer.Name = "buttonAgainstComputer";
            this.buttonAgainstComputer.UseVisualStyleBackColor = true;
            this.buttonAgainstComputer.Click += new System.EventHandler(this.buttonAgainstComputer_Click);
            // 
            // formProperties
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonAgainstComputer);
            this.Controls.Add(this.buttonAgainstFriend);
            this.Controls.Add(this.buttonBoardSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formProperties";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonBoardSize;
        private System.Windows.Forms.Button buttonAgainstFriend;
        private System.Windows.Forms.Button buttonAgainstComputer;
    }
}
