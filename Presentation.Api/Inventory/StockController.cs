using Application.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Api.Inventory;

[ApiController]
[Route("[controller]")]
public class StockController : ControllerBase
{
    [HttpGet("GetProductStockInfo")]
    public ActionResult<ProductAvailabilityDto> GetProductStockInfo(string productid)
    {
        throw new NotImplementedException();
    }
}