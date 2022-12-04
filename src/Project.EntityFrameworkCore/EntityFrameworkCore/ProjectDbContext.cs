using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Project.Authorization.Roles;
using Project.Authorization.Users;
using Project.MultiTenancy;
// Dont remove this line Auto generate usings
using Project.Employees.RootEntities;

using Project.Employees.Indecies;

using Project.Employees.Entities;





namespace Project.EntityFrameworkCore
{
    public class ProjectDbContext : AbpZeroDbContext<Tenant, Role, User, ProjectDbContext>
    {
        //DBSets Auto generate dont remove this line
       public DbSet<Employee> Employees{ get; set; }

       public DbSet<Country> Countries{ get; set; }

       public DbSet<Nationality> Nationalities{ get; set; }

       public DbSet<Children> Children{ get; set; }





        /* Define a DbSet for each entity of the application */

        public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
            : base(options)
        {
        }
    }
}
