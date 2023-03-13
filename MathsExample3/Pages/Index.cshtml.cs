using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathsExample3.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Windows.Forms;
using Microsoft.AspNetCore.Http;

namespace MathsExample3.Pages
{
    public class IndexModel : PageModel
    {
        private AppDbContext _db;
        [BindProperty]
        public Student Stud { get; set; }
        public IList<Qs> ExamQuestions;
        //1public IList<Qs> StudentIDs;
        [BindProperty]
        public IList<Answer> StudAnswers { get; set; }
        public decimal total = 0;
        public decimal percentage = -1;
        public IndexModel(AppDbContext db)
        {
            _db = db;
            ExamQuestions = _db.Questions.FromSqlRaw("SELECT * FROM Questions").ToList();
            //StudentIDs = _db.Students.FromSqlRaw("SELECT * FROM Students.studentID").ToList();
        }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostCheckAsync()
        {
            //need to insert the id here so it reads the database with the studentID input
            var existingStudent = _db.Students.FromSqlRaw("SELECT * FROM Students WHERE StudentID = " + Stud.StudentID).SingleOrDefault();
            if (existingStudent == null)
            {
                foreach (Answer A in StudAnswers)
                {
                    Qs Q = await _db.Questions.FindAsync(A.ID);
                    if (Q.Answer == A.Ans)
                    {
                        total += 1;
                    }
                }
                percentage = (total / ExamQuestions.Count) * 100;
                Stud.Result = percentage;

                _db.Students.Add(Stud);
                await _db.SaveChangesAsync();
            }
            else
            {
                errorMessage = "You have already submitted";
                //MessageBox.Show("You have already submitted");
            }
            return Page();
        }

    }
}