using Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using ServiceLib.Interfaces;

namespace WebApplicationRazor.Pages.TodoList
{
    public class DetailsModel : PageModel
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IValidationService _iValidationService;
        private readonly IDataService _iDataService;

        public DetailsModel(IMemoryCache memoryCache, IValidationService iValidationService, IDataService iDataService)
        {
            _memoryCache = memoryCache;
            _iValidationService = iValidationService;
            _iDataService = iDataService;
        }

        public Todo Todo { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _iDataService.GetListAsync<Todo>(_memoryCache);

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                Todo = result.FirstOrDefault(x => x.Id.Equals(id));
            }
            return Page();
        }
    }
}
