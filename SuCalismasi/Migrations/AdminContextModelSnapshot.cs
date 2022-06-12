﻿// <auto-generated />
using Areas.Admin.Water.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Water.Migrations
{
    [DbContext(typeof(AdminContext))]
    partial class AdminContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Areas.Admin.Water.Models.Branch", b =>
                {
                    b.Property<short>("BranchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("BranchId"), 1L, 1);

                    b.Property<string>("BranchName")
                        .IsRequired()
                        .HasColumnType("nchar(50)");

                    b.Property<byte>("CityId")
                        .HasColumnType("tinyint");

                    b.HasKey("BranchId");

                    b.HasIndex("CityId");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("Areas.Admin.Water.Models.City", b =>
                {
                    b.Property<byte>("CityId")
                        .HasColumnType("tinyint");

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("nchar(20)");

                    b.HasKey("CityId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Areas.Admin.Water.Models.User", b =>
                {
                    b.Property<short>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("UserId"), 1L, 1);

                    b.Property<byte>("Authorization")
                        .HasColumnType("tinyint");

                    b.Property<short>("BranchId")
                        .HasColumnType("smallint");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("char(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("char(64)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nchar(50)");

                    b.HasKey("UserId");

                    b.HasIndex("BranchId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Areas.Admin.Water.Models.Branch", b =>
                {
                    b.HasOne("Areas.Admin.Water.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("Areas.Admin.Water.Models.User", b =>
                {
                    b.HasOne("Areas.Admin.Water.Models.Branch", "Branch")
                        .WithMany()
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });
#pragma warning restore 612, 618
        }
    }
}