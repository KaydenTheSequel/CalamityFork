using System;
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

public class SanguineFuryWheel : ModProjectile, ILocalizedModType, IModType
{
	private bool initialized;

	private Vector2 direction;

	public const float MaxTime = 120f;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Melee/TrueBiomeBlade_SanguineFuryExtra";

	public ref float Shred => ref base.Projectile.ai[0];

	public float ShredRatio => MathHelper.Clamp(Shred / 250f, 0f, 1f);

	public Player Owner => Main.player[base.Projectile.owner];

	public float Timer => 120f - (float)base.Projectile.timeLeft;

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.width = (base.Projectile.height = 45);
		base.Projectile.tileCollide = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 3;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 60;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.95f;
		return false;
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
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		if (!initialized)
		{
			SoundEngine.PlaySound(in SoundID.Item90, base.Projectile.Center);
			initialized = true;
			direction = Owner.SafeDirectionTo(Owner.Calamity().mouseWorld, Vector2.Zero);
			((Vector2)(ref direction)).Normalize();
			base.Projectile.rotation = direction.ToRotation();
			base.Projectile.timeLeft = 120;
			base.Projectile.velocity = direction * 16f;
			base.Projectile.scale = 1f + ShredRatio;
			base.Projectile.netUpdate = true;
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.96f;
		Projectile projectile2 = base.Projectile;
		projectile2.position += base.Projectile.velocity;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (Owner.HeldItem.ModItem is OmegaBiomeBlade sword && Main.rand.NextFloat() <= OmegaBiomeBlade.SuperPogoAttunement_WheelProc)
		{
			sword.OnHitProc = true;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		Vector2 drawOrigin = default(Vector2);
		for (int i = 0; i < 4; i++)
		{
			Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/TrueBiomeBlade_SanguineFuryExtra", (AssetRequestMode)2).Value;
			float drawAngleWheel = MathHelper.WrapAngle((float)i * ((float)Math.PI / 2f) + Main.GlobalTimeWrappedHourly * 6f);
			((Vector2)(ref drawOrigin))._002Ector(0f, (float)tex.Height);
			Vector2 drawPositionWheel = base.Projectile.Center - drawAngleWheel.ToRotationVector2() * 20f - Main.screenPosition;
			float opacityFade = ((base.Projectile.timeLeft > 15) ? 1f : ((float)base.Projectile.timeLeft / 15f));
			Main.EntitySpriteDraw(tex, drawPositionWheel, null, Color.Lerp(Color.White, lightColor, 0.5f) * 0.8f * opacityFade, drawAngleWheel, drawOrigin, base.Projectile.scale * 0.85f, (SpriteEffects)0);
		}
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item60, base.Projectile.Center);
		for (int i = 0; i < 10; i++)
		{
			GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center, Main.rand.NextVector2Circular(1f, 1f) * Main.rand.NextFloat(17.5f, 25f) * base.Projectile.scale, Color.White, Main.rand.NextBool() ? Color.Crimson : Color.DarkRed, 0.4f + Main.rand.NextFloat(0f, 3.5f), 20 + Main.rand.Next(30), 1f, 3f));
		}
	}

	public SanguineFuryWheel()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		direction = Vector2.Zero;
		base._002Ector();
	}
}
