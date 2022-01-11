using System;
using System.Windows.Forms;
using Project_DataStructure.Data;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

/* 완성이력 : 2021-03-16
* 수정이력 : 03-11 : 표기법 수정(수정, 출력 부분 제외)
*            03-14 : 삭제기능 수정(최종확인 메시지 박스 추가)  
*            03-15 : 수정기능 추가, 예외 처리 수정
*            03-16 : 리스트뷰 선택된 항목 초기화 
*            03-17 : 나이 숫자 text박스 숫자만 입력받는 기능 추가
*            03-18 : DB 접속, 삽입 기능 추가
*            03-21 : DB 조회 기능 추가 : DB저장된 것을 리스트뷰에 추가
*            03-22 : DB 삭제 , 수정 기능 추가, 함수 네이밍 수정, 정보 수정시 비밀번호 확인폼 구현
*            03-23 : DB 클래스 생성 (삽입, 삭제 ,생성, 수정)
*            03-24 : btn_ConnectDB_Click 수정 (MySqlDataReader클래스 -> MySqlDataAdapter클래스)
*            03-30 : 서버 접속 버튼 , 서버 시작 버튼, CommonData ->string화 변환
*            03-31 : DB.SelectDataDB()의 while문 이벤트 처리 구현
*            04-01 : txt_id, txt_pw 키 프레스 이벤트 구현, server Accept() 스레드 구현
*            04-02 : received Data를 DB,ListView,Node에 삽입 (이벤트로 구현)
*            04-05 : invoke 함수 추가 (delegate 기반)
*            04-07 : 패킷 설정 메소드 추가, 데이터 전송까지 확인
*            04-08 : 패킷 받아서 수정, 삭제, 동기화 메소드 구현
*            04-12 : 코드네이밍, 모듈화, 동기화 구현
* 기능     : 삽입, 삭제, 생성, 수정  
*/
namespace Project_DataStructure.chlee
{

    delegate bool FuntionType_EventHandler(string[] _data);
    delegate void Send_SyncData_EventHandler();
    delegate void Resend_EventHandler(string _id, FunctionType _functionType);
    delegate void SyncData_Collection_EventHandler(string _id);
    delegate void CRUD_Add_ListView_TextCount(CommonData _data);
    /// <summary>
    /// 회원가입 관리 프로그램
    /// Copy_List : 클라이언트에서 서버로 동기화 데이터를 보낼때 사용할 리스트 변수
    /// </summary>
    public partial class ucFormChlee : UserControl
    {
        delegate bool FuntionType_EventHandler1(string[] _data);
        DataBase_chlee DB;
        LinkedList_chlee<CommonData> List;
        List<CommonData> list;
        Client_chlee client;
        Server_chlee server;

        /// <summary>
        /// 폼 생성자
        /// </summary>
        public ucFormChlee()
        {
            InitializeComponent();
            GroupBox_Activate(false);
            txt_count.Enabled = false;
            txt_createDate.Enabled = false;
            txt_modifyDate.Enabled = false;
            chk_AgeUpDown.Enabled = false;
            Able_Disable_Input(false);
            Able_Disable_Btn(false);
            btn_cancel.Enabled = false;
                      
            this.DB = new DataBase_chlee();
            this.List = new LinkedList_chlee<CommonData>();
            this.client = new Client_chlee();
            this.server = new Server_chlee();
            this.list = new List<CommonData>();
            EventManagement();
        }

        /// <summary>
        /// DB 연결 버튼
        /// 기능 : App 활성화
        ///        DB 연결 유무
        ///        DB의 데이터를 Copy_List에 복사하고 서버로 부터 받은 데이터에서 중복된 것을 뺀 것을 보내기 위해 여기서 this.Copy_List=this.List 선언
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ConnectDB_Click(object sender, EventArgs e)
        {   
            if(this.DB.ConnectCheckDB() == true)
            {
                GroupBox_Activate(true);
                Able_Disable_Btn(true);
                btn_cancel.Enabled = true;
                this.DB.SelectDataDB();
                txt_count.Text = List.Count().ToString();
                this.List.PrintData();               
                btn_ConnectDB.Enabled = false;
            }
        }
        public void Copy_NodeList_Data_For_Sync()
        {
            Node _node = List.last;
            if (_node != null)
            {
                for (int i = 0; i < List.Count(); i++)
                {
                    this.list.Add((CommonData)_node.item);
                    _node = _node.nextNode;
                }
            }
        }

        /// <summary>
        /// DB 연결 해제 버튼
        /// 기능 : App 비활성화
        ///        DB 연결 유무
        ///        리스트뷰 항목 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_CancelDB_Click(object sender, EventArgs e)
        {
            this.DB.Connect_Cancel_DataBase();
            
            if(this.DB.Check_DB_Status()== false)
            {
                Console.WriteLine("DB 연결 해제 정상 종료");
                GroupBox_Activate(false);
                ListView.Items.Clear();
                ClearTextBox_RadBtn_chkBox();
                this.List.DeleteAllNode();
                txt_count.Text = List.Count().ToString();
                btn_ConnectDB.Enabled = true;
                Able_Disable_Btn(false);
                btn_cancel.Enabled = false;
            }
            else
            {
                Console.WriteLine("DB 연결 해제가 정상 종료되지 않음");
            }

        }

