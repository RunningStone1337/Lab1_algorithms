using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Lab4
{
    public class MyDeque<T>:IEnumerable<T>
    {

        public int Count { get; private set; }
        DoubleNode<T> Head { get; set; }

        DoubleNode<T> Tail { get; set; }
        public MyDeque() : base()
        {
            Tail = null;
            Head = null;
        }

        /// <summary>
        ///  Добавляет элемент в конец дека
        /// </summary>
        /// <returns></returns>
        public void Enqueue(T val)
        {
            DoubleNode<T> newbie = new DoubleNode<T>(val);
            if (Head == null)
            {
                Head = newbie;
                Tail = newbie;
                Count++;
                return;
            }
            if (Tail != null)
            {
                Tail.Right = newbie;
                newbie.Left = Tail;
                Tail = newbie;
                Count++;
            }
        }
        /// <summary>
        /// Извлекает головной элемент
        /// </summary>
        /// <returns></returns>
        public T RemoveFirst()
        {
            if (Head != null)
            {
                var val = Head.Value;
                var newhead = Head.Right;
                Head.Right = null;
                Head.Left = null;
                if (newhead != null)
                {
                    newhead.Left = null;
                }
                Head = newhead;
                Count--;
                if (Count<2)
                {
                    Tail = Head;
                }
                return val;
            }
            else throw new InvalidOperationException("Dequeue empty.");
        }
        /// <summary>
        /// Извлекает конечный элемент
        /// </summary>
        /// <returns></returns>
        public T RemoveLast()
        {
            if (Tail != null)
            {
                var val = Tail.Value;
                var newtail = Tail.Left;
                Tail.Left = null;
                if (newtail != null)
                {
                    newtail.Right = null;
                }
                Tail = newtail;
                Count--;
                if (Count<2)
                {
                    Head = Tail;
                }
                return val;
            }
            else throw new InvalidOperationException("Dequeue empty.");
        }

        /// <summary>
        /// Добавляет элемент в начало дека
        /// </summary>
        /// <param name="val"></param>
        public void Dequeue(T val)
        {
            DoubleNode<T> newbie = new DoubleNode<T>(val);
            if (Head == null)
            {
                Head = newbie;
                Tail = newbie;
                Count++;
                return;
            }
            if (Head != null)
            {
                Head.Left = newbie;
                newbie.Right = Head;
                Head = newbie;
                Count++;
            }
        }
        /// <summary>
        /// Возвращает значение головного элемента без извлечения
        /// </summary>
        /// <returns></returns>
        public T PopHead()
        {
            if (Head != null)
            {
                return Head.Value;
            }
            else throw new InvalidOperationException("Dequeue empty.");
        }
        /// <summary>
        /// Возвращает значение последнего элемента без извлечения
        /// </summary>
        /// <returns></returns>
        public T PopTail()
        {
            if (Tail != null)
            {
                return Tail.Value;
            }
            else throw new InvalidOperationException("Dequeue empty.");

        }
        /// <summary>
        /// Проверяет дек на пустоту
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            if (Count != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var temp = Head;
            while (temp.HasNext())
            {
                yield return temp.Value;
                temp = temp.Right;
            }
            yield return temp.Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private protected class DoubleNode<T>
        {
            internal T Value { get; set; }
            protected internal DoubleNode<T> Right { get; set; }
            protected internal DoubleNode<T> Left { get; set; }
            protected internal DoubleNode(T val)
            {
                Value = val;
                Right = null;
                Left = null;
            }

            protected internal bool HasNext()
            {
                if (Right!=null)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
