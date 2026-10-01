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

public class SwordsmithsPride : ModProjectile, ILocalizedModType, IModType
{
	private bool initialized;

	private Vector2 direction;

	public const float throwOutTime = 60f;

	public const float throwOutDistance = 440f;

	public static float snapPoint = 0.45f;

	public static float retractionPoint = 0.6f;

	public float OverEmpowerment;

	public const float maxEmpowerment = 600f;

	public float AngleReset;

	public Particle smear;

	public CalamityUtils.CurveSegment launch;

	public CalamityUtils.CurveSegment hold;

	public CalamityUtils.CurveSegment retract;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Melee/TrueBiomeBlade_SwordsmithsPride";

	public ref float CurrentState => ref base.Projectile.ai[0];

	public Player Owner => Main.player[base.Projectile.owner];

	public float snapTimer
	{
		get
		{
			if (!(throwTimer / 60f < snapPoint))
			{
				return (throwTimer / 60f - snapPoint) / (1f - snapPoint);
			}
			return 0f;
		}
	}

	public float retractionTimer
	{
		get
		{
			if (!(throwTimer / 60f < retractionPoint))
			{
				return (throwTimer / 60f - retractionPoint) / (1f - retractionPoint);
			}
			return 0f;
		}
	}

	public ref float Empowerment => ref base.Projectile.ai[1];

	public ref float hasMadeSound => ref base.Projectile.localAI[0];

	public ref float hasMadeChargeSound => ref base.Projectile.localAI[1];

