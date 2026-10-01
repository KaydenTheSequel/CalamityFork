using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class PerfectDark : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 50;
		base.Item.height = 50;
		base.Item.damage = 29;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = 24;
		base.Item.useStyle = 1;
		base.Item.useTime = 24;
		base.Item.useTurn = true;
		base.Item.knockBack = 4.25f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.shoot = ModContent.ProjectileType<DarkBall>();
		base.Item.shootSpeed = 14.5f;
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<BrainRot>(), 300);
	}

	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		target.AddBuff(ModContent.BuffType<BrainRot>(), 300);
	}
}
