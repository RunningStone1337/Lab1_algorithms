using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Lab4
{
    public class MyStack<T>:IEnumerable<T>
    {
        Node<T> Head { get; set; }
        public int Count { get; protected set; }

        public MyStack()
        {
            Count = 0;
            Head = null;
        }
        public T Pop()
        {
            if (Count!=0)
            {
                var val = Peek();
                Head = Head.Next;
                Count--;
                return val;
        }
            throw new InvalidOperationException("Stack empty.");
        }
        virtual public bool IsEmpty()
        {
            if (Count==0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Push(T value)
        {
            Node<T> newbie = new Node<T>(value);
            Count++;
            newbie.Next = Head;
            Head = newbie;
        }
        public T Peek()
        {
            return Head.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var temp = Head;
            while (temp.HasNext())
            {
                yield return temp.Value;
                temp = temp.Next;
            }
            yield return temp.Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        internal class Node<T>
        {
            protected internal T Value { get; set; }
            protected internal Node<T> Next { get; set; }

            protected internal Node(T val)
            {
                Value = val;
                Next = null;
            }

            protected internal bool HasNext()
            {
                if (Next!=null)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
