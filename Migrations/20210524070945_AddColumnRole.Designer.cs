// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SelfServiceHrProject.Data;

namespace SelfServiceHrProject.Migrations
{
    [DbContext(typeof(SelfServiceHrProjectContext))]
    [Migration("20210524070945_AddColumnRole")]
    partial class AddColumnRole
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SelfServiceHrProject.Models.Address", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployeesId")
                        .HasColumnType("int");

                    b.Property<string>("LocalAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OtherAddress")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("EmployeesId")
                        .IsUnique();

                    b.ToTable("Address");
                });

            modelBuilder.Entity("SelfServiceHrProject.Models.Employees", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EGN")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Family")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("OrganisationsId")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PositionsId")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.HasIndex("OrganisationsId");

                    b.HasIndex("PositionsId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("SelfServiceHrProject.Models.Organisations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Organisations");
                });

            modelBuilder.Entity("SelfServiceHrProject.Models.Positions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("SelfServiceHrProject.Models.Salary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("BonusSalary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("EmployeesId")
                        .HasColumnType("int");

                    b.Property<decimal>("NetSalary")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeesId")
                        .IsUnique();

                    b.ToTable("Salary");
                });

            modelBuilder.Entity("SelfServiceHrProject.Models.SystemUsers", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EmployeesId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("EmployeesId")
                        .IsUnique();

                    b.ToTable("SystemUsers");
                });

            modelBuilder.Entity("SelfServiceHrProject.Models.Address", b =>
                {
                    b.HasOne("SelfServiceHrProject.Models.Employees", "Employee")
                        .WithOne("Address")
                        .HasForeignKey("SelfServiceHrProject.Models.Address", "EmployeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("SelfServiceHrProject.Models.Employees", b =>
                {
                    b.HasOne("SelfServiceHrProject.Models.Organisations", "Oragnisation")
                        .WithMany("Employees")
                        .HasForeignKey("OrganisationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SelfServiceHrProject.Models.Positions", "Position")
                        .WithMany("Employees")
                        .HasForeignKey("PositionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Oragnisation");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("SelfServiceHrProject.Models.Salary", b =>
                {
                    b.HasOne("SelfServiceHrProject.Models.Employees", "Employee")
                        .WithOne("Salary")
                        .HasForeignKey("SelfServiceHrProject.Models.Salary", "EmployeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("SelfServiceHrProject.Models.SystemUsers", b =>
                {
                    b.HasOne("SelfServiceHrProject.Models.Employees", "Employee")
                        .WithOne("SystemUser")
                        .HasForeignKey("SelfServiceHrProject.Models.SystemUsers", "EmployeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("SelfServiceHrProject.Models.Employees", b =>
                {
                    b.Navigation("Address");

                    b.Navigation("Salary");

                    b.Navigation("SystemUser");
                });

            modelBuilder.Entity("SelfServiceHrProject.Models.Organisations", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("SelfServiceHrProject.Models.Positions", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
