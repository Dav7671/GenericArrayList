using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericArrayList
{
    public class _ArrayList<T>
    { 
        private int _size;
        private int _version;
        private T[] _items;
        private const int _defaultCapacity = 4;
        private static readonly T[] _emptyArray = new T[0];
        public _ArrayList(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (capacity == 0)
            {
                _items = _emptyArray;
            }

            else
            {
                _items = new T[capacity];
            }

        }

        public virtual int Capacity
        {
            get
            {
                return _items.Length;
            }
            set
            {
                if (value < _size)
                {
                    throw new ArgumentOutOfRangeException();
                }

                if (value != _items.Length)
                {
                    if (value > 0)
                    {
                        T[] newItems = new T[value];
                        if (_size > 0)
                        {
                            Array.Copy(_items, 0, newItems, 0, _size);
                        }
                        _items = newItems;
                    }
                    else
                    {
                        _items = new T[_defaultCapacity];
                    }
                }
            }
        }

        public virtual int Count
        {
            get
            {
                return _size;
            }
        }
        public virtual bool IsFixedSize
        {
            get { return false; }
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        public virtual T this[int index]
        {
            get
            {
                if (index < 0 || index >= _size) throw new ArgumentOutOfRangeException();
                return _items[index];
            }
            set
            {
                if (index < 0 || index >= _size) throw new ArgumentOutOfRangeException();
                _items[index] = value;
                _version++;
            }
        }
        public void EnsureCapacity(int min)
        {
            if (_items.Length < min)
            {
                int newCapacity = _items.Length == 0 ? _defaultCapacity : _items.Length * 2;
                if ((uint)newCapacity > 0X7FEFFFFF)
                    newCapacity = 0X7FEFFFFF;
                if (newCapacity < min)
                    newCapacity = min;
                Capacity = newCapacity;
            }
        }

        public virtual int Add(T value)
        {
            if (_size == _items.Length)
            {
                EnsureCapacity(_size + 1);
            }
             // System.NullReferenceException: 'Object reference not set to an instance of an object.'

                
            _items[_size] = value;
            _version++;
            return _size++;
        }


        public virtual void Clear()
        {
            if (_size > 0)
            {
                Array.Clear(_items, 0, _size);
                _size = 0;
            }
            _version++;
        }

        public void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            if (_size - index < count)
            {
                throw new ArgumentOutOfRangeException();
            }
            Array.Copy(_items, index, array, arrayIndex, count);
        }

        public virtual bool Contains(T item)
        {
            if (item == null)
            {
                for (int i = 0; i < _size; i++)
                    if (_items[i] == null)
                    {
                        return true;
                    }

                return false;
            }
            else
            {
                for (int i = 0; i < _size; i++)
                    if ((_items[i] != null) && (_items[i].Equals(item)))
                    {
                        return true;
                    }

                return false;
            }
        }

        public virtual int IndexOf(T value)
        {
            return Array.IndexOf((Array)_items, value, 0, _size);
        }

        public virtual int IndexOf(T value, int startIndex)
        {
            if (startIndex > _size)
            {
                throw new ArgumentOutOfRangeException();
            }

            return Array.IndexOf((Array)_items, value, startIndex, _size - startIndex);
        }

        public virtual int IndexOf(T value, int startIndex, int count)
        {
            if (startIndex > _size)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (count < 0 || startIndex > _size - count)
            {
                throw new ArgumentOutOfRangeException();
            }


            return Array.IndexOf((Array)_items, value, startIndex, count);
        }

        public virtual void Insert(int index, T value)
        {
            if (index < 0 || index > _size)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (_size == _items.Length)
            {
                EnsureCapacity(_size + 1);
            }

            if (index < _size)
            {
                Array.Copy(_items, index, _items, index + 1, _size - index);
            }
            _items[index] = value;
            _size++;
            _version++;
        }
        public virtual void Remove(T obj)
        {
            int index = IndexOf(obj);
            if (index >= 0)
            {
                RemoveAt(index);
            }

        }

        public virtual void RemoveAt(int index)
        {
            if (index < 0 || index >= _size)
            {
                throw new ArgumentOutOfRangeException();
            }

            _size--;
            if (index < _size)
            {
                Array.Copy(_items, index + 1, _items, index, _size - index);
            }
            _items[_size] = default(T);
            _version++;
        }

        public _ListEnumerator GetEnumerator()
        {
            return new _ListEnumerator(_items, _version);
        }

        public class _ListEnumerator
        {
            private int _size;
            private int _count;
            private T[] _items;
            public _ListEnumerator(T[] items, int size)
            {
                _items = items;
                _size = size;
            }

            public T Current
            {
                get
                {
                    return _items[_count++];
                }
            }

            public bool MoveNext()
            {
                return _count < _size;
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }

    }
}


