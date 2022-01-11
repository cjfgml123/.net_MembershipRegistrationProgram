using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Project_DataStructure.Data;
using System.Data;

/* 완성이력 : 2021-03-22
* 수정이력 :  03-23 : 코드 모듈화 수정, 조회 부분(MySqlDataReader클래스 -> MySqlDataAdapter클래스)
*             03-31 : MySqlDataReader 클래스 사용하면서 이벤트 호출 (while문 안에)
* 기능     : 삽입, 삭제, 생성, 수정  
*/
namespace Project_DataStructure.chlee
{
    delegate void DB_Data_Insert_EventHandler(CommonData _data);

    /// <summary>
    /// DB (생성, 삽입, 삭제, 수정) 기능 클래스
    /// </summary>
    class DataBase_chlee
    {
        
        string ServerName = "localhost";
        string DataBase = "commondata";
        string UserId = "";
        string UserPw = "";    
        string tableName = "test";

        MySqlConnection Connection;

        public event DB_Data_Insert_EventHandler Insert_DBData_To_ListView_Node;

        /// <summary>
        /// DB 연결 해제 메소드
        /// </summary>
        public void Connect_Cancel_DataBase()
        {
            Connection.Close();
        }

        /// <summary>
        /// DB 연결 상태 확인 메소드
        /// </summary>
        /// <returns></returns>
        public bool Check_DB_Status()
        {
            return Connection.Ping();
        }

        /// <summary>
        /// 처음 DB 연결 체크 유무
        /// 기능 : 접속하고 다시 닫는다.
        /// </summary>
        public bool ConnectCheckDB()
        {
            string _strConn = "Server=" + ServerName + ";Database=" + DataBase + ";Uid=" + UserId + ";Pwd=" + UserPw + ";";
            Connection = new MySqlConnection(_strConn);
            Connection.Open();
            if (Connection.Ping() == true)
            {
                CreateTableDB();
                Console.WriteLine("처음 DB 연결 성공");
                return true;
            }
            else 
            {
                Console.WriteLine("처음 DB 연결 실패");
                return false;
            }
        }

        /// <summary>
        /// 쿼리 처리 함수(insert, delete, update)에 사용
        /// </summary>
        /// <param name="_sql">쿼리문</param>
        public void ProcessQuery(string _sql)
        {
            try
            {
                MySqlCommand _cmd = new MySqlCommand(_sql, Connection);
                _cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}");
            }
        }

        /// <summary>
        /// 테이블이 이미 존재하는지 여부 체크 후 테이블 생성
        /// </summary>
        public void CreateTableDB()
        {   // 테이블이 있는지 조회
            string _tableCheck = "select 1 from Information_schema.tables where table_schema='"+DataBase+"' and table_name='" + tableName + "';";
            MySqlCommand cmd = new MySqlCommand(_tableCheck,Connection);
            int _tableCheckResult = Convert.ToInt32(cmd.ExecuteScalar());   // true : 1 , false : 0

            if (_tableCheckResult == 0)
            {
                string _sqlQuery = "CREATE TABLE " + tableName + "(" +
                    "createDate DATETIME," +
                    "modifyDate DATETIME," +
                    "id VARCHAR(30) NOT null," +
                    "pw VARCHAR(30) NOT NULL," +
                    "age INT NOT NULL," +
                    "agechk boolean NOT null," +
                    "tall FLOAT(1) NOT NULL," +
                    "sexType boolean NOT NULL," +
                    "PRIMARY KEY(id));";

                ProcessQuery(_sqlQuery);
                Console.WriteLine($"{tableName} 테이블 생성 완료");               
            }
            else
            {
                Console.WriteLine($"{tableName} 테이블 존재함");
            }
        }

        /// <summary>
        /// DB의 데이터를 가져오는 메소드
        /// </summary>
        //public DataSet SelectDataDB_Adapter()
        //{
        //    try {
        //        DataSet _dataset = new DataSet();
        //        string _sqlQuery = "select createDate,modifyDate,id,pw,age,agechk,tall,sexType from " + tableName + ";";
        //        MySqlDataAdapter adpt = new MySqlDataAdapter(_sqlQuery, Connection);

        //        // Fill 메소드를 실행하여 결과 DataSet을 리턴 받음.
        //        adpt.Fill(_dataset);
        //        return _dataset;
        //    }
        //    catch(Exception e)
        //    {
        //        Console.WriteLine($"{e}... select 실패");
        //        return null;
        //    }
            
        //}

