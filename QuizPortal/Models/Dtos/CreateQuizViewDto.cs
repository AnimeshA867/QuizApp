﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizPortal.Models.Dtos
{
    public class CreateQuizViewDto


    {
        public int QuizId { get; set; }

        [Required]
        public List<ArticleDto> ArticleList { get; set; }

        [Required]
        public string SelectedArticleId { get; set; }

        [Required]
        public QuestionDto[] QuestionArr { get; set; } = new QuestionDto[4];

        public string ErrorMessage { get; set; }

       

        
    }
}
