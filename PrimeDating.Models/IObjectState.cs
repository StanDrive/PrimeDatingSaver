using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeDating.Models
{
    public interface IObjectState
    {
        [NotMapped]
        ObjectState ObjectState { get; set; }
    }
}
