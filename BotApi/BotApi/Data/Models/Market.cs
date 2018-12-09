using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotApi.Data.Models
{
    [Table("markets")]
    public class Market: BaseWithIncrementsId
    {
        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string ApiEndpoint { get; set; }
    }
}
