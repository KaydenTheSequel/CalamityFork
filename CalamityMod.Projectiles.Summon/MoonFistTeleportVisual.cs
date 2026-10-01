using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class MoonFistTeleportVisual : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 2);
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.hide = true;
		base.Projectile.timeLeft = 45;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.penetrate = -1;
	}

	public override void AI()
	{
		base.Projectile.Opacity = Utils.GetLerpValue(0f, 12f, base.Projectile.timeLeft);
		base.Projectile.scale = Utils.GetLerpValue(45f, 5f, base.Projectile.timeLeft);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		Texture2D telegraphTexture = TextureAssets.Projectile[base.Type].Value;
		Color telegraphColor = Color.White * base.Projectile.Opacity * 0.2f;
		((Color)(ref telegraphColor)).A = 0;
		for (int i = 0; i < 35; i++)
		{
			Vector2 drawPosition = base.Projectile.Center + ((float)Math.PI * 2f * (float)i / 5f + Main.GlobalTimeWrappedHourly * 3f).ToRotationVector2() * 2f;
			drawPosition -= Main.screenPosition;
			Vector2 scale = new Vector2(0.58f, 1f) * base.Projectile.scale;
			scale *= MathHelper.Lerp(0.015f, 1f, (float)i / 35f);
			Main.spriteBatch.Draw(telegraphTexture, drawPosition, (Rectangle?)null, telegraphColor, 0f, telegraphTexture.Size() * 0.5f, scale, (SpriteEffects)0, 0f);
		}
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 20; i++)
		{
			Dust magic = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(30f, 30f), 267);
			magic.color = Color.SkyBlue;
			magic.scale = 1.1f;
			magic.fadeIn = 0.6f;
			magic.velocity = Main.rand.NextVector2Circular(2f, 2f);
			magic.velocity = Vector2.Lerp(magic.velocity, -Vector2.UnitY * ((Vector2)(ref magic.velocity)).Length(), Main.rand.NextFloat(0.65f, 1f));
			magic.noGravity = true;
		}
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		behindNPCsAndTiles.Add(index);
	}
}
