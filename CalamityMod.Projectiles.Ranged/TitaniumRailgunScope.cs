using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class TitaniumRailgunScope : ModProjectile, ILocalizedModType, IModType
{
	public const float BaseMaxCharge = 60f;

	public const float MinimumCharge = 18f;

	public const float WeaponLength = 62f;

	public const float MaxSightAngle = (float)Math.PI * 2f / 3f;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public ref float Charge => ref base.Projectile.ai[0];

	public ref float MaxChargeOrTargetRotation => ref base.Projectile.ai[1];

	public float ChargePercent => MathHelper.Clamp(Charge / MaxChargeOrTargetRotation, 0f, 1f);

	public Player Owner => Main.player[base.Projectile.owner];

	public Vector2 MousePosition
	{
		get
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			return Owner.Calamity().mouseWorld - Owner.MountedCenter;
		}
	}

	public Color ScopeColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.White;
		}
	}

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 1;
		base.Projectile.height = 1;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override void AI()
	{
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_0519: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		Owner.Calamity().mouseWorldListener = true;
		if (Owner.channel && Charge != -1f)
		{
			if (base.Projectile.owner == Main.myPlayer)
			{
				Charge++;
				base.Projectile.rotation = MousePosition.ToRotation();
				base.Projectile.Center = base.Projectile.rotation.ToRotationVector2() * 62f + Owner.MountedCenter;
				Owner.heldProj = base.Projectile.whoAmI;
				Owner.ChangeDir((MousePosition.X >= 0f) ? 1 : (-1));
				Owner.itemRotation = (MousePosition * (float)Owner.direction).ToRotation();
				Owner.itemTime++;
				Owner.itemAnimation++;
				base.Projectile.timeLeft = Owner.itemAnimation;
				if (Charge == MaxChargeOrTargetRotation)
				{
					SoundStyle style = SoundID.Item82 with
					{
						Volume = SoundID.Item82.Volume * 0.7f
					};
					SoundEngine.PlaySound(in style, Owner.MountedCenter);
				}
				if (ChargePercent == 1f && Charge % 2f == 0f)
				{
					Vector2 direction = MousePosition.SafeNormalize(Vector2.UnitX);
					Vector2 sparkVelocity = direction.RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 4f, (float)Math.PI / 4f)) * 6f;
					GeneralParticleHandler.SpawnParticle(new CritSpark(Owner.MountedCenter + direction * 62f, sparkVelocity + Owner.velocity, Color.White, Color.LightBlue, 1f, 16));
				}
				base.Projectile.netUpdate = true;
			}
		}
		else if (Charge != -1f && base.Projectile.owner == Main.myPlayer)
		{
			if (Charge < 18f)
			{
				base.Projectile.netUpdate = true;
				base.Projectile.Kill();
				Owner.itemTime = 1;
				Owner.itemAnimation = 1;
				return;
			}
			Vector2 direction2 = MousePosition.SafeNormalize(Vector2.UnitX);
			Player owner = Owner;
			owner.velocity += direction2 * (ChargePercent * -5f);
			Owner.SetScreenshake(4f * ChargePercent);
			int shotDamage = (int)((float)base.Projectile.damage * ChargePercent);
			Projectile.NewProjectile(new EntitySource_ItemUse_WithAmmo(Owner, Owner.HeldItem, -1), Owner.MountedCenter + direction2 * 62f, direction2, ModContent.ProjectileType<TitaniumRailgunShot>(), shotDamage, base.Projectile.knockBack * ChargePercent, base.Projectile.owner, 0f, ChargePercent);
			float recoil = direction2.RotatedBy((float)Math.PI * -3f / 4f * ChargePercent * (float)Owner.direction).ToRotation();
			float initialRecoil = Owner.itemRotation.ToRotationVector2().RotatedBy((float)Math.PI / 4f * (float)(-Owner.direction) * ChargePercent).ToRotation();
			float originalScale = 0.2f * ChargePercent;
			float maxScale = 1f * ChargePercent;
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(Owner.MountedCenter + direction2 * 62f, Vector2.Zero, Color.White, new Vector2(0.5f, 1f), direction2.ToRotation(), originalScale, maxScale, 30));
			SoundStyle style = SoundID.Item62 with
			{
				Volume = SoundID.Item62.Volume * ChargePercent
			};
			SoundEngine.PlaySound(in style, Owner.MountedCenter);
			if (Owner.Calamity().luxorsGift)
			{
				double rangedDamage = (double)shotDamage * 0.15;
				if (rangedDamage >= 1.0)
				{
					float speed = 24f * ChargePercent;
					int projectile = Projectile.NewProjectile(new EntitySource_ItemUse_WithAmmo(Owner, Owner.HeldItem, -1), base.Projectile.Center, direction2 * speed, ModContent.ProjectileType<LuxorsGiftRanged>(), (int)rangedDamage, 0f, base.Projectile.owner);
					if (projectile.WithinBounds(Main.maxProjectiles))
					{
						Main.projectile[projectile].DamageType = DamageClass.Generic;
					}
				}
			}
			Owner.itemRotation = initialRecoil;
			Charge = -1f;
			MaxChargeOrTargetRotation = recoil;
			base.Projectile.netUpdate = true;
		}
		else
		{
			float newRotation = UpdateAimPostShotRecoil(MaxChargeOrTargetRotation.ToRotationVector2());
			Owner.heldProj = base.Projectile.whoAmI;
			Owner.itemRotation = newRotation;
		}
	}

	private float UpdateAimPostShotRecoil(Vector2 target)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		return Vector2.Lerp(target * (float)Owner.direction, Owner.itemRotation.ToRotationVector2(), 0.825f).ToRotation();
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		if (Charge == -1f)
		{
			return false;
		}
		float sightsSize = 700f;
		float sightsResolution = MathHelper.Lerp(0.04f, 0.2f, Math.Min(ChargePercent * 1.5f, 1f));
		float halfAngle = (1f - ChargePercent) * ((float)Math.PI * 2f / 3f) / 2f;
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Color sightsColor = Color.Lerp(Color.LightBlue, Color.Crimson, ChargePercent);
		Effect spreadEffect = Filters.Scene["CalamityMod:SpreadTelegraph"].GetShader().Shader;
		spreadEffect.Parameters["centerOpacity"].SetValue(0.9f);
		spreadEffect.Parameters["mainOpacity"].SetValue(ChargePercent);
		spreadEffect.Parameters["halfSpreadAngle"].SetValue(halfAngle);
		spreadEffect.Parameters["edgeColor"].SetValue(((Color)(ref sightsColor)).ToVector3());
		spreadEffect.Parameters["centerColor"].SetValue(((Color)(ref sightsColor)).ToVector3());
		spreadEffect.Parameters["edgeBlendLength"].SetValue(0.07f);
		spreadEffect.Parameters["edgeBlendStrength"].SetValue(8f);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, spreadEffect, Main.GameViewMatrix.TransformationMatrix);
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, null, Color.White, base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)texture.Height / 2f), sightsSize, (SpriteEffects)0);
		Effect laserScopeEffect = Filters.Scene["CalamityMod:PixelatedSightLine"].GetShader().Shader;
		laserScopeEffect.Parameters["sampleTexture2"].SetValue((Texture)(object)ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/CertifiedCrustyNoise", (AssetRequestMode)2).Value);
		laserScopeEffect.Parameters["noiseOffset"].SetValue((float)Main.GameUpdateCount * -0.003f);
		laserScopeEffect.Parameters["mainOpacity"].SetValue(ChargePercent);
		laserScopeEffect.Parameters["Resolution"].SetValue(new Vector2(sightsResolution * sightsSize));
		laserScopeEffect.Parameters["laserAngle"].SetValue(0f - base.Projectile.rotation + halfAngle);
		laserScopeEffect.Parameters["laserWidth"].SetValue(0.0025f + (float)Math.Pow(ChargePercent, 5.0) * ((float)Math.Sin(Main.GlobalTimeWrappedHourly * 3f) * 0.002f + 0.002f));
		laserScopeEffect.Parameters["laserLightStrenght"].SetValue(7f);
		laserScopeEffect.Parameters["color"].SetValue(((Color)(ref sightsColor)).ToVector3());
		EffectParameter obj = laserScopeEffect.Parameters["darkerColor"];
		Color black = Color.Black;
		obj.SetValue(((Color)(ref black)).ToVector3());
		laserScopeEffect.Parameters["bloomSize"].SetValue(0.06f);
		laserScopeEffect.Parameters["bloomMaxOpacity"].SetValue(0.4f);
		laserScopeEffect.Parameters["bloomFadeStrenght"].SetValue(7f);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, laserScopeEffect, Main.GameViewMatrix.TransformationMatrix);
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, null, Color.White, 0f, new Vector2((float)texture.Width / 2f, (float)texture.Height / 2f), sightsSize, (SpriteEffects)0);
		laserScopeEffect.Parameters["laserAngle"].SetValue(0f - base.Projectile.rotation - halfAngle);
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, null, Color.White, 0f, new Vector2((float)texture.Width / 2f, (float)texture.Height / 2f), sightsSize, (SpriteEffects)0);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		return false;
	}
}
