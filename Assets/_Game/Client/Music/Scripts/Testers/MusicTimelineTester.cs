using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOK1game.Tools;

namespace LOK1game.Testers
{
    [RequireComponent(typeof(MusicTimeline))]
    public class MusicTimelineTester : BaseTester
    {
        [SerializeField] private MusicData _songToPlay;

        private MusicTimeline _timeline;

        private void Start()
        {
            _timeline = GetComponent<MusicTimeline>();
        }

        public override void Test1()
        {
            _timeline.StartPlayback(_songToPlay);
        }

        public override void Test2()
        {
            _timeline.EndPlayback();
        }

        public override void Test3()
        {
            throw new System.NotImplementedException();
        }
    }

}