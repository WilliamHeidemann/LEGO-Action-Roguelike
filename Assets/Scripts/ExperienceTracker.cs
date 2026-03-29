using System;

namespace Components
{
    public class ExperienceTracker
    {
        public int RequiredForNextLevel { get; private set; }
        private int _experience;
        public Action OnLevelUp { get; set; }
        
        public ExperienceTracker(int requiredForNextLevel)
        {
            RequiredForNextLevel = requiredForNextLevel;
        }
        
        public void Gain(int experience)
        {
            _experience += experience;
            if (_experience >= RequiredForNextLevel)
            {
                OnLevelUp?.Invoke();
                _experience %= RequiredForNextLevel;
                RequiredForNextLevel += 1;
            }
        }
    }
}