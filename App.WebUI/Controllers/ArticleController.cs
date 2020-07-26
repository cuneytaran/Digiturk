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
    public class ArticleController : Controller
    {

        private IArticleRepository _ArticleRepository;
        private IMapper _mapper;
        public ArticleController(IMapper mapper, IArticleRepository ArticleRepository)
        {
            _mapper = mapper;
            _ArticleRepository = ArticleRepository;
        }


        //http://localhost:44330/api/Article/List
        /*
        {
        "articleId":1,
        "userId":1,
        "articleTitle":"KONU BAŞLIĞI",
        "articleContent":"KONU İÇERİĞİ VE AÇIKLAMASI",
        "articleDate":"2020-07-23T00:00:00",
        "active":true}
        */
        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {
            var DataList = await _ArticleRepository.GetsAsync();
            var DataListToReturn = _mapper.Map<List<Articles>>(DataList);
            return Ok(DataListToReturn);
        }

        //http://localhost:44330/api/Article/Detail/?id=1
        /*
        {
        "articleId":1,
        "userId":1,
        "articleTitle":"KONU BAŞLIĞI",
        "articleContent":"KONU İÇERİĞİ VE AÇIKLAMASI",
        "articleDate":"2020-07-23T00:00:00",
        "active":true}
        */
        [HttpGet]
        [Route("Detail")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var GetById = await _ArticleRepository.GetByIdAsync(id);
            var DataListToReturn = _mapper.Map<ArticleDto>(GetById);
            return Ok(DataListToReturn);
        }


        //http://localhost:44330/api/Article/Add
        /*
                {    
            "userId":1,
            "articleTitle":"KONU BAŞLIĞI ikincisi",
            "articleContent":"KONU İÇERİĞİ VE AÇIKLAMASI ikincisi",
            "articleDate":"2020-08-23",
            "active":true
        }
        */
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] ArticleDto data)
        {
            if (ModelState.IsValid)
            {

                var SaveData = _mapper.Map<Articles>(data);

                _ArticleRepository.Add(SaveData);
                await _ArticleRepository.SaveAll();


                return Ok(SaveData);
            }
            return BadRequest("Kayıt Yapılamadı ");
        }

        //http://localhost:44330/api/Article/Update
        /*
        {    
            "articleId":1,
            "userId":1,
            "articleTitle":"KONU BAŞLIĞI güncellendi",
            "articleContent":"KONU İÇERİĞİ VE AÇIKLAMASI güncelledi",
            "articleDate":"2020-08-23",
            "active":true
        }
        */
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ArticleDto data)
        {
            if (ModelState.IsValid)
            {
                var UpdateData = _mapper.Map<Articles>(data);
                _ArticleRepository.Update(UpdateData);
                await _ArticleRepository.SaveAll();

                return Ok(data);

            }
            return BadRequest("Güncelleme Yapılamadı ");
        }

        //http://localhost:44330/api/Article/Delete/?id=1
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var DeleteData = await _ArticleRepository.GetByIdAsync(id);
                _ArticleRepository.Delete(DeleteData);
                if (await _ArticleRepository.SaveAll())
                    return Ok("Silindi");
            }
            return NotFound("Aradığınız Değer bulunamadı.");
        }

    }
}