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

public class TrueBitingEmbrace : ModProjectile, ILocalizedModType, IModType
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

	public override string Texture => "CalamityMod/Projectiles/Melee/MendedBiomeBlade_BitingEmbrace";

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
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		float collisionPoint = 0f;
		float bladeLength = 0f;
		Vector2 displace = Vector2.Zero;
		float swingMode = SwingMode;
		if (swingMode != 0f && swingMode != 1f)
		{
			if (swingMode == 2f)
			{
				bladeLength = 225f * base.Projectile.scale;
				displace = direction * ThrustDisplaceRatio() * 60f;
			}
		}
		else
		{
			bladeLength = 160f * base.Projectile.scale;
		}
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Owner.Center + displace, Owner.Center + displace + rotation.ToRotationVector2() * bladeLength, 26f, ref collisionPoint);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		base.OnHitNPC(target, hit, damageDone);
		if (SwingMode == 2f)
		{
			target.AddBuff(ModContent.BuffType<GlacialState>(), 40);
		}
	}

	public override void AI()
	{
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0610: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0490: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_0512: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0550: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
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
						base.Projectile.width = (base.Projectile.height = 170);
						SoundEngine.PlaySound(in SoundID.DD2_PhantomPhoenixShot, base.Projectile.Center);
						base.Projectile.damage = (int)((float)base.Projectile.damage * TrueBiomeBlade.ColdAttunement_ThirdSwingBoost);
					}
				}
				else
				{
					base.Projectile.width = (base.Projectile.height = 100);
					base.Projectile.width = (base.Projectile.height = 100);
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
			base.Projectile.scale = 1f + ThrustScaleRatio() * 0.3f;
			base.Projectile.Center = Owner.Center + direction * ThrustDisplaceRatio() * 60f;
			base.Projectile.frameCounter++;
			if (base.Projectile.frameCounter % 5 == 0 && base.Projectile.frame + 1 < Main.projFrames[base.Type])
			{
				base.Projectile.frame++;
			}
			if (Timer % 2f == 0f && Owner.whoAmI == Main.myPlayer)
			{
				Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), Owner.Center + direction * 40f + Main.rand.NextVector2Circular(30f, 30f), Vector2.Zero, ModContent.ProjectileType<BitingEmbraceMist>(), (int)((float)base.Projectile.damage * TrueBiomeBlade.ColdAttunement_MistDamageReduction), 0f, Owner.whoAmI);
				projectile.velocity = (projectile.Center - Owner.Center) * 0.2f + Owner.velocity;
			}
		}
		else if (Timer % 2f == 0f && Owner.whoAmI == Main.myPlayer)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), Owner.Center + direction * 40f, rotation.ToRotationVector2() * 5f, ModContent.ProjectileType<BitingEmbraceMist>(), (int)((float)base.Projectile.damage * TrueBiomeBlade.ColdAttunement_MistDamageReduction), 0f, Owner.whoAmI);
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
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		Texture2D handle = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/MendedBiomeBlade", (AssetRequestMode)2).Value;
		if (SwingMode != 2f)
		{
			Texture2D blade = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/MendedBiomeBlade_BitingEmbrace", (AssetRequestMode)2).Value;
			float drawAngle = rotation;
			float drawRotation = rotation + (float)Math.PI / 4f;
			Vector2 drawOrigin = default(Vector2);
			((Vector2)(ref drawOrigin))._002Ector(0f, (float)handle.Height);
			Vector2 drawOffset = Owner.Center + drawAngle.ToRotationVector2() * 10f - Main.screenPosition;
			Main.EntitySpriteDraw(handle, drawOffset, null, lightColor, drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			((Vector2)(ref drawOrigin))._002Ector(0f, (float)blade.Height);
			Main.EntitySpriteDraw(blade, drawOffset, null, Color.Lerp(Color.White, lightColor, 0.5f) * 0.8f, drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		}
		else
		{
			Texture2D blade2 = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/MendedBiomeBlade_BitingEmbraceThrust", (AssetRequestMode)2).Value;
			Vector2 thrustDisplace = direction * (ThrustDisplaceRatio() * 60f);
			float drawAngle2 = rotation;
			float drawRotation2 = rotation + (float)Math.PI / 4f;
			Vector2 drawOrigin2 = default(Vector2);
			((Vector2)(ref drawOrigin2))._002Ector(0f, (float)handle.Height);
			Vector2 drawOffset2 = Owner.Center + drawAngle2.ToRotationVector2() * 10f - Main.screenPosition;
			Main.EntitySpriteDraw(handle, drawOffset2 + thrustDisplace, null, lightColor, drawRotation2, drawOrigin2, base.Projectile.scale, (SpriteEffects)0);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			((Vector2)(ref drawOrigin2))._002Ector(0f, (float)blade2.Height);
			Main.EntitySpriteDraw(blade2, drawOffset2 + thrustDisplace, null, Color.Lerp(Color.White, lightColor, 0.5f) * 0.9f, drawRotation2, drawOrigin2, base.Projectile.scale, (SpriteEffects)0);
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

	public TrueBitingEmbrace()
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
