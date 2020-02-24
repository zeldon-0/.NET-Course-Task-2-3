using System;
using System.Collections;
namespace TreeImplementation
{
    public class TreeChangeEventArgs: EventArgs
    {
        public string Info {get; set;}

    }
    public class Tree<T>:IEnumerable where T:IComparable
    {
        private Node<T> _root;

        public delegate void TreeChangeDelegate (object sender, TreeChangeEventArgs e);
        private event TreeChangeDelegate _alert; 
        public event TreeChangeDelegate Alert
        {
            add
            {
                _alert+=value;
            }
            remove
            {
                _alert-=value;
            }
        }

        protected virtual void OnTreeChanged (TreeChangeEventArgs e)
        {
            if (_alert!=null)
                _alert(this,e);
        }
        public Tree()
        {
            _root=null;
        }
        public Tree(T data)
        {
            _root=new Node<T>(data);
        }

        public void Add(T data)
        {
            if (data==null)
                throw new ArgumentException("Cannot add uninitialized data.");
            if (_root==null)
            {
                _root=new Node<T>(data);
                OnTreeChanged(new TreeChangeEventArgs()
                {Info="Added the root."});
                return;
            }
            Node<T> cur= _root;
            Node<T> prev=cur;
            while (cur!= null)
            {
                prev=cur;
                if (cur.Value.CompareTo(data)>0)
                    {
                        cur=cur.Left;
                    }

                else
                { 
                    cur=cur.Right;
                }
            }
            if (prev.Value.CompareTo(data)>0)
                    prev.Left=new Node<T>(data);
            else prev.Right=new Node<T>(data);
            OnTreeChanged(new TreeChangeEventArgs()
                {Info="Added a node."});
            return;
        }
        
        public void Delete(T data)
        {
            if (_root==null)
                throw new NullReferenceException("The root has not been initialized.");
            if (data.CompareTo(_root.Value)==0)
                _root=ReplaceNode(_root);
                
            else
            {


                Node<T> predecessor=ParentSearch(data);
                if (predecessor==null)
                    throw new ArgumentException("The requested node is not in the tree.");


                if (predecessor.Left.Value.CompareTo(data)==0)
                    {
                        predecessor.Left=ReplaceNode(predecessor.Left);
                    }
                else
                    {
                        predecessor.Right=ReplaceNode(predecessor.Right);
                    }
            }
            OnTreeChanged(new TreeChangeEventArgs()
                {Info="Deleted the node."});
        }
        private Node<T> ReplaceNode( Node<T> node)
        {
        
            if (node.Right==null)
            {
                if(node.Left==null)
                    node=null;
                else
                {
                    if (node.Left.Right==null)
                    {
                        node=node.Left;
                        return node;
                    }
                    Node<T> leftCopy=node.Left;
                    node=GetLeftSubstitute(node);
                    node.Left=leftCopy;
                }
            }
            else
            {
                Node<T> leftCopy=node.Left;
                if (node.Right.Left==null)
                {
                    node=node.Right;
                    node.Left=leftCopy;
                    return node;
                }
                Node<T> rightCopy=node.Right;
                node=GetRightSubstitute(node);
                node.Right=rightCopy;
                node.Left=leftCopy;
            }
            return node;

        }

        private Node<T> GetLeftSubstitute(Node<T> node)
        {
            Node<T> prev=node;
            node=node.Left;
            while (node.Right!=null)
            {
                prev=node;
                node=node.Right;
            }
            if (node.Left!=null)
                prev.Right=node.Left;
            else
                prev.Right=null;
            return node;
        }

        private Node<T> GetRightSubstitute(Node<T> node)
        {
            Node<T> prev=node;
            node=node.Right;
            while (node.Left!=null)
            {
                prev=node;
                node=node.Left;
            }
            if (node.Right!=null)
                prev.Left=node.Right;
            else
                prev.Left=null;
            return node;
        }
        public bool Contains(T data)
        {
            if (_root==null)
                throw new NullReferenceException("The root has not been initialized.");
            if (ParentSearch(data)!=null)
                return true;
            return false;
        }
        private Node<T> ParentSearch(T data)
        {
            Node<T> prev=_root;
            Node<T> curr=_root;

            while (curr!=null && curr.Value.CompareTo(data)!=0)
            {
                prev=curr;
                if (curr.Value.CompareTo(data)>0)
                    curr=curr.Left;
                else
                    curr=curr.Right;
            }
            if (curr==null)
                return null;
            
            return prev;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            if (_root==null)
                throw new NullReferenceException("The root has not been initialized.");
            foreach(var node in PreOrderTraversal(_root))
                yield return node;
        }


        public IEnumerable PreOrder()
        {
            if (_root==null)
                throw new NullReferenceException("The root has not been initialized.");
            return  PreOrderTraversal (_root);
        }

        public IEnumerable PostOrder()
        {
            if (_root==null)
                throw new NullReferenceException("The root has not been initialized.");
            return  PostOrdertraversal (_root);
        }
        
        public IEnumerable InOrder()
        {
            if (_root==null)
                throw new NullReferenceException("The root has not been initialized.");
            return  InOrderTraversal (_root);
        }

        private IEnumerable PreOrderTraversal (Node<T> node)
        {
            if (node.Value!=null) yield return node.Value;
            if (node.Left!=null)
            {
                foreach(T leftValue in PreOrderTraversal(node.Left))
                    yield return leftValue;
            }
            if (node.Right!=null)
            {
                foreach(T rightValue in PreOrderTraversal(node.Right))
                    yield return rightValue;
            }
        }
        private IEnumerable PostOrdertraversal (Node<T> node)
        {
            if (node.Left!=null)
            {
                foreach(T leftValue in PostOrdertraversal(node.Left))
                    yield return leftValue;
            }
            if (node.Right!=null)
            {
                foreach(T rightValue in PostOrdertraversal(node.Right))
                    yield return rightValue;
            }
            yield return node.Value;
        }

        private IEnumerable InOrderTraversal (Node<T> node)
        {
            if (node.Left!=null)
            {
                foreach(T leftValue in InOrderTraversal(node.Left))
                    yield return leftValue;
            }
            yield return node.Value;
            if (node.Right!=null)
            {
                foreach(T rightValue in InOrderTraversal(node.Right))
                    yield return rightValue;
            }
        }
    }

    public class Node<T> where T:IComparable
    {
        private T _value;

        public Node(T data)
        {
            if(data==null)
                throw new NullReferenceException("Data not initialized.");
            _value=data;
        }

        public Node<T> Left
        {
            get;
            set;
        }

        public Node<T> Right
        {
            get;
            set;
        }

        public T Value
        {
            get=> _value;
        }
    }
}
