using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.NPCs.PlaguebringerGoliath;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class PlagueStingerGoliath : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.hostile = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 300;
	}

	public override void AI()
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.position.Y > base.Projectile.ai[1])
		{
			base.Projectile.tileCollide = true;
		}
		base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) + (float)Math.PI / 2f;
		int playerTracker = Player.FindClosest(base.Projectile.Center, 1, 1);
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] < 60f + base.Projectile.ai[0] * 0.5f && base.Projectile.localAI[0] > 30f)
		{
			float projVelocity = ((Vector2)(ref base.Projectile.velocity)).Length();
			Vector2 playerDirection = Main.player[playerTracker].Center - base.Projectile.Center;
			((Vector2)(ref playerDirection)).Normalize();
			playerDirection *= projVelocity;
			base.Projectile.velocity = (base.Projectile.velocity * 24f + playerDirection) / 25f;
			((Vector2)(ref base.Projectile.velocity)).Normalize();
			Projectile projectile = base.Projectile;
			projectile.velocity *= projVelocity;
		}
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 16f + base.Projectile.ai[0] * 0.04f)
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 1.02f;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Plague>(), 120);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.DrawBackglow(PlaguebringerGoliath.BackglowColor, 4f, null, null, (SpriteEffects)0);
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		Texture2D glow = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/PlagueStingerGoliathGlow", (AssetRequestMode)2).Value;
		Vector2 textureArea = default(Vector2);
		((Vector2)(ref textureArea))._002Ector((float)(glow.Width / 2), (float)(glow.Height / Main.projFrames[base.Type] / 2));
		Vector2 drawArea = base.Projectile.Center - Main.screenPosition;
		drawArea -= new Vector2((float)glow.Width, (float)(glow.Height / Main.projFrames[base.Type])) / 2f;
		drawArea += textureArea + new Vector2(0f, base.Projectile.gfxOffY);
		Color whiteColor = Color.White;
		Main.spriteBatch.Draw(glow, drawArea, (Rectangle?)null, whiteColor, base.Projectile.rotation, textureArea, base.Projectile.scale, (SpriteEffects)0, 0f);
	}
}
