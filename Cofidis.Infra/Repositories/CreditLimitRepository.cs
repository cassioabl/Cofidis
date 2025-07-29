using Cofidis.Domain.ValueObjects;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Cofidis.Infra.Repositories
{
    public class CreditLimitRepository(CofidisDbContext cofidisDbContext)
    {
        public async Task<CreditLimit> GetCreditLimit(decimal monthlyIncome)
        {
            cofidisDbContext.Database.OpenConnection();
            var connection = cofidisDbContext.Database.GetDbConnection();

            await using var command = connection.CreateCommand();
            command.CommandText = "GetCreditLimit";
            command.CommandType = CommandType.StoredProcedure;

            var monthlyIncomeParam = new SqlParameter("@MonthlyIncome", SqlDbType.Decimal)
            {
                Precision = 10,
                Scale = 2,
                Value = monthlyIncome
            };

            var output = new SqlParameter("@MaxCreditLimit", SqlDbType.Decimal)
            {
                Precision = 10,
                Scale = 2,
                Direction = ParameterDirection.Output
            };

            command.Parameters.Add(monthlyIncomeParam);
            command.Parameters.Add(output);

            if (command.Connection.State != ConnectionState.Open)
                command.Connection.Open();

            command.ExecuteNonQuery();

            var maxAllowed = output.Value != DBNull.Value
                ? (decimal)output.Value
                : 0;

            return new CreditLimit(monthlyIncome, maxAllowed);
        }
    }
}
