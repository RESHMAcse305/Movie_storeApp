using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Movie_StoreApp.Models
{
    public class LoginDB
    {
        SqlConnection con = new SqlConnection(@"server=LAPTOP-FKULG3C3\SQLEXPRESS;database=Movie_store;Integrated security=true");

        public string LoginDb(Login objcls, ITempDataDictionary tempData)
        {
            try
            {
                // Open connection once at the beginning
                con.Open();

                // First command: sp_Login
                SqlCommand cmd = new SqlCommand("sp_Login", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@una", objcls.Username);
                cmd.Parameters.AddWithValue("@pass", objcls.Password);

                string cid = cmd.ExecuteScalar()?.ToString();
                int regid = Convert.ToInt32(cid);
                if (regid > 0)
                {

                    // Second command: sp_getId
                    SqlCommand cmd1 = new SqlCommand("sp_getId", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@una", objcls.Username);
                    cmd1.Parameters.AddWithValue("@pass", objcls.Password);

                    string rid = cmd1.ExecuteScalar()?.ToString();
                    if (!string.IsNullOrEmpty(rid))
                    {
                        tempData["uid"] = rid;
                    }

                    // Third command: sp_logintype
                    SqlCommand cmd2 = new SqlCommand("sp_logintype", con);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@una", objcls.Username);
                    cmd2.Parameters.AddWithValue("@pass", objcls.Password);

                    string userType = cmd2.ExecuteScalar()?.ToString();
                    tempData["type"] = userType;

                    return userType;

                }
                else
                {
                    return "Invalid credentials";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                // Ensure connection is closed in all cases
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

    }
}
