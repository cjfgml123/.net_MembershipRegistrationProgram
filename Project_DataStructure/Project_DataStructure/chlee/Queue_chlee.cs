using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DataStructure.chlee
{
    class Queue_chlee<E>
    {

        private E[] array;          // 큐를 위한 배열
        private int front;          // 첫번째 item을 가리키는 변수 (실제 front item의 전 인덱스를 지칭)
        private int rear;           // 마지막 item
        private int count;

        public Queue_chlee()
        {
            array = new E[1];
            front = rear = count = 0;
        }
        public int Count() { return count; }    //큐 항목 수 리턴 
        public bool isEmpty() { return (count == 0); }

        public void add(E item)
        {
            //if ((rear + 1) == front) //큐가 full인 경우(비어있는 원소가 1개일때)
            Array.Resize(ref array, array.Length + 1);
            //rear = (rear + 1);       //새로운 item을 넣기위한 계산
            array[rear++] = item;
            count++;
            Console.WriteLine("Queue(Add) : Ok");

        }

        public E remove()
        {

            if (isEmpty())
            {
                Console.WriteLine("Queue(remove) : Array is Empty.");
                return default(E);
            }
            else
            {

                //front = (front + 1);     //꺼낼 item index 지정 기능
                E Removeitem = array[front];
                array[front] = default(E);
                //Array.Clear(array, front,1);
                count--;
                front++;
                Console.WriteLine("Queue(remove) : Ok");
                return Removeitem;
            }
        }

        public void print()
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine();
        }
    }
    
}
