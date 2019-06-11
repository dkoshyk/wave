﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using SwissKnife.API.Data.Entities;

namespace SwissKnife.API.Queries
{
    public class QueriesService : IQueriesService
    {
        private readonly string _connectionString;

        public QueriesService(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("message", nameof(connectionString));

            _connectionString = connectionString;
        }

        public async Task<IList<Product>> GetAll()
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                var result = await conn.QueryAsync<Product>("SELECT * FROM Products");

                return result.ToList();
            }
        }
    }
}