using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class ArkoftheElementsParryHoldout : ModProjectile, ILocalizedModType, IModType
{
	private bool initialized;

	private const float MaxTime = 340f;

	private static float ParryTime = 15f;

	public CalamityUtils.CurveSegment anticipation = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineBump, 0f, 0.2f, -0.05f);

	public CalamityUtils.CurveSegment thrust = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyInOut, 0.2f, 0.2f, 0.8f, 2);

	public CalamityUtils.CurveSegment retract = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.CircIn, 0.7f, 1f, -0.1f);

	public CalamityUtils.CurveSegment openMore = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineBump, 0f, 0f, -0.15f);

	public CalamityUtils.CurveSegment close = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyIn, 0.3f, 0f, 1f, 4);

	public CalamityUtils.CurveSegment stayClosed = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.Linear, 0.5f, 1f, 0f);

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Melee/RendingScissorsRight";

	public Vector2 DistanceFromPlayer
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.velocity * 10f + base.Projectile.velocity * 10f * ThrustDisplaceRatio();
		}
	}

	public float Timer => 340f - (float)base.Projectile.timeLeft;

	public float ParryProgress => (340f - (float)base.Projectile.timeLeft) / ParryTime;

	public ref float AlreadyParried => ref base.Projectile.ai[1];

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.width = (base.Projectile.height = 75);
		base.Projectile.width = (base.Projectile.height = 75);
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.noEnchantmentVisuals = true;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override bool? CanDamage()
	{
		return Timer <= ParryTime && AlreadyParried == 0f;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		float collisionPoint = 0f;
		float bladeLength = 142f * base.Projectile.scale;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Owner.Center + DistanceFromPlayer, Owner.Center + DistanceFromPlayer + base.Projectile.velocity * bladeLength, 44f, ref collisionPoint);
	}

	public void GeneralParryEffects()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		if (Owner.HeldItem.ModItem is ArkoftheElements sword)
		{
			sword.Charge = 10f;
			sword.Combo = 0f;
		}
		SoundEngine.PlaySound(in SoundID.DD2_WitherBeastCrystalImpact);
		SoundEngine.PlaySound(CommonCalamitySounds.ScissorGuillotineSnapSound with
		{
			Volume = CommonCalamitySounds.ScissorGuillotineSnapSound.Volume * 1.3f
		}, base.Projectile.Center);
		CombatText.NewText(base.Projectile.Hitbox, new Color(111, 247, 200), CalamityUtils.GetTextValue("Misc.ArkParry"), dramatic: true);
		for (int i = 0; i < 5; i++)
		{
			Vector2 particleDispalce = Main.rand.NextVector2Circular((float)Owner.Hitbox.Width * 2f, (float)Owner.Hitbox.Height * 1.2f);
			float particleScale = Main.rand.NextFloat(0.5f, 1.4f);
			GeneralParticleHandler.SpawnParticle(new FlareShine(Owner.Center + particleDispalce, particleDispalce * 0.01f, Color.White, Color.Red, 0f, new Vector2(0.6f, 1f) * particleScale, new Vector2(1.5f, 2.7f) * particleScale, 20 + Main.rand.Next(6), 0f, 3f, 0f, Main.rand.Next(7) * 2));
		}
		AlreadyParried = 1f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<ElementalMix>(), 90);
		if (!(AlreadyParried > 0f))
		{
			GeneralParryEffects();
			if (target.damage > 0)
			{
				int arkParryIFrames = Owner.ComputeParryIFrames();
				Owner.GiveUniversalIFrames(arkParryIFrames);
			}
			Vector2 val = target.Hitbox.Size();
			Vector2 particleOrigin = ((((Vector2)(ref val)).Length() < 140f) ? target.Center : (base.Projectile.Center + base.Projectile.rotation.ToRotationVector2() * 60f));
			GeneralParticleHandler.SpawnParticle(new GenericSparkle(particleOrigin, Vector2.Zero, Color.White, Color.HotPink, 1.2f, 35, 0.1f, 2f));
			for (int i = 0; i < 10; i++)
			{
				Vector2 particleSpeed = Main.rand.NextVector2CircularEdge(1f, 1f) * Main.rand.NextFloat(2.6f, 4f);
				GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(particleOrigin, particleSpeed, Main.rand.NextFloat(0.3f, 0.6f), Color.Cyan, 60, 1f, 1.5f, 3f, 0.02f));
			}
		}
	}

	public override void AI()
	{
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		if (!initialized)
		{
			base.Projectile.timeLeft = 340;
			SoundStyle style = SoundID.Item84 with
			{
				Volume = SoundID.Item84.Volume * 0.3f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.Projectile.velocity = Owner.SafeDirectionTo(Owner.Calamity().mouseWorld, Vector2.Zero);
			((Vector2)(ref base.Projectile.velocity)).Normalize();
			base.Projectile.rotation = base.Projectile.velocity.ToRotation();
			initialized = true;
			base.Projectile.ForceNetUpdate();
		}
		base.Projectile.Center = Owner.Center + DistanceFromPlayer;
		base.Projectile.scale = 1.4f + ThrustDisplaceRatio() * 0.2f;
		if (Timer > ParryTime)
		{
			return;
		}
		float collisionPoint = 0f;
		float bladeLength = 142f * base.Projectile.scale;
		for (int k = 0; k < Main.maxProjectiles; k++)
		{
			Projectile proj = Main.projectile[k];
			if (!proj.active || !proj.hostile || proj.damage <= 1 || !(((Vector2)(ref proj.velocity)).Length() * (float)(proj.extraUpdates + 1) > 1f))
			{
				continue;
			}
			Vector2 size = proj.Size;
			if (!(((Vector2)(ref size)).Length() < 300f) || !Collision.CheckAABBvLineCollision(proj.Hitbox.TopLeft(), proj.Hitbox.Size(), Owner.Center + DistanceFromPlayer, Owner.Center + DistanceFromPlayer + base.Projectile.velocity * bladeLength, 24f, ref collisionPoint))
			{
				continue;
			}
			if (AlreadyParried == 0f)
			{
				GeneralParryEffects();
				if (Owner.velocity.Y != 0f)
				{
					Player owner = Owner;
					owner.velocity += (Owner.Center - proj.Center).SafeNormalize(Vector2.Zero) * 2f;
				}
			}
			if (proj.Calamity().flatDR < 100)
			{
				proj.Calamity().flatDR = 100;
			}
			if (proj.Calamity().flatDRTimer < 60)
			{
				proj.Calamity().flatDRTimer = 60;
			}
			break;
		}
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.ChangeDir(Math.Sign(base.Projectile.velocity.X));
		Owner.itemRotation = base.Projectile.rotation;
		if (Owner.direction != 1)
		{
			Owner.itemRotation -= (float)Math.PI;
		}
		Owner.itemRotation = MathHelper.WrapAngle(Owner.itemRotation);
		if (AlreadyParried > 0f)
		{
			AlreadyParried++;
		}
	}

	internal float ThrustDisplaceRatio()
	{
		return CalamityUtils.PiecewiseAnimation(ParryProgress, anticipation, thrust, retract);
	}

	internal float RotationRatio()
	{
		return CalamityUtils.PiecewiseAnimation(ParryProgress, openMore, close, stayClosed);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		if (Timer > ParryTime)
		{
			if (Main.myPlayer == Owner.whoAmI)
			{
				Texture2D barBG = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/GenericBarBack", (AssetRequestMode)2).Value;
				Texture2D barFG = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/GenericBarFront", (AssetRequestMode)2).Value;
				Vector2 drawPos = Owner.Center - Main.screenPosition + new Vector2(0f, -36f) - barBG.Size() / 2f;
				Rectangle frame = default(Rectangle);
				((Rectangle)(ref frame))._002Ector(0, 0, (int)((Timer - ParryTime) / (340f - ParryTime) * (float)barFG.Width), barFG.Height);
				float opacity = ((Timer <= ParryTime + 25f) ? ((Timer - ParryTime) / 25f) : ((340f - Timer <= 8f) ? ((float)base.Projectile.timeLeft / 8f) : 1f));
				Color color = Main.hslToRgb((float)Math.Sin(Main.GlobalTimeWrappedHourly * 1.2f) * 0.05f + 0.08f, 1f, 0.65f + (float)Math.Sin(Main.GlobalTimeWrappedHourly * 7f) * 0.1f);
				Main.spriteBatch.Draw(barBG, drawPos, color * opacity);
				Main.spriteBatch.Draw(barFG, drawPos, (Rectangle?)frame, color * opacity * 0.8f);
			}
			return false;
		}
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/RendingScissorsRight", (AssetRequestMode)2).Value;
		Texture2D frontBladeGlow = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/RendingScissorsRightGlow", (AssetRequestMode)2).Value;
		Texture2D value2 = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/RendingScissorsLeft", (AssetRequestMode)2).Value;
		Texture2D backBladeGlow = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/RendingScissorsLeftGlow", (AssetRequestMode)2).Value;
		float snippingRotation = base.Projectile.rotation + (float)Math.PI / 4f;
		float snippingRotationBack = base.Projectile.rotation + (float)Math.PI * 7f / 16f;
		float drawRotation = MathHelper.Lerp(snippingRotation + (float)Math.PI / 4f, snippingRotation, RotationRatio());
		float drawRotationBack = MathHelper.Lerp(snippingRotationBack - (float)Math.PI / 4f, snippingRotationBack, RotationRatio());
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector(51f, 86f);
		Vector2 drawOriginBack = default(Vector2);
		((Vector2)(ref drawOriginBack))._002Ector(22f, 109f);
		Vector2 drawPosition = Owner.Center + base.Projectile.velocity * 15f + base.Projectile.velocity * ThrustDisplaceRatio() * 50f - Main.screenPosition;
		Main.EntitySpriteDraw(value2, drawPosition, null, lightColor, drawRotationBack, drawOriginBack, base.Projectile.scale, (SpriteEffects)0);
		Main.EntitySpriteDraw(backBladeGlow, drawPosition, null, Color.Lerp(lightColor, Color.White, 0.75f), drawRotationBack, drawOriginBack, base.Projectile.scale, (SpriteEffects)0);
		Main.EntitySpriteDraw(value, drawPosition, null, lightColor, drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
		Main.EntitySpriteDraw(frontBladeGlow, drawPosition, null, Color.Lerp(lightColor, Color.White, 0.75f), drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == Owner.whoAmI)
		{
			SoundStyle style = SoundID.Item35 with
			{
				Volume = SoundID.Item35.Volume * 2f
			};
			SoundEngine.PlaySound(in style);
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(initialized);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		initialized = reader.ReadBoolean();
	}
}
