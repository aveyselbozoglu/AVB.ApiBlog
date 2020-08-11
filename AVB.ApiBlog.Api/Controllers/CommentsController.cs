using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AVB.ApiBlog.DataAccess.EntityFrameworkCore;
using AVB.ApiBlog.DataAccess.Interfaces;
using AVB.ApiBlog.Entities.Models;

namespace AVB.ApiBlog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentDal _commentDal;

        public CommentsController(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            return await _commentDal.GetAll();
        }

        // GET: api/Comments/GetAllCommentsByArticleId/3
        [HttpGet]
        [Route("GetAllCommentsByArticleId/{id}")]
        public async Task<ActionResult<List<Comment>>> GetAllCommentsByArticleId(int id)
        {
            var commentsByArticleId = await _commentDal.GetAllCommentsByArticleId(id);

            if (!commentsByArticleId.Any())
                return NotFound();

            return commentsByArticleId;
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = await _commentDal.GetById(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.


        // POST: api/Comments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {

            if (comment == null)
                return BadRequest();

            comment.PublishDate = DateTime.Now;

            await _commentDal.Add(comment);
            
            return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Comment>> DeleteComment(int id)
        {
            var comment = await _commentDal.GetById(id);
            if (comment == null)
            {
                return NotFound();
            }

            await _commentDal.Remove(comment);
            
            return comment;
        }

    }
}


//[HttpPut("{id}")]
//public async Task<IActionResult> PutComment(int id, Comment comment)
//{
//    if (id != comment.Id)
//    {
//        return BadRequest();
//    }

//    _commentDal.Entry(comment).State = EntityState.Modified;

//    try
//    {
//        await _commentDal.SaveChangesAsync();
//    }
//    catch (DbUpdateConcurrencyException)
//    {
//        if (!CommentExists(id))
//        {
//            return NotFound();
//        }
//        else
//        {
//            throw;
//        }
//    }

//    return NoContent();
//}
