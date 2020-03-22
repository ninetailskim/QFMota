using UnityEngine;
using Rotorz.Tile;
using QFramework;
namespace Mota
{
    public class TileManager : MonoBehaviour
    {
        public TileSystem tileSystem;
        public TileIndex playerTileIndex;
        public TileIndex otherTileIndex;
        public TileData otherTileData;

        //private otherCommand AM;
        private GameManager GM;
        public Transform playerTransform;

        void Start()
        {
            GM = this.GetComponent<GameManager>();
            tileSystem = GameObject.Find("Floor" + GM.CurrentFloor.Value).gameObject.GetComponent<TileSystem>();
        }

        public bool checkTile(string arrow)
        {
            playerTileIndex = tileSystem.ClosestTileIndexFromWorld(playerTransform.position);
            int x = playerTileIndex.row;
            int y = playerTileIndex.column;
            switch (arrow)
            {
                case "up":
                    x -= 1;
                    break;
                case "down":
                    x += 1;
                    break;
                case "left":
                    y -= 1;
                    break;
                case "right":
                    y += 1;
                    break;
                default:
                    break;
            }
            if (x < 0 || y < 0 || x > 10 || y > 10)
            {
                return false;
            }
            otherTileData = tileSystem.GetTile(x, y);
            if (otherTileData != null)
            {
                if (otherTileData.GetUserFlag(1))
                {
                    //AM.talk(x, y, otherTileData);
                    otherTileData.gameObject.GetComponent<Talk>().Execute(x, y, otherTileData);
                    return false;
                }
                if (otherTileData.GetUserFlag(2))
                {
                    otherTileData.gameObject.GetComponent<Daoju>().Execute(x, y, otherTileData);
                    //AM.daoju(x, y, otherTileData);
                    return true;
                }
                if (otherTileData.GetUserFlag(3))
                {
                    otherTileData.gameObject.GetComponent<Guaiwu>().Execute(x, y, otherTileData);
                    //AM.guaiwu();
                    return false;
                }
                if (otherTileData.GetUserFlag(4))
                {
                    //AM.door(x, y, otherTileData);
                    otherTileData.gameObject.GetComponent<TRAnimation>().Execute(x, y, otherTileData);
                    return false;
                }
                if (otherTileData.GetUserFlag(5))
                {
                    otherTileData.gameObject.GetComponent<Key>().Execute(x, y, otherTileData);
                    //AM.key(x, y, otherTileData);
                    return true;
                }
                if (otherTileData.GetUserFlag(6))
                {
                    //AM.stair(x, y, otherTileData);
                    otherTileData.gameObject.GetComponent<Stair>().Execute(x, y, otherTileData);
                    return false;
                }
                if (otherTileData.GetUserFlag(7))
                {
                    otherCommand.tujian(x, y, otherTileData);
                    return true;
                }
                if (otherTileData.GetUserFlag(8))
                {
                    otherCommand.feixing(x, y, otherTileData);
                    return true;
                }
                if (otherTileData.GetUserFlag(9))
                {
                    otherCommand.boss(x, y, otherTileData);
                    return true;
                }
                if (otherTileData.SolidFlag)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return true;
            }
        }
    }

}