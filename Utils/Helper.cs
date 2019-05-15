using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OnBoard.Utils
{
    public class Helper<T> : IEnumerable<T>
    {
        public static IEnumerable<T> Paginating(IEnumerable<T> resource, int pageIndex, int pageSize)
        {
            var count = resource.Count();
            return resource.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}


//   private IEnumerable<CustomerReturnResource> Paginating(IEnumerable<CustomerReturnResource> resource, int pageIndex, int pageSize)
//     {
//         var count = resource.Count();
//         return resource.Skip((pageIndex - 1) * pageSize).Take(pageSize);
//     }