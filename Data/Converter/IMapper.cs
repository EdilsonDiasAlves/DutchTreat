using System.Collections.Generic;

namespace DutchTreat.Data
{
    // Work as a parser from an object to another being O (Origin object) and D (Destiny object)
    public interface IMapper<O,D>
    {
        D Map(O origin);
        IEnumerable<D> Map(IEnumerable<O> origin);
    }
}
