﻿// <auto-generated />
using System;
using Data_Access.DataContext_Class;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data_Access.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220321162538_someTablesAddingToDatabase")]
    partial class someTablesAddingToDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Business_Core.Entities.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"), 1L, 1);

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Created_At")
                        .HasColumnType("datetime2");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Business_Core.Entities.DynamicFormStructure", b =>
                {
                    b.Property<int>("DynamicFormStructureID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DynamicFormStructureID"), 1L, 1);

                    b.Property<DateTime?>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("FormStructure")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NestCategoryId")
                        .HasColumnType("int");

                    b.HasKey("DynamicFormStructureID");

                    b.ToTable("DynamicFormStructures");
                });

            modelBuilder.Entity("Business_Core.Entities.NestSubCategory", b =>
                {
                    b.Property<int>("NestSubCategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NestSubCategoryID"), 1L, 1);

                    b.Property<DateTime?>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<int>("DynamicFormStructureId")
                        .HasColumnType("int");

                    b.Property<string>("NestSubCategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubCategoryId")
                        .HasColumnType("int");

                    b.HasKey("NestSubCategoryID");

                    b.HasIndex("DynamicFormStructureId")
                        .IsUnique();

                    b.HasIndex("SubCategoryId");

                    b.ToTable("NestSubCategories");
                });

            modelBuilder.Entity("Business_Core.Entities.ProductBrand", b =>
                {
                    b.Property<int>("ProductBrandID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductBrandID"), 1L, 1);

                    b.Property<string>("BrandName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<int>("NestSubCategoryId")
                        .HasColumnType("int");

                    b.HasKey("ProductBrandID");

                    b.HasIndex("NestSubCategoryId");

                    b.ToTable("ProductBrands");
                });

            modelBuilder.Entity("Business_Core.Entities.SubCategory", b =>
                {
                    b.Property<int>("SubCategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubCategoryID"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("SubCategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubCategoryID");

                    b.HasIndex("CategoryId");

                    b.ToTable("SubCategories");
                });

            modelBuilder.Entity("Business_Core.Entities.NestSubCategory", b =>
                {
                    b.HasOne("Business_Core.Entities.DynamicFormStructure", "DynamicFormStructure")
                        .WithOne("NestSubCategory")
                        .HasForeignKey("Business_Core.Entities.NestSubCategory", "DynamicFormStructureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Business_Core.Entities.SubCategory", "SubCategory")
                        .WithMany("NestSubCategories")
                        .HasForeignKey("SubCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DynamicFormStructure");

                    b.Navigation("SubCategory");
                });

            modelBuilder.Entity("Business_Core.Entities.ProductBrand", b =>
                {
                    b.HasOne("Business_Core.Entities.NestSubCategory", "NestSubCategory")
                        .WithMany("ProductBrands")
                        .HasForeignKey("NestSubCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NestSubCategory");
                });

            modelBuilder.Entity("Business_Core.Entities.SubCategory", b =>
                {
                    b.HasOne("Business_Core.Entities.Category", "Category")
                        .WithMany("SubCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Business_Core.Entities.Category", b =>
                {
                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("Business_Core.Entities.DynamicFormStructure", b =>
                {
                    b.Navigation("NestSubCategory");
                });

            modelBuilder.Entity("Business_Core.Entities.NestSubCategory", b =>
                {
                    b.Navigation("ProductBrands");
                });

            modelBuilder.Entity("Business_Core.Entities.SubCategory", b =>
                {
                    b.Navigation("NestSubCategories");
                });
#pragma warning restore 612, 618
        }
    }
}