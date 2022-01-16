//using DataAccess;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace DeluzionalPenguinz.API.Controllers
//{
//    [Route("api/[controller]/[action]")]
//    [ApiController]
//    public class BananasController : ControllerBase
//    {
//        private readonly ApplicationDbContext appDb;

//        public BananasController(ApplicationDbContext appDb)
//        {
//            this.appDb = appDb;
//        }
//        [HttpGet]
//        public IActionResult GetBananasWithoutId()
//        {
//            var bananasWithoutId = appDb.Bananas.Select(banana => new Banana() { Title = banana.Title }).ToList();

//            return Ok(bananasWithoutId);
//        }
//        [HttpPost]
//        public async Task<IActionResult> AddBanana([FromBody] Banana banana)
//        {


//            await appDb.Bananas.AddAsync(banana);

//            await appDb.SaveChangesAsync();

//            return Ok(true);
//        }
//        [HttpPost]
//        public async Task<IActionResult> DeleteBanana([FromBody] Banana banana)
//        {
//            var bananaResult = GetBanana(banana.Title);

//            appDb.Bananas.Remove(bananaResult);

//            await appDb.SaveChangesAsync();

//            return Ok(true);
//        }

//        Banana GetBanana(string BananaTitle)
//        {
//            return appDb.Bananas.FirstOrDefault(banana => banana.Title.ToLower() == BananaTitle.ToLower());
//        }

//    }
//}
