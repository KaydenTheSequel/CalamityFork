using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class HowlsHeartFireball : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.netImportant = true;
		base.Projectile.timeLeft = 180;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter >= 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		Vector2 center = base.Projectile.Center;
		float maxDistance = 500f;
		bool homeIn = false;
		int target = (int)base.Projectile.ai[0];
		if (player.HasMinionAttackTargetNPC)
		{
			NPC npc = Main.npc[player.MinionAttackTargetNPC];
			if (npc.CanBeChasedBy(base.Projectile))
			{
				float extraDistance = npc.width / 2 + npc.height / 2;
				bool canHit = true;
				if (extraDistance < maxDistance)
				{
					canHit = Collision.CanHit(base.Projectile.Center, 1, 1, npc.Center, 1, 1);
				}
				if ((Vector2.Distance(npc.Center, base.Projectile.Center) < maxDistance + extraDistance) & canHit)
				{
					center = npc.Center;
					homeIn = true;
				}
			}
		}
		else if (Main.npc[target].CanBeChasedBy(base.Projectile))
		{
			NPC npc2 = Main.npc[target];
			float extraDistance2 = npc2.width / 2 + npc2.height / 2;
			bool canHit2 = true;
			if (extraDistance2 < maxDistance)
			{
				canHit2 = Collision.CanHit(base.Projectile.Center, 1, 1, npc2.Center, 1, 1);
			}
			if ((Vector2.Distance(npc2.Center, base.Projectile.Center) < maxDistance + extraDistance2) & canHit2)
			{
				center = npc2.Center;
				homeIn = true;
			}
		}
		if (!homeIn)
		{
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC npc3 = enumerator.Current;
				if (npc3.CanBeChasedBy(base.Projectile))
				{
					float extraDistance3 = npc3.width / 2 + npc3.height / 2;
					bool canHit3 = true;
					if (extraDistance3 < maxDistance)
					{
						canHit3 = Collision.CanHit(base.Projectile.Center, 1, 1, npc3.Center, 1, 1);
					}
					if ((Vector2.Distance(npc3.Center, base.Projectile.Center) < maxDistance + extraDistance3) & canHit3)
					{
						center = npc3.Center;
						homeIn = true;
						break;
					}
				}
			}
		}
		if (homeIn)
		{
			Vector2 moveDirection = base.Projectile.SafeDirectionTo(center, Vector2.UnitY);
			base.Projectile.velocity = (base.Projectile.velocity * 20f + moveDirection * 21f) / 21f;
		}
		int blueT = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 59, 0f, 0f, 100, default(Color), 0.6f);
		Main.dust[blueT].noGravity = true;
		Dust obj = Main.dust[blueT];
		obj.velocity *= 0.5f;
		Dust obj2 = Main.dust[blueT];
		obj2.velocity += base.Projectile.velocity * 0.1f;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item45, base.Projectile.position);
		int blue = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 59, 0f, 0f, 100);
		Dust obj = Main.dust[blue];
		obj.velocity *= 0.5f;
		if (Main.rand.NextBool())
		{
			Main.dust[blue].scale = 0.5f;
			Main.dust[blue].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
		}
		int torch = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 59, 0f, 0f, 100, default(Color), 1.4f);
		Main.dust[torch].noGravity = true;
		Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 59, 0f, 0f, 100, default(Color), 0.8f);
	}
}
