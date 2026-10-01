using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class VolatileStarcore : ModProjectile, ILocalizedModType, IModType
{
	private static int Lifetime = 240;

	private static int NumAnimationFrames = 6;

	private static int AnimationFrameTime = 1;

	public bool explode = true;

	public int time;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = NumAnimationFrames;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 1;
		base.Projectile.height = 1;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.extraUpdates = 6;
		base.Projectile.timeLeft = Lifetime;
	}

	public override void AI()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		float num = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		if (base.Projectile.timeLeft == 1)
		{
			explode = false;
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = 1f;
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
		}
		if (base.Projectile.localAI[0] >= 5f)
		{
			SpawnDust();
		}
		Lighting.AddLight(base.Projectile.Center, 1.8f, 1.6f, 0.5f);
		base.Projectile.localAI[0]++;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > AnimationFrameTime)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= NumAnimationFrames)
		{
			base.Projectile.frame = 0;
		}
		if (num < 1400f)
		{
			if (time > 8 && time % 2 == 0)
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + Main.rand.NextVector2Circular(15f, 15f) - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 10f, -base.Projectile.velocity * Main.rand.NextFloat(0.2f, 0.8f), affectedByGravity: false, 35, 0.9f, Color.Lerp(Color.Orange, Color.OrangeRed, Main.rand.NextFloat(0f, 1f))));
			}
			if (time > 6 && time % 2 == 0)
			{
				for (int i = 0; i < 2; i++)
				{
					GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 10f, -base.Projectile.velocity, affectedByGravity: false, 8, 0.13f * ((i == 0) ? 0.5f : 1f), Color.Lerp(Color.Red, Color.OrangeRed, 0.25f) * 0.5f, new Vector2(0.3f, 1f), quickShrink: false, glow: false, 0.8f));
				}
			}
		}
		time++;
	}

	private void SpawnDust()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		int trailDustCount = 3;
		int trailDustType = 303;
		for (int i = 0; i < trailDustCount; i++)
		{
			float scale = Main.rand.NextFloat(0.7f, 1.1f);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(25f, 25f), trailDustType, Vector2.Zero, 120);
			dust.velocity = -base.Projectile.velocity * Main.rand.NextFloat(0.4f, 0.8f);
			dust.scale = scale;
			dust.noGravity = true;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(189, 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(189, 180);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			int type = ModContent.ProjectileType<HeliumFlashBlast>();
			int damage = (int)(1.4f * (float)base.Projectile.damage);
			float kb = 9.5f;
			if (explode)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/HeliumFlashCoreImpact");
				style.Volume = 1f;
				style.Pitch = 0f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, type, damage, kb, base.Projectile.owner);
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/VolatileStarcore", (AssetRequestMode)2).Value;
		Rectangle frame = value.Frame(1, NumAnimationFrames, 0, base.Projectile.frame);
		Vector2 origin = frame.Size() * 0.5f;
		Texture2D rechargeTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		float randSize = Main.rand.NextFloat(0.8f, 1.2f);
		Vector2 position = base.Projectile.Center - Main.screenPosition;
		Color val = Color.OrangeRed;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(rechargeTexture, position, null, val, base.Projectile.rotation, rechargeTexture.Size() * 0.5f, 0.6f * randSize, (SpriteEffects)0);
		Vector2 position2 = base.Projectile.Center - Main.screenPosition;
		val = Color.White;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(rechargeTexture, position2, null, val * 0.75f, base.Projectile.rotation, rechargeTexture.Size() * 0.5f, 0.35f * randSize, (SpriteEffects)0);
		Main.EntitySpriteDraw(value, base.Projectile.Center - Main.screenPosition, frame, Color.White, 0f, origin, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 40f, targetHitbox);
	}
}
