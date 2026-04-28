using GoodHamburger.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.Controllers;

[ApiController]
[Route("api/menu", Name = "Menu")]
public class MenuController : ControllerBase
{
    private readonly IGetMenuService _getMenuService;

    public MenuController(IGetMenuService getMenuService)
    {
        _getMenuService = getMenuService;
    }

    [HttpGet(Name =  "GetMenu")]
    public async Task<IActionResult> GetMenu()
    {
        var response = await _getMenuService.GetMenuAsync();

        if (!response.IsSucess)
            return NotFound(response);

        return Ok(response);
    }
}