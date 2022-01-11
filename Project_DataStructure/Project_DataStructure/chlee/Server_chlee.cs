using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Project_DataStructure.Data;
/* 완성이력 : 2021-03-30 : 서버 구현
* 수정이력 :  03-31 : 서버 대기 상태 메소드, data주고 받는 메소드 구현
*             04-01 : Data receive ,send 스레드 구현, 함수 모듈화
*             04-07 : 프로토콜 적용하여 데이터 삽입 구현
*             04-08 : 프로토콜 적용하여 데이터 수정,동기화,삭제 구현
*             04-12 : 코드 네이밍, 모듈화, 주석 수정
*/
namespace Project_DataStructure.chlee
{

    /// <summary>
    /// 서버 구현 
    /// server, client : 소켓 생성
    /// receivedString : 클라이언트에서 받을 데이터를 byte에서 string으로 형 변환 후 저장할 string 변수
    /// ip , port : 서버의 ip, port
    /// client_number : 접속 가능한 클라이언트 수
    /// headerBuffer : 클라이언트에서 받은 헤드 버퍼
    /// DataBuffer : 클라이언트에서 받은 바디 버퍼
    /// header : 프로토콜을 지닌 헤더 클래스 객체 
    /// </summary>
    class Server_chlee
    {
        Socket server, client;
        string receivedString;
        string ip;
        int port;
        int client_number;
        byte[] headerBuffer;
        byte[] DataBuffer;
        Header header;

        /// event ReceivedData_Insert_To_DB_Node_ListView :클라이언트로 부터 받은 데이터를 DB,Node,ListView에 저장
        /// event ReceivedData_Update_To_DB_Node_ListView :클라이언트로 부터 받은 데이터를 DB,Node,ListView에 업데이트
        /// event ReceivedData_Delete_To_DB_Node_ListView :클라이언트로 부터 받은 데이터를 DB,Node,ListView에 삭제
        /// event Send_SyncData_To_Client : 클라이언트에 동기화 데이터를 전송한다.
        /// event Resend_Data_To_Client   : 실패의 응답 패킷이 올때 데이터를 다시 전송한다.
        public event FuntionType_EventHandler ReceivedData_Insert_To_DB_Node_ListView;
        public event FuntionType_EventHandler ReceivedData_Update_To_DB_Node_ListView;
        public event FuntionType_EventHandler ReceivedData_Delete_To_DB_Node_ListView;
        public event Send_SyncData_EventHandler Send_SyncData_To_Client;
        public event Resend_EventHandler Resend_Data_To_Client;

        public Server_chlee()
        {
            this.server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.receivedString = null;
            this.ip = "192.168.99.112";
            this.client_number = 5;
            this.port = 8000;
            this.headerBuffer = new byte[10];
        }

        /// <summary>
        /// 서버 구동 메소드
        /// </summary>
        public void RunServer()
        {
            try
            {
                IPAddress _serverIP = IPAddress.Parse(this.ip);
                IPEndPoint _serverEndPoint = new IPEndPoint(_serverIP, this.port);

                Console.WriteLine("현재 서버 정보");
                Console.WriteLine("IP Address : {0}, Port : {1}", _serverEndPoint.Address, _serverEndPoint.Port);

                this.server.Bind(_serverEndPoint);
                this.server.Listen(this.client_number);
                Console.WriteLine("클라이언트 접속 대기 중");

                AcceptServer();
            }
            catch (SocketException socketEx)
            {
                Console.WriteLine("[Error]:{0} ㅡㅡServer_chlee.RunServer()", socketEx.Message);
            }
            catch (Exception commonEx)
            {
                Console.WriteLine("[Error]:{0} ㅡㅡServer_chlee.RunServer()", commonEx.Message);
            }
        }

        /// <summary>
        /// 서버 대기 상태 메소드
        /// 클라이언트 접속시 클라이언트마다 개별 데이터 주고 받는 스레드 생성
        /// 기능 : 클라이언트가 연결되면 동기화 데이터를 보낸다.
        /// </summary>
        public void AcceptServer()
        {
            while (true)
            {
                this.client = this.server.Accept();
                if (this.client.Connected)
                {   
                    string _clientAddress = ((IPEndPoint)this.client.RemoteEndPoint).Address.ToString();
                    Send_SyncData_To_Client();

                    Console.WriteLine($"{_clientAddress}: 클라이언트 접속 됨");
                    Thread _DataCommunication_thread = new Thread(new ThreadStart(Communication_Server_To_Client));
                    _DataCommunication_thread.IsBackground = true;
                    _DataCommunication_thread.Start();
                }
            }
        }

