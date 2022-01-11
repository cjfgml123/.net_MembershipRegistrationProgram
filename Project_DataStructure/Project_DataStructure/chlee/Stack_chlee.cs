using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* 완성이력:2021-03-03
 * 
 */
namespace Project_DataStructure.chlee
{
    public class Stack_chlee<E>
    {
        /// <summary>
        /// 스택을 위한 배열
        /// </summary>
        private E[] array;
        private int top;

        public Stack_chlee()
        {
            array = new E[1]; // 초기 크기1인 배열 생성
            this.top = -1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int count() { return (top + 1); } //스택 항목 수 리턴
        public bool isEmpty() //스택이 빈것인지 아닌지
        {
            return (top == -1);
        }

        public E peek() //스택 top 항목의 내용만 리턴
        {
            if (isEmpty())
            {
                Console.WriteLine("top item is empty");
                return default(E);
            }
            else
            {
                return array[top];
            }
        }
        public void push(E item) //stack 항목 넣기
        {
            array[++top] = item;  
            Array.Resize(ref array, array.Length + 1); 
            Console.WriteLine("stack push OK.");
        }

        public E Pop()          //stack 항목 빼기
        {
            E Popitem;
            if (isEmpty())
            {
                Console.WriteLine("Pop :Array is Empty.");
                return default(E);
            }
            else
            {
                Popitem = array[top];
                array[top--] = default(E);
                Array.Resize(ref array, array.Length - 1);
                Console.WriteLine("stack pop is ok.");
                return Popitem;
            }
        }
        public void print()     //출력 
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                Console.Write($"({array[i]}) ");
            }
            Console.WriteLine();
        }
    }
    
}
