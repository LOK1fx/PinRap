using System;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game.UI
{
    [Obsolete]
    public class FarmCrystalScorePopups : MonoBehaviour
    {
        private List<PopupText2D> _popupTexts = new List<PopupText2D>();
        private List<Vector3> _popupTextsPositions = new List<Vector3>();
        
        private void LateUpdate()
        {
            if (_popupTexts.Count > 0)
            {
                var camera = Camera.main;

                for (int i = 0; i < _popupTexts.Count; i++)
                {
                    var position = _popupTextsPositions[i];

                    _popupTexts[i].SetPosition(camera.WorldToScreenPoint(position));
                }
            }
        }
    }
}