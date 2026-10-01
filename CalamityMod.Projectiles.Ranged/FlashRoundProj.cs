using System;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class FlashRoundProj : ModProjectile, ILocalizedModType, IModType
{
	public int bounces = 2;

	public float shineRot;

	public bool onSpawn = true;

	private const int MaxTime = 600;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public ref float time => ref base.Projectile.ai[2];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 15;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 8;
		base.Projectile.height = 8;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 2;
		base.Projectile.timeLeft = 600;
		base.Projectile.extraUpdates = 3;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.aiStyle = 1;
		base.AIType = 14;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		base.Projectile.spriteDirection = base.Projectile.direction;
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.White;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 0.3f);
		if (bounces < 2 || base.Projectile.numHits > 0)
		{
			int falloffTime = 7;
			if (time > (float)falloffTime)
			{
				base.Projectile.velocity.X *= 0.983f;
			}
			if (base.Projectile.velocity.Y < 15f && time > (float)falloffTime)
			{
				base.Projectile.velocity.Y += 0.16f;
			}
			if (base.Projectile.velocity.Y < 5f)
			{
				base.Projectile.velocity.Y *= 0.98f;
			}
		}
		if (onSpawn)
		{
			base.Projectile.knockBack = 0f;
			base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 10f;
			shineRot = Main.rand.NextFloat(-5f, 5f);
			onSpawn = false;
		}
		if (time == 0f && base.Projectile.timeLeft < 600)
		{
			MakeFlash(onlyFlash: false);
		}
		shineRot += (float)Math.Sign(shineRot) * 0.05f;
		float targetDist = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		if (time > 2f && time % 2f == 0f && targetDist < 1400f)
		{
			float trailSize = Utils.GetLerpValue(2f, 5f, ((Vector2)(ref base.Projectile.velocity)).Length(), clamped: true);
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, -base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 0.5f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 5, 0.13f, Color.White * 0.6f, new Vector2(1f - 0.2f * trailSize, 1f + 2f * trailSize), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.8f * trailSize));
		}
		if (Main.rand.NextBool(3))
		{
			Vector2 center2 = base.Projectile.Center;
			int type = (Main.rand.NextBool(4) ? 91 : 264);
			Vector2? velocity = -base.Projectile.velocity.RotatedByRandom(0.20000000298023224) * Main.rand.NextFloat(0.5f, 0.9f);
			newColor = default(Color);
			Dust dust = Dust.NewDustPerfect(center2, type, velocity, 0, newColor);
			dust.scale = Main.rand.NextFloat(0.55f, 0.7f);
			dust.noGravity = true;
		}
		time++;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		float damageMult = ((base.Projectile.numHits == 0) ? 1f : 0.3f);
		modifiers.SourceDamage *= damageMult;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		time = 0f;
		base.Projectile.velocity = Vector2.Lerp(base.Projectile.Center.DirectionFrom(target.Center) * 12f, Vector2.UnitY * -7f, 0.75f).RotatedByRandom(0.25);
		base.Projectile.netUpdate = true;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		time = 0f;
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			base.Projectile.velocity.X = 0f - oldVelocity.X;
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			base.Projectile.velocity.Y = 0f - oldVelocity.Y;
		}
		if (base.Projectile.velocity.X < 2f && base.Projectile.velocity.X > 0f)
		{
			base.Projectile.velocity.X = 2f;
		}
		if (base.Projectile.velocity.X > -2f && base.Projectile.velocity.X < 0f)
		{
			base.Projectile.velocity.X = -2f;
		}
		if (base.Projectile.velocity.Y < 2f && base.Projectile.velocity.Y > 0f)
		{
			base.Projectile.velocity.Y = 2f;
		}
		if (base.Projectile.velocity.Y > -2f && base.Projectile.velocity.Y < 0f)
		{
			base.Projectile.velocity.Y = -2f;
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.97f;
		int expectedDamage = Math.Max((int)((float)base.Projectile.damage * 1.13f), base.Projectile.damage + 2);
		base.Projectile.damage = expectedDamage;
		if (bounces <= 0)
		{
			base.Projectile.Kill();
			return false;
		}
		for (int i = 0; i < Main.maxNPCs; i++)
		{
			base.Projectile.localNPCImmunity[i] = 0;
		}
		bounces--;
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		float num = (float)Math.Sin((float)base.Projectile.timeLeft * 0.175f / (float)Math.PI);
		float trueSine = MathHelper.Lerp(num, (float)Math.Sign(num) * 0.5f, 0.6f);
		ModContent.Request<Texture2D>("CalamityMod/Particles/BloomLineSoftEdge", (AssetRequestMode)2);
		Asset<Texture2D> shine = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2);
		Texture2D value = shine.Value;
		Vector2 position = base.Projectile.Center - Main.screenPosition;
		Color white = Color.White;
		((Color)(ref white)).A = 0;
		Main.EntitySpriteDraw(value, position, null, white * 0.6f, base.Projectile.rotation + shineRot, shine.Size() / 2f, new Vector2(1f + 0.9f * trueSine, 1f + 0.9f * (0f - trueSine)) * base.Projectile.scale * 0.13f, (SpriteEffects)0);
		Texture2D value2 = shine.Value;
		Vector2 position2 = base.Projectile.Center - Main.screenPosition;
		white = Color.White;
		((Color)(ref white)).A = 0;
		Main.EntitySpriteDraw(value2, position2, null, white * 0.6f, base.Projectile.rotation + shineRot, shine.Size() / 2f, new Vector2(1f + 0.9f * (0f - trueSine), 1f + 0.9f * trueSine) * base.Projectile.scale * 0.13f, (SpriteEffects)0);
		return false;
	}

	public void MakeFlash(bool onlyFlash)
	{
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		if (!onlyFlash)
		{
			for (int i = 0; i < 4; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(4) ? 91 : 264, base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(8.5f, 12f));
				dust.scale = Main.rand.NextFloat(0.75f, 1.1f);
				dust.noGravity = true;
			}
			SoundStyle style = SoundID.Item50 with
			{
				Volume = 0.3f,
				Pitch = -0.2f + 0.4f * (float)bounces,
				MaxInstances = 15
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		for (int j = 0; j < 2; j++)
		{
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.2f * (float)j, 0.45f * (float)j, 9, UseAdditiveBlend: true, 1f, fade: true, 0f, (SpriteEffects)0));
		}
		float rot = Main.rand.NextFloat(-5f, 5f);
		for (int k = 0; k < 5; k++)
		{
			Vector2 dustVel = ((float)Math.PI * 2f * (float)k / 5f + rot).ToRotationVector2() * 5f;
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<SquashDust>(), dustVel);
			dust2.scale = 1.1f;
			dust2.fadeIn = 1.5f;
			dust2.noGravity = true;
			dust2.color = Color.White;
			dust2.noLightEmittence = true;
		}
		if (Main.myPlayer == base.Projectile.owner)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<FlashRoundFlash>(), 5, 0f, base.Projectile.owner);
		}
	}
}
