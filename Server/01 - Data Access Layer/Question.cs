using System;
using System.Collections.Generic;

namespace Tomedia
{
    public partial class Question
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public int BusinessId { get; set; }
        public string QuestionContent { get; set; }

        public virtual Answer Answer { get; set; }
        public virtual Business Business { get; set; }
    }
}
