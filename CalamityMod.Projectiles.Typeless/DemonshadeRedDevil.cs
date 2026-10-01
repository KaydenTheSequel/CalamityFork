using System;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class DemonshadeRedDevil : ModProjectile, ILocalizedModType, IModType
{
	public int dust = 3;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public ref float State => ref base.Projectile.ai[0];

	public ref float Timer => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 48;
		base.Projectile.height = 48;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft *= 5;
	}

	public override void AI()
	{
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Unknown result type (might be due to invalid IL or missing references)
		//IL_057c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0605: Unknown result type (might be due to invalid IL or missing references)
		//IL_060a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06de: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_070b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0710: Unknown result type (might be due to invalid IL or missing references)
		bool isMinion = base.Projectile.type == ModContent.ProjectileType<DemonshadeRedDevil>();
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (!modPlayer.redDevil)
		{
			base.Projectile.active = false;
			return;
		}
		if (isMinion)
		{
			if (player.dead)
			{
				modPlayer.rDevil = false;
			}
			if (modPlayer.rDevil)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		dust--;
		if (dust >= 0)
		{
			for (int i = 0; i < 50; i++)
			{
				int brimDust = Dust.NewDust(new Vector2(base.Projectile.position.X, base.Projectile.position.Y + 16f), base.Projectile.width, base.Projectile.height - 16, 235);
				Dust obj = Main.dust[brimDust];
				obj.velocity *= 2f;
				Main.dust[brimDust].scale *= 1.15f;
			}
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 8)
		{
			base.Projectile.frameCounter = 0;
			base.Projectile.frame++;
			if (base.Projectile.frame > 4)
			{
				base.Projectile.frame = 0;
			}
		}
		float lights = (float)Main.rand.Next(90, 111) * 0.01f;
		lights *= Main.essScale;
		Lighting.AddLight(base.Projectile.Center, 1f * lights, 0f * lights, 0.15f * lights);
		base.Projectile.rotation = base.Projectile.velocity.X * 0.04f;
		if ((double)Math.Abs(base.Projectile.velocity.X) > 0.2)
		{
			base.Projectile.spriteDirection = -base.Projectile.direction;
		}
		if (State == 2f)
		{
			Timer++;
			if (Timer > 60f)
			{
				Timer = 1f;
				State = 0f;
				base.Projectile.netUpdate = true;
			}
			return;
		}
		Vector2 attackPos = base.Projectile.position;
		float attackRange = 2000f;
		bool canAttack = false;
		if (player.HasMinionAttackTargetNPC)
		{
			NPC npc = Main.npc[player.MinionAttackTargetNPC];
			if (npc.CanBeChasedBy(base.Projectile))
			{
				float npcDist = Vector2.Distance(npc.Center, base.Projectile.Center);
				if (!canAttack && npcDist < attackRange)
				{
					attackRange = npcDist;
					attackPos = npc.Center;
					canAttack = true;
				}
			}
		}
		else
		{
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC nPC2 = enumerator.Current;
				if (nPC2.CanBeChasedBy(base.Projectile))
				{
					float npcDist2 = Vector2.Distance(nPC2.Center, base.Projectile.Center);
					if (!canAttack && npcDist2 < attackRange && Collision.CanHitLine(base.Projectile.position, base.Projectile.width, base.Projectile.height, nPC2.position, nPC2.width, nPC2.height))
					{
						attackRange = npcDist2;
						attackPos = nPC2.Center;
						canAttack = true;
					}
				}
			}
		}
		float separationAnxietyDist = (canAttack ? 3000f : 2000f);
		if (Vector2.Distance(player.Center, base.Projectile.Center) > separationAnxietyDist)
		{
			State = 1f;
			base.Projectile.netUpdate = true;
		}
		if (canAttack && State == 0f)
		{
			Vector2 projDirection = attackPos - base.Projectile.Center;
			float num = ((Vector2)(ref projDirection)).Length();
			((Vector2)(ref projDirection)).Normalize();
			if (num > 200f)
			{
				projDirection *= 25f;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + projDirection) / 41f;
			}
			else
			{
				projDirection *= -9f;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + projDirection) / 41f;
			}
		}
		else
		{
			bool isReturning = State == 1f;
			float returnSpeed = (isReturning ? 24f : 15f);
			Vector2 playerDirection = player.Center - base.Projectile.Center + new Vector2(0f, -30f);
			float num2 = ((Vector2)(ref playerDirection)).Length();
			if (num2 > 200f && returnSpeed < 10f)
			{
				returnSpeed = 18f;
			}
			if (((num2 < 150f) & isReturning) && !Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
			{
				State = 0f;
				base.Projectile.netUpdate = true;
			}
			if (num2 > 2000f)
			{
				base.Projectile.Center = player.Center;
				base.Projectile.netUpdate = true;
			}
			if (num2 > 70f)
			{
				((Vector2)(ref playerDirection)).Normalize();
				playerDirection *= returnSpeed;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + playerDirection) / 41f;
			}
			else if (base.Projectile.velocity == Vector2.Zero)
			{
				base.Projectile.velocity = new Vector2(-0.2f, -0.1f);
			}
		}
		if (Timer > 0f)
		{
			Timer += Main.rand.Next(1, 3);
		}
		if (Timer > 80f)
		{
			Timer = 0f;
			base.Projectile.netUpdate = true;
		}
		if (State != 0f || !canAttack || Timer != 0f)
		{
			return;
		}
		Timer++;
		if (Main.myPlayer == base.Projectile.owner && Collision.CanHitLine(base.Projectile.Center, base.Projectile.width, base.Projectile.height, attackPos, 0, 0))
		{
			Vector2 velocity = Vector2.Normalize(attackPos - base.Projectile.Center) * 24f;
			int trident = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, 114, base.Projectile.damage, 0f, Main.myPlayer);
			if (trident.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[trident].timeLeft = 300;
				Main.projectile[trident].usesLocalNPCImmunity = true;
				Main.projectile[trident].localNPCHitCooldown = 10;
				Main.projectile[trident].DamageType = DamageClass.Generic;
			}
			base.Projectile.netUpdate = true;
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
