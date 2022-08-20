﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SqLiteWithLinqDataProvider;

#nullable disable

namespace SqLiteWithLinqDataProvider.Migrations
{
    [DbContext(typeof(NetphonebookContext))]
    [Migration("20220819205909_revert")]
    partial class revert
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.7");

            modelBuilder.Entity("NetPhonebook.Core.Models.ExtraCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ExtraCategories");
                });

            modelBuilder.Entity("NetPhonebook.Core.Models.ExtraInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ExtraCategoryId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ExtraCategoryId");

                    b.ToTable("ExtraInfos");
                });

            modelBuilder.Entity("NetPhonebook.Core.Models.FavouriteColor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("HexColor")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("favouriteColors");
                });

            modelBuilder.Entity("NetPhonebook.Core.Models.VirtualModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("virtualModels");
                });

            modelBuilder.Entity("NetPhonebook.Core.Models.VirtualModelsCellData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<sbyte>("CellId")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("CellType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstText")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("MainDataId")
                        .HasColumnType("TEXT");

                    b.Property<string>("SecondText")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("extraInfoId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MainDataId");

                    b.HasIndex("extraInfoId");

                    b.ToTable("virtualModelsCellDatas");
                });

            modelBuilder.Entity("NetPhonebook.Core.Models.VirtualModelsCustomization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("BackgroundColor")
                        .HasColumnType("TEXT");

                    b.Property<string>("BorderColor")
                        .HasColumnType("TEXT");

                    b.Property<string>("BorderSize")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("TEXT");

                    b.Property<byte>("CellId")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("CellType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CornerRadius")
                        .HasColumnType("TEXT");

                    b.Property<string>("FontSize")
                        .HasColumnType("TEXT");

                    b.Property<string>("ForegroundColor")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ModelId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ModelId");

                    b.ToTable("virtualModelsCustomizations");
                });

            modelBuilder.Entity("NetPhonebook.Core.Models.VirtualModelsData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("DisplayedNumber")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ModelBaseId")
                        .HasColumnType("TEXT");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ModelBaseId");

                    b.ToTable("virtualModelsDatas");
                });

            modelBuilder.Entity("NetPhonebook.Core.Models.ExtraInfo", b =>
                {
                    b.HasOne("NetPhonebook.Core.Models.ExtraCategory", "ExtraCategory")
                        .WithMany("ExtraInfos")
                        .HasForeignKey("ExtraCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExtraCategory");
                });

            modelBuilder.Entity("NetPhonebook.Core.Models.VirtualModelsCellData", b =>
                {
                    b.HasOne("NetPhonebook.Core.Models.VirtualModelsData", "MainData")
                        .WithMany("CellDatas")
                        .HasForeignKey("MainDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NetPhonebook.Core.Models.ExtraInfo", "extraInfo")
                        .WithMany()
                        .HasForeignKey("extraInfoId");

                    b.Navigation("MainData");

                    b.Navigation("extraInfo");
                });

            modelBuilder.Entity("NetPhonebook.Core.Models.VirtualModelsCustomization", b =>
                {
                    b.HasOne("NetPhonebook.Core.Models.ExtraCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("NetPhonebook.Core.Models.VirtualModel", "Model")
                        .WithMany("CustomizationCells")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Model");
                });

            modelBuilder.Entity("NetPhonebook.Core.Models.VirtualModelsData", b =>
                {
                    b.HasOne("NetPhonebook.Core.Models.VirtualModel", "ModelBase")
                        .WithMany()
                        .HasForeignKey("ModelBaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ModelBase");
                });

            modelBuilder.Entity("NetPhonebook.Core.Models.ExtraCategory", b =>
                {
                    b.Navigation("ExtraInfos");
                });

            modelBuilder.Entity("NetPhonebook.Core.Models.VirtualModel", b =>
                {
                    b.Navigation("CustomizationCells");
                });

            modelBuilder.Entity("NetPhonebook.Core.Models.VirtualModelsData", b =>
                {
                    b.Navigation("CellDatas");
                });
#pragma warning restore 612, 618
        }
    }
}
