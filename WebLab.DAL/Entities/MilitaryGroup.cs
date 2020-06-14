using System.Collections.Generic;

namespace WebLab.DAL.Entities
{
    public class MilitaryGroup {
        public int MilitaryGroupId { get; set; }
        public string GroupName { get; set; }
        /// <summary> 
        /// Навигационное свойство 1-ко-многим 
        /// </summary> 
        public List<Military> Militaries { get; set; }
    }
}
