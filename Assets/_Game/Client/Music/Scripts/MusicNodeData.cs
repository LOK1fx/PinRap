using UnityEngine;

namespace LOK1game
{
    [CreateAssetMenu]
    public class MusicNodeData : ScriptableObject
    {
        public EBeatEffectStrength Strength => _strength;

        [SerializeField] private EBeatEffectStrength _strength = EBeatEffectStrength.Medium;
    }
}