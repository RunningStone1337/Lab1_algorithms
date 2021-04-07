using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4
{
    public class MyStack<T>
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
        internal class Node<T>
        {
            internal T Value { get; set; }
            internal Node<T> Next { get; set; }

            internal Node(T val)
            {
                Value = val;
                Next = null;
            }
        }
    }
}
