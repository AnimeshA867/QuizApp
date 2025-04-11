using QuizPortal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizPortal.Repositories
{
    public interface IQuizRepository
    {
        Task CreateQuizAsync(Quiz quiz);

        Task<Quiz> GetQuizAsync(int id);

        Task<ICollection<Quiz>> GetAllQuizzesAsync();

        void DeleteQuiz(Quiz quiz);
        void UpdateQuiz(Quiz quiz); // Add this method
    }
}
