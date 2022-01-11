using System;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Project_DataStructure.Data;
using System.Threading;
using System.Collections;
using System.Linq;
/* 완성이력 : 2021-03-30 : 클라이언트 구현
* 수정이력 :  03-31 : 서버 연결 메소드, 서버에 data주고 받는 메소드 구현
*             04-01 : 코드 모듈화
*             04-07 : header와 body 전송 기능 추가
*             04-12 : 동기화 구현, 코드 네이밍, 주석 처리
*/
namespace Project_DataStructure.chlee
{
    /// <summary>
    /// socket : 서버에 접속할 클라이언트 소켓
    /// receivedString : 서버에서 받을 데이터를 byte에서 string으로 형 변환 후 저장할 string 변수
    /// headerBuffer : 서버에서 받은 헤드 버퍼
    /// DataBuffer : 서버에서 받은 바디 버퍼
    /// header : 프로토콜을 지닌 헤더 클래스 객체
    /// sync_cnt : 서버에서 받은 마지막 동기화 데이터를 가리키는 변수 
    /// </summary>
    class Client_chlee
    {
        Socket socket;

        string receivedString;
        byte[] headerBuffer;
        byte[] DataBuffer;
        Header header;
        int sync_cnt;

        /// <summary>
        /// event ReceivedData_Insert_To_DB_Node_ListView :서버로 부터 받은 데이터를 DB,Node,ListView에 저장
        /// event ReceivedData_Update_To_DB_Node_ListView :서버로 부터 받은 데이터를 DB,Node,ListView에 업데이트
        /// event ReceivedData_Delete_To_DB_Node_ListView :서버로 부터 받은 데이터를 DB,Node,ListView에 삭제
        /// event ReceivedData_Sync_To_DB_Node_ListView   :서버로 부터 받은 데이터를 DB,Node,ListView에 동기화
        /// event Resend_Data_To_Server : 실패의 응답 패킷이 올때 데이터를 다시 전송한다.  
        /// event Synchronization_Data_Collection : 서버에 보낼 데이터를 수집한다.
        /// event Send_SyncData_To_Server : 서버에 동기화 데이터를 전송한다.
        /// </summary>
        public event FuntionType_EventHandler ReceivedData_Insert_To_DB_Node_ListView;
        public event FuntionType_EventHandler ReceivedData_Update_To_DB_Node_ListView;
        public event FuntionType_EventHandler ReceivedData_Delete_To_DB_Node_ListView;
        public event FuntionType_EventHandler ReceivedData_Sync_To_DB_Node_ListView;
        public event SyncData_Collection_EventHandler Synchronization_Data_Collection;
        public event Resend_EventHandler Resend_Data_To_Server;       
        public event Send_SyncData_EventHandler Send_SyncData_To_Server;


        public Client_chlee()
        {
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.receivedString = null;
            this.headerBuffer = new byte[10];
            this.DataBuffer = null;
            this.sync_cnt = 0;
        }

