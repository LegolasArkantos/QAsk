using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Mscc.GenerativeAI;
using Microsoft.Extensions.Configuration;


namespace CrudApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AiController : ControllerBase
    {
        private readonly string _apiKey;
        public AiController(IConfiguration configuration)
        {
            _apiKey = configuration["AI:ApiKey"]; 
        }

        [HttpPost("Generate")]
        public async Task<IActionResult> GenerateAiResponse([FromBody] string prompt)
        {
            if (string.IsNullOrWhiteSpace(prompt))
            {

                return BadRequest("Prompt cant be empty");
            }

            var googleAI = new GoogleAI(apiKey: _apiKey);
            var model = googleAI.GenerativeModel(model: Model.Gemini15Pro);


            try
            {
                var response = await model.GenerateContent(prompt);
                Console.WriteLine(response);
                return Ok(new { response = response.Text });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }


    }
}
