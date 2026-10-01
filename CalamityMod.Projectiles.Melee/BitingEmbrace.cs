using System;
using System.IO;
using CalamityMod.Buffs.StatDebuffs;
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

public class BitingEmbrace : ModProjectile, ILocalizedModType, IModType
{
	private bool initialized;

	private Vector2 direction;

	public float rotation;

	public CalamityUtils.CurveSegment anticipation;

	public CalamityUtils.CurveSegment thrust;

	public CalamityUtils.CurveSegment hold;

	public CalamityUtils.CurveSegment retract;

	public CalamityUtils.CurveSegment expandSize;

	public CalamityUtils.CurveSegment holdSize;

	public CalamityUtils.CurveSegment shrinkSize;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Melee/BrokenBiomeBlade_BitingEmbraceSmall";

	public ref float SwingMode => ref base.Projectile.ai[0];

	public ref float MaxTime => ref base.Projectile.ai[1];

	public float Timer => MaxTime - (float)base.Projectile.timeLeft;

	public int SwingDirection
	{
		get
		{
			float swingMode = SwingMode;
			if (swingMode != 0f)
			{
				if (swingMode == 1f)
				{
					return Math.Sign(direction.X);
				}
				return 0;
			}
			return -1 * Math.Sign(direction.X);
		}
	}

