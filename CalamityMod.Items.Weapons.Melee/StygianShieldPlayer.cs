using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class StygianShieldPlayer : ModPlayer
{
	public int disableDashTimer;

	public override void UpdateEquips()
	{
		if (base.Player.HeldItem.type == ModContent.ItemType<StygianShield>())
		{
			base.Player.hasRaisableShield = true;
			base.Player.statDefense += 16;
			base.Player.noKnockback = true;
			disableDashTimer = 90;
		}
		if (disableDashTimer > 0)
		{
			base.Player.Calamity().blockAllDashes = true;
			disableDashTimer--;
		}
		else if (base.Player.dead || !base.Player.active)
		{
			disableDashTimer = 0;
		}
	}

	public override void UpdateVisibleVanityAccessories()
	{
		if (base.Player.HeldItem.type == ModContent.ItemType<StygianShield>())
		{
			base.Player.shield = EquipLoader.GetEquipSlot(base.Mod, "StygianShield", EquipType.Shield);
			base.Player.cShield = 0;
		}
	}
}
