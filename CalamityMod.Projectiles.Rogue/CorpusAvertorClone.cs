using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class CorpusAvertorClone : ModProjectile, ILocalizedModType, IModType
{
	public const int MaxClones = 3;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/CorpusAvertor";

	private ref float CloneNum => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 24;
		base.Projectile.height = 24;
		base.Projectile.ignoreWater = true;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 180;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		base.Projectile.rotation += (Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y)) * 0.04f;
		if (base.Projectile.timeLeft < 165)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 480f, 14f, 20f);
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft < 85)
		{
			return new Color((int)(150f * ((float)base.Projectile.timeLeft / 85f)), 0, 0, base.Projectile.timeLeft / 5 * 3);
		}
		return new Color(150, 0, 0, 50);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override bool? CanDamage()
	{
		if (base.Projectile.timeLeft <= 165)
		{
			return null;
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 180);
		if (CloneNum < 3f && Main.myPlayer == base.Projectile.owner)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, -base.Projectile.velocity.RotatedByRandom(0.39269909262657166), ModContent.ProjectileType<CorpusAvertorClone>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, CloneNum + 1f);
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 180);
	}
}
