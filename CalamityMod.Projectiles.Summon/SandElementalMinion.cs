using System;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class SandElementalMinion : ModProjectile, ILocalizedModType, IModType
{
	public int dust = 3;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 12;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 42;
		base.Projectile.height = 98;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 0f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0543: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0571: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0528: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0660: Unknown result type (might be due to invalid IL or missing references)
		//IL_068a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_087d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0898: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08be: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08db: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e0: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (!modPlayer.sandElemental && !modPlayer.allElementals && !modPlayer.sandElementalVanity && !modPlayer.allElementalsVanity)
		{
			base.Projectile.active = false;
			return;
		}
		if (base.Projectile.type == ModContent.ProjectileType<SandElementalMinion>())
		{
			if (player.dead)
			{
				modPlayer.sandEleBuff = false;
			}
			if (modPlayer.sandEleBuff)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		this.dust--;
		if (this.dust >= 0)
		{
			for (int i = 0; i < 50; i++)
			{
				int dust = Dust.NewDust(new Vector2(base.Projectile.position.X, base.Projectile.position.Y + 16f), base.Projectile.width, base.Projectile.height - 16, 32);
				Dust obj = Main.dust[dust];
				obj.velocity *= 2f;
				Main.dust[dust].scale *= 1.15f;
			}
		}
		bool passive = modPlayer.sandElementalVanity || modPlayer.allElementalsVanity;
		if (Math.Abs(base.Projectile.velocity.X) > 0.2f)
		{
			base.Projectile.spriteDirection = -base.Projectile.direction;
		}
		float attackDistance = 700f;
		if (!passive)
		{
			float lights = (float)Main.rand.Next(90, 111) * 0.01f;
			lights *= Main.essScale;
			Lighting.AddLight(base.Projectile.Center, 0.7f * lights, 0.6f * lights, 0f * lights);
		}
		base.Projectile.MinionAntiClump();
		Vector2 projPos = base.Projectile.position;
		bool canAttack = false;
		if (base.Projectile.ai[0] != 1f)
		{
			base.Projectile.tileCollide = false;
		}
		if (base.Projectile.tileCollide && WorldGen.SolidTile(Framing.GetTileSafely((int)base.Projectile.Center.X / 16, (int)base.Projectile.Center.Y / 16)))
		{
			base.Projectile.tileCollide = false;
		}
		if (player.HasMinionAttackTargetNPC && !passive)
		{
			NPC npc = Main.npc[player.MinionAttackTargetNPC];
			if (npc.CanBeChasedBy(base.Projectile))
			{
				float targetDist = Vector2.Distance(npc.Center, base.Projectile.Center);
				if (!canAttack && targetDist < attackDistance)
				{
					projPos = npc.Center;
					canAttack = true;
				}
			}
		}
		if (!canAttack && !passive)
		{
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC nPC2 = enumerator.Current;
				if (nPC2.CanBeChasedBy(base.Projectile))
				{
					float targetDist2 = Vector2.Distance(nPC2.Center, base.Projectile.Center);
					if (!canAttack && targetDist2 < attackDistance && Collision.CanHitLine(base.Projectile.position, base.Projectile.width, base.Projectile.height, nPC2.position, nPC2.width, nPC2.height))
					{
						attackDistance = targetDist2;
						projPos = nPC2.Center;
						canAttack = true;
					}
				}
			}
		}
		float separationAnxietyDist = 800f;
		if (canAttack && !passive)
		{
			if (base.Projectile.frame < 6)
			{
				base.Projectile.frame = 6;
			}
			base.Projectile.frameCounter++;
			if (base.Projectile.frameCounter > 7)
			{
				base.Projectile.frame++;
				base.Projectile.frameCounter = 0;
			}
			if (base.Projectile.frame > 11)
			{
				base.Projectile.frame = 6;
			}
			separationAnxietyDist = 1200f;
		}
		else
		{
			base.Projectile.frameCounter++;
			if (base.Projectile.frameCounter > 7)
			{
				base.Projectile.frame++;
				base.Projectile.frameCounter = 0;
			}
			if (base.Projectile.frame > 5)
			{
				base.Projectile.frame = 0;
			}
		}
		if (Vector2.Distance(player.Center, base.Projectile.Center) > separationAnxietyDist)
		{
			base.Projectile.ai[0] = 1f;
			base.Projectile.tileCollide = false;
			base.Projectile.netUpdate = true;
		}
		if (canAttack && base.Projectile.ai[0] == 0f)
		{
			Vector2 targetDirection = projPos - base.Projectile.Center;
			float num = ((Vector2)(ref targetDirection)).Length();
			((Vector2)(ref targetDirection)).Normalize();
			if (num > 200f)
			{
				float scaleFactor2 = 8f;
				targetDirection *= scaleFactor2;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + targetDirection) / 41f;
			}
			else
			{
				targetDirection *= -4f;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + targetDirection) / 41f;
			}
		}
		else
		{
			bool isReturning = false;
			if (!isReturning)
			{
				isReturning = base.Projectile.ai[0] == 1f;
			}
			float returnSpeed = 6f;
			if (isReturning)
			{
				returnSpeed = 15f;
			}
			Vector2 center2 = base.Projectile.Center;
			Vector2 playerDirection = player.Center - center2 + new Vector2(250f, -60f);
			float num2 = ((Vector2)(ref playerDirection)).Length();
			if (num2 > 200f && returnSpeed < 8f)
			{
				returnSpeed = 8f;
			}
			if (((num2 < 200f) & isReturning) && !Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
			{
				base.Projectile.ai[0] = 0f;
				base.Projectile.netUpdate = true;
			}
			if (num2 > 2000f)
			{
				base.Projectile.position.X = player.Center.X - (float)(base.Projectile.width / 2);
				base.Projectile.position.Y = player.Center.Y - (float)(base.Projectile.height / 2);
				base.Projectile.netUpdate = true;
			}
			if (num2 > 70f)
			{
				((Vector2)(ref playerDirection)).Normalize();
				playerDirection *= returnSpeed;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + playerDirection) / 41f;
			}
			else if (base.Projectile.velocity.X == 0f && base.Projectile.velocity.Y == 0f)
			{
				base.Projectile.velocity.X = -0.195f;
				base.Projectile.velocity.Y = -0.095f;
			}
		}
		if (base.Projectile.ai[1] > 0f)
		{
			base.Projectile.ai[1] += Main.rand.Next(1, 4);
		}
		if (base.Projectile.ai[1] > 220f)
		{
			base.Projectile.ai[1] = 0f;
			base.Projectile.netUpdate = true;
		}
		if (base.Projectile.localAI[0] < 120f)
		{
			base.Projectile.localAI[0]++;
		}
		if (base.Projectile.ai[0] != 0f)
		{
			return;
		}
		float scaleFactor3 = 11f;
		int projType = ModContent.ProjectileType<SandBolt>();
		if (canAttack && base.Projectile.ai[1] == 0f && base.Projectile.localAI[0] >= 120f)
		{
			base.Projectile.ai[1]++;
			if (Main.myPlayer == base.Projectile.owner && Collision.CanHitLine(base.Projectile.position, base.Projectile.width, base.Projectile.height, projPos, 0, 0))
			{
				Vector2 projVel = projPos - base.Projectile.Center;
				((Vector2)(ref projVel)).Normalize();
				projVel *= scaleFactor3;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, projVel, projType, base.Projectile.damage, 0f, Main.myPlayer);
				base.Projectile.netUpdate = true;
			}
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
