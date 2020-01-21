using System.Collections.Generic;
using DataAccess.DTOs;
using DataAccess.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public EmployeeController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            var employees = _repositoryWrapper.Employee
                .Get(includeProperties: nameof(Department));

            return new string[] {"value1", "value2"};
        }
    }
}