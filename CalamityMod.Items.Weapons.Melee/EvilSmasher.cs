using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class EvilSmasher : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 64;
		base.Item.height = 66;
		base.Item.damage = 120;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = (base.Item.useTime = 38);
		base.Item.useStyle = 1;
		base.Item.useTurn = true;
		base.Item.knockBack = 6f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
	}

	public override float UseSpeedMultiplier(Player player)
	{
		return 1f + (float)player.Calamity().evilSmasherBoost * 0.1f;
	}

	public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
	{
		damage *= 1f + (float)player.Calamity().evilSmasherBoost * 0.1f;
	}

	public override void ModifyWeaponKnockback(Player player, ref StatModifier knockback)
	{
		knockback *= 1f + (float)player.Calamity().evilSmasherBoost * 0.1f;
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (target.life <= 0 && player.Calamity().evilSmasherBoost < 10)
		{
			player.Calamity().evilSmasherBoost++;
		}
	}
}
