﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TestPatient.Data;

#nullable disable

namespace TestPatient.Migrations
{
    [DbContext(typeof(HangfireContext))]
    [Migration("20230718070357_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TestPatient.Models.LogModel", b =>
                {
                    b.Property<string>("PatientId")
                        .HasColumnType("text");

                    b.Property<string>("CreatedAt")
                        .HasColumnType("text");

                    b.Property<string>("CriticalStatus")
                        .HasColumnType("text");

                    b.Property<string>("ErrorState")
                        .HasColumnType("text");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<string>("ReceivedAt")
                        .HasColumnType("text");

                    b.Property<string>("ServerId")
                        .HasColumnType("text");

                    b.Property<string>("ServiceName")
                        .HasColumnType("text");

                    b.Property<string>("ServiceType")
                        .HasColumnType("text");

                    b.HasKey("PatientId");

                    b.ToTable("PatientLogs");
                });
#pragma warning restore 612, 618
        }
    }
}