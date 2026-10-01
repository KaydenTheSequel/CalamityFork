using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class GodKiller : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 22;
		base.Projectile.height = 46;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 180;
	}

	public override void AI()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 5;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (base.Projectile.owner == Main.myPlayer && base.Projectile.timeLeft <= 3)
		{
			base.Projectile.tileCollide = false;
			base.Projectile.ai[1] = 0f;
			base.Projectile.alpha = 255;
			base.Projectile.position.X = base.Projectile.position.X + (float)(base.Projectile.width / 2);
			base.Projectile.position.Y = base.Projectile.position.Y + (float)(base.Projectile.height / 2);
			base.Projectile.width = 100;
			base.Projectile.height = 100;
			base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
			base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
			base.Projectile.knockBack = 5f;
		}
		else if (Math.Abs(base.Projectile.velocity.X) >= 2f || Math.Abs(base.Projectile.velocity.Y) >= 2f)
		{
			for (int i = 0; i < 2; i++)
			{
				float shortXVel = 0f;
				float shortYVel = 0f;
				if (i == 1)
				{
					shortXVel = base.Projectile.velocity.X * 0.5f;
					shortYVel = base.Projectile.velocity.Y * 0.5f;
				}
				int dusting = Dust.NewDust(new Vector2(base.Projectile.position.X + 3f + shortXVel, base.Projectile.position.Y + 3f + shortYVel) - base.Projectile.velocity * 0.5f, base.Projectile.width - 8, base.Projectile.height - 8, 173, 0f, 0f, 100);
				Main.dust[dusting].scale *= 1f + (float)Main.rand.Next(5) * 0.1f;
				Dust obj = Main.dust[dusting];
				obj.velocity *= 0.2f;
				Main.dust[dusting].noGravity = true;
				dusting = Dust.NewDust(new Vector2(base.Projectile.position.X + 3f + shortXVel, base.Projectile.position.Y + 3f + shortYVel) - base.Projectile.velocity * 0.5f, base.Projectile.width - 8, base.Projectile.height - 8, 199, 0f, 0f, 100, default(Color), 0.1f);
				Main.dust[dusting].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
				Dust obj2 = Main.dust[dusting];
				obj2.velocity *= 0.05f;
			}
		}
		CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 300f, 12f, 20f);
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector(11f, 23f);
		Main.EntitySpriteDraw(ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/GodKillerGlow", (AssetRequestMode)2).Value, base.Projectile.Center - Main.screenPosition, null, Color.White, base.Projectile.rotation, origin, 1f, (SpriteEffects)0);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item89, base.Projectile.position);
		for (int j = 0; j < 5; j++)
		{
			int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 199, 0f, 0f, 100, default(Color), 1.5f);
			Dust obj = Main.dust[dust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[dust].scale = 0.5f;
				Main.dust[dust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int k = 0; k < 10; k++)
		{
			int dust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 173, 0f, 0f, 100, default(Color), 2f);
			Main.dust[dust2].noGravity = true;
			Dust obj2 = Main.dust[dust2];
			obj2.velocity *= 5f;
			dust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 173, 0f, 0f, 100, default(Color), 1.5f);
			Dust obj3 = Main.dust[dust2];
			obj3.velocity *= 2f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 120);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 120);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		return false;
	}
}
