﻿using UnityEngine;
using Rotorz.Tile;
using QFramework;
using UniRx;


namespace Mota
{
    public class GameManager : MonoBehaviour, ISingleton
    {

        public static GameManager Instance {
            get {
                return MonoSingletonProperty<GameManager>.Instance;
            }
        }

        public IntReactiveProperty CurrentFloor = new IntReactiveProperty(0);
        public IntReactiveProperty MaxFloor = new IntReactiveProperty(0);
        public GameObject globalGameObject;
        public GameObject[] floorGO;

        private TileManager TM;
        private DialogManager DM;
        private GameDataManager GDM;
        private Transform playerTransform;

        void Start()
        {
            GameObject player = GameObject.Find("Player").gameObject;
            TM = this.GetComponent<TileManager>();
            DM = this.GetComponent<DialogManager>();
            GDM = this.GetComponent<GameDataManager>();
            playerTransform = player.transform;
        }

        public void changeFloor(int floor, bool checkUpDown = true)
        {
            if (CurrentFloor.Value != floor)
            {
                floorGO[floor].SetActive(true);
                //摄像机位置
                Vector3 gGOPosition = globalGameObject.transform.position;
                gGOPosition.x = 5 + (floor * (11 + 1));
                globalGameObject.transform.position = gGOPosition;
                if (checkUpDown)
                {
                    if (CurrentFloor.Value < floor)
                    {
                        playerTransform.position = PlayerInfo.Instance.Data.GetPlayerPositionUp(floor);
                    }
                    else
                    {
                        playerTransform.position = PlayerInfo.Instance.Data.GetPlayerPositionDown(floor);
                    }
                }
                else
                {
                    playerTransform.position = PlayerInfo.Instance.Data.GetPlayerPositionUp(floor);
                }
                floorGO[CurrentFloor.Value].SetActive(false);
                CurrentFloor.Value = floor;
                if (CurrentFloor.Value > MaxFloor.Value)
                {
                    MaxFloor.Value = CurrentFloor.Value;
                }
                TM.tileSystem = floorGO[CurrentFloor.Value].GetComponent<TileSystem>();
                DM.dialogTime = 3;
                DM.state = "floorchange";
            }
        }
        public bool clear2Door()
        {
            try
            {
                floorGO[2].SetActive(true);
                TileSystem ts_object = floorGO[2].GetComponent<TileSystem>();
                GameObject door = GameObject.Find("Floor2/chunk_0_0/door-01_3").gameObject;
                TileData tile = ts_object.GetTile(6, 1);
                GameObject.Destroy(door.gameObject);
                tile.Clear();
                GDM.sceneData[2][6, 1] = 1;
                floorGO[2].SetActive(false);
                return true;
            }
            catch
            {
                return false;
            }
        }
        void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                DM.state = "menu";
            }
            checkFix();
        }
        void checkFix()
        {
            if (Dialoguer.GetGlobalBoolean(1) && GDM.sceneData[2][6, 1] == 0)
            {
                clear2Door();
                DM.tipContent = "已帮你打开2楼的门，修正错误，非常抱歉！";
                DM.tipTime = 6;
            }
        }

        public void OnSingletonInit()
        {
            
        }
    }
}