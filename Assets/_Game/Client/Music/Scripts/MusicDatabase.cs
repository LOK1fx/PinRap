using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOK1game.Tools;
using UnityEditor;

namespace LOK1game
{
    [CreateAssetMenu()]
    public class MusicDatabase : ScriptableObject
    {
        public List<MusicData> AllMusicData => _allMusicData;

        [SerializeField] private List<MusicData> _allMusicData;
    }
}