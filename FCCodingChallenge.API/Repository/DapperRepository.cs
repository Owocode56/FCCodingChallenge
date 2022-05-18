using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCCodingChallenge.API.Repository
{
    public class DapperRepository : IDapperRepository
    {
        private readonly IConfiguration _config;
        private readonly string _conString;
        private string Connectionstring = "FCreditConnectionString";

        public DapperRepository(IConfiguration config)
        {
            _config = config;
        }
        public DapperRepository(string conString)
        {
            _conString = conString;
        }


        public void Dispose()
        {

        }

        public async Task<T> Execute<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            //using IDbConnection db = new SqlConnection(!string.IsNullOrEmpty(_conString) ? _conString : _config.GetConnectionString(Connectionstring));
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var trans = db.BeginTransaction();
                try
                {
                    result = db.ExecuteScalar<T>(sp, parms, commandType: commandType, transaction: trans);
                    if (trans.Connection != null)
                        trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }

        public async Task<T> Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            T result;
            using IDbConnection db = new SqlConnection(!string.IsNullOrEmpty(_conString) ? _conString : _config.GetConnectionString(Connectionstring));
            //using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
            if (db.State == ConnectionState.Closed)
                db.Open();
            using (var tran = db.BeginTransaction())
            {
                try
                {
                    result = (await db.QueryAsync<T>(sp, parms, commandType: commandType, transaction: tran)).FirstOrDefault();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
            return result;
        }

        public async Task<List<T>> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            List<T> result;
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));

            if (db.State == ConnectionState.Closed)
                db.Open();
            using (var tran = db.BeginTransaction())
            {
                try
                {
                    result = (await db.QueryAsync<T>(sp, parms, commandType: commandType, transaction: tran)).ToList();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }

            return result;
        }

        public async Task<T> BulkInsert<T>(DataTable dataTable, object parameter, string procedurename)
        {
            T result;
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                using var tran = db.BeginTransaction();
                try
                {
                    result = await db.ExecuteScalarAsync<T>(procedurename, parameter, commandType: CommandType.StoredProcedure, transaction: tran);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
            return result;
        }

        public async Task<T> BulkInsert<T>(DataTable dataTable, object parameter, string procedurename, bool userTransactionScope = true)
        {
            T result;
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                if (userTransactionScope)
                {
                    using var tran = db.BeginTransaction();
                    try
                    {
                        result = await db.ExecuteScalarAsync<T>(procedurename, parameter, commandType: CommandType.StoredProcedure, transaction: tran);
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw;
                    }
                }
                else
                {
                    try
                    {
                        result = await db.ExecuteScalarAsync<T>(procedurename, parameter, commandType: CommandType.StoredProcedure);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
            return result;
        }

        public DbConnection GetDbconnection()
        {
            return new SqlConnection(_config.GetConnectionString(Connectionstring));
        }

        public async Task<T> Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }

        public async Task<T> Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(Connectionstring));
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = (await db.QueryAsync<T>(sp, parms, commandType: commandType, transaction: tran)).FirstOrDefault();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }
    }
}
