namespace OnlineLibraryWinForms
{
    partial class BookSearch
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
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.tbAuthor = new System.Windows.Forms.TextBox();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.tbYearOfPublication = new System.Windows.Forms.TextBox();
            this.lblYearOfPublication = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.pnlSearchResult = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.Location = new System.Drawing.Point(18, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(29, 15);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Title";
            // 
            // tbTitle
            // 
            this.tbTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbTitle.Location = new System.Drawing.Point(53, 12);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(100, 23);
            this.tbTitle.TabIndex = 1;
            // 
            // tbAuthor
            // 
            this.tbAuthor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbAuthor.Location = new System.Drawing.Point(235, 12);
            this.tbAuthor.Name = "tbAuthor";
            this.tbAuthor.Size = new System.Drawing.Size(100, 23);
            this.tbAuthor.TabIndex = 3;
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblAuthor.Location = new System.Drawing.Point(185, 15);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(44, 15);
            this.lblAuthor.TabIndex = 2;
            this.lblAuthor.Text = "Author";
            // 
            // tbYearOfPublication
            // 
            this.tbYearOfPublication.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbYearOfPublication.Location = new System.Drawing.Point(481, 12);
            this.tbYearOfPublication.Name = "tbYearOfPublication";
            this.tbYearOfPublication.Size = new System.Drawing.Size(100, 23);
            this.tbYearOfPublication.TabIndex = 5;
            // 
            // lblYearOfPublication
            // 
            this.lblYearOfPublication.AutoSize = true;
            this.lblYearOfPublication.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblYearOfPublication.Location = new System.Drawing.Point(367, 15);
            this.lblYearOfPublication.Name = "lblYearOfPublication";
            this.lblYearOfPublication.Size = new System.Drawing.Size(108, 15);
            this.lblYearOfPublication.TabIndex = 4;
            this.lblYearOfPublication.Text = "Year Of Publication";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(642, 11);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // pnlSearchResult
            // 
            this.pnlSearchResult.AutoScroll = true;
            this.pnlSearchResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSearchResult.Location = new System.Drawing.Point(10, 50);
            this.pnlSearchResult.Name = "pnlSearchResult";
            this.pnlSearchResult.Size = new System.Drawing.Size(980, 480);
            this.pnlSearchResult.TabIndex = 7;
            // 
            // BookSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlSearchResult);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.tbYearOfPublication);
            this.Controls.Add(this.lblYearOfPublication);
            this.Controls.Add(this.tbAuthor);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.tbTitle);
            this.Controls.Add(this.lblTitle);
            this.Name = "BookSearch";
            this.Size = new System.Drawing.Size(1000, 540);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblTitle;
        private TextBox tbTitle;
        private TextBox tbAuthor;
        private Label lblAuthor;
        private TextBox tbYearOfPublication;
        private Label lblYearOfPublication;
        private Button btnSearch;
        private Panel pnlSearchResult;
    }
}
