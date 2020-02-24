using System;
using System.Collections;
using System.Collections.Generic;
namespace ArrayImplementation
{

    public class ArrayEnumerator<T>:IEnumerator<T>
    {
        public ArrayNode<T> _start;
        public ArrayNode<T> _current;

        public ArrayEnumerator(ArrayNode<T> start)
        {
            _start=start;
            _current=new ArrayNode<T>();
            _current.Next=_start;
        }
        public bool MoveNext()
        {
            _current=_current.Next;
            return(_current!=null);
        }

        public void Reset()
        {
            _current=new ArrayNode<T>();
            _current.Next=_start;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public T Current
        {
            get
            {
                try
                {
                    return _current.Value;
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }

        }

        void IDisposable.Dispose() {}

    }
    public class IntervalArray<T>:IEnumerable, ICollection<T> where T:IComparable
    {
        private ArrayNode<T> _start;
        public IntervalArray(int start, int finish)
        {
            if (start>finish)
            {
                throw new ArgumentException("Last index cannot be lower than the starting one.");
            }
            FirstIndex=start;
            LastIndex=finish;
            _start=new ArrayNode<T>();
            ArrayNode<T> tmp=_start;
            for (int i=0; i<finish-start; i++)
            {

                tmp.Next=new ArrayNode<T>();
                tmp=tmp.Next;
            }
        }
        public IntervalArray()
        {
            _start=null;
            FirstIndex=0;
            LastIndex=-1;
        }
        public void Clear()
        {

            _start=null;
        }
        public bool Contains(T data)
        {
            ArrayNode<T> temp= _start;
            if (_start.Value.CompareTo(data)==0)
                return true;
            while (temp!=null)
            {
                if (temp.Value.CompareTo(data)==0)
                    return true;
                temp=temp.Next;
            }
            return false;
        }

        public void CopyTo (T[] array, int start)
        {
            int counter=0;
            foreach(T value in this)
            {
                array[counter+start]=value;
                counter++;
            }
        }

        public bool Remove (T value)
        {
            if(_start.Value.CompareTo(value)==0)
            {
                _start=_start.Next;
                LastIndex--;
                return true ;
            }
            ArrayNode<T> temp= _start;
            while(temp.Next!=null)
            {
                if(temp.Next.Value.CompareTo(value)==0)
                {
                    temp.Next=temp.Next.Next;
                    LastIndex--;
                    return true ;
                }
                temp=temp.Next;

            }
            return false; 
        }
        public int Count
        {
            get=> LastIndex-FirstIndex+1;                      
        }

        public bool IsReadOnly
        {
            get=>false;
        }
        public void Add(T value)
        {
            if (_start==null)
            {
                _start=new ArrayNode<T>(value);
                return;
            }
            ArrayNode<T> temp=_start;
            while(temp.Next!=null)
            {
                temp=temp.Next;
            }
            temp.Next=new ArrayNode<T>(value);
            LastIndex++;
        }
        public int FirstIndex
        {
            get;
            private set;
        }

        public int LastIndex
        {
            get;
            private set;
        }
        public T this[int index]
        {
            get
            {
                if (index<FirstIndex || index> LastIndex)
                    throw new IndexOutOfRangeException("The provided index is outside the array's index interval.");
                ArrayNode<T> tmp=_start;
                for (int i=0; i<index-FirstIndex; i++)
                {
                    tmp=tmp.Next;
                }
                return tmp.Value;
            }
            set
            {
                if (index<FirstIndex || index> LastIndex)
                    throw new IndexOutOfRangeException("The provided index is outside the array's index interval.");

                ArrayNode<T> tmp=_start;
                for (int i=0; i<index-FirstIndex; i++)
                {
                    tmp=tmp.Next;
                }
                tmp.Value= value;
            }
        }
        
        IEnumerator  IEnumerable.GetEnumerator()
        {
            if(_start==null)
                throw new NullReferenceException("The array has not been initialized.");
            
            return new ArrayEnumerator<T>(_start);

        }
        public IEnumerator<T> GetEnumerator()
        {
            if(_start==null)
                throw new NullReferenceException("The array has not been initialized.");
            return new ArrayEnumerator<T>(_start);
        }
        

    }

    public class ArrayNode<T>
        {   
            public T Value {get; set;}
            public ArrayNode<T> Next {get; set;}

            public ArrayNode(T value)
            {
                Value=value;
            }
            public ArrayNode()
            {
                Next=null;

            }

        }
}

