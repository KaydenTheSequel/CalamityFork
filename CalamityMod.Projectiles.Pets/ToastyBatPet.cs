using System;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Pets;

public class ToastyBatPet : ModProjectile, ILocalizedModType, IModType
{
	public Color effectColor;

	public float flapMult;

	public int flapTime;

	public int flapRate;

	public Vector2 goalPosition;

	public new string LocalizationCategory => "Projectiles.Pets";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 8;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.LightPet[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 24);
		base.Projectile.friendly = true;
		base.Projectile.netImportant = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		if (VerifyOwnerIsPresent())
		{
			return;
		}
		HandleFrames();
		int maxFlapTime = flapRate * Main.projFrames[base.Type];
		if (base.Projectile.frame == 4)
		{
			if (flapTime == 0)
			{
				flapTime = maxFlapTime;
			}
			float lerp = 1f - (float)Math.Pow(Utils.GetLerpValue(flapRate - 1, 0f, base.Projectile.frameCounter % flapRate), 1.75);
			flapMult = CalamityUtils.EaseInOutExp(lerp, 2.5f, 1f);
		}
		else
		{
			flapMult = 1f - (float)Math.Pow(Utils.GetLerpValue(maxFlapTime, 1f, flapTime), 3.0);
		}
		if (flapTime > 0)
		{
			flapTime--;
		}
		float sine = (float)Math.Sin(time * 0.08f / (float)Math.PI);
		float sine2 = (float)Math.Sin(time * 0.08f / (float)Math.PI * 2f);
		goalPosition = Owner.Center + new Vector2(35f * sine, -50f + 15f * sine2) + Owner.velocity * 8.5f;
		float followSpeed = 5.5f;
		base.Projectile.velocity = (goalPosition - base.Projectile.Center) / followSpeed;
		base.Projectile.rotation = base.Projectile.velocity.X * 0.035f;
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.Lerp(effectColor, Color.White, 0.6f);
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 0.9f);
		float speedMult = Utils.GetLerpValue(4f, 8f, ((Vector2)(ref Owner.velocity)).Length(), clamped: true);
		if (Main.rand.NextBool((int)(20f - 15f * speedMult)))
		{
			Vector2 vel = Vector2.One.RotatedByRandom(6.2831854820251465);
			Vector2 position = base.Projectile.Center + vel * 4f;
			int type = ModContent.DustType<SquashDust>();
			Vector2? velocity = vel * Main.rand.NextFloat(0.1f, 0.5f) + base.Projectile.velocity * Main.rand.NextFloat(0.3f, 0.75f);
			newColor = default(Color);
			Dust dust = Dust.NewDustPerfect(position, type, velocity, 0, newColor, Main.rand.NextFloat(0.4f, 0.7f));
			dust.noGravity = true;
			dust.color = effectColor;
			dust.noLightEmittence = false;
			dust.fadeIn = -0.95f;
		}
		if (Main.rand.NextBool((int)(10f - 7f * speedMult)) || speedMult >= 1f)
		{
			Vector2 vel2 = Vector2.One.RotatedByRandom(6.2831854820251465);
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + vel2 * 3f, vel2 * Main.rand.NextFloat(0.1f, 0.3f) + base.Projectile.velocity * Main.rand.NextFloat(0.1f, 0.4f), "CalamityMod/Particles/BloomCircle", affectedByGravity: false, Main.rand.Next(35, 45), Main.rand.NextFloat(0.3f, 0.45f), effectColor * 0.2f, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: true, Main.rand.NextFloat(0f, (float)Math.PI * 2f), fadeIn: false, affectedByLight: false, 0f, 1f, 0.2f, flipHorizontal: false, noShrink: true, Main.rand.NextFloat(-0.2f, 0.2f)));
		}
		time++;
	}

	public bool VerifyOwnerIsPresent()
	{
		if (!Owner.active)
		{
			base.Projectile.Kill();
			return true;
		}
		if (Owner.dead)
		{
			Owner.Calamity().toastyBat = false;
		}
		if (Owner.Calamity().toastyBat)
		{
			base.Projectile.timeLeft = 2;
		}
		return false;
	}

	public void HandleFrames()
	{
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / flapRate % Main.projFrames[base.Type];
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		Texture2D glow = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		float sine = (float)Math.Sin(time * 0.08f / (float)Math.PI * 3f);
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition + new Vector2(0f, 7f) * MathHelper.Lerp(1f, -1f, flapMult);
		_ = base.Projectile.rotation;
		_ = base.Projectile.spriteDirection;
		_ = -1;
		_ = texture.Size() * 0.5f;
		Rectangle frame = texture.Frame(1, 8, 0, base.Projectile.frame);
		Vector2 origin = frame.Size() * 0.5f;
		for (int i = 0; i < 2; i++)
		{
			Color val = effectColor;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(glow, drawPosition, null, val * (0.3f + 0.65f * (float)i), base.Projectile.rotation, glow.Size() * 0.5f, new Vector2(1.2f, 1f) * base.Projectile.scale * (0.5f + sine * 0.05f * (float)(1 - i) - 0.3f * (float)i), (SpriteEffects)(Owner.direction == -1));
		}
		Main.EntitySpriteDraw(texture, drawPosition, frame, Color.White, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)(Owner.direction == -1));
		return false;
	}

	public ToastyBatPet()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		effectColor = Color.OrangeRed;
		flapRate = 3;
		base._002Ector();
	}
}
