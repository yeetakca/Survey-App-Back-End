﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestApp.API.Data;

#nullable disable

namespace TestApp.API.Migrations
{
    [DbContext(typeof(SurveyDbContext))]
    partial class SurveyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TestApp.API.Models.Domain.Survey", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatorUserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CreatorUserID");

                    b.HasIndex("TypeID");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("TestApp.API.Models.Domain.User", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatorUserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("RoleID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            ID = new Guid("62539a22-ba5c-4dd8-95db-67e0926bd1df"),
                            CreatedAt = new DateTime(2023, 11, 1, 21, 22, 32, 495, DateTimeKind.Local).AddTicks(5927),
                            CreatorUserID = new Guid("00000000-0000-0000-0000-000000000000"),
                            Email = "",
                            IsActive = true,
                            IsDeleted = false,
                            ModifiedAt = new DateTime(2023, 11, 1, 21, 22, 32, 495, DateTimeKind.Local).AddTicks(5927),
                            Name = "root",
                            Password = "root",
                            RoleID = 3,
                            Username = "root"
                        });
                });

            modelBuilder.Entity("TestApp.API.Models.Domains.Answer", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatorUserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("QuestionChoiceID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("QuestionID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("CreatorUserID");

                    b.HasIndex("QuestionChoiceID");

                    b.HasIndex("QuestionID");

                    b.HasIndex("UserID");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("TestApp.API.Models.Domains.Assign", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatorUserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("SurveyID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("CreatorUserID");

                    b.HasIndex("SurveyID");

                    b.HasIndex("UserID");

                    b.ToTable("Assigns");
                });

            modelBuilder.Entity("TestApp.API.Models.Domains.Question", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatorUserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("SurveyID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CreatorUserID");

                    b.HasIndex("SurveyID");

                    b.HasIndex("TypeID");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("TestApp.API.Models.Domains.QuestionChoice", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatorUserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("QuestionID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("CreatorUserID");

                    b.HasIndex("QuestionID");

                    b.ToTable("QuestionChoices");
                });

            modelBuilder.Entity("TestApp.API.Models.Domains.QuestionType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("QuestionTypes");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            CreatedAt = new DateTime(2023, 11, 1, 21, 22, 32, 495, DateTimeKind.Local).AddTicks(5830),
                            IsActive = true,
                            IsDeleted = false,
                            Name = "text"
                        },
                        new
                        {
                            ID = 2,
                            CreatedAt = new DateTime(2023, 11, 1, 21, 22, 32, 495, DateTimeKind.Local).AddTicks(5832),
                            IsActive = true,
                            IsDeleted = false,
                            Name = "true/false"
                        },
                        new
                        {
                            ID = 3,
                            CreatedAt = new DateTime(2023, 11, 1, 21, 22, 32, 495, DateTimeKind.Local).AddTicks(5833),
                            IsActive = true,
                            IsDeleted = false,
                            Name = "sa multiple choice"
                        },
                        new
                        {
                            ID = 4,
                            CreatedAt = new DateTime(2023, 11, 1, 21, 22, 32, 495, DateTimeKind.Local).AddTicks(5834),
                            IsActive = true,
                            IsDeleted = false,
                            Name = "ma multiple choice"
                        });
                });

            modelBuilder.Entity("TestApp.API.Models.Domains.Role", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            CreatedAt = new DateTime(2023, 11, 1, 21, 22, 32, 495, DateTimeKind.Local).AddTicks(5645),
                            IsActive = true,
                            IsDeleted = false,
                            Name = "end user"
                        },
                        new
                        {
                            ID = 2,
                            CreatedAt = new DateTime(2023, 11, 1, 21, 22, 32, 495, DateTimeKind.Local).AddTicks(5660),
                            IsActive = true,
                            IsDeleted = false,
                            Name = "admin"
                        },
                        new
                        {
                            ID = 3,
                            CreatedAt = new DateTime(2023, 11, 1, 21, 22, 32, 495, DateTimeKind.Local).AddTicks(5661),
                            IsActive = true,
                            IsDeleted = false,
                            Name = "root"
                        });
                });

            modelBuilder.Entity("TestApp.API.Models.Domains.Submit", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatorUserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("SurveyID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("CreatorUserID");

                    b.HasIndex("SurveyID");

                    b.HasIndex("UserID");

                    b.ToTable("Submits");
                });

            modelBuilder.Entity("TestApp.API.Models.Domains.SurveyType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("SurveyTypes");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            CreatedAt = new DateTime(2023, 11, 1, 21, 22, 32, 495, DateTimeKind.Local).AddTicks(5807),
                            IsActive = true,
                            IsDeleted = false,
                            Name = "single page"
                        },
                        new
                        {
                            ID = 2,
                            CreatedAt = new DateTime(2023, 11, 1, 21, 22, 32, 495, DateTimeKind.Local).AddTicks(5809),
                            IsActive = true,
                            IsDeleted = false,
                            Name = "multi page"
                        });
                });

            modelBuilder.Entity("TestApp.API.Models.Domain.Survey", b =>
                {
                    b.HasOne("TestApp.API.Models.Domain.User", "CreatorUser")
                        .WithMany()
                        .HasForeignKey("CreatorUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestApp.API.Models.Domains.SurveyType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatorUser");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("TestApp.API.Models.Domain.User", b =>
                {
                    b.HasOne("TestApp.API.Models.Domains.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("TestApp.API.Models.Domains.Answer", b =>
                {
                    b.HasOne("TestApp.API.Models.Domain.User", "CreatorUser")
                        .WithMany()
                        .HasForeignKey("CreatorUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestApp.API.Models.Domains.QuestionChoice", "QuestionChoice")
                        .WithMany()
                        .HasForeignKey("QuestionChoiceID");

                    b.HasOne("TestApp.API.Models.Domains.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestApp.API.Models.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatorUser");

                    b.Navigation("Question");

                    b.Navigation("QuestionChoice");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TestApp.API.Models.Domains.Assign", b =>
                {
                    b.HasOne("TestApp.API.Models.Domain.User", "CreatorUser")
                        .WithMany()
                        .HasForeignKey("CreatorUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestApp.API.Models.Domain.Survey", "Survey")
                        .WithMany()
                        .HasForeignKey("SurveyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestApp.API.Models.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatorUser");

                    b.Navigation("Survey");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TestApp.API.Models.Domains.Question", b =>
                {
                    b.HasOne("TestApp.API.Models.Domain.User", "CreatorUser")
                        .WithMany()
                        .HasForeignKey("CreatorUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestApp.API.Models.Domain.Survey", "Survey")
                        .WithMany()
                        .HasForeignKey("SurveyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestApp.API.Models.Domains.QuestionType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatorUser");

                    b.Navigation("Survey");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("TestApp.API.Models.Domains.QuestionChoice", b =>
                {
                    b.HasOne("TestApp.API.Models.Domain.User", "CreatorUser")
                        .WithMany()
                        .HasForeignKey("CreatorUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestApp.API.Models.Domains.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatorUser");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("TestApp.API.Models.Domains.Submit", b =>
                {
                    b.HasOne("TestApp.API.Models.Domain.User", "CreatorUser")
                        .WithMany()
                        .HasForeignKey("CreatorUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestApp.API.Models.Domain.Survey", "Survey")
                        .WithMany()
                        .HasForeignKey("SurveyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestApp.API.Models.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatorUser");

                    b.Navigation("Survey");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
