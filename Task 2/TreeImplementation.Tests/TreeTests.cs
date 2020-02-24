using System;
using Xunit;
using System.Collections;
using System.Collections.Generic;

namespace TreeImplementation.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(40)]
        
        public void Tree_CheckIfTreeContainsAddedData_ReturnsTrue(int data)
        {
            Tree<int> tree= new Tree<int>();
            bool check;

            tree.Add(data);
            check=tree.Contains(data);

            Assert.True(check);

        }

        [Fact]

        public void Tree_DeleteNonexistentNode_ShouldThrowArgumentException()
        {
            Tree<int> tree= new Tree<int>(){1,2,3};
            
            Exception exception= Assert.Throws<ArgumentException>(()=>tree.Delete(5));

            Assert.Equal("The requested node is not in the tree.", exception.Message);
        }

        [Fact]

        public void Tree_DeleteNodeFromEmptyTree_ShouldThrowArgumentException()
        {
            Tree<int> tree= new Tree<int>();
            
            Exception exception= Assert.Throws<NullReferenceException>(()=>tree.Delete(5));

            Assert.Equal("The root has not been initialized.", exception.Message);
        
        }

        [Fact]
        public void Tree_PreOrderTraversal_ReturnsRespectiveEnumeratorOfElements()
        {
            Tree<int> tree= new Tree<int>() {5, 9, -15, 8, 10, 25, 18, -30, -10, -11};

            List<int> result= new List<int>();
            foreach(var data in tree.PreOrder())
            {
                result.Add((int)data);
            }

            Assert.Equal(new List<int>(){5, -15, -30, -10, -11, 9, 8, 10, 25, 18}, result);
        }

        [Fact]
        public void Tree_InOrderTraversal_ReturnsRespectiveEnumeratorOfElements()
        {
            Tree<int> tree= new Tree<int>() {5, 9, -15, 8, 10, 25, 18, -30, -10, -11};

            List<int> result= new List<int>();
            foreach(var data in tree.InOrder())
            {
                result.Add((int)data);
            }

            Assert.Equal(new List<int>(){-30, -15, -11, -10, 5, 8, 9, 10, 18, 25}, result);
        }

        [Fact]
        public void Tree_PostOrderTraversal_ReturnsRespectiveEnumeratorOfElements()
        {
            Tree<int> tree= new Tree<int>() {5, 9, -15, 8, 10, 25, 18, -30, -10, -11};

            List<int> result= new List<int>();
            foreach(var data in tree.PostOrder())
            {
                result.Add((int)data);
            }

            Assert.Equal(new List<int>(){-30, -11, -10, -15, 8, 18, 25, 10, 9, 5 }, result);
        }

        [Fact]
        public void Tree_DeleteNode_ReturnsCorrectCollection()
        {
       
            Tree<int> tree= new Tree<int>() {5, 9, -15, 8, 10, 25, 18, -30, -10, -11};

            tree.Delete(-15);
            List<int> result= new List<int>();
            foreach(var data in tree.InOrder())
            {
                result.Add((int)data);
            }

            Assert.Equal(new List<int>(){-30, -11,  -10, 5, 8, 9, 10, 18, 25}, result);
        
        }

        [Fact]
        public void Tree_GetForeachEnumerator_ReturnsPreOrderCollection()
        {
            
            Tree<int> tree= new Tree<int>() {5, 9, -15, 8, 10, 25, 18, -30, -10, -11};

            List<int> result= new List<int>();
            foreach(var data in tree)
            {
                result.Add((int)data);
            }

            Assert.Equal(new List<int>(){5, -15, -30, -10, -11, 9, 8, 10, 25, 18}, result);
        
        }

        [Fact]
        public void Tree_SubscribeToTreeChangeEvent_ReturnsRootAddedMessage()
        {
            Tree<int> tree= new Tree<int>();
            string message="";
            
            tree.Alert+=(object o, TreeChangeEventArgs e)=>message=e.Info;
            tree.Add(1);

            Assert.Equal("Added the root.", message);
        }

        [Fact]
        public void Tree_SubscribeToTreeChangeEvent_ReturnsNodeAddedMessage()
        {
            Tree<int> tree= new Tree<int>(1);
            string message="";
            
            tree.Alert+=(object o, TreeChangeEventArgs e)=>message=e.Info;
            tree.Add(12);

            Assert.Equal("Added a node.", message);
        }

        [Fact]
        public void Tree_SubscribeToTreeChangeEvent_ReturnsNodeDeletedMessage()
        {
            Tree<int> tree= new Tree<int>(){2,1,3};
            string message="";
            
            tree.Alert+=(object o, TreeChangeEventArgs e)=>message=e.Info;
            tree.Delete(1);

            Assert.Equal("Deleted the node.", message);
        }
    }

}
