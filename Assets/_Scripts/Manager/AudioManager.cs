using UnityEngine;
using UnityEngine.UI;
using QFramework;
namespace Mota
{
    public class AudioManager : MonoBehaviour,ISingleton
    {
        public static AudioManager Instance
        {
            get
            {
                return MonoSingletonProperty<AudioManager>.Instance;
            }
        }

        public Sprite sound_on;
        public Sprite sound_off;

        public AudioClip daoju;
        public AudioClip door;
        public AudioClip feixing;
        public AudioClip fight;
        public AudioClip shangdian;
        public AudioClip talk;

        public AudioSource gameAudio;
        public AudioSource playerAudio;

        public void playAudio(string audioname)
        {
            switch (audioname)
            {
                case "daoju":
                    playerAudio.clip = daoju;
                    break;
                case "door":
                    playerAudio.clip = door;
                    break;
                case "feixing":
                    playerAudio.clip = feixing;
                    break;
                case "fight":
                    playerAudio.clip = fight;
                    break;
                case "shangdian":
                    playerAudio.clip = shangdian;
                    break;
                case "talk":
                    playerAudio.clip = talk;
                    break;
                default:
                    playerAudio.clip = null;
                    break;
            }
            playerAudio.Stop();
            playerAudio.Play();
        }

        public void voice()
        {
            GameObject player = GameObject.Find("Player").gameObject;
            playerAudio = player.GetComponent<AudioSource>();
            if (gameAudio.mute == true)
            {
                gameAudio.mute = false;
                playerAudio.mute = false;
                PlayerInfo.Instance.AudioImage.sprite = sound_on;
            }
            else
            {
                gameAudio.mute = true;
                playerAudio.mute = true;
                PlayerInfo.Instance.AudioImage.sprite = sound_off;
            }
        }

        public void OnSingletonInit()
        {
            
        }
    }
}