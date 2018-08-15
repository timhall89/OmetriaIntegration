using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmIntLib
{
    public static class Extensions
    {
        public static IEnumerable<IEnumerable<T>> Group<T>(this IEnumerable<T> source, int GroupSize)
        {
            if (GroupSize <= 0) throw new ArgumentOutOfRangeException("GroupSize must be greater than 0");

            List<T> _list = new List<T>(); 
            int counter = 0;

            foreach (T o in source)
            {
                _list.Add(o);
                counter++;
                if (counter == GroupSize)
                {
                    counter = 0;
                    yield return _list;
                    _list = new List<T>();
                }
            }
            if (counter != 0) yield return _list;
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T element in source) action(element);
        }

        public static IEnumerable<T> IteratorDelay<T>(this IEnumerable<T> source, int millisecondDelay)
        {
            bool firstYield = true;
            foreach (T o in source)
            {
                if (!firstYield) System.Threading.Thread.Sleep(millisecondDelay);
                yield return o;
                firstYield = false;
            }
        }
    }
}
