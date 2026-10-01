using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.CalPlayer;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using CalamityMod.Utilities.Daybreak;
using CalamityMod.Utilities.Daybreak.Buffers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class HalleysStarburst : ModProjectile, ILocalizedModType, IModType
{
	private Color drawColor;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Particles/Sparkle";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 20;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 24);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 1;
		base.Projectile.MaxUpdates = 6;
		base.Projectile.timeLeft = 60 * base.Projectile.MaxUpdates;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.frame = Main.rand.Next(0, 6);
		base.Projectile.scale = 0.75f;
		base.Projectile.rotation += Main.rand.NextFloat(0f, 3f);
		base.Projectile.stopsDealingDamageAfterPenetrateHits = true;
	}

	public override void AI()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation += (float)base.Projectile.direction * 0.05f;
		if (base.Projectile.FinalExtraUpdate())
		{
			BloomParticle star = new BloomParticle(base.Projectile.Center, Vector2.Zero, drawColor, 0.2f, 0.25f, 2, fade: false);
			CustomSpark particle = new CustomSpark(base.Projectile.Center, Vector2.UnitX.RotatedBy(base.Projectile.rotation) * 0.1f, Texture, affectedByGravity: false, 2, 1f, Color.White, Vector2.One);
			GeneralParticleHandler.SpawnParticle(star);
			GeneralParticleHandler.SpawnParticle(particle);
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 5)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 5)
		{
			base.Projectile.frame = 0;
		}
		if (base.Projectile.timeLeft == 1)
		{
			for (int i = 0; i < 5; i++)
			{
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, Main.rand.NextVector2CircularEdge(10f, 10f), affectedByGravity: false, 10, 0.02f, drawColor, new Vector2(0.5f, 1f)));
			}
			if (base.Projectile.damage != 0)
			{
				Main.player[base.Projectile.owner].Calamity().HalleyAccuracyCounter -= HalleysInferno.LostAccuracyPerMiss;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		if (drawColor == Color.Black)
		{
			float num = base.Projectile.ai[0];
			if (num != 1f)
			{
				if (num != 2f)
				{
					if (num != 3f)
					{
						if (num != 4f)
						{
							if (num != 5f)
							{
								if (num == 6f)
								{
									drawColor = Color.White;
								}
							}
							else
							{
								drawColor = Color.Lavender;
							}
						}
						else
						{
							drawColor = Color.SkyBlue;
						}
					}
					else
					{
						drawColor = Color.LimeGreen;
					}
				}
				else
				{
					drawColor = Color.Yellow;
				}
			}
			else
			{
				drawColor = Color.HotPink;
			}
		}
		Main.spriteBatch.End(out var ss);
		GraphicsDevice device = ((Game)Main.instance).GraphicsDevice;
		using RenderTargetLease lease = RenderTargetPool.Shared.Rent(device, Main.screenWidth / 2, Main.screenHeight / 2, RenderTargetDescriptor.Default);
		using (lease.Scope(preserveContents: true, Color.Transparent))
		{
			GameShaders.Misc["CalamityMod:ImpFlameTrail"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/ScarletDevilStreak", (AssetRequestMode)2));
			PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(FireWidthFunction, FireColorFunction, delegate
			{
				//IL_0006: Unknown result type (might be due to invalid IL or missing references)
				//IL_0010: Unknown result type (might be due to invalid IL or missing references)
				return base.Projectile.Size * 0.5f;
			}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:ImpFlameTrail"], useUnscaledMatrices: true), base.Projectile.oldPos.Length + 32);
			Vector2[] fireCoreLength = base.Projectile.oldPos.Take(8).ToArray();
			GameShaders.Misc["CalamityMod:ImpFlameTrail"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/SylvestaffStreak", (AssetRequestMode)2));
			PrimitiveRenderer.RenderTrail(fireCoreLength, new PrimitiveSettings(FireCoreWidthFunction, FireCoreColorFunction, delegate
			{
				//IL_0006: Unknown result type (might be due to invalid IL or missing references)
				//IL_0010: Unknown result type (might be due to invalid IL or missing references)
				return base.Projectile.Size * 0.5f;
			}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:ImpFlameTrail"], useUnscaledMatrices: true), fireCoreLength.Length + 24);
		}
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		Main.spriteBatch.Draw((Texture2D)(object)lease.Target, Vector2.Zero, (Rectangle?)null, Color.White, 0f, Vector2.Zero, 2f, (SpriteEffects)0, 0f);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin(in ss);
		lightColor = drawColor;
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<Voidfrost>(), 75);
		SoundEngine.PlaySound(in SoundID.DD2_CrystalCartImpact, base.Projectile.Center);
		for (int i = 0; i < 14; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? 172 : 206, base.Projectile.velocity);
			dust.scale = Main.rand.NextFloat(1.1f, 1.9f);
			dust.velocity = base.Projectile.velocity.RotatedByRandom(0.5) * Main.rand.NextFloat(0.2f, 2.1f);
			dust.noGravity = true;
		}
		CalamityPlayer cplay = Main.player[base.Projectile.owner].Calamity();
		cplay.HalleyAccuracyCounter++;
		cplay.HalleyAccuracyCounter = MathF.Min(HalleysInferno.MaxAccuracy, cplay.HalleyAccuracyCounter);
		Main.player[base.Projectile.owner].Calamity().StarburstSpawnFrameCounter += cplay.HalleyAccuracyCounter / HalleysInferno.MaxAccuracy * HalleysInferno.MaxStarburstPerStar;
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.25f;
		base.Projectile.timeLeft = base.Projectile.MaxUpdates;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<Nightwither>(), 450);
		SoundEngine.PlaySound(in SoundID.DD2_CrystalCartImpact, base.Projectile.Center);
		for (int i = 0; i < 14; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? 172 : 206, base.Projectile.velocity);
			dust.scale = Main.rand.NextFloat(1.1f, 1.9f);
			dust.velocity = base.Projectile.velocity.RotatedByRandom(0.5) * Main.rand.NextFloat(0.2f, 2.1f);
			dust.noGravity = true;
		}
	}

	public float FireWidthFunction(float completion, Vector2 pos)
	{
		float maxBodyWidth = 38f * base.Projectile.scale;
		float curveRatio = 0.2f;
		List<Vector2> positions = base.Projectile.oldPos.ToList();
		positions.RemoveAll(delegate(Vector2 x)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return x == Vector2.Zero;
		});
		float width = ((!(completion < curveRatio)) ? Utils.Remap(completion, curveRatio, 1f, maxBodyWidth, 0f) : (MathF.Pow(completion / curveRatio, 0.5f) * maxBodyWidth));
		float pulseInterpolant = MathF.Cos((float)Math.PI * completion - Main.GlobalTimeWrappedHourly * 20f) * 0.5f + 0.5f;
		float additionalPulseWidth = MathHelper.Lerp(0f, 12f, pulseInterpolant);
		return (width + additionalPulseWidth) * (float)positions.Count() / (float)ProjectileID.Sets.TrailCacheLength[base.Type];
	}

	public Color FireColorFunction(float completion, Vector2 pos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		Color val = drawColor * 1.3f;
		Color endColor = Color.Lerp(val, Color.Transparent, Utils.GetLerpValue(0.8f, 1f, completion, clamped: true));
		return Color.Lerp(val, endColor, completion) * base.Projectile.Opacity;
	}

	public float FireCoreWidthFunction(float completion, Vector2 pos)
	{
		float maxBodyWidth = base.Projectile.scale * 16f;
		float curveRatio = 0.25f;
		List<Vector2> positions = base.Projectile.oldPos.ToList();
		positions.RemoveAll(delegate(Vector2 x)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return x == Vector2.Zero;
		});
		float width = ((!(completion < curveRatio)) ? Utils.Remap(completion, curveRatio, 1f, maxBodyWidth, 0f) : (MathF.Sin(completion / curveRatio * ((float)Math.PI / 2f)) * maxBodyWidth + curveRatio));
		return width * (float)positions.Count() / (float)ProjectileID.Sets.TrailCacheLength[base.Type];
	}

	public Color FireCoreColorFunction(float completion, Vector2 pos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		Color val = drawColor;
		Color tipColor = Color.Lerp(val, Color.Transparent, Utils.GetLerpValue(0.8f, 1f, completion, clamped: true));
		return Color.Lerp(Color.Lerp(val, tipColor, completion), Color.White, 0.175f) * base.Projectile.Opacity;
	}

	public HalleysStarburst()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		drawColor = Color.Black;
		base._002Ector();
	}
}
