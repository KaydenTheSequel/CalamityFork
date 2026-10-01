using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class DankCreeperMinion : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.tileCollide = false;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = base.Projectile.MaxUpdates * 30;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0790: Unknown result type (might be due to invalid IL or missing references)
		//IL_0795: Unknown result type (might be due to invalid IL or missing references)
		//IL_0798: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0582: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_087f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0889: Unknown result type (might be due to invalid IL or missing references)
		//IL_088e: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (base.Projectile.localAI[0] == 0f)
		{
			int constant = 36;
			for (int i = 0; i < constant; i++)
			{
				Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.75f).RotatedBy((float)(i - (constant / 2 - 1)) * ((float)Math.PI * 2f) / (float)constant) + base.Projectile.Center;
				Vector2 faceDirection = val - base.Projectile.Center;
				int dust = Dust.NewDust(val + faceDirection, 0, 0, 14, faceDirection.X * 1.5f, faceDirection.Y * 1.5f, 100, default(Color), 1.4f);
				Main.dust[dust].noGravity = true;
				Main.dust[dust].noLight = true;
				Main.dust[dust].velocity = faceDirection;
			}
			base.Projectile.localAI[0]++;
		}
		bool num = base.Projectile.type == ModContent.ProjectileType<DankCreeperMinion>();
		player.AddBuff(ModContent.BuffType<DankCreeperBuff>(), 3600);
		if (num)
		{
			if (player.dead)
			{
				modPlayer.dCreeper = false;
			}
			if (modPlayer.dCreeper)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		base.Projectile.MinionAntiClump();
		float projX = base.Projectile.position.X;
		float projY = base.Projectile.position.Y;
		float attackRange = 1300f;
		bool canAttack = false;
		int separationAnxietyDist = 1100;
		if (base.Projectile.ai[1] != 0f)
		{
			separationAnxietyDist = 1800;
		}
		if (Math.Abs(base.Projectile.Center.X - Main.player[base.Projectile.owner].Center.X) + Math.Abs(base.Projectile.Center.Y - Main.player[base.Projectile.owner].Center.Y) > (float)separationAnxietyDist)
		{
			base.Projectile.ai[0] = 1f;
		}
		if (base.Projectile.ai[0] == 0f)
		{
			if (player.HasMinionAttackTargetNPC)
			{
				NPC npc = Main.npc[player.MinionAttackTargetNPC];
				if (npc.CanBeChasedBy(base.Projectile))
				{
					float npcX = npc.position.X + (float)(npc.width / 2);
					float npcY = npc.position.Y + (float)(npc.height / 2);
					if (Math.Abs(base.Projectile.position.X + (float)(base.Projectile.width / 2) - npcX) + Math.Abs(base.Projectile.position.Y + (float)(base.Projectile.height / 2) - npcY) < attackRange && Collision.CanHit(base.Projectile.position, base.Projectile.width, base.Projectile.height, npc.position, npc.width, npc.height))
					{
						projX = npcX;
						projY = npcY;
						canAttack = true;
					}
				}
			}
			if (!canAttack)
			{
				ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					NPC n = enumerator.Current;
					if (n.CanBeChasedBy(base.Projectile))
					{
						float otherNPCX = n.position.X + (float)(n.width / 2);
						float otherNPCY = n.position.Y + (float)(n.height / 2);
						float otherNPCDist = Math.Abs(base.Projectile.position.X + (float)(base.Projectile.width / 2) - otherNPCX) + Math.Abs(base.Projectile.position.Y + (float)(base.Projectile.height / 2) - otherNPCY);
						if (otherNPCDist < attackRange && Collision.CanHit(base.Projectile.position, base.Projectile.width, base.Projectile.height, n.position, n.width, n.height))
						{
							attackRange = otherNPCDist;
							projX = otherNPCX;
							projY = otherNPCY;
							canAttack = true;
						}
					}
				}
			}
		}
		if (!canAttack)
		{
			float returnSpeed = 8f;
			if (base.Projectile.ai[0] == 1f)
			{
				returnSpeed = 12f;
			}
			Vector2 playerDirection = base.Projectile.Center;
			float playerXDist = player.Center.X - playerDirection.X;
			float playerYDist = player.Center.Y - playerDirection.Y - 60f;
			float playerDist = (float)Math.Sqrt(playerXDist * playerXDist + playerYDist * playerYDist);
			if (playerDist < 100f && base.Projectile.ai[0] == 1f && !Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
			{
				base.Projectile.ai[0] = 0f;
			}
			if (playerDist > 2000f)
			{
				base.Projectile.position.X = player.Center.X - (float)(base.Projectile.width / 2);
				base.Projectile.position.Y = player.Center.Y - (float)(base.Projectile.width / 2);
			}
			if (playerDist > 70f)
			{
				playerDist = returnSpeed / playerDist;
				playerXDist *= playerDist;
				playerYDist *= playerDist;
				base.Projectile.velocity.X = (base.Projectile.velocity.X * 20f + playerXDist) / 21f;
				base.Projectile.velocity.Y = (base.Projectile.velocity.Y * 20f + playerYDist) / 21f;
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
			base.Projectile.rotation = base.Projectile.velocity.X * 0.05f;
			if ((double)Math.Abs(base.Projectile.velocity.X) > 0.2)
			{
				base.Projectile.spriteDirection = -base.Projectile.direction;
			}
			return;
		}
		if (base.Projectile.ai[1] == -1f)
		{
			base.Projectile.ai[1] = 11f;
		}
		if (base.Projectile.ai[1] > 0f)
		{
			base.Projectile.ai[1]--;
		}
		if (base.Projectile.ai[1] == 0f)
		{
			float hoverSpeed = 8f;
			Vector2 playerDirectionAgain = base.Projectile.Center;
			float playerXDistAgain = projX - playerDirectionAgain.X;
			float playerYDistAgain = projY - playerDirectionAgain.Y;
			float playerDistAgain = (float)Math.Sqrt(playerXDistAgain * playerXDistAgain + playerYDistAgain * playerYDistAgain);
			if (playerDistAgain < 100f)
			{
				hoverSpeed = 10f;
			}
			playerDistAgain = hoverSpeed / playerDistAgain;
			playerXDistAgain *= playerDistAgain;
			playerYDistAgain *= playerDistAgain;
			base.Projectile.velocity.X = (base.Projectile.velocity.X * 40f + playerXDistAgain) / 41f;
			base.Projectile.velocity.Y = (base.Projectile.velocity.Y * 40f + playerYDistAgain) / 41f;
		}
		else if (Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y) < 10f)
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 1.05f;
		}
		base.Projectile.rotation = base.Projectile.velocity.X * 0.05f;
		if ((double)Math.Abs(base.Projectile.velocity.X) > 0.2)
		{
			base.Projectile.spriteDirection = -base.Projectile.direction;
		}
	}

	public override bool MinionContactDamage()
	{
		return true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (base.Projectile.owner == Main.myPlayer)
		{
			base.Projectile.ai[1] = -1f;
			base.Projectile.netUpdate = true;
		}
		target.AddBuff(ModContent.BuffType<BrainRot>(), 90);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
