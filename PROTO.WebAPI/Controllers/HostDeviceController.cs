using Microsoft.AspNetCore.Mvc;
using PROTO.UseCase.Interfaces;
using PROTO.Core.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace PROTO.WebAPI.Controllers
{
    [Authorize(Roles="ro,rw")]
    [Route("api/[controller]")]
    [ApiController]
   
    public class HostDeviceController : Controller
    {
        //Unit of work to give access to the repositories
        private readonly IUnitOfWork _unitOfWork;
        public HostDeviceController(IUnitOfWork unitOfWork)
        {
            // Inject Unit Of Work to the contructor of the product controller
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// This endpoint returns all products from the repository matching a createdOn date
        /// </summary>
        /// <returns>List of product objects</returns>

        [Authorize(Roles = "ro,rw")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _unitOfWork.HostDevice.GetAllAsync();
            return Ok(data);
        }
        /// <summary>
        /// Gets all hosts with CreatedOn Date 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>List of matching servers</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /getByDate
        ///     {
        ///        "dateTime": "2022-11-05"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [Authorize(Roles = "ro,rw")]
        [Route("getByDate/{dateTime}")]
        [HttpGet]
        public async Task<IActionResult> GetAllByDate(DateTime dateTime)
        {
            var data = await _unitOfWork.HostDevice.GetAllByDateAsync(dateTime);
            return Ok(data);
        }
        /// <summary>
        /// This endpoint returns a single product by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product object</returns>
        [Authorize(Roles = "rw")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _unitOfWork.HostDevice.GetByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }

        /// <summary>
        /// This endpoint adds a product to the database based on Product model
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Status for creation</returns>
        [Authorize(Roles = "rw")]
        [HttpPost]
        public async Task<IActionResult> Add(HostDevice product)
        {
            var data = await _unitOfWork.HostDevice.AddAsync(product);
            return Ok(data);
        }

        /// <summary>
        /// This endpont deletes a product form the database by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status for deletion</returns>
        [Authorize(Roles = "rw")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _unitOfWork.HostDevice.DeleteAsync(id);
            return Ok(data);
        }

        /// <summary>
        /// This endpoint updates a product by ID
        /// </summary>
        /// <param name="HostDevice"></param>
        /// <returns>Status for update</returns>
        [Authorize(Roles = "rw")]
        [HttpPut]
        public async Task<IActionResult> Update(HostDevice dev)
        {
            var data = await _unitOfWork.HostDevice.UpdateAsync(dev);
            return Ok(data);
        }
    }
}
