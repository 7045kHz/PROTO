using Dapper;
using Microsoft.Extensions.Configuration;
using PROTO.UseCase.Interfaces;
using PROTO.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PROTO.Infrastructure.Repositories
{
    public class ProjectScheduleRepository : IProjectScheduleRepository
    {
        // Make it possible to read a connection string from configuration
        private readonly IConfiguration _configuration;

        public ProjectScheduleRepository(IConfiguration configuration)
        {
            // Injecting Iconfiguration to the contructor of the product repository
            _configuration = configuration;
        }

 
        public async Task<IReadOnlyList<ProjectSchedule>> GetAllAsync()
        {
            var sql = "SELECT Id, Project, StartDate, EndDate, UpdateOn, OwnedBy  FROM PROTO.dbo.ProjectSchedule";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();

                // Map all products from database to a list of type Product defined in Models.
                // this is done by using Async method which is also used on the GetByIdAsync
                var result = await connection.QueryAsync<ProjectSchedule>(sql);
                return result.ToList();
            }
        }



    }
}