        /// <summary>
        /// DB의 데이터를 가져오는 메소드
        /// </summary>
        public void SelectDataDB()
        {
            try
            {
                string _sqlQuery = "select createDate,modifyDate,id,pw,age,agechk,tall,sexType from " + tableName + ";";

                MySqlCommand _cmd = new MySqlCommand(_sqlQuery, Connection);
                MySqlDataReader _reader = _cmd.ExecuteReader();         // 데이터는 db에서 가져오도록 실행

                
                while (_reader.Read())
                {
                    //Console.WriteLine($"{_reader["createDate"]}: {_reader["modifyDate"]}: {_reader["id"]}:" +
                    //$"{ _reader["pw"]}: { _reader["age"]}: {_reader["agechk"]}: {_reader["tall"]}: { _reader["sexType"]}");

                    CommonData _data = new CommonData()
                    {
                        CreateDate = (DateTime)_reader["createDate"],
                        ModifyDate = (DateTime)_reader["modifyDate"],
                        ID = (string)_reader["id"],
                        PW = (string)_reader["pw"],
                        Age = byte.Parse(_reader["age"].ToString()),
                        AgeChk = (bool)_reader["agechk"],
                        Tall = (float)_reader["tall"], // 첫째자리까지 표시
                        SexType = DB_sexType_change_CommondataType(_reader)
                    };
                    //(리스트뷰, 노드에 데이터 추가)이벤트 호출
                    Insert_DBData_To_ListView_Node(_data);
                }
                _reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}, DB접속실패 - select");
            }
        }

        /// <summary>
        /// DB에 데이터 삽입
        /// </summary>
        /// <param name="_data">삽입할 CommonData 객체</param>
        public void InsertDataDB(CommonData _data)
        {
            string _sqlQuery = "insert into " + tableName + "(createDate,modifyDate,id,pw,age,agechk,tall,sexType)" +
                    "values('" + _data.CreateDate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + _data.ModifyDate.ToString("yyyy-MM-dd HH:mm:ss") +
                    "','" + _data.ID + "','" + _data.PW + "','" + _data.Age + "'," + _data.AgeChk + ",'" +
                    _data.Tall + "'," + Insert_DB_SEX(_data.SexType) + ")";

            ProcessQuery(_sqlQuery);
        }

        /// <summary>
        /// DB의 데이터 삭제 메소드
        /// 기능 : 기본키 id를 기준으로 행을 삭제
        /// </summary>
        /// <param name="_id">삭제할 id 값</param>
        public void DeleteDataDB(string _id)
        {
            string _sqlQuery = "DELETE FROM " + tableName + " WHERE id='" + _id + "';";

            ProcessQuery(_sqlQuery);
        }

        /// <summary>
        /// DB의 데이터 갱신 메소드
        /// 기능 : 기본키 id를 기준으로 행 정보 업데이트(createData는 수정하지 않는다.)
        /// 업데이트된 노드 data
        /// </summary>
        /// <param name="_data">업데이트된 노드 data</param>
        public void UpdateDataDB(CommonData _data)
        {
            string _sqlQuery = "update " + tableName + " set modifyDate='" + _data.ModifyDate.ToString("yyyy-MM-dd HH:mm:ss") + "',pw='" +
                _data.PW + "',age=" + _data.Age + ",agechk=" + _data.AgeChk + ",tall=" + _data.Tall +
                ",sexType=" + Insert_DB_SEX(_data.SexType) + " WHERE id='" + _data.ID + "';";

            ProcessQuery(_sqlQuery);
        }
         
        /// <summary>
        /// DB의 행 데이터 수를 가져온다.
        /// </summary>
        /// <returns></returns>
        public int Get_DBRow_Count()
        {
            string _sqlQuery = "select count(*) from "+ tableName;
            MySqlCommand _cmd = new MySqlCommand(_sqlQuery, Connection);
            int _DataCount = Convert.ToInt32(_cmd.ExecuteScalar());
            return _DataCount;
        }

        /// <summary>
        /// DB SexType 속성값을 Commondata형식으로 변환
        /// DB SexType 데이터를 가져와서 열겨형식으로 변환 메소드
        /// </summary>
        /// <param name="_reader">DB의 행을 가져오는 MySqlDataReader클래스</param>
        /// <returns></returns>
        public SexTypes DB_sexType_change_CommondataType(MySqlDataReader _reader)
        {
            if ((bool)_reader["sexType"] == true)
            {
                return SexTypes.Male;
            }
            else
            {
                return SexTypes.FeMale;
            }
        }

        /// <summary>
        /// SexTypes의 데이터를 bool타입 DB[sexType]으로 변환
        /// 기능 : 남자 true , 여자 false
        /// </summary>
        /// <returns></returns>
        public bool Insert_DB_SEX(SexTypes _sexType)
        {
            if (_sexType == SexTypes.Male)
            {
                return true;
            }
            else
            {
                return false;
            }
        }  
    }
}
