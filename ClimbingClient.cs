
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

        public async Task<List<ClimbingRoute>> getClimbingLoggs()
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
        public async void addClimbinglog(String grade, String name, String location, String typeOfRoute)
        {
            if (httpClient.DefaultRequestHeaders.Authorization == null)
            {
                Console.WriteLine("User is not logged in");
            }
            var climbingLogRequest = new ClimbingRoute();
            climbingLogRequest.grade = grade;
            climbingLogRequest.name = name;
            climbingLogRequest.location = location;
            climbingLogRequest.typeOfRoute = typeOfRoute;
            HttpResponseMessage climbingLogResponse = await httpClient.PostAsJsonAsync("api/", climbingLogRequest);
            if (!climbingLogResponse.IsSuccessStatusCode)
            {
                throw new Exception("Failed to add logg");

            }
        }
        public async void deleteClimbingLog(string id)
        {
            if (httpClient.DefaultRequestHeaders.Authorization == null)
            {
                Console.WriteLine("User is not logged in");
            }
            var climbingLogRequest = new ClimbingRoute();
            HttpResponseMessage climbingLogResponse = await httpClient.DeleteAsync($"api/{id}");
            if (!climbingLogResponse.IsSuccessStatusCode)
            {
                throw new Exception("Failed to delete logg");

            }


        }
        public async Task Login(String username, String password)
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
        public async void logoutUser()
        {
            httpClient.DefaultRequestHeaders.Authorization = null;
        }
        public async void Register(String fullname, String username, String password)
        {
            var registerRequest = new RegisterRequest();
            registerRequest.fullName = fullname;
            registerRequest.userName = username;
            registerRequest.password = password;
            HttpResponseMessage registerResponse = await httpClient.PostAsJsonAsync("auth/signup", registerRequest);
        }
        public Boolean CheckIfUserIsLoggedIn()
        {
            if (httpClient.DefaultRequestHeaders.Authorization != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }

}