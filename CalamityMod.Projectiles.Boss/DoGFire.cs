using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Enums;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class DoGFire : ModProjectile, ILocalizedModType, IModType, IPixelatedPrimitiveRenderer
{
	public static readonly SoundStyle SpawnSound = new SoundStyle("CalamityMod/Sounds/Custom/DoGFireball");

	public bool IsAPassiveFireball
	{
		get
		{
			return base.Projectile.ai[0] == 2f;
		}
		set
		{
			base.Projectile.ai[0] = value.ToInt();
		}
	}

	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 34;
		base.Projectile.height = 34;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 1;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 300;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 15;
		base.CooldownSlot = 1;
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		if (IsAPassiveFireball)
		{
			SoundEngine.PlaySound(in SpawnSound, base.Projectile.Center);
		}
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (Main.rand.NextBool(12))
		{
			for (int i = 0; i < 2; i++)
			{
				Vector2 position = base.Projectile.Center + Main.rand.NextVector2Circular(6f, 6f);
				Vector2 dustVelocity = base.Projectile.velocity * -1.2f;
				Dust obj = Dust.NewDustDirect(Scale: Main.rand.NextFloat(0.6f, 0.8f) * base.Projectile.scale, newColor: Color.Lerp(Color.White, IsAPassiveFireball ? Color.SkyBlue : Color.Purple, Main.rand.NextFloat(0.5f, 1f)), Position: position, Width: 1, Height: 1, Type: 43, SpeedX: dustVelocity.X, SpeedY: dustVelocity.Y);
				obj.noGravity = true;
				obj.noLight = false;
				obj.noLightEmittence = false;
			}
		}
		if (Main.rand.NextBool(2))
		{
			for (int j = 0; j < 3; j++)
			{
				Vector2 smokeVelocity = -base.Projectile.velocity * 0.7f + Main.rand.NextVector2Circular(1f, 1f) * 0.65f;
				int smokeLifetime = Main.rand.Next(10, 15) * (base.Projectile.friendly ? 1 : 2);
				float smokeScale = Main.rand.NextFloat(0.25f, 0.45f) * base.Projectile.scale;
				float smokeOpacity = Main.rand.NextFloat(0.7f, 0.9f);
				Color flameColor = Color.Lerp(Color.White, IsAPassiveFireball ? Color.SkyBlue : Color.Purple, Main.rand.NextFloat(0.5f, 1f));
				GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center + Main.rand.NextVector2Circular(8f, 8f), smokeVelocity, flameColor, smokeLifetime, smokeScale, smokeOpacity, 0.02f, glowing: true));
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), CalamityUtils.SecondsToFrames(5));
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 12; i++)
		{
			Vector2 dustVelocity = Main.rand.NextVector2Circular(1f, 1f) * 6f;
			float dustScale = Main.rand.NextFloat(3f, 5f);
			Color dustColor = Color.Lerp(Color.White, IsAPassiveFireball ? Color.SkyBlue : Color.Purple, Main.rand.NextFloat(0.5f, 1f));
			Dust dust = Dust.NewDustDirect(base.Projectile.Center, 1, 1, 43, dustVelocity.X, dustVelocity.Y, 0, dustColor, dustScale);
			dust.noGravity = true;
			dust.noLight = false;
			dust.noLightEmittence = false;
		}
		base.Projectile.Damage();
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}

	public float FireWidthFunction(float completion, Vector2 vertexPos)
	{
		float maxBodyWidth = 72f * base.Projectile.scale;
		float curveRatio = 0.2f;
		float width = ((!(completion < curveRatio)) ? Utils.Remap(completion, curveRatio, 1f, maxBodyWidth, 0f) : (MathF.Pow(completion / curveRatio, 0.5f) * maxBodyWidth));
		float pulseInterpolant = MathF.Cos((float)Math.PI * completion - Main.GlobalTimeWrappedHourly * 20f) * 0.5f + 0.5f;
		float additionalPulseWidth = MathHelper.Lerp(0f, 12f, pulseInterpolant);
		return width + additionalPulseWidth;
	}

	public Color FireColorFunction(float completion, Vector2 vertexPos)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		Color val = (IsAPassiveFireball ? Color.Cyan : (Color.Purple * 1.3f));
		Color endColor = Color.Lerp(val, Color.Transparent, Utils.GetLerpValue(0.8f, 1f, completion, clamped: true));
		return Color.Lerp(val, endColor, completion);
	}

	public float FireCoreWidthFunction(float completion, Vector2 vertexPos)
	{
		float maxBodyWidth = base.Projectile.scale * (IsAPassiveFireball ? 24f : 64f);
		float curveRatio = 0.25f;
		if (completion < curveRatio)
		{
			return MathF.Sin(completion / curveRatio * ((float)Math.PI / 2f)) * maxBodyWidth + curveRatio;
		}
		return Utils.Remap(completion, curveRatio, 1f, maxBodyWidth, 0f);
	}

	public Color FireCoreColorFunction(float completion, Vector2 vertexPos)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		Color val = (IsAPassiveFireball ? Color.SkyBlue : Color.Fuchsia);
		Color tipColor = Color.Lerp(val, Color.Transparent, Utils.GetLerpValue(0.8f, 1f, completion, clamped: true));
		return Color.Lerp(Color.Lerp(val, tipColor, completion), Color.White, 0.175f);
	}

	public void RenderPixelatedPrimitives(SpriteBatch spriteBatch, GeneralDrawLayer layer)
	{
		GameShaders.Misc["CalamityMod:ImpFlameTrail"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/ScarletDevilStreak", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(FireWidthFunction, FireColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f + base.Projectile.velocity.SafeNormalize(Vector2.Zero) * 24f;
		}, smoothen: true, pixelate: true, GameShaders.Misc["CalamityMod:ImpFlameTrail"]), base.Projectile.oldPos.Length + 12);
		int coreLength = (IsAPassiveFireball ? 6 : 7);
		GameShaders.Misc["CalamityMod:ImpFlameTrail"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/SylvestaffStreak", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos[..coreLength], new PrimitiveSettings(FireCoreWidthFunction, FireCoreColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f + base.Projectile.velocity.SafeNormalize(Vector2.Zero) * 24f;
		}, smoothen: true, pixelate: true, GameShaders.Misc["CalamityMod:ImpFlameTrail"]), coreLength + 8);
	}
}
