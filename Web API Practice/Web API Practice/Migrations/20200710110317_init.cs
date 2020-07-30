using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web_API_Practice.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Resturants",
                columns: table => new
                {
                    ResturantId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ResturantName = table.Column<string>(nullable: true),
                    ResturantAddress_AddressApartmentNumber = table.Column<string>(nullable: true),
                    ResturantAddress_AddressBuildingNumber = table.Column<string>(nullable: true),
                    ResturantAddress_AddressStreetName = table.Column<string>(nullable: true),
                    ResturantAddress_AddressSuburbName = table.Column<string>(nullable: true),
                    ResturantAddress_AddressCityName = table.Column<string>(nullable: true),
                    ResturantAddress_AddressStateName = table.Column<string>(nullable: true),
                    ResturantAddress_AddressCountryName = table.Column<string>(nullable: true),
                    ResturantAddress_AddressPostcode = table.Column<string>(nullable: true),
                    ResturantStarRating = table.Column<float>(nullable: false),
                    ResturantImage = table.Column<byte[]>(nullable: true),
                    ResturantOwnerName = table.Column<string>(nullable: true),
                    ResturantDescription = table.Column<string>(nullable: true),
                    ResturantType = table.Column<int>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resturants", x => x.ResturantId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    ProfileDescription = table.Column<string>(nullable: true),
                    UserReputationRating = table.Column<int>(nullable: false),
                    CityOfResidence = table.Column<string>(nullable: true),
                    CountryOfResidence = table.Column<string>(nullable: true),
                    UserProfileImage = table.Column<byte[]>(nullable: true),
                    TotalReviews = table.Column<int>(nullable: false),
                    TotalActiveReviews = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ResturantReviews",
                columns: table => new
                {
                    ResturantReviewId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReviewText = table.Column<string>(nullable: true),
                    ReviewScore = table.Column<float>(nullable: false),
                    ReviewImage = table.Column<byte[]>(nullable: true),
                    ReviewDate = table.Column<DateTime>(nullable: false),
                    Verified = table.Column<bool>(nullable: false),
                    NumberOfUsersVerfied = table.Column<int>(nullable: false),
                    ReviewAuthorUserId = table.Column<int>(nullable: true),
                    ResturantId = table.Column<int>(nullable: true),
                    ResturantReviewId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResturantReviews", x => x.ResturantReviewId);
                    table.ForeignKey(
                        name: "FK_ResturantReviews_Resturants_ResturantId",
                        column: x => x.ResturantId,
                        principalTable: "Resturants",
                        principalColumn: "ResturantId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResturantReviews_ResturantReviews_ResturantReviewId1",
                        column: x => x.ResturantReviewId1,
                        principalTable: "ResturantReviews",
                        principalColumn: "ResturantReviewId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResturantReviews_Users_ReviewAuthorUserId",
                        column: x => x.ReviewAuthorUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResturantReviews_ResturantId",
                table: "ResturantReviews",
                column: "ResturantId");

            migrationBuilder.CreateIndex(
                name: "IX_ResturantReviews_ResturantReviewId1",
                table: "ResturantReviews",
                column: "ResturantReviewId1");

            migrationBuilder.CreateIndex(
                name: "IX_ResturantReviews_ReviewAuthorUserId",
                table: "ResturantReviews",
                column: "ReviewAuthorUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResturantReviews");

            migrationBuilder.DropTable(
                name: "Resturants");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
