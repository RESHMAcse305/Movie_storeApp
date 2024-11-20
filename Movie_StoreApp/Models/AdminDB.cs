
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Data;
using System.Data.SqlClient;


namespace Movie_StoreApp.Models
{
    public class AdminDB
    {
        SqlConnection con = new SqlConnection(@"server=LAPTOP-FKULG3C3\SQLEXPRESS;database=Movie_store;Integrated security=true");

        public string AdminDBInsert(AdminInsert obcls)
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

                SqlCommand cmd = new SqlCommand("sp_Admin_Reg", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@aid", reg_id);
                cmd.Parameters.AddWithValue("@aname", obcls.AdminName);
                cmd.Parameters.AddWithValue("@aaddr", obcls.Admin_Address);
                cmd.Parameters.AddWithValue("@aphone", obcls.Admin_Phone);
                cmd.Parameters.AddWithValue("@aemail", obcls.Admin_email);
                int s = cmd.ExecuteNonQuery();
                if (s == 1)
                {
                    SqlCommand cmd2 = new SqlCommand("sp_loginInsert", con);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@rid", reg_id);
                    cmd2.Parameters.AddWithValue("@una", obcls.Username);
                    cmd2.Parameters.AddWithValue("@pass", obcls.Password);
                    cmd2.Parameters.AddWithValue("@utype", "Admin");
                    cmd2.ExecuteNonQuery();
                    con.Close();
                    return ("admin inserted successfully");
                }

                return ("registered successfully");


            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ex.Message.ToString();
            }
        }

        public string InsertGenre(Genre clsobj)
        {
            try
            {
                SqlCommand cmd3 = new SqlCommand("sp_genre", con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@gname", clsobj.GenreName);
                cmd3.Parameters.AddWithValue("@gdes", clsobj.Genre_Description);
                con.Open();
                cmd3.ExecuteNonQuery();
                con.Close();
                return ("genre inserted successfully");


            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ex.Message.ToString();
            }
        }
        public string InsertDirector(Director clsobj)
        {
            try
            {
                SqlCommand cmd3 = new SqlCommand("sp_Director", con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@dname", clsobj.DirectorName);
                cmd3.Parameters.AddWithValue("@ddetails", clsobj.DirectorDetails);
                cmd3.Parameters.AddWithValue("@dfilmograpy", clsobj.Filimograpy);
                con.Open();
                cmd3.ExecuteNonQuery();
                con.Close();
                return ("Director inserted successfully");


            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ex.Message.ToString();
            }
        }
        public string MovieInsert(MovieInsert clsobj)
        {
            try
            {
                SqlCommand cmd4 = new SqlCommand("sp_movieInsert", con);
                cmd4.CommandType = CommandType.StoredProcedure;
                cmd4.Parameters.AddWithValue("@Title", clsobj.Movie_Title);
                cmd4.Parameters.AddWithValue("@ReleaseDate", clsobj.ReleaseDate);
                cmd4.Parameters.AddWithValue("@GenreId", clsobj.GenreId);
                cmd4.Parameters.AddWithValue("@DirectorId", clsobj.DirectorId);
                cmd4.Parameters.AddWithValue("@Actors", clsobj.Actors);
                cmd4.Parameters.AddWithValue("@Description", clsobj.Description);
                cmd4.Parameters.AddWithValue("@duration", clsobj.Duration);
                cmd4.Parameters.AddWithValue("@rating", clsobj.Rating);
                cmd4.Parameters.AddWithValue("@image", clsobj.Poster_Image);
                con.Open();
                cmd4.ExecuteNonQuery();
                con.Close();
                return ("movie inserted successfully");

            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ex.Message.ToString();
            }

        }

        public List<Genre> GetGenres()
        {
            var genre = new List<Genre>();

            SqlCommand cmd5 = new SqlCommand("sp_genreDrop", con);
            cmd5.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader reader = cmd5.ExecuteReader();
            while (reader.Read())
            {
                genre.Add(new Genre
                {
                    GenreId = reader.GetInt32(0),
                    GenreName = reader.GetString(1)
                    
                });
            }
            con.Close();
            return genre;

        }


        public List<Director> GetDirectors()
        {
            var directors = new List<Director>();

            SqlCommand cmd6 = new SqlCommand("sp_GetDirectores", con);
            cmd6.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd6.ExecuteReader();
            while (dr.Read())
            {
                directors.Add(new Director
                {
                    DirectorId = dr.GetInt32(0),
                    DirectorName = dr.GetString(1)
                   
                });
            }
            con.Close();
            return directors;

        }

        public List<MovieInsert> SelectMovieList()
        {
            var list = new List<MovieInsert>();
            try
            {
                SqlCommand cmd6 = new SqlCommand("sp_MovieList", con);
                cmd6.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd6.ExecuteReader();
                while (dr.Read())
                {
                    var o = new MovieInsert
                    {
                        Movie_Id = Convert.ToInt32(dr["Movie_Id"]),
                        Movie_Title = dr["Title"].ToString(),
                        ReleaseDate = dr["Release_Date"].ToString(),
                        Actors = dr["Movie_Actors"].ToString(),
                        GenreName = dr["Genre_Name"].ToString(),
                        DirectorName = dr["Director_Name"].ToString(),
                        Poster_Image = dr["Poster_Image"].ToString()

                    };
                    list.Add(o);


                }
                con.Close();
                return list;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }
        }

        public string DeleteMovie(int id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_delete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return ("movie deleted successfully");
            } catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }
        }
        public MovieInsert MovieSelect(int id)
        {
            var getdata = new MovieInsert();
            try
            {
                SqlCommand cmd7 = new SqlCommand("sp_selectId", con);
                cmd7.CommandType = CommandType.StoredProcedure;
                cmd7.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader dr1 = cmd7.ExecuteReader();
                while (dr1.Read())
                {
                    getdata = new MovieInsert
                    {
                        Movie_Id = Convert.ToInt32(dr1["Movie_Id"]),
                        Movie_Title = dr1["Title"].ToString(),
                        ReleaseDate = dr1["Release_Date"].ToString(),
                        Actors = dr1["Movie_Actors"].ToString(),
                        GenreId = Convert.ToInt32(dr1["Genre_Id"]),
                        DirectorId = Convert.ToInt32(dr1["Director_Id"]),
                        Description = dr1["Movie_Description"].ToString(),
                        Duration = dr1["Duration"].ToString(),
                        Rating = dr1["Rating"].ToString(),
                        Poster_Image = dr1["Poster_Image"].ToString()
                    };
                }
                con.Close();
                return (getdata);
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }   
    }
    public string UpdMovie(MovieInsert movie)
        {
            string retVal = "";
            try
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateMovie", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", movie.Movie_Id);
                cmd.Parameters.AddWithValue("@Actors", movie.Actors);
                cmd.Parameters.AddWithValue("@Description", movie.Description);
                cmd.Parameters.AddWithValue("@Duration", movie.Duration);
                cmd.Parameters.AddWithValue("@Rating", movie.Rating);
                if (!string.IsNullOrEmpty(movie.Poster_Image))
                {
                    cmd.Parameters.AddWithValue("@Poster_Image", movie.Poster_Image);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Poster_Image", DBNull.Value);
                }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                retVal = "ok ...updated..";
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return (ex.Message);
            }
            return (retVal);
        }
    }




}
