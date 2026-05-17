using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLoyaltyDiscountFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseInvoice_Vendors_VendorId",
                table: "PurchaseInvoice");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseInvoiceItem_Parts_PartId",
                table: "PurchaseInvoiceItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseInvoiceItem_PurchaseInvoice_PurchaseInvoiceId",
                table: "PurchaseInvoiceItem");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesInvoice_Customers_CustomerId",
                table: "SalesInvoice");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesInvoiceItem_Parts_PartId",
                table: "SalesInvoiceItem");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesInvoiceItem_SalesInvoice_SalesInvoiceId",
                table: "SalesInvoiceItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalesInvoiceItem",
                table: "SalesInvoiceItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalesInvoice",
                table: "SalesInvoice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseInvoiceItem",
                table: "PurchaseInvoiceItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseInvoice",
                table: "PurchaseInvoice");

            migrationBuilder.RenameTable(
                name: "SalesInvoiceItem",
                newName: "SalesInvoiceItems");

            migrationBuilder.RenameTable(
                name: "SalesInvoice",
                newName: "SalesInvoices");

            migrationBuilder.RenameTable(
                name: "PurchaseInvoiceItem",
                newName: "PurchaseInvoiceItems");

            migrationBuilder.RenameTable(
                name: "PurchaseInvoice",
                newName: "PurchaseInvoices");

            migrationBuilder.RenameIndex(
                name: "IX_SalesInvoiceItem_SalesInvoiceId",
                table: "SalesInvoiceItems",
                newName: "IX_SalesInvoiceItems_SalesInvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesInvoiceItem_PartId",
                table: "SalesInvoiceItems",
                newName: "IX_SalesInvoiceItems_PartId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesInvoice_CustomerId",
                table: "SalesInvoices",
                newName: "IX_SalesInvoices_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseInvoiceItem_PurchaseInvoiceId",
                table: "PurchaseInvoiceItems",
                newName: "IX_PurchaseInvoiceItems_PurchaseInvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseInvoiceItem_PartId",
                table: "PurchaseInvoiceItems",
                newName: "IX_PurchaseInvoiceItems_PartId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseInvoice_VendorId",
                table: "PurchaseInvoices",
                newName: "IX_PurchaseInvoices_VendorId");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "SalesInvoices",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentage",
                table: "SalesInvoices",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FinalTotal",
                table: "SalesInvoices",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SubTotal",
                table: "SalesInvoices",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "PurchaseInvoices",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentage",
                table: "PurchaseInvoices",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FinalTotal",
                table: "PurchaseInvoices",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SubTotal",
                table: "PurchaseInvoices",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalesInvoiceItems",
                table: "SalesInvoiceItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalesInvoices",
                table: "SalesInvoices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseInvoiceItems",
                table: "PurchaseInvoiceItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseInvoices",
                table: "PurchaseInvoices",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseInvoiceItems_Parts_PartId",
                table: "PurchaseInvoiceItems",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseInvoiceItems_PurchaseInvoices_PurchaseInvoiceId",
                table: "PurchaseInvoiceItems",
                column: "PurchaseInvoiceId",
                principalTable: "PurchaseInvoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseInvoices_Vendors_VendorId",
                table: "PurchaseInvoices",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesInvoiceItems_Parts_PartId",
                table: "SalesInvoiceItems",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesInvoiceItems_SalesInvoices_SalesInvoiceId",
                table: "SalesInvoiceItems",
                column: "SalesInvoiceId",
                principalTable: "SalesInvoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesInvoices_Customers_CustomerId",
                table: "SalesInvoices",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseInvoiceItems_Parts_PartId",
                table: "PurchaseInvoiceItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseInvoiceItems_PurchaseInvoices_PurchaseInvoiceId",
                table: "PurchaseInvoiceItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseInvoices_Vendors_VendorId",
                table: "PurchaseInvoices");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesInvoiceItems_Parts_PartId",
                table: "SalesInvoiceItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesInvoiceItems_SalesInvoices_SalesInvoiceId",
                table: "SalesInvoiceItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesInvoices_Customers_CustomerId",
                table: "SalesInvoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalesInvoices",
                table: "SalesInvoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalesInvoiceItems",
                table: "SalesInvoiceItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseInvoices",
                table: "PurchaseInvoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseInvoiceItems",
                table: "PurchaseInvoiceItems");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "FinalTotal",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "SalesInvoices");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "PurchaseInvoices");

            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "PurchaseInvoices");

            migrationBuilder.DropColumn(
                name: "FinalTotal",
                table: "PurchaseInvoices");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "PurchaseInvoices");

            migrationBuilder.RenameTable(
                name: "SalesInvoices",
                newName: "SalesInvoice");

            migrationBuilder.RenameTable(
                name: "SalesInvoiceItems",
                newName: "SalesInvoiceItem");

            migrationBuilder.RenameTable(
                name: "PurchaseInvoices",
                newName: "PurchaseInvoice");

            migrationBuilder.RenameTable(
                name: "PurchaseInvoiceItems",
                newName: "PurchaseInvoiceItem");

            migrationBuilder.RenameIndex(
                name: "IX_SalesInvoices_CustomerId",
                table: "SalesInvoice",
                newName: "IX_SalesInvoice_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesInvoiceItems_SalesInvoiceId",
                table: "SalesInvoiceItem",
                newName: "IX_SalesInvoiceItem_SalesInvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesInvoiceItems_PartId",
                table: "SalesInvoiceItem",
                newName: "IX_SalesInvoiceItem_PartId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseInvoices_VendorId",
                table: "PurchaseInvoice",
                newName: "IX_PurchaseInvoice_VendorId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseInvoiceItems_PurchaseInvoiceId",
                table: "PurchaseInvoiceItem",
                newName: "IX_PurchaseInvoiceItem_PurchaseInvoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseInvoiceItems_PartId",
                table: "PurchaseInvoiceItem",
                newName: "IX_PurchaseInvoiceItem_PartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalesInvoice",
                table: "SalesInvoice",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalesInvoiceItem",
                table: "SalesInvoiceItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseInvoice",
                table: "PurchaseInvoice",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseInvoiceItem",
                table: "PurchaseInvoiceItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseInvoice_Vendors_VendorId",
                table: "PurchaseInvoice",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseInvoiceItem_Parts_PartId",
                table: "PurchaseInvoiceItem",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseInvoiceItem_PurchaseInvoice_PurchaseInvoiceId",
                table: "PurchaseInvoiceItem",
                column: "PurchaseInvoiceId",
                principalTable: "PurchaseInvoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesInvoice_Customers_CustomerId",
                table: "SalesInvoice",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesInvoiceItem_Parts_PartId",
                table: "SalesInvoiceItem",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesInvoiceItem_SalesInvoice_SalesInvoiceId",
                table: "SalesInvoiceItem",
                column: "SalesInvoiceId",
                principalTable: "SalesInvoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
