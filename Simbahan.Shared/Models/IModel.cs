using System.Collections.Generic;

namespace Simbahan.Models
{
    public interface IModel<T>
    {
        T Create();

        T Find(int id);

        T Update();

        void Delete();

        List<T> Get(int relationId = 0, int relationId2 = 0, int relationId3 = 0, int relationId4 = 0);
    }
}