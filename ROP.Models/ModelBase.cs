using System;

namespace ROP.Models
{
    public abstract class ModelBase<TKey> : IModel<TKey>
    {
        public ModelBase(TKey id)
        {
            Id = id;
            CreationTime = DateTime.Now;
        }
        public TKey Id { get; private set; }
        public DateTime CreationTime { get; private set; }
    }
}