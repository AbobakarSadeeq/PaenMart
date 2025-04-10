using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access.Migrations
{
    public partial class PaenMart2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carousels",
                columns: table => new
                {
                    CarouselID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePriority = table.Column<int>(type: "int", nullable: false),
                    ImageTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NavigationUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carousels", x => x.CarouselID);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryID);
                });

            migrationBuilder.CreateTable(
                name: "DiscountDeals",
                columns: table => new
                {
                    DiscountDealID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DealName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DealStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackgroundJobId = table.Column<int>(type: "int", nullable: true),
                    DealExpireAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DealCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountDeals", x => x.DiscountDealID);
                });

            migrationBuilder.CreateTable(
                name: "ProductBrands",
                columns: table => new
                {
                    ProductBrandID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBrands", x => x.ProductBrandID);
                });

            migrationBuilder.CreateTable(
                name: "SendingEmails",
                columns: table => new
                {
                    SendingEmailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendingEmails", x => x.SendingEmailID);
                });

            migrationBuilder.CreateTable(
                name: "SponsorsAds",
                columns: table => new
                {
                    AdID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SponsoredByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdUrlDestination = table.Column<string>(type: "nvarchar(2083)", maxLength: 2083, nullable: true),
                    AdPrice = table.Column<int>(type: "int", nullable: false),
                    AdStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShowAdOnPage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdPictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Update_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Expire_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SponsorsAds", x => x.AdID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdminAccounts",
                columns: table => new
                {
                    AdminAccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentBalance = table.Column<int>(type: "int", nullable: false),
                    BeforeBalance = table.Column<int>(type: "int", nullable: false),
                    TransactionPurpose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BalanceSituation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TransactionDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminAccounts", x => x.AdminAccountID);
                    table.ForeignKey(
                        name: "FK_AdminAccounts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DathOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeHireDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shippers",
                columns: table => new
                {
                    ShipperID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DathOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShipperHireDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShipmentVehicleType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehiclePlatNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfShipmentsDone = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippers", x => x.ShipperID);
                    table.ForeignKey(
                        name: "FK_Shippers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionHeader = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsMainPhoto = table.Column<bool>(type: "bit", nullable: false),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomIdentityId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserImages_AspNetUsers_CustomIdentityId",
                        column: x => x.CustomIdentityId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    SubCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.SubCategoryID);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityID);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePayments",
                columns: table => new
                {
                    EmployeeMonthlyPaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Payment = table.Column<bool>(type: "bit", nullable: false),
                    PaymentHistory = table.Column<bool>(type: "bit", nullable: false),
                    Payment_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePayments", x => x.EmployeeMonthlyPaymentID);
                    table.ForeignKey(
                        name: "FK_EmployeePayments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShippedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShipperId = table.Column<int>(type: "int", nullable: true),
                    CustomIdentityId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_CustomIdentityId",
                        column: x => x.CustomIdentityId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Shippers_ShipperId",
                        column: x => x.ShipperId,
                        principalTable: "Shippers",
                        principalColumn: "ShipperID");
                });

            migrationBuilder.CreateTable(
                name: "ShipperPayments",
                columns: table => new
                {
                    ShipperMonthlyPaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Payment = table.Column<bool>(type: "bit", nullable: false),
                    PaymentHistory = table.Column<bool>(type: "bit", nullable: false),
                    Payment_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShipperId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipperPayments", x => x.ShipperMonthlyPaymentID);
                    table.ForeignKey(
                        name: "FK_ShipperPayments_Shippers_ShipperId",
                        column: x => x.ShipperId,
                        principalTable: "Shippers",
                        principalColumn: "ShipperID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NestSubCategories",
                columns: table => new
                {
                    NestSubCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NestSubCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NestSubCategories", x => x.NestSubCategoryID);
                    table.ForeignKey(
                        name: "FK_NestSubCategories_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "SubCategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAddresses",
                columns: table => new
                {
                    UserAddressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompleteAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddresses", x => x.UserAddressID);
                    table.ForeignKey(
                        name: "FK_UserAddresses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserAddresses_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DynamicFormStructures",
                columns: table => new
                {
                    DynamicFormStructureID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormStructure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NestSubCategoryId = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductSize = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicFormStructures", x => x.DynamicFormStructureID);
                    table.ForeignKey(
                        name: "FK_DynamicFormStructures_NestSubCategories_NestSubCategoryId",
                        column: x => x.NestSubCategoryId,
                        principalTable: "NestSubCategories",
                        principalColumn: "NestSubCategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NestSubCategoryProductBrands",
                columns: table => new
                {
                    NestSubCategoryId = table.Column<int>(type: "int", nullable: false),
                    ProductBrandId = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NestSubCategoryProductBrands", x => new { x.NestSubCategoryId, x.ProductBrandId });
                    table.ForeignKey(
                        name: "FK_NestSubCategoryProductBrands_NestSubCategories_NestSubCategoryId",
                        column: x => x.NestSubCategoryId,
                        principalTable: "NestSubCategories",
                        principalColumn: "NestSubCategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NestSubCategoryProductBrands_ProductBrands_ProductBrandId",
                        column: x => x.ProductBrandId,
                        principalTable: "ProductBrands",
                        principalColumn: "ProductBrandID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StockAvailiability = table.Column<bool>(type: "bit", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SellUnits = table.Column<int>(type: "int", nullable: false),
                    ProductBrandId = table.Column<int>(type: "int", nullable: false),
                    NestSubCategoryId = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Modified_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductTotalStars = table.Column<int>(type: "int", nullable: false),
                    Raitings = table.Column<int>(type: "int", nullable: false),
                    OnDiscount = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Products_NestSubCategories_NestSubCategoryId",
                        column: x => x.NestSubCategoryId,
                        principalTable: "NestSubCategories",
                        principalColumn: "NestSubCategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_ProductBrands_ProductBrandId",
                        column: x => x.ProductBrandId,
                        principalTable: "ProductBrands",
                        principalColumn: "ProductBrandID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    DiscountPercentage = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductActuallQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailID);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProductReviews",
                columns: table => new
                {
                    OrderProductReviewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    RaitingStars = table.Column<double>(type: "float", nullable: false),
                    ProductComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Add_Review_Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProductReviews", x => x.OrderProductReviewID);
                    table.ForeignKey(
                        name: "FK_OrderProductReviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProductReviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductDiscountDeals",
                columns: table => new
                {
                    ProductDiscountDealID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductBeforePrice = table.Column<int>(type: "int", nullable: false),
                    ProductAfterDiscountPrice = table.Column<int>(type: "int", nullable: false),
                    ProductPercentage = table.Column<int>(type: "int", nullable: false),
                    DiscountDealId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDiscountDeals", x => x.ProductDiscountDealID);
                    table.ForeignKey(
                        name: "FK_ProductDiscountDeals_DiscountDeals_DiscountDealId",
                        column: x => x.DiscountDealId,
                        principalTable: "DiscountDeals",
                        principalColumn: "DiscountDealID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductDiscountDeals_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    ProductImageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.ProductImageID);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductWishlists",
                columns: table => new
                {
                    ProductWishlistID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductWishlists", x => x.ProductWishlistID);
                    table.ForeignKey(
                        name: "FK_ProductWishlists_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductWishlists_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProductReviewsPhotos",
                columns: table => new
                {
                    OrderProductReviewsPhotoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderProductReviewId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProductReviewsPhotos", x => x.OrderProductReviewsPhotoID);
                    table.ForeignKey(
                        name: "FK_OrderProductReviewsPhotos_OrderProductReviews_OrderProductReviewId",
                        column: x => x.OrderProductReviewId,
                        principalTable: "OrderProductReviews",
                        principalColumn: "OrderProductReviewID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminAccounts_UserId",
                table: "AdminAccounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicFormStructures_NestSubCategoryId",
                table: "DynamicFormStructures",
                column: "NestSubCategoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayments_EmployeeId",
                table: "EmployeePayments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NestSubCategories_SubCategoryId",
                table: "NestSubCategories",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_NestSubCategoryProductBrands_ProductBrandId",
                table: "NestSubCategoryProductBrands",
                column: "ProductBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProductReviews_ProductId",
                table: "OrderProductReviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProductReviews_UserId",
                table: "OrderProductReviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProductReviewsPhotos_OrderProductReviewId",
                table: "OrderProductReviewsPhotos",
                column: "OrderProductReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomIdentityId",
                table: "Orders",
                column: "CustomIdentityId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShipperId",
                table: "Orders",
                column: "ShipperId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDiscountDeals_DiscountDealId",
                table: "ProductDiscountDeals",
                column: "DiscountDealId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDiscountDeals_ProductId",
                table: "ProductDiscountDeals",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_NestSubCategoryId",
                table: "Products",
                column: "NestSubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductBrandId",
                table: "Products",
                column: "ProductBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWishlists_ProductId",
                table: "ProductWishlists",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWishlists_UserId",
                table: "ProductWishlists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipperPayments_ShipperId",
                table: "ShipperPayments",
                column: "ShipperId");

            migrationBuilder.CreateIndex(
                name: "IX_Shippers_UserId",
                table: "Shippers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_CityId",
                table: "UserAddresses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_UserId",
                table: "UserAddresses",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_CustomIdentityId",
                table: "UserImages",
                column: "CustomIdentityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminAccounts");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Carousels");

            migrationBuilder.DropTable(
                name: "DynamicFormStructures");

            migrationBuilder.DropTable(
                name: "EmployeePayments");

            migrationBuilder.DropTable(
                name: "NestSubCategoryProductBrands");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "OrderProductReviewsPhotos");

            migrationBuilder.DropTable(
                name: "ProductDiscountDeals");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProductWishlists");

            migrationBuilder.DropTable(
                name: "SendingEmails");

            migrationBuilder.DropTable(
                name: "ShipperPayments");

            migrationBuilder.DropTable(
                name: "SponsorsAds");

            migrationBuilder.DropTable(
                name: "UserAddresses");

            migrationBuilder.DropTable(
                name: "UserImages");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "OrderProductReviews");

            migrationBuilder.DropTable(
                name: "DiscountDeals");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Shippers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "NestSubCategories");

            migrationBuilder.DropTable(
                name: "ProductBrands");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
