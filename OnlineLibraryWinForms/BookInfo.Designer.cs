namespace OnlineLibraryWinForms
{
    partial class BookInfo
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.lblYearOfPublication = new System.Windows.Forms.Label();
            this.lblPages = new System.Windows.Forms.Label();
            this.btnBorrow = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(10, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 20);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Title";
            // 
            // lblAuthor
            // 
            this.lblAuthor.Location = new System.Drawing.Point(210, 10);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(200, 20);
            this.lblAuthor.TabIndex = 1;
            this.lblAuthor.Text = "Author";
            // 
            // lblYearOfPublication
            // 
            this.lblYearOfPublication.Location = new System.Drawing.Point(410, 10);
            this.lblYearOfPublication.Name = "lblYearOfPublication";
            this.lblYearOfPublication.Size = new System.Drawing.Size(100, 20);
            this.lblYearOfPublication.TabIndex = 2;
            this.lblYearOfPublication.Text = "2010";
            // 
            // lblPages
            // 
            this.lblPages.Location = new System.Drawing.Point(510, 10);
            this.lblPages.Name = "lblPages";
            this.lblPages.Size = new System.Drawing.Size(100, 20);
            this.lblPages.TabIndex = 3;
            this.lblPages.Text = "100";
            // 
            // btnBorrow
            // 
            this.btnBorrow.Location = new System.Drawing.Point(610, 8);
            this.btnBorrow.Name = "btnBorrow";
            this.btnBorrow.Size = new System.Drawing.Size(60, 24);
            this.btnBorrow.TabIndex = 4;
            this.btnBorrow.Text = "Borrow";
            this.btnBorrow.UseVisualStyleBackColor = true;
            // 
            // BookInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnBorrow);
            this.Controls.Add(this.lblPages);
            this.Controls.Add(this.lblYearOfPublication);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.lblTitle);
            this.Name = "BookInfo";
            this.Size = new System.Drawing.Size(900, 40);
            this.ResumeLayout(false);

        }

        #endregion

        private Label lblTitle;
        private Label lblAuthor;
        private Label lblYearOfPublication;
        private Label lblPages;
        private Button btnBorrow;
    }
}
