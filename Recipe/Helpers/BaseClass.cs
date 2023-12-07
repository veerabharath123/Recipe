﻿namespace Recipe.Helpers
{
    public class BaseClass<T>
    {
        public decimal ID { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public TimeSpan? CREATED_TIME { get; set; }
        public TimeSpan? UPDATED_TIME { get; set; }
        public string? CREATED_USER { get; set; } = string.Empty;
        public string? UPDATED_USER { get; set; } = string.Empty;
        public string? DELETE_FLAG { get; set; }
        public string? ACTIVE { get; set; }
    }
}
