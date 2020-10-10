﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SystemRequirement.API.Data;

namespace SystemRequirement.API.Migrations
{
    [DbContext(typeof(SysReqDbContext))]
    partial class SysReqDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SystemRequirement.API.Models.CPUNeeds", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AMDType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IntelType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("CPUNeeds");
                });

            modelBuilder.Entity("SystemRequirement.API.Models.GPUNeeds", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AMDType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NVIDIAType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("GPUNeeds");
                });

            modelBuilder.Entity("SystemRequirement.API.Models.NetworkNeeds", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Bandwidth")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("NetworkNeeds");
                });

            modelBuilder.Entity("SystemRequirement.API.Models.RAMNeeds", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("RAMNeeds");
                });

            modelBuilder.Entity("SystemRequirement.API.Models.StorageNeeds", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("StorageNeeds");
                });

            modelBuilder.Entity("SystemRequirement.API.Models.SysReq", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CPUID")
                        .HasColumnType("int");

                    b.Property<int>("GPUID")
                        .HasColumnType("int");

                    b.Property<int>("NetworkID")
                        .HasColumnType("int");

                    b.Property<string>("ProductID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RAMID")
                        .HasColumnType("int");

                    b.Property<int>("StorageID")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("CPUID");

                    b.HasIndex("GPUID");

                    b.HasIndex("NetworkID");

                    b.HasIndex("RAMID");

                    b.HasIndex("StorageID");

                    b.ToTable("SysReqs");
                });

            modelBuilder.Entity("SystemRequirement.API.Models.SysReq", b =>
                {
                    b.HasOne("SystemRequirement.API.Models.CPUNeeds", "CPU")
                        .WithMany()
                        .HasForeignKey("CPUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SystemRequirement.API.Models.GPUNeeds", "GPU")
                        .WithMany()
                        .HasForeignKey("GPUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SystemRequirement.API.Models.NetworkNeeds", "Network")
                        .WithMany()
                        .HasForeignKey("NetworkID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SystemRequirement.API.Models.RAMNeeds", "RAM")
                        .WithMany()
                        .HasForeignKey("RAMID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SystemRequirement.API.Models.StorageNeeds", "Storage")
                        .WithMany()
                        .HasForeignKey("StorageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}