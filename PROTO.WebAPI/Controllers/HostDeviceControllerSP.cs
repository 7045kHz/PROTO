using Microsoft.AspNetCore.Mvc;
using PROTO.UseCase.Interfaces;
using PROTO.Core.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace PROTO.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HostDeviceControllerSP : Controller
    {
        //Unit of work to give access to the repositories
        private readonly IUnitOfWorkSP _unitOfWorkSP;
        public HostDeviceControllerSP(IUnitOfWorkSP unitOfWorkSP)
        {
            // Inject Unit Of Work to the contructor of the product controller
            _unitOfWorkSP = unitOfWorkSP;
        }

        /// <summary>
        /// This endpoint returns all products from the repository matching a createdOn date
        /// </summary>
        /// <returns>List of product objects</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Get list of all Host Devices", Description = "Use Stored Procedure to get list of all Host_Devices")]
        [SwaggerResponse(200,"Successful execution of stored proc")]
        [SwaggerResponse(400, "Failed execution of stored proc")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _unitOfWorkSP.HostDevice.GetAllAsync();
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
        [Route("getByDate/{dateTime}")]
        [HttpGet]
        public async Task<IActionResult> GetAllByDate(DateTime dateTime)
        {
            var data = await _unitOfWorkSP.HostDevice.GetAllByDateAsync(dateTime);
            return Ok(data);
        }
        /// <summary>
        /// This endpoint returns a single product by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product object</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _unitOfWorkSP.HostDevice.GetByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }

        /// <summary>
        /// This endpoint adds a product to the database based on Product model
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Status for creation</returns>
        [HttpPost]
        public async Task<IActionResult> Add(HostDevice product)
        {
            var data = await _unitOfWorkSP.HostDevice.AddAsync(product);
            return Ok(data);
        }

        /// <summary>
        /// This endpont deletes a product form the database by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status for deletion</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _unitOfWorkSP.HostDevice.DeleteAsync(id);
            return Ok(data);
        }

        /// <summary>
        /// This endpoint updates a product by ID
        /// </summary>
        /// <param name="HostDevice"></param>
        /// <returns>Status for update</returns>
        [HttpPut]
        public async Task<IActionResult> Update(HostDevice dev)
        {
            var data = await _unitOfWorkSP.HostDevice.UpdateAsync(dev);
            return Ok(data);
        }
    }
}
