using AutoMapper;
using Common.Constants;
using DataAccess.Domain;
using MapModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagementMVC.Controllers
{
    [Authorize]
    public class PostCommentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;

        public PostCommentController(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<User> userManager,
            IEmailSender emailSender
        )
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._userManager = userManager;
            this._emailSender = emailSender;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost]
        public async Task<JsonResult> CommentPost(long postId, string title, string content, long? parentCommentId)
        {
            if (await _unitOfWork.Posts.IsExists(null, postId))
            {
                try
                {
                    if (parentCommentId.Value == 0 || parentCommentId == null)
                    {
                        parentCommentId = null;
                    }
                    //postVM.UserId = ;
                    PostCommentVM commentVM = new PostCommentVM
                    {
                        PostId = postId,
                        Title = title,
                        Content = content,
                        ParentId = parentCommentId,
                        UserId = _userManager.GetUserId(User),
                        Published = true,
                        CreatedAt = DateTime.Now,
                        PublishedAt = DateTime.Now,
                    };
                    PostComment newComment = _mapper.Map<PostComment>(commentVM);
                    await _unitOfWork.PostComments.InsertAsync(newComment);
                    await _unitOfWork.SaveAsync();

                    //Send Email
                    Post postCommented = await _unitOfWork.Posts.GetAllPostDetail(null, postId);
                    User userComment = _userManager.GetUserAsync(User).Result;
                    if(postCommented.UserId != userComment.Id)
                    {
                        string emailSubject = $"{userComment.FirstName} {userComment.LastName} have comment in your blog! [{commentVM.PublishedAt}]";
                        StringBuilder emailBody = new StringBuilder();
                        emailBody.Append($"Dear {postCommented.User.LastName},\n");
                        emailBody.Append($"Your blog [{postCommented.Title}] have been comment by another blogger, whose name is {userComment.FirstName} {userComment.LastName}!\n");
                        emailBody.Append($"The comment is about: {newComment.Title}.\n");
                        emailBody.Append($"Let check it in the link below! Thank you for reading!\n");
                        emailBody.Append(Url.Action("Details", "Post", new { postId = postCommented.PostId }));
                        await _emailSender.SendEmailAsync(
                            postCommented.User.Email,
                            emailSubject,
                            htmlMessage: emailBody.ToString()
                        );
                    }
                    return Json("Your comment have been send successfully!");
                }
                catch (DbUpdateException ex)
                {
                    return Json($"An error occured when our trying to send you comment:\n{ex.Message}");
                }
            }
            else
                return Json("The Blog you commented doesn't exists! There are some thing wrong! Let's Try again!");
        }
        [HttpPost]
        public async Task<JsonResult> DeleteComment(long postCommentId)
        {
            if(await _unitOfWork.PostComments.IsExists(postCommentId))
            {
                string commentedUserId = _unitOfWork.PostComments.GetByIDAsync(postCommentId).Result.UserId;
                User currentLoginUser = await _userManager.GetUserAsync(User);
                if (_userManager.GetUserId(User) == commentedUserId || 
                    await _userManager.IsInRoleAsync( currentLoginUser, Roles.Administrator)
                )
                {
                    try
                    {
                        await _unitOfWork.PostComments.DeleteAsync(postCommentId);
                        await _unitOfWork.SaveAsync();
                        return Json("Your comment have been deleted!");
                    }
                    catch(DbUpdateException ex)
                    {
                        return Json($"This comment have child comment, so you can't delete it! Or another exception occur:\n {ex.Message}");
                    }
                }
                else
                {
                    return Json("You don't have right to delete other's comment!");
                }
            }
            else
                return Json("This comment is doesn't exists");
        }
        
        [HttpPost]
        public async Task<JsonResult> SwitchPublisedCommentStatus(long postCommentId)
        {
            if(await _unitOfWork.PostComments.IsExists(postCommentId))
            {
                PostComment comment =await _unitOfWork.PostComments.GetByIDAsync(postCommentId);
                User currentLoginUser = await _userManager.GetUserAsync(User);
                if (_userManager.GetUserId(User) == comment.UserId || 
                    await _userManager.IsInRoleAsync( currentLoginUser, Roles.Administrator)
                )
                {
                    try
                    {
                        if(comment.Published)
                            comment.Published = false;
                        else
                            comment.Published = true;
                        _unitOfWork.PostComments.Update(comment);
                        await _unitOfWork.SaveAsync();
                        return Json(comment.Published);
                    }
                    catch(DbUpdateException ex)
                    {
                        return Json($"An exception occured:\n {ex.Message}");
                    }
                }
                else
                {
                    return Json("You don't have right to unpublised other's comment!");
                }
            }
            else
                return Json("This comment is doesn't exists");
        }
    }
}
