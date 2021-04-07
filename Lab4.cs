#define stack
#define deque

using System;
using System.Diagnostics;
using System.Collections;

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
            MyDeque<int> deque = new MyDeque<int>();
            deque.Enqueue(0);
            deque.Enqueue(1);
            deque.Enqueue(2);
            deque.Dequeue(-1);
            var first = deque.RemoveFirst();
            var last = deque.RemoveLast();
            var head = deque.PopHead();
            var tail = deque.PopTail();

            #endregion
#endif
        }
    }
}
