using CalamityMod.Cooldowns;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.DesertProwler;

public class DesertProwlerProjectile : GlobalProjectile
{
	public bool LightsOut;

	public int ExtraCrit;

	public override bool InstancePerEntity => true;

	public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
	{
		if (entity.DamageType.CountsAsClass(RogueDamageClass.Instance))
		{
			return true;
		}
		return false;
	}

	public override void OnSpawn(Projectile projectile, IEntitySource source)
	{
		if (Main.gameMenu || projectile.damage <= 0 || projectile.owner < 0 || !DesertProwlerHat.ShroudedInSmoke(Main.player[projectile.owner], out var _))
		{
			return;
		}
		int critPool = DesertProwlerHat.FreeCrit;
		int achievedDamage = projectile.damage;
		while (critPool >= 100)
		{
			if (achievedDamage + projectile.damage <= DesertProwlerHat.BonusDamageCap)
			{
				ExtraCrit += 100;
				critPool -= 100;
				achievedDamage += projectile.damage;
				continue;
			}
			if (achievedDamage < DesertProwlerHat.BonusDamageCap)
			{
				int remainingDamageTilCap = DesertProwlerHat.BonusDamageCap - achievedDamage;
				ExtraCrit += (int)((float)(100 * remainingDamageTilCap) / (float)projectile.damage);
			}
			break;
		}
		projectile.CritChance += ExtraCrit;
		projectile.Calamity().supercritHits = 1;
		LightsOut = true;
		Main.player[projectile.owner].GetModPlayer<DesertProwlerPlayer>().stopSmokeBomb = true;
	}

	public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		if (LightsOut)
		{
			Player owner = Main.player[projectile.owner];
			projectile.CritChance -= ExtraCrit;
			if (target.life <= 0 && owner.Calamity().cooldowns.TryGetValue(SandsmokeBomb.ID, out var cd) && cd.timeLeft <= DesertProwlerHat.SmokeCooldown && cd.timeLeft > DesertProwlerHat.LightsOutReset)
			{
				cd.timeLeft = DesertProwlerHat.LightsOutReset;
				SoundEngine.PlaySound(in DesertProwlerHat.CDResetSound);
				GeneralParticleHandler.SpawnParticle(new DesertProwlerSkullParticle(target.Center, Vector2.UnitY * -3f, Color.Gold, Color.DarkGoldenrod, Main.rand.NextFloat(1f, 2f), 250f));
			}
			LightsOut = false;
		}
	}
}
