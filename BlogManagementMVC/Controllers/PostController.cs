using AutoMapper;
using DataAccess.Domain;
using MapModels.ViewModels.PostVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Common.Constants;
using BlogManagementMVC.Configurations;
using MapModels.ViewModels;

namespace BlogManagementMVC.Controllers
{
    public class PostController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public PostController(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<User> userManager
            )
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._userManager = userManager;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            DateTime? startDate,
            DateTime? endDate,
            string searchString,
            long? categoryId,
            int? isMyBlogs,
            int? pageNumber,
            int pageSize = 5
        )
        {
            try
            {
                //Set sortOrder
                ViewData["CurrentSort"] = sortOrder;
                ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
                ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
                ViewBag.PageSize = pageSize;
                ViewBag.IsMyBlogs = isMyBlogs ?? null;
                if (searchString != null) //When reset search string
                    pageNumber = 1;
                else
                    searchString = currentFilter;


                //<===  Send to View 
                ViewData["CurrentFilter"] = searchString;

                if (startDate.HasValue)
                    ViewData["StartDate"] = startDate.Value;

                if (endDate.HasValue)
                    ViewData["EndDate"] = endDate.Value;

                ViewData["Category"] = categoryId ?? null;
                //Send to View  ===>

                //if blogger want to see their blogs
                string userId = isMyBlogs.HasValue && isMyBlogs.Value == 1 ? _userManager.GetUserAsync(User).Result.Id : null;

                //if user is Admin
                bool isAdmin = false;
                User currentUserLogined = await _userManager.GetUserAsync(User);
                if (currentUserLogined != null)
                    isAdmin = await _userManager.IsInRoleAsync(currentUserLogined, Roles.Administrator);

                //Get query post from unit of work
                IQueryable<Post> posts = await _unitOfWork.Posts.GetListPostsAsync(startDate, endDate, searchString, userId, isAdmin, categoryId);

                //Switch sort oder base above
                posts = sortOrder switch
                {
                    "title_desc" => posts.OrderByDescending(s => s.Title),
                    "Date" => posts.OrderBy(s => s.CreatedAt),
                    "date_desc" => posts.OrderByDescending(s => s.CreatedAt),
                    _ => posts.OrderBy(s => s.Title),
                };

                //Query posts and map them to view model list
                List<ListPostsVM> postsVM = _mapper.Map<List<ListPostsVM>>(posts.ToList());

                //Query posts into Paginated List
                PaginatedList<ListPostsVM> postsPaginated = PaginatedList<ListPostsVM>.Create(postsVM, pageNumber ?? 1, pageSize);

                //This is list category to searching
                ViewBag.CategoriesSelect = _mapper.Map<List<CategoryVM>>(await _unitOfWork.Categories.ListIdTitle());
                ViewBag.NoResult = postsPaginated.Count() == 0 ?
                    string.IsNullOrEmpty(userId) ? "Sorry, we can't find any blog matched with information you gave!" : "You don't have any blogs yet!"
                    : null;

                return View(postsPaginated);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { requestId = "404" });
            }
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string postSlug, long? postId)
        {

            if (await _unitOfWork.Posts.IsExists(postSlug, postId))
            {
                string currentLoginUserId = _userManager.GetUserId(User);
                Post post = await _unitOfWork.Posts.GetAllPostDetail(postSlug, postId);
                if (post.IsDeleted && currentLoginUserId != post.UserId)
                    return NotFound();
                else if (!post.Published && currentLoginUserId != post.UserId)
                    return NotFound();
                else
                {
                    var postDetailVM = _mapper.Map<PostDetailVM>(post);
                    return View(postDetailVM);
                }
            }
            else
                return NotFound();
        }


        [Authorize]
        public ActionResult Create(long? parentId)
        {
            PostVM postVM = new PostVM();
            if (parentId.HasValue)
                postVM.ParentId = parentId.Value;
            return View(postVM);
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(long? parentId, PostVM postVM)
        {
            try
            {
                if (parentId.HasValue)
                {
                    if (!_unitOfWork.Categories.IsExists(parentId.Value).Result)
                    {
                        ViewBag.NotFound = $"Id for parent blog {postVM.ParentId.Value} is not exists!";
                        return View(postVM);
                    }
                    if (parentId.Value != postVM.ParentId.Value)
                    {
                        ViewBag.NotFound = $"Id for parent blog {parentId.Value} and parent id of this blog {postVM.ParentId.Value} is not match!";
                        return View(postVM);
                    }
                }

                DateTime currentTime = DateTime.Now;
                postVM.CreatedAt = currentTime;
                postVM.UserId = _userManager.GetUserId(User);

                if (!postVM.ValidDatePublishedAndModify())
                {
                    ViewBag.NotFound = $"Datetime of this blog is so wrong!";
                    return View(postVM);
                }

                if (postVM.Published)
                {
                    postVM.PublishedAt = currentTime;
                }

                if (Request.Form.Files.Count > 0)
                {
                    IFormFile file = Request.Form.Files.FirstOrDefault();
                    using (var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        postVM.Thumbnail = dataStream.ToArray();
                    }
                }

                var newPost = _mapper.Map<Post>(postVM);
                await _unitOfWork.Posts.InsertAsync(newPost);
                await _unitOfWork.SaveAsync();

                if (!postVM.ParentId.HasValue)
                    return RedirectToAction(nameof(AddCategoriesForBlog), new { id = newPost.PostId });
                else
                    return RedirectToAction(nameof(Details), new { id = postVM.ParentId.Value });
            }
            catch (Exception ex)
            {
                ViewBag.ExceptionMessage = ex.ToString();
                return View(postVM);
            }

        }


        [Authorize]
        public async Task<IActionResult> Edit(long id)
        {
            if (await _unitOfWork.Posts.IsExists(null, id))
            {
                Post post = await _unitOfWork.Posts.GetPostForEdition(id);
                if (post.UserId == _userManager.GetUserId(User) ||
                    _userManager.IsInRoleAsync(await _userManager.GetUserAsync(User), Roles.Administrator).Result)
                {
                    //ViewBag parent id
                    PostEditVM postEditVM = _mapper.Map<PostEditVM>(post);
                    ViewBag.ParrentPost = new SelectList(await _unitOfWork.Posts.ListIdTitle(id), "PostId", "Title");
                    return View(postEditVM);
                }
                return Forbid();
            }
            else
                return NotFound();
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, PostEditVM postEditVM)
        {
            #region Validate Zone
            if (id != postEditVM.PostId)
            {
                ViewBag.NotFound = "Route blog id and model's id information is not match!";
                return View(postEditVM);
            }

            if (!await _unitOfWork.Posts.IsExists(null, id))
            {
                ViewBag.NotFound = "This blog information doesn't exists!";
                return View(postEditVM);
            }

            //Check authorize
            if (postEditVM.UserId != _userManager.GetUserId(User) ||  //is author
                !_userManager.IsInRoleAsync(await _userManager.GetUserAsync(User), Roles.Administrator).Result) //or Admin
            {
                ViewBag.NotFound = "You don't have right to edit other's blog!";
                return Forbid();
            }

            //Validate time
            if (!postEditVM.ValidDatePublishedAndModify())
            {
                ViewBag.NotFound = "Datetime of this blog is so wrong!";
                return View(postEditVM);
            }
            #endregion //Validate Zone

            //Get an no tracking post to compare vs postEditVM
            Post originPost = await _unitOfWork.Posts.GetPostForEdition(id);

            //Check new file upload
            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    postEditVM.Thumbnail = dataStream.ToArray();
                }
            }
            else
                postEditVM.Thumbnail = originPost.Thumbnail;

            try
            {
                //Map model
                var postUpdated = _mapper.Map<Post>(postEditVM);

                //CheckUpdateObject will change the createAt value to default => {1/1/0001 12:00:00 AM}
                postUpdated.CreatedAt = originPost.CreatedAt;
                postEditVM.UpdatedAt = DateTime.Now;

                //Add value for field wasn't changed
                postUpdated = _unitOfWork.Posts.CheckUpdateObject(originPost, postUpdated);

                //Update database
                _unitOfWork.Posts.Update(postUpdated);
                await _unitOfWork.SaveAsync();

                return RedirectToAction(nameof(Details), new { postId = postUpdated.PostId });
            }
            catch (Exception ex)
            {
                Post post = await _unitOfWork.Posts.GetPostForEdition(id);
                postEditVM = _mapper.Map<PostEditVM>(post);

                ViewBag.ExceptionMessage = ex.ToString();
                ViewBag.ParrentPost = new SelectList(await _unitOfWork.Posts.ListIdTitle(id), "PostId", "Title");

                return View(postEditVM);
            }
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AddCategoriesForBlog(long id)
        {
            string authorId = _userManager.GetUserId(User);
            bool isAdmin = await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(User), Roles.Administrator);
            if (await _unitOfWork.Posts.CheckAuthor(id, authorId) || isAdmin)
            {
                if (await _unitOfWork.Posts.IsExists(null, id))
                {

                    ViewBag.CategoriesSelect = await _unitOfWork.Categories.ListIdTitle(postId: id);
                    ViewBag.PostId = id;
                    return View();
                }
                else
                    return NotFound();
            }
            else
                return Forbid();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddCategoriesForBlog(long id, long[] categoriesId)
        {
            string authorId = _userManager.GetUserId(User);
            bool isAdmin = await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(User), Roles.Administrator);
            if (await _unitOfWork.Posts.CheckAuthor(id, authorId) || isAdmin)
            {
                if (categoriesId.Count() > 0)
                {
                    try
                    {
                        foreach (var item in categoriesId)
                        {
                            if (await _unitOfWork.PostCategories.IsExists(id, item) == true)
                            {
                                continue;
                            }
                            else
                            {
                                PostCategory newPostCategory = new PostCategory
                                {
                                    PostId = id,
                                    CategoryId = item
                                };
                                await _unitOfWork.PostCategories.InsertAsync(newPostCategory);
                            }
                        }
                        await _unitOfWork.SaveAsync();
                        return RedirectToAction(nameof(AddTagsForBlog), new { id = id });
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ExceptionDetail = ex.ToString();
                        ViewBag.CategoriesSelect = await _unitOfWork.Categories.ListIdTitle(postId: id);
                        return View();
                    }
                }
                //If none selected categories ID
                return RedirectToAction(nameof(AddTagsForBlog), new { id = id });
            }
            else
                return Forbid();
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AddTagsForBlog(long id)
        {
            string authorId = _userManager.GetUserId(User);
            bool isAdmin = await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(User), Roles.Administrator);
            if (await _unitOfWork.Posts.CheckAuthor(id, authorId) || isAdmin)
            {
                if (await _unitOfWork.Posts.IsExists(null,id))
                {
                    ViewBag.TagsSelect = await _unitOfWork.Tags.ListIdTitle(id);
                    ViewBag.PostId = id;
                    return View();
                }
                return NotFound();
            }
            else
                return Forbid();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddTagsForBlog(long id, long[] tagsId)
        {
            string authorId = _userManager.GetUserId(User);
            bool isAdmin = await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(User), Roles.Administrator);
            if (await _unitOfWork.Posts.CheckAuthor(id, authorId) || isAdmin)
            {
                //If blogger select any tags ID
                if (tagsId.Count() > 0)
                {
                    try
                    {
                        foreach (var item in tagsId)
                        {
                            if (await _unitOfWork.PostTags.IsExists(id, item))
                            {
                                continue;
                            }
                            else
                            {
                                PostTag newPostTag = new PostTag
                                {
                                    PostId = id,
                                    TagId = item
                                };
                                await _unitOfWork.PostTags.InsertAsync(newPostTag);
                            }
                        }
                        await _unitOfWork.SaveAsync();
                        return RedirectToAction(nameof(Details), new { postId = id });
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ExceptionDetail = ex.ToString();
                        ViewBag.TagsSelect = await _unitOfWork.Tags.ListIdTitle(id);
                        return View();
                    }
                }

                //If none selected tags ID
                return RedirectToAction(nameof(AddMetasForBlog), new { id = id });
            }
            else
                return RedirectToAction("Error","Home", new { requestId = "403" });
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AddMetasForBlog(long id)
        {
            string authorId = _userManager.GetUserId(User);
            bool isAdmin = await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(User), Roles.Administrator);
            if (await _unitOfWork.Posts.CheckAuthor(id, authorId) || isAdmin)
            {
                if (await _unitOfWork.Posts.IsExists(null, id))
                {
                    ViewBag.PostId = id;
                    return View();
                }
                return NotFound();
            }
            else
                return Forbid();
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddMetasForBlog(long id, string[] keys, string[] contents)
        {
            string authorId = _userManager.GetUserId(User);
            bool isAdmin = await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(User), Roles.Administrator);

            if (!await _unitOfWork.Posts.CheckAuthor(id, authorId) && !isAdmin)
                return Forbid();

            if (keys.Count() != contents.Count())
            {
                ViewBag.ExceptionDetail = "Something when wrong!";
                return View();
            }

            //If blogger select any tags ID
            if (keys.Count() > 0)
            {
                try
                {
                    for (int i = 0; i < keys.Count(); i++)
                    {
                        PostMeta postMeta = new PostMeta
                        {
                            Key = keys[i],
                            Content = contents[i],
                            PostId = id
                        };

                        if (!await _unitOfWork.PostMetas.IsExists(key: keys[i], postId: id))
                            await _unitOfWork.PostMetas.InsertAsync(postMeta);
                    }

                    await _unitOfWork.SaveAsync();

                    return RedirectToAction(nameof(Details), new { postId = id });
                }
                catch (Exception ex)
                {
                    ViewBag.ExceptionDetail = ex.ToString();

                    return View();
                }
            }

            //If none selected tags ID
            return RedirectToAction(nameof(Details), new { postId = id });
            
        }


        [HttpPost]
        [Authorize]
        public async Task<JsonResult> RemoveCategoryOfPost(long postId, long categoryId)
        {
            string authorId = _userManager.GetUserId(User);
            bool isAdmin = await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(User), Roles.Administrator);
            if (!await _unitOfWork.Posts.CheckAuthor(postId, authorId) && !isAdmin)
            {
                return Json("You don't have rights to remove category of other's blog!");
            }

            if (!await _unitOfWork.PostCategories.IsExists(postId, categoryId))
            {
                return Json("Something went wrong or this postId doesn't have this category!");
            }
            else
                
            try
            {
                await _unitOfWork.PostCategories.DeleteAsync(postId, categoryId);
                await _unitOfWork.SaveAsync();
                return Json("Remove Category of this blog successfully!");
            }
            catch (Exception ex)
            {
                return Json($"Get Error when trying to remove this category! Detail: {ex}");
            }
        }


        [HttpPost]
        [Authorize]
        public async Task<JsonResult> RemoveTagOfPost(long postId, long tagId)
        {
            string authorId = _userManager.GetUserId(User);
            bool isAdmin = await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(User), Roles.Administrator);
            if (!await _unitOfWork.Posts.CheckAuthor(postId, authorId) && !isAdmin)
            {
                return Json("You don't have rights to remove tag of other's blog!");
            }

            if (!await _unitOfWork.PostTags.IsExists(postId, tagId))
            {
                return Json("Something went wrong or this postId doesn't have this tag!");

            }
            try
            {
                await _unitOfWork.PostTags.DeleteAsync(postId, tagId);
                await _unitOfWork.SaveAsync();
                return Json("Remove tag of this blog successfully!");
            }
            catch (Exception ex)
            {
                return Json($"Get Error when trying to remove this tag! Detail: {ex}");
            }
        }


        [HttpPost]
        [Authorize]
        public async Task<JsonResult> RemoveMetaOfPost(long postId, long id)
        {
            string authorId = _userManager.GetUserId(User);
            bool isAdmin = await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(User), Roles.Administrator);
            if (!await _unitOfWork.Posts.CheckAuthor(postId, authorId) && !isAdmin)
            {
                return Json("You don't have rights to remove metadata of other's blog!");
            }

            if (!await _unitOfWork.PostMetas.IsExists(id: id))
            {
                return Json("Something went wrong or this Blog doesn't have this metadata!");

            }
            try
            {
                await _unitOfWork.PostMetas.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
                return Json("Remove metadata of this blog successfully!");
            }
            catch (Exception ex)
            {
                return Json($"Get Error when trying to remove this metadata! Detail: {ex}");
            }
        }


        [HttpPost]
        [Authorize]
        public async Task<JsonResult> SwitchDeletePostStatus(long postId)
        {
            if(await _unitOfWork.Posts.IsExists(null, postId))
            {
                Post post = await _unitOfWork.Posts.GetByIDAsync(postId);
                if(post.UserId == _userManager.GetUserAsync(User).Result.Id || //case user is author of post
                    _userManager.IsInRoleAsync(await _userManager.GetUserAsync(User), Roles.Administrator).Result //case user is administrator
                )
                {
                    post.IsDeleted = post.IsDeleted ? false : true;

                    _unitOfWork.Posts.Update(post);
                    await _unitOfWork.SaveAsync();

                    string result = post.IsDeleted ? $"Delete post: {post.Title} completed!" : $"Recover post: {post.Title} completed!";

                    return Json(result);
                }
                else
                {
                    return Json("You don't have rights to execute this action!");
                }
            }
            else
            {
                return Json("Bad request!");
            }
        }
        
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize]
        public async Task<JsonResult> SwitchPublishedPostStatus(long postId)
        {
            if(await _unitOfWork.Posts.IsExists(null, postId))
            {
                Post post = await _unitOfWork.Posts.GetByIDAsync(postId);
                if(post.UserId == _userManager.GetUserAsync(User).Result.Id ||
                    _userManager.IsInRoleAsync(await _userManager.GetUserAsync(User), Roles.Administrator).Result
                )
                {
                    post.Published = post.Published ? false : true;
                    if(post.Published)
                        post.PublishedAt = DateTime.Now;
                    _unitOfWork.Posts.Update(post);
                    await _unitOfWork.SaveAsync();
                    string result = post.Published ? $"Published post: {post.Title} completed!" : $"Unpublished post: {post.Title} completed!";
                    return Json(result);
                }
                else
                {
                    return Json("Bad request!");
                }
            }
            else
            {
                return Json("Bad request!");
            }
        }
    }
}
