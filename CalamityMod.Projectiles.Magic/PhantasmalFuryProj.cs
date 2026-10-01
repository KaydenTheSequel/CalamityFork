using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class PhantasmalFuryProj : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public float dustRotation;

	public bool launched;

	public NPC targeted;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 5;
		base.Projectile.timeLeft = 900;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 16 * base.Projectile.MaxUpdates;
		base.Projectile.ArmorPenetration = 15;
	}

	public override void AI()
	{
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		dustRotation += 0.12f;
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.White;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 0.5f);
		base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt());
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? 0f : ((float)Math.PI));
		if (time >= 500)
		{
			if (time == 500)
			{
				base.Projectile.penetrate = 1;
				launched = true;
			}
			if (targeted == null || targeted.life <= 0)
			{
				targeted = base.Projectile.Center.ClosestNPCAt(950f);
			}
			CalamityUtils.HomeInOnSelectedNPC(base.Projectile, targeted, ignoreTiles: true, 0.15f, 6f, 0.98f, 0.95f, accelerate: true);
			if (time < 550 && targeted == null)
			{
				if (((Vector2)(ref base.Projectile.velocity)).Length() < 6f)
				{
					Projectile projectile = base.Projectile;
					projectile.velocity += (Owner.Calamity().mouseWorld - base.Projectile.Center).SafeNormalize(Vector2.UnitX) * 0.35f;
				}
				else
				{
					Projectile projectile2 = base.Projectile;
					projectile2.velocity *= 0.9f;
				}
			}
		}
		else if (time > 15)
		{
			Vector2 moveToEnemy = (Owner.Center + Utils.RotatedBy(new Vector2(0f, -30f), (double)((float)time * 0.05f), default(Vector2)) - base.Projectile.Center).SafeNormalize(Vector2.UnitX);
			if (((Vector2)(ref base.Projectile.velocity)).Length() < 8f)
			{
				Projectile projectile3 = base.Projectile;
				projectile3.velocity += moveToEnemy * Main.rand.NextFloat(0.2f, 0.4f);
			}
			else
			{
				Projectile projectile4 = base.Projectile;
				projectile4.velocity *= 0.85f;
			}
		}
		if (time > 5)
		{
			Vector2 position = base.Projectile.Center + ((float)Math.PI + dustRotation + (float)Math.PI / 2f).ToRotationVector2() * 10f * base.Projectile.scale;
			Vector2? velocity = ((float)Math.PI + dustRotation * (float)Math.Sign(((Vector2)(ref base.Projectile.velocity)).Length())).ToRotationVector2() * 2f;
			newColor = default(Color);
			Dust dust = Dust.NewDustPerfect(position, 175, velocity, 0, newColor);
			dust.noGravity = false;
			dust.scale = Main.rand.NextFloat(0.75f, 1.2f);
			dust.alpha = Main.rand.Next(100, 171);
			dust.velocity = dust.velocity.RotatedByRandom(0.0);
		}
		time++;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		modifiers.SourceDamage *= (launched ? 1f : 0.3f);
		Vector2 launchVel = (Main.player[base.Projectile.owner].Center - target.Center).SafeNormalize(Vector2.UnitY) * -10f * (launched ? 0.5f : 1f);
		target.MoveNPC(launchVel, 10f * (launched ? 0.5f : 1f), ignoreKBImmune: true);
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (targeted != null)
		{
			if (target != targeted)
			{
				return false;
			}
			return null;
		}
		return null;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i <= 3; i++)
		{
			Dust.NewDustPerfect(base.Projectile.Center, 175, (base.Projectile.velocity * 6f).RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(0.1f, 0.8f), 100, default(Color), Main.rand.NextFloat(1.2f, 1.8f)).noGravity = true;
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(4) ? 278 : 267);
			dust.velocity = base.Projectile.velocity.RotatedByRandom(0.25) * Main.rand.NextFloat(1f, 4f);
			dust.scale = Main.rand.NextFloat(0.5f, 0.9f);
			dust.noGravity = true;
			dust.color = Color.Lerp(Color.White, Color.Aqua, 0.3f);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], Color.White);
		return false;
	}
}
