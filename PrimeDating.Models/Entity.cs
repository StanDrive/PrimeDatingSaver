using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeDating.Models
{
    public abstract class Entity : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
