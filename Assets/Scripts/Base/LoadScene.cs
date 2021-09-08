using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base
{
    public class LoadScene : SingletonGen<LoadScene>
    {
        [SerializeField]
        private string sceneName = "MenuScene";

        [SerializeField]
        private bool waitFullLoad = false;
        [SerializeField]
        private int waitForSecond = 5;

        private void Start()
        {
            Load();
        }


        public void Load()
        {
            if(Application.internetReachability == NetworkReachability.NotReachable)
            {
                ShowConnectionErrorPopup();
                return;
            }

            if(waitFullLoad && !LoadComplete())
            {
                StartCoroutine(WaitLoad());
            }
            else
            {
                FinishLoad();
            }
        }


        private void ShowConnectionErrorPopup()
        {
            
        }

        private void FinishLoad()
        {
            SceneLoader.E_LoadScene -= OnLoadSceneComplete;
            SceneLoader.E_LoadScene += OnLoadSceneComplete;
            SceneLoader.LoadScene(sceneName);
        }

        private bool LoadComplete()
        {
            return true;
        }

        private void OnLoadSceneComplete()
        {
            Game.Pool.hudManager.ShowWidget(HUD.EWidgetType.MAIN);
        }


        private IEnumerator WaitLoad()
        {
            bool show = true;

            for(int i = 0; i < waitForSecond; ++i)
            {
                yield return new WaitForSeconds(1);
                if(LoadComplete())
                {
                    FinishLoad();
                    show = false;
                    break;
                }
            }
            if(show)
                ShowConnectionErrorPopup();
        }
    }
}
