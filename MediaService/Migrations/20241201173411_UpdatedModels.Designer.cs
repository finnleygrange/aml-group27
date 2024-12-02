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
    [Migration("20241201173411_UpdatedModels")]
    partial class UpdatedModels
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

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MediaTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MediaTypeId");

                    b.ToTable("MediaItems", (string)null);

                    b.UseTptMappingStrategy();
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

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PageCount")
                        .HasColumnType("int");

                    b.ToTable("Books", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "John Doe",
                            Description = "An example book description.",
                            Genre = "Fiction",
                            MediaTypeId = 1,
                            Title = "Example Book",
                            ISBN = "123-456-789",
                            PageCount = 300
                        });
                });

            modelBuilder.Entity("MediaService.Models.DVD", b =>
                {
                    b.HasBaseType("MediaService.Models.MediaItem");

                    b.Property<int>("DurationMinutes")
                        .HasColumnType("int");

                    b.ToTable("DVDs", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 2,
                            Author = "Jane Doe",
                            Description = "An example DVD description.",
                            Genre = "Documentary",
                            MediaTypeId = 2,
                            Title = "Example DVD",
                            DurationMinutes = 120
                        });
                });

            modelBuilder.Entity("MediaService.Models.Game", b =>
                {
                    b.HasBaseType("MediaService.Models.MediaItem");

                    b.Property<string>("AgeRating")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Platform")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Games", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 3,
                            Author = "Game Studio",
                            Description = "An example game description.",
                            Genre = "Action",
                            MediaTypeId = 3,
                            Title = "Example Game",
                            AgeRating = "Mature",
                            Platform = "PC"
                        });
                });

            modelBuilder.Entity("MediaService.Models.Journal", b =>
                {
                    b.HasBaseType("MediaService.Models.MediaItem");

                    b.Property<int>("IssueNumber")
                        .HasColumnType("int");

                    b.ToTable("Journals", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 4,
                            Author = "Editor Team",
                            Description = "An example journal description.",
                            Genre = "Science",
                            MediaTypeId = 4,
                            Title = "Example Journal",
                            IssueNumber = 12
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

            modelBuilder.Entity("MediaService.Models.Book", b =>
                {
                    b.HasOne("MediaService.Models.MediaItem", null)
                        .WithOne()
                        .HasForeignKey("MediaService.Models.Book", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MediaService.Models.DVD", b =>
                {
                    b.HasOne("MediaService.Models.MediaItem", null)
                        .WithOne()
                        .HasForeignKey("MediaService.Models.DVD", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MediaService.Models.Game", b =>
                {
                    b.HasOne("MediaService.Models.MediaItem", null)
                        .WithOne()
                        .HasForeignKey("MediaService.Models.Game", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MediaService.Models.Journal", b =>
                {
                    b.HasOne("MediaService.Models.MediaItem", null)
                        .WithOne()
                        .HasForeignKey("MediaService.Models.Journal", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
