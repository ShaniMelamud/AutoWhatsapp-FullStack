using System;
using System.Collections.Generic;
using System.Text;

namespace Tomedia
{
    public class AnswerModel
    {
        public int AnswerId { get; set; }
        public string AnswerContent { get; set; }

        public AnswerModel()
        {

        }

        public AnswerModel(Answer answer)
        {
            AnswerId = answer.AnswerId;
            AnswerContent = answer.AnswerContent;
        }

        public Answer ConvertToAnswer()
        {
            return new Answer
            {
                AnswerId = AnswerId,
                AnswerContent = AnswerContent
            };
        }
    }
}
