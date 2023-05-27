﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MvcBiketripsMay2021.Data;

#nullable disable

namespace MVCCitybike.Migrations.MvcBiketripsMay2021
{
    [DbContext(typeof(MvcBiketripsMay2021Context))]
    [Migration("20230527072343_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("MVCCitybike.Models.BiketripsMay2021", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Covered_distance_m")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Departure")
                        .HasColumnType("TEXT");

                    b.Property<int>("Departure_station_id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Departure_station_name")
                        .HasColumnType("TEXT");

                    b.Property<int>("Duration_sec")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Return")
                        .HasColumnType("TEXT");

                    b.Property<int>("Return_station_id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Return_station_name")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("BiketripsMay2021");
                });
#pragma warning restore 612, 618
        }
    }
}
