using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fundoo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        INotesBL Notesbl;
        public NotesController(INotesBL Notesbl)
        {
            this.Notesbl = Notesbl;
        }
        [HttpPost("Add")]
        public IActionResult AddNotes(NotesModel addnote)
        {
            try
            {
                //long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = Notesbl.AddNote(addnote, userID);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Notes Added  Sucessfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "notes adding is  Unsucessfull" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpDelete("Delete")]
        public IActionResult DeleteNotes(long NoteId)
        {
            try
            {
                var delete = Notesbl.DeleteNote(NoteId);
                if (delete != null)
                {
                    return this.Ok(new { Success = true, message = "Notes Deleted Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Notes Deleted Unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet("GetNoteByNoteID")]
        public IActionResult GetNote(long NoteId)
        {
            try
            {
                List<NotesEntity> result = Notesbl.GetNote(NoteId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = " Note got Successfully", data = result });
                }
                else
                    return this.BadRequest(new { Success = false, message = "Note not Available" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet("GetNoteByUserId")]
        public IActionResult GetNoteByUserID(long userID)
        {
            try
            {
                //long note = Convert.ToInt32(User.Claims.FirstOrDefault(X => X.Type == "userID").Value);
                List<NotesEntity> result = Notesbl.GetNotebyUserId(userID);
                if (result == null)
                {
                    return this.Ok(new { Success = false, message = " Note not Available" });
                }
                else
                {
                    if (result != null)
                    {
                        return this.Ok(new { Success = true, message = " Note got Successfully", data = result });
                    }
                    return this.BadRequest(new { Success = false, message = " error occured" });

                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet("GetAllNote")]
        public IActionResult GetAllNote()
        {
            try
            {
                List<NotesEntity> result = Notesbl.GetAllNote();
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = " Note got Successfully", data = result });
                }
                else
                    return this.BadRequest(new { Success = false, message = "Note not Available" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateNote(NotesModel noteModel, long NoteId)
        {
            try
            {
                var result = Notesbl.UpdateNote(noteModel, NoteId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Notes Updated Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "No Notes Found" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPut]
        [Route("Archive")]
        public IActionResult ArchiveNote(long NoteId)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(X => X.Type == "userID").Value);
                var result = Notesbl.Archieved(NoteId, userid);
                if (result == true)
                {
                    return this.Ok(new { Success = true, message = "Archived Successfully", data = result });
                }
                else if (result == false)
                {
                    return this.Ok(new { Success = true, message = "Unarchived", data = result });
                }

                else
                {
                    return this.BadRequest(new { Success = false, Message = "Archived unsuccessful" });
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("Pin")]
        public IActionResult PinNote(long NoteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(p => p.Type == "userID").Value);
                var result = Notesbl.Pinned(NoteId, userId);
                if (result == true)
                {
                    return this.Ok(new { Success = true, message = "Note Pinned Successfully", data = result });
                }
                else if (result == false)
                {
                    return this.Ok(new { Success = true, message = "Unpinned", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Note Pinned Unsuccessfully" });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("Trash")]
        public IActionResult TrashNote(long NotesId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(t => t.Type == "userID").Value);
                var result = Notesbl.Trashed(NotesId, userId);
                if (result == true)
                {
                    return this.Ok(new { Success = true, message = "Trashed Successfully", data = result });
                }
                else if (result == false)
                {
                    return this.Ok(new { Success = true, message = "Untrashed", data = result });
                }

                else
                {
                    return this.BadRequest(new { Success = false, message = "Trashed unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("Color")]
        public IActionResult ColourNote(long NoteId, string color)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
                var colors = Notesbl.ColorNote(NoteId, color);
                if (colors != null)
                {
                    return Ok(new { Success = true, message = "Added Colour Successfully", data = colors });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Added Colour Unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        [Route("Image")]
        public IActionResult Imaged(long noteId, IFormFile image)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = Notesbl.Imaged(noteId, userID, image);
                if (result != null)
                {
                    return Ok(new { Status = true, Message = "Image Uploaded Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new { Status = true, Message = "Image Uploaded Unsuccessfully", Data = result });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
