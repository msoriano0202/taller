using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
//using Taller.Common.Models;
using Taller.Contracts.Managers;
using Taller.Domain;
using Taller.Dto.Request;
using Taller.Dto.Response;

namespace Taller.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class CarsController : Controller
    {
        private readonly ICarManager _carManager;
        private readonly IMapper _mapper;

        public CarsController(ICarManager carManager, IMapper mapper)
        {
            _carManager = carManager;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CarResponse>))]
        public ActionResult<List<CarResponse>> GetAllCars()
        {
            var users = _carManager.GetAllCars();
            var response = _mapper.Map<List<CarResponse>>(users);
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CarResponse))]
        public ActionResult<CarResponse> GetCarById([FromRoute] int id)
        {
            var car = _carManager.GetCarById(id);
            if (car == null)
                return NotFound();

            return Ok(_mapper.Map<CarResponse>(car));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> CreateCar([FromBody] CreateCarRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var car = _mapper.Map<Car>(request);
            var response = _carManager.AddCar(car);
            if (response) return Ok(response);

            return new StatusCodeResult(500);
        }

        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> DeleteCarById([FromRoute] int id)
        {
            var response = _carManager.DeleteCarById(id);
            if(response) return Ok(response);

            return NotFound();
        }
    }
}
