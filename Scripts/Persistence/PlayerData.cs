namespace Persistence
{
    //[System.Serializable]
    public class PlayerData 
    {
        public LevelInfo[] levels;
    }
    [System.Serializable]
    public class LevelInfo
    {
        public bool isCompleted;
        public int stars;

        public LevelInfo() { }

        public LevelInfo(bool isCompleted, int stars)
        {
            this.isCompleted = isCompleted;
            this.stars = stars;
        }
    }
}