using BusinessLayer.BusinessObjects;
using BusinessLayer.Managers.Interfaces;

namespace OnlineLibraryWinForms
{
    public partial class LoginForm : Form
    {
        private IUserManager _userManager;

        public LoginForm(IUserManager userManager)
        {
            InitializeComponent();

            _userManager = userManager;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            User user;
            if ((user = _userManager.LoginUser(tbUsername.Text, tbPassword.Text)).IsValid)
            {
                Program.ActiveUser = user;
                lblInvalidCredentials.Visible = false;

                Hide();

                new MainForm().Show();
            }
            else
            {
                lblInvalidCredentials.Visible = true;
            }
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {

        }

        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }
    }
}