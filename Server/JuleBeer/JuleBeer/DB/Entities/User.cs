using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace JuleBeer.DB.Entities;

public class User : IEntityTypeConfiguration<User>
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; }

    public List<BeerReview> BeerReviews { get; set; }

    public void Configure(EntityTypeBuilder<User> builder)
    {

    }
}
