/****************************************************************************
* ScriptType: 主框架
* 请勿修改!!!
****************************************************************************/

namespace FutureCore
{
    /// <summary>
    /// 通用消息数据
    /// </summary>
    public class CommonMsgData
    {
        public string msg;
        public int int1Value;
        public float float1Value;
        public string string1Value;
        public int int2Value;
        public float float2Value;
        public string string2Value;

        public CommonMsgData()
        {
        }

        public CommonMsgData(string msg)
        {
            this.msg = msg;
        }

        public CommonMsgData(string msg, int int1Value)
        {
            this.msg = msg;
            this.int1Value = int1Value;
        }

        public CommonMsgData(string msg, float float1Value)
        {
            this.msg = msg;
            this.float1Value = float1Value;
        }

        public CommonMsgData(string msg, string string1Value)
        {
            this.msg = msg;
            this.string1Value = string1Value;
        }

        public CommonMsgData Set(int int1Value)
        {
            this.int1Value = int1Value;
            return this;
        }

        public CommonMsgData Set(float float1Value)
        {
            this.float1Value = float1Value;
            return this;
        }

        public CommonMsgData Set(string string1Value)
        {
            this.string1Value = string1Value;
            return this;
        }

        public CommonMsgData Set2(int int2Value)
        {
            this.int2Value = int2Value;
            return this;
        }

        public CommonMsgData Set2(float float2Value)
        {
            this.float2Value = float2Value;
            return this;
        }

        public CommonMsgData Set2(string string2Value)
        {
            this.string2Value = string2Value;
            return this;
        }
    }
}