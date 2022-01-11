using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Project_DataStructure.Data;

namespace Project_DataStructure.chlee
{
    public partial class chlee : UserControl
    {
        Stack_chlee<CommonData> MyStack;        // stack 
        Queue_chlee<CommonData> MyQueue;        // Queue
        LinkedList_chlee<CommonData> MyLinkedList; // LinkedList


        /// <summary>
        /// 
        /// </summary>
        public chlee()
        {
            InitializeComponent();
            MyStack = new Stack_chlee<CommonData>();
            MyQueue = new Queue_chlee<CommonData>();
            MyLinkedList = new LinkedList_chlee<CommonData>();
        }
        public void DataClear() //input data 입력창 초기화
        {
            tbxName.Clear();
            tbxAge.Clear();
            tbxPhoneNumber.Clear();
            tbxEmail.Clear();
            //tbxCurrentCount.Clear();
        }

        /// <summary>
        /// // 스택 추가 버튼 이벤트 함수
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddScack_Click(object sender, EventArgs e) //스택 넣기
        {
            try
            {
                CommonData stackData = new CommonData()
                {
                    //name = this.tbxName.Text,
                    //age = int.Parse(this.tbxAge.Text),              //Text는 string으로 받음.
                    //phoneNumber = this.tbxPhoneNumber.Text,
                    //emailAddress = this.tbxEmail.Text
                };
                MyStack.push(stackData);
                tbxCurrentCount.Text = MyStack.count().ToString();  //count() : int type
                DataClear();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("입력 값이 없거나 2번째 창에 숫자 입력 확인 " + ex.Message);
                //Console.WriteLine("입력값이 없어요.");          
            }

        }
        /// <summary>
        /// // 스택 삭제 버튼 이벤트 함수
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveScack_Click(object sender, EventArgs e)
        {
            CommonData popItem = MyStack.Pop();
            if (popItem != null)
            {

                tbxCurrentCount.Text = MyStack.count().ToString();
                //this.tbxName.Text = popItem.name;
                //this.tbxAge.Text = (popItem.age).ToString();
                //this.tbxPhoneNumber.Text = popItem.phoneNumber;
                //this.tbxEmail.Text = popItem.emailAddress;
            }
            else
            {
                MessageBox.Show("Stack에서 꺼내올 값이 없어요.");
            }
        }
        /// <summary>
        ///  // 큐 추가 버튼 이벤트 함수
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddQueue_Click(object sender, EventArgs e)
        {
            try
            {
                CommonData queueData = new CommonData()
                {
                    //name = this.tbxName.Text,
                    //age = int.Parse(this.tbxAge.Text),              //Text는 string으로 받음.
                    //phoneNumber = this.tbxPhoneNumber.Text,
                    //emailAddress = this.tbxEmail.Text
                };
                MyQueue.add(queueData);
                DataClear();
                tbxCurrentCount.Text = MyQueue.Count().ToString();
            }
            catch (FormatException)
            {

                MessageBox.Show("입력 값이 없거나 2번째 창에 숫자 입력 확인");
                //Console.WriteLine("입력값이 없어요.");          
            }
        }
        /// <summary>
        ///  // 큐 삭제 버튼 이벤트 함수
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveQueue_Click(object sender, EventArgs e)
        {
            CommonData removeItem = MyQueue.remove();
            if (removeItem != null)
            {
                tbxCurrentCount.Text = MyQueue.Count().ToString();
                //this.tbxName.Text = removeItem.name;
                //this.tbxAge.Text = (removeItem.age).ToString();
                //this.tbxPhoneNumber.Text = removeItem.phoneNumber;
                //this.tbxEmail.Text = removeItem.emailAddress;
            }
            else
            {
                MessageBox.Show("Queue에서 꺼내올 값이 없어요.");
            }
        }
        /// <summary>
        /// // 리스트 추가 버튼 이벤트 함수
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddList_Click(object sender, EventArgs e) //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ콘솔창에 구현 필요
        {
            CommonData listData = new CommonData()
            {
                //name = this.tbxName.Text,
                //age = int.Parse(this.tbxAge.Text),              //Text는 string으로 받음.
                //phoneNumber = this.tbxPhoneNumber.Text,
                //emailAddress = this.tbxEmail.Text
            };

            //MyLinkedList.insertFront(listData);
            DataClear();
            tbxCurrentCount.Text = MyLinkedList.Count().ToString();
        }
        /// <summary>
        /// // 리스트삭제 버튼 이벤트 함수
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveList_Click(object sender, EventArgs e)
        {
            //CommonData listitem = (CommonData)MyLinkedList.head.getItem();
            //Console.WriteLine(listitem.name);
            //Console.WriteLine(listitem.age);
            //Console.WriteLine(listitem.phoneNumber);
            //Console.WriteLine(listitem.emailAddress);
            //MyLinkedList.deleteFornt();
            tbxCurrentCount.Text = MyLinkedList.Count().ToString();
        }

        private void tbxName_TextChanged(object sender, EventArgs e)
        {


        }

        private void tbxAge_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxPhoneNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxCurrentCount_TextChanged(object sender, EventArgs e)
        {


        }
    }
}
