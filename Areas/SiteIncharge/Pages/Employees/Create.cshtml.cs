using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VijayLaxmi.Classes;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.SiteIncharge.Pages.Employees
{
    public class CreateModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;

        IWebHostEnvironment Environmet;
        [BindProperty]
        public IFormFile Userimg { get; set; } 
        public string UserimgName { get; set; }
        public string UserDocumentName { get; set; }
        public string UserPanName { get; set; }
        public CreateModel(ApplicationDbContext context, IWebHostEnvironment Env)
        {
            _context = context;
            Environmet = Env;
        }

        public string Sitename { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var SiteId = Convert.ToInt32(HttpContext.Session.GetString("siteid"));

            Designation = await _context.TblDesignation.Select(d => new SelectListItem { Text = d.DesignationName, Value = d.Id.ToString() }).ToListAsync();
            Site = await _context.TblSite.Where(e=>e.Siteid==SiteId).Select(d => new SelectListItem { Text = d.SiteName, Value = d.Siteid.ToString() }).ToListAsync();
            Sitename = Site.FirstOrDefault().Value;
            return Page();
        }

        [BindProperty]
        public Employee Employee { get; set; }


        public List<SelectListItem> Designation { get; set; }
        public List<SelectListItem> Site { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string olduserimg, string olduserdoc, string olduserpan)
        {
            Int64 empid = 0;
            try
            {
                Upload u = new Upload(Environmet);
                var userdetail = await _context.TblEmployees.SingleOrDefaultAsync(e => e.AAdharno == Employee.AAdharno);

                if (userdetail == null)
                {
                    GetUserDate date = new GetUserDate();
                    UserimgName = u.UploadImage(Userimg, "UserImg");
                    Employee.IsActive= true;                   
                    Employee.AddedDate = date.ReturnDate();
                    Employee.Image = UserimgName.ToString().Replace(" ", "_");
                    _context.TblEmployees.Add(Employee);
                }

                else
                {
                    userdetail.Name = Employee.Name;
                    userdetail.Email = Employee.Email;
                    userdetail.Contactno = Employee.Contactno;
                    userdetail.Designation = Employee.Designation;
                    userdetail.Employeetype = Employee.Employeetype;
                    userdetail.Addresslocal = Employee.Addresslocal;
                    userdetail.AddressPermanent = Employee.AddressPermanent;
                    userdetail.State = Employee.State;
                    userdetail.City = Employee.City;
                    userdetail.Pancard = Employee.Pancard;
                    userdetail.Site = Employee.Site;
                    userdetail.EmployeeRole = Employee.EmployeeRole;
                    userdetail.FathersName = Employee.FathersName;
                    userdetail.Salary = Employee.Salary;
                    userdetail.EsiNo = Employee.EsiNo;
                    userdetail.UANNo = Employee.UANNo;
                    empid = Employee.Id;
                    if (Userimg == null)
                    {
                        userdetail.Image = olduserimg;
                    }
                    else
                    {
                        UserimgName = u.UploadImage(Userimg, "UserImg");
                        userdetail.Image = UserimgName.ToString().Replace(" ", "_");

                    }
                   
                }
                await _context.SaveChangesAsync();
                var empdetail = await _context.TblEmployees.SingleOrDefaultAsync(e => e.AAdharno == Employee.AAdharno);
                empid = empdetail.Id;
                var isfamilyexist = await _context.TblFamilyDetail.Where(e => e.EmployeeAAdhar == empid).ToListAsync();
                if (isfamilyexist.Any())
                {
                    return Redirect("Details?id=" + empid);
                }
                else
                {
                    return Redirect("EmployeeDocuments?Empid=" + empid + "&aadharno=" + Employee.AAdharno);
                }
            }
            catch (Exception)
            {
                return Page();
            }

        }
    }
}
