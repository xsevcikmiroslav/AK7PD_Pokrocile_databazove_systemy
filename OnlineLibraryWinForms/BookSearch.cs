using BusinessLayer.Managers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineLibraryWinForms
{
    public partial class BookSearch : UserControl
    {
        public BookSearch()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //pnlSearchResult.Controls.Clear();

            var bookManager = Program.MyServiceProvider.GetRequiredService<IBookManager>();
            int.TryParse(tbYearOfPublication.Text, out int yearOfPublication);
            var books = bookManager.Find(BusinessLayer.Managers.FindType.OR, tbTitle.Text, tbAuthor.Text, yearOfPublication, string.Empty);

            var increment = 0;
            foreach (var book in books)
            {
                var bookInfo = new BookInfo();
                bookInfo.SetBook(book);
                pnlSearchResult.Controls.Add(bookInfo);
                bookInfo.Location = new Point(0, 0 + increment);
                increment += 40;
            }
        }
    }
}
