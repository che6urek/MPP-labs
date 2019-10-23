using System;
using System.Collections;
using System.Collections.Generic;

namespace lab6
{
    public class DynamicList<T>: IEnumerable<T>
    {
        private const int InitialSize = 10;
        private const double SizeMultiplier = 2;

        private T[] _items;
        private int _size;
        private int _lastIndex;

        public int Count => _lastIndex + 1;

        private bool IsFull => _lastIndex == _size - 1;

        public T this[int index]
        {
            get => _items[index];
            set => _items[index] = value;
        }
        public DynamicList()
        {
            _items = new T[InitialSize];
            _size = InitialSize;
            _lastIndex = -1;
        }

        public void Add(T value)
        {
            _lastIndex++;
            if (IsFull)
            {
                IncreaseSize();
            }

            _items[_lastIndex] = value;
        }

        private void IncreaseSize()
        {
            _size = (int) Math.Round(_size * SizeMultiplier);
            Array.Resize(ref _items, _size);
        }

        private void DecreaseSize()
        {
            _size = (int) Math.Round(_size / SizeMultiplier);
            _size = _size > InitialSize ? _size : InitialSize;
            Array.Resize(ref _items, _size);
        }

        public void RemoveAt(int index)
        {
            if (index > _lastIndex || index < 0)
            {
                return;
            }
            for (var i = index; i < _lastIndex; i++)
            {
                _items[i] = _items[i + 1];
            }

            _lastIndex--;
            if ((int)Math.Round(_lastIndex * SizeMultiplier * SizeMultiplier) < _size)
            {
                DecreaseSize();
            }
        }

        public void Remove(T value)
        {
            for (var i = 0; i <= _lastIndex; i++)
            {
                if (_items[i].Equals(value))
                {
                    RemoveAt(i);
                    break;
                }
            }
        }

        public void Clear()
        {
            _lastIndex = -1;
            _items = new T[10];
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i <= _lastIndex; i++)
            {
                yield return _items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
