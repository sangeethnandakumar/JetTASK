using JetTask.Entities;
using JetTask.Entities.Misc;
using Microsoft.EntityFrameworkCore;

namespace JetTask.Data
{
    public class JetTaskContext : DbContext
    {
        private readonly AppConfig config;

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<AdditionalCost> AdditionalCosts { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Release> Releases { get; set; }
        public DbSet<TicketStatus> TicketStatuses { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<AppVersion> AppVersions { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<HourleyRate> HourleyRates { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        public JetTaskContext()
        { }

        public JetTaskContext(AppConfig config)
        {
            this.config = config;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //User
            modelBuilder.Entity<User>().HasOne(a => a.CreatedBy).WithMany().OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<User>().HasOne(a => a.UpdatedBy).WithMany().OnDelete(DeleteBehavior.SetNull);
            //Project
            modelBuilder.Entity<Project>().HasOne(a => a.CreatedBy).WithMany().OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Project>().HasOne(a => a.UpdatedBy).WithMany().OnDelete(DeleteBehavior.SetNull);
            //Client
            modelBuilder.Entity<Client>().HasOne(a => a.CreatedBy).WithMany().OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Client>().HasOne(a => a.UpdatedBy).WithMany().OnDelete(DeleteBehavior.SetNull);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=DESKTOP-QJ02OLT\SQLEXPRESS;Database=JetTask;Trusted_Connection=True;");
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder builder)
        //{
        //    if (config.Database.IsTrustedConnection)
        //    {
        //        builder.UseSqlServer($"Server={config.Database.Server};Database={config.Database.DatabaseName};Trusted_Connection=True;");
        //    }
        //}
    }
}