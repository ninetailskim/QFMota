//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QFramework.Example
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;
    
    
    public class UIHomePanelData : QFramework.UIPanelData
    {
    }
    
    public partial class UIHomePanel : QFramework.UIPanel
    {
        
        protected override void ProcessMsg(int eventId, QFramework.QMsg msg)
        {
            throw new System.NotImplementedException ();
        }
        
        protected override void OnInit(QFramework.IUIData uiData)
        {
            mData = uiData as UIHomePanelData ?? new UIHomePanelData();
            // please add init code here
        }
        
        protected override void OnOpen(QFramework.IUIData uiData)
        {
        }
        
        protected override void OnShow()
        {
        }
        
        protected override void OnHide()
        {
        }
        
        protected override void OnClose()
        {
        }

        protected override void RegisterUIEvent()
        {
            BtnStartGame.onClick.AddListener(()=> {
                //Application.LoadLevel(1);
                SceneManager.LoadScene(1);
            });

            BtnContinue.onClick.AddListener(() =>
            {
                PlayerPrefs.SetInt("loadgame", 1);
                SceneManager.LoadScene(1);
            });

            BtnQuit.onClick.AddListener(()=> {
                Application.Quit();
            });
        }
    }
}
