using Microsoft.EntityFrameworkCore.Migrations;

namespace ExchangeLemonCore.Migrations
{
    public partial class IndexOnTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE NONCLUSTERED INDEX [nci_wi_Transactions_TransactionDate] 
            ON [dbo].[Transactions] ([TransactionDate]) INCLUDE ([Amount], [CurrencyPair], 
            [IsBuyTaker], [OrderId], [TransactionRate])
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "nci_wi_Transactions_TransactionDate",
                table: "Transactions");
        }
    }
}
