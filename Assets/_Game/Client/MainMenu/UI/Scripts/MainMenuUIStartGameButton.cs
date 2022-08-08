using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOK1game.Tools;
using UnityEngine.UI;

namespace LOK1game.PinRap.MainMenu
{
    [RequireComponent(typeof(Button))]
    public class MainMenuUIStartGameButton : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            MainMenu.Instance.LoadToGame();
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }
    }
}