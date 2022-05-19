using Microsoft.EntityFrameworkCore;

namespace QuizApi.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Questao> questoes { get; set; } = null!;
        public DbSet<Resposta> respostas { get; set; } = null!;
        public DbSet<Quiz> quizzes { get; set; } = null!;
    }
}