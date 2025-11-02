using System.Collections;

namespace DataStructuresLib.Models;

/// <summary>
/// A positional doubly linked list that implements the PositionalList ADT.
/// </summary>
/// <typeparam name="T"></typeparam>
public class DLinkedList<T> : IPositionalList<T>, IEnumerable<T>
{
    /// <summary>
    /// The head of the list.
    /// </summary>
    private DLLNode<T>? _head;

    /// <summary>
    /// The tail of the list.
    /// </summary>
    private DLLNode<T>? _tail;

    /// <summary>
    /// The number of the positions/elements in the list.
    /// </summary>
    private int _size;

    // TC: O(1)
    public IPosition<T>? AddAfter(IPosition<T>? pos, T element)
    {
        // 1 + 1 + 1 = 3 ops
        if (pos is DLLNode<T> currPos)
        {
            // 1 + 1 = 2 ops
            DLLNode<T> newPos = new(element, null, currPos);

            // 1 + 1 + 1 + 1 = 4 ops
            if (currPos.GetNext() is DLLNode<T> next)
            {
                newPos.SetNext(next);   // 1 op
                next.SetPrev(newPos);   // 1 op
            }
            else if (currPos.GetNext() is null)
               return AddLast(element);

            // 1 op
            currPos.SetNext(newPos);
            // 1 + 1 = 2 ops
            ++_size;
            // 1 op
            return newPos;
        }
        // 1 op
        return null;
    }

    // TC: O(1)
    public IPosition<T>? AddBefore(IPosition<T>? pos, T element)
    {
        if (pos is DLLNode<T> currPos)
        {
            DLLNode<T> newPos = new(element, currPos, null);

            if (currPos.GetPrev() is DLLNode<T> prev)
            {
                newPos.SetPrev(prev);
                prev.SetNext(newPos);
            }
            else if (currPos.GetPrev() is null)
                return AddFirst(element);

            currPos.SetPrev(newPos);            
            ++_size;
            return newPos;
        }

        return null;
    }

    // TC: O(1)
    public IPosition<T>? AddFirst(T element)
    {
        if (_head == null)
        {
            _head = new(element, null, null);
            _tail = _head;
            ++_size;
            return _head;
        }
        else
        {
            DLLNode<T> newHead = new(element, _head, null);
            _head.SetPrev(newHead);
            _head = newHead;
            ++_size;
            return newHead;
        }
    }

    // TC: O(1)
    public IPosition<T>? AddLast(T element)
    {
        if (_tail == null)
            return AddFirst(element);

        DLLNode<T> newPos = new(element, null, _tail);
        _tail.SetNext(newPos);
        _tail = newPos;
        ++_size;
        return newPos;
    }

    // TC: O(1)
    public IPosition<T>? After(IPosition<T>? pos)
    {
        if (pos is DLLNode<T> currPos && currPos.GetNext() is DLLNode<T> next)
            return next;
        return null;
    }

    // TC: O(1)
    public IPosition<T>? Before(IPosition<T>? pos)
    {
        if (pos is DLLNode<T> currPos && currPos.GetPrev() is DLLNode<T> prev)
            return prev;

        return null;
    }

    // TC: O(1)
    public IPosition<T>? First() => _head;

    // TC: O(1)
    public bool IsEmpty() => Size() == 0;

    // TC: O(1)
    public IPosition<T>? Last() => _tail;

    // TC: O(1)
    public T? Remove(IPosition<T>? pos)
    {
        if (!IsEmpty() && pos is DLLNode<T> currPos)
        {
            DLLNode<T>? prev = currPos.GetPrev();
            DLLNode<T>? next = currPos.GetNext();
            T? element = currPos.GetElement();

            if (prev != null && next != null)
            {
                prev.SetNext(next);
                next.SetPrev(prev);
                currPos.SetNext(null);
                currPos.SetPrev(null);
                --_size;
                return element;
            }
            // remove the last position.
            else if (prev != null)
            {
                prev.SetNext(null);
                currPos.SetPrev(null);
                _tail = prev;
                --_size;
                return element;
            }
            // remove the first position
            else if (next != null)
            {
                next.SetPrev(null);
                currPos.SetNext(null);
                _head = next;
                --_size;
                return element;
            }
            // its the only position in the list.
            else if (pos == _head)
            {
                _head = _tail = null;
                --_size;
                return element;
            }
        }
        return default;
    }

    // TC: O(1)
    public T? Set(IPosition<T>? pos, T newElement)
    {
        if (pos is DLLNode<T> currPos)
        {
            T? oldElement = currPos.GetElement();
            currPos.SetElement(newElement);
            return oldElement;
        }

        return default;
    }

    // TC: O(1)
    public int Size() => _size;

    /// <summary>
    /// Searches the list for an element that matches the argument, and then returns the
    /// position if found, otherwise, returns null. <br /> <br />
    /// TC: O(n), where n is the number of positions in the list.
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    public IPosition<T>? FindPosition(T element)
    {
        if (!IsEmpty())
        {
            DLLNode<T> currPos = _head!;

            while(currPos != null)
            {
                if (currPos.GetElement()!.Equals(element))
                    return currPos;

                currPos = currPos.GetNext()!;
            }
        }

        return null;
    }

    /// <summary>
    /// Returns a generic iterator for this list.
    /// </summary>
    /// <returns></returns>
    public IEnumerator<T> GetEnumerator()
    {
        if (!IsEmpty())
        {
            DLLNode<T> currPos = _head!;

            while (currPos != _tail!.GetNext())
            {
                // return each element in the list one at a time.
                yield return currPos!.GetElement()!;

                currPos = currPos.GetNext()!;
            }
        }
    }

    /// <summary>
    /// Returns an iterator for this list.
    /// </summary>
    /// <returns></returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// A doubly linked list node.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="next"></param>
    /// <param name="prev"></param>
    public class DLLNode<TData>(TData data, DLLNode<TData>? next, DLLNode<TData>? prev) : IPosition<TData>
    {
        /// <summary>
        /// The data stored in the position.
        /// </summary>
        private TData? _data = data;

        /// <summary>
        /// Stores the reference to the next position.
        /// </summary>
        private DLLNode<TData>? _next = next;

        /// <summary>
        /// Stores the reference to the previous position.
        /// </summary>
        private DLLNode<TData>? _prev = prev;

        public TData? GetElement() => _data;

        /// <summary>
        /// It sets the value to be stored in this position.
        /// </summary>
        /// <param name="data"></param>
        public void SetElement(TData data) => _data = data;

        /// <summary>
        /// Returns the reference to the position immediately after this one.
        /// </summary>
        /// <returns></returns>
        public DLLNode<TData>? GetNext() => _next;

        /// <summary>
        /// Makes this position point to the next position.
        /// </summary>
        /// <param name="next"></param>
        public void SetNext(IPosition<TData>? next)
        {
            if (next is DLLNode<TData> nxt)
                _next = nxt;
            else if (next == null)
                _next = null;
        }

        /// <summary>
        /// Returns the reference to the position that immediately preceeds this one.
        /// </summary>
        /// <returns></returns>
        public DLLNode<TData>? GetPrev() => _prev;

        /// <summary>
        /// Makes this position point to a previous position.
        /// </summary>
        /// <param name="prev"></param>
        /// <exception cref="InvalidCastException"></exception>
        public void SetPrev(IPosition<TData>? prev)
        {
            if (prev is DLLNode<TData> prv)
                _prev = prv;
            else if (prev == null)
                _prev = null;
        }
    }
}
