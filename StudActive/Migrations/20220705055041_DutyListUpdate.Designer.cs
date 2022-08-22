﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudActive.Entities;

namespace StudActive.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20220705055041_DutyListUpdate")]
    partial class DutyListUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("StudActive.Entities.DutyList", b =>
                {
                    b.Property<Guid>("DutyListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateDuty")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DutyListCalendarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Fio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsDone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsVerification")
                        .HasColumnType("bit");

                    b.HasKey("DutyListId");

                    b.ToTable("DutyLists");
                });

            modelBuilder.Entity("StudActive.Entities.DutyListCalendar", b =>
                {
                    b.Property<Guid>("DutyListCalendarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorFio")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DutyListCalendarId");

                    b.ToTable("DutyListCalendars");
                });

            modelBuilder.Entity("StudActive.Entities.Group", b =>
                {
                    b.Property<Guid>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CourseNumber")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GroupId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("StudActive.Entities.HigherSchool", b =>
                {
                    b.Property<Guid>("HigherSchoolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HigherSchoolId");

                    b.ToTable("HigherSchools");
                });

            modelBuilder.Entity("StudActive.Entities.RoleStudActive", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RoleStudActives");
                });

            modelBuilder.Entity("StudActive.Entities.Student", b =>
                {
                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobilePhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("StudentId");

                    b.HasIndex("UserId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("StudActive.Entities.StudentCouncil", b =>
                {
                    b.Property<Guid>("StudentCouncilId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentCouncilId");

                    b.ToTable("StudentCouncils");
                });

            modelBuilder.Entity("StudActive.Entities.StudentStudActive", b =>
                {
                    b.Property<Guid>("StudActiveId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsArchive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LeavingDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ReEntryDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("Role")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StudentCouncilId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("VkLink")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudActiveId");

                    b.ToTable("StudentStudActives");
                });

            modelBuilder.Entity("StudActive.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("StudActive.Entities.Student", b =>
                {
                    b.HasOne("StudActive.Entities.User", "User")
                        .WithMany("Students")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Students_Users")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("StudActive.Entities.User", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
