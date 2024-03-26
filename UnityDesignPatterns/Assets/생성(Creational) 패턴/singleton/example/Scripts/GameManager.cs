using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NG.Patterns.Structure.Singleton
{
    //GameManagers, InputManagers and anything else you want to only exist once make great Singletons.
    public class GameManager : Singleton<GameManager>
    {
        public Text DebugText;

        private string _debugMessage = "GameManager Is Running: ";

        private int _numElipses = 0;
        public void Start()
        {
            //Begin the GameLoop Coroutine on start
            StartCoroutine(GameLoop());
        }

        IEnumerator GameLoop()
        {
            while(true)
            {
                DebugText.text = _debugMessage + GetElipses();
                yield return new WaitForSeconds(0.5f);
            }
        }

        private string GetElipses()
        {
            string text = "";
            for (int i = 0; i <= _numElipses; i++)
            {
                text += i.ToString();
            }
            _numElipses++;
            if (_numElipses > 5)
                Dispose();
                //_numElipses = 0;
            return text;
        }
    }
}