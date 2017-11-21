using System.Collections;
using System.Collections.Generic;

namespace WOPR.Infrastructure.Classes
{
    public class Maybe<T> : IEnumerable<T>
    {
        IEnumerable<T> Content { get; }

        Maybe(IEnumerable<T> content)
        {
            Content = content;
        }

        public static Maybe<T> Some(T value) => new Maybe<T>(new[] { value });

        public static Maybe<T> None() => new Maybe<T>(new T[0]);

        public IEnumerator<T> GetEnumerator() => Content.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }
}
