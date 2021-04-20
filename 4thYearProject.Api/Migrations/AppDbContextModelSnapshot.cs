﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _4thYearProject.Api.Models;

namespace _4thYearProject.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("HashTagPost", b =>
                {
                    b.Property<int>("HashTagsId")
                        .HasColumnType("int");

                    b.Property<int>("PostsPostId")
                        .HasColumnType("int");

                    b.HasKey("HashTagsId", "PostsPostId");

                    b.HasIndex("PostsPostId");

                    b.ToTable("HashTagPost");
                });

            modelBuilder.Entity("_4thYearProject.Shared.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("UserAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserAddress2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCountry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserFName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserLName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPostcode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("_4thYearProject.Shared.Models.BusinessLogic.Order", b =>
                {
                    b.Property<int?>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime?>("DatePlaced")
                        .HasColumnType("datetime2");

                    b.Property<int?>("OrderAddressId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderId");

                    b.HasIndex("OrderAddressId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("_4thYearProject.Shared.Models.BusinessLogic.OrderLineItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("ShoppingCartId")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("PostId");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("LineItems");
                });

            modelBuilder.Entity("_4thYearProject.Shared.Models.BusinessLogic.ShoppingCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("_4thYearProject.Shared.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int?>("ProfileDataId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SubmittedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("ProfileDataId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("_4thYearProject.Shared.Models.FeedProfileData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("FName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FeedData");
                });

            modelBuilder.Entity("_4thYearProject.Shared.Models.Following", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Followed_ID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Follower_ID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Followers");
                });

            modelBuilder.Entity("_4thYearProject.Shared.Models.HashTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Hashtags");
                });

            modelBuilder.Entity("_4thYearProject.Shared.Models.Like", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Post_ID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("User_ID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("_4thYearProject.Shared.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Caption")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<bool>("LicenseEnabled")
                        .HasColumnType("bit");

                    b.Property<double>("LicensePrice")
                        .HasColumnType("float");

                    b.Property<bool>("Liked")
                        .HasColumnType("bit");

                    b.Property<int>("Likes")
                        .HasColumnType("int");

                    b.Property<string>("MimeType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoFile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PostDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("PrintsEnabled")
                        .HasColumnType("bit");

                    b.Property<bool>("ShirtsEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("Thumbnail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PostId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("_4thYearProject.Shared.Models.UserData", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HashTagPost", b =>
                {
                    b.HasOne("_4thYearProject.Shared.Models.HashTag", null)
                        .WithMany()
                        .HasForeignKey("HashTagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_4thYearProject.Shared.Models.Post", null)
                        .WithMany()
                        .HasForeignKey("PostsPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("_4thYearProject.Shared.Models.BusinessLogic.Order", b =>
                {
                    b.HasOne("_4thYearProject.Shared.Models.Address", "OrderAddress")
                        .WithMany()
                        .HasForeignKey("OrderAddressId");

                    b.Navigation("OrderAddress");
                });

            modelBuilder.Entity("_4thYearProject.Shared.Models.BusinessLogic.OrderLineItem", b =>
                {
                    b.HasOne("_4thYearProject.Shared.Models.BusinessLogic.Order", null)
                        .WithMany("LineItems")
                        .HasForeignKey("OrderId");

                    b.HasOne("_4thYearProject.Shared.Models.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_4thYearProject.Shared.Models.BusinessLogic.ShoppingCart", null)
                        .WithMany("BasketItems")
                        .HasForeignKey("ShoppingCartId");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("_4thYearProject.Shared.Models.Comment", b =>
                {
                    b.HasOne("_4thYearProject.Shared.Models.Post", null)
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_4thYearProject.Shared.Models.FeedProfileData", "ProfileData")
                        .WithMany()
                        .HasForeignKey("ProfileDataId");

                    b.Navigation("ProfileData");
                });

            modelBuilder.Entity("_4thYearProject.Shared.Models.UserData", b =>
                {
                    b.HasOne("_4thYearProject.Shared.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("_4thYearProject.Shared.Models.BusinessLogic.Order", b =>
                {
                    b.Navigation("LineItems");
                });

            modelBuilder.Entity("_4thYearProject.Shared.Models.BusinessLogic.ShoppingCart", b =>
                {
                    b.Navigation("BasketItems");
                });

            modelBuilder.Entity("_4thYearProject.Shared.Models.Post", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
