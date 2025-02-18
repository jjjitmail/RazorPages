using Dto.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class DtoItemRelations
    {
        public int ID {  get; set; }
        public int ParentId { get; set; }
        public int ChildId { get; set; }
        public SiteType SiteType { get; set; }
    }
}
