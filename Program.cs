using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;




namespace RestCSharp
{
    public class Program
    {
        static void Main(string[] args)
        {
            RunAsync().Wait();
        }
        //Creating an async funtion so that the call will not be blocked, but instad wait to return the next line of code when the next request happens. 
        static async Task RunAsync()
        {
            using(var client= new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:3001/");
                client.DefaultRequestHeaders.Accept.Clear();
                //Tells that we want to return in json format. 
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Get data
                Console.WriteLine("GET users");
                //Asyncronros call
                HttpResponseMessage response = await client.GetAsync("auth/user");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = response.Content.ReadAsStringAsync();
                    jsonResponse.Wait();
                    Console.WriteLine((string)jsonResponse.Result);
                    List<User> users = JsonSerializer.Deserialize<List<User>>((string)jsonResponse.Result);
                    foreach(User user in users){
                        Console.WriteLine(user.userName);
                    }
                }
                var loginRequest = new LoginRequest();
                loginRequest.userName = "asaberglund";
                loginRequest.password = "asaberglund";
                HttpResponseMessage loginResponse = await client.PostAsJsonAsync("auth/login", loginRequest);
                var authToken = "";
                if (loginResponse.IsSuccessStatusCode)
                {
                    authToken = loginResponse.Headers.GetValues("auth-token").FirstOrDefault();
                    Console.WriteLine($"Auth token: {authToken}");
                }
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                HttpResponseMessage routeResponse = await client.GetAsync("api/");
                Console.WriteLine(routeResponse.StatusCode);
                var routeResponseJson = routeResponse.Content.ReadAsStringAsync();
                routeResponseJson.Wait();
                Console.WriteLine(routeResponseJson.Result);
                List<ClimbinRoute> climbinRoutes = JsonSerializer.Deserialize<List<ClimbinRoute>>((string)routeResponseJson.Result);
                foreach(ClimbinRoute climbinRoute in climbinRoutes){
                    Console.WriteLine($"grade: {climbinRoute.grade}, name:: {climbinRoute.name}");
                }
            }
        }
    }
}
