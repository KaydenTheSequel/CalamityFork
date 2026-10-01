using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class CosmicViperConcussionMissile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		Main.projFrames[base.Type] = 3;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.alpha = 255;
		base.Projectile.extraUpdates = 4;
		base.Projectile.scale = 1.18f;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.aiStyle = 1;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 600;
		base.Projectile.tileCollide = false;
		base.AIType = 242;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		float colorScale = (float)base.Projectile.alpha / 255f;
		Lighting.AddLight((int)base.Projectile.Center.X / 16, (int)base.Projectile.Center.Y / 16, 1f * colorScale, 0.1f * colorScale, 1f * colorScale);
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 3)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 180);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.position);
		base.Projectile.ExpandHitboxBy(50);
		for (int i = 0; i < 3; i++)
		{
			int idx = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, Main.rand.NextBool(3) ? 56 : 242, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[idx];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[idx].scale = 0.5f;
			}
			if (Main.rand.NextBool())
			{
				Main.dust[idx].scale *= 0.5f;
				Main.dust[idx].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
	}
}
