using AutoMapper;
using Common.Constants;
using DataAccess.Domain;
using MapModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogManagementMVC.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryController(
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categoriesVM = _mapper.Map<List<CategoryVM>>(await _unitOfWork.Categories.GetAsync());
            return View(categoriesVM);
        }

        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            var category = _mapper.Map<CategoryVM>(await _unitOfWork.Categories.GetAndChildsAsync(id));
            return View(category);
        }

        [HttpGet]
        public ActionResult Create(long? parentId)
        {
            CategoryVM categoryVM = new CategoryVM();
            if (parentId.HasValue)
                categoryVM.ParentId = parentId.Value;
            return View(categoryVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(long? parentId, CategoryVM categoryVM)
        {
            try
            {
                if(parentId.HasValue)
                {
                    if (!_unitOfWork.Categories.IsExists(parentId.Value).Result)
                    {
                        ViewBag.NotFound = $"Id for parent category {categoryVM.ParentId.Value} is not exists!";
                        return View(categoryVM);
                    }
                    if (parentId.Value != categoryVM.ParentId.Value)
                    {
                        ViewBag.NotFound = $"Id for parent category {parentId.Value} and parent id of this catagories {categoryVM.ParentId.Value} is not match!";
                        return View(categoryVM);
                    }
                }

                if (ModelState.IsValid)
                {
                    var newCategory = _mapper.Map<Category>(categoryVM);
                    await _unitOfWork.Categories.InsertAsync(newCategory);
                    await _unitOfWork.SaveAsync();
                    if (!categoryVM.ParentId.HasValue)
                        return RedirectToAction(nameof(Index));
                    else
                        return RedirectToAction(nameof(Details), new { id = categoryVM.ParentId.Value });
                }
                else
                    return View(categoryVM);
            }
            catch (Exception ex)
            {
                ViewBag.ExceptionMessage = ex.Message;
                return View(categoryVM);
            }
        }

        public async Task<ActionResult> Edit(long id)
        {
            var categoryVM = _mapper.Map<CategoryVM>(await _unitOfWork.Categories.GetByIDAsync(id));
            ViewBag.ListCategories = new SelectList(await _unitOfWork.Categories.ListIdTitle(excategoryIdToEditItParentceptId:id), "Id", "Title");
            return View(categoryVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, CategoryVM categoryVM)
        {
            ViewBag.ListCategories = new SelectList(await _unitOfWork.Categories.ListIdTitle(excategoryIdToEditItParentceptId: id, postId: null), "Id", "Title");
            if (id != categoryVM.Id)
            {
                ViewBag.NotFound = "This information is not match!";
                return View(categoryVM);
            }

            if (!_unitOfWork.Categories.IsExists(id).Result)
            {
                ViewBag.NotFound = "This information is not match!";
                return View(categoryVM);
            }

            if(_unitOfWork.Categories.IsTitleExists(categoryVM.Id, categoryVM.Title).Result)
            {
                ViewBag.Duplicate = $"Database already have category which title is {categoryVM.Title}!";
                return View(categoryVM);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var category = _mapper.Map<Category>(categoryVM);
                    _unitOfWork.Categories.Update(category);
                    await _unitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    ViewBag.Execption = ex.Message;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoryVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            await _unitOfWork.Categories.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
