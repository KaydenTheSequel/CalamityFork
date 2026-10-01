using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class GacruxianHome : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/GacruxianMollusk";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.aiStyle = 18;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.penetrate = 3;
		base.Projectile.timeLeft = 120;
		base.Projectile.ignoreWater = true;
		base.AIType = 274;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 5;
	}

	public override void AI()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(4))
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f);
		}
		if (base.Projectile.timeLeft % 30 == 0 && base.Projectile.owner == Main.myPlayer)
		{
			int proj = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<UltimusCleaverDust>(), (int)((double)base.Projectile.damage * 0.5), base.Projectile.knockBack, base.Projectile.owner);
			if (proj.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[proj].DamageType = RogueDamageClass.Instance;
			}
		}
		CalamityUtils.HomeInOnNPC(base.Projectile, !base.Projectile.tileCollide, 250f, 12f, 20f);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<StaticDischarge>(), 120);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<StaticDischarge>(), 120);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i <= 10; i++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f);
		}
	}
}
