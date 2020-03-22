using UnityEngine;
using QFramework;
using Rotorz.Tile;

namespace Mota
{
    public class Guaiwu : MonoBehaviour,ICommand
    {
        public int gongji;
        public int fangyu;
        public int shengming;
        public int jingyan;
        public int jinbi;

        public void Execute(int x, int y, TileData otherTileData)
        {
            DialogManager.Instance.infoDabuguo = "你打不过他。\n\n";
            DialogManager.Instance.infoDabuguo += "怪物属性：\n";
            DialogManager.Instance.infoDabuguo += "生命：" + shengming + "\n";
            DialogManager.Instance.infoDabuguo += "攻击：" + gongji + "\n";
            DialogManager.Instance.infoDabuguo += "防御：" + fangyu + "\n";
            DialogManager.Instance.infoDabuguo += "金币：" + jinbi + "\n";
            DialogManager.Instance.infoDabuguo += "经验：" + jingyan + "\n";

            if (PlayerInfo.Instance.Data.Attack.Value <= fangyu)
            {
                DialogManager.Instance.state = "dabuguo";
            }
            else
            {
                int shanghai = PlayerInfo.Instance.Data.Attack.Value - fangyu;
                float cishu = Mathf.Ceil(shengming / shanghai);
                float zongshanghai = 0;
                if (gongji > PlayerInfo.Instance.Data.Defence.Value)
                {
                    float shoushang = gongji - PlayerInfo.Instance.Data.Defence.Value;
                    zongshanghai = shoushang * cishu;
                }
                if (zongshanghai >= PlayerInfo.Instance.Data.Life.Value)
                {
                    DialogManager.Instance.state = "dabuguo";
                }
                else
                {
                    AudioManager.Instance.playAudio("fight");
                    PlayerInfo.Instance.Data.Life.Value -= (int)zongshanghai;
                    PlayerInfo.Instance.Data.Experience.Value += jingyan;
                    PlayerInfo.Instance.Data.Gold.Value += jinbi;
                    DialogManager.Instance.tipContent = "经验+" + jingyan + "，金币+" + jinbi;
                    DialogManager.Instance.tipTime = 3f;
                    this.DestroyGameObj();
                    if (otherTileData.GetUserFlag(9))
                    {
                        Application.LoadLevel(2);
                    }
                    otherTileData.Clear();
                    GameDataManager.Instance.sceneData[GameManager.Instance.CurrentFloor.Value][x, y] = 1;
                }
            }
        }
    }
}