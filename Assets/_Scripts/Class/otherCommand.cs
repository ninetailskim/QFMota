using UnityEngine;
using Rotorz.Tile;
using UniRx;
using QFramework;

namespace Mota
{
    public static class otherCommand
    {
        public static void feixing(int x, int y, TileData otherTileData)
        {
            AudioManager.Instance.playAudio("daoju");
            DialogManager.Instance.tipContent = "开启“传送”，可传送到其他楼层";
            DialogManager.Instance.tipTime = 3f;
            PlayerInfo.Instance.Data.Fly.Value = true;
            GameObject.Destroy(otherTileData.gameObject);
            otherTileData.Clear();
            GameDataManager.Instance.sceneData[GameManager.Instance.CurrentFloor.Value][x, y] = 1;
        }
        public static void tujian(int x, int y, TileData otherTileData)
        {
            AudioManager.Instance.playAudio("daoju");
            DialogManager.Instance.tipContent = "开启“图鉴”，开启后可点击怪物查看信息";
            DialogManager.Instance.tipTime = 3f;
            PlayerInfo.Instance.Data.HandBook.Value = true;
            GameObject.Destroy(otherTileData.gameObject);
            otherTileData.Clear();
            GameDataManager.Instance.sceneData[GameManager.Instance.CurrentFloor.Value][x, y] = 1;
        }
        public static void boss(int x, int y, TileData otherTileData)
        {
            Guaiwu guaiwu = otherTileData.gameObject.GetComponent<Guaiwu>();
            DialogManager.Instance.infoDabuguo = "你打不过他。\n\n";
            DialogManager.Instance.infoDabuguo += "怪物属性：\n";
            DialogManager.Instance.infoDabuguo += "生命：" + guaiwu.shengming + "\n";
            DialogManager.Instance.infoDabuguo += "攻击：" + guaiwu.gongji + "\n";
            DialogManager.Instance.infoDabuguo += "防御：" + guaiwu.fangyu + "\n";
            DialogManager.Instance.infoDabuguo += "金币：" + guaiwu.jinbi + "\n";
            DialogManager.Instance.infoDabuguo += "经验：" + guaiwu.jingyan + "\n";
            if (PlayerInfo.Instance.Data.Attack.Value <= guaiwu.fangyu)
            {
                DialogManager.Instance.state = "dabuguo";
            }
            else
            {
                int shanghai = PlayerInfo.Instance.Data.Attack.Value - guaiwu.fangyu;
                float cishu = Mathf.Ceil(guaiwu.shengming / shanghai);
                float zongshanghai = 0;
                if (guaiwu.gongji > PlayerInfo.Instance.Data.Defence.Value)
                {
                    float shoushang = guaiwu.gongji - PlayerInfo.Instance.Data.Defence.Value;
                    zongshanghai = shoushang * cishu;
                }
                if (zongshanghai >= PlayerInfo.Instance.Data.Life.Value)
                {
                    DialogManager.Instance.state = "dabuguo";
                }
                else
                {
                    Application.LoadLevel(2);
                }
            }
        }
    }

}