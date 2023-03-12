using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MathsExample3.Data
{
    public class Student
    {
        public int StudentID { get; set; }
        public decimal Result { get; set; }
    }

    /*
         CREATE TABLE Students
            StudentID int PRIMARY KEY,
            Result decimal
    */
}
