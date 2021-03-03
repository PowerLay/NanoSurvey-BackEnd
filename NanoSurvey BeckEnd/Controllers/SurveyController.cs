using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            var actionResult = await db.Questions.ToListAsync();
            foreach (var question in actionResult)
            {
                GetAnswers(question);
            }
            return actionResult;
        }

        private void GetAnswers(Question question)
        {
            var answers = db.Answers.Where(p => p.Question == question).ToList();
            foreach (var answer in answers)
            {
                answer.Question = null;
            }

            question.Answers = answers;
        }

        // GET api/survey/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> Get(int id)
        {
            Question question = await db.Questions.FirstOrDefaultAsync(x => x.Id == id);
            if (question == null)
                return NotFound();

            GetAnswers(question);

            return new ObjectResult(question);
        }

        // POST api/survey
        [HttpPost]
        public async Task<ActionResult<Result>> Post(int[] ids)
        {
            var answer = new Result()
            {
                Interview = db.Interviews.FirstOrDefault(x => x.Id == ids[0]),
                Question = db.Questions.FirstOrDefault(x => x.Id == ids[1]),
                Answer = db.Answers.FirstOrDefault(x => x.Id == ids[2])
            };
            if (answer.Answer == null || answer.Question == null)
            {
                return BadRequest();
            }

            answer.Answer.Question = null;
            answer.Question.Answers = null;

            db.Results.Add(answer);
            await db.SaveChangesAsync();
            return Ok(answer);
        }
    }
}
