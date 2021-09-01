using CommonInterfaces.Entities;
using CommonInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Data
{
    public class BetData : IBet
    {
        #region Public Methods
        public bool CreateBet(Bet bet)
        {
            bool result = false;
            try 
            {
                SqlConnection con = new SqlConnection(Environment.GetEnvironmentVariable("ConnectionString"));
                SqlCommand cmd = new SqlCommand("sp_CreateBet", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idRoulette", bet.idRoulette);
                cmd.Parameters.AddWithValue("@idUser", bet.idUser);
                cmd.Parameters.AddWithValue("@betValue", bet.betValue);
                if (bet.betNumber.Equals(null))
                    cmd.Parameters.AddWithValue("@betNumber", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@betNumber", bet.betNumber);
                if (bet.betColor.Equals(null))
                    cmd.Parameters.AddWithValue("@betColor", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@betColor", bet.betColor);
                con.Open();
                int responseBd = cmd.ExecuteNonQuery();
                con.Close();
                if (responseBd == 1)
                    result = true;
            }
            catch(Exception ex)
            {

            }

            return result;
        }

        public List<Bet> GetListBetsByRoulette(Bet bet)
        {
            List<Bet> result = new List<Bet>();
            try
            {
                SqlConnection con = new SqlConnection(Environment.GetEnvironmentVariable("ConnectionString"));
                SqlCommand cmd = new SqlCommand("sp_GetBetsXRoulette", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idRoulette", bet.idRoulette);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Bet auxBet = new Bet()
                        {
                            id = (int)reader[0],
                            creationDate = (DateTime)reader[1],
                            state = (int)reader[2],
                            idRoulette = (int)reader[3],
                            idUser = (string)reader[4],
                            betValue = (decimal)reader[5],
                            betNumber = reader[6].Equals(DBNull.Value) ? null : (int?)reader[6],
                            betColor = reader[7].Equals(DBNull.Value) ? null : (int?)reader[7],
                        };
                        result.Add(auxBet);
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public bool UpdateWinnerBetsXNumber(Bet bet)
        {
            bool result = false;
            try
            {
                SqlConnection con = new SqlConnection(Environment.GetEnvironmentVariable("ConnectionString"));
                SqlCommand cmd = new SqlCommand("sp_UpdateWinnerBetsXNumber", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idRoulette",bet.idRoulette);
                cmd.Parameters.AddWithValue("@betNumber", bet.betNumber);
                con.Open();
                int responseBd = cmd.ExecuteNonQuery();
                con.Close();
                result = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public bool UpdateWinnerBetsXColor(Bet bet)
        {
            bool result = false;
            try
            {
                SqlConnection con = new SqlConnection(Environment.GetEnvironmentVariable("ConnectionString"));
                SqlCommand cmd = new SqlCommand("sp_UpdateWinnerBetsXColor", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idRoulette", bet.idRoulette);
                cmd.Parameters.AddWithValue("@betColor", bet.betColor);
                con.Open();
                int responseBd = cmd.ExecuteNonQuery();
                con.Close();
                result = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public bool UpdateLosserBetsXNumber(Bet bet)
        {
            bool result = false;
            try
            {
                SqlConnection con = new SqlConnection(Environment.GetEnvironmentVariable("ConnectionString"));
                SqlCommand cmd = new SqlCommand("sp_UpdateLosserBetsXNumber", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idRoulette", bet.idRoulette);
                cmd.Parameters.AddWithValue("@betNumber", bet.betNumber);
                con.Open();
                int responseBd = cmd.ExecuteNonQuery();
                con.Close();
                result = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public bool UpdateLosserBetsXColor(Bet bet)
        {
            bool result = false;
            try
            {
                SqlConnection con = new SqlConnection(Environment.GetEnvironmentVariable("ConnectionString"));
                SqlCommand cmd = new SqlCommand("sp_UpdateLosserBetsXColor", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idRoulette", bet.idRoulette);
                cmd.Parameters.AddWithValue("@betColor", bet.betColor);
                con.Open();
                int responseBd = cmd.ExecuteNonQuery();
                con.Close();
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
