using Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using ServiceLib.Interfaces;

namespace WebApplicationRazor.Pages.TodoList
{
    public class EditModel : PageModel
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IValidationService _iValidationService;
        private readonly IDataService _iDataService;

        public EditModel(IMemoryCache memoryCache, IValidationService iValidationService, IDataService iDataService)
        {
            _memoryCache = memoryCache;
            _iValidationService = iValidationService;
            _iDataService = iDataService;
        }

        [BindProperty]
        public Todo Todo { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _iDataService.GetListAsync<Todo>(_memoryCache);
            var item = result?.FirstOrDefault(x => x.Id.Equals(id));

            if (item == null)
            {
                return NotFound();
            }
            
            Todo = item;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            bool isValid = await _iValidationService.ValidateForUpdateAsync(Todo, _memoryCache);

            if (!isValid)
            {
                return RedirectToPage("/Error");
            }

            var cache = await _iDataService.GetListAsync<Todo>(_memoryCache);
            if (cache != null)
            {
                IList<Todo> list = cache;
                list = list.Where(x => !x.Id.Equals(Todo.Id)).ToList();
                list.Add(Todo);

                var result = await _iDataService.UpdateCacheAsync(list, _memoryCache);

                if (!result)
                    return Page();
            }
            return RedirectToPage("./Index");
        }

    }
}