        /// <summary>
        /// 생성버튼 이벤트
        /// 기능 : TextBox,RadioButton,CheckBox 활성화, 초기화 
        ///        성별 선택시 디폴트 : 남 으로 설정
        ///        리스트뷰 항목 선택 초기화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_create_Click(object sender, EventArgs e)
        {
            if (ListView.SelectedItems.Count > 0)
            {
                ListView.SelectedItems[0].Selected = false;
            }
            rad_man.Checked = true;
            btn_Add.Enabled = true;
            btn_update.Enabled = false;
            btn_delete.Enabled = false;
            ClearTextBox_RadBtn_chkBox();
            Able_Disable_Input(true);
        }

        /// <summary>
        /// 저장버튼 이벤트
        /// 기능 : 빈칸 예외 처리 및 ID중복 검사, 리스트 뷰에 추가,수정 데이터 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (TextBox_Exception_Check())
            {   
                Add_Data_To_App();
            }
        }


        /// <summary>
        /// 버튼 취소 메소드
        /// 기능 : text_box 초기화, input입력창, CRUD버튼 비활성화
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            if (ListView.SelectedItems.Count > 0)
            {
                ListView.SelectedItems[0].Selected = false;
            }
            ClearTextBox_RadBtn_chkBox();
            Able_Disable_Input(false);
            Able_Disable_Btn(true);
        }

        /// <summary>
        /// Data 업데이트 메소드
        /// 기능 : 수정할 ID 데이터를 수정 준비 상태로 만드는 메소드, 비밀번호 확인창 기능
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_update_Click(object sender, EventArgs e)
        {
            PwCheckForm _pwCheckForm = new PwCheckForm();
            if (ListView.SelectedItems.Count > 0)
            {   // 모달 옵션으로 폼 실행
                _pwCheckForm.ShowDialog();
                if (txt_pw.Text == _pwCheckForm._result)
                {
                    Able_Disable_Input(true);
                    txt_id.Enabled = false;
                    Able_Disable_Btn(false);
                    btn_Add.Enabled = true;
                }
                else
                {
                    MessageBox.Show("비밀번호가 맞지 않습니다.");
                }
            }
            else
            {
                MessageBox.Show($"리스트에서 수정할 항목을 선택해주세요.");
            }
        }

        /// <summary>
        /// 데이터 삭제 메소드
        /// 기능 : 최종 삭제 메시지 박스(OK, Cancel)  Node, DB, ListView에서 항목 삭제 및 삭제 패킷 전송 기능
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (ListView.SelectedItems.Count > 0)
            {
                DialogResult _deleteCheckMessage = MessageBox.Show($"계정: {txt_id.Text} 정말 삭제 하시겠습니까?", "최종삭제",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (_deleteCheckMessage == DialogResult.OK)
                {               
                    ListViewItem _selectedItem = ListView.SelectedItems[0];

                    Node _deleteNode = List.DeleteNode(_selectedItem.Text);   

                    this.DB.DeleteDataDB(_selectedItem.Text);           
                    ListView.Items.Remove(_selectedItem);             
                    txt_count.Text = this.List.Count().ToString();
                    ClearTextBox_RadBtn_chkBox();

                    //데이터 삭제 패킷 전송
                    Send_Data((CommonData)_deleteNode.item, FunctionType.DELETE_DATA);

                    MessageBox.Show("삭제가 완료되었습니다.");

                }
                else if (_deleteCheckMessage == DialogResult.Cancel)
                {
                    ClearTextBox_RadBtn_chkBox();
                    MessageBox.Show("취소되었습니다.");
                    ListView.SelectedItems[0].Selected = false;     //리스트뷰 항목 선택 초기화
                }
            }
            else
            {
                MessageBox.Show($"리스트에서 삭제할 항목을 선택해주세요.");
            }
            Able_Disable_Input(false);
        }

        /// <summary>
        /// 클라이언트 입장에서 서버 접속 버튼 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_connect_Click(object sender, EventArgs e)
        {
            Copy_NodeList_Data_For_Sync();
            this.client.Connect_To_Server(txt_ip.Text, int.Parse(txt_port.Text));
            if (this.client.Connected_Check())
            {
                btn_connect.Enabled = false;
            }
        }

        /// <summary>
        /// 서버 시작 버튼
        /// 클라이언트 접속시 Accept() thread 생성
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_serverStart_Click(object sender, EventArgs e)
        {
            Thread _server_Accept_th = new Thread(new ThreadStart(server.RunServer));
            _server_Accept_th.IsBackground = true;
            _server_Accept_th.Start();
        }

        /// <summary>
        /// 헤더 패킷을 만드는 메소드
        /// </summary>
        /// <param name="_dataType"></param>
        /// <param name="_functionType"></param>
        /// <param name="_sync_cnt"></param>
        /// <param name="_dataLength"></param>
        /// <returns></returns>
        public byte[] Make_headerByte(DataType _dataType, FunctionType _functionType,int _sync_cnt, int _dataLength)
        {
            Header _header = new Header()
            {
                dataType = (byte)_dataType,
                functionType = (byte)_functionType,
                sync_cnt =_sync_cnt, 
                bodyLen = _dataLength
            };
            byte[] _headerArray = new byte[10];
            _headerArray[0] = (byte)_header.dataType;
            _headerArray[1] = (byte)_header.functionType;
            byte[] _syncCnt = BitConverter.GetBytes(_header.sync_cnt);
            byte[] _dataSize = BitConverter.GetBytes(_header.bodyLen);
            Array.Copy(_syncCnt, 0, _headerArray, 2, 4);
            Array.Copy(_dataSize, 0, _headerArray, 6, 4);
            return _headerArray;
        }

        /// <summary>
        /// 보낼 데이터를 각각의 알맞은 body와 header를 만든다.
        /// </summary>
        /// <param name="_data">CommonData</param>
        /// <param name="_dataType">Header</param>
        /// <param name="_functionType">Header</param>
        /// <returns>헤더와 바디</returns>
        public Tuple<byte[], byte[]> Make_header_body(CommonData _data, DataType _dataType,int _sync_cnt, FunctionType _functionType)
        {
            string _StringData = Convert_CommonData_To_String(_data);
            byte[] _Body = Encoding.UTF8.GetBytes(_StringData);   
            byte[] _header = Make_headerByte(_dataType, _functionType, _sync_cnt, _Body.Length);
            return new Tuple<byte[], byte[]>(_header, _Body);           
        }

        /// <summary>
        /// 데이터를 전송하는 메소드
        /// </summary>
        /// <param name="_data">보낼 데이터</param>
        /// <param name="_funtionType">어떤 기능을 할 데이터를 보낼지</param>
        public void Send_Data(CommonData _data, FunctionType _funtionType)
        {
            var _packet = Make_header_body(_data, DataType.SEND_DATA,0, _funtionType);
           
                // 로컬이 클라이언트일때                   
                if (client.Connected_Check())
                {
                    client.Send_To_Server(_packet.Item1, _packet.Item2); // header , body
                    Console.WriteLine("서버에 데이터 전송함");
                }
                // 로컬이 서버일때
                if (server.Connected_Check())
                {
                    server.Send_To_Client(_packet.Item1, _packet.Item2);
                    Console.WriteLine("클라이언트에 데이터 전송함");
                }
        }

        /// <summary>
        /// 패킷 실패 메시지가 오면 그 데이터를 다시 전송한다.
        /// </summary>
        /// <param name="_id">다시 전송할 데이터를 찾을 Key</param>
        /// <param name="_functionType">어떤 기능을할지</param>
        public void Resend_Data(string _id, FunctionType _functionType)
        {
            Node _findNode = List.FindNode(_id);
            Send_Data((CommonData)_findNode.item, _functionType);
            
        }

        /// <summary>
        /// 데이터 중복검사 후 Insert
        /// </summary>
        public void Add_Data_To_ListView_List_DB()
        {
            CommonData _data = new CommonData()
            {
                CreateDate = DateTime.Now,
                ModifyDate = DateTime.Now,
                ID = txt_id.Text,
                PW = txt_pw.Text,
                Age = byte.Parse(txt_age.Text),
                AgeChk = CheckAgeUpDown(),
                Tall = (float)Math.Truncate(float.Parse(txt_tall.Text) * 10) / 10,
                SexType = CheckSex()
            };
            // ID 중복검사 진행
            if (List.ID_Duplicate_inspection(_data.ID) == false)
            {
                MessageBox.Show("중복된 아이디가 있습니다. 수정하세요.");
            }
            else
            {
                if (this.DB.Check_DB_Status() == true)
                {   
       
                    Send_Data(_data, FunctionType.SAVE_DATA); // 서버나 클라이언트에 패킷전송

                    List.InsertNode(_data);
                    this.DB.InsertDataDB(_data);
                    Add_Data_To_ListView(_data);

                    List.PrintData();
                    txt_count.Text = List.Count().ToString();

                    btn_update.Enabled = false;
                    btn_delete.Enabled = false;
                    btn_Add.Enabled = false;

                    MessageBox.Show("저장이 완료 되었습니다.");
                    ClearTextBox_RadBtn_chkBox();
                    Able_Disable_Input(false);
                }
                else
                {
                    MessageBox.Show("DB 연결을 먼저 해주세요.  -- 저장부분");
                }

            }
        }

        /// <summary>
        /// 리스트 뷰에서 선택한 id를 기준으로 정보를 업데이트 후
        /// DB, ListView, Node에 정보 업데이트 기능
        /// </summary>
        public void Update_Data_To_ListView_List_DB()
        {

            ListViewItem _selectedItem = Update_Data_To_ListView();

            // 노드에 업데이트된 정보 삽입
            CommonData _data = new CommonData()
            {
                ModifyDate = DateTime.Now,
                ID = _selectedItem.Text,
                PW = txt_pw.Text,
                Age = byte.Parse(txt_age.Text),
                AgeChk = CheckAgeUpDown(),
                Tall = float.Parse(txt_tall.Text),               
            };
            CommonData _UpdateItem = List.UpdateNode(_data);
            if (rad_man.Checked == true)
            { _UpdateItem.SexType = SexTypes.Male; }
            else if (rad_woman.Checked == true)
            { _UpdateItem.SexType = SexTypes.FeMale; }

           
            Send_Data(_UpdateItem, FunctionType.UPDATE_DATA);
            // DB 내용 업데이트
            if (this.DB.Check_DB_Status() == true)
            {
                this.DB.UpdateDataDB(_UpdateItem);
                txt_count.Text = List.Count().ToString();
                List.PrintData();
                ClearTextBox_RadBtn_chkBox();
                Able_Disable_Input(false);
                btn_create.Enabled = true;
                btn_update.Enabled = false;
                btn_Add.Enabled = false;
                MessageBox.Show("수정이 완료되었습니다.");
                ListView.SelectedItems[0].Selected = false; // 리스트뷰 항목 선택 초기화
            }
            else
            {
                MessageBox.Show("DB 연결을 먼저 해주세요. -- 수정부분");
            }
        }

        /// <summary>
        /// 네트워크 통신시 데이터 송신할때 CommonData SexTypes열거 형식을 string으로 변환해서 데이터 보내기 위한 메소드
        /// </summary>
        /// <param name="_data">SexTypes 형식</param>
        /// <returns></returns>
        public string Convert_SexType_To_String(SexTypes _data)
        {
            if (_data == SexTypes.Male)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        /// <summary>
        /// 네트워크 통신시 데이터 송신할때 CommonData bool형식을 string으로 변환해서 데이터 보내기 위한 메소드
        /// </summary>
        /// <param name="_data">bool 형식</param>
        /// <returns></returns>
        public string Convert_AgeChk_To_String(bool _data)
        {
            if (_data == true)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        /// <summary>
        /// 네트워크 통신시 데이터 수신할때 string형식을CommonData SexTypes로 변환해서 데이터 저장을 위한 메소드
        /// </summary>
        /// <param name="_data">string 형식</param>
        /// <returns></returns>
        public SexTypes Convert_String_To_SexTypes(string _data)
        {
            if(_data == "1")
            {
                return SexTypes.Male;
            }else
            {
                return SexTypes.FeMale;
            }
        }

        /// <summary>
        /// 네트워크 통신시 데이터 수신할때 string형식을CommonData bool으로 변환해서 데이터 저장을 위한 메소드
        /// </summary>
        /// <param name="_data">string 형식</param>
        /// <returns></returns>
        public bool Convert_String_To_Boolean(string _data)
        {
            if(_data == "1")
            {
                return true;
            }else
            {
                return false;
            }
        }

        /// <summary>
        /// CommonData 데이터를 갖고 있는 배열을 CommonData형식으로 변환 
        /// </summary>
        /// <param name="_CommonData"></param>
        /// <returns></returns>
        public CommonData Convert_StringArray_To_CommonData(string[] _CommonData)
        {
            CommonData _data = new CommonData()
            {
                CreateDate = Convert.ToDateTime(_CommonData[0]),
                ModifyDate = Convert.ToDateTime(_CommonData[1]),
                ID = _CommonData[2],
                PW = _CommonData[3],
                Age = byte.Parse(_CommonData[4]),
                AgeChk = Convert_String_To_Boolean(_CommonData[5]),
                Tall = (float)Math.Truncate(float.Parse(_CommonData[6]) * 10) / 10,
                SexType = Convert_String_To_SexTypes(_CommonData[7])
            };
            return _data;
        }

        public bool Sync_ReceiveData_To_DB_Node_ListView(string[] _CommonData)
        {

            if (_CommonData != null)
            {
                CommonData _data = Convert_StringArray_To_CommonData(_CommonData);
                if (List.ID_Duplicate_inspection(_data.ID) == false)
                {
                    //업데이트
                    List.UpdateNode(_data);
                    List.PrintData();
                    this.DB.UpdateDataDB(_data);

                    if (this.InvokeRequired)
                    {
                        this.Invoke(new CRUD_Add_ListView_TextCount(Update_ListView_TextCount), _data);
                    }
                    else
                    {
                        Update_ListView_TextCount(_data);
                    }
                }
                else 
                {
                    List.InsertNode(_data);
                    List.PrintData();
                    this.DB.InsertDataDB(_data);
                    //리스트뷰에 항목 추가 함수
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new CRUD_Add_ListView_TextCount(Insert_ListView_TxtCount), _data);
                    }
                    else
                    {
                        Insert_ListView_TxtCount(_data);
                    }
                }
                return true;
            }            
            else
            {
                Console.WriteLine("Error");
                return false;
            }
        }

     

        /// <summary>
        /// server.DataSendRecv() 스레드에서 접근해야할 Form 컨트롤
        /// </summary>
        /// <param name="_data"></param>
        public void Insert_ListView_TxtCount(CommonData _data)
        {
            Add_Data_To_ListView(_data);
            txt_count.Text = List.Count().ToString();
        }

  
       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_CommonData"></param>
        public bool Insert_ReceiveData_To_DB_Node_ListView(string[] _CommonData)
        {
            if (_CommonData != null)
            {
                CommonData _data = Convert_StringArray_To_CommonData(_CommonData);

                // 중복검사
                if (List.ID_Duplicate_inspection(_data.ID) == false)
                {
                    Console.WriteLine("중복된 id가 있습니다.");
                    
                }
                else
                {
                    List.InsertNode(_data);                 
                    List.PrintData();
                    this.DB.InsertDataDB(_data);
                    //리스트뷰에 항목 추가 함수
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new CRUD_Add_ListView_TextCount(Insert_ListView_TxtCount), _data);
                    }
                    else
                    {
                        Insert_ListView_TxtCount(_data);
                    }
                }
                return true;
            }
            else
            {
                Console.WriteLine("Error");
                return false;
            }
        }

        // 리스트뷰 항목 업데이트 : 클라이언트로 부터 데이터를 수정하라는 패킷을 받을 때 실행
        public void Update_ListView_TextCount(CommonData _data)
        {
            for(int i=0; i<ListView.Items.Count; i++)
            {
                if(ListView.Items[i].Text == _data.ID)
                {
                    ListView.Items[i].SubItems[1].Text = _data.Age.ToString();
                    if (_data.SexType == SexTypes.Male)
                    {
                        ListView.Items[i].SubItems[2].Text = "남성";
                    }
                    else
                    {
                        ListView.Items[i].SubItems[2].Text = "여성";
                    }
                }
            }
            txt_count.Text = List.Count().ToString();
        }

        /// <summary>
        /// 잠재적 수정 사항 : 날짜 부분 기존꺼는 노드,디비 업데이트부분에 수정된 날짜만 할지 안할지
        /// </summary>
        /// <param name="_CommonData"></param>
        public bool Update_ReceiveData_To_DB_Node_ListView(string[] _CommonData)
        {
            if(_CommonData != null)
            {
                CommonData _data = Convert_StringArray_To_CommonData(_CommonData);

                List.UpdateNode(_data);
                List.PrintData();
                this.DB.UpdateDataDB(_data);

                if (this.InvokeRequired)
                {
                    this.Invoke(new CRUD_Add_ListView_TextCount(Update_ListView_TextCount), _data);
                }
                else
                {
                    Update_ListView_TextCount(_data);
                }
                return true;
            }
            else
            {
                Console.WriteLine("Error ㅡㅡ update");
                return false;
            }

        }


        public void Delete_ListView_TextCount(CommonData _data)
        {
            for (int i = 0; i < ListView.Items.Count; i++)
            {
                if (ListView.Items[i].Text == _data.ID)
                {   
                    ListView.Items.Remove(ListView.Items[i]);
                }
            }
            txt_count.Text = List.Count().ToString();
            
        }

        public bool Delete_ReceiveData_To_DB_Node_ListView(string[] _CommonData)
        {
            if (_CommonData != null)
            {
                CommonData _data = Convert_StringArray_To_CommonData(_CommonData);

                List.DeleteNode(_data.ID);
                List.PrintData();
                this.DB.DeleteDataDB(_data.ID);

                if (this.InvokeRequired)
                {
                    this.Invoke(new CRUD_Add_ListView_TextCount(Delete_ListView_TextCount), _data);
                }
                else
                {
                    Delete_ListView_TextCount(_data);
                }
                return true;
            }
            else
            {
                Console.WriteLine("Error ㅡㅡ Delete");
                return false;
            }
        }


        /// <summary>
        /// DB.SelectDataDB() 메소드 이벤트를 위한 메소드
        /// 기능 : 리스트뷰, 노드에 데이터 추가
        /// </summary>
        /// <param name="_data">CommonData</param>
        public void DB_Data_Insert_To_ListView_Node(CommonData _data)
        {
            Add_Data_To_ListView(_data);
            List.InsertNode(_data);
        }

        /// <summary>
        /// 클라이언트 에서 서버로 동기화 데이터를 보내줄때 사용 메소드
        /// 기능 : 서버와 중복된 데이터를 배제한 클라이언트 데이터를 갖는다.
        /// </summary>
        /// <param name="_id">아이디를 기준으로 서버와 중복된 데이터를 삭제</param>
        public void Data_Collection_To_Server(string _data)
        {
            CommonData _findData = list.Find(delegate (CommonData p)
            {
                return p.ID.Equals(_data);
            });
            if (_findData != null)
            {
                this.list.Remove(_findData);
            }
        }

        /// <summary>
        /// 클라이언트가 서버로 선별된 동기화 데이터를 전송하는 메소드
        /// </summary>
        public void SendSyncData_Client_To_Server()
        {
            if (list != null)
            {
                for (int i = 0; i < this.list.Count; i++)
                {
                    CommonData _data = list[i];
                    var _packet = Make_header_body(_data, DataType.SEND_DATA, list.Count, FunctionType.SYNC_SEND);
                    this.client.Send_To_Server(_packet.Item1, _packet.Item2);
                }
            }
            else
            {
                Console.WriteLine("list is empty. ㅡㅡㅡㅡㅡ클라이언트에서 서버로 동기화 데이터 보내는거 시도하는 부분");
            }
        }

        /// <summary>
        /// 클라이언트가 서버에 접속되면 동기화를 위해 서버의 노드 데이터 모두를 보낸다.
        /// </summary>
        public void SendSyncData_Server_To_Client()
        {
            Node _node = this.List.last;
            if (_node != null)
            {
                for (int i = 0; i < this.List.Count(); i++)
                {
                    CommonData _data = (CommonData)_node.item;
                    var _packet = Make_header_body(_data, DataType.SEND_DATA,List.Count(), FunctionType.SYNC_SEND);
                    this.server.Send_To_Client(_packet.Item1, _packet.Item2);
                    _node = _node.nextNode;
                }
            }
            else
            {
                //_data : 쓰레기값
                CommonData _data = new CommonData()
                {
                    CreateDate = DateTime.Now,
                    ModifyDate = DateTime.Now,
                    ID = "0",
                    PW = "0",
                    Age = 0,
                    AgeChk = false,
                    Tall = 0,
                    SexType = SexTypes.None
                };
                var _packet = Make_header_body(_data, DataType.SEND_DATA, this.List.Count(), FunctionType.SYNC_SEND);
                this.server.Send_To_Client(_packet.Item1, _packet.Item2);
                Console.WriteLine("보낼 동기화 패킷이 없습니다. 쓰레기 패킷을 전송합니다.");
            }
        }

        /// <summary>
        /// 네트워크 통신시 데이터 송신할때를 위한 CommonData 객체를 String으로 변환
        /// </summary>
        /// <param name="_data">CommonData 객체</param>
        /// <returns>","를 구분자로 갖는 CommonData string형식</returns>
        public string Convert_CommonData_To_String(CommonData _data)
        {
            string _sendStringData = _data.CreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "," + _data.ModifyDate.ToString("yyyy-MM-dd HH:mm:ss") +
                    "," + _data.ID + "," + _data.PW + "," + _data.Age + "," + Convert_AgeChk_To_String(_data.AgeChk) + "," +
                    _data.Tall + "," + Convert_SexType_To_String(_data.SexType);

            return _sendStringData;
        }

        /// <summary>
        /// 30세 이상 체크 true/false Commondata  (bool)AgeChk에 값 넣는 메소드
        /// </summary>
        /// <returns>30 세 이상: true, 30세 이하 : false</returns>
        public bool CheckAgeUpDown()
        {
            if (byte.Parse(txt_age.Text) > 29)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 라디오 버튼 유무로 CommonData (SexTypes)SexType에 값 넣는 메소드
        /// </summary>
        /// <returns>남 : 1,여 : 2, None : 0</returns>
        public SexTypes CheckSex()
        {
            SexTypes _result;
            if (rad_man.Checked)
            {
                _result = SexTypes.Male;
                return _result;
            }
            else if (rad_woman.Checked)
            {
                _result = SexTypes.FeMale;
                return _result;
            }
            else
            {
                _result = SexTypes.None;
                return _result;
            }
        }

        /// <summary>
        /// 일반 데이터 저장 및 수정 데이터 저장 기능 메소드(리스트뷰, 노드, DB에 저장)
        /// 기능 : txt_id.Enabled로 일반 데이터 저장인지 수정 데이터 저장인지 구분
        /// </summary>
        public void Add_Data_To_App()
        {
            // 일반적인 생성 후 새로운 데이터 저장
            if (txt_id.Enabled == true)
            {
                Add_Data_To_ListView_List_DB();
            }
            // 수정된 정보 저장 
            else if (txt_id.Enabled == false)
            {
                Update_Data_To_ListView_List_DB();
            }
        }

        /// <summary>
        /// 리스트뷰에 Data 추가 함수
        /// 설명 : Commondata 객체의 ID 기준으로 정보 추가
        /// </summary>
        /// <param name="_memberData">회원정보의 ID</param>
        public void Add_Data_To_ListView(CommonData _memberData)
        {
            ListViewItem _memberID = new ListViewItem(_memberData.ID);
            _memberID.SubItems.Add(_memberData.Age.ToString());
            if (_memberData.SexType == SexTypes.Male)
            {
                _memberID.SubItems.Add("남성");
            }
            else if (_memberData.SexType == SexTypes.FeMale)
            {
                _memberID.SubItems.Add("여성");
            }
            ListView.Items.Add(_memberID);
        }

        /// <summary>
        /// 리스트뷰에서 선택한 id를 기준으로 리스트뷰 나이, 성별 업데이트
        /// </summary>
        /// <returns>리스트뷰에서 선택된 item 객체</returns>
        public ListViewItem Update_Data_To_ListView()
        {
            ListViewItem _selectedItem = ListView.SelectedItems[0];
            _selectedItem.SubItems[1].Text = txt_age.Text.ToString();

            if (rad_man.Checked == true)
            {
                _selectedItem.SubItems[2].Text = "남성";
            }
            else if (rad_woman.Checked == true)
            {
                _selectedItem.SubItems[2].Text = "여성";
            }
            return _selectedItem;
        }

        /// <summary>
        /// ListView 항목 선택 메소드
        /// 기능 : 항목 한개만 선택 가능,   수정, 삭제, 조회 기능시 사용 메소드
        ///        선택된 항목을 노드에서 찾아 텍스트박스에 띄운다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {

            if (ListView.SelectedItems.Count > 0)
            {
                ListViewItem _selectedItem = ListView.SelectedItems[0];
                CommonData _findData = (CommonData)List.FindNode(_selectedItem.Text).item;
                txt_id.Text = _findData.ID;
                txt_pw.Text = _findData.PW;
                txt_pw_ok.Text = txt_pw.Text;
                txt_tall.Text = _findData.Tall.ToString();
                txt_age.Text = _findData.Age.ToString();
                txt_createDate.Text = _findData.CreateDate.ToString();
                txt_modifyDate.Text = _findData.ModifyDate.ToString();
                chk_AgeUpDown.Checked = _findData.AgeChk;
                if (_findData.SexType == SexTypes.Male)
                {
                    rad_man.Checked = true;
                }
                else if (_findData.SexType == SexTypes.FeMale)
                {
                    rad_woman.Checked = true;
                }
            }
            btn_update.Enabled = true;
            btn_delete.Enabled = true;
            btn_Add.Enabled = false;
            Able_Disable_Input(false);
        }

        /// <summary>
        /// textbox exception 처리 메소드
        /// </summary>
        /// <returns>true : 통과, false : 예외발생</returns>
        public bool TextBox_Exception_Check()
        {
            try
            {
                if (String.IsNullOrWhiteSpace(txt_id.Text))
                {
                    MessageBox.Show("입력한 ID가 없습니다.");
                    return false;
                }
                else if (String.IsNullOrWhiteSpace(txt_pw.Text))
                {
                    MessageBox.Show("비밀번호를 입력해주세요.");
                    return false;
                }
                else if (String.IsNullOrWhiteSpace(txt_pw_ok.Text))
                {
                    MessageBox.Show("비밀번호 확인창에 다시 입력해주세요.");
                    return false;
                }
                else if (txt_pw.Text != txt_pw_ok.Text)
                {
                    MessageBox.Show("비밀번호가 맞지 않습니다.다시 입력해주세요.");
                    return false;
                }
                else if (String.IsNullOrWhiteSpace(txt_tall.Text))
                {
                    MessageBox.Show("키를 입력해주세요.");
                    return false;
                }
                else if ((float.Parse(txt_tall.Text) > 300) || (float.Parse(txt_tall.Text) < 1))
                {
                    MessageBox.Show("키를 다시 확인해 주세요.");
                    return false;
                }
                else if (String.IsNullOrWhiteSpace(txt_age.Text))
                {
                    MessageBox.Show("나이를 입력해주세요.");
                    return false;
                }
                else if (int.Parse(txt_age.Text) > 150)
                {
                    MessageBox.Show("나이를 다시 확인해 주세요.");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message} 예외 생김 수정필요");
                return false;
            }
        }

        /// <summary>
        /// TextBox,RadioButton,CheckBox 초기화 메소드
        /// </summary>
        public void ClearTextBox_RadBtn_chkBox()
        {
            txt_id.Clear();
            txt_pw.Clear();
            txt_pw_ok.Clear();
            txt_tall.Clear();
            txt_age.Clear();

            rad_woman.Checked = false;
            chk_AgeUpDown.Checked = false;
            txt_createDate.Clear();
            txt_modifyDate.Clear();
        }

        /// <summary>
        /// TextBox,RadioButton 활성화 메소드
        /// 기능 : true : 활성화, false : 비활성화
        /// </summary>
        public void Able_Disable_Input(bool _activeStatus)
        {
            txt_id.Enabled = _activeStatus;
            txt_pw.Enabled = _activeStatus;
            txt_pw_ok.Enabled = _activeStatus;
            txt_tall.Enabled = _activeStatus;
            txt_age.Enabled = _activeStatus;
            rad_man.Enabled = _activeStatus;
            rad_woman.Enabled = _activeStatus;
        }

        /// <summary>
        /// 버튼 활성화 메소드
        /// 기능 : true : 활성화, false : 비활성화
        /// </summary>
        public void Able_Disable_Btn(bool _activeStatus)
        {
            btn_Add.Enabled = _activeStatus;
            btn_create.Enabled = _activeStatus;
            btn_update.Enabled = _activeStatus;
            btn_delete.Enabled = _activeStatus;
        }

        /// <summary>
        /// DB 접속버튼 누를시 그룹박스 모두 활성화 메소드
        /// </summary>
        /// <param name="_activeStatus">활성화 여부 true, false</param>
        public void GroupBox_Activate(bool _activeStatus)
        {
            gbx_ListView.Enabled = _activeStatus;
            gbx_memberData.Enabled = _activeStatus;
            gbx_Connect.Enabled = _activeStatus;
        }

        /// <summary>
        /// txt_id "," 입력 안되게 하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_id_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 44))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// txt_pw "," 입력 안되게 하는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_pw_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 44))
            {
                e.Handled = true;
            }
        }

        // <summary>
        // txt_Age 정수 숫자만 받을 수 있는 메소드
        // </summary>
        // <param name = "sender" ></ param >
        // < param name="e"></param>
        private void txt_age_KeyPress(object sender, KeyPressEventArgs e)
        {
            //아스키코드 기준으로 0~9 와 백스페이스만 받게 
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// txt_tall 정수 숫자만 받을 수 있는 메소드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_tall_KeyPress(object sender, KeyPressEventArgs e)
        {
            //아스키코드 기준으로 0~9 와 백스페이스, 온점만 받게 
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8) && (e.KeyChar != 46))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// txt_age.Text 입력시 chk_ageUpDown 자동 체크 메소드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_age_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(txt_age.Text))
                {
                    chk_AgeUpDown.Checked = false;
                }
                else if (int.Parse(txt_age.Text) > 29)
                {
                    chk_AgeUpDown.Checked = true;
                }
                else
                {
                    chk_AgeUpDown.Checked = false;
                }
            }
            catch (OverflowException ex)
            {
                txt_age.Clear();
                MessageBox.Show($"{ex} , 알맞은 나이를 입력해주세요.");
            }
        }

        /// <summary>
        /// 이벤트 관리 메소드
        /// </summary>
        public void EventManagement()
        {
            this.server.ReceivedData_Insert_To_DB_Node_ListView += new FuntionType_EventHandler(Insert_ReceiveData_To_DB_Node_ListView);
            this.server.ReceivedData_Update_To_DB_Node_ListView += new FuntionType_EventHandler(Update_ReceiveData_To_DB_Node_ListView);
            this.server.ReceivedData_Delete_To_DB_Node_ListView += new FuntionType_EventHandler(Delete_ReceiveData_To_DB_Node_ListView);
            this.server.Send_SyncData_To_Client += new Send_SyncData_EventHandler(SendSyncData_Server_To_Client);
            this.server.Resend_Data_To_Client += new Resend_EventHandler(Resend_Data);

            this.client.ReceivedData_Insert_To_DB_Node_ListView += new FuntionType_EventHandler(Insert_ReceiveData_To_DB_Node_ListView);
            this.client.ReceivedData_Update_To_DB_Node_ListView += new FuntionType_EventHandler(Update_ReceiveData_To_DB_Node_ListView);
            this.client.ReceivedData_Delete_To_DB_Node_ListView += new FuntionType_EventHandler(Delete_ReceiveData_To_DB_Node_ListView);
            this.client.ReceivedData_Sync_To_DB_Node_ListView += new FuntionType_EventHandler(Sync_ReceiveData_To_DB_Node_ListView);
            this.client.Send_SyncData_To_Server += new Send_SyncData_EventHandler(SendSyncData_Client_To_Server);
            this.client.Synchronization_Data_Collection += new SyncData_Collection_EventHandler(Data_Collection_To_Server);
            this.client.Resend_Data_To_Server += new Resend_EventHandler(Resend_Data);

            this.DB.Insert_DBData_To_ListView_Node += new DB_Data_Insert_EventHandler(DB_Data_Insert_To_ListView_Node);
        }
    }
}

