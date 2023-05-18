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
            var data = await _carManager.GetAllCars();
            var response = _mapper.Map<List<CarViewModel>>(data);

            return View(response);
        }

        public async Task<ActionResult> Details(int id)
        {
            var data = await _carManager.GetCarById(id);
            if (data == null)
            {
                TempData["ErrorMessage"] = "This Car does not exist!";
                return Redirect("/Cars");
            }

            data.Price = 0;
            if (TempData.ContainsKey("GuessPrice"))
            {
                data.Price = Convert.ToDecimal(TempData["GuessPrice"].ToString());
            }

            var response = _mapper.Map<CarViewModel>(data);
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GuessPrice(int id, decimal price)
        {
            TempData["GuessPrice"] = price.ToString();

            var validResponse = await _carManager.GuessCarPrice(id, price);
            if (validResponse)
            {
                TempData["SuccessMessage"] = "Great Job!!!";
            }
            else 
            {
                TempData["ErrorMessage"] = "Try again!";
            }

            return Redirect($"/Cars/Details/{id.ToString()}");
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
                var created = await _carManager.AddCar(request);
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

        //// GET: CarsController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: CarsController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: CarsController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}
    }
}
