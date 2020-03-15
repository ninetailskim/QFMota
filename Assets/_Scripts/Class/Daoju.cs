using UnityEngine;
using QFramework;

namespace Mota
{
    public class Daoju : MonoBehaviour
    {
        //攻击，防御，生命，金币，等级，提示
        public int gongji;
        public int fangyu;
        public int shengming;
        public int jinbi;
        public int dengji;
        public string tip;

        public void Execute()
        {
            AudioManager.Instance.playAudio("daoju");
            PlayerInfo.Instance.Data.Attack.Value += gongji;
            PlayerInfo.Instance.Data.Defence.Value += fangyu;
            PlayerInfo.Instance.Data.Life.Value += shengming;
            PlayerInfo.Instance.Data.Gold.Value += jinbi;
            PlayerInfo.Instance.Data.Level.Value += dengji;
            PlayerInfo.Instance.Data.Attack.Value += dengji * 7;
            PlayerInfo.Instance.Data.Defence.Value += dengji * 7;
            PlayerInfo.Instance.Data.Life.Value += dengji * 600;

            DialogManager.Instance.tipContent = daoju.tip;
            DialogManager.Instance.tipTime = 3f;

            this.DestroyGameObj();
        }
    }
}