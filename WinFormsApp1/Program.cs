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
        static async Task Main()
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

            // The Web server process may have started, but ASP.NET Core
            // may still be initializing.
            //
            // Therefore Process.Start() alone is not enough.
            // We repeatedly call the health endpoint until the server
            // actually starts accepting HTTP requests.
            bool serverReady = false;

            // Try for up to 30 seconds.
            const int maxAttempts = 30;

            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                try
                {
                    // Health endpoint does not require authentication.
                    HttpResponseMessage response =
                        await client.GetAsync("api/health");

                    if (response.IsSuccessStatusCode)
                    {
                        serverReady = true;
                        break;
                    }
                }
                catch (HttpRequestException)
                {
                    // The server has probably not started listening yet.
                    //
                    // This is expected during the first few attempts,
                    // so we simply wait and try again.
                }
                catch (TaskCanceledException)
                {
                    // The request timed out.
                    //
                    // The server may still be starting, so continue
                    // trying until the maximum number of attempts
                    // has been reached.
                }

                // Wait one second before the next health check.
                await Task.Delay(1000);
            }


            // If the Web API did not become available within the
            // allowed time, there is no point in showing the login form.
            if (!serverReady)
            {
                MessageBox.Show(
                    "The Web server could not be started or did not become ready within 30 seconds.",
                    "SettlementGame",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

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