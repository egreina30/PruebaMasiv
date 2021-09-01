using CommonInterfaces.Entities;
using CommonInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Data
{
    public class RouletteData : IRoulette
    {
        #region Public Methods
        public int CreateRoulette()
        {
            int idCreatedRoulette = 0;
            try
            {
                SqlConnection con = new SqlConnection(Environment.GetEnvironmentVariable("ConnectionString"));
                SqlCommand cmd = new SqlCommand("sp_InsertRoulette", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        idCreatedRoulette = (int)reader[0];
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {

            }

            return idCreatedRoulette;
        }

        public List<Roulette> GetRoulettes()
        {
            SqlConnection con = new SqlConnection(Environment.GetEnvironmentVariable("ConnectionString"));
            List<Roulette> ruletas = new List<Roulette>();
            try
            {
                using (con)
                {
                    SqlCommand command = new SqlCommand("sp_GetRoulettes", con);
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Roulette axuRuleta = new Roulette()
                            {
                                id = (int)reader[0],
                                creationDate = (DateTime)reader[1],
                                state = (int)reader[2],
                                openingDate = reader[3].Equals(DBNull.Value) ? null : (DateTime?)reader[3],
                                winningNumber = reader[4].Equals(DBNull.Value) ? null : (int?)reader[4],
                                closingDate = reader[5].Equals(DBNull.Value) ? null : (DateTime?)reader[5]
                            };
                            ruletas.Add(axuRuleta);
                        }
                    }
                    reader.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return ruletas;
        }

        public bool OpeningRoulette(Roulette roulette)
        {
            bool result = false;
            try
            {
                SqlConnection con = new SqlConnection(Environment.GetEnvironmentVariable("ConnectionString"));
                SqlCommand cmd = new SqlCommand("sp_OpeningRoulette", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", roulette.id);
                con.Open();
                int responseBd = cmd.ExecuteNonQuery();
                con.Close();
                if (responseBd == 1)
                    result = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public Roulette GetRouletteXId(Roulette roulette)
        {
            Roulette result = new Roulette();
            try
            {
                SqlConnection con = new SqlConnection(Environment.GetEnvironmentVariable("ConnectionString"));
                SqlCommand cmd = new SqlCommand("sp_GetRouletteXId", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", roulette.id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new Roulette()
                        {
                            id = (int)reader[0],
                            creationDate = (DateTime)reader[1],
                            state = (int)reader[2],
                            openingDate = reader[3].Equals(DBNull.Value) ? null : (DateTime?)reader[3],
                            winningNumber = reader[4].Equals(DBNull.Value) ? null : (int?)reader[4],
                            closingDate = reader[5].Equals(DBNull.Value) ? null : (DateTime?)reader[5]
                        };
                    }
                }
                con.Close();
            }
            catch(Exception ex)
            {

            }

            return result;
        }

        public bool SetClosingRoulette(Roulette roulette)
        {
            bool result = false;
            try
            {
                SqlConnection con = new SqlConnection(Environment.GetEnvironmentVariable("ConnectionString"));
                SqlCommand cmd = new SqlCommand("sp_ClosingRoulette", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", roulette.id);
                cmd.Parameters.AddWithValue("@winningNumber", roulette.winningNumber);
                con.Open();
                int responseBd = cmd.ExecuteNonQuery();
                con.Close();
                if (responseBd == 1)
                    result = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        #endregion
    }
}
