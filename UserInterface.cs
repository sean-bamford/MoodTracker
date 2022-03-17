using System;
using System.Collections.Generic;
using System.Text;

namespace MoodTracker
{
    public class UserInterface
    {
        public UserInterface(EntryDAO entryDAO, FactorDAO factorDAO)
        {
            this.entryDAO = entryDAO;
            this.factorDAO = factorDAO;
        }

        private readonly EntryDAO entryDAO;
        private readonly FactorDAO factorDAO;
        //todo allow viewing of History.
        //todo implement custom Factors.

        public void Run()
        {
            Console.WriteLine("  --- MoodTracker ---");
            bool done = false;
            while (!done)
            {
                string input;
                Console.WriteLine(String.Format("{0,5} {1,-10}", "1", "New Entry"));
                Console.WriteLine(String.Format("{0,5} {1,-10}", "2", "View History"));
                Console.WriteLine(String.Format("{0,5} {1,-10}", "3", "Exit"));
                Console.WriteLine();
                input = (Console.ReadLine());
                Console.WriteLine();
                switch (input)
                {
                    case "1":
                        NewEntry();
                        break;
                    case "2":
                    case "3": done = true; break;
                    default: Console.WriteLine("Please enter a valid selection."); break;
                }
            }
        }
        public bool YesNoInput()
        {
            bool correctEntry = false;
            bool yesNo = false;

            while (!correctEntry)
            {
                string input = Console.ReadLine().ToUpper();
                if (input == "Y" || input == "YES")
                {
                    correctEntry = true;
                    yesNo = true;
                    break;
                }
                if (input == "N" || input == "NO")
                {
                    correctEntry = true;
                    yesNo = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter either (Y)es or (N)o.");
                    continue;
                }

            }
            Console.WriteLine();
            return yesNo;
        }
        public int IntInput()
        {
            bool correctEntry = false;
            int output = 0;

            while (!correctEntry)
            {
                correctEntry = Int32.TryParse(Console.ReadLine(), out output);
                if (!correctEntry) { Console.WriteLine("Please enter a number."); }
            }
            Console.WriteLine();
            return output;
        }
        public int OneFiveInput()
        {
            bool correctEntry = false;
            int output = 0;
            while (!correctEntry)
            {
                int input = IntInput();
                if (input < 1 || input > 5)
                {
                    Console.WriteLine("Please enter either a value between 1 and 5.");
                    Console.WriteLine();
                    continue;
                }
                correctEntry = true;
                output = input;
            }
            return output;
        }
        public void DisplayLevels(int choice)
        {
            if (choice == 0 || choice == 1) { Console.WriteLine(String.Format("{0,5} {1,10} {2,10} {3,10} {4,10}", "1", "2", "3", "4", "5")); }
            if (choice == 0) { Console.WriteLine(String.Format("{0, 10} {1,7} {2, 11} {3,8} {4,12}", "Miserable", "Down", "Neutral", "Good", "Ecstatic")); } //mood-specific descriptions
            if (choice == 1) { Console.WriteLine(String.Format("{0, 7} {1,10} {2, 9} {3,10} {4,11}", "Awful", "Meh", "Okay", "Good", "Amazing")); } //general desriptions
            if (choice == 2) //Binary choice
            {
                Console.WriteLine(String.Format("{0, 7} {1,10}", "1", "2"));
                Console.WriteLine(String.Format("{0, 7} {1,10}", "Yes", "No"));
            }
            Console.WriteLine();
        }

        public void DisplayFactors(List<Factor> factors)
        {
            foreach (Factor factor in factors)
            {
                if (factor.Value == 0) { Console.WriteLine(String.Format("{0,5} {1,-10}", $"{factor.Id}", $"{factor.Name}")); }
            }
            Console.WriteLine();
        }

        public void NewEntry()
        {
            Entry entry = new Entry();
            Console.WriteLine("How are you feeling right now?");
            DisplayLevels(0);
            entry.Level = OneFiveInput();
            Console.WriteLine("Are there any contributing factors you want to include? Y/N");
            if (YesNoInput())
            {
                entry.Factors = entry.PopulateFactors(factorDAO);
                FactorEntry(entry);
            }
            Console.WriteLine("Would you like to add a note?");
            if (YesNoInput())
            {
                Console.WriteLine("Enter your note:");
                entry.Note = Console.ReadLine();
            }
            else { entry.Note = ""; }
            entryDAO.NewEntry(entry);
            Console.WriteLine("Entry complete!\n");
        }

        public void FactorEntry(Entry entry)    
        {
            bool closeFactors = false;

            DisplayFactors(entry.Factors);
            while (!closeFactors)
            {
                Factor choice = entry.GetFactorById(IntInput());
                if (choice.Name == null || choice.Value != 0) { Console.WriteLine("Please choose a valid selection."); continue; }
                //if (entry.Factors[])
                Console.WriteLine(choice.QuestionText);
                DisplayLevels(choice.ValueType);
                int value = OneFiveInput();
                entry.FactorEntry(choice, value);
                if (!entry.NoMoreFactors()) { closeFactors = true; break; }
                Console.WriteLine("Track another factor? Y/N");
                if (YesNoInput()) { DisplayFactors(entry.Factors); continue; }
                closeFactors = true; break;

            }
        }

    }

}