        /// <summary>
        /// 클라이언트 접속 상태 확인 메소드
        /// </summary>
        /// <returns>true: 연결, false: 연결 실패</returns>
        public bool Connected_Check()
        {
            try
            {
                if (this.client != null)
                {
                    if ((this.client.Connected))
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
                Console.WriteLine($"{ ex.ToString()}");
                return false;
            }

        }

        /// <summary>
        /// 클라이언트로 부터 받은 데이터를 ","를 구분으로 데이터 분리 메소드
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
                    Console.WriteLine("데이터의 개수가 부족합니다.");
                    return null;
                }
                else
                {
                    return _words;
                }
            }
            else
            {
                Console.WriteLine("데이터 분리 실패 ㅡㅡㅡ Server_chlee.StringDataSplit()");
                return null;
            }
        }


        /// <summary>
        /// 클라이언트에 보낼 바디와 헤더를 하나의 패킷으로 합쳐서 전송 
        /// </summary>
        /// <param name="_header">헤더</param>
        /// <param name="_body">데이터 (구분자 : ",")</param>
        public void Send_To_Client(byte[] _header, byte[] _body)
        {
            byte[] _packet = new byte[_header.Length + _body.Length];
            Array.Copy(_header, 0, _packet, 0, _header.Length);
            Array.Copy(_body, 0, _packet, _header.Length, _body.Length);
            this.client.Send(_packet, 0, _packet.Length, SocketFlags.None);

            string _receivedString = Encoding.UTF8.GetString(_body, 0, _body.Length);
            Console.WriteLine($"보낸 메시지 타입 :{_header[0]},기능:{_header[1]}, 데이터 : { _receivedString}|{_body.Length}");
        }

        /// <summary>
        /// _totalDataSize: 가변길이인 바디부분의 데이터 길이
        /// _leftDataSize : 전송이 안된 바디 데이터 길이
        /// _accumnlatedDataSize : 전송이 완료된 바디 데이터 길이
        /// _receivedDataSize : 받은 바디 데이터 길이
        /// 기능 : 클라이언트로 부터 헤더를 먼저 받고 헤더를 분해 후 바디의 데이터를 받는다.
        /// </summary>
        public void Receive_To_Client()
        {
            int _totalDataSize = 0;
            int _leftDataSize = 0;
            int _accumnlatedDataSize = 0;
            int _receivedDataSize = 0;

            this.client.Receive(this.headerBuffer, 0, 10, SocketFlags.None);

            this.header = new Header()
            {
                dataType = (byte)headerBuffer[0],
                functionType = (byte)headerBuffer[1],
                sync_cnt = BitConverter.ToInt32(this.headerBuffer, 2),
                bodyLen = BitConverter.ToInt32(this.headerBuffer, 6)
            };
            _totalDataSize = header.bodyLen;
            _leftDataSize = _totalDataSize;

            this.DataBuffer = new byte[_totalDataSize];

            while (_leftDataSize>0)
            {
                if (_leftDataSize < 0)
                {
                    Console.WriteLine("Error Data Receiving");
                    break;
                }else if(_leftDataSize == 0)
                {
                    Console.WriteLine("Data success");
                    break;
                }     
                _receivedDataSize = this.client.Receive(this.DataBuffer, _accumnlatedDataSize, _leftDataSize, SocketFlags.None);
                _accumnlatedDataSize += _receivedDataSize;
                _leftDataSize -= _receivedDataSize;
            }
            this.receivedString = Encoding.UTF8.GetString(this.DataBuffer, 0, _totalDataSize);
            //Console.WriteLine($"받은 메시지 타입 :{this.header.dataType},기능:{this.header.functionType}, 데이터 : { receivedString}|{this.DataBuffer.Length}");
        }

        /// <summary>
        /// 소켓과 현재 실행되는 스레드를 닫는 메소드
        /// </summary>
        public void Disconnect()
        {
            this.client.Close();
            Thread.CurrentThread.Abort();
        }

        /// <summary>
        /// 클라이언트로 부터 받은 패킷을 처리(DB,Node,ListView에 적용)한 후 그에 따른 응답을 보내는 메소드
        /// </summary>
        /// <param name="_eventSuccess">서버로부터 받은 패킷을 처리가 됬으면 true, 아니면 false</param>
        public void ReceivedData_Process_To_DB_Node_ListView(bool _eventSuccess)
        {
            try
            {
                if (_eventSuccess)
                {
                    headerBuffer[0] = (byte)DataType.RESP_DATA_SUCCESS;
                }
                else
                {
                    headerBuffer[0] = (byte)DataType.RESP_DATA_FAIL;     
                }
                Send_To_Client(headerBuffer, DataBuffer);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{ex.ToString()}");
            }
        }


        /// <summary>
        /// 클라이언트와 패킷 주고 받는 메소드
        /// 기능 : 삽입, 삭제, 수정, 동기화
        /// </summary>
        public void Communication_Server_To_Client()
        {   
            try
            {
                while (this.client.Connected)
                {
                    Receive_To_Client();
                    string[] _receivedData = Split_Received_StringData(this.receivedString);

                    if (this.header.dataType == (byte)DataType.SEND_DATA)
                    {                                                 
                        switch (this.header.functionType)
                        {
                            case (byte)FunctionType.SYNC_SEND:                           
                                Console.WriteLine($"호스트에 동기화된 데이터 : {this.receivedString} |길이: {DataBuffer.Length}");
                                ReceivedData_Process_To_DB_Node_ListView(ReceivedData_Insert_To_DB_Node_ListView(_receivedData));          
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
                    }

                    else if(this.header.dataType == (byte)DataType.RESP_DATA_SUCCESS) 
                    {
                        switch (this.header.functionType)
                        {
                            case (byte)FunctionType.SYNC_SEND:
                                Console.WriteLine($"클라이언트에 동기화된 데이터 : {this.receivedString} |길이: {DataBuffer.Length}");
                                break;
                            case (byte)FunctionType.SAVE_DATA:
                                Console.WriteLine($"클라이언트에 저장된 데이터 : {this.receivedString} |길이: {DataBuffer.Length}");
                                break;
                            case (byte)FunctionType.UPDATE_DATA:
                                Console.WriteLine($"클라이언트에 업데이트된 데이터 : {this.receivedString} |길이: {DataBuffer.Length}");
                                break;
                            case (byte)FunctionType.DELETE_DATA:
                                Console.WriteLine($"클라이언트에 삭제된 데이터 : {this.receivedString} |길이: {DataBuffer.Length}");
                                break;
                        }
                    }

                    else if (this.header.dataType == (byte)DataType.RESP_DATA_FAIL)
                    {   
                        switch(this.header.functionType)
                        {       
                            case (byte)FunctionType.SYNC_SEND:
                                Console.WriteLine($"클라이언트에 동기화 안된 데이터 : {this.receivedString} |길이: {DataBuffer.Length}");
                                Resend_Data_To_Client(_receivedData[0], FunctionType.SYNC_SEND);
                                break;
                            case (byte)FunctionType.SAVE_DATA:
                                Console.WriteLine($"클라이언트에 저장 안된 데이터 : {this.receivedString} |길이: {DataBuffer.Length}");
                                Resend_Data_To_Client(_receivedData[0], FunctionType.SAVE_DATA);
                                break;
                            case (byte)FunctionType.UPDATE_DATA:
                                Console.WriteLine($"클라이언트에 수정 안된 데이터 : {this.receivedString} |길이: {DataBuffer.Length}");
                                Resend_Data_To_Client(_receivedData[0], FunctionType.UPDATE_DATA);
                                break;
                            case (byte)FunctionType.DELETE_DATA:
                                Console.WriteLine($"클라이언트에 삭제 안된 데이터 : {this.receivedString} |길이: {DataBuffer.Length}");
                                Resend_Data_To_Client(_receivedData[0], FunctionType.DELETE_DATA);
                                break;
                        }
                    }
                    Array.Clear(this.headerBuffer, 0, this.headerBuffer.Length);
                    Array.Clear(this.DataBuffer, 0, this.DataBuffer.Length);
                }
            }
            catch (SocketException socketEx)
            {
                Console.WriteLine("[Error]:{0}소켓ㅡServer_chlee.Communication_Server_To_Client()", socketEx.ToString());
                Disconnect();
            }
            catch (Exception commonEx)
            {
                Console.WriteLine("[Error]:{0}일반ㅡServer_chlee.Communication_Server_To_Client()", commonEx.ToString());
                Disconnect();
            }
        }       
    }
}
