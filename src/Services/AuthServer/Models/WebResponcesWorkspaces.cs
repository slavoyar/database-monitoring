namespace Auth.Models
{
    public class WebResponcesWorkspaces
    {
        public static WebResponce WebResponceSuccessCreate = new() { Status = "Success", Message = "Workspace successfully created" };

        public static WebResponce WebResponceSuccessUpdate = new() { Status = "Success", Message = "Workspace successfully updated" };

        public static WebResponce WebResponceSuccessDelete = new() { Status = "Success", Message = "Workspace successfully deleted" };

        public static WebResponce WebResponceErrorAlreadyExist = new() { Status = "Error", Message = "Workspace with input name is already exist" };

        public static WebResponce WebResponceErrorNotFound = new() { Status = "Error", Message = "Workspace with input name is not found" };
    }
}