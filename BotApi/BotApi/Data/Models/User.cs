using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotApi.Data.Models
{
    [Table("users")]
    public class User: BaseWithIncrementsId
    {
        public int ChatId { get; set; }

        [MaxLength(256)]
        public string Username { get; set; }

        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
