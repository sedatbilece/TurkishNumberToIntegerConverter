using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TurkishNumberToIntegerConverter.Services;

namespace TurkishNumberToIntegerConverter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IAppConverterService _converterService;

        public ValuesController(IAppConverterService converterService)
        {
            _converterService = converterService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] JsonElement data)
        {
            if (data.TryGetProperty("UserText", out var userTextElement))
            {
                string userText = userTextElement.GetString();


               var  input = userText.ToLower();
               var seperatedText = _converterService.SeparateTheWords(input);

               var outputText = _converterService.ParseTheSentence(seperatedText);

                var response = new { Output = outputText };
                return Ok(response);
            }
            else
            {
                return BadRequest("Invalid JSON format. Expecting { \"UserText\": \"some words\" }");
            }
        }



    }
}
