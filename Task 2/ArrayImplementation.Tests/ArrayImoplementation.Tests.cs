using System;
using Xunit;
using System.Collections;
using System.Collections.Generic;

namespace ArrayImplementation.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void IntervalArray_InitializeWithUnaccaptableData_ShouldThrowArgumentException()
        {
            Exception ex= Assert.Throws<ArgumentException>(()=>new IntervalArray<int>(20,10));
            Assert.Equal("Last index cannot be lower than the starting one.",ex.Message);
        }

        [Fact]
        public void  IntervalArray_ClearTheArray_ShouldThrowNullReferenceException()
        {
            IntervalArray<int> array= new IntervalArray<int>(1,5){1,2,3,4};
            
            array.Clear();
            Exception ex=Assert.Throws<NullReferenceException>(()=>array[array.FirstIndex]);

            Assert.Equal("Object reference not set to an instance of an object.", ex.Message);
        }

        [Fact]
        public void IntervalArray_AddElements_ReturnsCorrectSize()
        {
            IntervalArray<int> arr= new IntervalArray<int>(-50,3){1,5,6,7};

            int size=arr.Count;

            Assert.Equal(58,size);
        }

        [Fact]
        public void IntervalArray_AddElement_CheckIfSaidElementIsTheLastOne()
        {
            IntervalArray<int> arr= new IntervalArray<int>(-50,3){1};

            int value=arr[arr.LastIndex];

            Assert.Equal(1,value);
        }

        [Fact]
        public void IntervalArray_RemoveElement_ReturnsTrue()
        {
            IntervalArray<int> arr= new IntervalArray<int>(-2,10){1};

            bool flag=arr.Remove(1);

            Assert.True(flag);
        
        }
        [Fact]
        public void IntervalArray_TryToRemoveNonexistentValue_ReturnsFalse()
        {
            IntervalArray<int> arr= new IntervalArray<int>(-2,10){2};

            bool flag=arr.Remove(1);

            Assert.False(flag);
            
        }

       [Fact]
        public void IntervalArray_CheckIfContainsValue_ReturnsTrue()
        {
            IntervalArray<int> arr= new IntervalArray<int>(-2,10){-40};

            bool flag=arr.Contains(-40);

            Assert.True(flag);
            
        }
        [Fact]
        public void IntervalArray_CheckIfContainsValue_ReturnsFalse()
        {
            IntervalArray<int> arr= new IntervalArray<int>(-2,10);

            bool flag=arr.Contains(-40);

            Assert.False(flag);
            
        }

        [Theory]
        [InlineData(10)]
        [InlineData(-10)]
        public void IntervalArray_CheckFirstIndex(int first)
        {
            IntervalArray<int> arr= new IntervalArray<int>(first,30);

            int start=arr.FirstIndex;

            Assert.Equal(first,start);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(-10)]
        public void IntervalArray_CheckLastIndex(int last)
        {
            IntervalArray<int> arr= new IntervalArray<int>(-30,last);

            int end=arr.LastIndex;

            Assert.Equal(last,end);
        }
        [Fact]
        public void IntervalArray_GetEnumeratorCheck()
        {
            IntervalArray<int> arr= new IntervalArray<int>(){1,2,3,4,5};

            List<int> result=new List<int>();
            foreach (var value in arr)
                result.Add(value);

            Assert.Equal(new List<int>{1,2,3,4,5}, result);
        }
        [Theory]
        [InlineData(-30,10)]
        [InlineData(1,6)]
        public void IntervalArray_CheckIndexator_ReturnsCorrectValue(int index, int value)
        {
            IntervalArray<int> arr= new IntervalArray<int>(-40,20);
            int result;

            arr[index]=value;
            result=arr[index];

            Assert.Equal(value,result);
        }

    }
}
