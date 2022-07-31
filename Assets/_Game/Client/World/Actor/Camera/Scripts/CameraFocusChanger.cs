using System.Collections.Generic;
using UnityEngine;

namespace LOK1game
{
    [RequireComponent(typeof(CameraSetManager))]
    public class CameraFocusChanger : MonoBehaviour
    {
        private CameraSetManager _manager;
        private MusicTimeline _timeline;
        private MusicData _musicData;

        private List<CameraChangeEvent> _markersInstance = new List<CameraChangeEvent>();

        private float _currentSecond;
        private int _position;

        private void Awake()
        {
            _manager = GetComponent<CameraSetManager>();
        }

        private void Start()
        {
            _timeline = MusicTimeline.Instance;
            _timeline.OnMusicStart += OnMusicStarted;
            _timeline.OnMusicEnd += OnMusicEnded;
        }

        private void Update()
        {
            if(_timeline.IsPlaying == false) { return; }
            
            if (_position < _markersInstance.Count)
            {
                var evt = _markersInstance[_position];

                if (IsEventInRange(evt))
                {
                    SetFocus(evt.Focus);

                    evt.IsPlayed = true;

                    _position++;
                }
            }
            
            _currentSecond += Time.deltaTime;
        }

        private void SetFocus(CameraChangeEvent.ECharacterCameraFocus focus)
        {
            switch (focus)
            {
                case CameraChangeEvent.ECharacterCameraFocus.Center:
                    _manager.SetFocusOnCenter();
                    break;
                case CameraChangeEvent.ECharacterCameraFocus.Main:
                    _manager.SetFocusOnMain();
                    break;
                case CameraChangeEvent.ECharacterCameraFocus.Left:
                    _manager.SetFocusOnPlayer();
                    break;
                case CameraChangeEvent.ECharacterCameraFocus.Right:
                    _manager.SetFocusOnEnemy();
                    break;
                default:
                    _manager.SetFocusOnMain();
                    break;
            }
        }

        private void OnMusicStarted()
        {
            _musicData = _timeline.MusicDataInstance;
            _markersInstance = _musicData.CameraChangeFocusMarkers;

            foreach (var marker in _markersInstance)
            {
                marker.IsPlayed = false;
            }
        }
        
        private void OnMusicEnded()
        {
            _musicData = null;
            _currentSecond = 0;
            _markersInstance = null;
        }
        
        private bool IsEventInRange(CameraChangeEvent evt)
        {
            if (evt.StartSecond >= _currentSecond && evt.StartSecond <= _currentSecond + _musicData.SecondError && !evt.IsPlayed)
                return true;

            return false;
        }

        private void OnDestroy()
        {
            _timeline.OnMusicStart -= OnMusicStarted;
            _timeline.OnMusicEnd -= OnMusicEnded;
        }
    }
}