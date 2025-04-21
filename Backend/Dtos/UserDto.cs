namespace Backend.Dtos { 
    

public class UserDto {
    public Guid Id{get; set;}
        public required string Name{get; set;}
        public required string Email {get; set;}
        public required string Password{get; set;}
        // Instead of having a list of ids or productdots of the listings we are going
        // to call it seperately when needed(this api will require the user id and returns list) 
}
}