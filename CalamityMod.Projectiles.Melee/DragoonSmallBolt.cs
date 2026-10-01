using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class DragoonSmallBolt : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public float colorValue;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 15;
		base.Projectile.height = 15;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = 3;
		base.Projectile.extraUpdates = 10;
		base.Projectile.timeLeft = 200;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		Player obj = Main.player[base.Projectile.owner];
		colorValue = MathHelper.Lerp(colorValue, 50f, 0.025f);
		Color usedColor = Color.Lerp(Color.Cyan, Color.Orchid, Utils.GetLerpValue(0f, 50f, colorValue)) * 0.7f;
		base.Projectile.velocity = base.Projectile.velocity.RotatedBy(base.Projectile.ai[1] * 0.022f);
		float num = Vector2.Distance(obj.Center, base.Projectile.Center);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (num < 1400f)
		{
			Vector2 pos = base.Projectile.Center;
			if (base.Projectile.timeLeft % 3 == 0)
			{
				GeneralParticleHandler.SpawnParticle(new BoltParticle(pos, -base.Projectile.velocity * 0.05f, affectedByGravity: false, 15, 0.4f, usedColor, new Vector2(1.8f, 0.8f), glowCenter: true, glowFade: true, fadeIn: false, 0.5f));
			}
			if (Main.rand.NextBool(35))
			{
				GeneralParticleHandler.SpawnParticle(new BoltParticle(pos, base.Projectile.velocity.RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(0.3f, 1.9f), affectedByGravity: false, 23, Main.rand.NextFloat(0.2f, 0.25f), usedColor, new Vector2(1.8f, 0.8f), glowCenter: true, glowFade: true, fadeIn: false, 0.3f));
			}
			if (time % 5 == 0)
			{
				Dust dust = Dust.NewDustPerfect(pos, 278, Utils.RotatedByRandom(new Vector2(2f, 2f), 100.0) * Main.rand.NextFloat(0.5f, 1f), 0, default(Color), Main.rand.NextFloat(0.45f, 0.6f));
				dust.noGravity = true;
				dust.color = usedColor;
			}
		}
		time++;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		float minMult = 0.5f;
		int hitsToMinMult = 2;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(144, 180);
		for (int i = 0; i < 4; i++)
		{
			GeneralParticleHandler.SpawnParticle(new BoltParticle(base.Projectile.Center, base.Projectile.velocity.RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(0.3f, 1.9f), affectedByGravity: false, 13, Main.rand.NextFloat(0.3f, 0.35f), Main.rand.NextBool() ? Color.Orchid : Color.Cyan, new Vector2(1.8f, 0.8f), glowCenter: true, glowFade: true, fadeIn: false, 0.9f));
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		if (time <= 1)
		{
			float _ = float.NaN;
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, Owner.Center, 25f, ref _);
		}
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 25f, targetHitbox);
	}

	public override bool? CanCutTiles()
	{
		return false;
	}
}
