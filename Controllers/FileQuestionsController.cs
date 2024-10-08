﻿using CrudApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using CrudApp.Data;
using System.Security.Claims;

namespace CrudApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileQuestionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FileQuestionsController(ApplicationDbContext context)
        {
            _context = context;
        }





        // GET: api/FileQuestions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FileQuestion>>> GetFileQuestions()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (email == null)
            {
                return Unauthorized();
            }

            return await _context.FileQuestions.ToListAsync();
        }




        // POST: api/FileQuestions
        [HttpPost]
        public async Task<IActionResult> PostFileQuestion([FromForm] IFormFile file, [FromForm] string title)
        {
            

            if (file == null || string.IsNullOrEmpty(title))
            {
                return BadRequest("Missing file or title.");
            }

            
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var filePath = Path.Combine(uploadPath, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileQuestion = new FileQuestion
            {
                Heading = title,
                FilePath = $"/uploads/{file.FileName}" 
            };

            _context.FileQuestions.Add(fileQuestion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFileQuestions), new { id = fileQuestion.Id }, fileQuestion);
        }



        
    }
}
