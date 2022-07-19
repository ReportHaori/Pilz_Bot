using System.ComponentModel.DataAnnotations;

namespace GachaBot.Database
{
    public partial class User
    {
        [Key]
        public int Id { get; set; }
        public ulong UId { get; set; }
        public int Coins { get; set; }
    }
}
