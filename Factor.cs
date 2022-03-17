using System;
using System.Collections.Generic;
using System.Text;

namespace MoodTracker
{
    public class Factor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
      
        public bool IsDefault { get; set; }

        public string QuestionText { get; set; }
        public int ValueType { get; set; }
        
    }
    
}
//Each entry has an ID, a date/time, a Mood Level, and a list of factors (can be none), and an optional note.
//Each factor has an ID, name, value for each factor 1-5, descripti question text.
//
//existing factor: look through list of factors (some marked default) in SQL. Print them in a nice list with ID numbers and names.
//an entry can have muliple factors.
//Need an Entry table with Entry ID, MoodLevel, DateTime, Note 
//Need a Factor table with Id, Name, IsDefault, and Question text
//Need a FactorEntry table with the Factor ID, Entry ID, Value, and Note.
//