	public float throwTimer => 60f - (float)base.Projectile.timeLeft;

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
		base.Projectile.localNPCHitCooldown = OmegaBiomeBlade.WhirlwindAttunement_LocalIFrames;
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
		float bladeLength = 140f * base.Projectile.scale;
		float bladeWidth = 25f * base.Projectile.scale;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + direction * bladeLength, bladeWidth, ref collisionPoint);
	}

	internal float ThrowCurve()
	{
		return CalamityUtils.PiecewiseAnimation((60f - (float)base.Projectile.timeLeft) / 60f, launch, hold, retract);
	}

	public override void AI()
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0962: Unknown result type (might be due to invalid IL or missing references)
		//IL_089b: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08db: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_049f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0793: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0807: Unknown result type (might be due to invalid IL or missing references)
		//IL_080d: Unknown result type (might be due to invalid IL or missing references)
		//IL_081d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0827: Unknown result type (might be due to invalid IL or missing references)
		//IL_082c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0581: Unknown result type (might be due to invalid IL or missing references)
		//IL_0586: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0627: Unknown result type (might be due to invalid IL or missing references)
		//IL_0654: Unknown result type (might be due to invalid IL or missing references)
		//IL_0659: Unknown result type (might be due to invalid IL or missing references)
		//IL_0680: Unknown result type (might be due to invalid IL or missing references)
		//IL_0682: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Unknown result type (might be due to invalid IL or missing references)
		//IL_0526: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0704: Unknown result type (might be due to invalid IL or missing references)
		//IL_070c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0711: Unknown result type (might be due to invalid IL or missing references)
		//IL_0713: Unknown result type (might be due to invalid IL or missing references)
		//IL_071e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0723: Unknown result type (might be due to invalid IL or missing references)
		//IL_0725: Unknown result type (might be due to invalid IL or missing references)
		//IL_072a: Unknown result type (might be due to invalid IL or missing references)
		//IL_072f: Unknown result type (might be due to invalid IL or missing references)
		if (!initialized)
		{
			SoundEngine.PlaySound(in SoundID.Item90, base.Projectile.Center);
			base.Projectile.velocity = Vector2.Zero;
			direction = Owner.SafeDirectionTo(Owner.Calamity().mouseWorld, Vector2.Zero);
			((Vector2)(ref direction)).Normalize();
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
					GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center, Owner.velocity + direction.RotatedBy(variation2) * (1f + strength2) * Main.rand.NextFloat(7.5f, 20f), Color.White, Color.GreenYellow, 2f + Main.rand.NextFloat(0f, 1.5f), 20 + Main.rand.Next(30), 1f, 2f));
				}
			}
		}
		if (CurrentState == 0f)
		{
			float rotation = direction.ToRotation();
			if (rotation > (float)Math.PI * -3f / 4f && rotation < -(float)Math.PI / 4f && hasMadeSound == 1f)
			{
				hasMadeSound = 0f;
			}
			else if (rotation > (float)Math.PI / 4f && rotation < (float)Math.PI * 3f / 4f && hasMadeSound == 0f)
			{
				hasMadeSound = 1f;
				SoundEngine.PlaySound(in SoundID.Item71, base.Projectile.Center);
			}
			if ((hasMadeChargeSound == 0f && (double)(Empowerment / 600f) >= 0.5) || (hasMadeChargeSound == 1f && (double)(Empowerment / 600f) >= 0.75))
			{
				hasMadeChargeSound++;
				SoundStyle style = DeusRitualDrama.PulseSound with
				{
					Pitch = -0.1f + hasMadeChargeSound * 0.1f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			if (hasMadeChargeSound == 2f && Empowerment >= 600f)
			{
				hasMadeChargeSound++;
				SoundEngine.PlaySound(in AstralBeacon.UseSound, base.Projectile.Center);
			}
			if ((double)(Empowerment / 600f) >= 0.5 && (Empowerment + OverEmpowerment) % 30f == 29f && Owner.whoAmI == Main.myPlayer)
			{
				Vector2 shotDirection = Main.rand.NextVector2CircularEdge(10f, 10f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), Owner.Center, shotDirection, ModContent.ProjectileType<SwordsmithsPrideAstralEnergy>(), (int)((float)base.Projectile.damage * OmegaBiomeBlade.WhirlwindAttunement_EnergyDamageMult), 0f, Owner.whoAmI);
			}
			if ((double)(Empowerment / 600f) >= 0.75)
			{
				Color currentColor = Color.Lerp(Color.HotPink, Color.DarkViolet, (float)Math.Sin(Main.GlobalTimeWrappedHourly * 2f)) * ((Empowerment / 600f - 0.75f) / 0.25f * 0.8f);
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
					smear.Scale = base.Projectile.scale * 1.9f;
					smear.Color = currentColor;
				}
				if (Main.rand.NextBool())
				{
					float maxDistance = base.Projectile.scale * 1.9f * 78f;
					Vector2 distance = Main.rand.NextVector2Circular(maxDistance, maxDistance);
					Vector2 angularVelocity = distance.RotatedBy(1.5707963705062866).SafeNormalize(Vector2.Zero) * 2f * (1f + ((Vector2)(ref distance)).Length() / 15f);
					GeneralParticleHandler.SpawnParticle(new CritSpark(Owner.Center + distance, Owner.velocity + angularVelocity, Color.White, currentColor, 1f + 1f * (((Vector2)(ref distance)).Length() / maxDistance), 10, 0.05f, 3f));
				}
			}
			base.Projectile.scale = 1f + Empowerment / 600f * 1.5f;
			direction = direction.RotatedBy(MathHelper.Clamp(Empowerment * 1.1f / 600f, 0.45f, 1f) * ((float)Math.PI / 4f) * 0.2f);
			((Vector2)(ref direction)).Normalize();
			base.Projectile.rotation = direction.ToRotation();
			base.Projectile.Center = Owner.Center + direction * base.Projectile.scale * 10f;
			base.Projectile.timeLeft = 61;
			Empowerment++;
			if (Empowerment > 600f)
			{
				Empowerment = 600f;
				OverEmpowerment++;
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
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		if (CurrentState == 1f && !CalamityUtils.AnyProjectiles(ModContent.ProjectileType<SwordsmithsPrideMonolith>()))
		{
			float monolithScale = MathHelper.Clamp((float)target.width / 100f, 0.3f, 1.25f);
			Vector2 monolithDirection = -Vector2.UnitY.RotatedByRandom(0.3141592741012573);
			if (Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, monolithDirection, ModContent.ProjectileType<SwordsmithsPrideMonolith>(), (int)((float)base.Projectile.damage * OmegaBiomeBlade.WhirlwindAttunement_MonolithDamageMult), base.Projectile.knockBack, Owner.whoAmI, Main.rand.Next(4), 1f, hasMadeChargeSound).ModProjectile is SwordsmithsPrideMonolith monolith)
			{
				monolith.Scale = monolithScale;
				monolith.OriginDirection = monolithDirection;
				monolith.Facing = 0f;
				monolith.Target = target;
			}
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (CurrentState == 1f)
		{
			modifiers.SourceDamage *= MathHelper.Lerp(1f, OmegaBiomeBlade.WhirlwindAttunement_ThrowDamageBoost, Empowerment / 600f);
		}
		else
		{
			modifiers.SourceDamage *= MathHelper.Lerp(OmegaBiomeBlade.WhirlwindAttunement_BaseSwingDamageMult, OmegaBiomeBlade.WhirlwindAttunement_FullSwingDamageMult, Empowerment / 600f);
		}
		if (CurrentState != 1f)
		{
			if (Owner.HeldItem.ModItem is OmegaBiomeBlade sword && Main.rand.NextFloat() <= OmegaBiomeBlade.WhirlwindAttunement_WhirlwindProc)
			{
				sword.OnHitProc = true;
			}
		}
		else if (Owner.HeldItem.ModItem is OmegaBiomeBlade blade && Main.rand.NextFloat() <= OmegaBiomeBlade.WhirlwindAttunement_SwordThrowProc)
		{
			blade.OnHitProc = true;
		}
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
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		Texture2D handle = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/OmegaBiomeBlade", (AssetRequestMode)2).Value;
		Texture2D blade = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/TrueBiomeBlade_SwordsmithsPride", (AssetRequestMode)2).Value;
		float drawRotation = direction.ToRotation() + (float)Math.PI / 4f;
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector(0f, (float)handle.Height);
		Vector2 drawOffset = base.Projectile.Center - Main.screenPosition;
		Main.EntitySpriteDraw(handle, drawOffset, null, lightColor, drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		((Vector2)(ref drawOrigin))._002Ector(0f, (float)blade.Height);
		if (CalamityClientConfig.Instance.Afterimages && CurrentState == 0f && Empowerment / 600f > 0.4f)
		{
			for (int i = 0; i < base.Projectile.oldRot.Length; i++)
			{
				Color color = base.Projectile.GetAlpha(lightColor) * (1f - (float)i / (float)base.Projectile.oldRot.Length);
				float afterimageRotation = base.Projectile.oldRot[i] + (float)Math.PI / 4f;
				Main.spriteBatch.Draw(blade, drawOffset, (Rectangle?)null, color * MathHelper.Lerp(0f, 0.5f, MathHelper.Clamp((Empowerment / 600f - 0.4f) / 0.1f, 0f, 1f)), afterimageRotation, drawOrigin, base.Projectile.scale - 0.2f * ((float)i / (float)base.Projectile.oldRot.Length), (SpriteEffects)0, 0f);
			}
		}
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

	public SwordsmithsPride()
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
