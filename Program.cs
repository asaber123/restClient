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
            Console.Write("Log in ");
            Console.CursorVisible = true;
            Console.Write("Username:");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            client.Login(username, password);
            Console.WriteLine("Login successful");
        }

        static void register(ClimbingClient client)
        {
            Console.Write("Register");
            Console.CursorVisible = true;
            Console.Write("Full name: ");
            string fullname = Console.ReadLine();
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            client.Register(fullname, username, password);
            Console.WriteLine("Register successful");
        }

        static async Task getRoutes(ClimbingClient client)
        {
            var routes = await client.getRoutes();
            if (routes.Count == 0)
            {
                Console.WriteLine("There is no loggs to get");
            }
            else
            {
                foreach (ClimbingRoute route in routes)
                {
                    Console.WriteLine($"Route name: {route.name}");
                }
            }

        }

        static void Main(string[] args)
        {
            var client = new ClimbingClient();
            while (true)
            {
                Console.WriteLine("MyLog\n\n");

                Console.WriteLine("1. Log in\n");
                Console.WriteLine("2. Register a new user\n");
                Console.WriteLine("3. Get climbing loggs\n");
                Console.WriteLine("4. Add a new climbing log\n");

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
                        getRoutes(client).Wait();
                        break;
                    case "4":
                        getRoutes(client).Wait();
                        break;
                    case "x":
                        Environment.Exit(0);
                        break;
                }

            }
        }
    }
}



