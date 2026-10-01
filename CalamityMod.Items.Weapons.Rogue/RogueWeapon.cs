using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public abstract class RogueWeapon : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Rogue";

	public virtual float StealthDamageMultiplier => 1f;

	public virtual float StealthVelocityMultiplier => 1f;

	public virtual float StealthKnockbackMultiplier => 1f;

	public override bool WeaponPrefix()
	{
		return true;
	}

	public override bool RangedPrefix()
	{
		return false;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = (ContentSamples.CreativeHelper.ItemGroup)570;
	}

	public virtual bool AdditionalStealthCheck()
	{
		return false;
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable() || AdditionalStealthCheck())
		{
			damage = (int)((float)damage * StealthDamageMultiplier);
			velocity *= StealthVelocityMultiplier;
			knockback *= StealthKnockbackMultiplier;
		}
		ModifyStatsExtra(player, ref position, ref velocity, ref type, ref damage, ref knockback);
	}

	public virtual void ModifyStatsExtra(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
	}
}
