namespace Auth.Models
{
    public class WebResponcesAuth
    {
        public static WebResponce authResponceSuccessUserExist = new() { Status = "Success", Message = "User with that login and password exist" };

        public static WebResponce authResponceSuccessUserCreate = new() { Status = "Success", Message = "User created successfully" };

        public static WebResponce authResponceSuccessUserUpdate = new() { Status = "Success", Message = "User updated successfully" };

        public static WebResponce authResponceSuccessUserDelete = new() { Status = "Success", Message = "User deleted successfully" };

        public static WebResponce authResponceSuccessUserAddToWorkspace = new() { Status = "Success", Message = "User successfully added to workspace" };

        public static WebResponce authResponceSuccessUserRemoveFromWorkspace = new() { Status = "Success", Message = "User successfully removed from workspace" };

        public static WebResponce authResponceErrorUserAddToWorkspace = new() { Status = "Error", Message = "User already added to workspace" };

        public static WebResponce authResponceErrorUserRemoveFromWorkspace = new() { Status = "Error", Message = "User not found in input workspace" };

        public static WebResponce authResponceErrorEmail = new() { Status = "Error", Message = "Email is null" };

        public static WebResponce authResponceErrorRole = new() { Status = "Error", Message = "userRole is null" };

        public static WebResponce authResponceErrorRoleAdmin = new() { Status = "Error", Message = "No permissions to create Admin" };

        public static WebResponce authResponceErrorNoRoleInDb = new() { Status = "Error", Message = $"Input Role do not exists in database" };

        public static WebResponce authResponceErrorUserExist = new() { Status = "Error", Message = "User already exists" };

        public static WebResponce authResponceErrorUserCreate = new() { Status = "Error", Message = "User create failed! Please check user details and try again" };

        public static WebResponce authResponceErrorUserUpdate = new() { Status = "Error", Message = "User update failed! Please check user details and try again" };

        public static WebResponce authResponceErrorUserDelete = new() { Status = "Error", Message = "User delete failed! Please check user details and try again" };

        public static WebResponce authResponceErrorUser = new() { Status = "Error", Message = "User is not found" };

        public static WebResponce authResponceErrorPassword = new() { Status = "Error", Message = "Incorrect Password" };

        public static WebResponce authResponceErrorUnauthorized = new() { Status = "Error", Message = "Invalid input data" };

        public static WebResponce authResponceErrorToken = new() { Status = "Error", Message = "Invalid token model" };

        public static WebResponce authResponceErrorAccessToken = new() { Status = "Error", Message = "Invalid input access token" };

        public static WebResponce authResponceErrorRefreshToken = new() { Status = "Error", Message = "Invalid input refresh token, check data and expiration date" };

        public static WebResponce authResponceErrorClaimsPrincipal = new() { Status = "Error", Message = "Invalid Claim Identity Data" };
    }
}