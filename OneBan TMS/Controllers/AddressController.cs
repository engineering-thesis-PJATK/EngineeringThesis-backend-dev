using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OneBan_TMS.Interfaces;
using OneBan_TMS.Models;

namespace OneBan_TMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private IAddress Address{ get; init; }

        public AddressController(IAddress address)
        {
            this.Address = address;
        }
        
        [HttpGet("GetAddressById")]
        public IActionResult GetAddressById(int addressId)
        {
            if (addressId < 1)
                return BadRequest();

            Address singleAddress = Address.GetAddressById(addressId);
            if (singleAddress is null)
                return NotFound();
            
            return Ok(singleAddress);
        }
        
        [HttpGet("GetAllAddresses")]
        public IActionResult GetAllAddresses()
        {
            var addressList = Address.GetAllAddresses();
            if (addressList.Any())
                return Ok(addressList);
            else
                return NoContent();
        }
    }
}