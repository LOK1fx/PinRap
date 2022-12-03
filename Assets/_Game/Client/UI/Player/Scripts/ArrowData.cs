using LOK1game.UI;

namespace LOK1game
{
    public struct ArrowData
    {
        public EArrowType Type;
        public EBeatEffectStrength Strength;
        public float Speed;

        public ArrowData(EArrowType type, EBeatEffectStrength strength, float speed)
        {
            Type = type;
            Strength = strength;
            Speed = speed;
        }
    }
}