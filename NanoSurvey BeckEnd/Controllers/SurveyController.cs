using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NanoSurvey_BeckEnd.Survey;

namespace NanoSurvey_BeckEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        SurveyContext db;
        public SurveyController(SurveyContext context)
        {
            db = context;
            if (!db.Surveys.Any())
            {
                db.Surveys.Add(new Survey.Survey() { Text = "NanoSurvey " });
                db.SaveChanges();
            }

            if (!db.Questions.Any())
            {
                db.Questions.Add(new Question()
                {
                    Text = "В каком регионе проживаете?",
                    Answers = new List<Answer>()
                    {
                        new Answer() { Text = "Москва" },
                        new Answer() { Text = "Московская область" }, 
                        new Answer() { Text = "Санкт-Петербург" },
                        new Answer() { Text = "Ленинградская область" },
                        new Answer() { Text = "Рязанская область" },
                        new Answer() { Text = "Владимирская область" },
                        new Answer() { Text = "Тульская область" },
                        new Answer() { Text = "Другой регион" }
                    }
                });
                db.Questions.Add(new Question()
                {
                    Text = "Сколько вам лет?",
                    Answers = new List<Answer>()
                    {
                        new Answer() { Text = "<10" },
                        new Answer() { Text = "11-18" },
                        new Answer() { Text = "19-29" },
                        new Answer() { Text = ">30" },
                    }
                });
                db.SaveChanges();
            }

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> Get()
        {
            return await db.Questions.ToListAsync();
        }

        // GET api/survay/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> Get(int id)
        {
            Question question = await db.Questions.FirstOrDefaultAsync(x => x.Id == id);
            if (question == null)
                return NotFound();
            return new ObjectResult(question);
        }

        // POST api/survay
        [HttpPost]
        public async Task<ActionResult<Answer>> Post(Answer answer)
        {
            if (answer == null)
            {
                return BadRequest();
            }

            db.Answers.Add(answer);
            await db.SaveChangesAsync();
            return Ok(answer);
        }
    }
}
