using BusinessLayer.BusinessObjects;

namespace OnlineLibraryWinForms
{
    public partial class BookInfo : UserControl
    {
        private Book Book { get; set; }

        public BookInfo()
        {
            InitializeComponent();
        }

        public void SetBook(Book book)
        {
            Book = book;

            lblTitle.Text = Book.Title;
            lblAuthor.Text = Book.Author;
            lblYearOfPublication.Text = Book.YearOfPublication.ToString();
            lblPages.Text = Book.NumberOfPages.ToString();
        }
    }
}
