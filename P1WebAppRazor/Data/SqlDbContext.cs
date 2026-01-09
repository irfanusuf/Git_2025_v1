using System;
using Microsoft.EntityFrameworkCore;
using P1WebAppRazor.Models;


namespace P1WebAppRazor.Data;

public class SqlDbContext : DbContext
{

    public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }    // ye khud se ek table banaye ga db may jiska name hoga users 

    // jisme her ek user ki wahi property hogey jo User class may hai 
    public DbSet<Blog> Blogs { get; set; }

}
