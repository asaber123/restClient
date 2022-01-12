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


        static async Task login(ClimbingClient client)
        {
            Console.Write("Log in \n\n");
            Console.CursorVisible = true;
            Console.Write("Username:");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            await client.Login(username, password);
            Console.WriteLine("Login successful");
        }

        static void register(ClimbingClient client)
        {
            Console.Write("Register\n\n");
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
        static Boolean isLoggedIn(ClimbingClient client)
        {
            return client.CheckIfUserIsLoggedIn();
        }
        static void logout(ClimbingClient client)
        {
            //return client.logoutUser();
            client.logoutUser();
        }

        static async Task getLoggs(ClimbingClient client)
        {
            var routes = await client.getClimbingLoggs();
            if (routes.Count == 0)
            {
                Console.WriteLine("There is no loggs to get");
            }
            else
            {
                foreach (ClimbingRoute route in routes)
                {
                    Console.WriteLine($"Route name: {route.name}\n");
                    Console.WriteLine($"Route grade: {route.grade}\n");
                    Console.WriteLine($"Route location: {route.location}\n");
                    Console.WriteLine($"Type of route: {route.typeOfRoute}\n");
                    Console.WriteLine($"Id: {route._id}\n");


                }
            }

        }
        static void addlog(ClimbingClient client)
        {
            Console.Write("Add a new logg\n\n");
            Console.CursorVisible = true;
            Console.Write("Name of route: ");
            string name = Console.ReadLine();
            Console.Write("Grade:");
            string grade = Console.ReadLine();
            Console.Write("Indoor or outdoor?: ");
            string location = Console.ReadLine();
            Console.Write("Sport, trad or bouldering?: ");
            string typeOfRoute = Console.ReadLine();

            client.addClimbinglog(grade, name, location, typeOfRoute);
            Console.WriteLine("Logg is added!");
        }
        static void deleteLog(ClimbingClient client)
        {
            Console.Write("Delete log\n\n");
            Console.CursorVisible = true;
            Console.Write("Choose id of route to delete: ");
            string id = Console.ReadLine();

            client.deleteClimbingLog(id);
            Console.WriteLine("Logg is deleted!");
        }


        static void Main(string[] args)
        {
            var client = new ClimbingClient();
            while (true)
            {
                if (isLoggedIn(client) == false)
                {
                    Console.WriteLine("MyLog\n\n");
                    Console.WriteLine("1. Log in\n");
                    Console.WriteLine("2. Registrera\n");
                    Console.WriteLine("X. Avsluta\n");
                    string inp = Console.ReadLine().ToLower();

                    switch (inp)
                    {
                        case "1":
                            login(client).Wait();
                            break;
                        case "2":
                            register(client);
                            break;
                        case "x":
                            Environment.Exit(0);
                            break;
                    }
                }
                else
                {

                    Console.WriteLine("MyLog\n\n");
                    Console.WriteLine("1. Log out\n");
                    Console.WriteLine("2. Get climbing loggs\n");
                    Console.WriteLine("3. Add a new climbing log\n");
                    Console.WriteLine("4. Delete climbing log\n");
                    Console.WriteLine("X. Avsluta\n");

                    string inp = Console.ReadLine().ToLower();

                    switch (inp)
                    {
                        case "1":
                            logout(client);
                            break;
                        case "2":
                            getLoggs(client).Wait();
                            break;
                        case "3":
                            addlog(client);
                            break;
                        case "4":
                            deleteLog(client);
                            break;
                        case "x":
                            Environment.Exit(0);
                            break;
                    }
                }
            }
        }
    }
}




