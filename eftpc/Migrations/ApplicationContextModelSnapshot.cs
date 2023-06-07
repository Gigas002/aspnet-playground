﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace eftpc.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0-preview.3.23174.2");

            modelBuilder.Entity("User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("Employee", b =>
                {
                    b.HasBaseType("User");

                    b.Property<int>("Salary")
                        .HasColumnType("INTEGER");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Manager", b =>
                {
                    b.HasBaseType("User");

                    b.Property<string>("Departament")
                        .HasColumnType("TEXT");

                    b.ToTable("Managers");
                });
#pragma warning restore 612, 618
        }
    }
}