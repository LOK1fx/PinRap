using LOK1game.PinRap.World;
using UnityEngine;
using LOK1game.World;

namespace LOK1game.PinRap
{
    public class MusicStarter : MonoBehaviour
    {
        public void Play()
        {
            if(GameWorld.TryGetWorld<PinRapGameplayWorld>(out var world))
                MusicTimeline.Instance.StartPlayback(world.MusicData);
        }
    }
}