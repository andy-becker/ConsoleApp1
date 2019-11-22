using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        static object lockObject = new object();
        static readonly List<string> displayed = new List<string>(); // could also be SynchronizedCollection<string>
        static readonly BindingFlags flags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public;

        static void Main(string[] args)
        {
            using (APIClient client = new APIClient())
            {
                var status = client.IssueGET(ConfigurationManager.AppSettings["APIURL"] + "films/?search=" + Uri.EscapeUriString(args[0]));

                if (status == System.Net.HttpStatusCode.OK)
                {
                    var response = client.RetreiveResponse();
                    Classes.FilmSearchResponse results = JsonConvert.DeserializeObject<Classes.FilmSearchResponse>(response);

                    if (results.Results.Count > 0)
                    {
                        var film = results.Results[0]; // only handling first match in this exercise
                        PropertyInfo info;

                        switch (args[1].ToLower())
                        {
                            case "characters":
                                info = typeof(Classes.Character).GetProperty(args[2], flags); // does Character have this property?

                                if (info != null)
                                {
                                    Parallel.ForEach(film.Characters, url => ProcessChildObject(client, url, info));
                                }
                                else
                                {
                                    Console.WriteLine($"Character does not have a property \"{args[2]}\"");
                                }
                                break;

                            case "planets":
                                info = typeof(Classes.Planet).GetProperty(args[2], flags); // does Planet have this property?

                                if (info != null)
                                {
                                    Parallel.ForEach(film.Planets, url => ProcessChildObject(client, url, info));
                                }
                                else
                                {
                                    Console.WriteLine($"Planet does not have a property \"{args[2]}\"");
                                }
                                break;

                            case "starships":
                                info = typeof(Classes.Starship).GetProperty(args[2], flags); // does Starship have this property?

                                if (info != null)
                                {
                                    Parallel.ForEach(film.Starships, url => ProcessChildObject(client, url, info));
                                }
                                else
                                {
                                    Console.WriteLine($"Starship does not have a property \"{args[2]}\"");
                                }
                                break;

                            default:
                                Console.WriteLine($"Cannot process \"{args[1]}\"");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No film titles found matching \"{args[0]}\"");
                    }
                }
                else
                {
                    Console.WriteLine("HTTP status: " + status.ToString());
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press Enter to exit");
            Console.ReadLine();
        }

        static void ProcessChildObject(APIClient client, string resourceUrl, PropertyInfo propertyInfo)
        {
            // threads always get their own exception handler
            try
            {
                // retrieve the specified resource, use propertyInfo to cast accordingly
                client.IssueGET(resourceUrl);
                object resource = JsonConvert.DeserializeObject(client.RetreiveResponse(), propertyInfo.ReflectedType);

                // get the value of the specified property, which should always be a string
                string value = propertyInfo.GetValue(resource).ToString();

                lock (lockObject) // lock this while checking for duplicates
                {
                    int index = displayed.IndexOf(value);

                    if (index < 1) // not already displayed
                    {
                        displayed.Add(value); // record as displayed

                        Console.WriteLine(value);
                    }
                }
            }
            catch (Exception ex)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                    System.Diagnostics.Debugger.Break();
                else
                    Console.WriteLine(ex.ToString());
            }
        }
    }
}
