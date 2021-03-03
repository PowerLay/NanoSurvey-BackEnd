using Microsoft.EntityFrameworkCore;

namespace NanoSurvey_BackEnd.Survey
{
    public sealed class SurveyContext : DbContext
    {
        public SurveyContext(DbContextOptions<SurveyContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Survey> Surveys { get; set; }
    }
}