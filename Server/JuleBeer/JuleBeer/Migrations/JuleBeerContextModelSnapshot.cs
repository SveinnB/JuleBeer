﻿// <auto-generated />
using System;
using JuleBeer.DB.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JuleBeer.Migrations
{
    [DbContext(typeof(JuleBeerContext))]
    partial class JuleBeerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("JuleBeer.DB.Entities.Beer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ABV")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Beers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ABV = "4,2",
                            Description = "Ljósgullinn, skýjaður. Ósætur, léttur, ferskur, meðalbeiskja. Mangó, grösugur, sítrusbörkur",
                            ImageUrl = "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/28823_r.jpg",
                            Name = "Jólasopi Session IPA"
                        },
                        new
                        {
                            Id = 2,
                            ABV = "8,5",
                            Description = "Rafbrúnn, óskír. Smásætur, þéttur, hverfandi beiskja. Ristað malt, kandís, þurrkaðar apríkósur, karamella",
                            ImageUrl = "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/26336_r.jpg",
                            Name = "Þriðji í jólum belgian tripel"
                        },
                        new
                        {
                            Id = 3,
                            ABV = "5,8",
                            Description = "Rafbrúnn. Sætuvottur, meðalfylltur, miðlungsbeiskja. Þurrkaðir ávextir, kandís, kóla",
                            ImageUrl = "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/27673_r.jpg",
                            Name = "Okkara Jól"
                        },
                        new
                        {
                            Id = 4,
                            ABV = "8",
                            Description = "Rauður, skýjaður. Hálfsætur, þéttur, meðalbeiskja, sýruríkur. Bláber, jarðarber, skyr",
                            ImageUrl = "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/28778_r.jpg",
                            Name = "Skyrjarmur Skyrjarmur nr. 99"
                        },
                        new
                        {
                            Id = 5,
                            ABV = "4,5",
                            Description = "Ljósgullinn, skýjaður. Ósætur, léttur, meðalbeiskja. Appelsínubörkur, kóríander, blómlegur",
                            ImageUrl = "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/26170_r.jpg",
                            Name = "Frostrósir White Ale"
                        },
                        new
                        {
                            Id = 6,
                            ABV = "6,5",
                            Description = "Svarbrúnn. Sætuvottur, þéttur, lítil beiskja. Ristað malt, kakó, kaffi, súkkulaði",
                            ImageUrl = "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/28823_r.jpg",
                            Name = "Jóla Kaldi Súkkulaði Porter"
                        },
                        new
                        {
                            Id = 7,
                            ABV = "5,2",
                            Description = "Ljósgullinn, skýjaður. Ósætur, meðalfylltur, beiskur. Sítrus, mangó, barr, grösugir humlar",
                            ImageUrl = "https://www.vinbudin.is/Portaldata/1/Resources/vorumyndir/medium/25300_r.jpg",
                            Name = "Bjúgnakrækir nr. 71"
                        });
                });

            modelBuilder.Entity("JuleBeer.DB.Entities.BeerReview", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BeerId")
                        .HasColumnType("int");

                    b.Property<int>("Starts")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BeerId");

                    b.HasIndex("UserId");

                    b.ToTable("BeerReviews");
                });

            modelBuilder.Entity("JuleBeer.DB.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("JuleBeer.DB.Entities.BeerReview", b =>
                {
                    b.HasOne("JuleBeer.DB.Entities.Beer", "Beer")
                        .WithMany("BeerReviews")
                        .HasForeignKey("BeerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JuleBeer.DB.Entities.User", "User")
                        .WithMany("BeerReviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("JuleBeer.DB.Entities.Beer", b =>
                {
                    b.Navigation("BeerReviews");
                });

            modelBuilder.Entity("JuleBeer.DB.Entities.User", b =>
                {
                    b.Navigation("BeerReviews");
                });
#pragma warning restore 612, 618
        }
    }
}
