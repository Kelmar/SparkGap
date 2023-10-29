using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace SG.Utilities
{
    /// <summary>
    /// Wrapper around a HashSet but with notifications.
    /// </summary>
    /// <typeparam name="T">The type to store in the set.</typeparam>
    public class NotifySet<T> : ISet<T>, INotifyCollectionChanged
    {
        private readonly HashSet<T> m_base = new HashSet<T>();

        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        public int Count => m_base.Count;

        public bool IsReadOnly => false;

        public bool Add(T item)
        {
            bool b = m_base.Add(item);

            if (b && CollectionChanged != null)
            {
                var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item);
                CollectionChanged(this, args);
            }

            return b;
        }

        public void Clear()
        {
            if (!m_base.Any())
                return;

            if (CollectionChanged != null)
            {
                var items = new List<T>(m_base);

                m_base.Clear();

                var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, items);
                CollectionChanged(this, args);
            }
            else
                m_base.Clear();
        }

        public bool Contains(T item) => m_base.Contains(item);

        public void CopyTo(T[] array, int arrayIndex)
        {
            m_base.CopyTo(array, arrayIndex);
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            var items = other.ToList();
            var toNotify = new List<T>(items.Count);

            foreach (var item in items)
            {
                if (m_base.Remove(item))
                    toNotify.Add(item);
            }

            if (toNotify.Any() && CollectionChanged != null)
            {
                var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, toNotify);
                CollectionChanged(this, args);
            }
        }

        public IEnumerator<T> GetEnumerator() => m_base.GetEnumerator();

        public void IntersectWith(IEnumerable<T> other)
        {
            var items = new HashSet<T>(other);

            var toRemove = new List<T>(Count);

            foreach (var item in m_base)
            {
                if (!items.Contains(item))
                    toRemove.Add(item);
            }

            ExceptWith(toRemove);
        }

        public bool IsProperSubsetOf(IEnumerable<T> other) => m_base.IsProperSubsetOf(other);

        public bool IsProperSupersetOf(IEnumerable<T> other) => m_base.IsProperSupersetOf(other);

        public bool IsSubsetOf(IEnumerable<T> other) => m_base.IsSubsetOf(other);

        public bool IsSupersetOf(IEnumerable<T> other) => m_base.IsSupersetOf(other);

        public bool Overlaps(IEnumerable<T> other) => m_base.Overlaps(other);

        public bool Remove(T item)
        {
            bool b = m_base.Remove(item);

            if (b && CollectionChanged != null)
            {
                var arg = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item);
                CollectionChanged(this, arg);
            }

            return b;
        }

        public bool SetEquals(IEnumerable<T> other) => m_base.SetEquals(other);

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            var items = new HashSet<T>(other);

            var toRemove = new List<T>();
            var toAdd = new List<T>();

            foreach (var item in m_base)
            {
                if (!items.Contains(item))
                    toRemove.Add(item);
            }

            foreach (var item in items)
            {
                if (m_base.Add(item))
                    toAdd.Add(item);
            }

            m_base.ExceptWith(toRemove);

            if (CollectionChanged != null && (toRemove.Any() || toAdd.Any()))
            {
                var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, toAdd, toRemove);
                CollectionChanged(this, args);
            }
        }

        public void UnionWith(IEnumerable<T> other)
        {
            var items = other.ToList();

            var added = new List<T>(items.Count);

            foreach (var item in items)
            {
                if (m_base.Add(item))
                    added.Add(item);
            }

            if (CollectionChanged != null && added.Any())
            {
                var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, added);
                CollectionChanged(this, args);
            }
        }

        void ICollection<T>.Add(T item)
        {
            if (m_base.Add(item) && CollectionChanged != null)
            {
                var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item);
                CollectionChanged(this, args);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => m_base.GetEnumerator();
    }
}