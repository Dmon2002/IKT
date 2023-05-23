
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

        public SavesYG()
        {
           
        }
    }
}
