using Dto;

namespace WebApplicationRazor.Models
{
    public class BestBuyViewModel
    {
        public List<DtoItem>? CurrentItems { get; set; }

        public List<DtoItem>? PreviousItems { get; set; }

        public List<DtoItem>? NewItems { get; set; }
    }
}
