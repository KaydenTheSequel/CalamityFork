using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class TrueAridGrandeurShot : ModProjectile, ILocalizedModType, IModType
{
	private bool initialized;

	private Vector2 direction;

	public const float pogoStrenght = 16f;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Melee/MendedBiomeBlade_AridGrandeurExtra";

	public ref float Shred => ref base.Projectile.ai[0];

	public float ShredRatio => MathHelper.Clamp(Shred / 250f, 0f, 1f);

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.width = (base.Projectile.height = 70);
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 30;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		float collisionPoint = 0f;
		float bladeLength = 84f * base.Projectile.scale;
		float bladeWidth = 76f * base.Projectile.scale;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center - direction * bladeLength / 2f, base.Projectile.Center + direction * bladeLength / 2f, bladeWidth, ref collisionPoint);
	}

	public override void AI()
	{
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		if (!initialized)
		{
			SoundEngine.PlaySound(in SoundID.Item90, base.Projectile.Center);
			base.Projectile.timeLeft = (int)(30f + ShredRatio * 30f);
			initialized = true;
			direction = Owner.SafeDirectionTo(Owner.Calamity().mouseWorld, Vector2.Zero);
			((Vector2)(ref direction)).Normalize();
			base.Projectile.rotation = direction.ToRotation();
			base.Projectile.velocity = direction * 6f;
			base.Projectile.scale = 1f + ShredRatio;
			base.Projectile.netUpdate = true;
		}
		Projectile projectile = base.Projectile;
		projectile.position += base.Projectile.velocity;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		Vector2 drawOrigin = default(Vector2);
		for (int i = 0; i < 4; i++)
		{
			Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/MendedBiomeBlade_AridGrandeurExtra", (AssetRequestMode)2).Value;
			float num = direction.ToRotation();
			float circleCompletion = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 5f + (float)i * ((float)Math.PI / 2f));
			float drawRotation = num + (float)Math.PI / 4f + circleCompletion * (float)Math.PI / 10f - circleCompletion * ((float)Math.PI / 9f) * ShredRatio;
			((Vector2)(ref drawOrigin))._002Ector(0f, (float)tex.Height);
			Vector2 drawOffsetStraight = base.Projectile.Center + direction * (float)Math.Sin(Main.GlobalTimeWrappedHourly * 7f) * 10f - Main.screenPosition;
			Vector2 drawDisplacementAngle = direction.RotatedBy(1.5707963705062866) * circleCompletion.ToRotationVector2().Y * (20f + 40f * ShredRatio);
			float opacityFade = ((base.Projectile.timeLeft > 15) ? 1f : ((float)base.Projectile.timeLeft / 15f));
			Main.EntitySpriteDraw(tex, drawOffsetStraight + drawDisplacementAngle, null, Color.Lerp(Color.White, lightColor, 0.5f) * 0.8f * opacityFade, drawRotation, drawOrigin, base.Projectile.scale, (SpriteEffects)0);
		}
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item60, base.Projectile.Center);
		for (int i = 0; i < 4; i++)
		{
			float num = direction.ToRotation();
			float circleCompletion = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 5f + (float)i * ((float)Math.PI / 2f));
			float drawRotation = num + (float)Math.PI / 4f + circleCompletion * (float)Math.PI / 10f - circleCompletion * ((float)Math.PI / 9f) * ShredRatio;
			Vector2 drawOffsetStraight = base.Projectile.Center + direction * (float)Math.Sin(Main.GlobalTimeWrappedHourly * 7f) * 10f;
			Vector2 drawDisplacementAngle = direction.RotatedBy(1.5707963705062866) * circleCompletion.ToRotationVector2().Y * (20f + 40f * ShredRatio);
			for (int j = 0; j < 4; j++)
			{
				GeneralParticleHandler.SpawnParticle(new GenericSparkle(drawOffsetStraight + (drawRotation - (float)Math.PI / 4f).ToRotationVector2() * (60f + (float)j * 50f) + drawDisplacementAngle, direction * ((Vector2)(ref base.Projectile.velocity)).Length() * Main.rand.NextFloat(0.9f, 1.1f), Color.Lerp(Color.Cyan, Color.Orange, (float)(j + 1) / 4f), Color.OrangeRed, 0.5f + Main.rand.NextFloat(-0.2f, 0.2f), 20 + Main.rand.Next(30), 1f, 2f));
			}
		}
	}

	public TrueAridGrandeurShot()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		direction = Vector2.Zero;
		base._002Ector();
	}
}
