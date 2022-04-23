using AutoMapper;
using DataAccess.Domain;
using MapModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Threading.Tasks;

namespace BlogManagementMVC.Controllers
{
    public class VoteController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public VoteController(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<User> userManager
            )
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> Voting(long postId, int rate)
        {
            //Current user login => vote
            string userVoteId = _userManager.GetUserAsync(User).Result.Id;
            if (await _unitOfWork.Posts.IsExists(slug: null, postId: postId))
            {
                if(rate > 5 || rate < 1)
                {
                    return Json("Vote rate have to be between 1 and 5!");
                }
                else
                {
                    if(await _unitOfWork.Votes.IsExists(postId, userVoteId))
                    {
                        //Update vote
                        try
                        {
                            Vote voteUpdate = await _unitOfWork.Votes.GetByIDAsync(userVoteId, postId);
                            voteUpdate.Rate = (byte)rate;
                            _unitOfWork.Votes.Update(voteUpdate);
                            await _unitOfWork.SaveAsync();
                            return Json("Voting successfully!");
                        }
                        catch (Exception ex)
                        {
                            return Json($"Bad Request! {ex.Message}");
                        }
                    }
                    else
                    {
                        //Add new vote
                        try
                        {
                            Vote newVote = new Vote
                            {
                                PostId = postId,
                                UserId = userVoteId,
                                Rate = (byte)rate
                            };
                            await _unitOfWork.Votes.InsertAsync(newVote);
                            await _unitOfWork.SaveAsync();
                            return Json("Voting successfully!");
                        }
                        catch (Exception ex)
                        {
                            return Json($"Bad Request! {ex.Message}");
                        }
                    }
                }
            }
            else
                return Json("The Id of blog which you voted is doesn't macth with the database! Try again!");
        }
    }
}
