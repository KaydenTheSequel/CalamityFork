using System;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class FinalDawnReticle : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		base.Projectile.scale = 1.5f;
		base.Projectile.width = 120;
		base.Projectile.height = 120;
		base.Projectile.penetrate = -1;
		base.Projectile.hostile = false;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.alpha = 0;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[1] == 0f)
		{
			base.Projectile.ai[1] = 1f;
		}
		if (base.Projectile.ai[0] == 0f)
		{
			int dustCount = 36;
			for (int i = 0; i < dustCount; i++)
			{
				Vector2 val = base.Projectile.Center + 10f * Vector2.UnitX;
				Vector2 offset = Vector2.UnitX * (float)base.Projectile.width * 0.1875f;
				offset = offset.RotatedBy((float)(i - (dustCount / 2 - 1)) * ((float)Math.PI * 2f) / 20f);
				int dustIdx = Dust.NewDust(val + offset, 0, 0, ModContent.DustType<FinalFlame>(), offset.X * 2f, offset.Y * 2f, 100, default(Color), 3.4f);
				Main.dust[dustIdx].noGravity = true;
				Main.dust[dustIdx].noLight = true;
				Main.dust[dustIdx].velocity = Vector2.Normalize(offset) * 5f;
			}
			base.Projectile.ai[0] = 1f;
		}
		base.Projectile.alpha += 8;
		base.Projectile.scale *= 0.98f;
		base.Projectile.ai[1] *= 1.01f;
		if (base.Projectile.alpha >= 255)
		{
			base.Projectile.Kill();
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		Texture2D ring = TextureAssets.Projectile[base.Type].Value;
		Texture2D symbol = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/FinalDawnReticleSymbol", (AssetRequestMode)2).Value;
		Main.EntitySpriteDraw(symbol, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(Color.White), base.Projectile.rotation, new Vector2((float)(symbol.Width / 2), (float)(symbol.Height / 2)), base.Projectile.scale, (SpriteEffects)(base.Projectile.spriteDirection != 1));
		Main.EntitySpriteDraw(ring, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(Color.White), base.Projectile.rotation, new Vector2((float)(ring.Width / 2), (float)(ring.Height / 2)), base.Projectile.ai[1], (SpriteEffects)(base.Projectile.spriteDirection != 1));
		return false;
	}
}
