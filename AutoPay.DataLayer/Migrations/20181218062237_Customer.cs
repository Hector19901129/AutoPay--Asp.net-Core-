using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoPay.DataLayer.Migrations
{
    public partial class Customer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 256, nullable: false),
                    Name = table.Column<string>(maxLength: 512, nullable: false),
                    Address = table.Column<string>(maxLength: 512, nullable: true),
                    City = table.Column<string>(maxLength: 256, nullable: true),
                    State = table.Column<string>(maxLength: 256, nullable: true),
                    FK_Countries = table.Column<string>(maxLength: 32, nullable: true),
                    ZipCode = table.Column<string>(maxLength: 128, nullable: true),
                    CardType = table.Column<string>(maxLength: 32, nullable: false),
                    CardNumber = table.Column<string>(maxLength: 128, nullable: false),
                    ExpiryMonth = table.Column<string>(maxLength: 32, nullable: false),
                    ExpiryYear = table.Column<string>(maxLength: 32, nullable: false),
                    Ccv = table.Column<string>(maxLength: 32, nullable: false),
                    CreatedOn = table.Column<string>(maxLength: 128, nullable: false),
                    FK_AspNetUsers = table.Column<string>(maxLength: 128, nullable: false),
                    UpdatedOn = table.Column<string>(nullable: true),
                    Status = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
