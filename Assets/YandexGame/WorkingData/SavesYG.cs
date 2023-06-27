
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Мои сохранения
        public bool[] hero = { false, false, false };
        public int score = 0;
        public int gold = 0;
        public int location = 0;
        public int lvlStartHealth = 0;
        public int lvlSpeedExp = 0;
        public int lvlPlusChest = 0;
        public int sound;
        public int music;
        public SavesYG()
        {
           
        }
    }
}
