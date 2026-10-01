using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "VirulentKatana" })]
public class Virulence : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 74;
		base.Item.height = 90;
		base.Item.damage = 100;
		base.Item.knockBack = 5.5f;
		base.Item.useAnimation = (base.Item.useTime = 15);
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useTurn = true;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 9f;
		base.Item.shoot = ModContent.ProjectileType<VirulentWave>();
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item1;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		damage = (int)((double)damage * 0.85);
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Plague>(), 300);
	}

	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		target.AddBuff(ModContent.BuffType<Plague>(), 300);
	}
}
