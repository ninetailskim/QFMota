using Rotorz.Tile;
using UnityEngine;
namespace Mota
{
    public class Talk : MonoBehaviour, ICommand
    {
        public int dialogureID = 0;

        public void Execute(int x, int y, TileData otherTileData)
        {
            AudioManager.Instance.playAudio("talk");
            //otherTileData.gameObject.GetComponent<Talk>();
            Dialoguer.StartDialogue(dialogureID);
        }
    }
}