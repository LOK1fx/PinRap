using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOK1game.Tools;

namespace LOK1game.Testers
{
    [RequireComponent(typeof(CameraSetManager))]
    public class CameraSetManagerTester : BaseTester
    {
        private CameraSetManager _manager;

        private void Awake()
        {
            _manager = GetComponent<CameraSetManager>();
        }

        public override void Test1()
        {
            _manager.SetFocusOnMain();
        }

        public override void Test2()
        {
            _manager.SetFocusOnPlayer();
        }

        public override void Test3()
        {
            _manager.SetFocusOnEnemy();
        }
    }
}