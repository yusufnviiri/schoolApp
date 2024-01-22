﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using schoolApp.Models.Context;

#nullable disable

namespace schoolApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("schoolApp.Models.AssessedStudent", b =>
                {
                    b.Property<int>("AssessedStudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AssessedStudentId"), 1L, 1);

                    b.Property<int>("SchoolLevelId")
                        .HasColumnType("int");

                    b.Property<int>("SemesterId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("AssessedStudentId");

                    b.HasIndex("SchoolLevelId");

                    b.HasIndex("SemesterId");

                    b.ToTable("AssessedStudents");
                });

            modelBuilder.Entity("schoolApp.Models.Assessement", b =>
                {
                    b.Property<int>("AssessementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AssessementId"), 1L, 1);

                    b.Property<int>("AssesedCourseId")
                        .HasColumnType("int");

                    b.Property<int>("Cat1")
                        .HasColumnType("int");

                    b.Property<int>("Cat2")
                        .HasColumnType("int");

                    b.Property<int>("Final")
                        .HasColumnType("int");

                    b.Property<string>("Grade")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MidExam")
                        .HasColumnType("int");

                    b.Property<int>("TotalScore")
                        .HasColumnType("int");

                    b.HasKey("AssessementId");

                    b.ToTable("Assessements");
                });

            modelBuilder.Entity("schoolApp.Models.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"), 1L, 1);

                    b.Property<int>("AssessementId")
                        .HasColumnType("int");

                    b.Property<string>("CourseCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ReportCardId")
                        .HasColumnType("int");

                    b.Property<int>("SchoolLevelId")
                        .HasColumnType("int");

                    b.Property<int>("SemesterId")
                        .HasColumnType("int");

                    b.HasKey("CourseId");

                    b.HasIndex("AssessementId");

                    b.HasIndex("ReportCardId");

                    b.HasIndex("SchoolLevelId");

                    b.HasIndex("SemesterId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("schoolApp.Models.FeesPayment", b =>
                {
                    b.Property<int>("FeespaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeespaymentId"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("DateOfPayment")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("PrevBalance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecievedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SemesterId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("StudentLevelSchoolLevelId")
                        .HasColumnType("int");

                    b.HasKey("FeespaymentId");

                    b.HasIndex("SemesterId");

                    b.HasIndex("StudentLevelSchoolLevelId");

                    b.ToTable("FeesPayments");
                });

            modelBuilder.Entity("schoolApp.Models.GeneralLedger", b =>
                {
                    b.Property<int>("GeneralLedgerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GeneralLedgerId"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalExpenses")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalIncome")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("GeneralLedgerId");

                    b.ToTable("GeneralLedger");
                });

            modelBuilder.Entity("schoolApp.Models.Guardian", b =>
                {
                    b.Property<int>("GuardianId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GuardianId"), 1L, 1);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("District")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OtherNames")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Relationship")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Residance")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GuardianId");

                    b.ToTable("Guardians");
                });

            modelBuilder.Entity("schoolApp.Models.LevelChange", b =>
                {
                    b.Property<int>("LevelChangeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LevelChangeId"), 1L, 1);

                    b.Property<int>("SchoolLevelId")
                        .HasColumnType("int");

                    b.Property<int>("SemesterId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<decimal>("schoolFess")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("LevelChangeId");

                    b.HasIndex("SchoolLevelId");

                    b.HasIndex("SemesterId");

                    b.ToTable("LevelChanges");
                });

            modelBuilder.Entity("schoolApp.Models.LevelData", b =>
                {
                    b.Property<int>("LevelDataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LevelDataId"), 1L, 1);

                    b.Property<int>("SchoolLevelId")
                        .HasColumnType("int");

                    b.Property<int>("SemesterId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("LevelDataId");

                    b.ToTable("LevelDatas");
                });

            modelBuilder.Entity("schoolApp.Models.LibItem", b =>
                {
                    b.Property<int>("LibItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LibItemId"), 1L, 1);

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LibItemId");

                    b.ToTable("LibItems");
                });

            modelBuilder.Entity("schoolApp.Models.LibUser", b =>
                {
                    b.Property<int>("LibUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LibUserId"), 1L, 1);

                    b.Property<int>("LibItemId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("LibUserId");

                    b.ToTable("LibUsers");
                });

            modelBuilder.Entity("schoolApp.Models.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Compulserysavings")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Paye")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StaffId")
                        .HasColumnType("int");

                    b.HasKey("PaymentId");

                    b.HasIndex("StaffId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("schoolApp.Models.ReportCard", b =>
                {
                    b.Property<int>("ReportCardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReportCardId"), 1L, 1);

                    b.Property<int>("LevelSchoolLevelId")
                        .HasColumnType("int");

                    b.Property<int>("SemesterId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("ReportCardId");

                    b.HasIndex("LevelSchoolLevelId");

                    b.HasIndex("SemesterId");

                    b.ToTable("ReportCards");
                });

            modelBuilder.Entity("schoolApp.Models.SchoolLevel", b =>
                {
                    b.Property<int>("SchoolLevelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SchoolLevelId"), 1L, 1);

                    b.Property<string>("LevelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SchoolLevelId");

                    b.ToTable("SchoolLevels");

                    b.HasData(
                        new
                        {
                            SchoolLevelId = 1,
                            LevelName = "Form One"
                        },
                        new
                        {
                            SchoolLevelId = 2,
                            LevelName = "Form Two"
                        },
                        new
                        {
                            SchoolLevelId = 3,
                            LevelName = "Form Three"
                        },
                        new
                        {
                            SchoolLevelId = 4,
                            LevelName = "Form Four"
                        },
                        new
                        {
                            SchoolLevelId = 5,
                            LevelName = "Form Five"
                        },
                        new
                        {
                            SchoolLevelId = 6,
                            LevelName = "Form Six"
                        });
                });

            modelBuilder.Entity("schoolApp.Models.Semester", b =>
                {
                    b.Property<int>("SemesterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SemesterId"), 1L, 1);

                    b.Property<string>("SemesterName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SemesterId");

                    b.ToTable("Semesters");

                    b.HasData(
                        new
                        {
                            SemesterId = 1,
                            SemesterName = "Term One"
                        },
                        new
                        {
                            SemesterId = 2,
                            SemesterName = "Term Two"
                        },
                        new
                        {
                            SemesterId = 3,
                            SemesterName = "Term Three"
                        });
                });

            modelBuilder.Entity("schoolApp.Models.Staff", b =>
                {
                    b.Property<int>("StaffId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StaffId"), 1L, 1);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CoursesId")
                        .HasColumnType("int");

                    b.Property<string>("District")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EmployementDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LeaveDays")
                        .HasColumnType("int");

                    b.Property<string>("MaritalStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OtherNames")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Qualification")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Residance")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StaffId");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("schoolApp.Models.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"), 1L, 1);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("District")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GuardianId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OtherNames")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Residance")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("SchoolFees")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("SchoolLevelId")
                        .HasColumnType("int");

                    b.Property<int>("SemesterId")
                        .HasColumnType("int");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId");

                    b.HasIndex("GuardianId");

                    b.HasIndex("SchoolLevelId");

                    b.HasIndex("SemesterId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("schoolApp.Models.StudentAssessemet", b =>
                {
                    b.Property<int>("StudentAssessementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentAssessementId"), 1L, 1);

                    b.Property<int>("AssessedLevelId")
                        .HasColumnType("int");

                    b.Property<int>("AssessedSemesterId")
                        .HasColumnType("int");

                    b.Property<int>("AssessedStudentId")
                        .HasColumnType("int");

                    b.Property<int>("AssessementId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.HasKey("StudentAssessementId");

                    b.HasIndex("AssessementId");

                    b.ToTable("StudentAssessemets");
                });

            modelBuilder.Entity("schoolApp.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("StudentId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("schoolApp.Models.AssessedStudent", b =>
                {
                    b.HasOne("schoolApp.Models.SchoolLevel", "SchoolLevel")
                        .WithMany()
                        .HasForeignKey("SchoolLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("schoolApp.Models.Semester", "Semester")
                        .WithMany()
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SchoolLevel");

                    b.Navigation("Semester");
                });

            modelBuilder.Entity("schoolApp.Models.Course", b =>
                {
                    b.HasOne("schoolApp.Models.Assessement", "Assessement")
                        .WithMany()
                        .HasForeignKey("AssessementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("schoolApp.Models.ReportCard", null)
                        .WithMany("Courses")
                        .HasForeignKey("ReportCardId");

                    b.HasOne("schoolApp.Models.SchoolLevel", "SchoolLevel")
                        .WithMany()
                        .HasForeignKey("SchoolLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("schoolApp.Models.Semester", "Semester")
                        .WithMany()
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assessement");

                    b.Navigation("SchoolLevel");

                    b.Navigation("Semester");
                });

            modelBuilder.Entity("schoolApp.Models.FeesPayment", b =>
                {
                    b.HasOne("schoolApp.Models.Semester", "Semester")
                        .WithMany()
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("schoolApp.Models.SchoolLevel", "StudentLevel")
                        .WithMany()
                        .HasForeignKey("StudentLevelSchoolLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Semester");

                    b.Navigation("StudentLevel");
                });

            modelBuilder.Entity("schoolApp.Models.LevelChange", b =>
                {
                    b.HasOne("schoolApp.Models.SchoolLevel", "SchoolLevel")
                        .WithMany()
                        .HasForeignKey("SchoolLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("schoolApp.Models.Semester", "Semester")
                        .WithMany()
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SchoolLevel");

                    b.Navigation("Semester");
                });

            modelBuilder.Entity("schoolApp.Models.Payment", b =>
                {
                    b.HasOne("schoolApp.Models.Staff", "Staff")
                        .WithMany()
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("schoolApp.Models.ReportCard", b =>
                {
                    b.HasOne("schoolApp.Models.SchoolLevel", "Level")
                        .WithMany()
                        .HasForeignKey("LevelSchoolLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("schoolApp.Models.Semester", "Semester")
                        .WithMany()
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Level");

                    b.Navigation("Semester");
                });

            modelBuilder.Entity("schoolApp.Models.Student", b =>
                {
                    b.HasOne("schoolApp.Models.Guardian", "Guardian")
                        .WithMany()
                        .HasForeignKey("GuardianId");

                    b.HasOne("schoolApp.Models.SchoolLevel", "SchoolLevel")
                        .WithMany()
                        .HasForeignKey("SchoolLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("schoolApp.Models.Semester", "Semester")
                        .WithMany()
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Guardian");

                    b.Navigation("SchoolLevel");

                    b.Navigation("Semester");
                });

            modelBuilder.Entity("schoolApp.Models.StudentAssessemet", b =>
                {
                    b.HasOne("schoolApp.Models.Assessement", "Assessement")
                        .WithMany()
                        .HasForeignKey("AssessementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assessement");
                });

            modelBuilder.Entity("schoolApp.Models.User", b =>
                {
                    b.HasOne("schoolApp.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("schoolApp.Models.ReportCard", b =>
                {
                    b.Navigation("Courses");
                });
#pragma warning restore 612, 618
        }
    }
}
