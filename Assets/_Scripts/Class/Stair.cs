using Rotorz.Tile;
using UnityEngine;
namespace Mota
{
    public class Stair : MonoBehaviour,ICommand
    {
        public int floor;

        public void Execute(int x, int y, TileData otherTileData)
        {
            AudioManager.Instance.playAudio("door");
            //Stair stair = otherTileData.gameObject.GetComponent<Stair>();
            GameManager.Instance.changeFloor(floor);
        }
    }
}