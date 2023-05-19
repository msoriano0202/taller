using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Taller.Dto.Request;
using Taller.Web.Managers;
using Taller.Web.Models;

namespace Taller.Web.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarManager _carManager;
        private readonly IMapper _mapper;

        public CarsController(ICarManager carManager, IMapper mapper)
        {
            _carManager = carManager;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            var data = await _carManager.GetAllCarsAsync();
            var response = _mapper.Map<List<CarViewModel>>(data);

            return View(response);
        }

        public async Task<ActionResult> Details(int id)
        {
            var data = await _carManager.GetCarByIdAsync(id);
            if (data == null)
            {
                TempData["ErrorMessage"] = "This Car does not exist!";
                return RedirectToAction(nameof(Index));
            }

            data.Price = 0;
            if (TempData.ContainsKey("GuessPrice"))
            {
                data.Price = Convert.ToDecimal(TempData["GuessPrice"]!.ToString());
            }

            var response = _mapper.Map<CarViewModel>(data);
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GuessPrice(int id, decimal price)
        {
            TempData["GuessPrice"] = price.ToString();

            var validResponse = await _carManager.GuessCarPriceAsync(id, price);
            if (validResponse)
            {
                TempData["SuccessMessage"] = "Great Job!!!";
            }
            else 
            {
                TempData["ErrorMessage"] = "Try again!";
            }

            return RedirectToAction(nameof(Details), new { id = id });
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CarCreateViewModel carCreateViewModel)
        {
            try
            {
                if(!ModelState.IsValid)
                    return View(carCreateViewModel);

                var request = _mapper.Map<CreateCarRequest>(carCreateViewModel);
                var created = await _carManager.AddCarAsync(request);
                if (created) {
                    TempData["SuccessMessage"] = "Car CREATED!";
                    return RedirectToAction(nameof(Index));
                }

                TempData["ErrorMessage"] = "Car was NOT CREATED!";
                return View(carCreateViewModel);
            }
            catch
            {
                TempData["ErrorMessage"] = "There were some errors!";
                return View(carCreateViewModel);
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _carManager.DeleteCarAsync(id);
            if (deleted)
            {
                TempData["SuccessMessage"] = "Car DELETED!";
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = "Car was NOT DELETED!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Update(int id)
        {
            var data = await _carManager.GetCarByIdAsync(id);

            if (data == null)
            {
                TempData["ErrorMessage"] = "This Car does not exist!";
                return RedirectToAction(nameof(Index));
            }

            var response = _mapper.Map<CarUpdateViewModel>(data);
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(CarUpdateViewModel carUpdateViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(carUpdateViewModel);

                var request = _mapper.Map<UpdateCarRequest>(carUpdateViewModel);
                var updated = await _carManager.UpdateCarAsync(carUpdateViewModel.Id, request);
                if (updated)
                {
                    TempData["SuccessMessage"] = "Car UPDATED!";
                    return RedirectToAction(nameof(Index));
                }

                TempData["ErrorMessage"] = "Car was NOT UPDATED!";
                return View(carUpdateViewModel);
            }
            catch
            {
                TempData["ErrorMessage"] = "There were some errors!";
                return View(carUpdateViewModel);
            }
        }
    }
}
