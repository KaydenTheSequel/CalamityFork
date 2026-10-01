using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class NettleRight : ModProjectile, ILocalizedModType, IModType
{
	public static int TotalSegments = 10;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 28);
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = -1;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (base.Projectile.ai[1] == 0f)
		{
			base.Projectile.alpha -= 100;
			if (base.Projectile.alpha <= 0)
			{
				base.Projectile.alpha = 0;
				base.Projectile.ai[1] = 1f;
				if (base.Projectile.ai[0] == 0f)
				{
					base.Projectile.ai[0]++;
					Projectile projectile = base.Projectile;
					projectile.position += base.Projectile.velocity;
				}
				if (Main.myPlayer == base.Projectile.owner && base.Projectile.ai[0] < (float)TotalSegments)
				{
					int nextSegment = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + base.Projectile.velocity, base.Projectile.velocity, base.Projectile.type, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, base.Projectile.ai[0] + 1f);
					NetMessage.SendData(27, -1, -1, null, nextSegment);
				}
			}
			return;
		}
		int AlphaPerFrame = 8;
		base.Projectile.alpha += AlphaPerFrame;
		if (base.Projectile.alpha == AlphaPerFrame * 21)
		{
			for (int i = 0; i < 8; i++)
			{
				Dust dust = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 7, base.Projectile.velocity.X * 0.025f, base.Projectile.velocity.Y * 0.025f, 200, default(Color), 1.3f);
				dust.noGravity = true;
				dust.velocity *= 0.5f;
			}
		}
		if (base.Projectile.alpha >= 255)
		{
			base.Projectile.Kill();
		}
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		if (base.Projectile.ai[0] == (float)TotalSegments)
		{
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/NettleTip", (AssetRequestMode)2).Value;
		}
		Main.spriteBatch.Draw(texture, base.Projectile.Center - Main.screenPosition, (Rectangle?)null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, texture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}
}
