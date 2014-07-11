using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxCDN_dll
{
    class Zones
    {
        public void ManageZones(int requestTimeout)
        {
            
            var api = new MaxCDN.Api("ALIAS", "KEY", "SECRET", requestTimeout);

            Console.Write("1. List\n2. Create\n3. Edit\n4. Delete\n\n");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                Console.Write("Zone Type? (pull/push/vod)\n");
                string list = Console.ReadLine();
                Console.Write(api.Get("/zones/" + list + ".json"));
                break;
                case 2:
                Console.Write("Zone Type? (pull/push/vod)\n");
                string create = Console.ReadLine();
                Console.Write("Zone Name: \n");
                string ZoneName = Console.ReadLine();
                string param = "";
                if (create == "pull")
                {
                    Console.Write("Origin URL (starting with http://): \n");
                    string url = Console.ReadLine();
                    param = "url=" + url + "&name=" + ZoneName;
                }
                if(create != "pull")
                {
                    Console.Write("Password: \n");
                    string password = Console.ReadLine();
                    param = "password=" + password + "&name=" + ZoneName;
                }
                
                api.Post("/zones/" + create + ".json", param);
                break;
                case 3:
                Console.Write("Zone Type: (pull/push/vod)");
                string edit = Console.ReadLine();
                if (edit == "pull") 
                {
                    Console.Write("Zone ID: \n");
                    int zoneID = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Choose property: compress, url, use_stale,... full list: https://docs.maxcdn.com\n");
                    string prop = Console.ReadLine();
                    Console.Write("New Value: \n");
                    string val = Console.ReadLine();
                    api.Put("/zones/" + edit + ".json/" + zoneID, prop + "=" + val);
                }
                break;
                case 4:
                Console.Write("Zone Type: (pull/push/vod)");
                string delete = Console.ReadLine();
                Console.Write("Zone ID: \n");
                    int zID = Convert.ToInt32(Console.ReadLine());
                api.Delete("/zones/" + delete + ".json/" + zID);
                break;
            }
            
        }

        internal void ListZones()
        {
            throw new NotImplementedException();
        }
    }
}
