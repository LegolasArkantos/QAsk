using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CrudApp.Models;

namespace CrudApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CrudApp.Models.Question>? Question { get; set; }
        public DbSet<Message> Messages { get; set; }

        public DbSet<FileQuestion> FileQuestions { get; set; }

        public DbSet<CustomUser> CustomUsers { get; set; }

        public DbSet<Answer> Answers { get; set; }  

    }
}
