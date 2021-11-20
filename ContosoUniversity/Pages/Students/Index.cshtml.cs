using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly SchoolContext _context;
        private readonly MvcOptions _mvcOptions;

        public IndexModel(SchoolContext context, IOptions<MvcOptions> mvcOptions)
        {
            _context = context;
            _mvcOptions = mvcOptions.Value;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IList<Student> Students { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            // using System;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            IQueryable<Student> studentsIQ = from s in _context.Students
                                             select s;

            switch (sortOrder)
            {
                case "name_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    studentsIQ = studentsIQ.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    studentsIQ = studentsIQ.OrderBy(s => s.LastName);
                    break;
            }
            // MaxModelBindingCollectionSize defaults to 1024
            studentsIQ = studentsIQ.Take(_mvcOptions.MaxModelBindingCollectionSize);

            Students = await studentsIQ.AsNoTracking().ToListAsync();
        }
    }


    //public class IndexModel : PageModel
    //{
    //    private readonly SchoolContext _context;
    //    private readonly MvcOptions _mvcOptions;

    //    public IndexModel(SchoolContext context, IOptions<MvcOptions> mvcOptions)
    //    {
    //        _context = context;
    //        _mvcOptions = mvcOptions.Value;
    //    }

    //    public IList<Student> Student { get;set; }

    //    public async Task OnGetAsync()
    //    {
    //        // MaxModelBindingCollectionSize defaults to 1024
    //        Student = await _context.Students.Take(
    //            _mvcOptions.MaxModelBindingCollectionSize).ToListAsync();
    //    }
    //}
}
