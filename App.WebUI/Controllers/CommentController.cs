using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data.Abstract;
using App.Data.Dtos;
using App.Entity.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : Controller
    {
        private ICommentRepository _CommentRepository;
        private IMapper _mapper;
        public CommentController(IMapper mapper, ICommentRepository CommentRepository)
        {
            _mapper = mapper;
            _CommentRepository = CommentRepository;
        }


        //http://localhost:44330/api/Comment/List
        /*
        [
            {
                "commentId": 1,
                "articleId": 1,
                "userId": 1,
                "comment": "BİRİNCİ YORUM YAZILDI",
                "commentDate": "2020-07-28T00:00:00",
                "active": true,
                "article": null,
                "user": null
            },
            {
                "commentId": 2,
                "articleId": 1,
                "userId": 1,
                "comment": "İKİNCİ YORUM YAZILDI",
                "commentDate": "2020-07-29T00:00:00",
                "active": true,
                "article": null,
                "user": null
            }
        ]
        */
        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {
            var DataList = await _CommentRepository.GetsAsync();
            var DataListToReturn = _mapper.Map<List<Comments>>(DataList);
            return Ok(DataListToReturn);
        }

        //http://localhost:44330/api/Comment/Detail/?id=1
        /*
        {
            "commentId": 1,
            "articleId": 1,
            "userId": 1,
            "comment": "BİRİNCİ YORUM YAZILDI",
            "commentDate": "2020-07-28T00:00:00",
            "active": true
        }
        */
        [HttpGet]
        [Route("Detail")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var GetById = await _CommentRepository.GetByIdAsync(id);
            var DataListToReturn = _mapper.Map<CommentDto>(GetById);
            return Ok(DataListToReturn);
        }


        //http://localhost:44330/api/Comment/Add
        /*
        {
            "articleId": 1,
            "userId": 1,
            "comment": "ÜÇÜNCÜ YORUM YAZILDI",
            "commentDate": "2020-08-28T00:00:00",
            "active": true
        }
        */
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CommentDto data)
        {
            if (ModelState.IsValid)
            {

                var SaveData = _mapper.Map<Comments>(data);

                _CommentRepository.Add(SaveData);
                await _CommentRepository.SaveAll();


                return Ok(SaveData);
            }
            return BadRequest("Kayıt Yapılamadı ");
        }

        //http://localhost:44330/api/Comment/Update
        /*
        {
            "commentId": 1,
            "articleId": 1,
            "userId": 1,
            "comment": "ÜÇÜNCÜ YORUM YAZILDI GÜNCELLENDİ",
            "commentDate": "2020-09-28T00:00:00",
            "active": true
        }
        */
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] CommentDto data)
        {
            if (ModelState.IsValid)
            {
                var UpdateData = _mapper.Map<Comments>(data);
                _CommentRepository.Update(UpdateData);
                await _CommentRepository.SaveAll();

                return Ok(data);

            }
            return BadRequest("Güncelleme Yapılamadı ");
        }

        //http://localhost:44330/api/Comment/Delete/?id=1
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var DeleteData = await _CommentRepository.GetByIdAsync(id);
                _CommentRepository.Delete(DeleteData);
                if (await _CommentRepository.SaveAll())
                    return Ok("Silindi");
            }
            return NotFound("Aradığınız Değer bulunamadı.");
        }

    }
}