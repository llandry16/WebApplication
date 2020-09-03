using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using WebApplication.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SondagesController : ControllerBase
    {
        private static readonly List<Sondage> sondages = new List<Sondage>(){
            new Sondage(){
                Id = 1,
                Questions = new List<Question>(){
                    new Question(){
                        Id = 1,
                        Text = "À quelle tranche d'âge appartenez-vous? a:0-25 ans, b:25-50 ans, c:50-75 ans, d:75 ans et plus"
                    },
                    new Question(){
                        Id = 2,
                        Text = "Êtes-vous une femme ou un homme? a:Femme, b:Homme, c:Je ne veux pas répondre"
                    },
                    new Question(){
                        Id = 3,
                        Text = "Quel journal lisez-vous à la maison? a:La Presse, b:Le Journal de Montréal, c:The Gazette, d:Le Devoir"
                    },
                    new Question(){
                        Id = 4,
                        Text = "Combien de temps accordez-vous à la lecture de votre journal quotidiennement? a:Moins de 10 minutes; b:Entre 10 et 30 minutes, c:Entre 30 et 60 minutes, d:60 minutes ou plus"
                    },
                }
            },
            new Sondage(){
                Id = 2,
                Questions = new List<Question>(){
                    new Question(){
                        Id = 1,
                        Text = "À quelle tranche d'âge appartenez-vous? a:0-25 ans, b:25-50 ans, c:50-75 ans, d:75 ans et +"
                    },
                    new Question(){
                        Id = 2,
                        Text = "Êtes-vous une femme ou un homme? a:Femme, b:Homme, c:Je ne veux pas répondre"
                    },
                    new Question(){
                        Id = 3,
                        Text = "Combien de tasses de café buvez-vous chaque jour? a:Je ne bois pas de café, b:Entre 1 et 5 tasses, c:Entre 6 et 10 tasses, d:10 tasses ou plus"
                    },
                    new Question(){
                        Id = 4,
                        Text = "Combien de consommations alcoolisées buvez-vous chaque jour? a:0, b:1, c:2 ou 3, d:3 ou plus"
                    },
                }
            },
        };

        private List<List<Dictionary<string, int>>> resultats = new List<List<Dictionary<string, int>>>()
        {
           new List<Dictionary<string, int>>(){ 
                new Dictionary<string, int>(){
                    {"a", 0},
                    {"b", 0},
                    {"c", 0},
                    {"d", 0},
                },
                new Dictionary<string, int>(){
                    {"a", 0},
                    {"b", 0},
                    {"c", 0},
                    {"d", 0},
                },
                new Dictionary<string, int>(){
                    {"a", 0},
                    {"b", 0},
                    {"c", 0},
                    {"d", 0},
                },
                new Dictionary<string, int>(){
                    {"a", 0},
                    {"b", 0},
                    {"c", 0},
                    {"d", 0},
                },
           },
           new List<Dictionary<string, int>>(){
                new Dictionary<string, int>(){
                    {"a", 0},
                    {"b", 0},
                    {"c", 0},
                    {"d", 0},
                },
                new Dictionary<string, int>(){
                    {"a", 0},
                    {"b", 0},
                    {"c", 0},
                    {"d", 0},
                },
                new Dictionary<string, int>(){
                    {"a", 0},
                    {"b", 0},
                    {"c", 0},
                    {"d", 0},
                },
                new Dictionary<string, int>(){
                    {"a", 0},
                    {"b", 0},
                    {"c", 0},
                    {"d", 0},
                },
           },
        };

        private static IDictionary<string, bool> asVoted = new Dictionary<string, bool>();

        // GET: api/sondages
        [HttpGet]
        public ActionResult<IEnumerable<Sondage>> Get()
        {
            return sondages;
        }

        // GET api/sondages/5
        [HttpGet("{id}")]
        public ActionResult<Sondage> Get(int id)
        {
            if (id != 1 && id != 2) {
                return NotFound();
            }
            return sondages[id-1];
        }

        // POST api/sondages
        [HttpPost]
        [Authorize(Policy = Policies.User)]
        public ActionResult Post(Reponse reponse)
        {
            var jwt = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var user = handler.ReadJwtToken(jwt).Claims.Where(c => c.Type == "username").FirstOrDefault().Value;
            if (asVoted.ContainsKey(user) && asVoted[user]) {
                return Unauthorized();
            }
            asVoted.Add(user, true);
            return Ok();
        }
    }
}
