
using SettlementGame.Domain;


namespace WinFormsAppUI
{
    public partial class AuthenticationClient : Form
    {
        private readonly AuthService _authService;
        // The authentication token can be read from outside,
        // but it can only be assigned inside this class
        public string Token { get; private set; }


        public AuthenticationClient(AuthService authService)
        {
            InitializeComponent();

            // Store the authentication service.
            _authService = authService;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Login button clicked");

            string email = txtLogin.Text;
            string password = txtPassword.Text;

            // The UI delegates the HTTP request to AuthService.
            // This keeps HTTP-related logic outside the UI layer.
            string token = await _authService.LoginTask(email, password);

            if (token == null)
            {
                MessageBox.Show(
                    "Please enter your Login and password again.",
                    "Authentication error",
                    MessageBoxButtons.OK);

                Activate();
                txtLogin.Focus();

                return;
            }

            // Store the JWT received from the server.
            Token = token;

            // Notify Program.cs that authentication was successful.
            DialogResult = DialogResult.OK;

            Close();
        }
    }

    }


