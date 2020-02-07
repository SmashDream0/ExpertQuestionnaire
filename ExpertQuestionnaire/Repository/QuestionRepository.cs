using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.Repository
{
    public class QuestionRepository : BaseRepository<POCO.Question>
    {
        public QuestionRepository(Context.IContext context) : base(context)
        { }

        /// <summary>
        /// Найти вопросы по ключу опроса
        /// </summary>
        /// <param name="questionnaireKey"></param>
        /// <returns></returns>
        public IEnumerable<POCO.Question> FindByQuestionnaireKey(int questionnaireKey)
        {
            var specification = new Specification.Question.ByQuestionnaireKey(questionnaireKey);

            var result = this.Find(specification);

            return result;
        }

        /// <summary>
        /// Найти вопросы по ключу опроса
        /// </summary>
        /// <param name="questionnaireKey"></param>
        /// <returns></returns>
        public int CountByQuestionnaireKey(int questionnaireKey)
        {
            var specification = new Specification.Question.ByQuestionnaireKey(questionnaireKey);

            var result = this.Count(specification);

            return result;
        }
    }
}