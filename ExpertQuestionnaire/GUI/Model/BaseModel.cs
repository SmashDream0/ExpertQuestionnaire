using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertQuestionnaire.GUI.Model
{
    public abstract class BaseModel<TGUIEntity, TPOCO>
        where TGUIEntity : Entity.BaseTypedDTO<TPOCO>
        where TPOCO : POCO.BasePOCO
    {
        public BaseModel(Repository.BaseRepository<TPOCO> mainRepository)
        { MainRepository = mainRepository; }

        public Repository.BaseRepository<TPOCO> MainRepository
        { get; private set; }

        public virtual void Save()
        {
            InnerSave();
            MainRepository.Save();
        }

        protected abstract void InnerSave();
    }
}
