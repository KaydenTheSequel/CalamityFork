using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.NPCs.PlaguebringerGoliath;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class PlagueStingerGoliathV2 : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/Boss/PlagueStingerGoliath";

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.hostile = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 2;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 300;
	}

	public override void AI()
	{
		if (base.Projectile.position.Y > base.Projectile.ai[1])
		{
			base.Projectile.tileCollide = true;
		}
		base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) + (float)Math.PI / 2f;
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
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)(glow.Width / 2), (float)(glow.Height / Main.projFrames[base.Type] / 2));
		Vector2 drawPos = base.Projectile.Center - Main.screenPosition;
		drawPos -= new Vector2((float)glow.Width, (float)(glow.Height / Main.projFrames[base.Type])) / 2f;
		drawPos += origin + new Vector2(0f, base.Projectile.gfxOffY);
		Color whiteColor = Color.White;
		Main.spriteBatch.Draw(glow, drawPos, (Rectangle?)null, whiteColor, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0, 0f);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		if (base.Projectile.owner == Main.myPlayer)
		{
			float scale = 1.5f + base.Projectile.ai[0] * 0.015f;
			int baseWidthAndHeight = 20;
			int proj = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<PlagueExplosion>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			Main.projectile[proj].scale = scale;
			Main.projectile[proj].width = (int)((float)baseWidthAndHeight * scale);
			Main.projectile[proj].height = (int)((float)baseWidthAndHeight * scale);
			Main.projectile[proj].position.X = base.Projectile.Center.X - (float)Main.projectile[proj].width * 0.5f;
			Main.projectile[proj].position.Y = base.Projectile.Center.Y - (float)Main.projectile[proj].height * 0.5f;
		}
	}
}
