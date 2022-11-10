using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace JuleBeer.DB.Entities;

public class Beer : IEntityTypeConfiguration<Beer>
{
    [Key]
    public int Id { get; set; }

    [MaxLength(500)]
    public string Name { get; set; }

    [MaxLength(1000)]
    public string Description { get; set; }

    [MaxLength(10)]
    public string ABV { get; set; }

    [MaxLength(2000)]
    public string ImageUrl { get; set; }

    public List<BeerReview> BeerReviews { get; set; }

    public void Configure(EntityTypeBuilder<Beer> builder)
    {
        builder.HasData(new Beer
        {
            Id = 1,
            Name = "Jólasopi Session IPA",
            Description = "Ljósgullinn, skýjaður. Ósætur, léttur, ferskur, meðalbeiskja. Mangó, grösugur, sítrusbörkur",
            ABV = "4,2",
            ImageUrl = "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/28823_r.jpg",
        },
        new Beer
        {
            Id = 2,
            Name = "Þriðji í jólum belgian tripel",
            Description = "Rafbrúnn, óskír. Smásætur, þéttur, hverfandi beiskja. Ristað malt, kandís, þurrkaðar apríkósur, karamella",
            ABV = "8,5",
            ImageUrl = "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/26336_r.jpg",
        },
        new Beer
        {
            Id = 3,
            Name = "Okkara Jól",
            Description = "Rafbrúnn. Sætuvottur, meðalfylltur, miðlungsbeiskja. Þurrkaðir ávextir, kandís, kóla",
            ABV = "5,8",
            ImageUrl = "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/27673_r.jpg",
        },
        new Beer
        {
            Id = 4,
            Name = "Skyrjarmur Skyrjarmur nr. 99",
            Description = "Rauður, skýjaður. Hálfsætur, þéttur, meðalbeiskja, sýruríkur. Bláber, jarðarber, skyr",
            ABV = "8,0",
            ImageUrl = "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/28778_r.jpg",
        },
        new Beer
        {
            Id = 5,
            Name = "Frostrósir White Ale",
            Description = "Ljósgullinn, skýjaður. Ósætur, léttur, meðalbeiskja. Appelsínubörkur, kóríander, blómlegur",
            ABV = "4,5",
            ImageUrl = "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/26170_r.jpg",
        },
        new Beer
        {
            Id = 6,
            Name = "Jóla Kaldi Súkkulaði Porter",
            Description = "Svarbrúnn. Sætuvottur, þéttur, lítil beiskja. Ristað malt, kakó, kaffi, súkkulaði",
            ABV = "6,5",
            ImageUrl = "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/22261_r.jpg",
        },
        new Beer
        {
            Id = 7,
            Name = "Bjúgnakrækir nr. 71",
            Description = "Ljósgullinn, skýjaður. Ósætur, meðalfylltur, beiskur. Sítrus, mangó, barr, grösugir humlar",
            ABV = "5,2",
            ImageUrl = "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/25300_r.jpg",
        },
        new Beer
        {
            Id = 8,
            Name = "Tuborg Julebryg",
            Description = "Rafbrúnn. Sætuvottur, meðalfylling, lítil beiskja. Ristað malt, þurrkaðir ávextir, kandís",
            ABV = "5,6",
            ImageUrl = "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/09452_r.jpg",
        },
        new Beer
        {
            Id = 9,
            Name = "Jóla Kaldi",
            Description = "Rafrauður. Ósætur, meðalfylltur, ferskur, miðlungsbeiskja. Karamella, þurrkaðir ávextir, barr, jörð",
            ABV = "5,4",
            ImageUrl = "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/13791_r.jpg",
        },
        new Beer
        {
            Id = 10,
            Name = "Stella Artois",
            Description = "Ljósgullinn. Ósætur, létt meðalfylling, lítil beiskja. Korn, blómlegir humlar",
            ABV = "5,0",
            ImageUrl = "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/25300_r.jpg",
        }
        );
    }
}
