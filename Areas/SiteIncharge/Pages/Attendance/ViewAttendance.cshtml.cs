using VijayLaxmi.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using VijayLaxmi.Classes;
using VijayLaxmi.StoredProcedure;

namespace VijayLaxmi.Areas.SiteIncharge.Pages.EmpAttendance
{
    public class ViewAttendanceModel : PageModel
    {

        ApplicationDbContext Context;
        public ViewAttendanceModel(ApplicationDbContext context)
        {
            Context = context;
        }
        public Root Root { get; set; }
        public List<string> Attendance { get; set; }
        public List<SelectListItem> Employee { get; set; }
        public List<SPAttendanceList> AttendanceList { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Employeeid { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime Startdate { get; set; } = DateTime.Now;

        [BindProperty(SupportsGet = true)]
        public DateTime EndDate { get; set; } = DateTime.Now;

        public int SiteId { get; set; }
        public async Task<IActionResult> OnGet()
        {
            SiteId = Convert.ToInt32(HttpContext.Session.GetString("siteid"));
            Employee = await Context.TblEmployees.Where(e=>e.Site==SiteId).Select(s => new SelectListItem
            {
                Text = s.Name + "-" + s.AAdharno,
                Value = s.Id.ToString()
            }).ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnGetSearch()
        {
            SiteId = Convert.ToInt32(HttpContext.Session.GetString("siteid"));
            string url;
            string start = Startdate.ToString("dd/MM/yyyy");
            string end = EndDate.ToString("dd/MM/yyyy");
            if (Employeeid > 0)
            {
                url = @"https://api.etimeoffice.com/api/DownloadInOutPunchData?Empcode=" + Employeeid + "&FromDate=" + start + "&ToDate=" + end + ""; // adjust the URL accordingly
            }
            else
            {
               url = @"https://api.etimeoffice.com/api/DownloadInOutPunchData?Empcode=ALL&FromDate=" + start + "&ToDate=" + end + ""; // adjust the URL accordingly
            }

            string[] StartDate = start.Split("/");
            int date = Convert.ToInt32(StartDate[0]);
            int StartMonth = Convert.ToInt32(StartDate[1]);
            int StartYear = Convert.ToInt32(StartDate[2]);

            string[] Enddates = end.Split("/");
            int Enddate = Convert.ToInt32(Enddates[0]);
            int EndMonth = Convert.ToInt32(Enddates[1]);
            int EndYear = Convert.ToInt32(Enddates[2]);


            var devicedetail = await Context.TblDevice.SingleOrDefaultAsync(e => e.SiteId == SiteId);
            if (devicedetail != null)
            {
                string auth = devicedetail.ApiUrl;
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    AuthenticationSchemes.Basic.ToString(),
                    Convert.ToBase64String(Encoding.ASCII.GetBytes(auth))
                    );
                var response = await httpClient.GetAsync($"{url}");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsStringAsync();
                    Root = JsonConvert.DeserializeObject<Root>(result);
                    List<InOutPunchDatum> PunchDatums = new List<InOutPunchDatum>();
                    List<string> str = new List<string>();
                    foreach (var item in Root.InOutPunchData)
                    {
                        str.Add(item.DateString.ToString() + "$" + item.Empcode + "$" + item.INTime + "$" + item.OUTTime + "$" + item.WorkTime + "$" + item.Status);
                    }
                    Attendance = new List<string>();
                    var unique_items = new HashSet<string>(str);
                    foreach (string s in unique_items)
                    {
                        Attendance.Add(s);

                        string[] empdetail = s.Split("$");
                        string dates = empdetail[0];
                        string empcode = empdetail[1];
                        string intime = empdetail[2];
                        string outtime = empdetail[3];
                        string worktime = empdetail[4];
                        string status = empdetail[5];
                        string[] datesplit = dates.Split('/');
                        int Currentdate = Convert.ToInt32(datesplit[0]);
                        int month = Convert.ToInt32(datesplit[1]);
                        int year = Convert.ToInt32(datesplit[2]);

                        GetUserDate userdate = new GetUserDate();

                        var attendanceexist = await Context.TblAttendence.FirstOrDefaultAsync(e => e.Year == year && e.Date == Currentdate && e.Month == month && e.EmpCode == empcode);
                        if (attendanceexist == null)
                        {
                            EmpAttendence a = new EmpAttendence
                            {
                                Attendence = status,
                                Date = date,
                                Year = year,
                                EmpCode = empcode,
                                Month = month,
                                InTime = intime,
                                Outtime = outtime,
                                WorkTime = worktime,
                                SiteId = SiteId,
                                FetchedDate = userdate.ReturnDate()
                            };
                            await Context.TblAttendence.AddAsync(a);
                            await Context.SaveChangesAsync();
                        }
                    }
                }
            }
            



            var sid = new SqlParameter("@SiteId", SiteId);
            var stDate = new SqlParameter("@StartDate", date);
            var endDate = new SqlParameter("@Enddate", Enddate);
            var stMonth = new SqlParameter("@StartMonth", StartMonth);
            var endMonth = new SqlParameter("@EndMonth", EndMonth);
            var stYear = new SqlParameter("@StartYear", StartYear);
            var endYear = new SqlParameter("@EndYear", EndYear);

            AttendanceList = await Context.SPAttendanceList.FromSqlRaw("SPAttendanceList @SiteId,@StartDate,@EndDate,@StartMonth,@EndMonth,@StartYear,@EndYear", sid, stDate, endDate, stMonth, endMonth, stYear, endYear).ToListAsync();
            return Page();
        }
    }
}



       

