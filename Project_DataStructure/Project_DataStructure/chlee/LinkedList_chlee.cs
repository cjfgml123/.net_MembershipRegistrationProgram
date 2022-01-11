using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_DataStructure.Data;
/* 완성이력 : 2021-03-10
 * 수정이력 : 03-11 : 표기법 수정(수정, 출력 부분 제외)
 *            03-14 : 삭제기능 수정(last삭제시 last 다음으로 last 변경)  
 *            04-05 : id 중복검사 메소드 추가 ID_Duplicate_inspection()  
 *            04-08 : 업데이트 기능 구현      
 * 기능     : 삽입, 삭제, 탐색, 수정 , 중복검사 
 */
namespace Project_DataStructure.chlee
{
    /// <summary>
    /// 노드 클래스
    /// </summary>
    /// <param name="NextNode">다음 노드를 가리키는 주소</param>
    /// <param name="PreviousNode">전 노드를 가리키는 주소</param>
    /// <param name="item">노드안에 저장된 item</param>
    public class Node
    {
        public Node nextNode { get; set; }
        public Node previousNode { get; set; }
        public object item { get; set; }

        /// <summary>
        /// 노드 생성자
        /// </summary>
        /// <param name="newitem">노드에 넣을 새로운 아이템</param>
        public Node(object _item)
        {
            item = _item;
            nextNode = null;
            previousNode = null;
        }
    }

    /// <summary>
    /// 이중 환형 연결리스트 구현
    /// 
    /// </summary>
    /// <typeparam name="last">마지막 노드를 지정하는 변수</typeparam>
    /// <typeparam name="size">리스트 항목 수</typeparam>
    class LinkedList_chlee<E>
    {
        public Node last;
        public int size;

        /// <summary>
        /// 생성자
        /// </summary>
        public LinkedList_chlee()
        {

            this.last = null;
            this.size = 0;
        }
        /// <summary>
        /// 리스트 항목수 메소드
        /// </summary>
        /// <returns>리스트 항목 수</returns>
        public int Count() { return size; }

        /// <summary>
        /// 노드 삽입 메소드
        /// 기능 : 1. 처음 노드 삽입시 노드가 last로 지정, last뒤로 
        /// </summary>
        /// <param name="_newItem">새로운 노드에 넣을 아이템</param>
        public void InsertNode(object _newItem)
        {
            Node _newNode = new Node(_newItem);
            if (last == null)                                       // last없을떄 자기 자신이 자신을 가리키도록
            {
                last = _newNode;
                last.nextNode = last;
                last.previousNode = last;
                size++;
            }
            else                                                    
            {
                _newNode.nextNode = last.nextNode;
                last.nextNode.previousNode = _newNode;
                last.nextNode = _newNode;
                _newNode.previousNode = last;
                size++;
            }
        }

        /// <summary>
        /// 원하는 노드 탐색 메소드
        /// </summary>
        /// <param name="_Id">탐색할 값을 갖은 노드 탐색 변수</param>
        /// <returns>탐색된 노드</returns>
        public Node FindNode(string _Id)
        {

            Node _findNode = last;
            if (_findNode != null)
            {
                CommonData find_data = (CommonData)_findNode.item;

                for (int i = 0; i < size; i++)
                {
                    if (find_data.ID == _Id)
                    {
                        return _findNode;
                    }
                    _findNode = _findNode.nextNode;
                    find_data = (CommonData)_findNode.item;

                }
                Console.WriteLine("item is empty in list. 중복검사 완료");
                _findNode = null;
                return _findNode;
            }
            else
            {
                Console.WriteLine("lastNode is null.");
                return _findNode;
            }

        }

        /// <summary>
        /// 업데이트할 id로 노드를 탐색 후 새로운 정보를 받아 업데이트 메소드
        /// </summary>
        /// <param name="_data">업데이트할 CommonData</param>
        /// <returns>업데이트된 CommonData객체</returns>
        public CommonData UpdateNode(CommonData _data)
        {
            Node _UpdateNode = FindNode(_data.ID);
            if (_UpdateNode != null)
            {
                CommonData _UpdateItem = (CommonData)_UpdateNode.item;
                _UpdateItem.PW = _data.PW;               
                _UpdateItem.Age = _data.Age;
                _UpdateItem.ModifyDate = _data.ModifyDate;
                _UpdateItem.AgeChk = _data.AgeChk;
                _UpdateItem.Tall = _data.Tall;
                _UpdateItem.SexType = _data.SexType;
                Console.WriteLine("노드 아이템 업데이트 완료");
                return _UpdateItem;
            }
            else
            {
                Console.WriteLine("업데이트할 노드를 못찾았습니다. ㅡㅡLinkedList_chlee.UpdateNode()");
                return null;
            }
        }

    /// <summary>
    /// 원하는 노드 삭제 메소드
    /// </summary>
    /// <param name="_Id">삭제할 값을 갖는 노드 삭제 변수</param>
    /// <returns>삭제된 노드</returns>
    public Node DeleteNode(string _Id)
    {
            Node _target = FindNode(_Id);

            if (_target != null)
            {
                if (size == 0)
                {
                    Console.WriteLine("list is empty");
                    return null;
                }
                else if (size == 1)
                {
                    last = null;
                    size--;
                    return _target;
                }
                else
                {
                    if (_target == last)
                    {
                        last = _target.nextNode;
                        _target.previousNode.nextNode = last;
                        last.previousNode = _target.previousNode;
                        _target.nextNode = null;
                        _target.previousNode = null;
                        size--;
                        Console.WriteLine("last Node changed");
                        return _target;
                    }
                    else
                    {
                        _target.previousNode.nextNode = _target.nextNode;
                        _target.nextNode.previousNode = _target.previousNode;
                        _target.nextNode = null;
                        _target.previousNode = null;
                        size--;
                        return _target;
                    }
                }
            }
            else
            {
                return null;
            }

        }

        public void DeleteAllNode()
        {
            Node x = last;   
                for (int i = 0; i < size; i++)
                {
                x = null;
                x = last.nextNode;
                }
            size = 0;
                Console.WriteLine("리스트 항목 모두 삭제");      
        }

        /// <summary>
        /// 리스트 값 출력
        /// </summary>
        public void PrintData()
        {
            Node _printNode = last;
            if(_printNode != null)
            {
                CommonData find_data = (CommonData)_printNode.item;

                for (int i = 0; i < size + 1; i++)
                {
                    Console.Write($"{ find_data.ID} ->");
                    _printNode = _printNode.nextNode;
                    find_data = (CommonData)_printNode.item;
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("list is empty. PrintData()");
            }
          
        }

        /// <summary>
        /// id 중복검사 메소드
        /// </summary>
        /// <param name="_data">Common Data 형식</param>
        /// <returns>false : 중복아이디가 있다. true : 중복 아이디가 없다.</returns>
        public bool ID_Duplicate_inspection(string _id)
        {
            if (FindNode(_id) != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}