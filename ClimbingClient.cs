
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace RestCSharp
{

    public class ClimbingClient
    {

        HttpClient httpClient;

        public ClimbingClient()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:3001/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            //Tells that we want to return in json format. 
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<ClimbingRoute>> getRoutes()
        {
            if (httpClient.DefaultRequestHeaders.Authorization == null)
            {
                Console.WriteLine("User is not logged in");
                return new List<ClimbingRoute>();
            }
            var routeResponse = await httpClient.GetAsync("api/");
            var routeResponseJson = routeResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ClimbingRoute>>(routeResponseJson.Result);
        }

        public async void Login(String username, String password)
        {
            var loginRequest = new LoginRequest();
            loginRequest.userName = username;
            loginRequest.password = password;
            HttpResponseMessage loginResponse = await httpClient.PostAsJsonAsync("auth/login", loginRequest);
            var authToken = "";
            if (loginResponse.IsSuccessStatusCode)
            {
                authToken = loginResponse.Headers.GetValues("auth-token").FirstOrDefault();
                //Set auth token to client so that we are authorized from now on.
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            }
            else
            {
                throw new Exception("Failed to login");
            }
        }
        public async void Register(String fullname, String username, String password)
        {
            var registerRequest = new RegisterRequest();
            registerRequest.fullName = fullname;
            registerRequest.userName = username;
            registerRequest.password = password;
            HttpResponseMessage registerResponse = await httpClient.PostAsJsonAsync("auth/signup", registerRequest);
        }
    }

}