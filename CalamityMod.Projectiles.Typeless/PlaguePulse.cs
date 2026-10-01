using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class PlaguePulse : ModProjectile, ILocalizedModType, IModType
{
	public float maxRadius = 300f;

	public bool visible;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float time => ref base.Projectile.ai[0];

	public ref float radius => ref base.Projectile.ai[1];

	public override void SetDefaults()
	{
		base.Projectile.width = 96;
		base.Projectile.height = 96;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 60;
		base.Projectile.extraUpdates = 1;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		visible = player.Calamity().toxicHeartVisuals;
		if (base.Projectile.ai[2] == 0f)
		{
			base.Projectile.Center = player.MountedCenter;
		}
		if (base.Projectile.ai[2] == 1f)
		{
			maxRadius = Main.rand.Next(150, 251);
			base.Projectile.extraUpdates = 2;
		}
		if (Main.rand.NextBool(5) && visible)
		{
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center + Main.rand.NextVector2Circular(radius, radius), Vector2.Zero, (Main.rand.NextBool(3) ? Color.LimeGreen : Color.Green) * 0.6f, new Vector2(1f, 1f), 0f, Main.rand.NextFloat(0.07f, 0.23f), 0f, 20));
		}
		if (Main.rand.NextBool(3) && visible)
		{
			for (int i = 0; i < 2; i++)
			{
				int DustID = 89;
				Vector2 spawnPos = base.Projectile.Center + Main.rand.NextVector2Circular(radius, radius);
				Dust dust = Dust.NewDustPerfect(spawnPos, DustID);
				dust.scale = Main.rand.NextFloat(0.5f, 0.9f);
				dust.velocity = (spawnPos - base.Projectile.Center).SafeNormalize(Vector2.UnitX) * Main.rand.NextFloat(5f, 10f);
				dust.noGravity = true;
			}
		}
		radius = Utils.Remap(time, 0f, 60f, 30f, maxRadius);
		time++;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		Player Owner = Main.player[base.Projectile.owner];
		if ((float)Main.rand.Next(0, 101) < Owner.GetTotalCritChance(Owner.GetBestClass()))
		{
			modifiers.SetCrit();
		}
		float minMult = 0.4f;
		int hitsToMinMult = 6;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		target.AddBuff(ModContent.BuffType<Plague>(), 420);
		if (target.life <= 0 && target.realLife == -1 && target.IsAnEnemy(allowStatues: false))
		{
			player.Heal(10);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<PlaguePulse>(), (int)((float)base.Projectile.damage * 0.9f), 0f, base.Projectile.owner, 0f, base.Projectile.ai[1] + 1f, 1f);
		}
		if (target.CanBeMoved())
		{
			Vector2 pushVelocity = player.Center.DirectionTo(target.Center) * 5f;
			target.velocity = pushVelocity;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.player[base.Projectile.owner];
		Texture2D rexture = ModContent.Request<Texture2D>("CalamityMod/Particles/SoftRoundExplosion", (AssetRequestMode)2).Value;
		Texture2D fexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		Color val = Color.Green * Utils.Remap(time, 30f, 60f, 0.35f, 0f);
		((Color)(ref val)).A = 0;
		Color color = val * (visible ? 1f : 0.25f);
		float scale = Utils.Remap(time, 0f, 60f, 1f, 6f) / 21f * ((maxRadius == 300f) ? 1f : 0.7f);
		Vector2 pos = base.Projectile.Center - Main.screenPosition;
		Main.EntitySpriteDraw(rexture, pos, null, color, base.Projectile.rotation - Main.GlobalTimeWrappedHourly * 1.5f, rexture.Size() * 0.5f, scale, (SpriteEffects)0);
		Main.EntitySpriteDraw(rexture, pos, null, color, base.Projectile.rotation + Main.GlobalTimeWrappedHourly * 3f, rexture.Size() * 0.5f, scale * 0.95f, (SpriteEffects)0);
		if (base.Projectile.ai[2] == 1f)
		{
			for (int i = 0; i < 4; i++)
			{
				float pulseScale = scale * 6f + 0.11f - (float)i * 0.022f;
				Main.EntitySpriteDraw(fexture, pos, null, color, base.Projectile.rotation, fexture.Size() * 0.5f, pulseScale, (SpriteEffects)0);
			}
		}
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, radius, targetHitbox);
	}
}
