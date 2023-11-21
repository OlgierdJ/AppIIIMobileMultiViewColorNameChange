using CoreLib.Interfaces;

namespace CoreLib.Models
{
    public abstract class BaseEntity<TId> : IEntity<TId>
    {
        public TId Id { get; set; }
        object IEntity.Id
        {
            get { return Id; }
            set { Id = (TId)value; }
        }

    }
}
