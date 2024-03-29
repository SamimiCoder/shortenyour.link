﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using shortenyour.link.Data;

#nullable disable

namespace shortenyour.link.Migrations.Link
{
    [DbContext(typeof(LinkContext))]
    [Migration("20230127153209_linkmigration")]
    partial class linkmigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("shortenyour.link.Models.Link", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Click_Count")
                        .HasColumnType("int");

                    b.Property<decimal>("LinkBalance")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("LinkUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("OwnerMail")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("originalUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Links");
                });
#pragma warning restore 612, 618
        }
    }
}
