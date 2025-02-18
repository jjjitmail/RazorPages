using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class BestBuyFilter
    {
        public Guid GuidId { get; set; }
        public string? Url {  get; set; }

        public List<DtoItem>? CurrentItems { get; set; }

        public List<DtoItem>? PreviousItems {  get; set; }

        public List<DtoItem>? NewItems { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
 