using CommonInterfaces.Entities;
using CommonInterfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Data
{
    public class UserData : IUser
    {
        #region Public Methods
        public bool CreateUser(User user)
        {
            bool result = false;
            try
            {
                SqlConnection con = new SqlConnection(Environment.GetEnvironmentVariable("ConnectionString"));
                SqlCommand cmd = new SqlCommand("sp_InsertUser", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@identification", user.identification);
                cmd.Parameters.AddWithValue("@nameUser", user.nameUser);
                cmd.Parameters.AddWithValue("@balance", user.balance);
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

        public User GetUserXId(User user)
        {
            User result = new User();
            try
            {
                SqlConnection con = new SqlConnection(Environment.GetEnvironmentVariable("ConnectionString"));
                SqlCommand cmd = new SqlCommand("sp_GetUserXId", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@identification", user.identification);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = new User()
                        {
                            identification = (string)reader[0],
                            nameUser = (string)reader[1],
                            balance = (decimal)reader[2]
                        };
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public bool UpdateBalanceUser(User user)
        {
            bool result = false;
            try
            {
                SqlConnection con = new SqlConnection(Environment.GetEnvironmentVariable("ConnectionString"));
                SqlCommand cmd = new SqlCommand("sp_UpdateBalanceUser", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@identification", user.identification);
                cmd.Parameters.AddWithValue("@balance", user.balance);
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
