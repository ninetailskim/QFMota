using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace Mota
{
    public class GameModule : MonoBehaviour
    {

        private void Awake()
        {
            ResMgr.Init();

            UIMgr.SetResolution(352, 636, 0);

            UIMgr.OpenPanel<UIGamePanel>();
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}