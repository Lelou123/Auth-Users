using Microsoft.AspNetCore.Mvc;

namespace OrderFlow.Controllers;

public class OrderTestController : ControllerBase
{
    public OrderTestController() { }
    
    public async Task<IActionResult> Index() => Ok();
}