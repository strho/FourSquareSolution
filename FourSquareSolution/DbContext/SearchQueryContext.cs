using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using FourSquareSolution.Entity;

namespace FourSquareSolution.DbContext
{
    /// <summary>
    ///     Database context for SearchQueries entity
    /// </summary>
    public class SearchQueryContext : System.Data.Entity.DbContext
    {
        public SearchQueryContext() : base("FourSquareAssignment")
        {
        }

        public DbSet<SearchQuery> SearchQueries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}