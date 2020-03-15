using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UnityEngine.UI;
using UniRx;

namespace Mota
{

    public class PlayerInfo : QMonoBehaviour,ISingleton
    {
        public static PlayerInfo Instance
        {
            get
            {
                return MonoSingletonProperty<PlayerInfo>.Instance;
            }
        }

        public override IManager Manager
        {
            get
            {
                return UIManager.Instance;
            }
        }

        public void OnSingletonInit()
        {
            
        }

        public PlayerData Data = new PlayerData();

        public Text LevelText;
        public Text GoldText;
        public Text ExperienceText;
        public Text LifeText;
        public Text AttackText;
        public Text DefenceText;
        public Text FloorText;
        public Text KeyYellowText;
        public Text KeyBlueText;
        public Text KeyRedText;
        public Image HandbookImage;
        public Image FlyImage;
        public Image ArchivesImage;
        public Image AudioImage;

        public Button BtnHandbook;
        public Button BtnFly;                                        

        // Start is called before the first frame update

        // Update is called once per frame
        void Update()
        {

        }

        void Awake()
        {
            LevelText = transform.Find("label_dengji/Text").GetComponent<Text>();
            GoldText = transform.Find("label_jinbi/Text").GetComponent<Text>();
            ExperienceText = transform.Find("label_jingyan/Text").GetComponent<Text>();
            LifeText = transform.Find("label_shengming/Text").GetComponent<Text>();
            AttackText = transform.Find("label_gongji/Text").GetComponent<Text>();
            DefenceText = transform.Find("label_fangyu/Text").GetComponent<Text>();
            FloorText = transform.Find("label_floor").GetComponent<Text>();
            KeyYellowText = transform.Find("key_yellow/Text").GetComponent<Text>();
            KeyBlueText = transform.Find("key_blue/Text").GetComponent<Text>();
            KeyRedText = transform.Find("key_red/Text").GetComponent<Text>();
            HandbookImage = transform.Find("Button_tujian/Image").GetComponent<Image>();
            FlyImage = transform.Find("Button_feixing/Image").GetComponent<Image>(); 
            ArchivesImage = transform.Find("Button_dangan/Image").GetComponent<Image>(); 
            AudioImage = transform.Find("Button_shengyin/Image").GetComponent<Image>();

            BtnHandbook = transform.Find("Button_tujian").GetComponent<Button>();
            BtnFly = transform.Find("Button_feixing").GetComponent<Button>();

        }

        void Start()
        {
            Data.Level.SubscribeToText(LevelText);
            Data.Experience.SubscribeToText(ExperienceText);
            Data.Gold.SubscribeToText(GoldText);
            Data.Life.SubscribeToText(LifeText);
            Data.Attack.SubscribeToText(AttackText);
            Data.Defence.SubscribeToText(DefenceText);
            Data.KeyYellow.SubscribeToText(KeyYellowText);
            Data.KeyBlue.SubscribeToText(KeyBlueText);
            Data.KeyRed.SubscribeToText(KeyRedText);

            GameManager.Instance.CurrentFloor.Select(floor =>"第"+floor+"关").SubscribeToText(FloorText);

            Data.HandBook.Subscribe(on =>
            {
                BtnHandbook.enabled = on;
                HandbookImage.color = on ? new Vector4(1, 1, 1, 1) : new Vector4(0, 0, 0, 0.2f);
            });

            Data.Fly.Subscribe(on =>
            {
                BtnFly.enabled = on;
                FlyImage.color = on ? new Vector4(1, 1, 1, 1) : new Vector4(0, 0, 0, 0.2f);
            });
        }
    }
}