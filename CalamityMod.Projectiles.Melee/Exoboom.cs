using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class Exoboom : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 250;
		base.Projectile.height = 250;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = false;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 30;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			for (int i = 0; i < 16; i++)
			{
				Vector2 position = -Vector2.UnitY.RotatedByRandom(1.5707963705062866) * Main.rand.NextFloat(50f);
				Vector2 cinderVelocity = -Vector2.UnitY.RotatedByRandom(0.7853981852531433) * Main.rand.NextFloat(5f, 18f);
				Color cinderColor = CalamityUtils.MulticolorLerp(Main.rand.NextFloat(), CalamityUtils.ExoPalette);
				GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(position, cinderVelocity, 1.1f, cinderColor, 32, 1f, 4f));
			}
			base.Projectile.localAI[0] = 1f;
		}
		for (int j = 0; j < 2; j++)
		{
			Color smokeColor = CalamityUtils.MulticolorLerp(Main.rand.NextFloat(), CalamityUtils.ExoPalette);
			smokeColor = Color.Lerp(smokeColor, Color.Gray, 0.55f);
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, Main.rand.NextVector2Circular(27f, 27f), smokeColor, 40, 1.8f, 1f, 0.03f, glowing: true, 0.075f));
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
	}
}
