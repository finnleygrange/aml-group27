﻿// <auto-generated />
using MediaService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MediaService.Migrations
{
    [DbContext(typeof(MediaServiceDbContext))]
    [Migration("20241118131740_ImplementInheritance")]
    partial class ImplementInheritance
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MediaService.Models.MediaItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MediaTypeDiscriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<int>("MediaTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MediaTypeId");

                    b.ToTable("MediaItems");

                    b.HasDiscriminator<string>("MediaTypeDiscriminator").HasValue("MediaItem");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("MediaService.Models.MediaType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MediaTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Book"
                        },
                        new
                        {
                            Id = 2,
                            Name = "DVD"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Game"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Journal"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Multimedia"
                        });
                });

            modelBuilder.Entity("MediaService.Models.Book", b =>
                {
                    b.HasBaseType("MediaService.Models.MediaItem");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PageCount")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Book");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "An example book description.",
                            ImageUrl = "https://example.com/images/book.jpg",
                            MediaTypeId = 1,
                            Title = "Example Book",
                            Author = "John Doe",
                            ISBN = "123-456-789",
                            PageCount = 300
                        });
                });

            modelBuilder.Entity("MediaService.Models.DVD", b =>
                {
                    b.HasBaseType("MediaService.Models.MediaItem");

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DurationMinutes")
                        .HasColumnType("int");

                    b.Property<int>("ReleaseYear")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("DVD");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            Description = "An example DVD description.",
                            ImageUrl = "https://example.com/images/dvd.jpg",
                            MediaTypeId = 2,
                            Title = "Example DVD",
                            Director = "Jane Smith",
                            DurationMinutes = 120,
                            ReleaseYear = 0
                        });
                });

            modelBuilder.Entity("MediaService.Models.Game", b =>
                {
                    b.HasBaseType("MediaService.Models.MediaItem");

                    b.Property<string>("AgeRating")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Developer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Platform")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Game");

                    b.HasData(
                        new
                        {
                            Id = 3,
                            Description = "An example game description.",
                            ImageUrl = "https://example.com/images/game.jpg",
                            MediaTypeId = 3,
                            Title = "Example Game",
                            AgeRating = "E",
                            Developer = "GameDev Studios",
                            Genre = "Survival",
                            Platform = "PC"
                        });
                });

            modelBuilder.Entity("MediaService.Models.Journal", b =>
                {
                    b.HasBaseType("MediaService.Models.MediaItem");

                    b.Property<int>("IssueNumber")
                        .HasColumnType("int");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Journal");

                    b.HasData(
                        new
                        {
                            Id = 4,
                            Description = "An example journal description.",
                            ImageUrl = "https://example.com/images/journal.jpg",
                            MediaTypeId = 4,
                            Title = "Example Journal",
                            IssueNumber = 42,
                            Publisher = "Science Journal Press"
                        });
                });

            modelBuilder.Entity("MediaService.Models.MediaItem", b =>
                {
                    b.HasOne("MediaService.Models.MediaType", "MediaType")
                        .WithMany()
                        .HasForeignKey("MediaTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MediaType");
                });
#pragma warning restore 612, 618
        }
    }
}
