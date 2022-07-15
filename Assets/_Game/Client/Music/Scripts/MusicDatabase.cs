using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOK1game.Tools;

namespace LOK1game
{
    public class MusicDatabase : ScriptableObject
    {
        public List<MusicData> MusicDatas => _musicDatas;

        [SerializeField] private List<MusicData> _musicDatas;
    }
}