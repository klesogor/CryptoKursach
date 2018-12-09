
using System.ComponentModel.DataAnnotations;

namespace BotApi.Data.Models
{
    public abstract class BaseWithIncrementsId
    {
        [Key]
        public int Id { get; set; }
    }
}
