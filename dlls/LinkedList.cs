

namespace LinkedList {
    class Node<T> {
        Node<T> next;
        Node<T> prev;
        T data;
    }

    public class LinkedList<T> {    
        private Node<T> head;
        private Node<T> tail;
    }
}