        /// <summary>
        /// 소켓 연결 상태 확인 메소드
        /// </summary>
        /// <returns>true: 연결, false: 연결 실패</returns>
        public bool Connected_Check()
        {
            try
            {
                if (this.socket != null)
                {
                    if (this.socket.Connected)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 폼에서 txt_ip,txt_port로 서버의 정보를 입력 후 접속 시도 메소드
        /// </summary>
        /// <param name="_ip">서버의 ip</param>
        /// <param name="_port">서버의 포트번호</param>
        public void Connect_To_Server(string _ip, int _port)
        {
            try
            {
                IPAddress _serverIP = IPAddress.Parse(_ip);
                IPEndPoint serverEndPoint = new IPEndPoint(_serverIP, _port);
                this.socket.Connect(serverEndPoint);

                if (this.socket.Connected)
                {
                    Console.WriteLine("서버에 연결이 성공했습니다.");
                    Thread _DataCommunication_thread = new Thread(new ThreadStart(Communication_Client_To_Server));
                    _DataCommunication_thread.IsBackground = true;
                    _DataCommunication_thread.Start();
                }
                else
                {
                    Console.WriteLine("서버의 연결이 실패 했습니다.");
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine($"{e}, 서버 연결 실패ㅡㅡㅡClient_chlee.Connect_To_Server()");
            }
            catch (Exception commonEx)
            {
                Console.WriteLine("[Error]:{0}", commonEx.Message);
            }
        }

        /// <summary>
        /// byte[]로 받은 Body데이터를 string으로 형변환 후 데이터 구분자인 ","으로 각 데이터 추출
        /// </summary>
        /// <param name="_receivedString">","로 구분자를 갖는 string 데이터</param>
        /// <returns>","를 구분자로 나눈 데이터</returns>
        public string[] Split_Received_StringData(string _receivedString)
        {
            string _data = _receivedString;
            if (_data != null)
            {
                string[] _words = _data.Split(',');

                if (_words.Length != 8)
                {
                    Console.WriteLine("CommonData의 각각 데이터의 개수가 부족합니다.");
                    return null;
                }
                else
                {
                    return _words;
                }
            }
            else
            {
                Console.WriteLine("데이터 분리 실패 ㅡㅡㅡ Client_chlee.StringDataSplit()");
                return null;
            }
        }

        /// <summary>
        /// 서버에 보낼 바디와 헤더를 하나의 패킷으로 합쳐서 전송 
        /// </summary>
        /// <param name="_header">헤더</param>
        /// <param name="_body">데이터 (구분자 : ",")</param>
        public void Send_To_Server(byte[] _header, byte[] _body)
        {
            byte[] _packet = new byte[_header.Length + _body.Length];
            Array.Copy(_header, 0, _packet, 0, _header.Length);
            Array.Copy(_body, 0, _packet, _header.Length, _body.Length);
            this.socket.Send(_packet, 0, _packet.Length, SocketFlags.None);

            string _receivedString = Encoding.UTF8.GetString(_body, 0, _body.Length);
            Console.WriteLine($"메시지 타입 :{_header[0]},기능:{_header[1]}, 보낸 데이터 : { _receivedString}|{_body.Length}");
        }

        /// <summary>
        /// _totalDataSize: 가변길이인 바디부분의 데이터 길이
        /// _leftDataSize : 전송이 안된 바디 데이터 길이
        /// _accumnlatedDataSize : 전송이 완료된 바디 데이터 길이
        /// _receivedDataSize : 받은 바디 데이터 길이
        /// 기능 : 서버로 부터 헤더를 먼저 받고 헤더를 분해 후 바디의 데이터를 받는다.
        /// </summary>
        public void Receive_To_Server()
        {
            int _totalDataSize = 0;
            int _leftDataSize = 0;
            int _accumnlatedDataSize = 0;
            int _receivedDataSize = 0;

            this.socket.Receive(this.headerBuffer, 0, 10, SocketFlags.None);
            
            this.header = new Header()
            {
                dataType = headerBuffer[0],
                functionType = headerBuffer[1],
                sync_cnt = BitConverter.ToInt32(this.headerBuffer,2),
                bodyLen = BitConverter.ToInt32(this.headerBuffer, 6)
            };

            _totalDataSize = this.header.bodyLen;
            _leftDataSize = _totalDataSize;

            this.DataBuffer = new byte[_totalDataSize];

            while (_leftDataSize > 0)
            {
                if(_leftDataSize <0)
                {
                    Console.WriteLine("Error Data Receiving");
                    break;
                }else if(_leftDataSize == 0)
                {
                    Console.WriteLine("Data success");
                    break;
                }
                _receivedDataSize = this.socket.Receive(this.DataBuffer,_accumnlatedDataSize,_leftDataSize,SocketFlags.None);
                _accumnlatedDataSize += _receivedDataSize;
                _leftDataSize -= _receivedDataSize;
            }
            this.receivedString = Encoding.UTF8.GetString(this.DataBuffer, 0, _totalDataSize);
        }

        /// <summary>
        /// 소켓과 현재 실행되는 스레드를 닫는 메소드
        /// </summary>
        public void Disconnect()
        {
            this.socket.Close();
            Thread.CurrentThread.Abort();
        }

        /// <summary>
        /// 서버로 부터 받은 패킷을 처리(DB,Node,ListView에 적용)한 후 그에 따른 응답을 보내는 메소드
        /// </summary>
        /// <param name="_eventSuccess">서버로부터 받은 패킷을 처리가 됬으면 true, 아니면 false</param>
        public void ReceivedData_Process_To_DB_Node_ListView(bool _eventSuccess)
        {
            try
            {
                if (_eventSuccess)
                {
                    headerBuffer[0] = (byte)DataType.RESP_DATA_SUCCESS;
                    ++this.sync_cnt;
                }
                else
                {
                    headerBuffer[0] = (byte)DataType.RESP_DATA_FAIL;
                }
                Send_To_Server(headerBuffer, DataBuffer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.ToString()}");
            }
        }

        /// <summary>
        /// 서버와 패킷 주고 받는 메소드
        /// 기능 : 삽입, 삭제, 수정, 동기화
        /// </summary>
        public void Communication_Client_To_Server()
        {           
            try
            {   
                while (this.socket.Connected)
                {
                    Receive_To_Server();                  
                    string[] _receivedData = Split_Received_StringData(this.receivedString);
              
                    if (this.header.dataType == (byte)DataType.SEND_DATA)
                    {                        
                        switch (this.header.functionType)
                        {   
                            case (byte)FunctionType.SYNC_SEND:
                                if(this.header.sync_cnt != 0) 
                                {
                                    Console.WriteLine($"호스트에 동기화된 데이터 : {this.receivedString} |길이: {DataBuffer.Length}");
                                    Synchronization_Data_Collection(_receivedData[2]); // 서버에 보낼 패킷 데이터 추출
                                    ReceivedData_Process_To_DB_Node_ListView(ReceivedData_Sync_To_DB_Node_ListView(_receivedData));
                                }
                                                        
                                if (this.sync_cnt == this.header.sync_cnt)
                                {
                                    //서버로부터 동기화 데이터를 다 받으면 클라이언트의 동기화 데이터를 전송한다.
                                    Console.WriteLine("서버에서 동기화 패킷을 전부 받았습니다. 서버에 동기화 데이터를 전송합니다.");
                                    ReceivedData_Process_To_DB_Node_ListView(true);
                                    Send_SyncData_To_Server();
                                    this.sync_cnt = 0;
                                }
                                break;
                            case (byte)FunctionType.SAVE_DATA:
                                Console.WriteLine($"호스트에 저장된 데이터 : {this.receivedString} |길이: {DataBuffer.Length}");
                                ReceivedData_Process_To_DB_Node_ListView(ReceivedData_Insert_To_DB_Node_ListView(_receivedData));
                                break;
                            case (byte)FunctionType.UPDATE_DATA:
                                Console.WriteLine($"호스트에 수정된 데이터 : {this.receivedString} |길이: {DataBuffer.Length}");
                                ReceivedData_Process_To_DB_Node_ListView(ReceivedData_Update_To_DB_Node_ListView(_receivedData));
                                break;
                            case (byte)FunctionType.DELETE_DATA:
                                Console.WriteLine($"호스트에 삭제된 데이터 : {this.receivedString} |길이: {DataBuffer.Length}");
                                ReceivedData_Process_To_DB_Node_ListView(ReceivedData_Delete_To_DB_Node_ListView(_receivedData));
                                break;
                        }    

                    }else if(this.header.dataType == (byte)DataType.RESP_DATA_SUCCESS)
                    {
                        switch (this.header.functionType)
                        {
                            case (byte)FunctionType.SYNC_SEND:
                                Console.WriteLine($"서버에 동기화된 데이터 : {this.receivedString} |길이: {DataBuffer.Length}");
                                break;
                            case (byte)FunctionType.SAVE_DATA:
                                Console.WriteLine($"서버에 저장된 데이터 : {this.receivedString} |길이: {DataBuffer.Length}");
                                break;
                            case (byte)FunctionType.UPDATE_DATA:
                                Console.WriteLine($"서버에 업데이트된 데이터 : {this.receivedString} |길이: {DataBuffer.Length}");
                                break;
                            case (byte)FunctionType.DELETE_DATA:
                                Console.WriteLine($"서버에 삭제된 데이터 : {this.receivedString} |길이: {DataBuffer.Length}");
                                break;
                        }
                    }
                    else if (this.header.dataType == (byte)DataType.RESP_DATA_FAIL)
                    {
                        switch (this.header.functionType)
                        {
                            case (byte)FunctionType.SYNC_SEND:
                                Resend_Data_To_Server(_receivedData[0], FunctionType.SYNC_SEND);
                                break;
                            case (byte)FunctionType.SAVE_DATA:
                                Resend_Data_To_Server(_receivedData[0], FunctionType.SAVE_DATA);
                                break;
                            case (byte)FunctionType.UPDATE_DATA:
                                Resend_Data_To_Server(_receivedData[0], FunctionType.UPDATE_DATA);
                                break;
                            case (byte)FunctionType.DELETE_DATA:
                                Resend_Data_To_Server(_receivedData[0], FunctionType.DELETE_DATA);
                                break;
                        }
                    }
                    Array.Clear(this.headerBuffer, 0, this.headerBuffer.Length);
                    Array.Clear(this.DataBuffer, 0, this.DataBuffer.Length);
                }          
            }
            catch (SocketException socketEx)
            {
                Console.WriteLine("[Error]:{0}소켓에러ㅡClient_chlee.Communication_Client_To_Server()", socketEx.ToString());
                Disconnect();
            }
            catch (Exception commonEx)
            {
                Console.WriteLine("[Error]:{0}일반에러ㅡClient_chlee.Communication_Client_To_Server()", commonEx.ToString());
                Disconnect();
            }
        }
    }
}
