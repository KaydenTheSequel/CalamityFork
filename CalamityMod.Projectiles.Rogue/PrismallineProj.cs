using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class PrismallineProj : ModProjectile, ILocalizedModType, IModType
{
	public bool hitEnemy;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/Prismalline";

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.aiStyle = 113;
		base.Projectile.timeLeft = 60;
		base.AIType = 598;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] != 40f)
		{
			return;
		}
		int numProj = 3;
		int numSpecProj = 0;
		int numStealthProj = 5;
		MathHelper.ToRadians(50f);
		if (base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		if (!base.Projectile.Calamity().stealthStrike)
		{
			for (int i = 0; i < numProj; i++)
			{
				Vector2 velocity = CalamityUtils.RandomVelocity(50f, 30f, 60f, 0.2f);
				if (numSpecProj < 1 && !hitEnemy)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<Prismalline3>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
					numSpecProj++;
				}
				else
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<Prismalline2>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
				}
			}
			return;
		}
		for (int j = 0; j < 4; j++)
		{
			Vector2 velocity2 = CalamityUtils.RandomVelocity(100f, 70f, 100f);
			int shard = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity2, ModContent.ProjectileType<AquashardSplit>(), base.Projectile.damage / 2, 0f, base.Projectile.owner, 0f, 1f);
			if (shard.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[shard].DamageType = RogueDamageClass.Instance;
				Main.projectile[shard].usesLocalNPCImmunity = true;
				Main.projectile[shard].localNPCHitCooldown = 10;
			}
		}
		for (int k = 0; k < numStealthProj + 1; k++)
		{
			Vector2 velocity3 = CalamityUtils.RandomVelocity(50f, 30f, 60f, 0.2f);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity3, ModContent.ProjectileType<Prismalline3>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 1f);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item27, base.Projectile.position);
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 154, base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f);
		}
		if (!base.Projectile.Calamity().stealthStrike)
		{
			return;
		}
		for (int s = 0; s < 5; s++)
		{
			Vector2 velocity = CalamityUtils.RandomVelocity(100f, 70f, 100f);
			int shard = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<AquashardSplit>(), base.Projectile.damage / 2, 0f, base.Projectile.owner);
			if (shard.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[shard].DamageType = RogueDamageClass.Instance;
				Main.projectile[shard].penetrate = 1;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		hitEnemy = true;
		if (base.Projectile.Calamity().stealthStrike)
		{
			target.AddBuff(ModContent.BuffType<Eutrophication>(), 30);
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		hitEnemy = true;
		if (base.Projectile.Calamity().stealthStrike)
		{
			target.AddBuff(ModContent.BuffType<Eutrophication>(), 30);
		}
	}
}
