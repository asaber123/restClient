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

        static void login(ClimbingClient client)
        {
            Console.Write("Logga in: ");
            Console.CursorVisible = true;
            Console.Write("Användarnamn: ");
            string username = Console.ReadLine();
            Console.Write("Lösenord: ");
            string password = Console.ReadLine();
            client.Login(username, password);
            Console.WriteLine("Login successful");
        }

        static void register(ClimbingClient client)
        {
            Console.Write("Registrera dig");
            Console.CursorVisible = true;
            Console.Write("Ange ditt namn: ");
            string fullname = Console.ReadLine();
            Console.Write("Ange användarnamn: ");
            string username = Console.ReadLine();
            Console.Write("Ange lösenord: ");
            string password = Console.ReadLine();
        }

        static void getRoutes(ClimbingClient client)
        {
            var routesTask = client.getRoutes();
            routesTask.Wait();
            foreach (ClimbingRoute route in routesTask.Result)
            {
                Console.WriteLine($"Route name: {route.name}");
            }
        }

        static void Main(string[] args)
        {
            var client = new ClimbingClient();
            while (true)
            {
                Console.WriteLine("MyLog\n\n");

                Console.WriteLine("1. Logga in\n");
                Console.WriteLine("2. Registrera användare\n");
                Console.WriteLine("3. Hämta rutter\n");
                Console.WriteLine("X. Avsluta\n");

                string inp = Console.ReadLine().ToLower();

                switch (inp)
                {
                    case "1":
                        login(client);
                        break;
                    case "2":
                        register(client);
                        break;
                    case "3":
                        getRoutes(client);
                        break;
                    case "x":
                        Environment.Exit(0);
                        break;
                }

            }
        }
    }
}



