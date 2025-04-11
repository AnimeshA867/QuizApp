using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizPortal.Helper;
using QuizPortal.Models;
using QuizPortal.Models.Dtos;
using QuizPortal.Proxies;
using QuizPortal.Repositories;

namespace QuizPortal.Controllers
{
    public class QuizController : Controller
    {
        private readonly IWiredProxy _wiredProxy;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IMapper _mapper;

        public QuizController(IWiredProxy wiredProxy, IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _wiredProxy = wiredProxy;
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
        }

        [BindProperty]
        public CreateQuizViewDto CreateQuizViewDto { get; set; }

        public async Task<IActionResult> CreateQuiz()
        {
            if (HttpContext.Session.GetString(Constants.SessionUserId) == null)
            {
                return Redirect(Url.Action("Login", "User"));
            }

            var articleList = await _wiredProxy.GetLastFiveArticlesAsync();

            CreateQuizViewDto = new CreateQuizViewDto();
            CreateQuizViewDto.ArticleList = articleList.ToList();

            return View(CreateQuizViewDto);
        }

        [HttpPost]
        [ActionName("CreateQuiz")]
        public async Task<IActionResult> CreateQuizPost()
        {
            CreateQuizViewDto.ErrorMessage = null;

            if (ModelState.IsValid)
            {
                // Distinct question control
                if (CreateQuizViewDto.QuestionArr.Select(q => q.QuestionText).Distinct().Count() != 4)
                {
                    CreateQuizViewDto.ErrorMessage = "Questions should be unique";

                    return View(CreateQuizViewDto);
                }

                // Distinct answers control
                foreach (var q in CreateQuizViewDto.QuestionArr)
                {
                    if (q.AnswerA == q.AnswerB ||
                        q.AnswerA == q.AnswerC ||
                        q.AnswerA == q.AnswerD ||
                        q.AnswerB == q.AnswerC ||
                        q.AnswerB == q.AnswerD ||
                        q.AnswerC == q.AnswerD)
                    {
                        CreateQuizViewDto.ErrorMessage = "A question cannot have the same answer more than once";

                        return View(CreateQuizViewDto);
                    }
                }

                var transaction = await _repositoryFactory.BeginTransactionAsync();

                var quizRepository = _repositoryFactory.GetQuizRepository();

                var selectedArt = CreateQuizViewDto.ArticleList.FirstOrDefault(a => a.ArticleId == CreateQuizViewDto.SelectedArticleId);

                if (selectedArt == null)
                {
                    return View("EditQuiz",CreateQuizViewDto);
                }

                var quiz = _mapper.Map<Quiz>(selectedArt);
                await quizRepository.DeleteQuizAsync(quiz);
                await quizRepository.CreateQuizAsync(quiz);
                await _repositoryFactory.SaveAsync();

                var questionRepository = _repositoryFactory.GetQuestionRepository();

                foreach (var item in CreateQuizViewDto.QuestionArr)
                {
                    var ques = _mapper.Map<Question>(item);
                    ques.QuizId = quiz.Id;

                    await questionRepository.CreateQuestionAsync(ques);
                }

                await _repositoryFactory.SaveAsync();

                transaction.Commit();

                return RedirectToAction("Index", "Quiz");
            }

            return View(CreateQuizViewDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (HttpContext.Session.GetString(Constants.SessionUserId) == null)
            {
                return Redirect(Url.Action("Login", "User"));
            }

            var quizRepository = _repositoryFactory.GetQuizRepository();
            var questionRepository = _repositoryFactory.GetQuestionRepository();

            var quiz = await quizRepository.GetQuizAsync(id);
            if (quiz == null)
            {
                return RedirectToAction("Index");
            }

            var questions = await questionRepository.GetAllQuestionsAsync(id);

            var quizDto = _mapper.Map<QuizDto>(quiz);
            var questionDtos = _mapper.Map<List<QuestionDto>>(questions);

            CreateQuizViewDto = new CreateQuizViewDto
            {
                QuizId = quiz.Id,
                SelectedArticleId = quiz.ArticleId,
                QuestionArr = questionDtos.ToArray(),
                ArticleList = (await _wiredProxy.GetLastFiveArticlesAsync()).ToList(),
                ErrorMessage = null
            };

            //return View("EditQuiz", CreateQuizViewDto);
            return View();
        }

        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> EditPost()
        {
            CreateQuizViewDto.ErrorMessage = null;

            if (ModelState.IsValid)
            {
                // Validation logic...

                var transaction = await _repositoryFactory.BeginTransactionAsync();
                var quizRepository = _repositoryFactory.GetQuizRepository();
                var questionRepository = _repositoryFactory.GetQuestionRepository();

                var quiz = await quizRepository.GetQuizAsync(CreateQuizViewDto.QuizId);
                if (quiz == null)
                {
                    return RedirectToAction("Index");
                }

                // Update quiz properties as needed
                // For example, if you have a Title or Description that can be updated
               

                // Since ArticleId cannot be changed, we skip updating it

                // Update the quiz
                quizRepository.UpdateQuiz(quiz);
                await _repositoryFactory.SaveAsync();

                // Delete existing questions
                var existingQuestions = await questionRepository.GetAllQuestionsAsync(quiz.Id);
                foreach (var existingQuestion in existingQuestions)
                {
                    questionRepository.DeleteQuestion(existingQuestion);
                }
                await _repositoryFactory.SaveAsync();

                // Add updated questions
                foreach (var item in CreateQuizViewDto.QuestionArr)
                {
                    var ques = _mapper.Map<Question>(item);
                    ques.QuizId = quiz.Id;
                    await questionRepository.CreateQuestionAsync(ques);
                }
                await _repositoryFactory.SaveAsync();

                transaction.Commit();

                return RedirectToAction("Index", "Quiz");
            }

            return View("EditQuiz", CreateQuizViewDto);
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString(Constants.SessionUserId) == null)
            {
                return Redirect(Url.Action("Login", "User"));
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuizzes()
        {
            var quizRepository = _repositoryFactory.GetQuizRepository();

            var quizList = await quizRepository.GetAllQuizzesAsync();

            var quizDtoList = _mapper.Map<List<QuizDto>>(quizList.ToList());

            return Json(new { data = quizDtoList });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var quizRepository = _repositoryFactory.GetQuizRepository();

            var quizFromDb = await quizRepository.GetQuizAsync(id);

            if (quizFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            quizRepository.DeleteQuiz(quizFromDb);
            await _repositoryFactory.SaveAsync();

            return Json(new { success = true, message = "Delete successful" });
        }

        [HttpGet]
        public async Task<IActionResult> Quiz(int id)
        {
            if (HttpContext.Session.GetString(Constants.SessionUserId) == null)
            {
                return Redirect(Url.Action("Login", "User"));
            }

            var quizRepository = _repositoryFactory.GetQuizRepository();
            var questionRepository = _repositoryFactory.GetQuestionRepository();

            var quizFromDb = await quizRepository.GetQuizAsync(id);

            // If quiz does not exist
            if (quizFromDb == null)
            {
                return Redirect(Url.Action("Index", "Quiz"));
            }

            var questionList = await questionRepository.GetAllQuestionsAsync(id);

            var quizDto = _mapper.Map<QuizDto>(quizFromDb);
            var questionDtoList = _mapper.Map<List<QuestionDto>>(questionList);

            var quizViewDto = new QuizViewDto
            {
                QuizDto = quizDto,
                QuestionDtoList = questionDtoList
            };

            return View(quizViewDto);
        }
    }



}
