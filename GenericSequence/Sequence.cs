using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Collections.Immutable;
using System.Text;

namespace JesseRussell.Collections
{
    /// <summary>
    /// Represents a generic immutable sequence of T. Equality is based on value, not reference, like strings.
    /// </summary>
    public readonly struct Sequence<T> : IEnumerable<T>, IEquatable<Sequence<T>>
    {
        #region constructors
        public Sequence(ImmutableArray<T> elements) => Elements = elements;
        public Sequence(IEnumerable<T> elements) => Elements = elements?.ToImmutableArray() ?? emptyElements;
        #endregion

        #region public Properties
        public ImmutableArray<T> Elements { get; }
        public T this[int i] => Elements[i];
        public int Length => Elements.Length;
        #endregion

        #region public Methods
        public bool Equals(Sequence<T> other)
        {
            if (Elements == other.Elements)
                return true;
            else if (Elements.Length == other.Elements.Length)
            {
                var enu = Elements.GetEnumerator();
                var o_enu = other.Elements.GetEnumerator();

                while (enu.MoveNext() & o_enu.MoveNext())
                    if (!(enu.Current?.Equals(o_enu.Current) ?? o_enu.Current == null)) return false;

                return true;
            }
            else return false;
        }
        public override bool Equals(object obj)
        {
            if (obj is Sequence<T> s)
                return Equals(s);
            else
                return false;
        }
        public override int GetHashCode()
        {
            int result = default;

            var enu = Elements.GetEnumerator();
            if    (enu.MoveNext()) result = HashCode.Combine(enu.Current);
            while (enu.MoveNext()) result = HashCode.Combine(result, enu.Current);

            return result;
        }
        public string ToString(string delim, string open = "(", string close = ")")
        {
            StringBuilder result = new StringBuilder();
            result.Append(open);                                           // opening character or string. ie: (
            var enu = Elements.GetEnumerator();
            if (enu.MoveNext()) result.Append(enu.Current.ToString());     // first element, without deliminator
            while (enu.MoveNext()) result.Append($"{delim}{enu.Current}"); // additional elements, with deliminators
            result.Append(close);                                          // closing character or string. ie: )
            return result.ToString();
        }
        public override string ToString() => ToString(", ");
        public IEnumerator<T> GetEnumerator() { foreach (T item in Elements) yield return item; }
        #endregion

        #region methods
        IEnumerator IEnumerable.GetEnumerator() { foreach (T item in Elements) yield return item; }
        #endregion

        #region public static Methods
        public static bool operator ==(Sequence<T> left, Sequence<T> right) => left.Equals(right);
        public static bool operator !=(Sequence<T> left, Sequence<T> right) => !(left == right);
        #endregion

        #region private static readonly fields
        private static readonly ImmutableArray<T> emptyElements = ImmutableArray.Create(new T[0]);
        #endregion
    }

    public static class SequenceExtensions
    {
        public static Sequence<T> ToSequence<T>(this IEnumerable<T> items) => new Sequence<T>(items);

    }
}