	public float SwingWidth
	{
		get
		{
			if (SwingMode == 0f)
			{
				return 2.3f;
			}
			return 1.8f;
		}
	}

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.width = (base.Projectile.height = 75);
		base.Projectile.width = (base.Projectile.height = 75);
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		float collisionPoint = 0f;
		float bladeLength = 0f;
		Vector2 displace = Vector2.Zero;
		float swingMode = SwingMode;
		if (swingMode != 0f)
		{
			if (swingMode != 1f)
			{
				if (swingMode == 2f)
				{
					bladeLength = ((base.Projectile.frame <= 2) ? 85f : 180f);
					bladeLength *= base.Projectile.scale;
					displace = direction * ThrustDisplaceRatio() * 60f;
				}
			}
			else
			{
				bladeLength = 120f * base.Projectile.scale;
			}
		}
		else
		{
			bladeLength = 95f * base.Projectile.scale;
		}
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Owner.Center + displace, Owner.Center + displace + rotation.ToRotationVector2() * bladeLength, 24f, ref collisionPoint);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		base.OnHitNPC(target, hit, damageDone);
		if (SwingMode == 2f)
		{
			target.AddBuff(ModContent.BuffType<GlacialState>(), 20);
		}
	}

	public override void AI()
	{
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_052f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_0541: Unknown result type (might be due to invalid IL or missing references)
		//IL_0546: Unknown result type (might be due to invalid IL or missing references)
		//IL_054b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		if (!initialized)
		{
			base.Projectile.timeLeft = (int)MaxTime;
			float swingMode = SwingMode;
			if (swingMode != 0f)
			{
				if (swingMode != 1f)
				{
					if (swingMode == 2f)
					{
						base.Projectile.width = (base.Projectile.height = 130);
						SoundEngine.PlaySound(in SoundID.DD2_PhantomPhoenixShot, base.Projectile.Center);
						base.Projectile.damage = (int)((float)base.Projectile.damage * BrokenBiomeBlade.ColdAttunement_ThirdSwingBoost);
					}
				}
				else
				{
					base.Projectile.width = (base.Projectile.height = 140);
					SoundEngine.PlaySound(in SoundID.DD2_OgreSpit, base.Projectile.Center);
				}
			}
			else
			{
				base.Projectile.width = (base.Projectile.height = 100);
				SoundEngine.PlaySound(in SoundID.DD2_MonkStaffSwing, base.Projectile.Center);
			}
			direction = Owner.SafeDirectionTo(Owner.Calamity().mouseWorld, Vector2.Zero);
			((Vector2)(ref direction)).Normalize();
			base.Projectile.rotation = direction.ToRotation();
			initialized = true;
			base.Projectile.ForceNetUpdate();
		}
		base.Projectile.Center = Owner.Center + direction * 30f;
		float factor = 1f - (float)Math.Pow((double)(0f - Timer / MaxTime) + 1.0, 2.0);
		rotation = base.Projectile.rotation + MathHelper.Lerp(SwingWidth / 2f * (float)SwingDirection, (0f - SwingWidth) / 2f * (float)SwingDirection, factor);
		base.Projectile.scale = 1f + (float)Math.Sin(Timer / MaxTime * (float)Math.PI) * 0.6f;
		Lighting.AddLight(Owner.MountedCenter, new Vector3(0.75f, 1f, 1f) * (float)Math.Sin(Timer / MaxTime * (float)Math.PI));
		if (SwingMode == 2f)
		{
			base.Projectile.scale = 1f + ThrustScaleRatio() * 0.6f;
			base.Projectile.Center = Owner.Center + direction * ThrustDisplaceRatio() * 60f;
			base.Projectile.frameCounter++;
			if (base.Projectile.frameCounter % 5 == 0 && base.Projectile.frame + 1 < Main.projFrames[base.Type])
			{
				base.Projectile.frame++;
			}
			if (Main.rand.NextBool())
			{
				Color initialColor = Color.Lerp(new Color(172, 238, 255), new Color(230, 172, 255), Main.rand.NextFloat(1f));
				MediumMistParticle mediumMistParticle = new MediumMistParticle(Owner.Center + direction * 40f + Main.rand.NextVector2Circular(30f, 30f), Vector2.Zero, initialColor, new Color(145, 170, 188), Main.rand.NextFloat(0.5f, 1.5f), 245 - Main.rand.Next(50), 0.02f);
				mediumMistParticle.Velocity = (mediumMistParticle.Position - Owner.Center) * 0.2f + Owner.velocity;
				GeneralParticleHandler.SpawnParticle(mediumMistParticle);
			}
		}
		else if (Main.rand.NextFloat(0f, 1f) > 0.6f)
		{
			Vector2 particlePosition = Owner.Center + rotation.ToRotationVector2() * 100f * base.Projectile.scale;
			if (Main.rand.NextBool())
			{
				GeneralParticleHandler.SpawnParticle(new SnowflakeSparkle(particlePosition, rotation.ToRotationVector2() * 3f, Color.White, new Color(75, 177, 250), Main.rand.NextFloat(0.3f, 1.5f), 40, 0.5f));
			}
			else
			{
				float scale = Main.rand.NextFloat(0.5f, 1.8f);
				GeneralParticleHandler.SpawnParticle(new CritSpark(particlePosition, rotation.ToRotationVector2() * 3f, Color.White, Color.Indigo, scale, 30, 0.5f, scale * 2f));
			}
		}
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.ChangeDir(Math.Sign(rotation.ToRotationVector2().X));
		Owner.itemRotation = rotation;
		if (Owner.direction != 1)
		{
			Owner.itemRotation -= (float)Math.PI;
		}
		Owner.itemRotation = MathHelper.WrapAngle(Owner.itemRotation);
	}

	internal float ThrustDisplaceRatio()
	{
		return CalamityUtils.PiecewiseAnimation(Timer / MaxTime, anticipation, thrust, hold, retract);
	}

	internal float ThrustScaleRatio()
	{
		return CalamityUtils.PiecewiseAnimation(Timer / MaxTime, expandSize, holdSize, shrinkSize);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		Texture2D handle = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/BrokenBiomeBlade", (AssetRequestMode)2).Value;
		if (SwingMode != 2f)
		{
			Texture2D blade = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/BrokenBiomeBlade_BitingEmbrace" + ((SwingMode == 0f) ? "Small" : "Big"), (AssetRequestMode)2).Value;
			float drawAngle = rotation;
			float drawRotation = rotation + (float)Math.PI / 4f;
			Vector2 drawOrigin = default(Vector2);
			((Vector2)(ref drawOrigin))._002Ector(0f, (float)handle.Height);
			Vector2 drawOffset = Owner.Center + drawAngle.ToRotationVector2() * 10f - Main.screenPosition;
			Main.EntitySpriteDraw(handle, drawOffset, null, lightColor, drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			((Vector2)(ref drawOrigin))._002Ector(0f, (float)blade.Height);
			drawOffset = Owner.Center + drawAngle.ToRotationVector2() * 28f * base.Projectile.scale - Main.screenPosition;
			Main.EntitySpriteDraw(blade, drawOffset, null, Color.Lerp(Color.White, lightColor, 0.5f) * 0.8f, drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		}
		else
		{
			Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/BrokenBiomeBlade_BitingEmbraceThrust", (AssetRequestMode)2).Value;
			Vector2 thrustDisplace = direction * (ThrustDisplaceRatio() * 60f);
			float drawAngle2 = rotation;
			float drawRotation2 = rotation + (float)Math.PI / 4f;
			Vector2 drawOrigin2 = default(Vector2);
			((Vector2)(ref drawOrigin2))._002Ector(0f, (float)handle.Height);
			Vector2 drawOffset2 = Owner.Center + drawAngle2.ToRotationVector2() * 10f - Main.screenPosition;
			Main.EntitySpriteDraw(handle, drawOffset2 + thrustDisplace, null, lightColor, drawRotation2, drawOrigin2, base.Projectile.scale, (SpriteEffects)0);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			((Vector2)(ref drawOrigin2))._002Ector(0f, 114f);
			drawOffset2 = Owner.Center + direction * 28f * base.Projectile.scale - Main.screenPosition;
			Rectangle frameRectangle = default(Rectangle);
			((Rectangle)(ref frameRectangle))._002Ector(0, 116 * base.Projectile.frame, 114, 114);
			Main.EntitySpriteDraw(value, drawOffset2 + thrustDisplace, frameRectangle, Color.Lerp(Color.White, lightColor, 0.5f) * 0.9f, drawRotation2, drawOrigin2, base.Projectile.scale, (SpriteEffects)0);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		}
		return false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(initialized);
		writer.WriteVector2(direction);
		writer.Write(rotation);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		initialized = reader.ReadBoolean();
		direction = reader.ReadVector2();
		rotation = reader.ReadSingle();
	}

	public BitingEmbrace()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		direction = Vector2.Zero;
		anticipation = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineBump, 0f, 0f, -0.15f);
		thrust = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyInOut, 0.2f, 0f, 0.9f, 3);
		hold = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.SineBump, 0.35f, 0.9f, 0.1f);
		retract = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyInOut, 0.7f, 0.9f, -0.9f, 3);
		expandSize = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.ExpIn, 0f, 0f, 1f);
		holdSize = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.Linear, 0.1f, 1f, 0f);
		shrinkSize = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.ExpIn, 0.85f, 1f, -1f);
		base._002Ector();
	}
}
