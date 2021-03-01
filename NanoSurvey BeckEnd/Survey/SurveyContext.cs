using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NanoSurvey_BeckEnd.Survey
{
    public sealed class SurveyContext : DbContext
    {
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public SurveyContext(DbContextOptions<SurveyContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
