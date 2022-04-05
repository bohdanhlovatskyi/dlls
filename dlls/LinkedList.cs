using System;
using System.IO; 
using System.Collections.Generic;
using System.Collections;

namespace LinkedList {

    class Node<T> {
        public Node<T> next { get; set; }
        public Node<T> prev { get; set; }
        public T data { get; set; }

        public Node(T val) {
            next = null;
            prev = null;
            data = val;
        }
    }

    // TODO: ask whether this can be incapsulated in LLIterator as it was done in C++
    public class LinkedList<T> : IEnumerable<T> {    
        private Node<T> head;
        private Node<T> tail;

        private int itemCount = 0;

        public int Length {
            get {
                return itemCount;
            }
        }

        public LinkedList() {
            head = null;
            tail = null;
        }

        public IEnumerator<T> GetEnumerator() {
            var current = head;
            while (current != null) {
                yield return current.data;
                current = current.next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            // this will invoke the public generic
            // version, so there is no recursion
            return this.GetEnumerator();
        }

        public void PushFront(T val) {
            var node = new Node<T>(val);
            if (head == null) {
                head = node;
                tail = node;
            } else {
                head.prev = node;
                node.next = head;
                head = node;
            }
            itemCount++;
        }

        public override string ToString()
        {
            var stream = new StringWriter();
            foreach (var node in this) {
                stream.Write(node.ToString() + " ");
            }

            return stream.ToString();
        }
    }
}