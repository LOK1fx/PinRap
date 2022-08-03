using System.Collections;
using UnityEngine;

namespace LOK1game.Game
{
    /// <summary>
    /// Absolutely empty gamemode. Useful when you wanna make level without any
    /// gameplay
    /// </summary>
    public sealed class EmptyGameMode : MonoBehaviour, IGameMode
    {
        public EGameModeId Id => EGameModeId.Empty;
        
        public IEnumerator OnStart()
        {
            yield return null;
        }

        public IEnumerator OnEnd()
        {
            yield return null;
        }
    }
}