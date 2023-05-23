﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeleWeb.Data;

#nullable disable

namespace TeleWeb.Data.Migrations
{
    [DbContext(typeof(TeleWebDbContext))]
    [Migration("20230521181953_Fourth")]
    partial class Fourth
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.1.23111.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ChannelAdmin", b =>
                {
                    b.Property<int>("AdminId")
                        .HasColumnType("int");

                    b.Property<int>("ChannelId")
                        .HasColumnType("int");

                    b.HasKey("AdminId", "ChannelId");

                    b.HasIndex("ChannelId");

                    b.ToTable("ChannelAdmin");
                });

            modelBuilder.Entity("TeleWeb.Domain.Models.Channel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PrimaryAdminId")
                        .HasColumnType("int");

                    b.Property<int?>("SubscribersCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PrimaryAdminId");

                    b.ToTable("Channels");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Channel");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("TeleWeb.Domain.Models.MediaFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("MediaFiles");
                });

            modelBuilder.Entity("TeleWeb.Domain.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AdminWhoPostedId")
                        .HasColumnType("int");

                    b.Property<int>("ChannelId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("dislikes")
                        .HasColumnType("int");

                    b.Property<int>("likes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdminWhoPostedId");

                    b.HasIndex("ChannelId");

                    b.HasIndex("PostId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("TeleWeb.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelegramId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("UserChannelSubscription", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("ChannelId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "ChannelId");

                    b.HasIndex("ChannelId");

                    b.ToTable("UserChannelSubscription");
                });

            modelBuilder.Entity("TeleWeb.Domain.Models.TelegramChannel", b =>
                {
                    b.HasBaseType("TeleWeb.Domain.Models.Channel");

                    b.Property<string>("TelegramId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelegramUsername")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("TelegramChannel");
                });

            modelBuilder.Entity("TeleWeb.Domain.Models.Admin", b =>
                {
                    b.HasBaseType("TeleWeb.Domain.Models.User");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("ChannelAdmin", b =>
                {
                    b.HasOne("TeleWeb.Domain.Models.Admin", null)
                        .WithMany()
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TeleWeb.Domain.Models.Channel", null)
                        .WithMany()
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("TeleWeb.Domain.Models.Channel", b =>
                {
                    b.HasOne("TeleWeb.Domain.Models.Admin", "PrimaryAdmin")
                        .WithMany("OwnedChannels")
                        .HasForeignKey("PrimaryAdminId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("PrimaryAdmin");
                });

            modelBuilder.Entity("TeleWeb.Domain.Models.MediaFile", b =>
                {
                    b.HasOne("TeleWeb.Domain.Models.Post", "Post")
                        .WithMany("MediaFiles")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("TeleWeb.Domain.Models.Post", b =>
                {
                    b.HasOne("TeleWeb.Domain.Models.Admin", "AdminWhoPosted")
                        .WithMany("Posts")
                        .HasForeignKey("AdminWhoPostedId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TeleWeb.Domain.Models.Channel", "Channel")
                        .WithMany("Posts")
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeleWeb.Domain.Models.Post", null)
                        .WithMany("Comments")
                        .HasForeignKey("PostId");

                    b.Navigation("AdminWhoPosted");

                    b.Navigation("Channel");
                });

            modelBuilder.Entity("UserChannelSubscription", b =>
                {
                    b.HasOne("TeleWeb.Domain.Models.Channel", null)
                        .WithMany()
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TeleWeb.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("TeleWeb.Domain.Models.Channel", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("TeleWeb.Domain.Models.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("MediaFiles");
                });

            modelBuilder.Entity("TeleWeb.Domain.Models.Admin", b =>
                {
                    b.Navigation("OwnedChannels");

                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
