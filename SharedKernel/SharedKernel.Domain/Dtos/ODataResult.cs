using System.Collections.Generic;

namespace SharedKernel.Domain.Dtos
{
    public class ODataResult<T>
    {
        public ResultODataInterno<T> d;

        public ODataResult(int count, IList<T> results)
        {
            d = new ResultODataInterno<T> { __count = count, results = results };
        }

        public ODataResult(IList<T> results)
        {
            d = new ResultODataInterno<T> { __count = results.Count, results = results };
        }

        public ODataResult()
        {
        }
    }

    public class ResultODataInterno<T>
    {
        public int __count { get; set; }
        public IList<T> results { get; set; }
    }
}
