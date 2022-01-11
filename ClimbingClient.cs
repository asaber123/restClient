
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace RestCSharp
{

    public class ClimbingClient
    {

        HttpClient client;

        public ClimbingClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:3001/");
            client.DefaultRequestHeaders.Accept.Clear();
            //Tells that we want to return in json format. 
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<ClimbingRoute>> getRoutes()
        {
            if (client.DefaultRequestHeaders.Authorization == null){
                Console.WriteLine("User is not logged in");
                return new List<ClimbingRoute>();
            }
            HttpResponseMessage routeResponse = await client.GetAsync("api/");
            var routeResponseJson = routeResponse.Content.ReadAsStringAsync();
            routeResponseJson.Wait();
            return JsonSerializer.Deserialize<List<ClimbingRoute>>((string)routeResponseJson.Result);
        }

        public async void Login(String username, String password)
        {
            var loginRequest = new LoginRequest();
            loginRequest.userName = username;
            loginRequest.password = password;
            HttpResponseMessage loginResponse = await client.PostAsJsonAsync("auth/login", loginRequest);
            var authToken = "";
            if (loginResponse.IsSuccessStatusCode)
            {
                authToken = loginResponse.Headers.GetValues("auth-token").FirstOrDefault();
                //Set auth token to client so that we are authorized from now on.
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            }
            else
            {
                throw new Exception("Failed to login");
            }
        }
    }

}