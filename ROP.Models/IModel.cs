using System;

namespace ROP.Models
{
    public interface IModel<TKey>
    {
        TKey Id { get; }
        DateTime CreationTime { get; }
    }
}