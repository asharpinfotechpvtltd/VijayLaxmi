using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VijayLaxmi.Models;
using VijayLaxmi.StoredProcedure;

namespace VijayLaxmi.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public void DetachAllEntities()
        {
            var changedEntriesCopy = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SPPendingEmployeeList>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPEmployeeDetailById>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPLoanadvanceList>().HasNoKey().ToView(null);
            modelBuilder.Entity<SpBankTransaction>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPAttendanceList>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPEmployeeList>().HasNoKey().ToView(null);
            modelBuilder.Entity<SpSearchEmployeeList>().HasNoKey().ToView(null);
            modelBuilder.Entity<SpSiteWiseEmployeeList>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPSiteInCharge>().HasNoKey().ToView(null);
        }


        #region Table
        public virtual DbSet<Designation> TblDesignation { get; set; }
        public virtual DbSet<Employee> TblEmployees { get; set; }
        public virtual DbSet<Bill> TblBill { get; set; }
        public virtual DbSet<Ledger> TblLedger { get; set; }
        public virtual DbSet<Loan_Advance> TblLoanAdvance { get; set; }
        public virtual DbSet<Site> TblSite { get; set; }
        public virtual DbSet<SiteDescription> TblSiteDescription { get; set; }
        public virtual DbSet<Loan_AdvanceAdjustment> TblLoanAdvanceAdjustment { get; set; }
        public virtual DbSet<Device> TblDevice { get; set; }
        public virtual DbSet<BankDetail> TblBankDetail { get; set; }        
        public virtual DbSet<FamilyDetail> TblFamilyDetail { get; set; }        
        public virtual DbSet<Requirement> TblRequirement { get; set; }        
        public virtual DbSet<EmpAttendence> TblAttendence { get; set; }        
        public virtual DbSet<Admin> TblAdmin { get; set; }        
        public virtual DbSet<TblOrderid> TblOrderid { get; set; }        
        public virtual DbSet<BillDescription> TblBillDescription { get; set; }        
        public virtual DbSet<CashHeads> TblCashHead { get; set; }        
        public virtual DbSet<BankHeads> TblBankHead { get; set; }        
        public virtual DbSet<CashSubHead> TblCashSubHead { get; set; }        
        public virtual DbSet<Document> TblDocuments { get; set; }        
        public virtual DbSet<EnrollmentLog> TblEnrollmentLog { get; set; }        
        public virtual DbSet<SiteWiseContract> TblSiteWiseContract { get; set; }        
        public virtual DbSet<StateDetail> TblStateDetail { get; set; }        
        public virtual DbSet<BankAccounts> TblBankAccounts { get; set; }        
        public virtual DbSet<BankLog> TblBanking { get; set; }        
        public virtual DbSet<Cashlog> TblCashlog { get; set; }        
        public virtual DbSet<SitesIncharge> TblSiteIncharge { get; set; }        
        public virtual DbSet<GstState> TblGstState { get; set; }        
        public virtual DbSet<Salary> TblSalary { get; set; }        
        public virtual DbSet<SPPendingEmployeeList> SPPendingEmployeeList { get; set; }
        public virtual DbSet<SPEmployeeDetailById> SPEmployeeDetailById { get; set; }
        public virtual DbSet<SPRequirement> SPRequirement { get; set; }
        public virtual DbSet<SPLoanadvanceList> SPLoanadvanceList { get; set; }
        public virtual DbSet<SpBankTransaction> SpBankTransaction { get; set; }
        public virtual DbSet<SPAttendanceList> SPAttendanceList { get; set; }
        public virtual DbSet<SPEmployeeList> SPEmployeeList { get; set; }
        public virtual DbSet<SpSearchEmployeeList> SpSearchEmployeeList { get; set; }
        public virtual DbSet<SpSiteWiseEmployeeList> SpSiteWiseEmployeeList { get; set; }
        public virtual DbSet<SPSiteInCharge> SPSiteInCharge { get; set; }
       
       

        #endregion



        

    }

}
