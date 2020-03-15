using UnityEngine;
using Rotorz.Tile;
using UniRx;
namespace Mota
{
    public class ActionManager : MonoBehaviour
    {
        void Start()
        {
            GameObject player = GameObject.Find("Player").gameObject;  
        }
        public void talk(int x, int y, TileData otherTileData)
        {
            AudioManager.Instance.playAudio("talk");
            Talk talk = otherTileData.gameObject.GetComponent<Talk>();
            Dialoguer.StartDialogue(talk.dialogureID);
        }
        public void daoju(int x, int y, TileData otherTileData)
        {
            
            otherTileData.gameObject.GetComponent<Daoju>()
                        .Execute();
            
            //GameObject.Destroy(otherTileData.gameObject.GetComponent<Daoju>().gameObject);
            otherTileData.Clear();
            GameDataManager.Instance.sceneData[GameManager.Instance.CurrentFloor.Value][x, y] = 1;
        }
        public void guaiwu(int x, int y, TileData otherTileData)
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
                    AudioManager.Instance.playAudio("fight");
                    PlayerInfo.Instance.Data.Life.Value -= (int)zongshanghai;
                    PlayerInfo.Instance.Data.Experience.Value += guaiwu.jingyan;
                    PlayerInfo.Instance.Data.Gold.Value += guaiwu.jinbi;
                    DialogManager.Instance.tipContent = "经验+" + guaiwu.jingyan + "，金币+" + guaiwu.jinbi;
                    DialogManager.Instance.tipTime = 3f;
                    GameObject.Destroy(guaiwu.gameObject);
                    if (otherTileData.GetUserFlag(9))
                    {
                        Application.LoadLevel(2);
                    }
                    otherTileData.Clear();
                    GameDataManager.Instance.sceneData[GameManager.Instance.CurrentFloor.Value][x, y] = 1;
                }
            }
        }
        public void door(int x, int y, TileData otherTileData)
        {
            TRAnimation door = otherTileData.gameObject.GetComponent<TRAnimation>();
            if (PlayerInfo.Instance.Data.KeyYellow.Value > 0 && door.currentIndex == 1)
            {
                AudioManager.Instance.playAudio("door");
                GameObject.Destroy(door.gameObject);
                otherTileData.Clear();
                GameDataManager.Instance.sceneData[GameManager.Instance.CurrentFloor.Value][x, y] = 1;
                PlayerInfo.Instance.Data.KeyYellow.Value -= 1;
                DialogManager.Instance.tipContent = "黄钥匙-1";
                DialogManager.Instance.tipTime = 3f;
            }
            if (PlayerInfo.Instance.Data.KeyBlue.Value > 0 && door.currentIndex == 2)
            {
                AudioManager.Instance.playAudio("door");
                GameObject.Destroy(door.gameObject);
                otherTileData.Clear();
                GameDataManager.Instance.sceneData[GameManager.Instance.CurrentFloor.Value][x, y] = 1;
                PlayerInfo.Instance.Data.KeyBlue.Value -= 1;
                DialogManager.Instance.tipContent = "蓝钥匙-1";
                DialogManager.Instance.tipTime = 3f;
            }
            if (PlayerInfo.Instance.Data.KeyRed.Value > 0 && door.currentIndex == 3)
            {
                AudioManager.Instance.playAudio("door");
                GameObject.Destroy(door.gameObject);
                otherTileData.Clear();
                GameDataManager.Instance.sceneData[GameManager.Instance.CurrentFloor.Value][x, y] = 1;
                PlayerInfo.Instance.Data.KeyRed.Value -= 1;
                DialogManager.Instance.tipContent = "红钥匙-1";
                DialogManager.Instance.tipTime = 3f;
            }
            if (door.spriteTexture.name == "door-02")
            {
                AudioManager.Instance.playAudio("door");
                GameObject.Destroy(door.gameObject);
                otherTileData.Clear();
                GameDataManager.Instance.sceneData[GameManager.Instance.CurrentFloor.Value][x, y] = 1;
            }
        }
        public void key(int x, int y, TileData otherTileData)
        {
            AudioManager.Instance.playAudio("daoju");
            Key key = otherTileData.gameObject.GetComponent<Key>();
            PlayerInfo.Instance.Data.KeyYellow.Value += key.key_yellow;
            PlayerInfo.Instance.Data.KeyBlue.Value += key.key_blue;
            PlayerInfo.Instance.Data.KeyRed.Value += key.key_red;
            DialogManager.Instance.tipContent = key.tip;
            DialogManager.Instance.tipTime = 3f;
            GameObject.Destroy(key.gameObject);
            otherTileData.Clear();
            GameDataManager.Instance.sceneData[GameManager.Instance.CurrentFloor.Value][x, y] = 1;
        }
        public void stair(int x, int y, TileData otherTileData)
        {
            AudioManager.Instance.playAudio("door");
            Stair stair = otherTileData.gameObject.GetComponent<Stair>();
            GameManager.Instance.changeFloor(stair.floor);
        }
        public void feixing(int x, int y, TileData otherTileData)
        {
            AudioManager.Instance.playAudio("daoju");
            DialogManager.Instance.tipContent = "开启“传送”，可传送到其他楼层";
            DialogManager.Instance.tipTime = 3f;
            PlayerInfo.Instance.Data.Fly.Value = true;
            GameObject.Destroy(otherTileData.gameObject);
            otherTileData.Clear();
            GameDataManager.Instance.sceneData[GameManager.Instance.CurrentFloor.Value][x, y] = 1;
        }
        public void tujian(int x, int y, TileData otherTileData)
        {
            AudioManager.Instance.playAudio("daoju");
            DialogManager.Instance.tipContent = "开启“图鉴”，开启后可点击怪物查看信息";
            DialogManager.Instance.tipTime = 3f;
            PlayerInfo.Instance.Data.HandBook.Value = true;
            GameObject.Destroy(otherTileData.gameObject);
            otherTileData.Clear();
            GameDataManager.Instance.sceneData[GameManager.Instance.CurrentFloor.Value][x, y] = 1;
        }
        public void boss(int x, int y, TileData otherTileData)
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