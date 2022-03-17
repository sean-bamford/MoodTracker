using System;
using System.Collections.Generic;
using System.Text;

namespace MoodTracker
{
    public class Entry
    {
        public int Id { get; set; }
       
        public int Level { get; set; }

        public DateTime DateTime { get; set; }

        public List<Factor> Factors { get; set; }

        public string Note { get; set; }       
        public List<Factor> PopulateFactors(FactorDAO factorDAO)
        {
            List<Factor> factors = new List<Factor>();
            factors = factorDAO.GetFactors();
            return factors;
        }
        public Factor GetFactorById(int Id)
        {
            Factor factor = new Factor();
            foreach (Factor item in Factors)
            {
                if (item.Id == Id)
                {
                    factor = item;
                    break;
                }
            }
            return factor;
        }

        public bool NoMoreFactors() // returns true if there are still factors left without assigned values, false if they're all used. 
        {
            bool stillMore = false;         
            foreach (Factor factor in this.Factors)
            {
                if (factor.Value == 0)
                {
                    stillMore = true;
                    break;
                } 
            }
            return stillMore;
        }

        public void FactorEntry(Factor factor, int value)
        {
            factor.Value = value;
        }

    }
} // :'(  :/ :| :) :D
//Each entry has an ID, a date/time, a Mood Level, and a list of factors (can be none), and an optional note.
//Each factor has an ID, name, value for each factor, tracking type (1-5, Y/N).

