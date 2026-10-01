using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class RancorMagicCircle : ModProjectile, ILocalizedModType, IModType
{
	private SlotId PulseLoopSoundSlot;

	public const int ChargeupTime = 180;

	public new string LocalizationCategory => "Projectiles.Magic";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float Time => ref base.Projectile.ai[0];

	public ActiveSound PulseLoopSound
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			if (SoundEngine.TryGetActiveSound(PulseLoopSoundSlot, out ActiveSound sound))
			{
				return sound;
			}
			return null;
		}
	}

	public float ChargeupCompletion => MathHelper.Clamp(Time / 180f, 0f, 1f);

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 114);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 90000;
	}

	public override void AI()
	{
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Calamity().UpdatePriority = 1f;
		if (!Owner.channel || Owner.noItems || Owner.CCed)
		{
			base.Projectile.Kill();
			return;
		}
		if (Time >= 1f && Owner.ownedProjectileCounts[ModContent.ProjectileType<RancorHoldout>()] <= 0)
		{
			base.Projectile.Kill();
			return;
		}
		AdjustVisualValues();
		UpdateAim();
		Vector2 circlePointDirection = base.Projectile.velocity.SafeNormalize(Vector2.UnitX * (float)Owner.direction);
		base.Projectile.Center = Owner.Center + circlePointDirection * base.Projectile.scale * 56f;
		Owner.ChangeDir(base.Projectile.direction);
		DoPrettyDustEffects();
		ActiveSound soundOut;
		if (Time < 180f)
		{
			HandleChargeEffects();
		}
		else if (!SoundEngine.TryGetActiveSound(PulseLoopSoundSlot, out soundOut) || !soundOut.IsPlaying)
		{
			SoundStyle style = SoundID.DD2_EtherianPortalIdleLoop with
			{
				IsLooped = true
			};
			PulseLoopSoundSlot = SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		if (Time == 15f)
		{
			SoundEngine.PlaySound(in SoundID.Item117, base.Projectile.Center);
		}
		Time++;
	}

	public void AdjustVisualValues()
	{
		base.Projectile.scale = Utils.GetLerpValue(0f, 35f, Time, clamped: true) * 1.4f;
		base.Projectile.Opacity = (float)Math.Pow(base.Projectile.scale / 1.4f, 2.0);
		base.Projectile.rotation -= MathHelper.ToRadians(base.Projectile.scale * 4f);
	}

	public void UpdateAim()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			Vector2 idealDirection = Owner.SafeDirectionTo(Main.MouseWorld, Vector2.UnitX * (float)Owner.direction);
			Vector2 newAimDirection = base.Projectile.velocity.MoveTowards(idealDirection, 0.05f);
			if (newAimDirection != base.Projectile.velocity)
			{
				base.Projectile.ForceNetUpdate();
			}
			base.Projectile.velocity = newAimDirection;
			base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt();
		}
	}

	public void DoPrettyDustEffects()
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		int dustSpawnChance = (int)MathHelper.SmoothStep(20f, 2f, ChargeupCompletion);
		for (int i = 0; i < 2; i++)
		{
			if (Main.rand.NextBool(dustSpawnChance))
			{
				float dustSpawnOffsetFactor = Main.rand.NextFloat((float)base.Projectile.width * 0.375f, (float)base.Projectile.width * 0.485f);
				Vector2 dustSpawnOffset = Main.rand.NextVector2CircularEdge(0.5f, 1f).RotatedBy(base.Projectile.velocity.ToRotation()) * dustSpawnOffsetFactor;
				Vector2 dustVelocity = (-dustSpawnOffset.SafeNormalize(Vector2.UnitY)).RotatedBy((float)Math.PI / 2f * Main.rand.NextFloatDirection());
				dustVelocity *= Main.rand.NextFloat(2f, 6f);
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + dustSpawnOffset, 264);
				dust.color = Color.Lerp(Color.Red, Color.Blue, Main.rand.NextFloat());
				dust.velocity = dustVelocity;
				dust.scale *= Main.rand.NextFloat(1f, 1.4f);
				dust.noLight = true;
				dust.noGravity = true;
			}
		}
		if (Time > 30f && Time % 5f == 0f)
		{
			Vector2 particleVel = Main.rand.NextVector2CircularEdge(1f, 1f);
			particleVel.SafeNormalize(Vector2.Zero);
			particleVel *= Main.rand.NextFloat(5f, 10f);
			GeneralParticleHandler.SpawnParticle(new GenericBloom(base.Projectile.Center, particleVel, Color.Lerp(Color.White, Color.Red, ChargeupCompletion), MathHelper.Lerp(0.075f, 0.35f, ChargeupCompletion), 30));
		}
	}

	public void HandleChargeEffects()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		if (Time == 30f)
		{
			SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/MoonLordLaserCharge"), base.Projectile.Center, (ActiveSound _) => new ProjectileAudioTracker(base.Projectile).IsActiveAndInGame());
		}
		if (Main.rand.NextBool(3))
		{
			float dustSpeed = MathHelper.Lerp(3.5f, 8f, ChargeupCompletion) * Main.rand.NextFloat(0.65f, 1f);
			float dustSpawnOffsetFactor = Main.rand.NextFloat((float)base.Projectile.width * 0.375f, (float)base.Projectile.width * 0.485f);
			Vector2 dustVelocity = base.Projectile.velocity * dustSpeed;
			Vector2 dustSpawnOffset = Main.rand.NextVector2CircularEdge(0.5f, 1f).RotatedBy(base.Projectile.velocity.ToRotation()) * dustSpawnOffsetFactor;
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + dustSpawnOffset, 264);
			dust.color = Color.Lerp(Color.Red, Color.Blue, Main.rand.NextFloat());
			dust.velocity = dustVelocity;
			dust.scale *= Main.rand.NextFloat(1f, 1.05f + ChargeupCompletion * 0.55f);
			dust.noLight = true;
			dust.noGravity = true;
		}
		if (Time == 179f)
		{
			SoundEngine.PlaySound(in SoundID.Zombie104, base.Projectile.Center);
			if (Main.myPlayer == base.Projectile.owner)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity, ModContent.ProjectileType<RancorLaserbeam>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, base.Projectile.identity);
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		Texture2D outerCircleTexture = TextureAssets.Projectile[base.Type].Value;
		Texture2D outerCircleGlowmask = ModContent.Request<Texture2D>(Texture + "Glowmask", (AssetRequestMode)2).Value;
		Texture2D innerCircleTexture = ModContent.Request<Texture2D>(Texture + "Inner", (AssetRequestMode)2).Value;
		Texture2D innerCircleGlowmask = ModContent.Request<Texture2D>(Texture + "InnerGlowmask", (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		float directionRotation = base.Projectile.velocity.ToRotation();
		Color startingColor = Color.Red;
		Color endingColor = Color.Blue;
		restartShader(outerCircleGlowmask, base.Projectile.Opacity, base.Projectile.rotation, BlendState.Additive);
		Main.EntitySpriteDraw(outerCircleGlowmask, drawPosition, null, Color.White, 0f, outerCircleGlowmask.Size() * 0.5f, base.Projectile.scale * 1.075f, (SpriteEffects)0);
		restartShader(outerCircleTexture, base.Projectile.Opacity * 0.7f, base.Projectile.rotation, BlendState.AlphaBlend);
		Main.EntitySpriteDraw(outerCircleTexture, drawPosition, null, Color.White, 0f, outerCircleTexture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		restartShader(innerCircleGlowmask, base.Projectile.Opacity * 0.5f, 0f, BlendState.Additive);
		Main.EntitySpriteDraw(innerCircleGlowmask, drawPosition, null, Color.White, 0f, innerCircleGlowmask.Size() * 0.5f, base.Projectile.scale * 1.075f, (SpriteEffects)0);
		restartShader(innerCircleTexture, base.Projectile.Opacity * 0.7f, 0f, BlendState.AlphaBlend);
		Main.EntitySpriteDraw(innerCircleTexture, drawPosition, null, Color.White, 0f, innerCircleTexture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		Main.spriteBatch.ExitShaderRegion();
		return false;
		void restartShader(Texture2D texture, float opacity, float circularRotation, BlendState blendMode)
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0067: Unknown result type (might be due to invalid IL or missing references)
			//IL_0125: Unknown result type (might be due to invalid IL or missing references)
			//IL_0159: Unknown result type (might be due to invalid IL or missing references)
			//IL_0186: Unknown result type (might be due to invalid IL or missing references)
			//IL_0187: Unknown result type (might be due to invalid IL or missing references)
			//IL_0188: Unknown result type (might be due to invalid IL or missing references)
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, blendMode, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			CalamityUtils.CalculatePerspectiveMatricies(out var viewMatrix, out var projectionMatrix);
			GameShaders.Misc["CalamityMod:RancorMagicCircle"].UseColor(startingColor);
			GameShaders.Misc["CalamityMod:RancorMagicCircle"].UseSecondaryColor(endingColor);
			GameShaders.Misc["CalamityMod:RancorMagicCircle"].UseSaturation(directionRotation);
			GameShaders.Misc["CalamityMod:RancorMagicCircle"].UseOpacity(opacity);
			GameShaders.Misc["CalamityMod:RancorMagicCircle"].Shader.Parameters["uDirection"].SetValue((float)base.Projectile.direction);
			GameShaders.Misc["CalamityMod:RancorMagicCircle"].Shader.Parameters["uCircularRotation"].SetValue(circularRotation);
			GameShaders.Misc["CalamityMod:RancorMagicCircle"].Shader.Parameters["uImageSize0"].SetValue(texture.Size());
			GameShaders.Misc["CalamityMod:RancorMagicCircle"].Shader.Parameters["overallImageSize"].SetValue(outerCircleTexture.Size());
			GameShaders.Misc["CalamityMod:RancorMagicCircle"].Shader.Parameters["uWorldViewProjection"].SetValue(viewMatrix * projectionMatrix);
			GameShaders.Misc["CalamityMod:RancorMagicCircle"].Apply();
		}
	}

	public override void OnKill(int timeLeft)
	{
		PulseLoopSound?.Stop();
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
