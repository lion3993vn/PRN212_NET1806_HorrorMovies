using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class dbInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    movieId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    movieTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    movieYear = table.Column<int>(type: "int", nullable: true),
                    movieURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    movieRank = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Movies__42EB374EE4BE64C6", x => x.movieId);
                });

            migrationBuilder.CreateTable(
                name: "CriticReview",
                columns: table => new
                {
                    reviewId = table.Column<int>(type: "int", nullable: false),
                    creationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    criticName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isTopCritic = table.Column<bool>(type: "bit", nullable: true),
                    publicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    reviewUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    quote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    scoreSentiment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    movieId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CriticRe__2ECD6E04F07C4348", x => x.reviewId);
                    table.ForeignKey(
                        name: "FK__CriticRev__movie__3B75D760",
                        column: x => x.movieId,
                        principalTable: "Movies",
                        principalColumn: "movieId");
                });

            migrationBuilder.CreateTable(
                name: "UserReviews",
                columns: table => new
                {
                    userId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    rating = table.Column<float>(type: "real", nullable: true),
                    isSuperReviewer = table.Column<bool>(type: "bit", nullable: true),
                    hasSpoilers = table.Column<bool>(type: "bit", nullable: true),
                    hasProfanity = table.Column<bool>(type: "bit", nullable: true),
                    creationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    movieId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserRevi__CB9A1CFF398AAF8A", x => x.userId);
                    table.ForeignKey(
                        name: "FK__UserRevie__movie__38996AB5",
                        column: x => x.movieId,
                        principalTable: "Movies",
                        principalColumn: "movieId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CriticReview_movieId",
                table: "CriticReview",
                column: "movieId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReviews_movieId",
                table: "UserReviews",
                column: "movieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CriticReview");

            migrationBuilder.DropTable(
                name: "UserReviews");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
