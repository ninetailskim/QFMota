using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UniRx;
using QFramework;

namespace Mota
{
    [System.Serializable]
    public class PlayerData
    {

        //private PlayerData() { }
        //玩家属性
        public IntReactiveProperty Level = new IntReactiveProperty(1);
        public IntReactiveProperty Experience = new IntReactiveProperty(0);
        public IntReactiveProperty Gold = new IntReactiveProperty(0);
        public IntReactiveProperty Life = new IntReactiveProperty(1000);
        public IntReactiveProperty Attack = new IntReactiveProperty(10);
        public IntReactiveProperty Defence = new IntReactiveProperty(10);
        public IntReactiveProperty KeyYellow = new IntReactiveProperty(0);
        public IntReactiveProperty KeyBlue = new IntReactiveProperty(0);
        public IntReactiveProperty KeyRed = new IntReactiveProperty(0);
        public BoolReactiveProperty HandBook = new BoolReactiveProperty(false);
        public BoolReactiveProperty Fly = new BoolReactiveProperty(false);

        public void LevelUp(int levels)
        {
            PlayerInfo.Instance.Data.Level.Value += levels;
            PlayerInfo.Instance.Data.Attack.Value += levels * 5;
            PlayerInfo.Instance.Data.Defence.Value += levels * 5;
            PlayerInfo.Instance.Data.Life.Value += levels * 600;
        }


        // Update is called once per frame
        void Update()
        {
            //PlayerInfo.Instance.FloorText.text = "第  " + GM.CurrentFloor.Value + "  层";
            
        }
        public Vector3 GetPlayerPositionUp(int floor)
        {
            switch (floor)
            {
                case 0:
                    return new Vector3(5.5f + floor * 12, -10.5f, 0);
                case 1:
                    return new Vector3(5.5f + floor * 12, -9.5f, 0);
                case 2:
                    return new Vector3(0.5f + floor * 12, -1.5f, 0);
                case 3:
                    return new Vector3(1.5f + floor * 12, -10.5f, 0);
                case 4:
                    return new Vector3(10.5f + floor * 12, -9.5f, 0);
                case 5:
                    return new Vector3(0.5f + floor * 12, -9.5f, 0);
                case 6:
                    return new Vector3(8.5f + floor * 12, -10.5f, 0);
                case 7:
                    return new Vector3(5.5f + floor * 12, -10.5f, 0);
                case 8:
                    return new Vector3(0.5f + floor * 12, -1.5f, 0);
                case 9:
                    return new Vector3(6.5f + floor * 12, -3.5f, 0);
                case 10:
                    return new Vector3(4.5f + floor * 12, -6.5f, 0);
                case 11:
                    return new Vector3(1.5f + floor * 12, -10.5f, 0);
                case 12:
                    return new Vector3(9.5f + floor * 12, -10.5f, 0);
                case 13:
                    return new Vector3(1.5f + floor * 12, -10.5f, 0);
                case 14:
                    return new Vector3(5.5f + floor * 12, -9.5f, 0);
                case 15:
                    return new Vector3(3.5f + floor * 12, -0.5f, 0);
                case 16:
                    return new Vector3(5.5f + floor * 12, -0.5f, 0);
                case 17:
                    return new Vector3(5.5f + floor * 12, -8.5f, 0);
                case 18:
                    return new Vector3(1.5f + floor * 12, -10.5f, 0);
                case 19:
                    return new Vector3(9.5f + floor * 12, -10.5f, 0);
                case 20:
                    return new Vector3(5.5f + floor * 12, -4.5f, 0);
                case 21:
                    return new Vector3(5.5f + floor * 12, -5.5f, 0);
                default:
                    return Vector3.zero;
            }
        }
        public Vector3 GetPlayerPositionDown(int floor)
        {
            switch (floor)
            {
                case 0:
                    return new Vector3(5.5f + floor * 12, -1.5f, 0);
                case 1:
                    return new Vector3(1.5f + floor * 12, -0.5f, 0);
                case 2:
                    return new Vector3(0.5f + floor * 12, -9.5f, 0);
                case 3:
                    return new Vector3(10.5f + floor * 12, -9.5f, 0);
                case 4:
                    return new Vector3(0.5f + floor * 12, -9.5f, 0);
                case 5:
                    return new Vector3(9.5f + floor * 12, -9.5f, 0);
                case 6:
                    return new Vector3(5.5f + floor * 12, -10.5f, 0);
                case 7:
                    return new Vector3(1.5f + floor * 12, -0.5f, 0);
                case 8:
                    return new Vector3(7.5f + floor * 12, -4.5f, 0);
                case 9:
                    return new Vector3(6.5f + floor * 12, -7.5f, 0);
                case 10:
                    return new Vector3(0.5f + floor * 12, -9.5f, 0);
                case 11:
                    return new Vector3(9.5f + floor * 12, -10.5f, 0);
                case 12:
                    return new Vector3(1.5f + floor * 12, -10.5f, 0);
                case 13:
                    return new Vector3(4.5f + floor * 12, -10.5f, 0);
                case 14:
                    return new Vector3(5.5f + floor * 12, -0.5f, 0);
                case 15:
                    return new Vector3(7.5f + floor * 12, -0.5f, 0);
                case 16:
                    return new Vector3(5.5f + floor * 12, -6.5f, 0);
                case 17:
                    return new Vector3(1.5f + floor * 12, -10.5f, 0);
                case 18:
                    return new Vector3(9.5f + floor * 12, -10.5f, 0);
                case 19:
                    return new Vector3(5.5f + floor * 12, -4.5f, 0);
                case 20:
                    return new Vector3(5.5f + floor * 12, -6.5f, 0);
                case 21:
                    return new Vector3(5.5f + floor * 12, -5.5f, 0);
                default:
                    return Vector3.zero;
            }
        }

        public void OnSingletonInit()
        {
            
        }
    }
}