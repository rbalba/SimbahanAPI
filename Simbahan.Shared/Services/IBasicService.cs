using System.Collections.Generic;

namespace Simbahan.Services
{
    public interface IBasicService<T>
    {
        T Create(T model);

        T Find(int id);

        T Update(int id, T model);

        void Delete(T model);

        List<T> Get(int relationId = 0, int relationId2 = 0, int relationId3 = 0, int relationId4 = 0);
    }
}