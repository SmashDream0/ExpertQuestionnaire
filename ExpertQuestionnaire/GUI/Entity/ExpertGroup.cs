using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.GUI.Entity
{
    public partial class ExpertGroup : BaseTypedDTO<POCO.ExpertGroup>
    {
        public ExpertGroup() : base() { }
        public ExpertGroup(POCO.ExpertGroup expertGroup) : base(expertGroup) { }

        static ExpertGroup()
        { Initialize(typeof(ExpertGroup)); }

        public override void Reset()
        {
            base.Reset();

            var experts = new List<ExpertGroupUser>();

            if (InnerObject.Experts != null)
            {
                foreach (var expert in InnerObject.Experts)
                {
                    var newExpert = new ExpertGroupUser(expert);

                    experts.Add(newExpert);
                }
            }

            Experts = experts;

            UpdateExpertsCount();
        }

        public override void Save()
        {
            base.Save();

            UpdateExpertsCount();
        }

        private void UpdateExpertsCount()
        {
            var repository = Binds.Injector.GetInstance<Repository.ExpertGroupUserRepository>() as Repository.ExpertGroupUserRepository;

            var result = repository.CountByExpertGroupKey(this.Key);

            ExpertsCount = result;
        }

        private string _name;
        private int _expertsCount;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                PropertyChangedAction("Name");
            }
        }

        public int ExpertsCount
        {
            get => _expertsCount;
            set
            {
                _expertsCount = value;
                PropertyChangedAction("ExpertsCount");
            }
        }

        public IEnumerable<ExpertGroupUser> Experts
        { get; private set; }

        public override string ToString()
        {
            return Name;
        }
    }
}