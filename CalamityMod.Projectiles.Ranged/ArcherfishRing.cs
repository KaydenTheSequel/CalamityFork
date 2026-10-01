using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class ArcherfishRing : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle PopSound = new SoundStyle("CalamityMod/Sounds/Custom/BubblyPop")
	{
		PitchVariance = 0.5f,
		Volume = 0.66f
	};

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Particles/HollowCircleHardEdge";

	public override void SetDefaults()
	{
		base.Projectile.width = 60;
		base.Projectile.height = 34;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 2;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 15;
		base.Projectile.timeLeft = 300;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		return base.Projectile.RotatingHitboxCollision(targetHitbox);
	}

	public override void AI()
	{
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] == 0f)
		{
			base.Projectile.scale = 0.5f;
			base.Projectile.ai[0]++;
		}
		if (base.Projectile.scale <= 1.5f)
		{
			base.Projectile.scale *= 1.015f;
		}
		else
		{
			base.Projectile.scale = 1.5f;
		}
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 0.08f)
		{
			base.Projectile.alpha += 15;
		}
		if (base.Projectile.alpha >= 255)
		{
			base.Projectile.Kill();
		}
		else
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.95f;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in PopSound, base.Projectile.Center);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Vector2 ringScale = base.Projectile.Size / tex.Size() * base.Projectile.scale;
		Color ringColor = Color.Lerp(Color.DodgerBlue, Color.Blue, base.Projectile.Opacity) * base.Projectile.Opacity * 1.2f;
		Main.spriteBatch.EnterShaderRegion(BlendState.Additive);
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, ringColor, base.Projectile.rotation, tex.Size() / 2f, ringScale, (SpriteEffects)0);
		Main.spriteBatch.ExitShaderRegion();
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		float totalDusts = 18f;
		for (float i = 0f; i < totalDusts; i++)
		{
			Vector2 ringSpeed = Utils.RotatedBy(new Vector2((float)Math.Cos(i / totalDusts * ((float)Math.PI * 2f)), (float)Math.Sin(i / totalDusts * ((float)Math.PI * 2f)) * 0.5f), (double)base.Projectile.rotation, default(Vector2)) * 4f * base.Projectile.scale;
			Dust.NewDustPerfect(base.Projectile.Center, 211, ringSpeed, 100).noGravity = true;
		}
	}
}
