using System;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MoodTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            #region
            IConfigurationBuilder builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
#endregion 
            string connectionString = configuration.GetConnectionString("MoodTracker");
            
            EntryDAO entryDAO = new EntryDAO(connectionString);
            FactorDAO factorDAO = new FactorDAO(connectionString);
            UserInterface userInterface = new UserInterface(entryDAO, factorDAO);
            userInterface.Run();
        }
    }
}
/*1. Allow user to save mood any time. 
 * Mood should be tracked 1-5. done
 * By description - done
 * With Emoticons 
 * With date and time to create a record -done
 * saved externally, possibly separately -done
 * A separate class to handle storing information should be able to be replaced with a class to communicate to a data base. -done
 * Extra tracking per day can be added optionally (sleep quality, diet quality) -done
 * Should be able to add new tracking elements with new title. This will have to be stored in a separate setup file. to do
 * User Interface Class: sets up the console. All console reads/writes happen here.-done
 * IO class: handles all reads and writes outside of the app. 
 * DAO class: handles database to and from. done
*/