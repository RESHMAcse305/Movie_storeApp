using System.ComponentModel.DataAnnotations;

namespace Movie_StoreApp.Models
{
    public class AdminInsert
    {
        
        public int Admin_Id { get; set; }
        [Required(ErrorMessage ="name is required")]
        public string? AdminName { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string? Admin_Address { get; set; }
        [Range(10,10,ErrorMessage = "number must be in the limit")]
        public string? Admin_Phone { get; set; }
        [Required(ErrorMessage = "email is required")]
        public string? Admin_email { get; set; }
        [Required(ErrorMessage = "Username  is required")]
        public string? Username { get; set; }
        public string? Password { get; set; }
        [Required(ErrorMessage = "UserType is required")]
        public string? UserType { get; set; }
    }
    
public class Genre
{
    public int GenreId { get; set; }
        [Required(ErrorMessage = "GenreName  is required")]
        public string GenreName { get; set; }
        [Required(ErrorMessage = "Description  is required")]
        public string Genre_Description { get; set; }
    }
    public class Director
    {
        public int DirectorId { get; set; }
        [Required(ErrorMessage = "DirectorName  is required")]
        public string DirectorName { get; set; }
        [Required(ErrorMessage = "Details  is required")]
        public string DirectorDetails{ get; set; }
        [Required(ErrorMessage = "Filimograpy  is required")]
        public string Filimograpy { get; set; }
    }


    public class MovieInsert
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
        public string? GenreName { get; set; } 
        public string? DirectorName { get; set; }

    }


}
