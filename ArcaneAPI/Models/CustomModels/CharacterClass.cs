using ArcaneAPI.Models.GameModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArcaneAPI.Models.CustomModels
{
    [Table("CharacterClass")]
    public class CharacterClass
    {
        public byte Id { get; set; }

        public string Name { get; set; }
        
        public string ShortName { get; set; }

        public virtual ICollection<Character> Characters { get; set; }

        public CharacterClassDTO DTO { get { return new CharacterClassDTO(this); } }
    }

    public class CharacterClassDTO
    {
        public CharacterClassDTO()
        {

        }

        public CharacterClassDTO(CharacterClass item)
        {
            Id = item.Id;
            Name = item.Name;
            ShortName = item.ShortName;
        }

        public byte Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }
    }
}