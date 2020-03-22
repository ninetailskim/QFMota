using QFramework;
using Rotorz.Tile;
using UnityEngine;


namespace Mota
{
    public class Key : MonoBehaviour, ICommand
    {
        public int key_yellow;
        public int key_blue;
        public int key_red;
        public string tip;

        public void Execute(int x, int y, TileData otherTileData)
        {
            AudioManager.Instance.playAudio("daoju");
            PlayerInfo.Instance.Data.KeyYellow.Value += key_yellow;
            PlayerInfo.Instance.Data.KeyBlue.Value += key_blue;
            PlayerInfo.Instance.Data.KeyRed.Value += key_red;
            DialogManager.Instance.tipContent = tip;
            DialogManager.Instance.tipTime = 3f;
            this.DestroyGameObj();

            otherTileData.Clear();
            GameDataManager.Instance.sceneData[GameManager.Instance.CurrentFloor.Value][x, y] = 1;
        }
    }
}