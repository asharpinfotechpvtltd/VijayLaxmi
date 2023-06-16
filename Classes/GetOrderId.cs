using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;


namespace VijayLaxmi.Models
{
    public class GetOrderId
    {
        private readonly ApplicationDbContext Context;


        public GetOrderId(ApplicationDbContext _context)
        {
            Context = _context;
        }

        public async Task<string> GetOrder(string serviceperiod, double TotalAmount, string site, string vendorcode=null)
        {

            DateTime timeUtc = System.DateTime.UtcNow;
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
            string date = cstTime.ToString("dd-MM-yyyy");

            var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@ServicePeriod",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 100,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = serviceperiod
                        },
                        new SqlParameter() {
                            ParameterName = "@Amount",
                            SqlDbType =  System.Data.SqlDbType.Float,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = TotalAmount
                        },
                        new SqlParameter() {
                            ParameterName = "@siteid",
                            SqlDbType =  System.Data.SqlDbType.NVarChar,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = site
                        },
                        new SqlParameter() {
                            ParameterName = "@VendorCode",
                            SqlDbType =  System.Data.SqlDbType.NVarChar,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = vendorcode
                        },
                        new SqlParameter() {
                            ParameterName = "@Date",
                            SqlDbType =  System.Data.SqlDbType.Date,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = date.ToString()
                        }                       
                        ,
                        new SqlParameter() {
                            ParameterName = "@Orderid",
                            SqlDbType =  System.Data.SqlDbType.NVarChar,
                            Size=100,
                            Direction = System.Data.ParameterDirection.Output
                        }};
            await Context.Database.ExecuteSqlRawAsync("SpOrderId @ServicePeriod,@Amount,@siteid,@VendorCode, @Date,@Orderid out", param);
            string Orderid = Convert.ToString(param[5].Value);

            return Orderid;

        }

    }
}

