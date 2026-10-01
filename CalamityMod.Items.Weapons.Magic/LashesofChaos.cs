using CalamityMod.Particles;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

[LegacyName(new string[] { "CalamitasInferno" })]
public class LashesofChaos : ModItem, ILocalizedModType, IModType
{
	public int FiringMode;

	public int ProjectilesFired;

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 30;
		base.Item.damage = 111;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 20;
		base.Item.useTime = 46;
		base.Item.useAnimation = 46;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 7.5f;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<BrimstoneHellfireballFriendly>();
		base.Item.shootSpeed = 11f;
	}

	public override float UseSpeedMultiplier(Player player)
	{
		if (FiringMode != 1 || ProjectilesFired >= 9)
		{
			return 1f;
		}
		return 2f;
	}

	public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
	{
		if (FiringMode == 1)
		{
			mult = 0.5f;
		}
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		ProjectilesFired++;
		type = ((FiringMode == 1) ? ModContent.ProjectileType<SeethingDischargeBrimstoneHellblast>() : base.Item.shoot);
		Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer);
		int circleDustAmt = ((FiringMode == 1) ? 8 : (5 + ProjectilesFired));
		for (int d = 0; d < circleDustAmt; d++)
		{
			Dust.NewDustPerfect(player.Center, 235, velocity.RotatedByRandom(MathHelper.ToRadians(30f)) * Main.rand.NextFloat(0.8f, 1.1f), 150, default(Color), (FiringMode == 1) ? 1.7f : (0.8f + (float)ProjectilesFired * 0.15f)).noGravity = true;
		}
		if (FiringMode == 1)
		{
			for (int i = 0; i < 8; i++)
			{
				Vector2 dustVel = Main.rand.NextVector2CircularEdge(1f, 1f);
				dustVel.SafeNormalize(Vector2.Zero);
				dustVel *= Main.rand.NextFloat(4f, 7f);
				int hellfireballDust = Dust.NewDust(player.position, player.width, player.height, 235, dustVel.X, dustVel.Y, 150, default(Color), 1.6f);
				Main.dust[hellfireballDust].noGravity = true;
			}
			for (int p = 0; p < 3; p++)
			{
				GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(position + velocity, velocity.RotatedByRandom(MathHelper.ToRadians(15f)), Color.LightGray, 30, 0.6f, 0.5f, 0f, glowing: true));
			}
		}
		if (FiringMode == 0 && ProjectilesFired >= 5)
		{
			FiringMode = 1;
			ProjectilesFired = 0;
		}
		if (FiringMode == 1 && ProjectilesFired >= 10)
		{
			FiringMode = 0;
			ProjectilesFired = 0;
		}
		return false;
	}
}
