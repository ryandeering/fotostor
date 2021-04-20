using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _4thYearProject.Api.Models;
using _4thYearProject.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace _4thYearProject.Api.DataSeeding
{

public class DbInitializer : IDbInitializer
{
    private readonly IServiceScopeFactory _scopeFactory;

    public DbInitializer(IServiceScopeFactory scopeFactory)
    {
        this._scopeFactory = scopeFactory;
    }

    public void Initialize()
    {
        using (var serviceScope = _scopeFactory.CreateScope())
        {
            using (var context = serviceScope.ServiceProvider.GetService<AppDbContext>())
            {
                context.Database.Migrate();
            }
        }
    }

    public void SeedData()
    {
        using (var serviceScope = _scopeFactory.CreateScope())
        {
            using (var context = serviceScope.ServiceProvider.GetService<AppDbContext>())
            {

                //add admin user
                if (!context.Users.Any())
                {

                    var hashTag1 = new HashTag
                    {
                        Content = "#glitch"
                    };


                    var hashTag2 = new HashTag
                    {
                        Content = "#art"
                    };

                    var hashTag3 = new HashTag
                    {
                        Content = "#urbex"
                    };

                    var hashTag4 = new HashTag
                    {
                        Content = "#street"
                    };

                    var hashTag5 = new HashTag
                    {
                        Content = "#photography"
                    };

                    var hashTag6 = new HashTag
                    {
                        Content = "#music"
                    };



                        context.Hashtags.Add(hashTag1);
                        context.Hashtags.Add(hashTag2);
                        context.Hashtags.Add(hashTag3);
                        context.Hashtags.Add(hashTag4);
                        context.Hashtags.Add(hashTag5);


                        var newUser =
                        new UserData
                        {
                            Id = "f3b8cafa-9bd2-4987-bb0d-1a229911e007",
                            FirstName = "Jarid",
                            SecondName = "Scott",
                            DisplayName = "jrdsctt",
                            Email = "r.yandeering1@gmail.com",
                            Bio = "Glitch artist from Utah.",
                            ProfilePic =
                                "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/f3b8cafa-9bd2-4987-bb0d-1a229911e007168.JPEG?generation=1618677765473892&alt=media",
                            Address = new Address()
                            {
                                UserPostcode = "\t\r\n84107-4813",
                                UserAddress = "5207 South State Street",
                                UserCity = "Murray",
                                UserCountry = "United States",
                                UserAddress2 = "",
                                UserFName = "Jarid",
                                UserLName = "Scott"
                            }
                        };
                   context.Users.Add(newUser);


                   var jrdPost1 = new Post
                   { 
                       UserId = "f3b8cafa-9bd2-4987-bb0d-1a229911e007",
                       PhotoFile =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/f3b8cafa-9bd2-4987-bb0d-1a229911e007_0177.JPEG?generation=1618680266330198&alt=media",
                       Thumbnail =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/f3b8cafa-9bd2-4987-bb0d-1a229911e007_0177T.JPEG?generation=1618680266884098&alt=media",
                       MimeType = "image/jpeg",
                       Caption = "Based skull. #glitch",
                       LicenseEnabled = true,
                       PrintsEnabled = true,
                       ShirtsEnabled = true,
                       LicensePrice = 50,
                       PostDeleted = false,
                       UploadDate = Convert.ToDateTime("2021-04-17 18:24:24.1900000"),
                       HashTags = {hashTag1}
                   };


                   context.Posts.Add(jrdPost1);

                   var jrdPost2 = new Post
                   {
                       UserId = "f3b8cafa-9bd2-4987-bb0d-1a229911e007",
                       PhotoFile =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/f3b8cafa-9bd2-4987-bb0d-1a229911e007_0105.JPEG?generation=1618680242872540&alt=media",
                       Thumbnail =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/f3b8cafa-9bd2-4987-bb0d-1a229911e007_0105T.JPEG?generation=1618680243423726&alt=media",
                       MimeType = "image/jpeg",
                       Caption = "Glitch girl. #glitch",
                       LicenseEnabled = true,
                       PrintsEnabled = true,
                       ShirtsEnabled = true,
                       LicensePrice = 50,
                       PostDeleted = false,
                       UploadDate = Convert.ToDateTime("2021-04-17 18:24:01.1770000"),
                       HashTags = { hashTag1 }
                   };


                   context.Posts.Add(jrdPost2);


                   var jrdPost3 = new Post
                   {
                       UserId = "f3b8cafa-9bd2-4987-bb0d-1a229911e007",
                       PhotoFile =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/f3b8cafa-9bd2-4987-bb0d-1a229911e007_0104.JPEG?generation=1618680223538682&alt=media",
                       Thumbnail =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/f3b8cafa-9bd2-4987-bb0d-1a229911e007_0104T.JPEG?generation=1618680223967556&alt=media",
                       MimeType = "image/jpeg",
                       Caption = "Sun. #glitch",
                       LicenseEnabled = true,
                       PrintsEnabled = true,
                       ShirtsEnabled = true,
                       LicensePrice = 50,
                       PostDeleted = false,
                       UploadDate = Convert.ToDateTime("2021-04-17 18:23:42.3200000"),
                       HashTags = { hashTag1 }
                   };


                   context.Posts.Add(jrdPost3);



                   var jrdPost4 = new Post
                   {
                       UserId = "f3b8cafa-9bd2-4987-bb0d-1a229911e007",
                       PhotoFile =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/f3b8cafa-9bd2-4987-bb0d-1a229911e007_0144.JPEG?generation=1618680109034529&alt=media",
                       Thumbnail =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/f3b8cafa-9bd2-4987-bb0d-1a229911e007_0144T.JPEG?generation=1618680109602487&alt=media",
                       MimeType = "image/jpeg",
                       Caption = "Man. #glitch",
                       LicenseEnabled = true,
                       PrintsEnabled = true,
                       ShirtsEnabled = true,
                       LicensePrice = 50,
                       PostDeleted = false,
                       UploadDate = Convert.ToDateTime("2021-04-17 18:21:47.4670000"),
                       HashTags = { hashTag1 }
                   };


                   context.Posts.Add(jrdPost4);


                   var jrdPost5 = new Post
                   {
                       UserId = "f3b8cafa-9bd2-4987-bb0d-1a229911e007",
                       PhotoFile =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/f3b8cafa-9bd2-4987-bb0d-1a229911e007_0144.JPEG?generation=1618680109034529&alt=media",
                       Thumbnail =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/f3b8cafa-9bd2-4987-bb0d-1a229911e007_0144T.JPEG?generation=1618680109602487&alt=media",
                       MimeType = "image/jpeg",
                       Caption = "Boat. #glitch",
                       LicenseEnabled = true,
                       PrintsEnabled = true,
                       ShirtsEnabled = true,
                       LicensePrice = 50,
                       PostDeleted = false,
                       UploadDate = Convert.ToDateTime("2021-04-17 18:20:43.7600000"),
                       HashTags = { hashTag1 }
                   };


                   context.Posts.Add(jrdPost5);


                   var jrdPost6 = new Post
                   {
                       UserId = "f3b8cafa-9bd2-4987-bb0d-1a229911e007",
                       PhotoFile =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/f3b8cafa-9bd2-4987-bb0d-1a229911e007_0171.JPEG?generation=1618680024773497&alt=media",
                       Thumbnail =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/f3b8cafa-9bd2-4987-bb0d-1a229911e007_0171T.JPEG?generation=1618680025273906&alt=media",
                       MimeType = "image/jpeg",
                       Caption = "Man. #glitch",
                       LicenseEnabled = true,
                       PrintsEnabled = true,
                       ShirtsEnabled = true,
                       LicensePrice = 50,
                       PostDeleted = false,
                       UploadDate = Convert.ToDateTime("2021-04-17 18:20:23.5300000"),
                       HashTags = { hashTag1 }
                   };


                   context.Posts.Add(jrdPost6);



                        var newUser2 =
                       new UserData
                       {
                           Id = "7d6e087e-f7b6-4b58-b6ec-3672b7a41810",
                           FirstName = "Ryan",
                           SecondName = "Deering",
                           DisplayName = "shinto29",
                           Email = "ryandeering1@gmail.com",
                           Bio = "Analog photographer from Dublin.",
                           ProfilePic = "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/e0720e90-a0b9-43ae-8b61-84777cab9be9190.JPEG?generation=1616551099533747&alt=media",
                               Address = new Address() {
                               UserPostcode = "D18 P521",
                               UserAddress = "One Microsoft Place, South County Business Park",
                               UserCity = "Dublin",
                               UserCountry = "Ireland",
                               UserAddress2 = "Leopardstown",
                               UserFName = "Ryan",
                               UserLName = "Deering"
                           }
                       };
                   context.Users.Add(newUser2);


                   var newUser3 =
                       new UserData
                       {
                           Id = "d6ee504c-81c4-4490-9d01-d60b88977274",
                           FirstName = "Klara",
                           SecondName = "Ahman",
                           DisplayName = "saintjulie",
                           Email = "ry.andeering1@gmail.com",
                           Bio = "Photographer from Belfast.",
                           ProfilePic = "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/d6ee504c-81c4-4490-9d01-d60b88977274136.JPEG?generation=1618677712128764&alt=media",
                           Address = new Address()
                           {
                               UserPostcode = "BT1 5GS",
                               UserAddress = "Donegall Square N",
                               UserCity = "Belfast",
                               UserCountry = "Northern Ireland",
                               UserAddress2 = "",
                               UserFName = "Klara",
                               UserLName = "Ahman"
                           }
                       };
                   context.Users.Add(newUser3);

                        var newUser4 =
                            new UserData
                            {
                                Id = "ad2f4d7f-1a8a-4fa1-afb0-defd2976bd9c",
                                FirstName = "Andrew",
                                SecondName = "",
                                DisplayName = "oscillik",
                                Email = "rya.ndeering1@gmail.com",
                                Bio = "Photographer from Belfast.",
                                ProfilePic = "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/ad2f4d7f-1a8a-4fa1-afb0-defd2976bd9c188.JPEG?generation=1618948633840672&alt=media",
                                Address = new Address(){}
                       };
                   context.Users.Add(newUser4);

                   var newUser5 =
                       new UserData
                       {
                           Id = "5df6a9d8-5492-4247-a860-38c2c2621887",
                           FirstName = "João",
                           SecondName = "",
                           DisplayName = "andlosotkut",
                           Email = "ryan.deering1@gmail.com",
                           Bio = "Artist and photographer from Porto, Portugal.",
                           ProfilePic = "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/35907067-a305-4d96-9a44-6996925058cd147.JPEG?generation=1618952489782818&alt=media",
                           Address = new Address() { }
                       };
                   context.Users.Add(newUser5);



                        var andyPost1 = new Post
                   {
                       UserId = "ad2f4d7f-1a8a-4fa1-afb0-defd2976bd9c",
                       PhotoFile =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/ad2f4d7f-1a8a-4fa1-afb0-defd2976bd9c_0144.JPEG?generation=1618948707834009&alt=media",
                       Thumbnail =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/ad2f4d7f-1a8a-4fa1-afb0-defd2976bd9c_0144T.JPEG?generation=1618948708161519&alt=media",
                       MimeType = "image/jpeg",
                       Caption = "#art",
                       LicenseEnabled = false,
                       PrintsEnabled = true,
                       ShirtsEnabled = false,
                       PostDeleted = false,
                       UploadDate = Convert.ToDateTime("2021-04-20 19:58:26.7650000"),
                       HashTags = { hashTag2 }
                   };


                   context.Posts.Add(andyPost1);


                   var andyPost2 = new Post
                   {
                       UserId = "ad2f4d7f-1a8a-4fa1-afb0-defd2976bd9c",
                       PhotoFile =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/ad2f4d7f-1a8a-4fa1-afb0-defd2976bd9c_0172.JPEG?generation=1618948838892897&alt=mediahttps://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/ad2f4d7f-1a8a-4fa1-afb0-defd2976bd9c_0172.JPEG?generation=1618948838892897&alt=media",
                       Thumbnail =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/ad2f4d7f-1a8a-4fa1-afb0-defd2976bd9c_0172T.JPEG?generation=1618948839263877&alt=media",
                       MimeType = "image/jpeg",
                       Caption = "#urbex",
                       LicenseEnabled = false,
                       PrintsEnabled = true,
                       ShirtsEnabled = false,
                       PostDeleted = false,
                       UploadDate = Convert.ToDateTime("2021-04-20 20:00:37.6730000"),
                       HashTags = { hashTag3 }
                   };


                   context.Posts.Add(andyPost2);



                   var andyPost3 = new Post
                   {
                       UserId = "ad2f4d7f-1a8a-4fa1-afb0-defd2976bd9c",
                       PhotoFile =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/ad2f4d7f-1a8a-4fa1-afb0-defd2976bd9c_0158.JPEG?generation=1618948880669231&alt=media",
                       Thumbnail =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/ad2f4d7f-1a8a-4fa1-afb0-defd2976bd9c_0158T.JPEG?generation=1618948880984972&alt=media",
                       MimeType = "image/jpeg",
                       Caption = "#street #photography",
                       LicenseEnabled = false,
                       PrintsEnabled = false,
                       ShirtsEnabled = true,
                       PostDeleted = false,
                       UploadDate = Convert.ToDateTime("2021-04-20 20:01:19.7680000"),
                       HashTags = { hashTag4, hashTag5 }
                   };


                   context.Posts.Add(andyPost3);



                   var andyPost4 = new Post
                   {
                       UserId = "ad2f4d7f-1a8a-4fa1-afb0-defd2976bd9c",
                       PhotoFile =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/ad2f4d7f-1a8a-4fa1-afb0-defd2976bd9c_0187.JPEG?generation=1618948905385827&alt=media",
                       Thumbnail =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/ad2f4d7f-1a8a-4fa1-afb0-defd2976bd9c_0187T.JPEG?generation=1618948905675702&alt=media",
                       MimeType = "image/jpeg",
                       Caption = "#music",
                       LicenseEnabled = false,
                       PrintsEnabled = false,
                       ShirtsEnabled = false,
                       PostDeleted = false,
                       UploadDate = Convert.ToDateTime("2021-04-20 20:01:43.6390000"),
                       HashTags = { hashTag6 }
                   };


                   context.Posts.Add(andyPost4);


                   var klaraPost1 = new Post
                   {
                       UserId = "35907067-a305-4d96-9a44-6996925058cd",
                       PhotoFile =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/35907067-a305-4d96-9a44-6996925058cd_0169.JPEG?generation=1618951360040946&alt=media",
                       Thumbnail =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/35907067-a305-4d96-9a44-6996925058cd_0169T.JPEG?generation=1618951360377407&alt=media",
                       MimeType = "image/png",
                       Caption = "",
                       LicenseEnabled = false,
                       PrintsEnabled = false,
                       ShirtsEnabled = false,
                       PostDeleted = false,
                       UploadDate = Convert.ToDateTime("2021-04-20 20:42:36.1500000"),
                       HashTags = {}
                   };


                   context.Posts.Add(klaraPost1);



                   var klaraPost2 = new Post
                   {
                       UserId = "35907067-a305-4d96-9a44-6996925058cd",
                       PhotoFile =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/35907067-a305-4d96-9a44-6996925058cd_0138.JPEG?generation=1618951371015072&alt=media",
                       Thumbnail =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/35907067-a305-4d96-9a44-6996925058cd_0138T.JPEG?generation=1618951371338213&alt=media",
                       MimeType = "image/png",
                       Caption = "",
                       LicenseEnabled = false,
                       PrintsEnabled = false,
                       ShirtsEnabled = false,
                       PostDeleted = false,
                       UploadDate = Convert.ToDateTime("2021-04-20 20:42:49.5130000"),
                       HashTags = {}
                   };


                   context.Posts.Add(klaraPost2);



                   var klaraPost3 = new Post
                   {
                       UserId = "35907067-a305-4d96-9a44-6996925058cd",
                       PhotoFile =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/35907067-a305-4d96-9a44-6996925058cd_0127.JPEG?generation=1618951380376374&alt=media",
                       Thumbnail =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/35907067-a305-4d96-9a44-6996925058cd_0127T.JPEG?generation=1618951380633388&alt=media",
                       MimeType = "image/png",
                       Caption = "",
                       LicenseEnabled = false,
                       PrintsEnabled = false,
                       ShirtsEnabled = false,
                       PostDeleted = false,
                       UploadDate = Convert.ToDateTime("2021-04-20 20:42:58.0910000"),
                       HashTags = { }
                   };


                   context.Posts.Add(klaraPost3);


                   var klaraPost4 = new Post
                   {
                       UserId = "35907067-a305-4d96-9a44-6996925058cd",
                       PhotoFile =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/35907067-a305-4d96-9a44-6996925058cd_0114.JPEG?generation=1618951389425554&alt=media",
                       Thumbnail =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/35907067-a305-4d96-9a44-6996925058cd_0114T.JPEG?generation=1618951389692870&alt=media",
                       MimeType = "image/png",
                       Caption = "",
                       LicenseEnabled = false,
                       PrintsEnabled = false,
                       ShirtsEnabled = false,
                       PostDeleted = false,
                       UploadDate = Convert.ToDateTime("2021-04-20 20:43:07.7150000"),
                       HashTags = { }
                   };


                   context.Posts.Add(klaraPost4);


                   var klaraPost5 = new Post
                   {
                       UserId = "35907067-a305-4d96-9a44-6996925058cd",
                       PhotoFile =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/35907067-a305-4d96-9a44-6996925058cd_0158.JPEG?generation=1618951401745998&alt=media",
                       Thumbnail =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/35907067-a305-4d96-9a44-6996925058cd_0158T.JPEG?generation=1618951401997566&alt=media",
                       MimeType = "image/png",
                       Caption = "",
                       LicenseEnabled = false,
                       PrintsEnabled = false,
                       ShirtsEnabled = false,
                       PostDeleted = false,
                       UploadDate = Convert.ToDateTime("2021-04-20 20:43:19.9190000"),
                       HashTags = { }
                   };


                   context.Posts.Add(klaraPost5);



                   var klaraPost6 = new Post
                   {
                       UserId = "35907067-a305-4d96-9a44-6996925058cd",
                       PhotoFile =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/35907067-a305-4d96-9a44-6996925058cd_0120.JPEG?generation=1618951411205295&alt=media",
                       Thumbnail =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/35907067-a305-4d96-9a44-6996925058cd_0120T.JPEG?generation=1618951411452377&alt=media",
                       MimeType = "image/png",
                       Caption = "",
                       LicenseEnabled = false,
                       PrintsEnabled = false,
                       ShirtsEnabled = false,
                       PostDeleted = false,
                       UploadDate = Convert.ToDateTime("2021-04-20 20:43:28.8220000"),
                       HashTags = { }
                   };


                   context.Posts.Add(klaraPost6);

                   var joaoPost1 = new Post
                   {
                       UserId = "5df6a9d8-5492-4247-a860-38c2c2621887",
                       PhotoFile =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/35907067-a305-4d96-9a44-6996925058cd_0190.JPEG?generation=1618952616376829&alt=media",
                       Thumbnail =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/35907067-a305-4d96-9a44-6996925058cd_0190T.JPEG?generation=1618952616723246&alt=media",
                       MimeType = "image/jpeg",
                       Caption = "",
                       LicenseEnabled = true,
                       PrintsEnabled = true,
                       ShirtsEnabled = false,
                       PostDeleted = false,
                       UploadDate = Convert.ToDateTime("2021-04-20 21:03:32.9360000"),
                       HashTags = { }
                   };


                   context.Posts.Add(joaoPost1);

                   var joaoPost2 = new Post
                   {
                       UserId = "5df6a9d8-5492-4247-a860-38c2c2621887",
                       PhotoFile =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/35907067-a305-4d96-9a44-6996925058cd_0163.JPEG?generation=1618952632224880&alt=media",
                       Thumbnail =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/35907067-a305-4d96-9a44-6996925058cd_0163T.JPEG?generation=1618952632616371&alt=media",
                       MimeType = "image/jpeg",
                       Caption = "",
                       LicenseEnabled = true,
                       PrintsEnabled = true,
                       ShirtsEnabled = false,
                       PostDeleted = false,
                       UploadDate = Convert.ToDateTime("2021-04-20 21:03:48.6450000"),
                       HashTags = { }
                   };


                   context.Posts.Add(joaoPost2);

                   var joaoPost3 = new Post
                   {
                       UserId = "5df6a9d8-5492-4247-a860-38c2c2621887",
                       PhotoFile = "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/35907067-a305-4d96-9a44-6996925058cd_0161.JPEG?generation=1618952675784198&alt=media",
                       Thumbnail =
                           "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/35907067-a305-4d96-9a44-6996925058cd_0161T.JPEG?generation=1618952676100511&alt=media",
                       MimeType = "image/jpeg",
                       Caption = "",
                       LicenseEnabled = true,
                       PrintsEnabled = true,
                       ShirtsEnabled = false,
                       PostDeleted = false,
                       UploadDate = Convert.ToDateTime("2021-04-20 21:04:33.7330000"),
                       HashTags = { }
                   };


                   context.Posts.Add(joaoPost3);

                   var joaoPost4 = new Post
                   {
                       UserId = "5df6a9d8-5492-4247-a860-38c2c2621887",
                       PhotoFile = "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/35907067-a305-4d96-9a44-6996925058cd_0186.JPEG?generation=1618952698449723&alt=media",
                       Thumbnail = "https://storage.googleapis.com/download/storage/v1/b/4thyearprojectrd/o/35907067-a305-4d96-9a44-6996925058cd_0186T.JPEG?generation=1618952698955355&alt=media",
                       MimeType = "image/jpeg",
                       Caption = "",
                       LicenseEnabled = true,
                       PrintsEnabled = true,
                       ShirtsEnabled = false,
                       PostDeleted = false,
                       UploadDate = Convert.ToDateTime("2021-04-20 21:04:49.4280000"),
                       HashTags = { }
                   };


                   context.Posts.Add(joaoPost4);



                   var like1 = new Like
                   {
                       Post_ID = "2",
                       User_ID = Guid.NewGuid().ToString()
                   };

                   context.Likes.Add(like1);


                   var like2 = new Like
                   {
                       Post_ID = "2",
                       User_ID = Guid.NewGuid().ToString()
                   };

                   context.Likes.Add(like2);

                   var like3 = new Like
                   {
                       Post_ID = "2",
                       User_ID = Guid.NewGuid().ToString()
                   };

                   context.Likes.Add(like3);


                   var like4 = new Like
                   {
                       Post_ID = "2",
                       User_ID = Guid.NewGuid().ToString()
                   };

                   context.Likes.Add(like4);


                   var like5 = new Like
                   {
                       Post_ID = "2",
                       User_ID = Guid.NewGuid().ToString()
                   };

                   context.Likes.Add(like5);


                   var like6 = new Like
                   {
                       Post_ID = "2",
                       User_ID = Guid.NewGuid().ToString()
                   };

                   context.Likes.Add(like6);


                   var like7 = new Like
                   {
                       Post_ID = "2",
                       User_ID = Guid.NewGuid().ToString()
                   };

                   context.Likes.Add(like7);


                   var like8 = new Like
                   {
                       Post_ID = "2",
                       User_ID = Guid.NewGuid().ToString()
                   };

                   context.Likes.Add(like8);

                   var like9 = new Like
                   {
                       Post_ID = "2",
                       User_ID = Guid.NewGuid().ToString()
                   };

                   context.Likes.Add(like9);


                   var like10 = new Like
                   {
                       Post_ID = "2",
                       User_ID = Guid.NewGuid().ToString()
                   };

                   context.Likes.Add(like10);

                   var like11 = new Like
                   {
                       Post_ID = "2",
                       User_ID = Guid.NewGuid().ToString()
                   };

                   context.Likes.Add(like11);

                   var like12 = new Like
                   {
                       Post_ID = "2",
                       User_ID = Guid.NewGuid().ToString()
                   };

                   context.Likes.Add(like12);

                   var like13 = new Like
                   {
                       Post_ID = "2",
                       User_ID = Guid.NewGuid().ToString()
                   };

                   context.Likes.Add(like13);

                   var like14 = new Like
                   {
                       Post_ID = "2",
                       User_ID = Guid.NewGuid().ToString()
                   };

                   context.Likes.Add(like14);



                }

                context.SaveChanges();
            }
        }
    }
}
}
