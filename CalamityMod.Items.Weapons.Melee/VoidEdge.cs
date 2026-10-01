using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "SoulEdge" })]
public class VoidEdge : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle ProjectileDeathSound = SoundID.Item100 with
	{
		Volume = 0.5f
	};

	internal const int TotalProjectilesPerSwing = 3;

	internal const int ProjectileSpreadOutTime = 20;

	internal const float ShootSpeed = 10f;

	internal const float SmallSoulStatMultiplier = 0.8f;

	internal const float MediumSoulStatMultiplier = 0.9f;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 88;
		base.Item.height = 88;
		base.Item.damage = 210;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useStyle = 1;
		base.Item.useTime = 24;
		base.Item.useAnimation = 24;
		base.Item.useTurn = true;
		base.Item.knockBack = 7f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<GhastlySoulLarge>();
		base.Item.shootSpeed = 10f;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		int numShots = 3;
		for (int i = 0; i < numShots; i++)
		{
			velocity = velocity.RotatedByRandom(0.35) * Main.rand.NextFloat(0.9f, 1.1f);
			float ai1 = MathHelper.Lerp(0.75f, 1.25f, Main.rand.NextFloat());
			switch (i)
			{
			case 1:
				type = ModContent.ProjectileType<GhastlySoulMedium>();
				knockback *= 0.9f;
				break;
			case 2:
				type = ModContent.ProjectileType<GhastlySoulSmall>();
				knockback *= 0.8f;
				break;
			}
			int proj = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f, ai1);
			Main.projectile[proj].netUpdate = true;
		}
		return false;
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool())
		{
			int dust = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 66, 0f, 0f, 0, Color.Plum, Main.rand.NextFloat(0.65f, 1.2f));
			Main.dust[dust].noGravity = true;
		}
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/PhantomSpirit");
		style.Volume = 0.65f;
		style.PitchVariance = 0.3f;
		style.Pitch = -0.5f;
		SoundEngine.PlaySound(in style, target.Center);
		for (int i = 0; i <= 30; i++)
		{
			Dust dust = Dust.NewDustPerfect(target.Center, 66, Utils.RotatedByRandom(new Vector2(0f, -18f), MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.1f, 1.9f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.7f, 1.6f);
			Dust dust2 = Dust.NewDustPerfect(target.Center, 66, Utils.RotatedByRandom(new Vector2(0f, -7f), MathHelper.ToRadians(35f)) * Main.rand.NextFloat(0.1f, 1.9f));
			dust2.noGravity = true;
			dust2.scale = Main.rand.NextFloat(0.7f, 1.6f);
		}
		float ai1 = MathHelper.Lerp(0.75f, 1.25f, Main.rand.NextFloat());
		int soulDamage = damageDone / 3;
		Vector2 velocity = Utils.RotatedByRandom(new Vector2(0f, -14f), 0.6499999761581421) * Main.rand.NextFloat(0.9f, 1.1f);
		Projectile.NewProjectile(player.GetSource_ItemUse(base.Item), target.Center + new Vector2(0f, 1300f), velocity.RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(0.9f, 1.1f), ModContent.ProjectileType<GhastlySoulLarge>(), soulDamage, 0f, player.whoAmI, 0f, ai1);
		Projectile.NewProjectile(player.GetSource_ItemUse(base.Item), target.Center + new Vector2(0f, 1300f), velocity.RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(0.9f, 1.1f), ModContent.ProjectileType<GhastlySoulMedium>(), soulDamage, 0f, player.whoAmI, 0f, ai1);
		Projectile.NewProjectile(player.GetSource_ItemUse(base.Item), target.Center + new Vector2(0f, 1300f), velocity.RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(0.9f, 1.1f), ModContent.ProjectileType<GhastlySoulSmall>(), soulDamage, 0f, player.whoAmI, 0f, ai1);
	}
}
