using Microsoft.AspNetCore.Mvc;
using MvcApiPersonajesExamenTemplate.Models;
using MvcApiPersonajesExamenTemplate.Services;

namespace MvcApiPersonajesExamenTemplate.Controllers
{
    public class PersonajesController : Controller
    {
        private PersonajesService service;
        private ServiceStorageS3 serviceS3;
        public PersonajesController(PersonajesService service, ServiceStorageS3 serviceS3)
        {
            this.serviceS3 = serviceS3;
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
            List<Personaje> personajes =
                await this.service.GetPersonajesAsync();
            return View(personajes);
        }

        public async Task<IActionResult> Details(int id)
        {
            Personaje personaje = await this.service.FindPersonaje(id);
            return View(personaje);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Personaje personaje,IFormFile file)
        {
            using (Stream stream = file.OpenReadStream())
            {
                await this.serviceS3.UploadFileAsync(file.FileName,stream);
            }

            personaje.Imagen = file.FileName;
            await this.service.CreatePersonajeAsync(personaje);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Personaje personaje = await this.service.FindPersonaje(id);
            return View(personaje);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Personaje personaje)
        {
            await this.service.UpdatePersonajeAsync(personaje);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeletePersonajeAsync(id);
            return RedirectToAction("Index");
        }

    }
}
