﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParkingControl.Data;

#nullable disable

namespace ParkingControl.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240901050222_addColumnsPriceAndPriceToPay")]
    partial class addColumnsPriceAndPriceToPay
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("ParkingControl.ParkingRegistration.ParkingRegistration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CheckInDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CheckOutDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("DurationInSeconds")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Plate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("PriceToPay")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ParkingRegistrations");
                });

            modelBuilder.Entity("ParkingControl.PriceTable.PriceTable", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("AdditionalHourlyValue")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("InitialHourValue")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ValidityFinalPeriod")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ValidityStartPeriod")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("PricesTable");
                });
#pragma warning restore 612, 618
        }
    }
}
