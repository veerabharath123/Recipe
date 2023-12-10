﻿namespace Recipe.Helpers
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Result { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        public string? Error { get; set; }

        public ApiResponse()
        {
            Success = true;
            StatusCode = 200;
        }
    }
}
