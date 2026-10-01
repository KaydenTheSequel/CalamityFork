using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.Melee.Yoyos;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class MicrowaveAura : ModProjectile, ILocalizedModType, IModType
{
	private int radius = 100;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.width = (base.Projectile.height = 200);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.alpha = 255;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 300;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		Projectile parent = FindParent();
		if (parent != null && parent.active)
		{
			base.Projectile.Center = parent.Center;
			base.Projectile.timeLeft = 2;
		}
		else
		{
			base.Projectile.Kill();
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180);
		if (target.life <= 0 && (FindParent().ModProjectile as MicrowaveYoyo).soundCooldown <= 0)
		{
			SoundEngine.PlaySound(in TheMicrowave.BeepSound, base.Projectile.Center);
			(FindParent().ModProjectile as MicrowaveYoyo).soundCooldown = 60;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180);
		if (target.statLife <= 0 && (FindParent().ModProjectile as MicrowaveYoyo).soundCooldown <= 0)
		{
			SoundEngine.PlaySound(in TheMicrowave.BeepSound, base.Projectile.Center);
			(FindParent().ModProjectile as MicrowaveYoyo).soundCooldown = 60;
		}
	}

	private Projectile FindParent()
	{
		Projectile parent = null;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if ((float)p.identity == base.Projectile.ai[0] && p.type == ModContent.ProjectileType<MicrowaveYoyo>() && p.owner == base.Projectile.owner)
			{
				parent = p;
				break;
			}
		}
		return parent;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, radius, targetHitbox);
	}
}
