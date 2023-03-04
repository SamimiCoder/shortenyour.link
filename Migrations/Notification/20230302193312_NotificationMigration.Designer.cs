﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using shortenyour.link.Data;

#nullable disable

namespace shortenyour.link.Migrations.Notification
{
    [DbContext(typeof(NotificationContext))]
    [Migration("20230302193312_NotificationMigration")]
    partial class NotificationMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("shortenyour.link.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CryptoWalletCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NotificationCategory")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NotificationText")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Notifications");
                });
#pragma warning restore 612, 618
        }
    }
}