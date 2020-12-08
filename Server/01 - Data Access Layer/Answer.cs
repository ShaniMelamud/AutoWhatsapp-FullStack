using System;
using System.Collections.Generic;

namespace Tomedia
{
    public partial class Answer
    {
        public Answer()
        {
            Questions = new HashSet<Question>();
        }

        public int AnswerId { get; set; }
        public string AnswerContent { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
