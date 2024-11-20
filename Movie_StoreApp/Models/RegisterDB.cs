
using System.Data;
using System.Data.SqlClient;
namespace Movie_StoreApp.Models
{
    public class RegisterDB
    {
        SqlConnection con = new SqlConnection(@"server=LAPTOP-FKULG3C3\SQLEXPRESS;database=Movie_store;Integrated security=true");
        public string InsertDB(Registration obcls)
        {
            int reg_id = 0;
            try
            {
                SqlCommand cmd1 = new SqlCommand("sp_maxId", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                con.Open();
                object regidResult = cmd1.ExecuteScalar();

                if (regidResult == null || string.IsNullOrWhiteSpace(regidResult.ToString()))
                {
                    reg_id = 1;
                }
                else
                {
                    reg_id = Convert.ToInt32(regidResult) + 1;
                }

                SqlCommand cmd = new SqlCommand("sp_userReg", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uid", reg_id);
                    cmd.Parameters.AddWithValue("@uname", obcls.Name);
                    cmd.Parameters.AddWithValue("@uadd", obcls.Address);
                    cmd.Parameters.AddWithValue("@uemail", obcls.Email);
                    cmd.Parameters.AddWithValue("@uphone", obcls.Phone);
                   int s= cmd.ExecuteNonQuery();
                if(s==1)
                {
                    SqlCommand cmd2 = new SqlCommand("sp_loginInsert", con);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@rid", reg_id);
                    cmd2.Parameters.AddWithValue("@una", obcls.Username);
                    cmd2.Parameters.AddWithValue("@pass", obcls.Password);
                    cmd2.Parameters.AddWithValue("@utype", "user");
                    cmd2.ExecuteNonQuery();
                    con.Close();
                    return ("inserted successfully");
                }

                return ("registered successfully");


            }
            catch (Exception ex)
            {
                if(con.State == ConnectionState.Open)
                {
                    con.Close ();
                }
                return ex.Message.ToString();
            }
        }

    }
}
