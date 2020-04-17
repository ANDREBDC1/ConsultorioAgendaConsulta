using AutoMapper;
using Consultorio.Dominio.Consultorios;
using Consultorio.Dominio.Consultorios.Agendamentos;
using Consultorio.Dominio.Consultorios.Agendamentos.Filtros;
using Consultorio_Web.ViewsModels.Agendamentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Consultorio_Web.Controllers
{
    public class AgendamentosController : Controller
    {
        private readonly IServicoClinica _servicoClinica;
        private readonly IServicoDeAgendamento _servicoDeAgendamento;

        public AgendamentosController(IServicoDeAgendamento servicoDeAgendamento,
            IServicoClinica servicoClinica)
        {
            _servicoDeAgendamento = servicoDeAgendamento;
            _servicoClinica = servicoClinica;
        }

        // GET: Agendamentos
        public ActionResult Index()
        {
            PreencherViewBagStatus();
            return View();
        }

        public PartialViewResult ListaAgendamento(FiltrosAgendamentosViewModel filtrosAgendamentosViewModel)
        {
            var filtrosAgendamentos = ObterFiltrosAgendamentos(filtrosAgendamentosViewModel);
            var agendamentos = _servicoDeAgendamento.ObterPor(filtrosAgendamentos);

            var agendamentoViewModel = ObterAgendamentosViewModel(agendamentos);

            return PartialView(agendamentoViewModel);
        }

        private FiltrosAgendamentos ObterFiltrosAgendamentos(FiltrosAgendamentosViewModel filtrosAgendamentosViewModel)
        {
            return Mapper.Map<FiltrosAgendamentosViewModel, FiltrosAgendamentos>(filtrosAgendamentosViewModel);
        }

        public PartialViewResult ObterQuantidadeVagas(string dataConsulta, int clinicaId)
        {
            DateTime dataConsulta2 = new DateTime();

            DateTime.TryParse(dataConsulta, out dataConsulta2);

            var totalDeVagasDiponives = _servicoDeAgendamento.ObterTotalDeVagas(dataConsulta2, clinicaId);
            return PartialView(new AgendamentoViewModel { TotalVagas = totalDeVagasDiponives });
        }

        private IEnumerable<AgendamentoViewModel> ObterAgendamentosViewModel(List<Agendamento> agendamento)
        {
            return Mapper.Map<IEnumerable<Agendamento>, IEnumerable<AgendamentoViewModel>>(agendamento);
        }

        // GET: Agendamentos/Details/5
        public ActionResult Details(int id)
        {
            var agendamento = _servicoDeAgendamento.ObterPor(id);
            var agendamentoViewModel = ObterAgendamentoViewModel(agendamento);
            return View(agendamentoViewModel);
        }

        // GET: Agendamentos/Create
        public ActionResult Create()
        {
            PreencherDadosViewBag(statusId: (int)StatusAgendamentoEnum.Aguardando);
            return View();
        }

        private void PreencherDadosViewBag(int statusId = 0, int clinicaId = 0, object valorPadraoSelecionado = null)
        {
            var clinicas = clinicaId == 0
                ? _servicoClinica.ObterTodas()
                : new List<Clinica> { _servicoClinica.ObterPor(clinicaId) };

            ViewBag.ClinicaId = new SelectList(clinicas, nameof(Clinica.Id), nameof(Clinica.Nome));
            var status = statusId == 0
                ? _servicoDeAgendamento.ObterTodosOsStatusAgendamento()
                : _servicoDeAgendamento.ObterTodosOsStatusAgendamento()
                .Where(c => Convert.ToInt32(c.ID) == statusId);

            var listaStatus = status.Select(c => new SelectListItem 
            {
                Text = c.Description,
                Value = ((int)c.ID).ToString(),
                Selected = valorPadraoSelecionado != null && ((int)c.ID).ToString().Equals(valorPadraoSelecionado.ToString())
            });
            ViewBag.StatusId = new SelectList(listaStatus, "Value", "Text");
        }

        private void PreencherViewBagStatus()
        {
            var status = _servicoDeAgendamento.ObterTodosOsStatusAgendamento();
            var listaStatus = new List<KeyValuePair<int, string>>() { new KeyValuePair<int, string>(0, string.Empty) };
            listaStatus.AddRange(status.Select(c => new KeyValuePair<int, string>((int)c.ID, c.Description)));
            ViewBag.StatusAgendamentoId = new SelectList(listaStatus, "Key", "Value");
        }

        // POST: Agendamentos/Create
        [HttpPost]
        public ActionResult Create(AgendamentoViewModel agendamentoViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    agendamentoViewModel.TotalVagas = _servicoDeAgendamento.ObterTotalDeVagas(agendamentoViewModel.DataConsulta, agendamentoViewModel.ClinicaId);
                    var agendamento = ObterAgendamento(agendamentoViewModel);
                    _servicoDeAgendamento.Salvar(agendamento);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("SalvarAgendamento", ex.Message);
                }

            }

            PreencherDadosViewBag(statusId: (int)StatusAgendamentoEnum.Aguardando);

            return View(agendamentoViewModel);
        }

        private Agendamento ObterAgendamento(AgendamentoViewModel agendamentoViewModel)
        {
            return Mapper.Map<AgendamentoViewModel, Agendamento>(agendamentoViewModel);
        }

        // GET: Agendamentos/Edit/5
        public ActionResult Edit(int id)
        {
            var agendamento = _servicoDeAgendamento.ObterPor(id);
            var agendamentoViewModel = ObterAgendamentoViewModel(agendamento);

            PreencherDadosViewBag(clinicaId: id, valorPadraoSelecionado: agendamentoViewModel.StatusId);
            return View(agendamentoViewModel);
        }

        private AgendamentoViewModel ObterAgendamentoViewModel(Agendamento agendamento)
        {
            return Mapper.Map<Agendamento, AgendamentoViewModel>(agendamento);
        }

        // POST: Agendamentos/Edit/5
        [HttpPost]
        public ActionResult Edit(AgendamentoViewModel agendamentoViewModel)
        {

            var agendamento = ObterAgendamento(agendamentoViewModel);

            _servicoDeAgendamento.AtualizarStatus(agendamento.Id, agendamento.status);

            return RedirectToAction("Index");

        }
    }
}
