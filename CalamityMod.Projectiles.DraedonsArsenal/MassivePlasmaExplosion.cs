using System;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class MassivePlasmaExplosion : ModProjectile, ILocalizedModType, IModType
{
	private float lightAmt = 1f;

	public int frameX;

	public int frameY;

	private const int horizontalFrames = 4;

	private const int verticalFrames = 5;

	private const int frameLength = 5;

	private const float radius = 191.5f;

	public new string LocalizationCategory => "Projectiles.Misc";

	public float Time
	{
		get
		{
			return base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 383);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 50;
	}

	public override void AI()
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter % 5 == 4)
		{
			frameY++;
			if (frameY >= 5)
			{
				frameX++;
				frameY = 0;
			}
			if (frameX >= 4)
			{
				base.Projectile.Kill();
			}
		}
		Vector2 center = base.Projectile.Center;
		Color white = Color.White;
		Lighting.AddLight(center, ((Color)(ref white)).ToVector3() * 4f * lightAmt);
		if (base.Projectile.localAI[0] == 0f)
		{
			SoundEngine.PlaySound(in CommonCalamitySounds.FlareSound, base.Projectile.Center);
			base.Projectile.localAI[0] = 1f;
		}
		lightAmt = (float)Math.Sin(Time / 37f * (float)Math.PI) * 2f;
		if (lightAmt > 1f)
		{
			lightAmt = 1f;
		}
		Time++;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 191.5f, targetHitbox);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(39, 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(39, 180);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		int length = value.Width / 4;
		int height = value.Height / 5;
		Vector2 drawPos = base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY);
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(frameX * length, frameY * height, length, height);
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)length / 2f, (float)height / 2f);
		Main.EntitySpriteDraw(value, drawPos, frame, Color.White, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
