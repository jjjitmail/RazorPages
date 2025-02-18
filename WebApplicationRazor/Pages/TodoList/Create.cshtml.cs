using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using ServiceLib.Interfaces;

namespace WebApplicationRazor.Pages.TodoList
{
    public class CreateModel : PageModel
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IValidationService _iValidationService;
        private readonly IDataService _iDataService;

        public CreateModel(IMemoryCache memoryCache, IValidationService iValidationService, IDataService iDataService)
        {
            _memoryCache = memoryCache;
            _iValidationService = iValidationService;
            _iDataService = iDataService;
        }
      

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Todo Todo { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("/Error");
            }

            bool isValid = await _iValidationService.ValidateForCreateAsync(Todo, _memoryCache);

            if (!isValid)
            {
                return RedirectToPage("/Error");
            }
            
            var cache = await _iDataService.GetListAsync<Todo>(_memoryCache);
            if (cache != null)
            {
                IList<Todo> list = cache;
                list.Add(Todo);

                var result = await _iDataService.UpdateCacheAsync(list, _memoryCache);

                if (!result)
                    return Page();
            }            
            
            return RedirectToPage("./Index");
        }
    }
}
