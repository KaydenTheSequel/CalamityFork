using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class CelestusMiniScythe : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 48;
		base.Projectile.height = 48;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 4;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 40;
		base.Projectile.timeLeft = 85;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.Calamity().CannotProc = true;
	}

	public override void AI()
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation += 2f;
		base.Projectile.velocity.X *= 1.02f;
		base.Projectile.velocity.Y *= 1.02f;
		Lighting.AddLight(base.Projectile.Center, (float)Main.DiscoR * 0.5f / 255f, (float)Main.DiscoG * 0.5f / 255f, (float)Main.DiscoB * 0.5f / 255f);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft < 85)
		{
			byte b2 = (byte)(base.Projectile.timeLeft * 3);
			byte a2 = (byte)(100f * ((float)(int)b2 / 255f));
			return new Color((int)b2, (int)b2, (int)b2, (int)a2);
		}
		return new Color(255, 255, 255, 100);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 180);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
