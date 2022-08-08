using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOK1game.Tools;

namespace LOK1game.PinRap
{
    public static class LocalPlayer
    {
        public static PlayerController Controller { get; private set; }

        public static void Initialize(Pawn pawn)
        {
            Controller = pawn.Controller as PlayerController;
        }
    }
}