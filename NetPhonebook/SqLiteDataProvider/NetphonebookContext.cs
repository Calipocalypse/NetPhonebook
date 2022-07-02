using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetPhonebook.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SqLiteDataProvider
{
    public class NetphonebookContext : DbContext
    {
        private string sqliteFilePath;

        /* Models */
        /* public DbSet<Setting> settings { get; set; }
        public DbSet<UserSetting> userSettings { get; set; }
        public DbSet<Image> images { get; set; } */
        public DbSet<ExtraInfo> extraInfos { get; set; }
        public DbSet<ExtraCategory> extraCategories { get; set; }
        public DbSet<VirtualModel> virtualModels { get; set; }
        public DbSet<VirtualModelsCustomization> virtualModelsCustomizations { get; set; }
        public DbSet<FavouriteColor> favouriteColors { get; set; }
        /*public DbSet<VirtualModelsData> virtualModelsDatas { get; set; }
        public DbSet<VirtualModelsCellData> virtualModelsCellDatas { get; set; } */

        /* Code */

        public NetphonebookContext()
        {
            sqliteFilePath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\database.sqlite3";
            if (!System.IO.File.Exists(sqliteFilePath)) System.IO.File.Create(sqliteFilePath);
        }

        public NetphonebookContext(DbContextOptions options) : base(options)
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
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ExtraInfo>();
            modelBuilder.Entity<ExtraCategory>();
        }
        */
    }
}
