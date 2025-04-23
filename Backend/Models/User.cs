using Backend.Dtos;

namespace Backend.Models
{
    public class User {
        public Guid Id{get; set;}
        public required string Name{get; set;}
        public required string Email {get; set;}
        public required string Password{get; set;}
        public List<Guid>? Favourites{get; set;}
        public UserRole Role {get; set;}
    }

    public enum UserRole
    {
        BackendEmployee,
        ProductSeller ,
        Customer
    }
}
