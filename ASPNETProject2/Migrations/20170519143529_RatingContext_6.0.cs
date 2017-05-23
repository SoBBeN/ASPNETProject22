using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPNETProject2.Migrations
{
    public partial class RatingContext_60 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Contractor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Contractor",
                nullable: true);
        }
    }
}
