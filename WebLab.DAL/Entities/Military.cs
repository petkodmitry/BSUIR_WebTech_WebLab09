
namespace WebLab.DAL.Entities
{
    public class Military {
        public int MilitaryId { get; set; } // id техники
        public string MilitaryName { get; set; } // название техники
        public string Description { get; set; } // описание техники
        public int Force { get; set; } // убойная сила
        public string Image { get; set; } // имя файла изображения

        // Навигационные свойства 
        /// <summary>
        /// группа техники (например, танки, артиллерия, самолёты и т.д.) 
        /// </summary>
        public int MilitaryGroupId { get; set; }
        public MilitaryGroup Group { get; set; }
    }
}
