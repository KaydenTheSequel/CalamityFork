using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Graphics.Metaballs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class CataclysmicFlame : ModProjectile, ILocalizedModType, IModType
{
	public int MistType = -1;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/FireProj";

	public static int Lifetime => 90;

	public static int Fadetime => 80;

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 10);
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.timeLeft = Lifetime;
		base.Projectile.tileCollide = false;
		base.CooldownSlot = 1;
	}

	public override void AI()
	{
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		if (Time < (float)Fadetime && Main.rand.NextBool(6))
		{
			Vector2 position = base.Projectile.Center + Main.rand.NextVector2Circular(60f, 60f) * Utils.Remap(Time, 0f, Lifetime, 0.5f, 1f);
			float cinderSize = Utils.GetLerpValue(6f, 12f, Time, clamped: true);
			Dust cinder = Dust.NewDustDirect(position, 4, 4, ModContent.DustType<BrimstoneFlame>(), base.Projectile.velocity.X * 0.25f, base.Projectile.velocity.Y * 0.25f);
			if (Main.rand.NextBool(3))
			{
				cinder.scale *= 2f;
				cinder.velocity *= 2f;
			}
			cinder.noGravity = true;
			cinder.scale *= cinderSize * 1.2f;
			cinder.velocity += base.Projectile.velocity * Utils.Remap(Time, 0f, (float)Fadetime * 0.75f, 1f, 0.1f) * Utils.Remap(Time, 0f, (float)Fadetime * 0.1f, 0.1f, 1f);
		}
		if (MistType == -1)
		{
			MistType = Main.rand.Next(3);
		}
		Lighting.AddLight(base.Projectile.Center, 0.75f, 0.15f, 0.15f);
		if (base.Projectile.timeLeft <= Lifetime - 10)
		{
			float fireSize = Utils.Remap(Utils.GetLerpValue(0f, Lifetime, Time), 0.2f, 0.5f, 0.25f, 1f);
			CataclysmMetaball.Particle particle = CataclysmMetaball.SpawnParticle(base.Projectile.Center, Vector2.Zero, (float)TextureAssets.Projectile[base.Type].Width() * fireSize);
			particle.rotation = Main.rand.NextFloat((float)Math.PI * 2f);
			particle.TextureToUse = TextureAssets.Projectile[base.Type].Value;
			particle.SizeScaling = 0.5f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity = oldVelocity * 0.95f;
		Projectile projectile = base.Projectile;
		projectile.position -= base.Projectile.velocity;
		Time++;
		base.Projectile.timeLeft--;
		return false;
	}

	public override void ModifyDamageHitbox(ref Rectangle hitbox)
	{
		int size = (int)Utils.Remap(Time, 0f, Fadetime, 8f, 32f);
		if (Time > (float)Fadetime)
		{
			size = (int)Utils.Remap(Time, Fadetime, Lifetime, 32f, 0f);
		}
		((Rectangle)(ref hitbox)).Inflate(size, size);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		if (!((Rectangle)(ref projHitbox)).Intersects(targetHitbox) || !Collision.CanHit(base.Projectile.Center, 0, 0, ((Rectangle)(ref targetHitbox)).Center.ToVector2(), 0, 0))
		{
			return false;
		}
		return null;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 240);
			int smokeCount = 4 + (int)MathHelper.Clamp((float)target.width * 0.1f, 0f, 20f);
			for (int i = 0; i < smokeCount; i++)
			{
				Vector2 position = target.Center + Main.rand.NextVector2Circular((float)target.width * 0.5f, (float)target.height * 0.5f);
				Vector2 smokeVel = Vector2.UnitY * Main.rand.NextFloat(-2.4f, -0.8f) * MathHelper.Clamp((float)target.height * 0.1f, 1f, 10f);
				GeneralParticleHandler.SpawnParticle(new MediumMistParticle(position, smokeVel, new Color(255, 50, 50), Color.DimGray, Main.rand.NextFloat(1f, 2f), 245 - Main.rand.Next(50), 0.1f));
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}
}
