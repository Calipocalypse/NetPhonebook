using Microsoft.EntityFrameworkCore;
using NetPhonebook.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SqLiteDataProvider
{
    public class NetbookContext : DbContext
    {
        private string sqliteFilePath;

        /* Models */
        /* public DbSet<Setting> settings { get; set; }
        public DbSet<UserSetting> userSettings { get; set; }
        public DbSet<Image> images { get; set; } */ 
        public DbSet<ExtraCategory> extraCategories { get; set; }
        //public DbSet<ExtraInfo> extraInfos { get; set; }
        /* public DbSet<VirtualModel> virtualModels { get; set; }
        public DbSet<VirtualModelsCustomization> virtualModelsCustomizations { get; set; }
        public DbSet<VirtualModelsData> virtualModelsDatas { get; set; }
        public DbSet<VirtualModelsCellData> virtualModelsCellDatas { get; set; } */

        /* Code */

        public NetbookContext()
        {
            sqliteFilePath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\database.sqlite3";
        }

        public NetbookContext(DbContextOptions options) : base(options)
        {
            sqliteFilePath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\database.sqlite3";
            //sqliteFilePath = path;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlite("Filename=" + sqliteFilePath, options =>
             {
                 options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
             });
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExtraCategory>()
                .Property(x => x.Name)
                .IsRequired();
        }
    }
}
