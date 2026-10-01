using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class KelvinCatalystStar : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.coldDamage = true;
	}

	public override void AI()
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] < 90f)
		{
			base.Projectile.ai[0]++;
			base.Projectile.velocity.X *= 0.98f;
			base.Projectile.velocity.Y *= 0.98f;
		}
		else
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 500f, 12f, 20f);
		}
		Lighting.AddLight(base.Projectile.Center, (float)Main.DiscoR * 0.075f / 255f, (float)Main.DiscoR * 0.1f / 255f, (float)Main.DiscoR * 0.125f / 255f);
		if (Main.rand.NextBool(6))
		{
			int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 67, 0f, 0f, 100, default(Color), 0.4f);
			Dust obj = Main.dust[dust];
			obj.velocity *= 0.3f;
			Main.dust[dust].noGravity = true;
		}
		base.Projectile.rotation += 0.25f;
	}

	public override bool? CanDamage()
	{
		if (!(base.Projectile.ai[0] >= 90f))
		{
			return false;
		}
		return null;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item27, base.Projectile.position);
		base.Projectile.ExpandHitboxBy(24);
		int dustAmt = 36;
		for (int j = 0; j < dustAmt; j++)
		{
			Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.75f).RotatedBy((float)(j - (dustAmt / 2 - 1)) * ((float)Math.PI * 2f) / (float)dustAmt) + base.Projectile.Center;
			Vector2 faceDirection = val - base.Projectile.Center;
			int icyDust = Dust.NewDust(val + faceDirection, 0, 0, 67, faceDirection.X * 0.5f, faceDirection.Y * 0.5f, 100, default(Color), 0.75f);
			Main.dust[icyDust].noGravity = true;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(324, 60);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(324, 60);
	}
}
