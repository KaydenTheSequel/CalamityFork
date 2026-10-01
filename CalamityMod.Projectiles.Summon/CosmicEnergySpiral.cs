using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class CosmicEnergySpiral : ModProjectile, ILocalizedModType, IModType
{
	private bool justSpawned = true;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 78;
		base.Projectile.height = 78;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 10f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.extraUpdates = 1;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_068e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06df: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_073e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0753: Unknown result type (might be due to invalid IL or missing references)
		//IL_0799: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a9: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		Lighting.AddLight((int)base.Projectile.Center.X / 16, (int)base.Projectile.Center.Y / 16, (float)Main.DiscoR / 255f, (float)Main.DiscoG / 255f, (float)Main.DiscoB / 255f);
		bool num = base.Projectile.type == ModContent.ProjectileType<CosmicEnergySpiral>();
		player.AddBuff(ModContent.BuffType<CosmicEnergy>(), 3600);
		if (num)
		{
			if (player.dead)
			{
				modPlayer.cEnergy = false;
			}
			if (modPlayer.cEnergy)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		float targetDist = 1400f;
		base.Projectile.rotation += base.Projectile.velocity.X * 0.1f;
		Vector2 projPos = base.Projectile.position;
		bool canAttack = false;
		int target = 0;
		if (player.HasMinionAttackTargetNPC)
		{
			NPC npc = Main.npc[player.MinionAttackTargetNPC];
			if (npc.CanBeChasedBy(base.Projectile))
			{
				float maxTargetDist = Vector2.Distance(npc.Center, base.Projectile.Center);
				if (!canAttack && maxTargetDist < targetDist)
				{
					projPos = npc.Center;
					canAttack = true;
					target = npc.whoAmI;
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
					float maxTargetDist2 = Vector2.Distance(nPC2.Center, base.Projectile.Center);
					if (!canAttack && maxTargetDist2 < targetDist)
					{
						targetDist = maxTargetDist2;
						projPos = nPC2.Center;
						canAttack = true;
						target = nPC2.whoAmI;
					}
				}
			}
		}
		float separationAnxietyDist = 1600f;
		if (canAttack)
		{
			separationAnxietyDist = 2400f;
		}
		if (Vector2.Distance(player.Center, base.Projectile.Center) > separationAnxietyDist)
		{
			base.Projectile.ai[1] = 1f;
			base.Projectile.netUpdate = true;
		}
		if (canAttack && base.Projectile.ai[1] == 0f)
		{
			Vector2 projDirection = projPos - base.Projectile.Center;
			float num2 = ((Vector2)(ref projDirection)).Length();
			((Vector2)(ref projDirection)).Normalize();
			if (num2 > 200f)
			{
				float scaleFactor2 = 6f;
				projDirection *= scaleFactor2;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + projDirection) / 41f;
			}
			else
			{
				projDirection *= -4f;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + projDirection) / 41f;
			}
		}
		else
		{
			bool isReturning = false;
			if (!isReturning)
			{
				isReturning = base.Projectile.ai[1] == 1f;
			}
			float returnSpeed = 6f;
			if (isReturning)
			{
				returnSpeed = 15f;
			}
			Vector2 center2 = base.Projectile.Center;
			Vector2 playerDirection = player.Center - center2 + new Vector2(0f, -60f);
			float num3 = ((Vector2)(ref playerDirection)).Length();
			if (num3 > 200f && returnSpeed < 8f)
			{
				returnSpeed = 8f;
			}
			if (((num3 < 800f) & isReturning) && !Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
			{
				base.Projectile.ai[1] = 0f;
				base.Projectile.netUpdate = true;
			}
			if (num3 > 2000f)
			{
				base.Projectile.position.X = Main.player[base.Projectile.owner].Center.X - (float)(base.Projectile.width / 2);
				base.Projectile.position.Y = Main.player[base.Projectile.owner].Center.Y - (float)(base.Projectile.height / 2);
				base.Projectile.netUpdate = true;
			}
			if (num3 > 70f)
			{
				((Vector2)(ref playerDirection)).Normalize();
				playerDirection *= returnSpeed;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + playerDirection) / 41f;
			}
			else if (base.Projectile.velocity.X == 0f && base.Projectile.velocity.Y == 0f)
			{
				base.Projectile.velocity.X = -0.15f;
				base.Projectile.velocity.Y = -0.05f;
			}
		}
		float projScale = (float)(int)Main.mouseTextColor / 200f - 0.35f;
		projScale *= 0.2f;
		base.Projectile.scale = projScale + 0.95f;
		if (justSpawned)
		{
			justSpawned = false;
			base.Projectile.ai[0] = 100f;
		}
		if (base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		if (base.Projectile.ai[0] != 0f)
		{
			base.Projectile.ai[0]--;
			return;
		}
		float projX = base.Projectile.position.X;
		float projY = base.Projectile.position.Y;
		float homeDistance = 1200f;
		bool isInRange = false;
		ActiveEntityIterator<NPC>.Enumerator enumerator2 = Main.ActiveNPCs.GetEnumerator();
		while (enumerator2.MoveNext())
		{
			NPC n = enumerator2.Current;
			if (n.CanBeChasedBy(base.Projectile))
			{
				float npcX = n.position.X + (float)(n.width / 2);
				float npcY = n.position.Y + (float)(n.height / 2);
				float npcDistance = Math.Abs(base.Projectile.position.X + (float)(base.Projectile.width / 2) - npcX) + Math.Abs(base.Projectile.position.Y + (float)(base.Projectile.height / 2) - npcY);
				if (npcDistance < homeDistance)
				{
					homeDistance = npcDistance;
					projX = npcX;
					projY = npcY;
					isInRange = true;
				}
			}
		}
		if (isInRange)
		{
			SoundEngine.PlaySound(in SoundID.Item105, base.Projectile.position);
			int blastAmt = Main.rand.Next(5, 8);
			for (int b = 0; b < blastAmt; b++)
			{
				Vector2 velocity = CalamityUtils.RandomVelocity(100f, 70f, 100f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<CosmicBlast>(), (int)((double)base.Projectile.damage * 0.5), 2f, base.Projectile.owner, target);
			}
			float projXSpeed = projX - base.Projectile.Center.X;
			float projYSpeed = projY - base.Projectile.Center.Y;
			float velocityMult = (float)Math.Sqrt(projXSpeed * projXSpeed + projYSpeed * projYSpeed);
			velocityMult = 15f / velocityMult;
			projXSpeed *= velocityMult;
			projYSpeed *= velocityMult;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, projXSpeed, projYSpeed, ModContent.ProjectileType<CosmicBlastBig>(), base.Projectile.damage, 3f, base.Projectile.owner, target);
			base.Projectile.ai[0] = 100f;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 255);
	}
}
