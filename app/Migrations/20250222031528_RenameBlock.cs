using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lms.Migrations
{
    /// <inheritdoc />
    public partial class RenameBlock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlockWorkItem_Blockers_BlocksId",
                table: "BlockWorkItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Progresses_WorkItems_WorkItemId",
                table: "Progresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Blockers_BlockId",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blockers",
                table: "Blockers");

            migrationBuilder.RenameTable(
                name: "Blockers",
                newName: "Block");

            migrationBuilder.AlterColumn<int>(
                name: "WorkItemId",
                table: "Progresses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Block",
                table: "Block",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlockWorkItem_Block_BlocksId",
                table: "BlockWorkItem",
                column: "BlocksId",
                principalTable: "Block",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Progresses_WorkItems_WorkItemId",
                table: "Progresses",
                column: "WorkItemId",
                principalTable: "WorkItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Block_BlockId",
                table: "Tags",
                column: "BlockId",
                principalTable: "Block",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlockWorkItem_Block_BlocksId",
                table: "BlockWorkItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Progresses_WorkItems_WorkItemId",
                table: "Progresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Block_BlockId",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Block",
                table: "Block");

            migrationBuilder.RenameTable(
                name: "Block",
                newName: "Blockers");

            migrationBuilder.AlterColumn<int>(
                name: "WorkItemId",
                table: "Progresses",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blockers",
                table: "Blockers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlockWorkItem_Blockers_BlocksId",
                table: "BlockWorkItem",
                column: "BlocksId",
                principalTable: "Blockers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Progresses_WorkItems_WorkItemId",
                table: "Progresses",
                column: "WorkItemId",
                principalTable: "WorkItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Blockers_BlockId",
                table: "Tags",
                column: "BlockId",
                principalTable: "Blockers",
                principalColumn: "Id");
        }
    }
}
