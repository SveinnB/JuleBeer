using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace JuleBeer.DB.Entities;

public class BeerReview : IEntityTypeConfiguration<BeerReview>
{
    [Key]
    public Guid Id { get; set; }

    public int BeerId { get; set; }
    public Beer Beer { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }

    public int Stars { get; set; }

    public void Configure(EntityTypeBuilder<BeerReview> builder)
    {
        builder
         .HasOne(x => x.User)
         .WithMany(x => x.BeerReviews)
         .HasForeignKey(x => x.UserId)
         .HasPrincipalKey(x => x.Id)
         .OnDelete(DeleteBehavior.Cascade);

        builder
        .HasOne(x => x.Beer)
        .WithMany(x => x.BeerReviews)
        .HasForeignKey(x => x.BeerId)
        .HasPrincipalKey(x => x.Id)
        .OnDelete(DeleteBehavior.Cascade);

    }
}
