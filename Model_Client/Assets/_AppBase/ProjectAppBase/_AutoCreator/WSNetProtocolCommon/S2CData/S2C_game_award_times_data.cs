namespace ProjectApp.Protocol
{
    public class S2C_game_award_times_data
    {
        public string game;
        
        /// <summary>
        /// (单倍)积分
        /// </summary>
        public int credit;
        /// <summary>
        /// 倍率
        ///  1:单倍, 表示加倍奖励失败
        ///  2:双倍
        /// </summary>
        public int times;
    }
}
