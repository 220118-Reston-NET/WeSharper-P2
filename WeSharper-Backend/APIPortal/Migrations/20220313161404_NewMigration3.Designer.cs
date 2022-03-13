﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeSharper.Models;

#nullable disable

namespace APIPortal.Migrations
{
    [DbContext(typeof(WeSharperContext))]
    [Migration("20220313161404_NewMigration3")]
    partial class NewMigration3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WeSharper.Models.MessageConnection", b =>
                {
                    b.Property<string>("ConnectionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MessageGroupName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ConnectionId");

                    b.HasIndex("MessageGroupName");

                    b.ToTable("MessageConnections");
                });

            modelBuilder.Entity("WeSharper.Models.MessageGroup", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.ToTable("MessageGroups");
                });

            modelBuilder.Entity("WeSharper.Models.MessageConnection", b =>
                {
                    b.HasOne("WeSharper.Models.MessageGroup", null)
                        .WithMany("MessageConnections")
                        .HasForeignKey("MessageGroupName")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WeSharper.Models.MessageGroup", b =>
                {
                    b.Navigation("MessageConnections");
                });
#pragma warning restore 612, 618
        }
    }
}
