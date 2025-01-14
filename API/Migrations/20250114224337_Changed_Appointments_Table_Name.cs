using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Changed_Appointments_Table_Name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentsEntity_Doctors_Doctor_Id",
                table: "AppointmentsEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentsEntity_Patients_Patient_Id",
                table: "AppointmentsEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentsEntity",
                table: "AppointmentsEntity");

            migrationBuilder.RenameTable(
                name: "AppointmentsEntity",
                newName: "Appointments");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentsEntity_Patient_Id",
                table: "Appointments",
                newName: "IX_Appointments_Patient_Id");

            migrationBuilder.RenameIndex(
                name: "IX_AppointmentsEntity_Doctor_Id",
                table: "Appointments",
                newName: "IX_Appointments_Doctor_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors_Doctor_Id",
                table: "Appointments",
                column: "Doctor_Id",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_Patient_Id",
                table: "Appointments",
                column: "Patient_Id",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_Doctor_Id",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_Patient_Id",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments");

            migrationBuilder.RenameTable(
                name: "Appointments",
                newName: "AppointmentsEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_Patient_Id",
                table: "AppointmentsEntity",
                newName: "IX_AppointmentsEntity_Patient_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_Doctor_Id",
                table: "AppointmentsEntity",
                newName: "IX_AppointmentsEntity_Doctor_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentsEntity",
                table: "AppointmentsEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentsEntity_Doctors_Doctor_Id",
                table: "AppointmentsEntity",
                column: "Doctor_Id",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentsEntity_Patients_Patient_Id",
                table: "AppointmentsEntity",
                column: "Patient_Id",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
