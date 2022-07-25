using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Data;

namespace PhoneBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public readonly PhonebookDbContext context;
        public IConfiguration configuration;

        public BaseController(PhonebookDbContext ctx, IConfiguration iConfig)
        {
            context = ctx;
            configuration = iConfig;
        }
    }
}
