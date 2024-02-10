using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    public class VersionApiController : BaseApiController
    {
    }
}
