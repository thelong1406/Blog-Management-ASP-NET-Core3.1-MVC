using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Persistence.EntitiesConfigurations
{
    public class VoteConfiguration : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.ToTable("[blog].[vote]");
            #region Properties config
            builder.HasKey(v => new { v.UserId, v.PostId });
            //User id
            builder.Property(v => v.UserId)
                .HasColumnName("userId")
                .IsRequired();
            //Post id
            builder.Property(v => v.PostId)
                .HasColumnName("postId")
                .IsRequired();
            //Rate
            builder.Property(v => v.Rate)
                .HasColumnName("rate")
                .HasColumnType("tinyint")
                .IsRequired();
            //Constraint rate
            builder.HasCheckConstraint(
                "check_vote_rate",
                "rate between 0 and 5");
            #endregion

            #region Index Config
            builder.HasIndex(v => v.PostId)
                .HasName("idx_vote_postId");
            #endregion

            #region Relationship Config
            builder.HasOne(v => v.User)
                .WithMany(u => u.Votes)
                .HasPrincipalKey(u => u.Id)
                .HasForeignKey(v=>v.UserId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("fpk_vote_user");
            
            builder.HasOne(v => v.Post)
                .WithMany(p => p.Votes)
                .HasPrincipalKey(p=> p.PostId)
                .HasForeignKey(v=>v.PostId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("fpk_vote_post");
            #endregion
        }
        public VoteConfiguration() { }
    }
}
