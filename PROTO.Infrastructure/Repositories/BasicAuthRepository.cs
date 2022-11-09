using Dapper;
using Microsoft.Extensions.Configuration;
using PROTO.UseCase.Interfaces;
using PROTO.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PROTO.Infrastructure.Repositories
{
    public class BasicAuthRepository : IBasicAuthRepository
    {
        // Make it possible to read a connection string from configuration
        private readonly IConfiguration _configuration;

        public BasicAuthRepository(IConfiguration configuration)
        {
            // Injecting Iconfiguration to the contructor of the product repository
            _configuration = configuration;
        }

        
        public async Task<BasicAuthorization> GetBasicAuthAsync(string UserName, string PassWord)
        {
            var sql = "[PROTO].[dbo].[GetBasicAuthUser]";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@Username", UserName, DbType.String);
                parameters.Add("@Password", PassWord, DbType.String);
                var result = await connection.QuerySingleOrDefaultAsync<BasicAuthorization>(sql, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

    }
}
