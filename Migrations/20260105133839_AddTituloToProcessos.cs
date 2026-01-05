using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntraNet.Migrations
{
    /// <inheritdoc />
    public partial class AddTituloToProcessos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Processos",
                newName: "Titulo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Titulo",
                table: "Processos",
                newName: "Nome");
        }
    }
}
