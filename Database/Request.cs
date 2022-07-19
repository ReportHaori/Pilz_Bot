using System.ComponentModel.DataAnnotations;

namespace GachaBot.Database
{
    public partial class Request
    {
        [Key]
        public int Id { get; set; }
        public ulong UId { get; set; }
        public string RequestName { get; set; }
        public string RequestedItem { get; set; }
        public int Quantity { get; set; }
        public int Coins { get; set; }
    }
}
