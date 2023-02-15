using AutoMapper;
using GoodsStore.App.Infra;
using GoodsStore.App.Models.AccessManagement;
using GoodsStore.App.Models.AccessManagement.ViewModels;
using GoodsStore.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using static GoodsStore.App.Models.AccessManagement.ViewModels.Helper;

namespace GoodsStore.App.Controllers
{
    public class NavigationAccessController : Controller
    {
        private readonly ILogger<NavigationAccessController> _logger;
        private readonly IMapper _mapper;
        private readonly IMenuRepository _menuRepository;
        private readonly IMainMenuRepository _mainMenuRepository;
        private readonly IUserRepository _userRepository;
        

        public NavigationAccessController(ILogger<NavigationAccessController> logger, IMapper mapper, IMenuRepository menuRepository, IMainMenuRepository mainMenuRepository, IUserRepository userRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _menuRepository = menuRepository;
            _mainMenuRepository = mainMenuRepository;
            _userRepository = userRepository;
            CheckUserAuthentication();
        }
        
        public async Task<IActionResult> MenuListAll()
        {
            IList<Menu> list = await _menuRepository.GetAll();
            var listMainMenus = PopulateMainMenus();
            var listViewModel = new List<MenuRegistrationViewModel>();
            
            if(list != null) 
            {                 
                foreach (var item in list)
                {
                    var viewModel = _mapper.Map<MenuRegistrationViewModel>(item);
                    
                    listViewModel.Add(viewModel);
                }                
            }
            return View(listViewModel);
        }

        [NoDirectAccess]
        public async Task<IActionResult> MenuAddOrEdit(int id = 0)
        {
            var listMainMenus = await _mainMenuRepository.GetAll();

            if (id == 0)
            {
                var view = new MenuRegistrationViewModel();
                view.MainMenus = await PopulateMainMenus();
                return View(view);
            }                
            else
            {
                var menu = await _menuRepository.GetById(id);
                
                if (menu == null)
                {
                    return NotFound();
                }
                //var menuViewModel = _mapper.Map<MenuRegistrationViewModel>(menu).FillMainMenus(listMainMenus.ToList());
                var view = _mapper.Map<MenuRegistrationViewModel>(menu);
                view.MainMenus = await PopulateMainMenus();
                return View(view);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MenuAddOrEdit(int id, MenuRegistrationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.DtRegistered = DateTime.Now;
                viewModel.MainMenu = await _mainMenuRepository.GetById(int.Parse(viewModel.MainMenuId));
                var menu = _mapper.Map<Menu>(viewModel);                
                await _menuRepository.AddOrEdit(menu);
                
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "MenuListAll", _menuRepository.GetAll()) });
            }
            viewModel.MainMenus = await PopulateMainMenus(viewModel.MainMenuId);
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "MenuAddOrEdit", viewModel) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterModule()
        {
            return View();
        }

        public IActionResult RegisterFeature()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void CheckUserAuthentication()
        {
            _userRepository.GetUserLoged();
        }

        private async Task<SelectList> PopulateMainMenus(string selectedItem = null)
        {
            List<SelectListItem> mainMenus = new List<SelectListItem>();
            mainMenus.Add(new SelectListItem { Text = "Select", Value = "" });
            var listMainMenus = _mainMenuRepository.GetAll().Result;
            var selected = new SelectListItem();
            foreach (var menu in listMainMenus)
            {
                var item = new SelectListItem { Text = menu.Title, Value = menu.Id.ToString() }; 
                if (selectedItem == null & menu.Id.ToString() != selectedItem)
                {
                    mainMenus.Add(item);
                }                    
                else
                {                    
                    selected = item;                    
                    mainMenus.Add(item);
                }
            }

            SelectList items = new SelectList(mainMenus, "Value", "Text", selectedItem);
            return items;
        }
    }
}