﻿namespace Ordering.Domain.Abstractions
{
    public interface IEntity<T> : IEntity
    {
        public T Id { get; set; }

    }   


    public interface IEntity
    {
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastModified { get; set; }
        
    }  
}
