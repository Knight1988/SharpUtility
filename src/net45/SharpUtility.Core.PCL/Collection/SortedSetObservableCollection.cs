using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using SharpUtility.Annotations;

namespace SharpUtility.Collection
{
    public class SortedSetObservableCollection<T> : ICollection<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        private readonly SortedSet<T> _sortedSet;

        public SortedSetObservableCollection()
        {
            _sortedSet = new SortedSet<T>();
        }

        public SortedSetObservableCollection(IComparer<T> comparer)
        {
            _sortedSet = new SortedSet<T>(comparer);
        }

        public SortedSetObservableCollection(IEnumerable<T> collection)
        {
            _sortedSet = new SortedSet<T>(collection);
        }

        public SortedSetObservableCollection(IEnumerable<T> collection, IComparer<T> comparer)
        {
            _sortedSet = new SortedSet<T>(collection, comparer);
        }

        public bool IsSynchronized { get; } = false;
        public object SyncRoot { get; } = null;

        public void RemoveWhere(Predicate<T> match)
        {
            var removed = _sortedSet.Where(p => match(p)).ToList();
            _sortedSet.RemoveWhere(match);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removed));
        }

        public bool Remove(T item)
        {
            var result = _sortedSet.Remove(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
            return result;
        }

        public int Count => _sortedSet.Count;
        public bool IsReadOnly { get; } = false;

        public IEnumerator<T> GetEnumerator()
        {
            return _sortedSet.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            var matchItem = _sortedSet.SingleOrDefault(p => _sortedSet.Comparer.Compare(p, item) == 0);
            bool success;
            if (matchItem == null)
            {
                success = _sortedSet.Add(item);
                if (success) OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
                return;
            }

            _sortedSet.Remove(matchItem);
            success = _sortedSet.Add(item);
            if (success) OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, item, matchItem));
        }

        public void Clear()
        {
            _sortedSet.Clear();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public bool Contains(T item)
        {
            return _sortedSet.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _sortedSet.CopyTo(array, arrayIndex);
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public void CopyTo(Array array, int index)
        {
            _sortedSet.CopyTo((T[])array, index);
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
