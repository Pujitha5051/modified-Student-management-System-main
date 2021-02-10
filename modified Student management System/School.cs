using System;
using System.Collections.Generic;
using System.Text;

namespace modified_Student_management_System
{
    public class School
    {
        public string SchoolName { get; set; }
        List<Student> Students = new List<Student>();

        public List<Student> Student {
            get
            {
                return Students;
            }
            set
            {
                Student = value;
            }
        }
        
    }
}

        

    

