﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TomaszewskiWawrzyniak.MonitoryApp.DAOSQL1;

#nullable disable

namespace TomaszewskiWawrzyniak.MonitoryApp.DAOSQL1.Migrations
{
    [DbContext(typeof(DAO))]
    [Migration("20240130194343_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.1");

            modelBuilder.Entity("TomaszewskiWawrzyniak.MonitoryApp.DAOSQL1.BO.Monitor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<float>("Diagonal")
                        .HasColumnType("REAL");

                    b.Property<int>("Matrix")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ProducerId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProducerId");

                    b.ToTable("Monitors");
                });

            modelBuilder.Entity("TomaszewskiWawrzyniak.MonitoryApp.DAOSQL1.BO.Producer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CountryFrom")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Producers");
                });

            modelBuilder.Entity("TomaszewskiWawrzyniak.MonitoryApp.DAOSQL1.BO.Monitor", b =>
                {
                    b.HasOne("TomaszewskiWawrzyniak.MonitoryApp.DAOSQL1.BO.Producer", "Producer")
                        .WithMany("Monitors")
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producer");
                });

            modelBuilder.Entity("TomaszewskiWawrzyniak.MonitoryApp.DAOSQL1.BO.Producer", b =>
                {
                    b.Navigation("Monitors");
                });
#pragma warning restore 612, 618
        }
    }
}