using System;
using System.IO;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class EarthenTides : ModProjectile, ILocalizedModType, IModType
{
	private bool initialized;

	private Vector2 direction;

	private const float MaxCharge = 420f;

	public Vector2 lastDisplacement;

	public float dashDuration;

	public static readonly SoundStyle FullChargeSound = new SoundStyle("CalamityMod/Sounds/Item/MagicRockSound");

	public static readonly SoundStyle GroundImpact = new SoundStyle("CalamityMod/Sounds/Item/MagicRockImpact");

	public CalamityUtils.CurveSegment QuickOut;

	public CalamityUtils.CurveSegment Bump;

	public CalamityUtils.CurveSegment QuickDraw;

	public CalamityUtils.CurveSegment SlowDrawOut;

	public CalamityUtils.CurveSegment OverShoot;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Melee/TrueBiomeBlade_EarthenTides";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float Charge => ref base.Projectile.ai[0];

	public ref float State => ref base.Projectile.ai[1];

	public ref float CurrentIndicator => ref base.Projectile.localAI[0];

	public ref float OverCharge => ref base.Projectile.localAI[1];

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.width = (base.Projectile.height = 70);
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 16;
	}

	public override bool? CanDamage()
	{
		return State == 1f;
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
		float bladeLength = 145f * base.Projectile.scale;
		float bladeWidth = 25f * base.Projectile.scale;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Owner.Center, Owner.Center + direction * bladeLength, bladeWidth, ref collisionPoint);
	}

	internal float ChargeDisplacement()
	{
		return CalamityUtils.PiecewiseAnimation(Charge / 420f, QuickOut, Bump, QuickDraw, SlowDrawOut, OverShoot);
	}

	public override void AI()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0800: Unknown result type (might be due to invalid IL or missing references)
		//IL_0855: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0594: Unknown result type (might be due to invalid IL or missing references)
		//IL_059a: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0620: Unknown result type (might be due to invalid IL or missing references)
		//IL_0626: Unknown result type (might be due to invalid IL or missing references)
		//IL_0630: Unknown result type (might be due to invalid IL or missing references)
		//IL_0640: Unknown result type (might be due to invalid IL or missing references)
		//IL_0645: Unknown result type (might be due to invalid IL or missing references)
		//IL_064a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0654: Unknown result type (might be due to invalid IL or missing references)
		//IL_0659: Unknown result type (might be due to invalid IL or missing references)
		//IL_065e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0660: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0703: Unknown result type (might be due to invalid IL or missing references)
		//IL_0709: Unknown result type (might be due to invalid IL or missing references)
		//IL_0713: Unknown result type (might be due to invalid IL or missing references)
		//IL_0719: Unknown result type (might be due to invalid IL or missing references)
		//IL_071b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0728: Unknown result type (might be due to invalid IL or missing references)
		//IL_0732: Unknown result type (might be due to invalid IL or missing references)
		//IL_074b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0750: Unknown result type (might be due to invalid IL or missing references)
		//IL_0755: Unknown result type (might be due to invalid IL or missing references)
		//IL_066e: Unknown result type (might be due to invalid IL or missing references)
		//IL_076d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0766: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_0512: Unknown result type (might be due to invalid IL or missing references)
		//IL_0523: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		if (!initialized)
		{
			base.Projectile.velocity = Vector2.Zero;
			SoundEngine.PlaySound(in SoundID.Item101, base.Projectile.Center);
			initialized = true;
		}
		if (Owner.CantUseHoldout() && State == 0f)
		{
			if (Charge / 420f < 0.25f)
			{
				SoundEngine.PlaySound(in SoundID.Item109, base.Projectile.Center);
				base.Projectile.Kill();
				return;
			}
			SoundEngine.PlaySound(SoundID.Item120 with
			{
				Volume = SoundID.Item120.Volume * 0.5f
			}, base.Projectile.Center);
			State = 1f;
			base.Projectile.timeLeft = (7 + (int)((Charge / 420f - 0.25f) * 20f)) * 2;
			dashDuration = base.Projectile.timeLeft;
			lastDisplacement = base.Projectile.Center - Owner.Center;
			base.Projectile.ForceNetUpdate();
		}
		if (State == 0f)
		{
			direction = Owner.SafeDirectionTo(Owner.Calamity().mouseWorld, Vector2.Zero);
			((Vector2)(ref direction)).Normalize();
			base.Projectile.Center = Owner.Center + direction * 70f * ChargeDisplacement();
			Charge++;
			OverCharge--;
			base.Projectile.timeLeft = 2;
			if ((Charge / 420f >= 0.25f && CurrentIndicator == 0f) || (Charge / 420f >= 0.5f && CurrentIndicator == 1f) || (Charge / 420f >= 0.75f && CurrentIndicator == 2f && Owner.whoAmI == Main.myPlayer))
			{
				for (int s = 0; s < 2; s++)
				{
					Vector2 swordVel = (Vector2.UnitX.RotatedBy(base.Projectile.rotation) * 10f).RotatedByRandom(0.39269909262657166);
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), Owner.Center, swordVel, ModContent.ProjectileType<EarthenTidesBeam>(), (int)((float)base.Projectile.damage * OmegaBiomeBlade.ShockwaveAttunement_BeamDamageMult), 10f, Owner.whoAmI);
				}
				SoundStyle style = SoundID.Item69 with
				{
					Pitch = -0.2f + 0.1f * CurrentIndicator
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				CurrentIndicator++;
				OverCharge = 20f;
			}
			if (Charge >= 420f)
			{
				Charge = 420f;
				if (Owner.whoAmI == Main.myPlayer && CurrentIndicator < 4f)
				{
					for (int i = 0; i < 5; i++)
					{
						Vector2 swordVel2 = (Vector2.UnitX.RotatedBy(base.Projectile.rotation) * 10f).RotatedByRandom(0.39269909262657166);
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), Owner.Center, swordVel2, ModContent.ProjectileType<EarthenTidesBeam>(), (int)((float)base.Projectile.damage * OmegaBiomeBlade.ShockwaveAttunement_BeamDamageMult), 10f, Owner.whoAmI);
					}
					OverCharge = 20f;
					SoundEngine.PlaySound(in FullChargeSound, base.Projectile.Center);
					CurrentIndicator++;
				}
				if (Main.rand.NextBool())
				{
					Vector2 relativePosition = base.Projectile.Center + direction * Main.rand.NextFloat(50f, 100f);
					Vector2 sparkVel = direction.RotatedByRandom(0.20943951606750488) * Main.rand.NextFloat(9f, 18f) + Owner.velocity * 0.5f;
					Color sparkColor = Color.Lerp(new Color(71, 191, 71), new Color(122, 213, 233), Main.rand.NextFloat());
					GeneralParticleHandler.SpawnParticle(new CustomSprite(relativePosition, sparkVel, 16, "CalamityMod/Particles/CritSpark", 1.4f, sparkColor, 0f, AddativeBlend: true, needed: false, 4, Main.rand.Next(4)));
				}
			}
		}
		if (State == 1f)
		{
			base.Projectile.Center = Owner.Center + Vector2.Lerp(lastDisplacement, direction * 40f, MathHelper.Clamp((dashDuration - (float)base.Projectile.timeLeft) / dashDuration * 2f, 0f, 1f));
			Owner.fallStart = (int)(Owner.position.Y / 16f);
			Owner.Calamity().LungingDown = true;
			Vector2 collisionCheckPos = Owner.Center + direction * 120f * base.Projectile.scale - Vector2.One * 5f;
			if (Collision.SolidCollision(collisionCheckPos, 10, 10))
			{
				SlamDown(collisionCheckPos);
				base.Projectile.timeLeft = 0;
				Owner.Calamity().LungingDown = false;
				base.Projectile.active = false;
				base.Projectile.ForceNetUpdate();
			}
			Owner.velocity = direction * 30f;
			float variation = Main.rand.NextFloat(-(float)Math.PI / 4f, (float)Math.PI / 4f);
			float strength = (float)Math.Sin(variation * 2f + (float)Math.PI / 2f);
			GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center, Owner.velocity - direction.RotatedBy(variation) * (1f + strength) * 2f * Main.rand.NextFloat(7.5f, 20f), Color.White, Main.rand.NextBool() ? Color.MediumTurquoise : Color.DarkOrange, 0.1f + Main.rand.NextFloat(0f, 1.5f), 20 + Main.rand.Next(30), 1f, 3f));
		}
		Lighting.AddLight(base.Projectile.Center, new Vector3(1f, 0.56f, 0.56f) * Charge / 420f);
		base.Projectile.rotation = direction.ToRotation();
		base.Projectile.scale = 1f + Charge / 420f * 0.3f;
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

	public void SlamDown(Vector2 collisionSpot)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		if (Owner.whoAmI == Main.myPlayer && Owner.velocity.Y != 0f)
		{
			SoundEngine.PlaySound(in GroundImpact, base.Projectile.Center);
			Main.LocalPlayer.SetScreenshake(15f);
			for (int d = 0; d < 13; d++)
			{
				Dust.NewDustPerfect(collisionSpot, Main.rand.NextBool() ? 28 : 0, Main.rand.NextVector2CircularEdge(6f, 6f), 0, default(Color), 1.2f);
			}
			GeneralParticleHandler.SpawnParticle(new CustomPulse(collisionSpot, Vector2.Zero, Color.SandyBrown, "CalamityMod/Particles/ShatteredExplosion", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0.03f, 0.275f, 30, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			for (int i = 0; i < 10; i++)
			{
				Vector2 rockVel = -(Owner.velocity * 0.55f).RotatedByRandom(1.2566370964050293);
				GeneralParticleHandler.SpawnParticle(new StoneDebrisParticle(collisionSpot, rockVel, Color.White, 1f, 35));
			}
			int duration = 12 + (int)CurrentIndicator * 12;
			Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), Owner.Center, Vector2.Zero, ModContent.ProjectileType<EarthenTidesBlastSpawner>(), (int)((float)base.Projectile.damage * OmegaBiomeBlade.ShockwaveAttunement_MonolithDamageBoost), 0f, Owner.whoAmI, duration);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		Owner.GiveUniversalIFrames(OmegaBiomeBlade.ShockwaveAttunement_DashHitIFrames);
		if (!CalamityUtils.AnyProjectiles(ModContent.ProjectileType<EarthenTidesShockwave>()))
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<EarthenTidesShockwave>(), (int)((float)base.Projectile.damage * 0.75f), 0f, Owner.whoAmI, 1f);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		modifiers.SourceDamage *= OmegaBiomeBlade.ShockwaveAttunement_FullChargeMult * (float)Math.Pow(Charge / 420f, 2.0);
		if (Owner.HeldItem.ModItem is OmegaBiomeBlade sword && Main.rand.NextFloat() <= OmegaBiomeBlade.ShockwaveAttunement_SwordProc)
		{
			sword.OnHitProc = true;
		}
		Projectile[] projectile = Main.projectile;
		foreach (Projectile proj in projectile)
		{
			if (proj.active && proj.type == ModContent.ProjectileType<PurityProjectionSigil>() && proj.owner == Owner.whoAmI)
			{
				proj.ai[0] = target.whoAmI;
				proj.timeLeft = OmegaBiomeBlade.ShockwaveAttunement_SigilTime;
				return;
			}
		}
		Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<PurityProjectionSigil>(), 0, 0f, Owner.whoAmI, target.whoAmI, 1f).timeLeft = OmegaBiomeBlade.ShockwaveAttunement_SigilTime;
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
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		Texture2D handle = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/OmegaBiomeBlade", (AssetRequestMode)2).Value;
		Texture2D blade = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/TrueBiomeBlade_EarthenTides", (AssetRequestMode)2).Value;
		float drawRotation = direction.ToRotation() + (float)Math.PI / 4f;
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector(0f, (float)handle.Height);
		Vector2 drawOffset = base.Projectile.Center - Main.screenPosition;
		Main.EntitySpriteDraw(handle, drawOffset, null, lightColor, drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		if (OverCharge < 0f)
		{
			OverCharge = 0f;
		}
		GameShaders.Misc["CalamityMod:BasicTint"].UseOpacity(OverCharge / 20f);
		GameShaders.Misc["CalamityMod:BasicTint"].UseColor(new Color(154, 244, 240));
		GameShaders.Misc["CalamityMod:BasicTint"].Apply();
		((Vector2)(ref drawOrigin))._002Ector(0f, (float)blade.Height);
		Main.EntitySpriteDraw(blade, drawOffset, null, Color.Lerp(Color.White, lightColor, 0.5f) * 0.9f, drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		if (State == 1f)
		{
			Player owner = Owner;
			owner.velocity *= 0.33f;
		}
		Owner.Calamity().LungingDown = false;
		base.Projectile.active = false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(initialized);
		writer.WriteVector2(direction);
		writer.Write(CurrentIndicator);
		writer.WriteVector2(lastDisplacement);
		writer.Write(dashDuration);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		initialized = reader.ReadBoolean();
		direction = reader.ReadVector2();
		CurrentIndicator = reader.ReadSingle();
		lastDisplacement = reader.ReadVector2();
		dashDuration = reader.ReadSingle();
	}

	public EarthenTides()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		direction = Vector2.Zero;
		QuickOut = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyIn, 0f, 0f, 0.2f, 3);
		Bump = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineBump, 0.06f, 0.2f, 0.1f);
		QuickDraw = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.Linear, 0.25f, 0.2f, -0.45f);
		SlowDrawOut = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyIn, 0.5f, -0.25f, -0.2f, 3);
		OverShoot = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineBump, 0.93f, -0.45f, -0.1f);
		base._002Ector();
	}
}
