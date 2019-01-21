using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Czar.Cms.Admin.Controllers
{
    [Authorize]
    public abstract class BaseController:Controller
    {
    }
}
