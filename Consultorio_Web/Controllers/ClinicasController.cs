using AutoMapper;
using Consultorio.Dominio.Consultorios;
using Consultorio_Web.ViewsModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Consultorio_Web.Controllers
{
    public class ClinicasController : Controller
    {
        private readonly IServicoClinica _servicoClinica;

        public ClinicasController(IServicoClinica servicoClinica)
        {
            _servicoClinica = servicoClinica;
        }
        // GET: Clinicas
        public ActionResult Index()
        {
            var clinicas = _servicoClinica.ObterTodas();
            var model = ObterModeloClinicas(clinicas);
            return View(model);
        }

        // GET: Clinicas/Details/5
        public ActionResult Details(int id)
        {
            var clinica = _servicoClinica.ObterPor(id);
            var clinicaViewModel = ObterClinicaViewModel(clinica);

            return View(clinicaViewModel);
        }

        // GET: Clinicas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clinicas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClinicaViewModel clinicaViewModel)
        {
            if (ModelState.IsValid)
            {
                var clinica = ObterClinica(clinicaViewModel);

                _servicoClinica.Salvar(clinica);

                return RedirectToAction("Index");
            }

            return View(clinicaViewModel);
        }

        // GET: Clinicas/Edit/5
        public ActionResult Edit(int id)
        {
            var clinica = _servicoClinica.ObterPor(id);
            var clinicaViewModel = ObterClinicaViewModel(clinica);

            return View(clinicaViewModel);
        }

        private ClinicaViewModel ObterClinicaViewModel(Clinica clinica)
        {
            var clinicaViewModel = Mapper.Map<Clinica,ClinicaViewModel>(clinica);
            clinicaViewModel.EnderecoViewModel = Mapper.Map<Endereco, EnderecoViewModel>(clinica.Endereco);
            return clinicaViewModel;
        }

        // POST: Clinicas/Edit/5
        [HttpPost]
        public ActionResult Edit(ClinicaViewModel clinicaViewModel)
        {
            if (ModelState.IsValid)
            {
                var clinica = ObterClinica(clinicaViewModel);

                _servicoClinica.Salvar(clinica);

                return RedirectToAction("Index");
            }

            return View(clinicaViewModel);
        }

        // GET: Clinicas/Delete/5
        public ActionResult Delete(int id)
        {
            var clinica = _servicoClinica.ObterPor(id);
            var clinicaViewModel = ObterClinicaViewModel(clinica);

            return View(clinicaViewModel);
        }

        // POST: Clinicas/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfimed(int id)
        {
            _servicoClinica.RemoverPor(id);

            return RedirectToAction("Index");
        }

        private IEnumerable<ClinicaViewModel> ObterModeloClinicas(List<Clinica> clinicas)
        {
            return Mapper.Map<IEnumerable<Clinica>, IEnumerable<ClinicaViewModel>>(clinicas);
        }

        private Clinica ObterClinica(ClinicaViewModel clinicaViewModel)
        {
            var clinica = Mapper.Map<ClinicaViewModel, Clinica>(clinicaViewModel);
            clinica.Endereco = Mapper.Map<EnderecoViewModel, Endereco>(clinicaViewModel.EnderecoViewModel);
            return clinica;
        }
    }
}
