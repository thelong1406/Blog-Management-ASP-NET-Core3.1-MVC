using AutoMapper;
using Common.Constants;
using DataAccess.Domain;
using MapModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogManagementMVC.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    public class TagController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TagController(
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        // GET: TagController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tags = _mapper.Map<List<TagVM>>(await _unitOfWork.Tags.GetAsync());
            return View(tags);
        }

        // GET: TagController/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            var tag = _mapper.Map<TagVM>(await _unitOfWork.Tags.GetByIDAsync(id));
            return View(tag);
        }

        // GET: TagController/Create
        public ActionResult Create()
        {
            TagVM tagVM = new TagVM();
            return View(tagVM);
        }

        // POST: TagController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TagVM tagVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newTag = _mapper.Map<Tag>(tagVM);
                    await _unitOfWork.Tags.InsertAsync(newTag);
                    await _unitOfWork.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                    return View(tagVM);
            }
            catch(Exception ex)
            {
                ViewBag.ExceptionMessage = ex.Message;
                return View(tagVM);
            }
        }

        // GET: TagController/Edit/5
        public async Task<IActionResult> Edit(long id)
        {
            Tag tag = await _unitOfWork.Tags.GetByIDAsync(id);
            var tagVM = _mapper.Map<TagVM>(tag);
            return View(tagVM);
        }

        // POST: TagController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(long id, TagVM tagVM)
        {
            if (id != tagVM.Id)
            {
                ViewBag.NotFound = "This information is not match!";
                return View(tagVM);
            }
            if(!_unitOfWork.Tags.IsExists(id).Result)
            {
                ViewBag.NotFound = "This information is not match!";
                return View(tagVM);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var tag = _mapper.Map<Tag>(tagVM);
                    _unitOfWork.Tags.Update(tag);
                    await _unitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    ViewBag.Execption = ex.Message;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tagVM);
        }

        // POST: TagController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            await _unitOfWork.Tags.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
