﻿// <auto-generated />
using System;
using DatabaseMonitoring.Services.Workspace.Infrustructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Workspace.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230818095744_Migration1")]
    partial class Migration1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DatabaseMonitoring.Services.Workspace.Core.Domain.Entity.Server", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("OuterId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("WorkspaceId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("WorkspaceId");

                    b.ToTable("Servers");
                });

            modelBuilder.Entity("DatabaseMonitoring.Services.Workspace.Core.Domain.Entity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("OuterId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("WorkspaceId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("WorkspaceId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DatabaseMonitoring.Services.Workspace.Core.Domain.Entity.WorkspaceEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Workspaces");
                });

            modelBuilder.Entity("DatabaseMonitoring.Services.Workspace.Core.Domain.Entity.Server", b =>
                {
                    b.HasOne("DatabaseMonitoring.Services.Workspace.Core.Domain.Entity.WorkspaceEntity", "Workspace")
                        .WithMany("Servers")
                        .HasForeignKey("WorkspaceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Workspace");
                });

            modelBuilder.Entity("DatabaseMonitoring.Services.Workspace.Core.Domain.Entity.User", b =>
                {
                    b.HasOne("DatabaseMonitoring.Services.Workspace.Core.Domain.Entity.WorkspaceEntity", "Workspace")
                        .WithMany("Users")
                        .HasForeignKey("WorkspaceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Workspace");
                });

            modelBuilder.Entity("DatabaseMonitoring.Services.Workspace.Core.Domain.Entity.WorkspaceEntity", b =>
                {
                    b.Navigation("Servers");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
