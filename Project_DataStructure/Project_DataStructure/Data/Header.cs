using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DataStructure.Data
{
    public enum DataType : byte
    {
        SEND_DATA = 0, // 전송
        RESP_DATA_SUCCESS,  // 응답 성공
        RESP_DATA_FAIL,  // 응답 실패
    }

    public enum FunctionType : byte
    {
        SYNC_SEND = 0, // 동기화
        SAVE_DATA, // 저장
        UPDATE_DATA, // 수정
        DELETE_DATA // 삭제
    }

    class Header
    {
        public byte dataType = 0;

        public byte functionType = 0;

        public int sync_cnt = 0;

        //DATAMAX
        public int bodyLen = 0;

        public Header()
        {

        }

        public Header(byte _dataType, byte _functionType, int _sync_cnt, int _bodyLen)
        {
            this.dataType = _dataType;
            this.functionType = _functionType;
            this.sync_cnt = _sync_cnt;
            this.bodyLen = _bodyLen;
        }
    }
}
