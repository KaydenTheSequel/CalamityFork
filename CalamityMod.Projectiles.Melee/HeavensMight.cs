using System;
using System.IO;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Tiles.Astral;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class HeavensMight : ModProjectile, ILocalizedModType, IModType
{
	private bool initialized;

	private Vector2 direction;

	private Vector2 oldDirection;

	public const float throwOutTime = 90f;

	public const float throwOutDistance = 440f;

	public static float snapPoint = 0.45f;

	public static float retractionPoint = 0.6f;

	public const float maxEmpowerment = 600f;

	public Particle smear;

	public CalamityUtils.CurveSegment launch;

	public CalamityUtils.CurveSegment hold;

	public CalamityUtils.CurveSegment retract;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Melee/MendedBiomeBlade_HeavensMight";

	public ref float CurrentState => ref base.Projectile.ai[0];

	public Player Owner => Main.player[base.Projectile.owner];

	public float snapTimer
	{
		get
		{
			if (!(throwTimer / 90f < snapPoint))
			{
				return (throwTimer / 90f - snapPoint) / (1f - snapPoint);
			}
			return 0f;
		}
	}

	public float retractionTimer
	{
		get
		{
			if (!(throwTimer / 90f < retractionPoint))
			{
				return (throwTimer / 90f - retractionPoint) / (1f - retractionPoint);
			}
			return 0f;
		}
	}

	public ref float Empowerment => ref base.Projectile.ai[1];

	public ref float hasMadeSound => ref base.Projectile.localAI[0];

	public ref float hasMadeChargeSound => ref base.Projectile.localAI[1];

	public float throwTimer => 90f - (float)base.Projectile.timeLeft;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.width = (base.Projectile.height = 74);
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = TrueBiomeBlade.HolyAttunement_LocalIFrames;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		float collisionPoint = 0f;
		float bladeLength = 105f * base.Projectile.scale;
		float bladeWidth = 20f * base.Projectile.scale;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + direction * bladeLength, bladeWidth, ref collisionPoint);
	}

	internal float ThrowCurve()
	{
		return CalamityUtils.PiecewiseAnimation((90f - (float)base.Projectile.timeLeft) / 90f, launch, hold, retract);
	}

	public override void AI()
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_07be: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_070d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0717: Unknown result type (might be due to invalid IL or missing references)
		//IL_071c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0722: Unknown result type (might be due to invalid IL or missing references)
		//IL_072c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0737: Unknown result type (might be due to invalid IL or missing references)
		//IL_073c: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0601: Unknown result type (might be due to invalid IL or missing references)
		//IL_0637: Unknown result type (might be due to invalid IL or missing references)
		//IL_063d: Unknown result type (might be due to invalid IL or missing references)
		//IL_063e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0643: Unknown result type (might be due to invalid IL or missing references)
		//IL_065a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0675: Unknown result type (might be due to invalid IL or missing references)
		//IL_067b: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0695: Unknown result type (might be due to invalid IL or missing references)
		//IL_069a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_052c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0585: Unknown result type (might be due to invalid IL or missing references)
		//IL_058a: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		if (!initialized)
		{
			SoundEngine.PlaySound(in SoundID.Item90, base.Projectile.Center);
			base.Projectile.velocity = Vector2.Zero;
			direction = Owner.SafeDirectionTo(Owner.Calamity().mouseWorld, Vector2.Zero);
			((Vector2)(ref direction)).Normalize();
			oldDirection = direction;
			initialized = true;
		}
		base.Projectile.rotation = direction.ToRotation();
		if (Owner.CantUseHoldout())
		{
			if (CurrentState == 2f || (CurrentState == 0f && (double)(Empowerment / 600f) < 0.5))
			{
				SoundEngine.PlaySound(in SoundID.Item77, base.Projectile.Center);
				base.Projectile.Kill();
				return;
			}
			if (CurrentState == 0f)
			{
				CurrentState = 1f;
				SoundEngine.PlaySound(in SoundID.Item80, base.Projectile.Center);
				direction = Owner.SafeDirectionTo(Owner.Calamity().mouseWorld, Vector2.Zero);
				for (int i = 0; i <= 8; i++)
				{
					float variation = Main.rand.NextFloat(-(float)Math.PI / 4f, (float)Math.PI / 4f);
					float strength = (float)Math.Sin(variation * 2f + (float)Math.PI / 2f);
					GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center, Owner.velocity + direction.RotatedBy(variation) * (1f + strength) * 2f * Main.rand.NextFloat(7.5f, 20f), Color.White, Color.HotPink, 2f + Main.rand.NextFloat(0f, 1.5f), 20 + Main.rand.Next(30), 1f, 2f));
				}
				for (int j = 0; j <= 8; j++)
				{
					float variation2 = Main.rand.NextFloat(-(float)Math.PI / 4f, (float)Math.PI / 4f);
					float strength2 = (float)Math.Sin(variation2 * 2f + (float)Math.PI / 2f);
					GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center, Owner.velocity + direction.RotatedBy(variation2) * (1f + strength2) * Main.rand.NextFloat(7.5f, 20f), Color.White, Color.Cyan, 2f + Main.rand.NextFloat(0f, 1.5f), 20 + Main.rand.Next(30), 1f, 2f));
				}
			}
		}
		if (CurrentState == 0f)
		{
			if (Owner.whoAmI == Main.myPlayer)
			{
				float rotation = direction.ToRotation();
				if (rotation > (float)Math.PI * -3f / 4f && rotation < -(float)Math.PI / 4f && hasMadeSound == 1f)
				{
					hasMadeSound = 0f;
				}
				else if (rotation > (float)Math.PI / 4f && rotation < (float)Math.PI * 3f / 4f && hasMadeSound == 0f)
				{
					hasMadeSound = 1f;
					SoundEngine.PlaySound(in SoundID.Item71);
				}
			}
			if (Empowerment / 600f >= 0.5f && hasMadeChargeSound == 0f)
			{
				SoundStyle style = DeusRitualDrama.PulseSound with
				{
					Pitch = -0.05f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				hasMadeChargeSound++;
			}
			if (Empowerment >= 600f)
			{
				Empowerment = Empowerment;
				if (Owner.whoAmI == Main.myPlayer && hasMadeChargeSound < 2f)
				{
					SoundEngine.PlaySound(in AstralBeacon.UseSound, base.Projectile.Center);
					hasMadeChargeSound++;
				}
			}
			if ((double)(Empowerment / 600f) >= 0.75)
			{
				if (smear == null)
				{
					smear = new CircularSmearVFX(Owner.Center, Color.HotPink, direction.ToRotation(), base.Projectile.scale * 1.5f);
					GeneralParticleHandler.SpawnParticle(smear);
				}
				else
				{
					smear.Rotation = direction.ToRotation() + (float)Math.PI / 2f;
					smear.Time = 0;
					smear.Position = Owner.Center;
					smear.Scale = base.Projectile.scale * 1.5f;
					smear.Color = Color.Lerp(Color.HotPink, Color.Purple, (float)Math.Sin(Main.GlobalTimeWrappedHourly * 2f)) * ((Empowerment / 600f - 0.75f) / 0.25f * 0.8f);
				}
			}
			base.Projectile.scale = 1f + Empowerment / 600f * 1.85f;
			oldDirection = direction;
			direction = direction.RotatedBy(MathHelper.Clamp(Empowerment * 1.1f / 600f, 0.45f, 1f) * ((float)Math.PI / 4f) * 0.2f);
			((Vector2)(ref direction)).Normalize();
			base.Projectile.rotation = direction.ToRotation();
			base.Projectile.Center = Owner.Center + direction * base.Projectile.scale * 10f;
			base.Projectile.timeLeft = 91;
			Empowerment++;
			if (Empowerment > 600f)
			{
				Empowerment = 600f;
			}
		}
		if (CurrentState == 1f)
		{
			base.Projectile.Center = Owner.Center + direction * base.Projectile.scale * 10f + direction * 440f * ThrowCurve();
			base.Projectile.scale = (1f + Empowerment / 600f * 1.5f) * MathHelper.Clamp(1f - retractionTimer, 0.3f, 1f);
		}
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.ChangeDir(Math.Sign(direction.X));
		Owner.itemRotation = direction.ToRotation();
		if (Owner.direction != 1)
		{
			Owner.itemRotation -= (float)Math.PI;
		}
		Owner.itemRotation = MathHelper.WrapAngle(Owner.itemRotation);
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		if (CurrentState == 1f && !CalamityUtils.AnyProjectiles(ModContent.ProjectileType<HeavensMonolith>()) && Empowerment >= 600f)
		{
			float monolithScale = MathHelper.Clamp((float)target.width / 100f, 0.3f, 1.25f);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center, -Vector2.UnitY.RotatedByRandom(0.3141592741012573), ModContent.ProjectileType<HeavensMonolith>(), (int)((float)base.Projectile.damage * TrueBiomeBlade.HolyAttunement_MonolithDamage), 10f, Owner.whoAmI, Main.rand.Next(4), monolithScale, target.whoAmI);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		if (CurrentState == 1f)
		{
			modifiers.SourceDamage *= MathHelper.Lerp(1f, TrueBiomeBlade.HolyAttunement_ThrowDamageBoost, Empowerment / 600f);
		}
		else
		{
			modifiers.SourceDamage *= MathHelper.Lerp(TrueBiomeBlade.HolyAttunement_BaseSwingDamageMult, TrueBiomeBlade.HolyAttunement_FullSwingDamageMult, Empowerment / 600f);
		}
		modifiers.HitDirectionOverride = (Owner.Center.X > target.Center.X).ToDirectionInt();
	}

	public override void OnKill(int timeLeft)
	{
		if (smear != null)
		{
			smear.Kill();
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		Texture2D handle = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/TrueBiomeBlade", (AssetRequestMode)2).Value;
		Texture2D blade = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/MendedBiomeBlade_HeavensMight", (AssetRequestMode)2).Value;
		float drawRotation = direction.ToRotation() + (float)Math.PI / 4f;
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector(0f, (float)handle.Height);
		Vector2 drawOffset = base.Projectile.Center - Main.screenPosition;
		if (CalamityClientConfig.Instance.Afterimages && CurrentState == 0f && Empowerment / 600f > 0.4f)
		{
			for (int i = 0; i < base.Projectile.oldRot.Length; i++)
			{
				Color color = base.Projectile.GetAlpha(lightColor) * (1f - (float)i / (float)base.Projectile.oldRot.Length);
				float afterimageRotation = base.Projectile.oldRot[i] + (float)Math.PI / 4f;
				Main.spriteBatch.Draw(handle, drawOffset, (Rectangle?)null, color * MathHelper.Lerp(0f, 0.5f, MathHelper.Clamp(Empowerment / 600f - 4f, 0f, 1f)), afterimageRotation, drawOrigin, base.Projectile.scale - 0.2f * ((float)i / (float)base.Projectile.oldRot.Length), (SpriteEffects)0, 0f);
			}
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			Vector2 afterimageDrawOrigin = default(Vector2);
			((Vector2)(ref afterimageDrawOrigin))._002Ector(0f, (float)blade.Height);
			for (int j = 0; j < base.Projectile.oldRot.Length; j++)
			{
				Color color2 = base.Projectile.GetAlpha(lightColor) * (1f - (float)j / (float)base.Projectile.oldRot.Length);
				float afterimageRotation2 = base.Projectile.oldRot[j] + (float)Math.PI / 4f;
				Main.spriteBatch.Draw(blade, drawOffset, (Rectangle?)null, color2 * MathHelper.Lerp(0f, 0.5f, MathHelper.Clamp((Empowerment / 600f - 0.4f) / 0.1f, 0f, 1f)), afterimageRotation2, afterimageDrawOrigin, base.Projectile.scale - 0.2f * ((float)j / (float)base.Projectile.oldRot.Length), (SpriteEffects)0, 0f);
			}
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		}
		Main.EntitySpriteDraw(handle, drawOffset, null, lightColor, drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		((Vector2)(ref drawOrigin))._002Ector(0f, (float)blade.Height);
		float opacityFade = ((CurrentState == 0f) ? 1f : (1f - retractionTimer));
		if (snapTimer > 0f && retractionTimer <= 0f)
		{
			GameShaders.Misc["CalamityMod:BasicTint"].UseOpacity(MathHelper.Clamp(0.8f - snapTimer, 0f, 1f));
			GameShaders.Misc["CalamityMod:BasicTint"].UseColor(Color.White);
			GameShaders.Misc["CalamityMod:BasicTint"].Apply();
		}
		Main.EntitySpriteDraw(blade, drawOffset, null, Color.Lerp(Color.White, lightColor, 0.5f) * 0.9f * opacityFade, drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
		if (CurrentState == 1f && snapTimer > 0f)
		{
			drawChain(snapTimer, retractionTimer);
		}
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		return false;
	}

	public void drawChain(float snapProgress, float retractProgress)
	{
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		Texture2D chainTex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/MendedBiomeBlade_HeavensMightChain", (AssetRequestMode)2).Value;
		float opacity = (((double)retractProgress < 0.5) ? 1f : ((retractProgress - 0.5f) / 0.5f));
		Vector2 Shake = ((retractProgress > 0f) ? Vector2.Zero : (Vector2.One.RotatedByRandom(6.2831854820251465) * (1f - snapProgress) * 10f));
		int dist = (int)Vector2.Distance(Owner.Center, base.Projectile.Center) / 16;
		Vector2[] Nodes = (Vector2[])(object)new Vector2[dist + 1];
		Nodes[0] = Owner.Center;
		Nodes[dist] = base.Projectile.Center;
		Rectangle frame = default(Rectangle);
		Vector2 scale = default(Vector2);
		Vector2 origin = default(Vector2);
		for (int i = 1; i < dist + 1; i++)
		{
			((Rectangle)(ref frame))._002Ector(0, 18 * (i % 2), 12, 18);
			Vector2 positionAlongLine = Vector2.Lerp(Owner.Center, base.Projectile.Center, (float)i / (float)dist);
			Nodes[i] = positionAlongLine + Shake * (float)Math.Sin((float)i / (float)dist * (float)Math.PI);
			float rotation = (Nodes[i] - Nodes[i - 1]).ToRotation() - (float)Math.PI / 2f;
			float yScale = Vector2.Distance(Nodes[i], Nodes[i - 1]) / (float)frame.Height;
			((Vector2)(ref scale))._002Ector(1f, yScale);
			Color chainLightColor = Lighting.GetColor((int)Nodes[i].X / 16, (int)Nodes[i].Y / 16);
			((Vector2)(ref origin))._002Ector((float)(frame.Width / 2), (float)frame.Height);
			Main.EntitySpriteDraw(chainTex, Nodes[i] - Main.screenPosition, frame, chainLightColor * opacity * 0.7f, rotation, origin, scale, (SpriteEffects)0);
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(initialized);
		writer.WriteVector2(direction);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		initialized = reader.ReadBoolean();
		direction = reader.ReadVector2();
	}

	public HeavensMight()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		direction = Vector2.Zero;
		launch = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.CircOut, 0f, 0f, 1f, 4);
		hold = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.Linear, snapPoint, 1f, 0f);
		retract = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyInOut, retractionPoint, 1f, -1.05f, 3);
		base._002Ector();
	}
}
