using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class EclipsesFallMain : ModProjectile, ILocalizedModType, IModType
{
	public static float RainDamageMult = 0.4f;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/EclipsesFall";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 40);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.penetrate = 3;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 6 * base.Projectile.MaxUpdates;
		base.Projectile.timeLeft = 150 * base.Projectile.MaxUpdates;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 1f, 0.8f, 0.3f);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
		if (Main.rand.NextBool(5))
		{
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + Vector2.UnitY.RotatedBy(base.Projectile.rotation) * Main.rand.NextFloat(-16f, 16f), scale: Main.rand.NextFloat(0.8f, 1.2f), color: Main.rand.NextBool() ? Color.Indigo : Color.DarkOrange, velocity: base.Projectile.velocity * 0.2f, affectedByGravity: false, lifetime: 60));
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		SpawnSpears(target);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		SpawnSpears(target);
	}

	private void SpawnSpears(Entity target)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		int spearAmt = Main.rand.Next(3, 5);
		for (int n = 0; n < spearAmt; n++)
		{
			Vector2 spawnPos = target.Center - new Vector2(Main.rand.NextFloat(-100f, 100f), Main.rand.NextFloat(500f, 800f));
			Vector2 spearVel = CalamityUtils.CalculatePredictiveAimToTargetMaxUpdates(spawnPos, target, 29f, 2) + Vector2.UnitX * Main.rand.NextFloat(-6f, 6f);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnPos, spearVel, ModContent.ProjectileType<EclipsesSmol>(), (int)((float)base.Projectile.damage * RainDamageMult), base.Projectile.knockBack * RainDamageMult, base.Projectile.owner);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		Texture2D glow = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2).Value;
		Main.EntitySpriteDraw(glow, base.Projectile.Center - Main.screenPosition, null, Color.White, base.Projectile.rotation, glow.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
	}
}
