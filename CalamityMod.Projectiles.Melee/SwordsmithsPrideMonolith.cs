using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class SwordsmithsPrideMonolith : ModProjectile, ILocalizedModType, IModType
{
	public float Scale;

	public Vector2 OriginDirection;

	public float Facing;

	public NPC Target;

	public const float BaseWidth = 90f;

	public const float BaseHeight = 420f;

	public CalamityUtils.CurveSegment StaySmall = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.Linear, 0f, 0.2f, 0f);

	public CalamityUtils.CurveSegment GoBig = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineOut, 0.25f, 0.2f, 1f);

	public CalamityUtils.CurveSegment GoNormal = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.CircIn, 0.4f, 1.2f, -0.2f);

	public CalamityUtils.CurveSegment StayNormal = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.Linear, 0.5f, 1f, 0f);

	public CalamityUtils.CurveSegment Anticipate = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.CircIn, 0f, 0f, 0.15f);

	public CalamityUtils.CurveSegment Overextend = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineOut, 0.2f, 0.15f, 1f);

	public CalamityUtils.CurveSegment Unextend = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.CircIn, 0.25f, 1.15f, -0.15f);

	public CalamityUtils.CurveSegment Hold = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.ExpOut, 0.7f, 1f, -0.1f);

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Melee/TrueBiomeBlade_SwordsmithsPrideMonolith";

	public Player Owner => Main.player[base.Projectile.owner];

	public float Timer => (100f - (float)base.Projectile.timeLeft) / 100f;

	public ref float Variant => ref base.Projectile.ai[0];

	public ref float Size => ref base.Projectile.ai[1];

	internal float Width()
	{
		return CalamityUtils.PiecewiseAnimation(Timer, StaySmall, GoBig, GoNormal, StayNormal) * 90f * Size;
	}

	internal float Height()
	{
		return CalamityUtils.PiecewiseAnimation(Timer, Anticipate, Overextend, Unextend, Hold) * 420f * Size;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.width = (base.Projectile.height = 70);
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 102;
		base.Projectile.scale = Scale;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
		base.Projectile.hide = true;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		float collisionPoint = 0f;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + (base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2() * Height(), Width(), ref collisionPoint);
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		behindNPCsAndTiles.Add(index);
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity != Vector2.Zero)
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
			base.Projectile.velocity = Vector2.Zero;
		}
		if (!Target.active)
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.Center = Target.Center;
		if (base.Projectile.timeLeft == 100)
		{
			if (Size >= 1f)
			{
				for (int i = 0; i < 20; i++)
				{
					GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center, Main.rand.NextVector2Circular(1f, 1f) * Main.rand.NextFloat(7.5f, 20f), Color.White, Main.rand.NextBool() ? Color.MediumTurquoise : Color.DarkOrange, 0.1f + Main.rand.NextFloat(0f, 1.5f), 20 + Main.rand.Next(30), 1f, 3f));
				}
			}
			if (base.Projectile.ai[2] > 1f)
			{
				if (Facing == 0f)
				{
					SideSprouts(-1f, Size * 0.8f);
					SideSprouts(1f, Size * 0.8f);
				}
				else
				{
					SideSprouts(Facing, Size * 0.8f);
				}
			}
		}
		if (base.Projectile.timeLeft == 80)
		{
			Vector2 particleDirection = (base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2();
			SoundEngine.PlaySound(in SoundID.DD2_EtherianPortalDryadTouch, base.Projectile.Center);
			for (int j = 0; j < 8; j++)
			{
				Vector2 hitPositionDisplace = particleDirection.RotatedBy(1.5707963705062866) * Main.rand.NextFloat(0f, 10f);
				Vector2 flyDirection = particleDirection.RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 2f, (float)Math.PI / 2f)) * Main.rand.NextFloat(5f, 15f);
				GeneralParticleHandler.SpawnParticle(new SmallSmokeParticle(base.Projectile.Center + hitPositionDisplace, flyDirection, Color.Lerp(Color.DarkOrange, Color.MediumTurquoise, Main.rand.NextFloat()), new Color(130, 130, 130), Main.rand.NextFloat(2.8f, 3.6f) * Size, 165 - Main.rand.Next(30), 0.1f));
			}
			for (int k = 0; k < 6; k++)
			{
				Vector2 hitPositionDisplace2 = particleDirection.RotatedBy(1.5707963705062866) * Main.rand.NextFloat(10f, 30f);
				Vector2 flyDirection2 = particleDirection.RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 4f, (float)Math.PI / 4f));
				GeneralParticleHandler.SpawnParticle(new StoneDebrisParticle(base.Projectile.Center + hitPositionDisplace2 * 3f, flyDirection2 * Main.rand.NextFloat(3f, 6f), Color.Lerp(Color.DarkSlateBlue, Color.LightSlateGray, Main.rand.NextFloat()), (1f + Main.rand.NextFloat(0f, 2.4f)) * Size, 30 + Main.rand.Next(50), 0.1f));
			}
		}
	}

	public void SideSprouts(float facing, float projSize)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		Vector2 monolithRotation = OriginDirection.RotatedBy(0.33069396f * facing);
		if (Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, monolithRotation, ModContent.ProjectileType<SwordsmithsPrideMonolith>(), base.Projectile.damage, 10f, Owner.whoAmI, Main.rand.Next(4), projSize, base.Projectile.ai[2] - 1f).ModProjectile is SwordsmithsPrideMonolith monolith)
		{
			monolith.Scale = Scale;
			monolith.OriginDirection = monolithRotation;
			monolith.Facing = facing;
			monolith.Target = Target;
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (Owner.HeldItem.ModItem is OmegaBiomeBlade sword && Main.rand.NextFloat() <= OmegaBiomeBlade.WhirlwindAttunement_MonolithProc)
		{
			sword.OnHitProc = true;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/TrueBiomeBlade_SwordsmithsPrideMonolith", (AssetRequestMode)2).Value;
		Vector2 Shake = ((base.Projectile.timeLeft < 70) ? Vector2.Zero : (Vector2.One.RotatedByRandom(6.2831854820251465) * (70f - (float)base.Projectile.timeLeft / 30f) * 0.05f));
		float drawAngle = base.Projectile.rotation;
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector((int)Variant * 94, 0, 94, 420);
		Vector2 drawScale = new Vector2(Width() / 90f, Height() / 420f) * Scale;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition - (base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2() * 26f * Scale;
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector((float)frame.Width / 2f, (float)frame.Height);
		float opacity = MathHelper.Clamp(1f - (Timer - 0.85f) / 0.15f, 0f, 1f);
		Main.EntitySpriteDraw(value, drawPosition + Shake, frame, lightColor * opacity, drawAngle, drawOrigin, drawScale, (SpriteEffects)0);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/MendedBiomeBlade_HeavensMonolith_Glow", (AssetRequestMode)2).Value;
		float drawAngle = base.Projectile.rotation;
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector((int)Variant * 94, 0, 94, 420);
		Vector2 drawScale = new Vector2(Width() / 90f, Height() / 420f) * Scale;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition - (base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2() * 26f * Scale;
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector((float)frame.Width / 2f, (float)frame.Height);
		float opacity = MathHelper.Clamp(1f - (Timer - 0.85f) / 0.15f, 0f, 1f);
		Main.EntitySpriteDraw(value, drawPosition, frame, Color.White * opacity, drawAngle, drawOrigin, drawScale, (SpriteEffects)0);
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(Facing);
		writer.WriteVector2(OriginDirection);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		Facing = reader.ReadSingle();
		OriginDirection = reader.ReadVector2();
	}
}
