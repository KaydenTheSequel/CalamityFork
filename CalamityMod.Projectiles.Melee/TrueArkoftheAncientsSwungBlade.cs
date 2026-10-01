using System;
using System.IO;
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

public class TrueArkoftheAncientsSwungBlade : ModProjectile, ILocalizedModType, IModType
{
	private bool initialized;

	private Vector2 direction;

	private const float MaxTime = 40f;

	private float SwingWidth;

	public CalamityUtils.CurveSegment anticipation;

	public CalamityUtils.CurveSegment thrust;

	public CalamityUtils.CurveSegment hold;

	public CalamityUtils.CurveSegment retract;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Items/Weapons/Melee/TrueArkoftheAncients";

	public float SwingDirection => base.Projectile.ai[0] * (float)Math.Sign(direction.X);

	public ref float Charge => ref base.Projectile.ai[1];

	public ref float HasFired => ref base.Projectile.localAI[0];

	public Vector2 DistanceFromPlayer
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			return direction * 30f;
		}
	}

	public float Timer => 40f - (float)base.Projectile.timeLeft;

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = TrueMeleeNoSpeedDamageClass.Instance;
		base.Projectile.width = (base.Projectile.height = 60);
		base.Projectile.width = (base.Projectile.height = 60);
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 40;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		float collisionPoint = 0f;
		float bladeLength = 88f * base.Projectile.scale;
		Vector2 distanceFromPlayer = DistanceFromPlayer;
		Vector2 holdPoint = ((Vector2)(ref distanceFromPlayer)).Length() * base.Projectile.rotation.ToRotationVector2();
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Owner.Center + holdPoint, Owner.Center + holdPoint + base.Projectile.rotation.ToRotationVector2() * bladeLength, 24f, ref collisionPoint);
	}

	internal float SwingRatio()
	{
		return CalamityUtils.PiecewiseAnimation(Timer / 40f, anticipation, thrust, hold);
	}

	public override void AI()
	{
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		if (!initialized)
		{
			base.Projectile.timeLeft = 40;
			SoundEngine.PlaySound((Charge > 0f) ? CommonCalamitySounds.LouderPhantomPhoenix : SoundID.DD2_MonkStaffSwing, base.Projectile.Center);
			direction = base.Projectile.velocity;
			((Vector2)(ref direction)).Normalize();
			base.Projectile.rotation = direction.ToRotation();
			initialized = true;
			base.Projectile.ForceNetUpdate();
		}
		base.Projectile.Center = Owner.Center + DistanceFromPlayer;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + MathHelper.Lerp(SwingWidth / 2f * SwingDirection, (0f - SwingWidth) / 2f * SwingDirection, SwingRatio());
		base.Projectile.scale = 1.6f + (float)Math.Sin(SwingRatio() * (float)Math.PI) * 0.65f + ((Charge > 0f) ? 0.6f : 0f);
		if (Owner.whoAmI == Main.myPlayer && SwingRatio() > 0.5f && HasFired == 0f && Charge > 0f)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), Owner.Center + direction * 30f, base.Projectile.velocity * 2f, ModContent.ProjectileType<TrueAncientBeam>(), (int)((float)base.Projectile.damage * TrueArkoftheAncients.beamDamageMultiplier), 2f, Owner.whoAmI);
			HasFired = 1f;
		}
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.ChangeDir(Math.Sign(base.Projectile.velocity.X));
		Owner.itemRotation = base.Projectile.rotation;
		if (Owner.direction != 1)
		{
			Owner.itemRotation -= (float)Math.PI;
		}
		Owner.itemRotation = MathHelper.WrapAngle(Owner.itemRotation);
		if (Charge > 0f && Main.rand.NextBool(2))
		{
			Vector2 position = base.Projectile.Center + base.Projectile.rotation.ToRotationVector2() * 85f * base.Projectile.scale;
			Vector2 particleSpeed = base.Projectile.rotation.ToRotationVector2().RotatedByRandom(0.7853981852531433) * Main.rand.NextFloat(1.2f, 2f);
			GeneralParticleHandler.SpawnParticle(new CritSpark(position, particleSpeed + Owner.velocity, Color.White, Color.Cyan, Main.rand.NextFloat(0.6f, 1.6f), 20 + Main.rand.Next(10), 0.1f, 1.5f, 0.02f));
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		Texture2D sword = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/TrueArkoftheAncients", (AssetRequestMode)2).Value;
		Texture2D glowmask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/TrueArkoftheAncientsGlow", (AssetRequestMode)2).Value;
		SpriteEffects flip = (SpriteEffects)(Owner.direction < 0);
		float extraAngle = ((Owner.direction < 0) ? ((float)Math.PI / 2f) : 0f);
		float drawAngle = base.Projectile.rotation;
		float drawRotation = base.Projectile.rotation + (float)Math.PI / 4f + extraAngle;
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector((Owner.direction < 0) ? ((float)sword.Width) : 0f, (float)sword.Height);
		Vector2 drawOffset = Owner.Center + drawAngle.ToRotationVector2() * 10f - Main.screenPosition;
		if (CalamityClientConfig.Instance.Afterimages && Timer > (float)ProjectileID.Sets.TrailCacheLength[base.Type])
		{
			for (int i = 0; i < base.Projectile.oldRot.Length; i++)
			{
				Color color = Main.hslToRgb((float)i / (float)base.Projectile.oldRot.Length * 0.7f, 1f, 0.6f + ((Charge > 0f) ? 0.3f : 0f));
				float afterimageRotation = base.Projectile.oldRot[i] + (float)Math.PI / 4f;
				Main.EntitySpriteDraw(glowmask, drawOffset, null, color * 0.15f, afterimageRotation + extraAngle, drawOrigin, base.Projectile.scale - 0.2f * ((float)i / (float)base.Projectile.oldRot.Length), flip);
			}
		}
		Main.EntitySpriteDraw(sword, drawOffset, null, lightColor, drawRotation, drawOrigin, base.Projectile.scale, flip);
		Main.EntitySpriteDraw(glowmask, drawOffset, null, Color.Lerp(lightColor, Color.White, 0.75f), drawRotation, drawOrigin, base.Projectile.scale, flip);
		if (Charge > 0f && Timer / 40f > 0.5f)
		{
			Texture2D smear = ModContent.Request<Texture2D>("CalamityMod/Particles/TrientCircularSmear", (AssetRequestMode)2).Value;
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			float opacity = (float)Math.Sin(Timer / 40f * (float)Math.PI);
			float rotation = (-(float)Math.PI / 8f + (float)Math.PI / 8f * Timer / 40f) * SwingDirection;
			Color smearColor = Main.hslToRgb((Timer - 20f) / 20f * 0.7f, 1f, 0.6f);
			Main.EntitySpriteDraw(smear, Owner.Center - Main.screenPosition, null, smearColor * 0.5f * opacity, base.Projectile.velocity.ToRotation() + (float)Math.PI + rotation, smear.Size() / 2f, base.Projectile.scale * 1.5f, (SpriteEffects)0);
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
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		initialized = reader.ReadBoolean();
		direction = reader.ReadVector2();
	}

	public TrueArkoftheAncientsSwungBlade()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		direction = Vector2.Zero;
		SwingWidth = (float)Math.PI * 3f / 4f;
		anticipation = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.ExpOut, 0f, 0f, 0.15f);
		thrust = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyInOut, 0.1f, 0.15f, 0.85f, 3);
		hold = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.Linear, 0.5f, 1f, 0.2f);
		retract = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyInOut, 0.7f, 0.9f, -0.9f, 3);
		base._002Ector();
	}
}
