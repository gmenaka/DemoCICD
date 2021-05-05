using DemoCiCd.Web.DbAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoCiCd.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DemoDbContext _demoDbContext;

        public EmployeeController(DemoDbContext demoDbContext)
        {
            _demoDbContext = demoDbContext;
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> Add([FromBody] Employee emp)
        {
            _demoDbContext.Employees.Add(emp);
            await _demoDbContext.SaveChangesAsync();

            return emp;
        }

        public async Task<IEnumerable<Employee>> GetAll() =>
            await _demoDbContext.Employees.ToArrayAsync();
        
    }
}
