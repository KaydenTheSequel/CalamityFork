using System;
using System.Collections.Generic;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class AuricLandMineExplosion : ModProjectile, ILocalizedModType, IModType
{
	public List<List<Vector2>> lightningTrails = new List<List<Vector2>>();

	public static int lightningCount = 15;

	public static int totalPoints = 10;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	private Player Owner => Main.player[base.Projectile.owner];

	public override void SetDefaults()
	{
		base.Projectile.width = 500;
		base.Projectile.height = 500;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 40;
		base.Projectile.tileCollide = false;
		base.Projectile.hostile = true;
		base.Projectile.friendly = true;
		base.Projectile.trap = true;
	}

	public override void AI()
	{
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft == 20)
		{
			Owner.SetScreenshake(200f);
		}
		if (base.Projectile.timeLeft > 20)
		{
			return;
		}
		if (base.Projectile.ai[0] % 4f == 0f)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/AuricBulletHit");
			style.Pitch = 0.4f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			style = new SoundStyle("CalamityMod/Sounds/Item/TheHiveNuke");
			style.Pitch = -0.4f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			lightningTrails.Clear();
			for (int i = 0; i < lightningCount; i++)
			{
				List<Vector2> points = new List<Vector2>();
				for (int j = 0; j < totalPoints; j++)
				{
					_ = (float)Math.PI * 2f / (float)lightningCount;
					if (j == 0)
					{
						points.Add(base.Projectile.Center + Main.rand.NextVector2Circular(20f, 20f));
						continue;
					}
					Vector2 newPoint = default(Vector2);
					Vector2 jtolookfor = ((j > 1) ? points[j - 2] : base.Projectile.Center);
					float baseDist = ((j == totalPoints - 1) ? 20 : (Main.rand.Next(60, 120) * (1 + (20 - base.Projectile.timeLeft) / 15)));
					newPoint = points[j - 1] + (jtolookfor.DirectionTo(points[j - 1]) * baseDist).RotatedByRandom(1.5707963705062866);
					points.Add(newPoint);
				}
				lightningTrails.Add(points);
			}
		}
		base.Projectile.ai[0]++;
		base.Projectile.damage = 40000;
		base.Projectile.CritChance = 0;
		for (int l = 0; l < 5; l++)
		{
			Vector2 rand = Vector2.UnitX.RotatedByRandom(6.2831854820251465);
			Dust.NewDustPerfect(base.Projectile.Center, 226).velocity = rand * Main.rand.NextFloat(-40f, 40f);
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(15.5f, 35.5f), affectedByGravity: true, 15, Main.rand.NextFloat(0.02f, 0.06f), Color.Cyan, new Vector2(2.5f, 0.7f), quickShrink: true));
		}
	}

	public override bool CanHitPlayer(Player target)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft <= 20)
		{
			Vector2 yeetVec = Vector2.Normalize(target.Center - base.Projectile.Center);
			target.velocity += yeetVec * (target.noKnockback ? 20f : 40f);
			return true;
		}
		return false;
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return MathHelper.Clamp(CalamityUtils.Convert01To010(completionRatio * 2f), 0.2f, 1f) * 4f;
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		return new Color(123, 205, 237);
	}

	internal float BackgroundWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return WidthFunction(completionRatio, vertexPos) * 2f;
	}

	internal Color BackgroundColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		return ColorFunction(completionRatio, vertexPos) * 0.5f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		if (base.Projectile.timeLeft <= 20)
		{
			if (lightningTrails.Count <= 0)
			{
				return false;
			}
			Main.spriteBatch.EnterShaderRegion(BlendState.Additive);
			GameShaders.Misc["CalamityMod:TeslaTrail"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/ZapTrail", (AssetRequestMode)2));
			foreach (List<Vector2> lightningTrail in lightningTrails)
			{
				PrimitiveRenderer.RenderTrail(lightningTrail, new PrimitiveSettings(WidthFunction, ColorFunction, null, smoothen: false, pixelate: false, GameShaders.Misc["CalamityMod:TeslaTrail"]), 60);
				PrimitiveRenderer.RenderTrail(lightningTrail, new PrimitiveSettings(BackgroundWidthFunction, BackgroundColorFunction, null, smoothen: false, pixelate: false, GameShaders.Misc["CalamityMod:TeslaTrail"]), 60);
			}
			Main.spriteBatch.ExitShaderRegion();
		}
		return false;
	}

	public override bool? CanDamage()
	{
		if (base.Projectile.timeLeft > 20)
		{
			return false;
		}
		return null;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AuricRebuke>(), 120);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<AuricRebuke>(), 120);
	}
}
