using System.ComponentModel.DataAnnotations;

namespace Movie_StoreApp.Models
{
    public class Registration
    {
        public  int User_Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? UserType { get; set; }
        public string?Umsg { get; set; }

    }
    public class MovieBox
    {
        public int Movie_Id { get; set; }
        public string Title { get; set; }
        public string? Genre_Name { get; set; }
        public string? Release_Date { get; set; }
        public string? Director_Name { get; set; }
        public string?Movie_Actors { get;set; }
    }
    public class Movie
    {
        public int Movie_Id { get; set; }
        [Required(ErrorMessage = "Title  is required")]
        public string? Movie_Title { get; set; }
        [Required(ErrorMessage = "ReleaseDate  is required")]
        public string? ReleaseDate { get; set; }

        public int GenreId { get; set; }

        public int DirectorId { get; set; }
        [Required(ErrorMessage = "Actors  is required")]
        public string Actors { get; set; }
        [Required(ErrorMessage = "Description  is required")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Duration  is required")]
        public string? Duration { get; set; }
        [Required(ErrorMessage = "Rating  is required")]
        public string? Rating { get; set; }

        public string? Poster_Image { get; set; }
        public IFormFile PosterFile { get; set; }
        public string? Genre_Name { get; set; }
        public string? Director_Name { get; set; }

    }
    public class Review
    {
        public int Movie_Id { get; set; }
        public int User_Id { get; set; }
        public string? ReviewText { get;set; }
        public string? Rating { get;set; }
        public DateTime CreatedDate { get; set; }
        public int status { get; set; }
    }
    public class MovieSearchViewModel
    {
        public string SearchTerm { get; set; }
        public List<Movie> Movies { get; set; }
    }


}
