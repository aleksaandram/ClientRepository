
using Microsoft.EntityFrameworkCore;
using ClientRepository.Models;

namespace ClientRepository.Data
{
    public class ClientContext : DbContext
    {/*
        public ClientContext() : base("ClientDBConnectionString")
        {
        }*/

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyLocalDatabase;Integrated Security=True;");
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Address> Addresses { get; set; }

       


    }
}
