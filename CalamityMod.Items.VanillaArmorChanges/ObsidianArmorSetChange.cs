using Terraria;

namespace CalamityMod.Items.VanillaArmorChanges;

public class ObsidianArmorSetChange : VanillaArmorChange
{
	public override int? HeadPieceID => 3266;

	public override int? BodyPieceID => 3267;

	public override int? LegPieceID => 3268;

	public override string ArmorSetName => "Obsidian";

	public override void UpdateSetBonusText(ref string setBonusText)
	{
		setBonusText = CalamityUtils.GetTextValue("Vanilla.Armor.SetBonus." + ArmorSetName) ?? "";
	}

	public override void ApplyArmorSetBonus(Player player)
	{
		player.fireWalk = true;
		player.lavaMax += 180;
	}
}
