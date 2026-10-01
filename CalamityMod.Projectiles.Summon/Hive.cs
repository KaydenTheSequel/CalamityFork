using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class Hive : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/NPCs/Astral/Astraglomerate";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 38;
		base.Projectile.height = 60;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = true;
		base.Projectile.sentry = true;
		base.Projectile.timeLeft = 36000;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.player[base.Projectile.owner];
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 5)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 5)
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.velocity.Y += 0.5f;
		if (base.Projectile.velocity.Y > 10f)
		{
			base.Projectile.velocity.Y = 10f;
		}
		int target = 0;
		float attackDist = 800f;
		_ = base.Projectile.position;
		bool canAttack = false;
		if (Main.player[base.Projectile.owner].HasMinionAttackTargetNPC)
		{
			NPC npc = Main.npc[Main.player[base.Projectile.owner].MinionAttackTargetNPC];
			if (npc.CanBeChasedBy(base.Projectile))
			{
				float targetDist = Vector2.Distance(npc.Center, base.Projectile.Center);
				if (!canAttack && targetDist < attackDist)
				{
					_ = npc.Center;
					canAttack = true;
					target = npc.whoAmI;
				}
			}
		}
		if (!canAttack)
		{
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC nPC2 = enumerator.Current;
				if (nPC2.CanBeChasedBy(base.Projectile))
				{
					float targetDist2 = Vector2.Distance(nPC2.Center, base.Projectile.Center);
					if (!canAttack && targetDist2 < attackDist)
					{
						attackDist = targetDist2;
						_ = nPC2.Center;
						canAttack = true;
						target = nPC2.whoAmI;
					}
				}
			}
		}
		if (!((base.Projectile.owner == Main.myPlayer) & canAttack))
		{
			return;
		}
		if (base.Projectile.ai[0] != 0f)
		{
			base.Projectile.ai[0]--;
			return;
		}
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] % 30f == 0f)
		{
			float velocityX = Main.rand.NextFloat(-0.4f, 0.4f);
			float velocityY = Main.rand.NextFloat(-0.3f, -0.5f);
			int p = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, velocityX, velocityY, ModContent.ProjectileType<Hiveling>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, target);
			if (Main.projectile.IndexInRange(p))
			{
				Main.projectile[p].originalDamage = base.Projectile.originalDamage;
			}
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		fallThrough = false;
		return true;
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
