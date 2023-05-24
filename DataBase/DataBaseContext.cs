using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection.Metadata;
using UseAll.Customers.API.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace UseAll.Customers.API.DataBase
{
    public class DataBaseContext : DbContext
    {
        private string _server;
        private string _database;
        private string _port;
        private string _userId;
        private string _password;
        private string _ConnectionString => $"Server={_server};Database={_database};Port={_port};User Id={_userId};Password={_password};Pooling=true;";

        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(_ConnectionString);
        }
        public DataBaseContext() 
        {
            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            _server = config.GetSection("Database:Server").Value;
            _database = config.GetSection("Database:Database").Value;
            _port = config.GetSection("Database:Port").Value;
            _userId = config.GetSection("Database:User").Value;
            _password = config.GetSection("Database:Password").Value;
        }
    }
    

}
