#We're Hiring!
Do you like building cool stuff?  Do APIs keep you up at night? We're looking for our next superstar hacker and you could be it. Interested? Check out our job posting on [stackoverflow](http://careers.stackoverflow.com/jobs/52171/obsessive-compulsive-full-web-stack-engineer-maxcdn?a=11CW0Jx0A&searchTerm=maxcdn).

# MaxCDN REST Web Services NET Client

This is a Visual Studio 2013 project using .NET 4.5 

Make sure and obtain a proper account alias, consumer key, and consumer secret prior to using this library

# Solution
This is a solution with two projects - .NET library and Console Application that uses joined .NET library project. It's an open project which you can use to add functionality or change existing per your needs. 

# Usage

## Open project in Visual Studio
## Execute debugging process or release solution into installable pacjage by choosing BUILD -> Publish 
## Use following syntax to issue API calls

//****** Account ******//
            //Address
            //Console.Write(api.Get("/account.json/address"));

            //Edit
            //Console.Write("Enter property to edit (): \n");
            //string prop = Console.ReadLine();
            //Console.Write("Enter new value: \n");
            //string val = Console.ReadLine();

            //api.Put("/account.json/", prop + "=" + val);

            //***** Custom Domains ******//
            //Create
            //Console.Write("Zone Id: \n");
            //int zoneId = Convert.ToInt32(Console.ReadLine());
            //Console.Write("Custom Domain: \n");
            //string dat = Console.ReadLine();
            
            //api.Post("/zones/pull/" + zoneId + "/customdomains.json", dat="custom_domain=" + dat);
            
            //List
            //Console.Write("Zone Id: \n");
            //int zoneId = Convert.ToInt32(Console.ReadLine());

            //Console.Write(api.Get("/zones/pull/" + zoneId + "/customdomains.json"));
            
            //Edit
            //Console.Write("Zone ID: \n");
            //int zoneID = Convert.ToInt32(Console.ReadLine());
            //Console.Write("Custom Doamin Id to Edit: \n");
            //int cId = Convert.ToInt32(Console.ReadLine());
            //Console.Write("New Value for this custom domain: \n");
            //string cdname = Console.ReadLine();
            //api.Put("/zones/pull/" + zoneID + "/customdomains.json/" + cId, "custom_domain=" + cdname);

            //***** Zones *****//
            //New Zone
            //Console.Write("Zone Name: \n");
            //string ZoneName = Console.ReadLine();
            //Console.Write("Origin URL (starting with http://): \n");
            //string url = Console.ReadLine();

            //api.Post("/zones/pull.json", "url=" + url + "&name=" + ZoneName);

            //List
            //Console.Write(api.Get("/zones/pull.json"));

            //Edit
            //Console.Write("Zone id to edit: \n");
            //int zoneId = Convert.ToInt32(Console.ReadLine());
            //Console.Write("Property to edit/change (url/compression/...): \n");
            //string prop = Console.ReadLine();
            //Console.Write("New value: \n");
            //string val = Console.ReadLine();

            //api.Put("/zones/pull.json/" + zoneId, prop + "=" + val);

            //SSL
            //Install
            //var cert = "";
            //var key = "";
            //Console.Write("Zone id: \n");
            //int zoneId = Convert.ToInt32(Console.ReadLine());
            //using (StreamReader sr = new StreamReader("cert.txt"))
            //{
            //    cert = sr.ReadToEnd();               
            //}
            //using (StreamReader sr = new StreamReader("key.txt"))
            //{
            //    key = sr.ReadToEnd();
            //}


            //var dat = "";
            //cert = "-----BEGIN CERTIFICATE-----\n" + cert + "\n-----END CERTIFICATE-----\n";
            //key = "-----BEGIN RSA PRIVATE KEY-----\n" + key + "\n-----END RSA PRIVATE KEY-----\n";
            //api.Post("/zones/pull/" + zoneId + "/ssl.json", dat="ssl_crt=" + cert + "&ssl_key=" + key);
            
            //Edit
            //var dat = "";
            //cert = "-----BEGIN CERTIFICATE-----\n" + cert + "\n-----END CERTIFICATE-----\n";
            //key = "-----BEGIN RSA PRIVATE KEY-----\n" + key + "\n-----END RSA PRIVATE KEY-----\n";
            //api.Put("/zones/pull/" + zoneId + "/ssl.json", dat="ssl_crt=" + cert + "&ssl_key=" + key);

            //Summary
            //Console.Write(api.Get("/zones.json/summary"));

            //Count
            //Console.Write("Zone type to count (pull, push, vod): \n");
            //string zType = Console.ReadLine();
            //Console.Write(api.Get("/zones/" + zType + ".json/count"));
            
            //Create User
            //Console.Write("User First Name: \n");
            //string fname = Console.ReadLine();
            //Console.Write("User Last Name: \n");
            //string lname = Console.ReadLine();
            //Console.Write("User email: \n");
            //string email = Console.ReadLine();
            //Console.Write("Password: \n");
            //string pwd = Console.ReadLine();

            //api.Post("/users.json", "firstname=" + fname + "&lastname=" + lname + "&password=" + pwd + "&email=" + email);

            //List
            //Console.Write(api.Get("/users.json"));

            //Edit
            //Console.Write("Enter user ID to edit: \n");
            //int uid = Convert.ToInt32(Console.ReadLine());
            //Console.Write("Enter property to edit: \n");
            //string prop = Console.ReadLine();
            //Console.Write("New value: \n");
            //string val = Console.ReadLine();

            //api.Put("/users.json/" + uid + "/", prop + "=" + val);

            //***** Manage Cache *****//
            //Console.Write("Zone ID: \n");
            //int zoneId = Convert.ToInt32(Console.ReadLine());
            //Console.Write("What do you want to purge? (all/file)");
            //string ptype = Console.ReadLine();
            //switch (ptype){
            //    case "all":
            //    api.Delete("/zones/pull.json/" + zoneId + "/cache");
            //    break;
            //    case "file":
            //    Console.Write("Enter File Path to Purge (relative path): \n");
            //    string file = Console.ReadLine();
            //    
            //    api.Purge("/zones/pull.json/" + zoneId + "/cache", file);
            //    break;
            //    case "fileS":
            //    Console.Write("How Many? \n");
            //    int loop = Convert.ToInt32(Console.ReadLine());
            //    Console.Write("Enter File Paths to Purge (relative paths): \n");
            //    string files = "";
            //    for (int i = 0; i < loop; i++)
            //    {
            //        Console.Write(i + 1 + ": \n");
            //        string File = Console.ReadLine();
            //        files += "file[" + i + "]=" + File + "&";
            //    }
            //    
            //    api.Purge("/zones/pull.json/" + zoneId + "/cache", files);
            //    break;
            // }
 
