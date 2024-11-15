using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StudentPortal.Web.Data;
using StudentPortal.Web.Models;
using StudentPortal.Web.Models.Entities;

namespace StudentPortal.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext DbContext;

        public StudentsController( ApplicationDbContext DbContext)
        {
            this.DbContext = DbContext;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudent addStudent)
        {

            if(!ModelState.IsValid)
            {
                return View(addStudent);
            }
            var studentEnity = new Student
            {
                Name = addStudent.Name,
                Email = addStudent.Email,
                PhoneNumber = addStudent.PhoneNumber,
                Subscribed = addStudent.Subscribed
               
            };

            await DbContext.Students.AddAsync(studentEnity);
            await DbContext.SaveChangesAsync();
            TempData["successMessage"] = "Student added successfully.";
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students=await DbContext.Students.ToListAsync();
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await DbContext.Students.FindAsync(id);
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit( Student studentModel)
        {
            var student = await DbContext.Students.FindAsync(studentModel.Id);

            if(student is not null)
            {
                student.Name = studentModel.Name;
                student.Email = studentModel.Email;
                student.PhoneNumber = studentModel.PhoneNumber;
                student.Subscribed = studentModel.Subscribed;

                await DbContext.SaveChangesAsync();
                TempData["successMessage"] = "Student edited successfully.";
            }
            return RedirectToAction ("List","Students");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Student studentModel)
        {

            var student = await DbContext.Students.FirstOrDefaultAsync(x=>x.Id == studentModel.Id);

            if(student is not null)
            {
                 DbContext.Students.Remove(student);

                await DbContext.SaveChangesAsync();
                TempData["successMessage"] = "Student delted successfully.";
            }
            return RedirectToAction("List","Students");
        }
    }
}
