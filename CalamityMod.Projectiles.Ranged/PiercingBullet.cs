using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class PiercingBullet : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/Ranged/AMRShot";

	public override void SetDefaults()
	{
		base.Projectile.width = 4;
		base.Projectile.height = 4;
		base.Projectile.light = 0.5f;
		base.Projectile.alpha = 255;
		base.Projectile.extraUpdates = 10;
		base.Projectile.scale = 1.18f;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ignoreWater = true;
		base.Projectile.aiStyle = 1;
		base.AIType = 242;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 600;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (!modifiers.SuperArmor && target.defense <= 999 && !(target.Calamity().DR >= 0.95f) && !target.Calamity().unbreakableDR)
		{
			modifiers.DefenseEffectiveness *= 0f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		Collision.HitTiles(base.Projectile.position, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
		SoundEngine.PlaySound(in SoundID.Dig, base.Projectile.position);
		return true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		OnHitEffects(target.Center, hit.Crit);
		target.AddBuff(ModContent.BuffType<MarkedforDeath>(), 300);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		OnHitEffects(target.Center, crit: true);
		target.AddBuff(ModContent.BuffType<MarkedforDeath>(), 300);
	}

	private void OnHitEffects(Vector2 targetPos, bool crit)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		if (!crit)
		{
			return;
		}
		IEntitySource source = base.Projectile.GetSource_FromThis();
		int bulletCount = 6;
		for (int x = 0; x < bulletCount; x++)
		{
			if (base.Projectile.owner == Main.myPlayer)
			{
				CalamityUtils.ProjectileBarrage(source, base.Projectile.Center, targetPos, x < bulletCount / 2, 500f, 500f, 0f, 500f, 12f, ModContent.ProjectileType<AMR2>(), (int)((double)base.Projectile.damage * 0.175), base.Projectile.knockBack, base.Projectile.owner);
			}
		}
	}
}
