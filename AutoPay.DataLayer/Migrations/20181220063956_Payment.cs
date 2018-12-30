using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoPay.DataLayer.Migrations
{
    public partial class Payment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BatchCustomers",
                table: "BatchCustomers");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "BatchCustomers",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BatchCustomers",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BatchCustomers",
                table: "BatchCustomers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FK_BatchCustomers = table.Column<int>(nullable: false),
                    AuthCode = table.Column<string>(maxLength: 50, nullable: true),
                    TransactionId = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    IsSuccess = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_BatchCustomers_FK_BatchCustomers",
                        column: x => x.FK_BatchCustomers,
                        principalTable: "BatchCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentErrors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FK_Payments = table.Column<int>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentErrors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentErrors_Payments_FK_Payments",
                        column: x => x.FK_Payments,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BatchCustomers_FK_Batches",
                table: "BatchCustomers",
                column: "FK_Batches");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentErrors_FK_Payments",
                table: "PaymentErrors",
                column: "FK_Payments");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_FK_BatchCustomers",
                table: "Payments",
                column: "FK_BatchCustomers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentErrors");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BatchCustomers",
                table: "BatchCustomers");

            migrationBuilder.DropIndex(
                name: "IX_BatchCustomers_FK_Batches",
                table: "BatchCustomers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BatchCustomers");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "BatchCustomers",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BatchCustomers",
                table: "BatchCustomers",
                columns: new[] { "FK_Batches", "CustomerId" });
        }
    }
}
