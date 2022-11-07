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
    public class HostDeviceRepositorySP : IHostDeviceRepositorySP
    {
        // Make it possible to read a connection string from configuration
        private readonly IConfiguration _configuration;

        public HostDeviceRepositorySP(IConfiguration configuration)
        {
            // Injecting Iconfiguration to the contructor of the product repository
            _configuration = configuration;
        }

        /// <summary>
        /// This method adds a new product to the database using Dapper
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>int</returns>
        public async Task<int> AddAsync(HostDevice entity)
        {
            // Set the time to the current moment
            entity.CreatedOn = DateTime.Now;

            // Basic SQL statement to insert a product into the products table
            var sql = "Insert into PROTO.dbo.Host_Devices (Hostname,Domain,SerialNumber,CreatedOn) VALUES (@Hostname,@Domain,@SerialNumber,@CreatedOn)";

            // Sing the Dapper Connection string we open a connection to the database
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();

                // Pass the product object and the SQL statement into the Execute function (async)
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        /// <summary>
        /// This method deleted a product specified by an ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>int</returns>
        public async Task<int> DeleteAsync(int id)
        {
            var sql = "[PROTO].[dbo].[DeleteHostDevicesById]";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.Int64);
                var result = await connection.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        /// <summary>
        /// This method returns all products in database in a list object
        /// </summary>
        /// <returns>IEnumerable Product</returns>
        public async Task<IReadOnlyList<HostDevice>> GetAllAsync()
        {
            var sql = "[PROTO].[dbo].[GetAllHostDevices]";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();

                // Map all products from database to a list of type Product defined in Models.
                // this is done by using Async method which is also used on the GetByIdAsync
                var result = await connection.QueryAsync<HostDevice>(sql, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        /// <summary>
        /// This method returns all products in database in a list object where matching date
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>IEnumerable Product</returns>
        public async Task<IReadOnlyList<HostDevice>> GetAllByDateAsync(DateTime dateTime)
        {
            var sql = "[PROTO].[dbo].[GetHostDevicesByDate]";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@FindDate", dateTime, DbType.DateTime);

                // Map all products from database to a list of type Product defined in Models.
                // this is done by using Async method which is also used on the GetByIdAsync
                var result = await connection.QueryAsync<HostDevice>(sql, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
        /// <summary>
        /// This method returns a single product specified by an ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product</returns>
        public async Task<HostDevice> GetByIdAsync(int id)
        {
            var sql = "[PROTO].[dbo].[GetHostDevicesById]";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@Id", id, DbType.Int64);
                var result = await connection.QuerySingleOrDefaultAsync<HostDevice>(sql, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        /// <summary>
        /// This method updates a product specified by an ID. Added column won't be touched.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>int</returns>
        public async Task<int> UpdateAsync(HostDevice entity)
        {
            entity.CreatedOn = DateTime.Now;
            var sql = "UPDATE PROTO.dbo.Host_Devices SET Hostname = @Hostname, Domain = @Domain, SerialNumber = @SerialNumber, CreatedOn = @CreatedOn WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
    }
}
