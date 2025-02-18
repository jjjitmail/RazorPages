using Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using ServiceLib.Interfaces;

namespace WebApplicationRazor.Pages.TodoList
{
    public class IndexModel : PageModel
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IValidationService _iValidationService;
        private readonly IDataService _iDataService;

        public IndexModel(IMemoryCache memoryCache, IValidationService iValidationService, IDataService iDataService)
        {
            _memoryCache = memoryCache;
            _iValidationService = iValidationService;
            _iDataService = iDataService;
        }

        public IList<Todo> TodoList { get;set; } = default!;

        public async Task OnGetAsync()
        {
            TodoList = new List<Todo>();
            TodoList = await _iDataService.GetListAsync<Todo>(_memoryCache);
        }
    }
}
