﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using University.Respository;

#nullable disable

namespace University.Respository.Migrations
{
    [DbContext(typeof(UniversityDbContext))]
    [Migration("20230314070533_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DepartamentLecture", b =>
                {
                    b.Property<int>("departamentsid")
                        .HasColumnType("int");

                    b.Property<int>("lecturesid")
                        .HasColumnType("int");

                    b.HasKey("departamentsid", "lecturesid");

                    b.HasIndex("lecturesid");

                    b.ToTable("DepartamentLecture");
                });

            modelBuilder.Entity("University.Respository.Models.Departament", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("departaments");
                });

            modelBuilder.Entity("University.Respository.Models.Lecture", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("lectures");
                });

            modelBuilder.Entity("University.Respository.Models.Student", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("departamentsid")
                        .HasColumnType("int");

                    b.Property<string>("fName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("departamentsid");

                    b.ToTable("students");
                });

            modelBuilder.Entity("DepartamentLecture", b =>
                {
                    b.HasOne("University.Respository.Models.Departament", null)
                        .WithMany()
                        .HasForeignKey("departamentsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("University.Respository.Models.Lecture", null)
                        .WithMany()
                        .HasForeignKey("lecturesid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("University.Respository.Models.Student", b =>
                {
                    b.HasOne("University.Respository.Models.Departament", "departaments")
                        .WithMany("students")
                        .HasForeignKey("departamentsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("departaments");
                });

            modelBuilder.Entity("University.Respository.Models.Departament", b =>
                {
                    b.Navigation("students");
                });
#pragma warning restore 612, 618
        }
    }
}
