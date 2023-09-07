using API.DTOs.Roles;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class PlacementDbContext :DbContext
    {
        public PlacementDbContext(DbContextOptions<PlacementDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Placement> Placements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Role>().HasData(new InsertRoleDefaultDto
                                                {
                                                    Guid = Guid.Parse("ae259a90-e2e8-442f-ce18-08db91a71ab9"),
                                                    Name = "Employee"
                                                },
                                                new InsertRoleDefaultDto
                                                {
                                                    Guid = Guid.Parse("4ec90656-e89c-4871-d9e5-08db8a7d0f37"),
                                                    Name = "Trainer"
                                                },
                                                new InsertRoleDefaultDto
                                                {
                                                    Guid = Guid.Parse("c0689b0a-5c87-46f1-ce19-08db91a71ab9"),
                                                    Name = "Operasional"
                                                },
                                                new InsertRoleDefaultDto
                                                {
                                                    Guid = Guid.Parse("5FB9ADC0-7D08-45D4-CD66-08DB9C7A678F"),
                                                    Name = "Admin"
                                                });
            modelBuilder.Entity<Employee>().HasIndex(e => new
            {
                e.NIK,
                e.Email,
                e.PhoneNumber
            }).IsUnique();
            modelBuilder.Entity<Client>().HasIndex(e => new
            {
                e.Email
            }).IsUnique();
            //Account Role
            modelBuilder.Entity<AccountRole>()
                        .HasOne(AccountRole => AccountRole.Role)
                        .WithMany(Role => Role.AccountRoles)
                        .HasForeignKey(AccountRole => AccountRole.RoleGuid);
            modelBuilder.Entity<AccountRole>()
                        .HasOne(AccountRole => AccountRole.Account)
                        .WithMany(Account => Account.AccountRoles)
                        .HasForeignKey(AccountRole => AccountRole.AccountGuid);
            //Employee
            modelBuilder.Entity<Employee>()
                        .HasOne(Employee => Employee.Account)
                        .WithOne(Account => Account.Employee)
                        .HasForeignKey<Account>(Account => Account.Guid);
            modelBuilder.Entity<Employee>()
                        .HasOne(Employee => Employee.Interview)
                        .WithOne(Interview => Interview.Employee)
                        .HasForeignKey<Interview>(Interview => Interview.Guid);
            /*modelBuilder.Entity<Employee>()
                        .HasOne(Employee => Employee.Client)
                        .WithOne(Client => Client.Employee)*//*
                        .HasForeignKey<Employee>(Employee => Employee.ClientGuid);*/
            modelBuilder.Entity<Employee>()
                        .HasOne(Employee => Employee.Grade)
                        .WithOne(Grade => Grade.Employee)
                        .HasForeignKey<Grade>(Grade => Grade.Guid);
            modelBuilder.Entity<Employee>()
                        .HasOne(Employee => Employee.Placement)
                        .WithOne(Placement => Placement.Employee)
                        .HasForeignKey<Placement>(Placement => Placement.Guid);
            //Placement
            modelBuilder.Entity<Placement>()
                        .HasOne(Placement => Placement.Client)
                        .WithMany(Client => Client.Placements)
                        .HasForeignKey(Placement => Placement.ClientGuid);
            //Client
            modelBuilder.Entity<Client>()
                        .HasMany(Client => Client.Interviews)
                        .WithOne(Interview => Interview.Client)
                        .HasForeignKey(Interview => Interview.ClientGuid);
            modelBuilder.Entity<Position>()
                        .HasOne(Position=>Position.Client)
                        .WithMany(Client=>Client.Positions)
                        .HasForeignKey(Position => Position.ClientGuid);
            modelBuilder.Entity<History>()
                        .HasOne(History => History.Employee)
                        .WithMany(Employee => Employee.Histories)
                        .HasForeignKey(History => History.EmployeeGuid);
            //Position
            /*modelBuilder.Entity<Position>()
                        .HasOne(Position => Position.Interview)
                        .WithOne(Interview => Interview.Position)
                        .HasForeignKey<Interview>(Position => Position.PositionGuid);*/
            



        }
    }
}
