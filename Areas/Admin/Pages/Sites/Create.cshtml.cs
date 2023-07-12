using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using VijayLaxmi.Classes;
using VijayLaxmi.Models;

namespace VijayLaxmi.Areas.Admin.Pages.Sites
{
    public class CreateModel : PageModel
    {
        private readonly VijayLaxmi.Models.ApplicationDbContext _context;

        IWebHostEnvironment Environmet;
        [BindProperty]
        public IFormFile Agreement { get; set; }

        public string AgreementName { get; set; }
        public List<SelectListItem> GstState { get; set; }
        public CreateModel(ApplicationDbContext context, IWebHostEnvironment Env)
        {
            _context = context;
            Environmet = Env;
        }

        public async Task<IActionResult> OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminLogin")))
            {
                return Redirect("~/adminlogin");
            }
            else
            {
                GstState = await _context.TblGstState.Select(g => new SelectListItem { Text = g.StateName + "-" + g.GSTStateCode, Value = g.StateName + "-" + g.GSTStateCode.ToString() }).ToListAsync();

                return Page();
            }
        }


        [BindProperty]
        public Site Site { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {



            GetUserDate date = new GetUserDate();
            // string ViId = HttpContext.Session.GetString("Login");

            string jobworkJSON = Request.Form["jobworkdesc"];
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(jobworkJSON);




            var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@SiteName",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = Site.SiteName
                        },
                        new SqlParameter() {
                            ParameterName = "@Address",
                            SqlDbType =  System.Data.SqlDbType.NVarChar,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = Site.Address
                        },
                        new SqlParameter() {
                            ParameterName = "@Gstno",
                            SqlDbType =  System.Data.SqlDbType.NVarChar,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = Site.Gstno
                        },



                        new SqlParameter() {
                            ParameterName = "@AddedDate",
                            SqlDbType =  System.Data.SqlDbType.DateTime,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = date.ReturnDate()
                        },
                        new SqlParameter() {
                            ParameterName = "@StartDate",
                            SqlDbType =  System.Data.SqlDbType.DateTime,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = Site.Startdate
                        },
                        new SqlParameter() {
                            ParameterName = "@Enddate",
                            SqlDbType =  System.Data.SqlDbType.DateTime,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = Site.Enddate
                        },
                         new SqlParameter() {
                            ParameterName = "@StateCode",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = Site.StateCode
                        },
                        new SqlParameter() {
                            ParameterName = "@TblSitedesc",
                            SqlDbType =  System.Data.SqlDbType.Structured,
                            TypeName="dbo.SiteDescription",
                            Value=dt

                        } };
            await _context.Database.ExecuteSqlRawAsync("SPSiteDescription @SiteName,@Address,@Gstno,@AddedDate,@StartDate,@Enddate,@StateCode,@TblSitedesc", param);
            ViewData["Message"] = "created";
            return Page();
        }
    }
}


