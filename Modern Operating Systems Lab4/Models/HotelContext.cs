using System.Data.Entity;

namespace Modern_Operating_Systems_Lab4.Models
{
    public class HotelContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<ClientAccommodation> ClientAccommodations { get; set; }
        public DbSet<ClientService> ClientServices { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Occupation> Occupations { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<OrderType> OrderTypes { get; set; }
        public DbSet<InternalOrder> InternalOrders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InternalOrder>()
                .HasRequired(e => e.AssignedTo)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<InternalOrder>()
                .HasRequired(e => e.RequestedBy)
                .WithMany()
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}