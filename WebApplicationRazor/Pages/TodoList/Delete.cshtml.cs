using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using ServiceLib.Interfaces;


namespace WebApplicationRazor.Pages.TodoList
{
    public class DeleteModel : PageModel
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IValidationService _iValidationService;
        private readonly IDataService _iDataService;

        public DeleteModel(IMemoryCache memoryCache, IValidationService iValidationService, IDataService iDataService)
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

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                Todo = result.FirstOrDefault(x=> x.Id.Equals(id));
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (!id.HasValue)
            {
                return RedirectToPage("/Error");
            }

            var result = false;

            var list = await _iDataService.GetListAsync<Todo>(_memoryCache);
            var itemToDelete = list.FirstOrDefault(x=> x.Id.Equals(id));

            var isValid = await _iValidationService.ValidateForDeleteAsync(itemToDelete, _memoryCache);

            if (!isValid) 
            {
                return RedirectToPage("/Error");
            }

            IList<Todo> newList = list.Where(x => !x.Id.Equals(id)).ToList();

            result = await _iDataService.UpdateCacheAsync<Todo>(newList, _memoryCache);

            if (!result)
                return RedirectToPage("/Error");

            return RedirectToPage("./Index");
        }
    }
}
