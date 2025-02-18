using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    [Table("Item")]
    public class DtoItem
    {
        public int ID {  get; set; }

        public string? ItemId { get; set; }
        public string? Name {  get; set; }

        public string? Description { get; set; }
        public string? Url {  get; set; }

        public double? SellPrice { get; set; }

        public double? BuyPrice { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        public bool Active {  get; set; }

    }
}
