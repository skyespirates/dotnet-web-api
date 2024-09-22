﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using project_service.Data;

#nullable disable

namespace project_service.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("project_service.Entities.Book", b =>
                {
                    b.Property<int>("book_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("book_id"));

                    b.Property<string>("author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("book_title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("borrower_id")
                        .HasColumnType("int");

                    b.HasKey("book_id");

                    b.HasIndex("borrower_id");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            book_id = 10,
                            author = "Eichirou Oda",
                            book_title = "One Piece",
                            borrower_id = 5
                        },
                        new
                        {
                            book_id = 11,
                            author = "Hajime Isayama",
                            book_title = "Shingeki No Kyoujin",
                            borrower_id = 5
                        },
                        new
                        {
                            book_id = 12,
                            author = "Yusuke Murate",
                            book_title = "One Punch Man",
                            borrower_id = 6
                        });
                });

            modelBuilder.Entity("project_service.Entities.Student", b =>
                {
                    b.Property<int>("student_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("student_id"));

                    b.Property<string>("student_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("student_id");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            student_id = 5,
                            student_name = "Zilong"
                        },
                        new
                        {
                            student_id = 6,
                            student_name = "Freya"
                        },
                        new
                        {
                            student_id = 7,
                            student_name = "Martis"
                        });
                });

            modelBuilder.Entity("project_service.Entities.Todo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isCompleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Todos");
                });

            modelBuilder.Entity("project_service.Entities.Book", b =>
                {
                    b.HasOne("project_service.Entities.Student", "borrower")
                        .WithMany("BorrowedBooks")
                        .HasForeignKey("borrower_id");

                    b.Navigation("borrower");
                });

            modelBuilder.Entity("project_service.Entities.Student", b =>
                {
                    b.Navigation("BorrowedBooks");
                });
#pragma warning restore 612, 618
        }
    }
}
