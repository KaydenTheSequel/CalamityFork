using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Events;
using CalamityMod.World;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class ShadeNimbusHostile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 54;
		base.Projectile.height = 28;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.hostile = true;
		base.Projectile.timeLeft = ((CalamityWorld.death || BossRushEvent.BossRushActive) ? 480 : 360);
		base.Projectile.penetrate = -1;
	}

	public override void AI()
	{
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 8)
		{
			base.Projectile.frameCounter = 0;
			base.Projectile.frame++;
			if (base.Projectile.frame > 5)
			{
				base.Projectile.frame = 0;
			}
		}
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] >= ((CalamityWorld.death || BossRushEvent.BossRushActive) ? 420f : 300f))
		{
			base.Projectile.alpha += 5;
			if (base.Projectile.alpha > 255)
			{
				base.Projectile.alpha = 255;
				base.Projectile.Kill();
			}
			return;
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= (Main.getGoodWorld ? 10f : 36f))
		{
			base.Projectile.ai[0] = 0f;
			int rainSpawnX = (int)(base.Projectile.position.X + 14f + (float)Main.rand.Next(base.Projectile.width - 28));
			int rainSpawnY = (int)(base.Projectile.position.Y + (float)base.Projectile.height + 4f);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), rainSpawnX, rainSpawnY, 0f, 8f, ModContent.ProjectileType<ShaderainHostile>(), base.Projectile.damage, 0f, Main.myPlayer);
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<BrainRot>(), 240);
		}
	}
}
