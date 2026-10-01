using System;
using CalamityMod.CalPlayer;
using CalamityMod.NPCs;
using CalamityMod.Projectiles.Healing;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

[PierceResistException(false)]
public class FungalClumpMinion : ModProjectile, ILocalizedModType, IModType
{
	private bool returnToPlayer;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 50;
		base.Projectile.height = 50;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0553: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0563: Unknown result type (might be due to invalid IL or missing references)
		//IL_0568: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0597: Unknown result type (might be due to invalid IL or missing references)
		//IL_0875: Unknown result type (might be due to invalid IL or missing references)
		//IL_087f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0884: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0809: Unknown result type (might be due to invalid IL or missing references)
		//IL_0810: Unknown result type (might be due to invalid IL or missing references)
		//IL_0815: Unknown result type (might be due to invalid IL or missing references)
		//IL_0817: Unknown result type (might be due to invalid IL or missing references)
		//IL_0824: Unknown result type (might be due to invalid IL or missing references)
		//IL_0829: Unknown result type (might be due to invalid IL or missing references)
		//IL_062a: Unknown result type (might be due to invalid IL or missing references)
		//IL_062e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0633: Unknown result type (might be due to invalid IL or missing references)
		//IL_0641: Unknown result type (might be due to invalid IL or missing references)
		//IL_0648: Unknown result type (might be due to invalid IL or missing references)
		//IL_064d: Unknown result type (might be due to invalid IL or missing references)
		//IL_064f: Unknown result type (might be due to invalid IL or missing references)
		//IL_065c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0661: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		bool correctMinion = base.Projectile.type == ModContent.ProjectileType<FungalClumpMinion>();
		if (!modPlayer.fungalClump && !modPlayer.fungalClumpVanity)
		{
			base.Projectile.active = false;
			return;
		}
		if (correctMinion)
		{
			if (player.dead)
			{
				modPlayer.fClump = false;
			}
			if (modPlayer.fClump)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		base.Projectile.damage = (int)player.GetBestClassDamage().ApplyTo(base.Projectile.originalDamage);
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
		if (base.Projectile.localAI[0] == 0f)
		{
			int dustAmt = 36;
			for (int i = 0; i < dustAmt; i++)
			{
				Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.75f).RotatedBy((float)(i - (dustAmt / 2 - 1)) * ((float)Math.PI * 2f) / (float)dustAmt) + base.Projectile.Center;
				Vector2 velocity = val - base.Projectile.Center;
				int idx = Dust.NewDust(val + velocity, 0, 0, 56, velocity.X * 1.5f, velocity.Y * 1.5f, 100, default(Color), 1.4f);
				Main.dust[idx].noGravity = true;
				Main.dust[idx].noLight = true;
				Main.dust[idx].velocity = velocity;
			}
			base.Projectile.localAI[0]++;
		}
		if (Main.rand.NextBool(16) && !modPlayer.fungalClumpVanity)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 56, base.Projectile.velocity.X * 0.05f, base.Projectile.velocity.Y * 0.05f);
		}
		base.Projectile.MinionAntiClump();
		float playerRange = 500f;
		if (base.Projectile.ai[1] != 0f || base.Projectile.friendly)
		{
			playerRange = 1400f;
		}
		if (Math.Abs(base.Projectile.Center.X - player.Center.X) + Math.Abs(base.Projectile.Center.Y - player.Center.Y) > playerRange)
		{
			returnToPlayer = true;
		}
		Vector2 targetVec = base.Projectile.Center;
		float range = 900f;
		bool npcFound = false;
		Vector2 half = default(Vector2);
		((Vector2)(ref half))._002Ector(0.5f);
		if (!returnToPlayer && !modPlayer.fungalClumpVanity)
		{
			if (player.HasMinionAttackTargetNPC)
			{
				NPC npc = Main.npc[player.MinionAttackTargetNPC];
				if (npc.CanBeChasedBy(base.Projectile))
				{
					Vector2 sizeCheck = npc.position + npc.Size * half;
					float targetDist = Vector2.Distance(npc.Center, base.Projectile.Center);
					bool canHit = Collision.CanHitLine(base.Projectile.position, base.Projectile.width, base.Projectile.height, npc.position, npc.width, npc.height);
					if ((!npcFound && targetDist < range) & canHit)
					{
						range = targetDist;
						targetVec = sizeCheck;
						npcFound = true;
					}
				}
			}
			if (!npcFound)
			{
				for (int npcIndex = 0; npcIndex < Main.maxNPCs; npcIndex++)
				{
					NPC npc2 = Main.npc[npcIndex];
					if (npc2.CanBeChasedBy(base.Projectile))
					{
						Vector2 sizeCheck2 = npc2.position + npc2.Size * half;
						float targetDist2 = Vector2.Distance(npc2.Center, base.Projectile.Center);
						bool canHit2 = Collision.CanHitLine(base.Projectile.position, base.Projectile.width, base.Projectile.height, npc2.position, npc2.width, npc2.height);
						if ((!npcFound && targetDist2 < range) & canHit2)
						{
							range = targetDist2;
							targetVec = sizeCheck2;
							npcFound = true;
						}
					}
				}
			}
		}
		base.Projectile.tileCollide = !returnToPlayer;
		if (!npcFound)
		{
			base.Projectile.friendly = true;
			float homingSpeed = 8f;
			float turnSpeed = 20f;
			if (returnToPlayer)
			{
				homingSpeed = 12f;
			}
			Vector2 playerVector = player.Center - base.Projectile.Center;
			playerVector.Y -= 60f;
			float num = ((Vector2)(ref playerVector)).Length();
			if (num < 100f && returnToPlayer && !Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
			{
				returnToPlayer = false;
			}
			if (num > 2000f)
			{
				base.Projectile.position.X = player.Center.X - (float)(base.Projectile.width / 2);
				base.Projectile.position.Y = player.Center.Y - (float)(base.Projectile.width / 2);
			}
			if (num > 70f)
			{
				((Vector2)(ref playerVector)).Normalize();
				playerVector *= homingSpeed;
				base.Projectile.velocity = (base.Projectile.velocity * turnSpeed + playerVector) / (turnSpeed + 1f);
			}
			else
			{
				if (base.Projectile.velocity.X == 0f && base.Projectile.velocity.Y == 0f)
				{
					base.Projectile.velocity.X = -0.15f;
					base.Projectile.velocity.Y = -0.05f;
				}
				Projectile projectile = base.Projectile;
				projectile.velocity *= 1.01f;
			}
			base.Projectile.friendly = false;
			base.Projectile.rotation = base.Projectile.velocity.X * 0.05f;
			if (!(Math.Abs(base.Projectile.velocity.X) <= 0f))
			{
				base.Projectile.spriteDirection = -base.Projectile.direction;
			}
			return;
		}
		if (base.Projectile.ai[1] == -1f)
		{
			base.Projectile.ai[1] = 17f;
		}
		if (base.Projectile.ai[1] > 0f)
		{
			base.Projectile.ai[1]--;
		}
		if (base.Projectile.ai[1] == 0f)
		{
			base.Projectile.friendly = true;
			float minionSpeed = 8f;
			float turnSpeed2 = 14f;
			if (base.Projectile.Distance(targetVec) < 100f)
			{
				minionSpeed = 10f;
			}
			Vector2 homingVelocity = base.Projectile.SafeDirectionTo(targetVec) * minionSpeed;
			base.Projectile.velocity = (base.Projectile.velocity * turnSpeed2 + homingVelocity) / (turnSpeed2 + 1f);
		}
		else
		{
			base.Projectile.friendly = false;
			if (Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y) < 10f)
			{
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 1.05f;
			}
		}
		base.Projectile.rotation = base.Projectile.velocity.X * 0.05f;
		if (!(Math.Abs(base.Projectile.velocity.X) <= 0.2f))
		{
			base.Projectile.spriteDirection = -base.Projectile.direction;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		Main.player[base.Projectile.owner].SpawnLifeStealProjectile(target, base.Projectile, ModContent.ProjectileType<FungalHeal>(), 1, 2f);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override bool? CanDamage()
	{
		if (Main.player[base.Projectile.owner].Calamity().fungalClumpVanity)
		{
			return false;
		}
		return null;
	}
}
