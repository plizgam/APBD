using Microsoft.EntityFrameworkCore.Migrations;

using System;

namespace Cw11.Migrations
{
    public partial class AddedMedicamentDoctorPatientDataTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Doctor",
                columns: new[] { "IdDoctor", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "anna@kowalska.pl", "Anna", "Kowalska" },
                    { 2, "magdalena@nowak.pl", "Magdalena", "Nowak" }
                });

            migrationBuilder.InsertData(
                table: "Medicament",
                columns: new[] { "IdMedicament", "Description", "Name", "Type" },
                values: new object[] { 1, "The best medicament", "Ibuprom Sprint Max Mega Plus", "PainKiller" });

            migrationBuilder.InsertData(
                table: "Patient",
                columns: new[] { "IdPatient", "BirthDate", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(1999, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Miłosz", "Pliżga" },
                    { 2, new DateTime(1998, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Oliwia", "Żak" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doctor",
                keyColumn: "IdDoctor",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Medicament",
                keyColumn: "IdMedicament",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patient",
                keyColumn: "IdPatient",
                keyValue: 2);
        }
    }
}
