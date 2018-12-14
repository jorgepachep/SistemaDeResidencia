using LiveChat.Data.Repository;
using LiveChat.Models.Entities;
using LiveChat.ViewModels.Copropietarios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Controllers
{
    public class CopropietariosController : Controller
    {
        public ICopropietarioRepository _CoPropietarioRepo { get; set; }
        public CopropietariosController(ICopropietarioRepository copropietarioRepo)
        {
            _CoPropietarioRepo = copropietarioRepo;
        }
        public IActionResult Listado()
        {
            var listadoCoPropietario = _CoPropietarioRepo.GetsElements();
            var ViewModel = new ListadoCopropietarioModel();
            ViewModel.Copropietarios = listadoCoPropietario.ToList();
            return View();
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(CrearModel coPropietario)
        {
            if (ModelState.IsValid)
            {
                var propietario = new Copropietario()
                {
                    Nombres=coPropietario.Nombres,
                    Apellidos=coPropietario.Apellidos,
                    Email=coPropietario.Email,
                    Phone=coPropietario.Phone,
                };
                _CoPropietarioRepo.Add(propietario);
            }
            return View(coPropietario);
        }
    }
}
