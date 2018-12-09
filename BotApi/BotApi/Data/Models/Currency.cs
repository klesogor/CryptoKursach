using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotApi.Data.Models
{
    [Table("currencies")]
    public class Currency: BaseWithIncrementsId
    {
        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string Symbol { get; set; }

        [MaxLength(256)]
        public string ImageUrl { get; set; }
    }
}
