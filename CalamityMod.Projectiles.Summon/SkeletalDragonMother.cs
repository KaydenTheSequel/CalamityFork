using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class SkeletalDragonMother : ModProjectile, ILocalizedModType, IModType
{
	public const float DistanceToCheck = 1100f;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 92;
		base.Projectile.height = 78;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 6;
		base.Projectile.minionSlots = 6f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (base.Projectile.localAI[0] == 0f)
		{
			for (int i = 0; i < 2; i++)
			{
				int p = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + Utils.RandomVector2(Main.rand, -24f, 24f), Main.rand.NextVector2CircularEdge(4f, 4f), ModContent.ProjectileType<SkeletalDragonChild>(), base.Projectile.damage, base.Projectile.knockBack, player.whoAmI, base.Projectile.whoAmI);
				if (Main.projectile.IndexInRange(p))
				{
					Main.projectile[p].originalDamage = base.Projectile.originalDamage;
				}
			}
			base.Projectile.localAI[0] = 1f;
		}
		bool num = base.Projectile.type == ModContent.ProjectileType<SkeletalDragonMother>();
		player.AddBuff(ModContent.BuffType<SkeletalDragonsBuff>(), 3600);
		if (num)
		{
			if (player.dead)
			{
				modPlayer.dragonFamily = false;
			}
			if (modPlayer.dragonFamily)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		NPC target = base.Projectile.Center.MinionHoming(1100f, player);
		if (target != null)
		{
			base.Projectile.extraUpdates = 1;
			base.Projectile.ai[0]++;
			float modulo = base.Projectile.ai[0] % 150f;
			if (modulo < 30f || (modulo >= 90f && modulo < 120f))
			{
				if (((Vector2)(ref base.Projectile.velocity)).Length() == 0f)
				{
					base.Projectile.velocity = base.Projectile.SafeDirectionTo(target.Center).RotatedByRandom(0.5) * -8f;
				}
				float angleToTarget = base.Projectile.AngleTo(target.Center);
				float resultantAngle = base.Projectile.velocity.ToRotation().AngleLerp(angleToTarget, 0.08f);
				if (base.Projectile.Distance(target.Center) > 70f)
				{
					base.Projectile.velocity = Utils.RotatedBy(new Vector2(((Vector2)(ref base.Projectile.velocity)).Length(), 0f), (double)resultantAngle, default(Vector2));
				}
				else
				{
					base.Projectile.velocity = (base.Projectile.velocity * 44f + base.Projectile.SafeDirectionTo(target.Center) * 24f) / 45f;
					base.Projectile.ai[0] += 30f - base.Projectile.ai[0] % 30f;
				}
				base.Projectile.ai[1] = 1f;
			}
			else
			{
				base.Projectile.ai[1] = 0f;
			}
			if (((modulo >= 30f && modulo <= 60f) || (modulo >= 90f && modulo <= 120f)) && base.Projectile.owner == player.whoAmI && base.Projectile.spriteDirection == (base.Projectile.SafeDirectionTo(target.Center).X > 0f).ToDirectionInt())
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.SafeDirectionTo(target.Center) * 11.5f, ModContent.ProjectileType<BloodBreath>(), base.Projectile.damage, 0f, base.Projectile.owner);
			}
		}
		else if (base.Projectile.Distance(player.Center) > 175f)
		{
			base.Projectile.extraUpdates = 0;
			base.Projectile.ai[1] = 0f;
			base.Projectile.velocity = (base.Projectile.velocity * 24f + base.Projectile.SafeDirectionTo(player.Center) * 16f) / 25f;
			if (base.Projectile.Distance(player.Center) > 3250f)
			{
				base.Projectile.Center = player.Center;
				ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Projectile p2 = enumerator.Current;
					if (p2.owner == base.Projectile.owner && p2.type == ModContent.ProjectileType<SkeletalDragonChild>())
					{
						p2.Center = player.Center;
						p2.netUpdate = true;
					}
				}
				base.Projectile.netUpdate = true;
			}
		}
		base.Projectile.direction = (base.Projectile.spriteDirection = (base.Projectile.velocity.X > 0f).ToDirectionInt());
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
	}

	public override bool MinionContactDamage()
	{
		return true;
	}
}
