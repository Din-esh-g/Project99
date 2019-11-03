using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project99.Migrations
{
    public partial class TransactioUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    accountNumber = table.Column<int>(nullable: false),
                    numberOfMonth = table.Column<int>(nullable: false),
                    accountType = table.Column<string>(nullable: true),
                    amount = table.Column<double>(nullable: false),
                    date = table.Column<DateTime>(nullable: false),
                    type = table.Column<string>(nullable: true),
                    CustomersId = table.Column<int>(nullable: true),
                    CheckingaccountNumber = table.Column<int>(nullable: true),
                    BusinessaccountNumber = table.Column<int>(nullable: true),
                    LoanaccountNumber = table.Column<int>(nullable: true),
                    TermaccountNumber = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Checking_BusinessaccountNumber",
                        column: x => x.BusinessaccountNumber,
                        principalTable: "Checking",
                        principalColumn: "accountNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_Checking_CheckingaccountNumber",
                        column: x => x.CheckingaccountNumber,
                        principalTable: "Checking",
                        principalColumn: "accountNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_Customers_CustomersId",
                        column: x => x.CustomersId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_Checking_LoanaccountNumber",
                        column: x => x.LoanaccountNumber,
                        principalTable: "Checking",
                        principalColumn: "accountNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_Checking_TermaccountNumber",
                        column: x => x.TermaccountNumber,
                        principalTable: "Checking",
                        principalColumn: "accountNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_BusinessaccountNumber",
                table: "Transaction",
                column: "BusinessaccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CheckingaccountNumber",
                table: "Transaction",
                column: "CheckingaccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CustomersId",
                table: "Transaction",
                column: "CustomersId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_LoanaccountNumber",
                table: "Transaction",
                column: "LoanaccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_TermaccountNumber",
                table: "Transaction",
                column: "TermaccountNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");
        }
    }
}
