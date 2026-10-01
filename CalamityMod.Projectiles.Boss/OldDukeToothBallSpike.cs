using System;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.NPCs.OldDuke;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class OldDukeToothBallSpike : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.hostile = true;
		base.Projectile.timeLeft = 600;
		base.Projectile.ignoreWater = true;
		base.CooldownSlot = 1;
	}

	public override void AI()
	{
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight((int)((base.Projectile.position.X + (float)(base.Projectile.width / 2)) / 16f), (int)((base.Projectile.position.Y + (float)(base.Projectile.height / 2)) / 16f), 0.45f, 0.35f, 0f);
		Vector2 val = new Vector2(base.Projectile.ai[0], base.Projectile.ai[1]);
		float finalVelocity = ((Vector2)(ref val)).Length();
		if (((Vector2)(ref base.Projectile.velocity)).Length() < finalVelocity)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.025f;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		for (int i = 0; i < 2; i++)
		{
			int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 75, 0f, 0f, 0, default(Color), 0.6f);
			Main.dust[dust].noGravity = true;
			Dust obj = Main.dust[dust];
			obj.velocity *= 0.3f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		Asset<Texture2D> tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
		for (int i = 0; i < 5; i++)
		{
			Main.EntitySpriteDraw(tex.Value, base.Projectile.Center - base.Projectile.velocity * (float)i - Main.screenPosition, tex.Frame(), OldDuke.GlowColor, base.Projectile.rotation, tex.Frame().Center(), MathHelper.Lerp(1.5f, 0.2f, (float)i / 5f), (SpriteEffects)0);
			Main.EntitySpriteDraw(tex.Value, base.Projectile.Center - base.Projectile.velocity * (float)i - Main.screenPosition, tex.Frame(), OldDuke.GlowColor, base.Projectile.rotation, tex.Frame().Center(), MathHelper.Lerp(1.5f, 0.2f, (float)i / 5f), (SpriteEffects)0);
		}
		Main.EntitySpriteDraw(tex.Value, base.Projectile.Center - Main.screenPosition, tex.Frame(), OldDuke.GlowColor, base.Projectile.rotation, tex.Frame().Center(), 1.5f, (SpriteEffects)0);
		Main.EntitySpriteDraw(tex.Value, base.Projectile.Center - Main.screenPosition, tex.Frame(), OldDuke.GlowColor, base.Projectile.rotation, tex.Frame().Center(), 1.5f, (SpriteEffects)0);
		base.Projectile.DrawBackglow(OldDuke.GlowColor, 4f, null, null, (SpriteEffects)0);
		return base.PreDraw(ref lightColor);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Irradiated>(), 360);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i <= 2; i++)
		{
			int idx = Dust.NewDust(base.Projectile.position, 8, 8, 75, 0f, 0f, 0, default(Color), 0.75f);
			Main.dust[idx].noGravity = true;
			Dust obj = Main.dust[idx];
			obj.velocity *= 3f;
			idx = Dust.NewDust(base.Projectile.position, 8, 8, 75, 0f, 0f, 0, default(Color), 0.75f);
			Main.dust[idx].noGravity = true;
			Dust obj2 = Main.dust[idx];
			obj2.velocity *= 3f;
		}
	}
}
