using CalamityMod.Items.Weapons.DraedonsArsenal;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon.SmallAresArms;

public class MinionGaussNuke : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 76);
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = true;
		base.Projectile.timeLeft = 180;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 12;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		NPC potentialTarget = base.Projectile.Center.ClosestNPCAt(960f, ignoreTiles: false);
		if (potentialTarget != null)
		{
			base.Projectile.velocity = base.Projectile.SuperhomeTowardsTarget(potentialTarget, 23f, 10f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		base.Projectile.Kill();
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in TeslaCannon.FireSound, base.Projectile.Center);
		if (Main.myPlayer == base.Projectile.owner)
		{
			int boom = Projectile.NewProjectile(base.Projectile.GetSource_Death(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<MinionGaussBoom>(), (int)((float)base.Projectile.damage * 1.1f), base.Projectile.knockBack, base.Projectile.owner);
			if (Main.projectile.IndexInRange(boom))
			{
				Main.projectile[boom].ai[1] = 720f;
				Main.projectile[boom].originalDamage = base.Projectile.originalDamage;
			}
		}
	}
}
