// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ItemzApp.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItemzApp.API.DbContexts
{
    public class ItemzTraceContext : DbContext
    {

        public ItemzTraceContext(DbContextOptions<ItemzTraceContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                   // TODO: for some reason, this is always true here. Investigate why
                   // EF Core team has provided this property and what is the real use 
                   // of the same.
            }
        }

        public DbSet<ItemzJoinItemzTrace>? ItemzJoinItemzTrace { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // EXPLANATION: Here we are defining Many to Many relationship between
            // Itemz join Itemz Trace
            // This is described as Indirect Many-to-Many relationships in Microsoft Docs at ...
            // https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key#indirect-many-to-many-relationships


            modelBuilder.Entity<ItemzJoinItemzTrace>()
                .HasOne(ijit => ijit.FromItemz)
                .WithMany(fi=> fi!.ToItemzJoinItemzTrace)
                .HasForeignKey(ijit => ijit.FromItemzId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ItemzJoinItemzTrace>()
                .HasOne(ijit => ijit.ToItemz)
                .WithMany(ti => ti!.FromItemzJoinItemzTrace)
                .HasForeignKey(ijit => ijit.ToItemzId)
                .OnDelete(DeleteBehavior.Restrict); // See ::DeleteBehavior.Restrict:: NOTES below.

            // EXPLANATION: ::DeleteBehavior.Restrict:: NOTES
            // Note that we have to turn the delete cascade off for at least one of the relationships and
            // manually delete the related join entities before deleting the main entity,
            // because self referencing relationships always introduce possible cycles or multiple cascade path issue,
            // preventing the usage of cascade delete.
            
            // EXPLANATION: Here we are defining a composite key for a join table.
            // it will use FromItemzId + ToItemzId as it's composite key.


            modelBuilder.Entity<ItemzJoinItemzTrace>()
                .HasKey(ijit => new { ijit.FromItemzId, ijit.ToItemzId });

            // TODO: While ItemzContext is the main class that is going to be
            // used for Database Migrations purposes, We should keep all the 
            // details about configuring models in that central place for now.

            base.OnModelCreating(modelBuilder);
        }
    }
}
