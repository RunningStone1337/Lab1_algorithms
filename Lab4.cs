#define stack
#define deque

using System;
using System.Diagnostics;

namespace Lab4
{
    public static class Lab4
    {
        internal static void Run4Lab()
        {
            Random rnd = new Random();
            Stopwatch sw = new Stopwatch();
#if stack
            #region Stack
            MyStack<int> stack = new MyStack<int>();
            stack.Push(0);
            stack.Push(3);
            var peek = stack.Peek();
            stack.Pop();
            stack.Pop();
            #endregion
#endif
#if deque
            #region Deque
            MyDequeqe<int> deque = new MyDequeqe<int>();
            deque.Enqueue(0);
            deque.Enqueue(3);
            var dpeek = deque.Dequeue();
            deque.Pop();
            deque.Pop();
            deque.Pop();
            #endregion
#endif
        }
    }
}
