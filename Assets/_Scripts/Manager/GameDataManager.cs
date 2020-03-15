using UnityEngine;
using Rotorz.Tile;
using QFramework;

namespace Mota
{
    public class GameDataManager : MonoBehaviour, ISingleton
    {
        public static GameDataManager Instance {
            get
            {
                return MonoSingletonProperty<GameDataManager>.Instance;
            }
        }
        public int[][,] sceneData = new int[22][,];

        private GameManager GM;
        private DialogManager DM;
        public bool hasCundang = false;

        void initComponent()
        {
            GameObject player = GameObject.Find("Player").gameObject;
            GM = this.GetComponent<GameManager>();
            DM = this.GetComponent<DialogManager>();
        }

        void Start()
        {
            for (int i = 0; i < sceneData.Length; i++)
            {
                if (sceneData[i] == null)
                {
                    sceneData[i] = new int[11, 11];
                }
            }
        }

        void Awake()
        {
            initComponent();
            Dialoguer.Initialize();
            if (PlayerPrefs.HasKey("loadgame"))
            {
                PlayerPrefs.DeleteKey("loadgame");
                if (checkGameData())
                {
                    LoadGame();
                }
                else
                {
                    DM.tipContent = "当前没有存档，无法读取。";
                    DM.tipTime = 3f;
                }
            }
        }

        public void SaveGame()
        {
            using (ES2Writer writer = ES2Writer.Create("player"))
            {
                writer.Write(PlayerInfo.Instance.Data.Level, "dengji");
                writer.Write(PlayerInfo.Instance.Data.Gold, "jinbi");
                writer.Write(PlayerInfo.Instance.Data.Experience, "jingyan");
                writer.Write(PlayerInfo.Instance.Data.Life, "shengming");
                writer.Write(PlayerInfo.Instance.Data.Attack, "gongji");
                writer.Write(PlayerInfo.Instance.Data.Defence, "fangyu");
                writer.Write(PlayerInfo.Instance.Data.KeyYellow, "key_yellow");
                writer.Write(PlayerInfo.Instance.Data.KeyBlue, "key_blue");
                writer.Write(PlayerInfo.Instance.Data.KeyRed, "key_red");
                writer.Write(PlayerInfo.Instance.Data.HandBook, "daoju_tujian");
                writer.Write(PlayerInfo.Instance.Data.Fly, "daoju_feixing");
                writer.Write(GM.MaxFloor.Value, "maxFloor");
                writer.Write(Dialoguer.GetGlobalBoolean(0), "hasJinglingTalked");
                writer.Write(Dialoguer.GetGlobalBoolean(1), "hasDaozeiTalked");
                writer.Write(Dialoguer.GetGlobalBoolean(2), "hasGoodJian");
                writer.Write(Dialoguer.GetGlobalBoolean(3), "hasGoodDun");
                writer.Write(Dialoguer.GetGlobalBoolean(4), "hasGangJian");
                writer.Write(Dialoguer.GetGlobalBoolean(5), "hasGangDun");
                writer.Save();
            }
#if UNITY_WP8
        for (int i = 0; i < GM.MaxFloor + 1; i++)
        {
            using (ES2Writer writer = ES2Writer.Create("floor" + i))
            {
                for (int x = 0; x < 11; x++)
                {
                    for (int y = 0; y < 11; y++)
                    {
                        writer.Write(sceneData[i][x, y], x + "v" + y);
                    }
                }
                writer.Save();
            }
        }
#else
            for (int i = 0; i < GM.MaxFloor.Value + 1; i++)
            {
                ES2.Save(sceneData[i], "floor" + i);
            }
#endif
            DM.state = "";
            DM.tipContent = "保存成功";
            DM.tipTime = 3;
        }

