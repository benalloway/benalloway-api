﻿// <auto-generated />
using Api.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(TodoDb))]
    partial class TodoDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.5");

            modelBuilder.Entity("Api.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("StatusId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Api.Models.ItemStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ItemStatuses");
                });

            modelBuilder.Entity("Api.Models.Item", b =>
                {
                    b.HasOne("Api.Models.ItemStatus", "Status")
                        .WithMany("Items")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Api.Models.ItemStatus", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
