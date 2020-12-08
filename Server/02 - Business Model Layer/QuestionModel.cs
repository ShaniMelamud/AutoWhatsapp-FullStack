using System;
using System.Collections.Generic;
using System.Text;

namespace Tomedia
{
    public class QuestionModel
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public int BusinessId { get; set; }
        public string QuestionContent { get; set; }

        public QuestionModel()
        {

        }

        public QuestionModel(Question question)
        {
            QuestionId = question.QuestionId;
            AnswerId = question.AnswerId;
            BusinessId = question.BusinessId;
            QuestionContent = question.QuestionContent;
        }

        public Question ConvertToQuestion()
        {
            return new Question
            {
                QuestionId = QuestionId,
                AnswerId = AnswerId,
                BusinessId = BusinessId,
                QuestionContent = QuestionContent
            };
        }
    }
}
