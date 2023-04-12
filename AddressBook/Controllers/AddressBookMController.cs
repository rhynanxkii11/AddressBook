using AddressBook.DAL;
using AddressBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.Controllers
{
    public class AddressBookMController : Controller
    {
        private readonly AddressBookDAL _dal;

        public AddressBookMController(AddressBookDAL dal)
        {
            _dal = dal;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<AddressBookM> ablist = new List<AddressBookM>();
            try
            {
                ablist = _dal.GetAll();
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
            }
            return View(ablist);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(AddressBookM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Model data is invalid!";
                }
                bool result = _dal.Insert(model);
                if (!result)
                {
                    TempData["errorMessage"] = "Unable to save the data";
                    return View();
                }
                TempData["successMessage"] = "New someone's address book added successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                AddressBookM ab = _dal.GetById(id);
                if (ab.Id == 0)
                {
                    TempData["errorMessage"] = $"No data found with an Id: {id}";
                    return RedirectToAction("Index");
                }
                return View(ab);
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpPost]
        public IActionResult Edit(AddressBookM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Model data is invalid!";
                    return View();
                }
                bool result = _dal.Update(model);
                if (!result)
                {
                    TempData["errorMessage"] = "Unable to update the data";
                    return View();
                }
                TempData["successMessage"] = "Detail/s updated successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                AddressBookM addressbook = _dal.GetById(id);
                if (addressbook.Id == 0)
                {
                    TempData["errorMessage"] = $"No data found with an Id: {id}";
                    return RedirectToAction("Index");
                }
                return View(addressbook);
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmation(AddressBookM model)
        {
            try
            {
                bool result = _dal.Delete(model.Id);

                if (!result)
                {
                    TempData["errorMessage"] = "Unable to delete the data";
                    return View();
                }
                TempData["successMessage"] = "Data has been deleted successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

    }
}
