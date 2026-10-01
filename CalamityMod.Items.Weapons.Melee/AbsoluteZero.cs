using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class AbsoluteZero : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 58;
		base.Item.height = 58;
		base.Item.damage = 75;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useTime = 20;
		base.Item.useAnimation = 20;
		base.Item.useStyle = 1;
		base.Item.useTurn = false;
		base.Item.knockBack = 4f;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<DarkIceZero>();
		base.Item.shootSpeed = 3f;
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(324, 300);
		target.AddBuff(ModContent.BuffType<GlacialState>(), 60);
		int p = Projectile.NewProjectile(player.GetSource_ItemUse(base.Item), target.Center, Vector2.Zero, ModContent.ProjectileType<DarkIceZero>(), base.Item.damage, 12f, player.whoAmI);
		Main.projectile[p].Kill();
	}

	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(324, 300);
		int p = Projectile.NewProjectile(player.GetSource_ItemUse(base.Item), target.Center, Vector2.Zero, ModContent.ProjectileType<DarkIceZero>(), base.Item.damage, 12f, player.whoAmI);
		Main.projectile[p].Kill();
	}
}
