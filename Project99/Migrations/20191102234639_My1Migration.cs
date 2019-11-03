using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project99.Migrations
{
    public partial class My1Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(nullable: true),
                    lastName = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    phoneNumber = table.Column<long>(nullable: false),
                    address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Business",
                columns: table => new
                {
                    accountNumber = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(nullable: true),
                    InterestRate = table.Column<double>(nullable: false),
                    Balance = table.Column<double>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    CustomersId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Business", x => x.accountNumber);
                    table.ForeignKey(
                        name: "FK_Business_Customers_CustomersId",
                        column: x => x.CustomersId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Checking",
                columns: table => new
                {
                    accountNumber = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(nullable: true),
                    InterestRate = table.Column<double>(nullable: false),
                    Balance = table.Column<double>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    CustomersId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checking", x => x.accountNumber);
                    table.ForeignKey(
                        name: "FK_Checking_Customers_CustomersId",
                        column: x => x.CustomersId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Loan",
                columns: table => new
                {
                    accountNumber = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(nullable: true),
                    InterestRate = table.Column<double>(nullable: false),
                    Balance = table.Column<double>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    CustomersId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan", x => x.accountNumber);
                    table.ForeignKey(
                        name: "FK_Loan_Customers_CustomersId",
                        column: x => x.CustomersId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Term",
                columns: table => new
                {
                    accountNumber = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    period = table.Column<int>(nullable: false),
                    type = table.Column<string>(nullable: true),
                    InterestRate = table.Column<double>(nullable: false),
                    Balance = table.Column<double>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    CustomersId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Term", x => x.accountNumber);
                    table.ForeignKey(
                        name: "FK_Term_Customers_CustomersId",
                        column: x => x.CustomersId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Business_CustomersId",
                table: "Business",
                column: "CustomersId");

            migrationBuilder.CreateIndex(
                name: "IX_Checking_CustomersId",
                table: "Checking",
                column: "CustomersId");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_CustomersId",
                table: "Loan",
                column: "CustomersId");

            migrationBuilder.CreateIndex(
                name: "IX_Term_CustomersId",
                table: "Term",
                column: "CustomersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Business");

            migrationBuilder.DropTable(
                name: "Checking");

            migrationBuilder.DropTable(
                name: "Loan");

            migrationBuilder.DropTable(
                name: "Term");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
