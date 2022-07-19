﻿// <auto-generated />
using GachaBot.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GachaBot.Migrations
{
    [DbContext(typeof(DBEntities))]
    [Migration("20220719080737_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.7");

            modelBuilder.Entity("GachaBot.Database.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Coins")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RequestName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RequestedItem")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<ulong>("UId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Request");
                });

            modelBuilder.Entity("GachaBot.Database.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Coins")
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("UId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}