        public void LoadGame()
        {
            using (ES2Reader reader = ES2Reader.Create("player"))
            {
                PlayerInfo.Instance.Data.Level.Value = reader.Read<int>("dengji");
                PlayerInfo.Instance.Data.Gold.Value = reader.Read<int>("jinbi");
                PlayerInfo.Instance.Data.Experience.Value = reader.Read<int>("jingyan");
                PlayerInfo.Instance.Data.Life.Value = reader.Read<int>("shengming");
                PlayerInfo.Instance.Data.Attack.Value = reader.Read<int>("gongji");
                PlayerInfo.Instance.Data.Defence.Value = reader.Read<int>("fangyu");
                PlayerInfo.Instance.Data.KeyYellow.Value = reader.Read<int>("key_yellow");
                PlayerInfo.Instance.Data.KeyBlue.Value = reader.Read<int>("key_blue");
                PlayerInfo.Instance.Data.KeyRed.Value = reader.Read<int>("key_red");
                PlayerInfo.Instance.Data.HandBook.Value = reader.Read<bool>("daoju_tujian");
                PlayerInfo.Instance.Data.Fly.Value = reader.Read<bool>("daoju_feixing");
                GM.MaxFloor.Value = reader.Read<int>("maxFloor");
                Dialoguer.SetGlobalBoolean(0, reader.Read<bool>("hasJinglingTalked"));
                Dialoguer.SetGlobalBoolean(1, reader.Read<bool>("hasDaozeiTalked"));
                Dialoguer.SetGlobalBoolean(2, reader.Read<bool>("hasGoodJian"));
                Dialoguer.SetGlobalBoolean(3, reader.Read<bool>("hasGoodDun"));
                Dialoguer.SetGlobalBoolean(4, reader.Read<bool>("hasGangJian"));
                Dialoguer.SetGlobalBoolean(5, reader.Read<bool>("hasGangDun"));
            }
#if UNITY_WP8
        for (int i = 0; i < GM.MaxFloor + 1; i++)
        {
            GM.floorGO[i].SetActive(true);
            using (ES2Reader reader = ES2Reader.Create("floor" + i))
            {
                TileSystem ts_object = GameObject.Find("Floor" + i).gameObject.GetComponent<TileSystem>();
                for (int x = 0; x < 11; x++)
                {
                    for (int y = 0; y < 11; y++)
                    {
                        int hasTile = reader.Read<int>(x + "v" + y);
                        if (sceneData[i] == null)
                        {
                            sceneData[i] = new int[11, 11];
                        }
                        sceneData[i][x, y] = hasTile;
                        if (sceneData[i][x, y] != null && sceneData[i][x, y] == 1)
                        {
                            TileData tile = ts_object.GetTile(x, y);
                            if (tile != null)
                            {
                                GameObject.Destroy(tile.gameObject);
                                tile.Clear();
                            }
                        }
                    }
                }
            }
            GM.floorGO[i].SetActive(false);
        }
#else
            for (int i = 0; i < GM.MaxFloor.Value + 1; i++)
            {
                GM.floorGO[i].SetActive(true);
                sceneData[i] = ES2.Load2DArray<int>("floor" + i);
                TileSystem ts_object = GameObject.Find("Floor" + i).gameObject.GetComponent<TileSystem>();
                for (int x = 0; x < 11; x++)
                {
                    for (int y = 0; y < 11; y++)
                    {
                        if (sceneData[i][x, y] == 1)
                        {
                            TileData tile = ts_object.GetTile(x, y);
                            if (tile != null)
                            {
                                GameObject.Destroy(tile.gameObject);
                                tile.Clear();
                            }
                        }
                    }
                }
                GM.floorGO[i].SetActive(false);
            }
#endif
            GM.floorGO[0].SetActive(true);
            DM.state = "";
            PlayerPrefs.DeleteKey("loadgame");
            DM.tipContent = "读取成功";
            DM.tipTime = 3;
        }

        public bool checkGameData()
        {
            if (ES2.Exists("player"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void OnSingletonInit()
        {
            
        }
    }
}