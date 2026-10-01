using Terraria;
using Terraria.Audio;

namespace CalamityMod.Items.VanillaArmorChanges;

public class NecroArmorSetChange : VanillaArmorChange
{
	public const int PostMortemDuration = 10;

	public static readonly SoundStyle TimerSound = new SoundStyle("CalamityMod/Sounds/Custom/TickingTimer");

	public override int? HeadPieceID => 151;

	public override int? BodyPieceID => 152;

	public override int? LegPieceID => 153;

	public override int[] AlternativeHeadPieceIDs => new int[1] { 959 };

	public override string ArmorSetName => "Necro";

	public override void UpdateSetBonusText(ref string setBonusText)
	{
		setBonusText = setBonusText + "\n" + CalamityUtils.GetText("Vanilla.Armor.SetBonus." + ArmorSetName).Format(10);
	}

	public override void ApplyArmorSetBonus(Player player)
	{
		player.Calamity().necroSet = true;
	}
}
