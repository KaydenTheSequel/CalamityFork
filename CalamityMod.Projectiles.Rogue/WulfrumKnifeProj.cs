using System;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class WulfrumKnifeProj : ModProjectile, ILocalizedModType, IModType
{
	internal Color PrimColorMult;

	public static int Lifetime = 1440;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/WulfrumKnife";

	public float LifetimeCompletion => MathHelper.Clamp((float)(Lifetime - base.Projectile.timeLeft) / (float)Lifetime, 0f, 1f);

	public float StealthEffectOpacity => MathHelper.Clamp(1f - LifetimeCompletion, 0f, 1f);

	public float StuckEnemyID
	{
		get
		{
			return base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public float StuckEnemyDistance
	{
		get
		{
			return base.Projectile.ai[1];
		}
		set
		{
			base.Projectile.ai[1] = value;
		}
	}

	public float StuckEnemyRotation
	{
		get
		{
			return base.Projectile.ai[2];
		}
		set
		{
			base.Projectile.ai[2] = value;
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 20;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.timeLeft = Lifetime;
		base.Projectile.extraUpdates = 5;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 1;
		base.Projectile.stopsDealingDamageAfterPenetrateHits = true;
	}

	public override void AI()
	{
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_050a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0534: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_0546: Unknown result type (might be due to invalid IL or missing references)
		//IL_054b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_0586: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		if (StuckEnemyID > 0f)
		{
			base.Projectile.tileCollide = false;
			if (!Main.npc[(int)StuckEnemyID - 1].active)
			{
				StuckEnemyID = 0f;
				base.Projectile.velocity = -Vector2.UnitY.RotatedByRandom(0.25) * Main.rand.NextFloat(0f, 1f);
				base.Projectile.tileCollide = true;
			}
			else
			{
				base.Projectile.Center = Main.npc[(int)StuckEnemyID - 1].Center + Vector2.UnitX.RotatedBy(StuckEnemyRotation) * StuckEnemyDistance;
			}
			return;
		}
		Color newColor;
		if (StuckEnemyID == -1f)
		{
			Player player = Main.player[base.Projectile.owner];
			base.Projectile.velocity = base.Projectile.DirectionTo(player.Center) * 10f;
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
			if (Main.rand.NextBool(3))
			{
				Vector2 position = base.Projectile.Center + Main.rand.NextVector2Circular(4f, 4f);
				Vector2? velocity = -base.Projectile.velocity * Main.rand.NextFloat(0.2f, 0.1f);
				float scale = Main.rand.NextFloat(1.2f, 1.8f);
				newColor = default(Color);
				Dust.NewDustPerfect(position, 15, velocity, 0, newColor, scale).noGravity = true;
			}
			if (base.Projectile.Distance(player.Center) < 16f)
			{
				player.Calamity().temporaryStealthTimer = 60;
				if (player.Calamity().rogueStealthMax < 0.1f)
				{
					player.Calamity().rogueStealthMax = 0.1f;
				}
				player.Calamity().rogueStealth += player.Calamity().rogueStealthMax * 0.084f;
				base.Projectile.Kill();
			}
			return;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.998f;
		if (base.Projectile.timeLeft < Lifetime - 100)
		{
			base.Projectile.velocity.Y += 0.01f;
		}
		if (!base.Projectile.Calamity().stealthStrike)
		{
			if (Main.rand.NextBool(10))
			{
				Vector2 position2 = base.Projectile.Center + base.Projectile.velocity.RotatedBy(1.5707963705062866).SafeNormalize(Vector2.Zero) * Main.rand.NextFloat(-3f, 3f);
				Vector2? velocity2 = -base.Projectile.velocity * Main.rand.NextFloat(0.6f, 1.5f);
				float scale = Main.rand.NextFloat(1f, 1.4f);
				newColor = default(Color);
				Dust chust = Dust.NewDustPerfect(position2, 15, velocity2, 0, newColor, scale);
				chust.noGravity = true;
				if (!Main.rand.NextBool(5))
				{
					chust.noLightEmittence = true;
				}
			}
			return;
		}
		Vector2 center = base.Projectile.Center;
		newColor = (Main.rand.NextBool() ? Color.GreenYellow : Color.DeepSkyBlue);
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * StealthEffectOpacity);
		if (Main.rand.NextBool(7))
		{
			Vector2 position3 = base.Projectile.Center + base.Projectile.velocity.RotatedBy(1.5707963705062866).SafeNormalize(Vector2.Zero) * Main.rand.NextFloat(-3f, 3f);
			Vector2 velocity3 = base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedByRandom(0.5235987901687622) * Main.rand.NextFloat(4f, 10f);
			GeneralParticleHandler.SpawnParticle(new TechyHoloysquareParticle(position3, velocity3, Main.rand.NextFloat(1f, 2f), Main.rand.NextBool() ? new Color(99, 255, 229) : new Color(25, 132, 247), 25, StealthEffectOpacity));
		}
		if (Main.rand.NextBool(8))
		{
			Vector2 position4 = base.Projectile.Center + base.Projectile.velocity.RotatedBy(1.5707963705062866).SafeNormalize(Vector2.Zero) * Main.rand.NextFloat(-3f, 3f);
			Vector2? velocity4 = -base.Projectile.velocity * Main.rand.NextFloat(0.6f, 1.5f);
			float scale = Main.rand.NextFloat(1f, 1.4f);
			newColor = default(Color);
			Dust dust = Dust.NewDustPerfect(position4, 15, velocity4, 0, newColor, scale);
			dust.noGravity = true;
			dust.noLightEmittence = true;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in WulfrumKnife.TileHitSound, base.Projectile.Center);
		base.Projectile.timeLeft = Lifetime;
		StuckEnemyID = target.whoAmI + 1;
		StuckEnemyDistance = base.Projectile.Distance(target.Center);
		StuckEnemyRotation = base.Projectile.DirectionFrom(target.Center).ToRotation();
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		Collision.HitTiles(base.Projectile.position, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
		SoundEngine.PlaySound(in WulfrumKnife.TileHitSound, base.Projectile.Center);
		return base.OnTileCollide(oldVelocity);
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		float fadeOpacity = (float)Math.Pow(1f - completionRatio, 2.0) * StealthEffectOpacity;
		return Color.GreenYellow.MultiplyRGB(PrimColorMult) * fadeOpacity;
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return 9.4f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		float opacitey = StealthEffectOpacity;
		if (base.Projectile.Calamity().stealthStrike && StuckEnemyID == 0f)
		{
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/BasicTrail", (AssetRequestMode)2));
			CalamityUtils.DrawChromaticAberration(Vector2.UnitX, 1f, delegate(Vector2 offset, Color colorMod)
			{
				//IL_000e: Unknown result type (might be due to invalid IL or missing references)
				//IL_000f: Unknown result type (might be due to invalid IL or missing references)
				//IL_001a: Unknown result type (might be due to invalid IL or missing references)
				//IL_001b: Unknown result type (might be due to invalid IL or missing references)
				PrimColorMult = colorMod;
				PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(WidthFunction, ColorFunction, delegate
				{
					//IL_0010: Unknown result type (might be due to invalid IL or missing references)
					//IL_0016: Unknown result type (might be due to invalid IL or missing references)
					//IL_001b: Unknown result type (might be due to invalid IL or missing references)
					return base.Projectile.Size + offset;
				}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:TrailStreak"]), 30);
			});
			CalamityUtils.DrawChromaticAberration(Vector2.UnitX, 3f, delegate(Vector2 offset, Color colorMod)
			{
				//IL_0011: Unknown result type (might be due to invalid IL or missing references)
				//IL_0016: Unknown result type (might be due to invalid IL or missing references)
				//IL_001b: Unknown result type (might be due to invalid IL or missing references)
				//IL_0020: Unknown result type (might be due to invalid IL or missing references)
				//IL_0021: Unknown result type (might be due to invalid IL or missing references)
				//IL_002f: Unknown result type (might be due to invalid IL or missing references)
				//IL_0034: Unknown result type (might be due to invalid IL or missing references)
				//IL_0035: Unknown result type (might be due to invalid IL or missing references)
				//IL_0040: Unknown result type (might be due to invalid IL or missing references)
				//IL_005b: Unknown result type (might be due to invalid IL or missing references)
				//IL_0065: Unknown result type (might be due to invalid IL or missing references)
				Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition + offset, null, Color.GreenYellow.MultiplyRGB(colorMod) * opacitey, base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
			});
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			opacitey = MathHelper.Clamp(LifetimeCompletion * 8f, 0f, 1f);
			Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor) * opacitey, base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		}
		else
		{
			opacitey = MathHelper.Clamp(LifetimeCompletion * 15f, 0f, 1f);
			Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor) * opacitey, base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		}
		return false;
	}

	public WulfrumKnifeProj()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		PrimColorMult = Color.White;
		base._002Ector();
	}
}
