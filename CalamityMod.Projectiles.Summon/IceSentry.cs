using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class IceSentry : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 18;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 94;
		base.Projectile.height = 94;
		base.Projectile.timeLeft = 36000;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.sentry = true;
		base.Projectile.coldDamage = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.player[base.Projectile.owner];
		base.Projectile.velocity = Vector2.Zero;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frameCounter = 0;
			base.Projectile.frame++;
		}
		if (base.Projectile.ai[1] < 300f)
		{
			base.Projectile.localAI[1] = 1f;
			if (base.Projectile.frame >= 9)
			{
				base.Projectile.frame = 0;
			}
		}
		else
		{
			if (base.Projectile.frame >= 18)
			{
				base.Projectile.frame = 9;
			}
			base.Projectile.localAI[1]++;
			if (base.Projectile.localAI[1] > 2f)
			{
				base.Projectile.localAI[1] = 0f;
				if (base.Projectile.owner == Main.myPlayer)
				{
					Vector2 speed = default(Vector2);
					((Vector2)(ref speed))._002Ector((float)Main.rand.Next(-1000, 1001), (float)Main.rand.Next(-1000, 1001));
					((Vector2)(ref speed)).Normalize();
					speed *= 15f;
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + speed, speed, ModContent.ProjectileType<IceSentryShard>(), base.Projectile.damage / 2, base.Projectile.knockBack / 2f, base.Projectile.owner);
				}
			}
		}
		NPC minionAttackTargetNpc = base.Projectile.OwnerMinionAttackTargetNPC;
		if (minionAttackTargetNpc != null && base.Projectile.ai[0] != (float)minionAttackTargetNpc.whoAmI && minionAttackTargetNpc.CanBeChasedBy(base.Projectile) && Collision.CanHit(base.Projectile.Center, 0, 0, minionAttackTargetNpc.position, minionAttackTargetNpc.width, minionAttackTargetNpc.height))
		{
			base.Projectile.ai[0] = minionAttackTargetNpc.whoAmI;
			base.Projectile.ai[1] = 0f;
			base.Projectile.localAI[0] = 0f;
			base.Projectile.netUpdate = true;
		}
		if (base.Projectile.ai[0] >= 0f && base.Projectile.ai[0] < (float)Main.maxNPCs)
		{
			NPC npc = Main.npc[(int)base.Projectile.ai[0]];
			bool rememberTarget = npc.CanBeChasedBy(base.Projectile);
			if (rememberTarget)
			{
				base.Projectile.localAI[0]++;
				if (base.Projectile.ai[1] < 300f)
				{
					base.Projectile.ai[1]++;
				}
				float delay = 60f - base.Projectile.ai[1] / 60f * 10f;
				if (base.Projectile.localAI[0] > delay)
				{
					base.Projectile.localAI[0] = 0f;
					rememberTarget = Collision.CanHit(base.Projectile.Center, 0, 0, npc.position, npc.width, npc.height);
					if (rememberTarget && base.Projectile.owner == Main.myPlayer)
					{
						Vector2 iceSpeed = CalamityUtils.CalculatePredictiveAimToTarget(base.Projectile.Center, npc, 12f * (Utils.GetLerpValue(0f, 1000f, Vector2.Distance(base.Projectile.Center, npc.Center)) + 1f));
						if (base.Projectile.ai[1] >= 300f)
						{
							iceSpeed = iceSpeed.RotatedByRandom(0.07853981852531433);
						}
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, iceSpeed, ModContent.ProjectileType<IceSentryFrostBolt>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
					}
				}
			}
			if (!rememberTarget)
			{
				base.Projectile.ai[0] = -1f;
				base.Projectile.ai[1] = 0f;
				base.Projectile.netUpdate = true;
			}
			return;
		}
		base.Projectile.localAI[0] = 0f;
		float maxDistance = 1000f;
		int possibleTarget = -1;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC npc2 = enumerator.Current;
			if (npc2.CanBeChasedBy(base.Projectile))
			{
				float npcDistance = base.Projectile.Distance(npc2.Center);
				if (npcDistance < maxDistance)
				{
					maxDistance = npcDistance;
					possibleTarget = npc2.whoAmI;
				}
			}
		}
		if (possibleTarget > 0)
		{
			base.Projectile.ai[0] = possibleTarget;
			base.Projectile.ai[1] = 0f;
			base.Projectile.netUpdate = true;
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
