using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleIMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVehiclesAppointmentsPartRequestsReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Customers_CustomerId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Vehicle_VehicleId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_PartRequest_Customers_CustomerId",
                table: "PartRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_PartRequest_Parts_PartId",
                table: "PartRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Customers_CustomerId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Parts_PartId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Customers_CustomerId",
                table: "Vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Review",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartRequest",
                table: "PartRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointment",
                table: "Appointment");

            migrationBuilder.RenameTable(
                name: "Vehicle",
                newName: "Vehicles");

            migrationBuilder.RenameTable(
                name: "Review",
                newName: "Reviews");

            migrationBuilder.RenameTable(
                name: "PartRequest",
                newName: "PartRequests");

            migrationBuilder.RenameTable(
                name: "Appointment",
                newName: "Appointments");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicle_CustomerId",
                table: "Vehicles",
                newName: "IX_Vehicles_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_PartId",
                table: "Reviews",
                newName: "IX_Reviews_PartId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_CustomerId",
                table: "Reviews",
                newName: "IX_Reviews_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_PartRequest_PartId",
                table: "PartRequests",
                newName: "IX_PartRequests_PartId");

            migrationBuilder.RenameIndex(
                name: "IX_PartRequest_CustomerId",
                table: "PartRequests",
                newName: "IX_PartRequests_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_VehicleId",
                table: "Appointments",
                newName: "IX_Appointments_VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_CustomerId",
                table: "Appointments",
                newName: "IX_Appointments_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartRequests",
                table: "PartRequests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Customers_CustomerId",
                table: "Appointments",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Vehicles_VehicleId",
                table: "Appointments",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PartRequests_Customers_CustomerId",
                table: "PartRequests",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PartRequests_Parts_PartId",
                table: "PartRequests",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Customers_CustomerId",
                table: "Reviews",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Parts_PartId",
                table: "Reviews",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Customers_CustomerId",
                table: "Vehicles",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Customers_CustomerId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Vehicles_VehicleId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_PartRequests_Customers_CustomerId",
                table: "PartRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_PartRequests_Parts_PartId",
                table: "PartRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Customers_CustomerId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Parts_PartId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Customers_CustomerId",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartRequests",
                table: "PartRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments");

            migrationBuilder.RenameTable(
                name: "Vehicles",
                newName: "Vehicle");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Review");

            migrationBuilder.RenameTable(
                name: "PartRequests",
                newName: "PartRequest");

            migrationBuilder.RenameTable(
                name: "Appointments",
                newName: "Appointment");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_CustomerId",
                table: "Vehicle",
                newName: "IX_Vehicle_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_PartId",
                table: "Review",
                newName: "IX_Review_PartId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_CustomerId",
                table: "Review",
                newName: "IX_Review_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_PartRequests_PartId",
                table: "PartRequest",
                newName: "IX_PartRequest_PartId");

            migrationBuilder.RenameIndex(
                name: "IX_PartRequests_CustomerId",
                table: "PartRequest",
                newName: "IX_PartRequest_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_VehicleId",
                table: "Appointment",
                newName: "IX_Appointment_VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_CustomerId",
                table: "Appointment",
                newName: "IX_Appointment_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review",
                table: "Review",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartRequest",
                table: "PartRequest",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointment",
                table: "Appointment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Customers_CustomerId",
                table: "Appointment",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Vehicle_VehicleId",
                table: "Appointment",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PartRequest_Customers_CustomerId",
                table: "PartRequest",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PartRequest_Parts_PartId",
                table: "PartRequest",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Customers_CustomerId",
                table: "Review",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Parts_PartId",
                table: "Review",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Customers_CustomerId",
                table: "Vehicle",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
