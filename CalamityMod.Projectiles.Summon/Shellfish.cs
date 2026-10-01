using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class Shellfish : ModProjectile, ILocalizedModType, IModType
{
	private int playerStill;

	private bool fly;

	private bool spawnDust = true;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 2;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 28;
		base.Projectile.height = 24;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.minionSlots = 2f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 120;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		fallThrough = false;
		return true;
	}

	public override void AI()
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0680: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0690: Unknown result type (might be due to invalid IL or missing references)
		//IL_069f: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0728: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0781: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0924: Unknown result type (might be due to invalid IL or missing references)
		//IL_092f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0939: Unknown result type (might be due to invalid IL or missing references)
		//IL_093e: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		base.Projectile.Calamity();
		if (this.spawnDust)
		{
			int dustAmt = 20;
			for (int d = 0; d < dustAmt; d++)
			{
				int water = Dust.NewDust(new Vector2(base.Projectile.position.X, base.Projectile.position.Y + 16f), base.Projectile.width, base.Projectile.height - 16, 33);
				Dust obj = Main.dust[water];
				obj.velocity *= 2f;
				Main.dust[water].scale *= 1.15f;
			}
			this.spawnDust = false;
		}
		bool num = base.Projectile.type == ModContent.ProjectileType<Shellfish>();
		player.AddBuff(ModContent.BuffType<ShellfishBuff>(), 3600);
		if (num)
		{
			if (player.dead)
			{
				modPlayer.shellfish = false;
			}
			if (modPlayer.shellfish)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 3)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 1)
		{
			base.Projectile.frame = 0;
		}
		if (base.Projectile.ai[0] == 0f)
		{
			base.Projectile.ai[1]++;
			if (!fly)
			{
				base.Projectile.tileCollide = true;
				Vector2 playerVec = player.Center - base.Projectile.Center;
				float playerDistance = ((Vector2)(ref playerVec)).Length();
				if (base.Projectile.velocity.Y == 0f && (base.Projectile.velocity.X != 0f || playerDistance > 200f))
				{
					float jumpHeight = Utils.SelectRandom<float>(Main.rand, 5f, 7.5f, 10f);
					base.Projectile.velocity.Y -= jumpHeight;
				}
				base.Projectile.velocity.Y += 0.3f;
				float maxDistance = 1000f;
				bool chaseNPC = false;
				float npcPositionX = 0f;
				if (player.HasMinionAttackTargetNPC)
				{
					NPC npc = Main.npc[player.MinionAttackTargetNPC];
					if (npc.CanBeChasedBy(base.Projectile))
					{
						float npcDist = Vector2.Distance(npc.Center, base.Projectile.Center);
						if (!chaseNPC && npcDist < maxDistance)
						{
							npcPositionX = npc.Center.X;
							chaseNPC = true;
						}
					}
				}
				if (!chaseNPC)
				{
					for (int index = 0; index < Main.maxNPCs; index++)
					{
						NPC npc2 = Main.npc[index];
						if (npc2.CanBeChasedBy(base.Projectile))
						{
							float npcDist2 = Vector2.Distance(npc2.Center, base.Projectile.Center);
							if (!chaseNPC && npcDist2 < maxDistance)
							{
								npcPositionX = npc2.Center.X;
								chaseNPC = true;
							}
						}
					}
				}
				if (chaseNPC)
				{
					if (npcPositionX - base.Projectile.position.X > 0f)
					{
						float rightDist = Utils.SelectRandom<float>(Main.rand, 0.15f, 0.2f);
						base.Projectile.velocity.X += rightDist;
						if (base.Projectile.velocity.X > 8f)
						{
							base.Projectile.velocity.X = 8f;
						}
					}
					else
					{
						float leftDist = Utils.SelectRandom<float>(Main.rand, 0.15f, 0.2f);
						base.Projectile.velocity.X -= leftDist;
						if (base.Projectile.velocity.X < -8f)
						{
							base.Projectile.velocity.X = -8f;
						}
					}
				}
				else
				{
					if (playerDistance > 800f)
					{
						fly = true;
						base.Projectile.velocity.X = 0f;
						base.Projectile.velocity.Y = 0f;
						base.Projectile.tileCollide = false;
					}
					if (playerDistance > 200f)
					{
						if (player.position.X - base.Projectile.position.X > 0f)
						{
							float rightDist2 = Utils.SelectRandom<float>(Main.rand, 0.05f, 0.1f, 0.15f);
							base.Projectile.velocity.X += rightDist2;
							if (base.Projectile.velocity.X > 6f)
							{
								base.Projectile.velocity.X = 6f;
							}
						}
						else
						{
							float leftDist2 = Utils.SelectRandom<float>(Main.rand, 0.05f, 0.1f, 0.15f);
							base.Projectile.velocity.X -= leftDist2;
							if (base.Projectile.velocity.X < -6f)
							{
								base.Projectile.velocity.X = -6f;
							}
						}
					}
					if (playerDistance < 200f && base.Projectile.velocity.X != 0f)
					{
						if (base.Projectile.velocity.X > 0.5f)
						{
							float leftDist3 = Utils.SelectRandom<float>(Main.rand, 0.05f, 0.1f, 0.15f);
							base.Projectile.velocity.X -= leftDist3;
						}
						else if (base.Projectile.velocity.X < -0.5f)
						{
							float rightDist3 = Utils.SelectRandom<float>(Main.rand, 0.05f, 0.1f, 0.15f);
							base.Projectile.velocity.X += rightDist3;
						}
						else if (Math.Abs(base.Projectile.velocity.X) < 0.5f)
						{
							base.Projectile.velocity.X = 0f;
						}
					}
				}
			}
			else if (fly)
			{
				Vector2 playerVec2 = player.Center - base.Projectile.Center + new Vector2(0f, 0f);
				float num2 = ((Vector2)(ref playerVec2)).Length();
				((Vector2)(ref playerVec2)).Normalize();
				playerVec2 *= 14f;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + playerVec2) / 41f;
				base.Projectile.rotation = base.Projectile.velocity.X * 0.03f;
				if (num2 > 1500f)
				{
					base.Projectile.Center = player.Center;
					base.Projectile.netUpdate = true;
				}
				if (num2 < 100f)
				{
					if (player.velocity.Y == 0f)
					{
						playerStill++;
					}
					else
					{
						playerStill = 0;
					}
					if (playerStill > 30 && !Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
					{
						fly = false;
						base.Projectile.tileCollide = true;
						base.Projectile.rotation = 0f;
						base.Projectile.velocity.X *= 0.3f;
						base.Projectile.velocity.Y *= 0.3f;
					}
				}
			}
			if (base.Projectile.velocity.X > 0.25f)
			{
				base.Projectile.spriteDirection = -1;
			}
			else if (base.Projectile.velocity.X < -0.25f)
			{
				base.Projectile.spriteDirection = 1;
			}
		}
		if (base.Projectile.ai[0] != 1f)
		{
			return;
		}
		base.Projectile.rotation = 0f;
		base.Projectile.tileCollide = false;
		bool breakAway = false;
		bool spawnDust = false;
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] % 30f == 0f)
		{
			spawnDust = true;
		}
		int npcIndex = (int)base.Projectile.ai[1];
		NPC host = Main.npc[npcIndex];
		if (base.Projectile.localAI[0] >= 600000f)
		{
			breakAway = true;
		}
		else if (npcIndex < 0 || npcIndex >= Main.maxNPCs)
		{
			breakAway = true;
		}
		else if (host.active && !host.dontTakeDamage && host.defense < 9999)
		{
			base.Projectile.Center = host.Center - base.Projectile.velocity * 2f;
			base.Projectile.gfxOffY = host.gfxOffY;
			if (spawnDust)
			{
				host.HitEffect(0, 1.0);
			}
		}
		else
		{
			breakAway = true;
		}
		if (breakAway)
		{
			base.Projectile.ai[0] = 0f;
			base.Projectile.localAI[0] = 0f;
			base.Projectile.velocity.X = 0f;
			base.Projectile.velocity.Y = 0f;
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		Rectangle myRect = default(Rectangle);
		((Rectangle)(ref myRect))._002Ector((int)base.Projectile.position.X, (int)base.Projectile.position.Y, base.Projectile.width, base.Projectile.height);
		if (base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		for (int npcIndex = 0; npcIndex < Main.maxNPCs; npcIndex++)
		{
			NPC npc = Main.npc[npcIndex];
			if (!npc.active || npc.dontTakeDamage || npc.defense >= 9999 || !(npc.Calamity().DR < 0.99f) || ((!base.Projectile.friendly || (npc.friendly && (npc.type != 22 || base.Projectile.owner >= 255 || !player.killGuide) && (npc.type != 54 || base.Projectile.owner >= 255 || !player.killClothier))) && (!base.Projectile.hostile || !npc.friendly || npc.dontTakeDamageFromHostiles)) || (base.Projectile.owner >= 0 && npc.immune[base.Projectile.owner] != 0 && base.Projectile.maxPenetrate != 1) || (!npc.noTileCollide && base.Projectile.ownerHitCheck))
			{
				continue;
			}
			bool stickingToNPC;
			if (npc.type == 414)
			{
				Rectangle rect = npc.getRect();
				int num5 = 8;
				rect.X -= num5;
				rect.Y -= num5;
				rect.Width += num5 * 2;
				rect.Height += num5 * 2;
				stickingToNPC = base.Projectile.Colliding(myRect, rect);
			}
			else
			{
				stickingToNPC = base.Projectile.Colliding(myRect, npc.getRect());
			}
			if (!stickingToNPC)
			{
				continue;
			}
			if (npc.reflectsProjectiles && base.Projectile.CanBeReflected())
			{
				npc.ReflectProjectile(base.Projectile);
				break;
			}
			base.Projectile.ai[0] = 1f;
			base.Projectile.ai[1] = npcIndex;
			base.Projectile.velocity = (npc.Center - base.Projectile.Center) * 0.75f;
			base.Projectile.netUpdate = true;
			Point[] array2 = (Point[])(object)new Point[10];
			int projCount = 0;
			for (int projIndex = 0; projIndex < Main.maxProjectiles; projIndex++)
			{
				Projectile proj = Main.projectile[projIndex];
				if (projIndex != base.Projectile.whoAmI && proj.active && proj.owner == Main.myPlayer && proj.type == base.Projectile.type && proj.ai[0] == 1f && proj.ai[1] == (float)npcIndex)
				{
					array2[projCount++] = new Point(projIndex, proj.timeLeft);
					if (projCount >= array2.Length)
					{
						break;
					}
				}
			}
			if (projCount < array2.Length)
			{
				continue;
			}
			int num30 = 0;
			for (int m = 1; m < array2.Length; m++)
			{
				if (array2[m].Y < array2[num30].Y)
				{
					num30 = m;
				}
			}
			Main.projectile[array2[num30].X].Kill();
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
		{
			((Rectangle)(ref targetHitbox)).Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
		}
		return null;
	}

	public override bool MinionContactDamage()
	{
		return base.Projectile.ai[0] == 0f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.buffImmune[ModContent.BuffType<ShellfishClaps>()] = target.Calamity().DR >= 0.99f;
		target.AddBuff(ModContent.BuffType<ShellfishClaps>(), 600000);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}
}
