namespace Auth.Models
{
    public class WebResponsesWorkspaces
    {
        public static WebResponse WebResponseSuccessCreate = new() { Status = "Success", Message = "Workspace successfully created" };

        public static WebResponse WebResponseSuccessUpdate = new() { Status = "Success", Message = "Workspace successfully updated" };

        public static WebResponse WebResponseSuccessDelete = new() { Status = "Success", Message = "Workspace successfully deleted" };

        public static WebResponse WebResponseErrorAlreadyExist = new() { Status = "Error", Message = "Workspace with input name is already exist" };

        public static WebResponse WebResponseErrorNotFound = new() { Status = "Error", Message = "Workspace with input name is not found" };
    }
}