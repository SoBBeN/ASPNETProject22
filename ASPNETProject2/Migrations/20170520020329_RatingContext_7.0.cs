using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPNETProject2.Migrations
{
    public partial class RatingContext_70 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "AverageRating",
                table: "Contractor",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ReviewStarTotal",
                table: "Contractor",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewStarTotal",
                table: "Contractor");

            migrationBuilder.AlterColumn<int>(
                name: "AverageRating",
                table: "Contractor",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
