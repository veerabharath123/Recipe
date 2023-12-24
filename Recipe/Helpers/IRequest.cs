namespace Recipe.Helpers
{
    public interface IRequest
    {
        Task<ApiResponse<T>> ApiCallPost<T>(string controller, string method, object? obj, bool onAuth = false);
    }
}