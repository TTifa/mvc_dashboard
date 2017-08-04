namespace Ttifa.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TtifaModel : DbContext
    {
        public TtifaModel()
            : base("name=TtifaModel")
        {
        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<CronTask> CronTasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
