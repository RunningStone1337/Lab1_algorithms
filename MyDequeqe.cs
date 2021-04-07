using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4
{
    public class MyDequeqe<T>
    {

        public int Count { get ; private set ; }
        Node<T> Head { get; set; }

        Node<T> Last { get; set; }
        public MyDequeqe() : base()
        {
            Last = null;
        }
        /// <summary>
        ///  Извлекает и возвращает первый элемент очереди
        /// </summary>
        /// <returns></returns>
        public T Dequeue()
        {
            
        }
        /// <summary>
        ///  Добавляет элемент в конец очереди
        /// </summary>
        /// <returns></returns>
        public void Enqueue(T val)
        {
            Node<T> newbie = new Node<T>(val);
            if (Last!=null)
            {
                Last.Next = newbie;
            }
            Last = newbie;
            Count++;
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
