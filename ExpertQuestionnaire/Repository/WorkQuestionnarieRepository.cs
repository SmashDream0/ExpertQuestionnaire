using System.Collections.Generic;
using ExpertQuestionnaire.Specification.WorkQuestionnaire;

namespace ExpertQuestionnaire.Repository
{
    public class WorkQuestionnaireRepository : BaseRepository<POCO.WorkQuestionnaire>
    {
        public WorkQuestionnaireRepository(Context.IContext context) : base(context)
        { }

        /// <summary>
        /// Найти не отвеченные, по ключу пользователя(эксперта)
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns></returns>
        public IEnumerable<POCO.WorkQuestionnaire> FindByUserKeyAndAnswers(int userKey, bool isAnswered)
        {
            var specification = new ByUserKey(userKey) & new ByAnswered(isAnswered, userKey);
            var result = Find(specification);

            return result;
        }
    }
}