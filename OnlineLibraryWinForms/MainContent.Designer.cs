namespace OnlineLibraryWinForms
{
    partial class MainContent
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.gbUser = new System.Windows.Forms.GroupBox();
            this.btnProfile = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.gbBooks = new System.Windows.Forms.GroupBox();
            this.btnBooksOverview = new System.Windows.Forms.Button();
            this.btnBooksHistory = new System.Windows.Forms.Button();
            this.btnSearchBooks = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.gbUser.SuspendLayout();
            this.gbBooks.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.gbUser);
            this.splitContainer.Panel1.Controls.Add(this.gbBooks);
            this.splitContainer.Size = new System.Drawing.Size(1000, 600);
            this.splitContainer.SplitterDistance = 60;
            this.splitContainer.TabIndex = 0;
            // 
            // gbUser
            // 
            this.gbUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbUser.Controls.Add(this.btnProfile);
            this.gbUser.Controls.Add(this.btnLogout);
            this.gbUser.Location = new System.Drawing.Point(845, 3);
            this.gbUser.Name = "gbUser";
            this.gbUser.Size = new System.Drawing.Size(152, 54);
            this.gbUser.TabIndex = 1;
            this.gbUser.TabStop = false;
            this.gbUser.Text = "User";
            // 
            // btnProfile
            // 
            this.btnProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProfile.AutoSize = true;
            this.btnProfile.Location = new System.Drawing.Point(6, 23);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(66, 25);
            this.btnProfile.TabIndex = 4;
            this.btnProfile.Text = "Profile";
            this.btnProfile.UseVisualStyleBackColor = true;
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.AutoSize = true;
            this.btnLogout.Location = new System.Drawing.Point(78, 23);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(66, 25);
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            // 
            // gbBooks
            // 
            this.gbBooks.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbBooks.Controls.Add(this.btnBooksOverview);
            this.gbBooks.Controls.Add(this.btnBooksHistory);
            this.gbBooks.Controls.Add(this.btnSearchBooks);
            this.gbBooks.Location = new System.Drawing.Point(3, 3);
            this.gbBooks.Name = "gbBooks";
            this.gbBooks.Size = new System.Drawing.Size(199, 52);
            this.gbBooks.TabIndex = 0;
            this.gbBooks.TabStop = false;
            this.gbBooks.Text = "Books";
            // 
            // btnBooksOverview
            // 
            this.btnBooksOverview.AutoSize = true;
            this.btnBooksOverview.Location = new System.Drawing.Point(125, 22);
            this.btnBooksOverview.Name = "btnBooksOverview";
            this.btnBooksOverview.Size = new System.Drawing.Size(66, 25);
            this.btnBooksOverview.TabIndex = 2;
            this.btnBooksOverview.Text = "Overview";
            this.btnBooksOverview.UseVisualStyleBackColor = true;
            // 
            // btnBooksHistory
            // 
            this.btnBooksHistory.AutoSize = true;
            this.btnBooksHistory.Location = new System.Drawing.Point(64, 22);
            this.btnBooksHistory.Name = "btnBooksHistory";
            this.btnBooksHistory.Size = new System.Drawing.Size(55, 25);
            this.btnBooksHistory.TabIndex = 1;
            this.btnBooksHistory.Text = "History";
            this.btnBooksHistory.UseVisualStyleBackColor = true;
            // 
            // btnSearchBooks
            // 
            this.btnSearchBooks.AutoSize = true;
            this.btnSearchBooks.Location = new System.Drawing.Point(6, 22);
            this.btnSearchBooks.Name = "btnSearchBooks";
            this.btnSearchBooks.Size = new System.Drawing.Size(52, 25);
            this.btnSearchBooks.TabIndex = 0;
            this.btnSearchBooks.Text = "Search";
            this.btnSearchBooks.UseVisualStyleBackColor = true;
            this.btnSearchBooks.Click += new System.EventHandler(this.btnSearchBooks_Click);
            // 
            // MainContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Name = "MainContent";
            this.Size = new System.Drawing.Size(1000, 600);
            this.splitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.gbUser.ResumeLayout(false);
            this.gbUser.PerformLayout();
            this.gbBooks.ResumeLayout(false);
            this.gbBooks.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SplitContainer splitContainer;
        private GroupBox gbUser;
        private Button btnProfile;
        private Button btnLogout;
        private GroupBox gbBooks;
        private Button btnBooksOverview;
        private Button btnBooksHistory;
        private Button btnSearchBooks;
    }
}
