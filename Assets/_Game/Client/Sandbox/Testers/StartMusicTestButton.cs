using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOK1game.Tools;
using UnityEngine.UI;

namespace LOK1game.Test
{
    [RequireComponent(typeof(Button))]
    public class StartMusicTestButton : MonoBehaviour
    {
        [SerializeField] private MusicData _songToPlay;
        
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            if (_songToPlay == null)
                throw new NullReferenceException("SongToPlay is null!");
            
            MusicTimeline.Instance.StartPlayback(_songToPlay);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }
    }
}