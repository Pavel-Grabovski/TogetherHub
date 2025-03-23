using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FillAuthorIdInTopics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"UPDATE ""Topics"" SET ""AuthorId"" = COALESCE((
                SELECT ""UserReference""
                FROM ""Relationships""
                WHERE ""TopicReference"" = ""Topics"".""Id"" AND ""Relationships"".""Role"" = 'Organizer'),
                    '00000000-0000-0000-0000-000000000000')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
