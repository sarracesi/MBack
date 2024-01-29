using Microsoft.EntityFrameworkCore;



namespace Mobilis
{
    
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Notification> Notifications { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<OTP> OTPs { get; set; } 
        public DbSet<Agency> Agencies { get; set; } 
        public DbSet<PointOfSale> PointOfSales { get; set; }

        
    }
    

    public class User
    {  
        
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Job { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class Token
    {
        public int Id { get; set; }
        public string TokenValue { get; set; }
        public DateTime Expiration { get; set; }
        
    }

    public class OTP
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public bool IsValid { get; set; }
        
    }
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string DeviceToken { get; set; }
       
    }
    public class Agency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string localisation { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public DateTime opening { get; set; }
        public DateTime closing { get; set; }
    }

     public class PointOfSale
    {
        public Guid Id { get; set; }
        public string localisation { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public TimeSpan opening { get; set; }
        public TimeSpan closing { get; set; }
    }
}