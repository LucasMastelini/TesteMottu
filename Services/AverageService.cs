using TesteMottu.Entitys.DTO_s;

namespace TesteMottu.Entitys
{
    public class AverageService
    {
        public double CalculateOverallAverage(List<Student> students)
        {
            double sum = students.Sum(s => s.Notas.Sum());
            int count = students.Sum(s => s.Notas.Count);
            double average = sum / count;
            return average;
        }

        public List<StudentDto> CalculateIndividualAverages(List<Student> students)
        {
            List<StudentDto> averages = new List<StudentDto>();
            foreach (var student in students)
            {
                double average = student.Notas.Average();
                StudentDto studentAverage = new StudentDto
                {
                    Nome = student.Nome,
                    Media = average
                };
                averages.Add(studentAverage);
            }
            return averages;
        }

        public StudentDto GetStudentWithBestAverage(List<Student> students)
        {
            var bestStudent = students.OrderByDescending(s => s.Notas.Average()).FirstOrDefault();
            if (bestStudent != null)
            {
                double average = bestStudent.Notas.Average();
                StudentDto studentAverage = new StudentDto
                {
                    Nome = bestStudent.Nome,
                    Media = average
                };
                return studentAverage;
            }
            return null;
        }
    }
}