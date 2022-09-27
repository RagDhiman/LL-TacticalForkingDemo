using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop_Web_UI.DTOs;
using Shop_Web_UI.HTTPRepository;
using Shop_Web_UI.Models;

namespace Shop_Web_UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHTTPRepository<Account> _accountRepository;
        private readonly IMapper _mapper;

        public AccountController(IHTTPRepository<Account> accountRepository,
            IMapper mapper)
        {
            _accountRepository = accountRepository;
            _accountRepository.APIPath = "api/account";
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            var accounts = await _accountRepository.GetAllAsync();

            var accountModels = _mapper.Map<IEnumerable<AccountModel>>(accounts);

            return View(accountModels);
        }

        [HttpGet("[controller]/[action]/{Id}")]
        public async Task<IActionResult> Details(int Id)
        {
            var Account = await _accountRepository.GetByIdAsync(Id);
            var model = _mapper.Map<AccountModel>(Account);

            return View(model);
        }
    }
}
