using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class SakuraBullet : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 20);
		base.Projectile.friendly = true;
		base.Projectile.netImportant = true;
		base.Projectile.timeLeft = 210;
		base.Projectile.extraUpdates = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		base.Projectile.rotation += (Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y)) * 0.02f;
		Vector2 center = base.Projectile.Center;
		float maxDistance = 400f;
		bool homeIn = false;
		if (player.HasMinionAttackTargetNPC)
		{
			NPC npc = Main.npc[player.MinionAttackTargetNPC];
			if ((npc.CanBeChasedBy(base.Projectile) || npc.type == 370) && npc.active)
			{
				float extraDistance = npc.width / 2 + npc.height / 2;
				if (Vector2.Distance(npc.Center, base.Projectile.Center) < maxDistance + extraDistance)
				{
					center = npc.Center;
					homeIn = true;
				}
			}
		}
		else if (base.Projectile.ai[0] != -1f)
		{
			NPC npc2 = Main.npc[(int)base.Projectile.ai[0]];
			if ((npc2.CanBeChasedBy(base.Projectile) || npc2.type == 370) && npc2.active)
			{
				float extraDistance2 = npc2.width / 2 + npc2.height / 2;
				if (Vector2.Distance(npc2.Center, base.Projectile.Center) < maxDistance + extraDistance2)
				{
					center = npc2.Center;
					homeIn = true;
				}
			}
		}
		if (!homeIn)
		{
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC npc3 = enumerator.Current;
				if ((npc3.CanBeChasedBy(base.Projectile) || (npc3.type == 370 && (!npc3.dontTakeDamage || npc3.ai[0] > 9f))) && npc3.active)
				{
					float extraDistance3 = npc3.width / 2 + npc3.height / 2;
					if (Vector2.Distance(npc3.Center, base.Projectile.Center) < maxDistance + extraDistance3)
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
			base.Projectile.velocity = (base.Projectile.velocity * 20f + moveDirection * 11f) / 21f;
		}
		int pinkDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 73, 0f, 0f, 100, default(Color), 0.6f);
		Main.dust[pinkDust].noGravity = true;
		Dust obj = Main.dust[pinkDust];
		obj.velocity *= 0.5f;
		Dust obj2 = Main.dust[pinkDust];
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
		SoundEngine.PlaySound(in SoundID.Item25, base.Projectile.position);
		int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 73, 0f, 0f, 100);
		Dust obj = Main.dust[dust];
		obj.velocity *= 0.5f;
		if (Main.rand.NextBool())
		{
			Main.dust[dust].scale = 0.5f;
			Main.dust[dust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
		}
		int dust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 73, 0f, 0f, 100, default(Color), 1.4f);
		Main.dust[dust2].noGravity = true;
		dust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 73, 0f, 0f, 100, default(Color), 0.8f);
	}
}
