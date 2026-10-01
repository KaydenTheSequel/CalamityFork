using CalamityMod.Items.Weapons.Magic;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class MeteorStarExplosion : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 7;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 84;
		base.Projectile.height = 152;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = Main.projFrames[base.Type] * 5;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 35;
		base.Projectile.tileCollide = false;
		base.Projectile.hostile = true;
		base.CooldownSlot = 0;
	}

	public override void AI()
	{
		if ((float)base.Projectile.timeLeft % 5f == 4f)
		{
			base.Projectile.frame++;
		}
	}

	public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
	{
		int intendedDamage = Main.rand.Next(GloriousEnd.PlayerExplosionDmgMin, GloriousEnd.PlayerExplosionDmgMax + 1);
		modifiers.SourceDamage *= 0f;
		modifiers.SourceDamage.Flat += (float)intendedDamage * (Main.masterMode ? 2.4f : (Main.expertMode ? 1.6f : 1f));
		if (base.Projectile.ai[0] == 1f)
		{
			modifiers.SourceDamage.Flat /= 2f;
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (target.townNPC)
		{
			int intendedDamage = Main.rand.Next(GloriousEnd.PlayerExplosionDmgMin, GloriousEnd.PlayerExplosionDmgMax + 1);
			modifiers.SourceDamage *= 0f;
			modifiers.SourceDamage.Flat += (float)intendedDamage * (Main.masterMode ? 2.4f : (Main.expertMode ? 1.6f : 1f));
			if (base.Projectile.ai[0] == 1f)
			{
				modifiers.SourceDamage.Flat /= 2f;
			}
		}
	}
}
