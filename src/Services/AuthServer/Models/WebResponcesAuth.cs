namespace Auth.Models
{
    public class WebResponsesAuth
    {
        public static WebResponse authResponseSuccessUserExist = new() { Status = "Success", Message = "User with that login and password exist" };

        public static WebResponse authResponseSuccessUserCreate = new() { Status = "Success", Message = "User created successfully" };

        public static WebResponse authResponseSuccessUserUpdate = new() { Status = "Success", Message = "User updated successfully" };

        public static WebResponse authResponseSuccessUserDelete = new() { Status = "Success", Message = "User deleted successfully" };

        public static WebResponse authResponseSuccessUserAddToWorkspace = new() { Status = "Success", Message = "User successfully added to workspace" };

        public static WebResponse authResponseSuccessUserRemoveFromWorkspace = new() { Status = "Success", Message = "User successfully removed from workspace" };

        public static WebResponse authResponseErrorUserAddToWorkspace = new() { Status = "Error", Message = "User already added to workspace" };

        public static WebResponse authResponseErrorUserRemoveFromWorkspace = new() { Status = "Error", Message = "User not found in input workspace" };

        public static WebResponse authResponseErrorEmail = new() { Status = "Error", Message = "Email is null" };

        public static WebResponse authResponseErrorRole = new() { Status = "Error", Message = "userRole is null" };

        public static WebResponse authResponseErrorRoleAdmin = new() { Status = "Error", Message = "No permissions to create Admin" };

        public static WebResponse authResponseErrorNoRoleInDb = new() { Status = "Error", Message = $"Input Role do not exists in database" };

        public static WebResponse authResponseErrorUserExist = new() { Status = "Error", Message = "User already exists" };

        public static WebResponse authResponseErrorUserCreate = new() { Status = "Error", Message = "User create failed! Please check user details and try again" };

        public static WebResponse authResponseErrorUserUpdate = new() { Status = "Error", Message = "User update failed! Please check user details and try again" };

        public static WebResponse authResponseErrorUserDelete = new() { Status = "Error", Message = "User delete failed! Please check user details and try again" };

        public static WebResponse authResponseErrorUser = new() { Status = "Error", Message = "User is not found" };

        public static WebResponse authResponseErrorPassword = new() { Status = "Error", Message = "Incorrect Password" };

        public static WebResponse authResponseErrorUnauthorized = new() { Status = "Error", Message = "Invalid input data" };

        public static WebResponse authResponseErrorToken = new() { Status = "Error", Message = "Invalid token model" };

        public static WebResponse authResponseErrorAccessToken = new() { Status = "Error", Message = "Invalid input access token" };

        public static WebResponse authResponseErrorRefreshToken = new() { Status = "Error", Message = "Invalid input refresh token, check data and expiration date" };

        public static WebResponse authResponseErrorClaimsPrincipal = new() { Status = "Error", Message = "Invalid Claim Identity Data" };
    }
}