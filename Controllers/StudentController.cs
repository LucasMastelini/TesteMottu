using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TesteMottu.Entitys;
using TesteMottu.Entitys.DTO_s;

namespace TesteMottu.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {



        private readonly AverageService _averageService;
        private readonly ILogger<StudentController> _logger;
        public StudentController(AverageService averageService, ILogger<StudentController> logger)
        {
            _logger = logger;
            _averageService = averageService;
        }

        [HttpPost]
        public ActionResult<string> CalculateOverallAverage([FromBody] List<Student> students)
        {
            double average = _averageService.CalculateOverallAverage(students);
            var result = $"Média Geral: {average:N2}"; // Formatação com duas casas decimais
            return result;
        }

        [HttpPost("individual")]
        public ActionResult<IEnumerable<StudentDto>> CalculateIndividualAverages([FromBody] List<Student> students)
        {
            IEnumerable<StudentDto> studentAverages = _averageService.CalculateIndividualAverages(students);
            foreach (var studentAverage in studentAverages)
            {
                studentAverage.Media = Math.Round(studentAverage.Media, 2); // Arredonda para duas casas decimais
            }
            return Ok(studentAverages);
        }

        [HttpPost("best")]
        public ActionResult<StudentDto> GetStudentWithBestAverage([FromBody] List<Student> students)
        {
            StudentDto bestStudent = _averageService.GetStudentWithBestAverage(students);
            if (bestStudent != null)
            {
                bestStudent.Media = Math.Round(bestStudent.Media, 2); // Arredonda para duas casas decimais
            }
            return bestStudent;
        }
    }
}