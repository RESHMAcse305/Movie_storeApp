using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Movie_StoreApp.Models
{
    public class UserDB
    {
        SqlConnection con = new SqlConnection(@"server=LAPTOP-FKULG3C3\SQLEXPRESS;database=Movie_store;Integrated security=true");

        public List<MovieInsert> MovieList()
        {
            var list = new List<MovieInsert>();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_SelectAllimg ", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var o = new MovieInsert
                    {
                        Movie_Id = Convert.ToInt32(dr["Movie_Id"]),
                        Movie_Title = dr["Title"].ToString(),
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

        public Movie SelectMovie(int id)
        {
            var getdata = new Movie();
            try
            {
                SqlCommand cmd2 = new SqlCommand("sp_selectMovie", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader dr1 = cmd2.ExecuteReader();
                while (dr1.Read())
                {
                    getdata = new Movie
                    {
                        Movie_Id = Convert.ToInt32(dr1["Movie_Id"]),
                        Movie_Title = dr1["Title"].ToString(),
                        ReleaseDate = dr1["Release_Date"].ToString(),
                        Actors = dr1["Movie_Actors"].ToString(),
                        Genre_Name = dr1["Genre_Name"].ToString(),
                        Director_Name = dr1["Director_Name"].ToString(),
                        Duration = dr1["Duration"].ToString(),
                        Rating = dr1["Rating"].ToString()
                       
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

        public string AddReviews(Review clsobj)
        {
            try
            {
                clsobj.CreatedDate = DateTime.Now;
                clsobj.status = 0;
                SqlCommand cmd3 = new SqlCommand("sp_AddReview",con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@mid",clsobj.Movie_Id);
                cmd3.Parameters.AddWithValue("@uid",clsobj.User_Id);
                cmd3.Parameters.AddWithValue("@review",clsobj.ReviewText);
                cmd3.Parameters.AddWithValue("@rating",clsobj.Rating);
                cmd3.Parameters.AddWithValue("@created",clsobj.CreatedDate);
                cmd3.Parameters.AddWithValue("@status", clsobj.status);
                con.Open();
                cmd3.ExecuteNonQuery(); 
                con.Close() ;
                return ("review inserted successfully");


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

        public List<Movie> SearchMovies(string searchTerm)
        {
            var movieList = new List<Movie>();

            try
            {
                SqlCommand cmd = new SqlCommand("SearchStores", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SearchTerm", searchTerm ?? string.Empty);
                con.Open();
                SqlDataReader dr1 = cmd.ExecuteReader();
                while (dr1.Read())
                {
                    var movie = new Movie
                    {
                        Movie_Id = Convert.ToInt32(dr1["Movie_Id"]),
                        Movie_Title = dr1["Title"].ToString(),
                        //Genre_Name = dr1["Genre_Name"].ToString(),
                        //Director_Name = dr1["Director_Name"].ToString(),
                        ReleaseDate = dr1["Release_Date"].ToString(),
                        Actors = dr1["Movie_Actors"].ToString()
                    };
                    movieList.Add(movie);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
               
            }

            return movieList;
        }

    }
}
