using ProjectApp.Data;

namespace ProjectApp.Protocol
{
    public class FBLogin_Data
    {
        public string uid;

        public string invite_code;
        
        public int login_days;

        /// <summary>
        /// 	注册时间(timestamp)
        /// </summary>
        /// <returns></returns>
        public int reg_time;

        /// <summary>
        /// 基本信息
        /// </summary>
        /// <returns></returns>
        public object basic;

        /// <summary>
        ///  个人信息 即S2C_info(personal)
        /// </summary>
        /// <param name="???"></param>
        public object personal;

        /// <summary>
        ///  即S2C_info(preferences)
        /// </summary>
        /// <returns></returns>
         public Preferences preferences; 


    }
}