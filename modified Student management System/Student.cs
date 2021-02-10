using System;
using System.Collections.Generic;
using System.Text;

namespace modified_Student_management_System
{
   public class Student
    {
        List<ProgressCard> ProgressCards = new List<ProgressCard>();
        public string StudentName { get; set; }

        public int StudentRollNumber { get; set; }

        public string StudentRollNumberString { get; set; }

        public List<ProgressCard> ProgressCard
        {
            get 
            {
                return ProgressCards;
            }
            set
            {
                ProgressCard = value;
            }
        }
    }
}
