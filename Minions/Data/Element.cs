using DataAccess.Contracts;

namespace Minions.Data;

public class Element : IModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? Name { get; set; }
    public int Age { get; set; }
    public Race Race { get; set; }
    public Sex Sex { get; set; }
    public double Payment { get; set; }
    public Job Job { get; set; }
    public double CurrentPayment { get; set; }
    public int DayWihtoutPay { get; set; }

    public int TimeOnBord {  get; set; }

}
    public enum Sex { Male, Female, Other };
    public enum Job
    {
        Rower, 
    First_Mate, 
    Second_Mate, 
    Soldier, 
    Sailor, 
    Magus, 
    Priest, 
    Quartermaster, 
    Officer, 
    Carpenter, 
    Weapons_Officer
    }
    public enum Race
    {
        Aarakocra, 
    Aasimar, 
    Aetherborn, 
    Astral_Elf, 
    Autognome, 
    Aven, 
    Bugbear, 
    Bullywug, 
    Centaur, 
    Changeling, 
    Deep_Gnome,
    Dhampir, 
    Dragonborn, 
    Duergar, 
    Dwarf, 
    Eladrin, 
    Elf, 
    Fairy, 
    Firbolg, 
    Genasi, 
    Giff, 
    Gith, 
    Githyanki, 
    Githzerai, 
    Glitchling, 
    Gnoll, 
    Gnome, 
    Goblin, 
    Goliath, 
    Grimlock, 
    Grung, 
    Hadozee, 
    Half_Elf, 
    Half_Orc, 
    Halfling, 
    Harengon, 
    Hexblood, 
    Hobgoblin, 
    Human, 
    Kalashtar, 
    Kender, 
    Kenku, 
    Khenra, 
    Kobold, 
    Kor, 
    Kuo_Toa, 
    Leonin, 
    Lizardfolk, 
    Locathah, 
    Loxodon, 
    Merfolk, 
    Minotaur, 
    Naga, 
    Nekomata, 
    Orc, 
    Owlfolk, 
    Owlin, 
    Plasmoid, 
    Rabbitfolk, 
    Reborn, 
    Revenant, 
    Satyr, 
    Sea_Elf, 
    Shadar_Kai, 
    Shifter, 
    Simic_Hybrid, 
    Siren, 
    Skeleton, 
    Tabaxi, 
    Thri_kreen, 
    Tiefling, 
    Tortle, 
    Triton, 
    Troglodyte, 
    Vampire, 
    Vedalken, 
    Verdan, 
    Viashino,
    Warforged, 
    Yuan_Ti, 
    Yuan_ti_Pureblood, 
    Zombie
    }