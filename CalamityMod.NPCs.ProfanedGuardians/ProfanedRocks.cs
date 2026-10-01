using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Events;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.ProfanedGuardians;

[HasPierceResist(false)]
public class ProfanedRocks : ModNPC
{
	private bool start = true;

	private const double MinDistance = 200.0;

	private double distance = 200.0;

	private const double MinMaxDistance = 300.0;

	public const int MaxHP = 8000;

	public const int MaxBossRushHP = 20000;

	public static Asset<Texture2D>[] Textures = new Asset<Texture2D>[6];

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
		NPCID.Sets.TrailingMode[base.Type] = 1;
		if (!Main.dedServ)
		{
			for (int i = 0; i < 6; i++)
			{
				Textures[i] = ModContent.Request<Texture2D>(Texture + (i + 1), (AssetRequestMode)2);
			}
		}
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 100;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.dontTakeDamage = true;
		base.NPC.width = 50;
		base.NPC.height = 50;
		base.NPC.defense = 100;
		base.NPC.lifeMax = (BossRushEvent.BossRushActive ? 20000 : 8000);
		base.NPC.knockBackResist = 0f;
		base.NPC.Opacity = 0f;
		base.NPC.noGravity = true;
		base.NPC.chaseable = false;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = SoundID.NPCHit52;
		base.NPC.DeathSound = SoundID.NPCDeath55;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToWater = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(start);
		writer.Write(distance);
		writer.Write(base.NPC.dontTakeDamage);
		writer.Write(base.NPC.noGravity);
		writer.Write(base.NPC.Opacity);
		for (int i = 0; i < 4; i++)
		{
			writer.Write(base.NPC.Calamity().newAI[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		start = reader.ReadBoolean();
		distance = reader.ReadDouble();
		base.NPC.dontTakeDamage = reader.ReadBoolean();
		base.NPC.noGravity = reader.ReadBoolean();
		base.NPC.Opacity = reader.ReadSingle();
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
	}

	public override void AI()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a92: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0603: Unknown result type (might be due to invalid IL or missing references)
		//IL_061c: Unknown result type (might be due to invalid IL or missing references)
		//IL_062a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0631: Unknown result type (might be due to invalid IL or missing references)
		//IL_0636: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0528: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_079d: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ac: Unknown result type (might be due to invalid IL or missing references)
		if (start)
		{
			for (int k = 0; k < 15; k++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 244);
			}
			start = false;
			base.NPC.ai[3] = base.NPC.ai[0];
		}
		if (CalamityGlobalNPC.doughnutBossDefender < 0 || !Main.npc[CalamityGlobalNPC.doughnutBossDefender].active || CalamityGlobalNPC.doughnutBoss < 0 || !Main.npc[CalamityGlobalNPC.doughnutBoss].active)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.active = false;
			base.NPC.netUpdate = true;
			return;
		}
		if (Main.npc[CalamityGlobalNPC.doughnutBossDefender].localAI[3] == 1f)
		{
			base.NPC.dontTakeDamage = true;
			base.NPC.Opacity -= 0.01f;
			if (base.NPC.Opacity < 0f)
			{
				base.NPC.Opacity = 0f;
			}
			base.NPC.scale = MathHelper.Lerp(0.05f, 1f, base.NPC.Opacity);
		}
		else if (base.NPC.Opacity < 1f)
		{
			base.NPC.dontTakeDamage = true;
			base.NPC.Opacity += 0.01f;
			if (base.NPC.Opacity > 1f)
			{
				base.NPC.Opacity = 1f;
			}
			base.NPC.scale = MathHelper.Lerp(0.05f, 1f, base.NPC.Opacity);
		}
		else
		{
			base.NPC.dontTakeDamage = false;
		}
		Lighting.AddLight((int)(base.NPC.Center.X / 16f), (int)(base.NPC.Center.Y / 16f), 0.7f * base.NPC.Opacity, 0.55f * base.NPC.Opacity, 0f);
		if (base.NPC.timeLeft < 1800)
		{
			base.NPC.timeLeft = 1800;
		}
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		if ((Main.npc[CalamityGlobalNPC.doughnutBossDefender].ai[0] != 0f && Main.npc[CalamityGlobalNPC.doughnutBossDefender].ai[1] >= -60f) || base.NPC.Calamity().newAI[0] < 0f)
		{
			if (base.NPC.Calamity().newAI[0] > -1f)
			{
				base.NPC.Calamity().newAI[0] = -1f;
			}
			Player player = Main.player[Main.npc[CalamityGlobalNPC.doughnutBoss].target];
			float chargeSpeed = (death ? 18f : (revenge ? 17f : (expertMode ? 16f : 14f)));
			float fallDownGateValue = 4800f / chargeSpeed;
			if (base.NPC.Calamity().newAI[0] == -3f)
			{
				base.NPC.damage = base.NPC.defDamage;
				Vector2 finalVelocity = default(Vector2);
				((Vector2)(ref finalVelocity))._002Ector(base.NPC.Calamity().newAI[2], base.NPC.Calamity().newAI[3]);
				if (((Vector2)(ref base.NPC.velocity)).Length() < ((Vector2)(ref finalVelocity)).Length())
				{
					NPC nPC = base.NPC;
					nPC.velocity *= 1.05f;
					if (((Vector2)(ref base.NPC.velocity)).Length() > ((Vector2)(ref finalVelocity)).Length())
					{
						((Vector2)(ref base.NPC.velocity)).Normalize();
						NPC nPC2 = base.NPC;
						nPC2.velocity *= ((Vector2)(ref finalVelocity)).Length();
					}
				}
				base.NPC.rotation += 0.25f;
				base.NPC.Calamity().newAI[1]++;
				if (!(base.NPC.Calamity().newAI[1] >= fallDownGateValue))
				{
					return;
				}
				base.NPC.noGravity = false;
				base.NPC.velocity.Y += 0.1f;
				if (Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
				{
					if (base.NPC.DeathSound.HasValue)
					{
						SoundEngine.PlaySound(base.NPC.DeathSound.GetValueOrDefault(), base.NPC.Center);
					}
					base.NPC.life = 0;
					base.NPC.HitEffect();
					base.NPC.active = false;
					base.NPC.netUpdate = true;
				}
				return;
			}
			if (base.NPC.Calamity().newAI[0] == -2f)
			{
				base.NPC.damage = base.NPC.defDamage;
				Vector2 finalVelocity2 = base.NPC.SafeDirectionTo(player.Center, -Vector2.UnitY) * chargeSpeed;
				if (Main.getGoodWorld)
				{
					finalVelocity2 *= Main.rand.NextFloat(1f, 1.7f);
				}
				base.NPC.Calamity().newAI[2] = finalVelocity2.X;
				base.NPC.Calamity().newAI[3] = finalVelocity2.Y;
				base.NPC.velocity = finalVelocity2 * 0.1f;
				base.NPC.rotation += 0.25f;
				base.NPC.Calamity().newAI[0] = -3f;
				base.NPC.netUpdate = true;
				return;
			}
			base.NPC.damage = 0;
			if (death)
			{
				float pushVelocity = 0.15f;
				ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					NPC n = enumerator.Current;
					if (n.whoAmI == base.NPC.whoAmI || n.type != base.NPC.type)
					{
						continue;
					}
					if (Vector2.Distance(base.NPC.Center, n.Center) < 160f)
					{
						if (base.NPC.position.X < n.position.X)
						{
							base.NPC.velocity.X -= pushVelocity;
						}
						else
						{
							base.NPC.velocity.X += pushVelocity;
						}
						if (base.NPC.position.Y < n.position.Y)
						{
							base.NPC.velocity.Y -= pushVelocity;
						}
						else
						{
							base.NPC.velocity.Y += pushVelocity;
						}
					}
					else
					{
						NPC nPC3 = base.NPC;
						nPC3.velocity *= 0.95f;
					}
				}
			}
			else
			{
				NPC nPC4 = base.NPC;
				nPC4.velocity *= 0.95f;
			}
			base.NPC.Calamity().newAI[1]++;
			float chargeGateValue = (death ? 80f : (revenge ? 90f : (expertMode ? 100f : 120f)));
			chargeGateValue += chargeGateValue * 0.5f * base.NPC.ai[1];
			float anglularSpeed = base.NPC.Calamity().newAI[1] / chargeGateValue;
			anglularSpeed = 0.05f + anglularSpeed * 0.2f;
			base.NPC.rotation += anglularSpeed;
			if (base.NPC.Calamity().newAI[1] >= chargeGateValue)
			{
				base.NPC.netUpdate = true;
				base.NPC.Calamity().newAI[0] = -2f;
				base.NPC.Calamity().newAI[1] = 0f;
			}
			return;
		}
		base.NPC.damage = 0;
		double maxDistance = (death ? 340.0 : (revenge ? 330.0 : (expertMode ? 320.0 : 300.0)));
		double rateOfChangeIncrease = maxDistance / 300.0 - 1.0;
		double rateOfChange = (double)(base.NPC.ai[1] * 0.5f) + 2.0 + rateOfChangeIncrease;
		if (base.NPC.Calamity().newAI[0] == 0f)
		{
			distance += rateOfChange;
			if (distance >= maxDistance)
			{
				distance = maxDistance;
				base.NPC.Calamity().newAI[0] = 1f;
			}
		}
		else
		{
			distance -= rateOfChange;
			if (distance <= 200.0)
			{
				distance = 200.0;
				base.NPC.Calamity().newAI[0] = 0f;
			}
		}
		float minRotationVelocity = 0.5f;
		float rotationVelocityIncrease = (death ? 0.2f : (revenge ? 0.15f : (expertMode ? 0.1f : 0f)));
		rotationVelocityIncrease += rotationVelocityIncrease * (base.NPC.ai[1] * 0.5f);
		NPC parent = Main.npc[NPC.FindFirstNPC(ModContent.NPCType<ProfanedGuardianDefender>())];
		double radians = (double)base.NPC.ai[3] * (Math.PI / 180.0);
		base.NPC.position.X = parent.Center.X - (float)(int)(Math.Cos(radians) * distance) - (float)(base.NPC.width / 2);
		base.NPC.position.Y = parent.Center.Y - (float)(int)(Math.Sin(radians) * distance) - (float)(base.NPC.height / 2);
		base.NPC.rotation = (float)radians;
		base.NPC.ai[3] += minRotationVelocity + rotationVelocityIncrease;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		int npcType = (int)MathHelper.Clamp(base.NPC.ai[2], 1f, 6f);
		Texture2D texture = Textures[npcType - 1].Value;
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector((float)(texture.Width / 2), (float)(texture.Height / 2));
		Vector2 drawPos = base.NPC.Center - screenPos;
		drawPos -= new Vector2((float)texture.Width, (float)texture.Height) * base.NPC.scale / 2f;
		drawPos += drawOrigin * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(0, 0, texture.Width, texture.Height);
		if (!base.NPC.dontTakeDamage)
		{
			base.NPC.DrawBackglow(Color.Orange.MultiplyRGBA(new Color(255, 255, 255, 0)), 4f, (SpriteEffects)0, frame, screenPos, texture);
		}
		spriteBatch.Draw(texture, drawPos, (Rectangle?)frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, drawOrigin, base.NPC.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.5f * balance);
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<HolyFlames>(), 120);
		}
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		Rectangle targetHitbox = target.Hitbox;
		float num = Vector2.Distance(base.NPC.Center, targetHitbox.TopLeft());
		float hitboxTopRight = Vector2.Distance(base.NPC.Center, targetHitbox.TopRight());
		float hitboxBotLeft = Vector2.Distance(base.NPC.Center, targetHitbox.BottomLeft());
		float hitboxBotRight = Vector2.Distance(base.NPC.Center, targetHitbox.BottomRight());
		float minDist = num;
		if (hitboxTopRight < minDist)
		{
			minDist = hitboxTopRight;
		}
		if (hitboxBotLeft < minDist)
		{
			minDist = hitboxBotLeft;
		}
		if (hitboxBotRight < minDist)
		{
			minDist = hitboxBotRight;
		}
		return minDist <= ((base.NPC.ai[2] == 6f) ? 16f : 22f);
	}

	public override bool CheckDead()
	{
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 244, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			if (!Main.dedServ)
			{
				int npcType = (int)base.NPC.ai[2];
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("ProfanedRocksGore" + npcType).Type, base.NPC.scale);
			}
			for (int i = 0; i < 30; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 244, hit.HitDirection, -1f);
			}
		}
	}
}
