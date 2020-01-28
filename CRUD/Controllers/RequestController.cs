using System;
using System.Linq;
using DataAccess.DTOs;
using DataAccess.Repositories.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILogger<RequestController> _logger;

        public RequestController(IRepositoryWrapper repositoryWrapper, ILogger<RequestController> logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        // GET api/requests
        [HttpGet]
        [Authorize]
        public IQueryable<User> Get()
        {
            IQueryable<User> user = null;
            try
            {
                user = _repositoryWrapper.User.Get();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }

            return user;
        }
    }
}