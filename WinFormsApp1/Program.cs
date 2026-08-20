using SettlementGame.Domain;
using WinFormsAppUI;

namespace WinFormsApp1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Initialize the Windows Forms application.
            ApplicationConfiguration.Initialize();

            // Create one HttpClient for the entire application.
            // The same client is used for authentication and all subsequent API requests.
            HttpClient client = new HttpClient();

            // Allow long-running requests, such as simulation Ticks.
            client.Timeout = TimeSpan.FromMinutes(15);

            // Set the base address of the Web API.
            client.BaseAddress = new Uri("http://localhost:5126/");

            // Create the authentication service.
            // AuthService is responsible for sending Login requests to the Web API.
            AuthService authService = new AuthService(client);

            // Create the Login form.
            AuthenticationClient authForm = new AuthenticationClient(authService);

            // Show the Login form modally.
            // The main application does not start until the user successfully logs in.
            if (authForm.ShowDialog() == DialogResult.OK)
            {
                // Get the JWT returned by the authentication form.
                string token = authForm.Token;

                // Add the JWT to the Authorization header of the shared HttpClient.
                // All subsequent API requests will therefore contain:
                // Authorization: Bearer <token>
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue(
                        "Bearer",
                        token);

                // Start the main application form.
                Application.Run(new MainForm(client));
            }
        }
    }
}