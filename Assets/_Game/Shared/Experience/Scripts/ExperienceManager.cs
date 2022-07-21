using System;

namespace LOK1game
{
    public class ExperienceManager
    {
        public event Action<IGainExperience> OnExperienceGiven; 

        private IGainExperience _experienceGainer;

        public void SetExperienceGainer(IGainExperience experienceGainer)
        {
            _experienceGainer = experienceGainer;
        }
        
        public void AddExperience(Experience experience)
        {
            if (_experienceGainer == null)
                throw new NullReferenceException("Experience gainer is null!");
            
            _experienceGainer.AddExperience(experience);
            
            OnExperienceGiven?.Invoke(_experienceGainer);
        }
    }
}