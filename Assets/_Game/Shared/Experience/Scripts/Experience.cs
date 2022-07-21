namespace LOK1game
{
    public struct Experience
    {
        public uint Value { get; private set; }
        public EExperienceSourceType SourceType { get; private set; }

        public Experience(uint value, EExperienceSourceType sourceType)
        {
            Value = value;
            SourceType = sourceType;
        }

        public Experience(uint value)
        {
            Value = value;
            SourceType = EExperienceSourceType.None;
        }
    }
}