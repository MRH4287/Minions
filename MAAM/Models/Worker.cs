using DataAccess.Contracts;
namespace MAAM.Models;


public class Worker : IModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? Name { get; set; }

    public string? Surname { get; set; }
    public int Age { get; set; }
    public string? Race { get; set; }
    public Sex Sex { get; set; }
    public string? Rank { get; set; }
    public int Rating { get; set; }
    public double Payment { get; set; }
    public string? Job { get; set; }
    public double CurrentPayment { get; set; }
    public int DayWithoutPay { get; set; }
    public string? ServiceStarted { get; set; }
    public string? ServiceEnded { get; set; }
    public int TimeOnBord { get; set; }
    public string? Inventory { get; set; }
    public string? Skills { get; set; }
    public string? Notes { get; set; }
    public string? Condition { get; set; }

    public List<string>? Tags { get; set; }

    public bool Deleted { get; set; }

    public string? ImageUri { get; set; }

}


public enum Sex { Male, Female, Other };





//public enum Race
//{
//    Aarakocra,
//    Aasimar,
//    Aetherborn,
//    Astral_Elf,
//    Autognome,
//    Aven,
//    Bugbear,
//    Bullywug,
//    Centaur,
//    Changeling,
//    Deep_Gnome,
//    Dhampir,
//    Dragonborn,
//    Duergar,
//    Dwarf,
//    Eladrin,
//    Elf,
//    Fairy,
//    Firbolg,
//    Genasi,
//    God,
//    Giff,
//    Gith,
//    Githyanki,
//    Githzerai,
//    Glitchling,
//    Gnoll,
//    Gnome,
//    Goblin,
//    Goliath,
//    Grimlock,
//    Grung,
//    Hadozee,
//    Half_Elf,
//    Half_Orc,
//    Halfling,
//    Harengon,
//    Hexblood,
//    Hobgoblin,
//    Human,
//    Kalashtar,
//    Kender,
//    Kenku,
//    Khenra,
//    Kobold,
//    Kor,
//    Kuo_Toa,
//    Leonin,
//    Lizardfolk,
//    Locathah,
//    Loxodon,
//    Merfolk,
//    Mew,
//    Minotaur,
//    Naga,
//    Nekomata,
//    Orc,
//    Owlfolk,
//    Owlin,
//    Plasmoid,
//    Rabbitfolk,
//    Reborn,
//    Revenant,
//    Satyr,
//    Sea_Elf,
//    Shadar_Kai,
//    Shifter,
//    Simic_Hybrid,
//    Siren,
//    Skeleton,
//    Tabaxi,
//    Thri_kreen,
//    Tiefling,
//    Tortle,
//    Triton,
//    Troglodyte,
//    Vampire,
//    Vedalken,
//    Verdan,
//    Viashino,
//    Warforged,
//    Yuan_Ti,
//    Yuan_ti_Pureblood,
//    Zombie
//}