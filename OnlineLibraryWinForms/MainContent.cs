using BusinessLayer.Managers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineLibraryWinForms
{
    public partial class MainContent : UserControl
    {
        private IUserManager _userManager;

        private UserControl _activeUserControl;

        public MainContent()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _userManager = Program.MyServiceProvider.GetRequiredService<IUserManager>();
        }

        private void btnSearchBooks_Click(object sender, EventArgs e)
        {
            _activeUserControl?.Hide();
            _activeUserControl = new BookSearch();
            splitContainer.Panel2.Controls.Add(_activeUserControl);
            _activeUserControl.Visible = true;
        }
    }
}
