using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Events;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Ravager;

public class RockPillar : ModNPC
{
	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/NPCHit/RavagerRockPillarHit", 3);

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
		NPCID.Sets.ImmuneToAllBuffs[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 90;
		base.NPC.width = 60;
		base.NPC.height = 300;
		base.NPC.defense = 50;
		base.NPC.DR_NERD(0.3f);
		base.NPC.chaseable = false;
		base.NPC.alpha = 255;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.HitSound = RavagerBody.PillarSound;
		base.NPC.DeathSound = SoundID.NPCDeath14;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToWater = true;
		NPCID.Sets.ImmuneToAllBuffs[base.Type] = true;
		base.NPC.lifeMax = 1800;
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		if (base.NPC.velocity.Y == 0f || base.NPC.ai[0] == 0f)
		{
			return false;
		}
		return base.CanHitPlayer(target, ref cooldownSlot);
	}

	public override void AI()
	{
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.lifeMax > 1800)
		{
			base.NPC.lifeMax = 1800;
		}
		if (base.NPC.life > base.NPC.lifeMax)
		{
			base.NPC.life = base.NPC.lifeMax;
		}
		if (CalamityGlobalNPC.scavenger < 0 || !Main.npc[CalamityGlobalNPC.scavenger].active)
		{
			if (Main.netMode != 1)
			{
				base.NPC.StrikeInstantKill();
			}
			return;
		}
		if (base.NPC.timeLeft < 1800)
		{
			base.NPC.timeLeft = 1800;
		}
		if (base.NPC.alpha > 0)
		{
			base.NPC.damage = 0;
			base.NPC.alpha -= 10;
			if (base.NPC.alpha < 0)
			{
				base.NPC.alpha = 0;
			}
		}
		if (base.NPC.ai[0] == 0f)
		{
			if (base.NPC.velocity.Y != 0f || !(base.NPC.ai[1] >= 2f))
			{
				return;
			}
			SoundEngine.PlaySound(in SoundID.Item62, base.NPC.Center);
			for (int i = 0; i < 10; i++)
			{
				int rockDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 8, 0f, 0f, 100, default(Color), 2f);
				Dust obj = Main.dust[rockDust];
				obj.velocity *= 3f;
				if (Main.rand.NextBool())
				{
					Main.dust[rockDust].scale = 0.5f;
					Main.dust[rockDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			for (int j = 0; j < 10; j++)
			{
				int rockDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 1, 0f, 0f, 100, default(Color), 3f);
				Main.dust[rockDust2].noGravity = true;
				Dust obj2 = Main.dust[rockDust2];
				obj2.velocity *= 5f;
				rockDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 8, 0f, 0f, 100, default(Color), 2f);
				Dust obj3 = Main.dust[rockDust2];
				obj3.velocity *= 2f;
			}
			base.NPC.noTileCollide = true;
			if (base.NPC.rotation == 0f)
			{
				base.NPC.velocity.X = 12 * base.NPC.direction;
			}
			base.NPC.velocity.Y = -28.5f;
			base.NPC.ai[0] = 1f;
			base.NPC.ai[1] = 0f;
			base.NPC.damage = base.NPC.defDamage;
			if (DownedBossSystem.downedProvidence && !BossRushEvent.BossRushActive)
			{
				base.NPC.damage = (int)((double)base.NPC.defDamage * 1.5);
			}
		}
		else if (base.NPC.velocity.Y == 0f || Vector2.Distance(base.NPC.Center, Main.npc[CalamityGlobalNPC.scavenger].Center) > 2800f)
		{
			SoundEngine.PlaySound(in SoundID.Item14, base.NPC.Center);
			base.NPC.ai[0] = 0f;
			if (Main.netMode != 1)
			{
				base.NPC.StrikeInstantKill();
			}
		}
		else
		{
			base.NPC.velocity.Y += 0.2f;
			if (base.NPC.velocity.Y >= 0f && !Collision.SolidCollision(base.NPC.position, base.NPC.width, base.NPC.height))
			{
				base.NPC.noTileCollide = false;
			}
		}
	}

	public override bool? CanFallThroughPlatforms()
	{
		return base.NPC.ai[0] != 0f || base.NPC.alpha > 10 || (base.NPC.target >= 0 && Main.player[base.NPC.target].position.Y > base.NPC.position.Y + (float)base.NPC.height);
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<ArmorCrunch>(), 240);
			SoundEngine.PlaySound(in SoundID.Item14, base.NPC.Center);
			base.NPC.ai[0] = 0f;
			if (Main.netMode != 1)
			{
				base.NPC.StrikeInstantKill();
			}
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0548: Unknown result type (might be due to invalid IL or missing references)
		//IL_054e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0602: Unknown result type (might be due to invalid IL or missing references)
		//IL_0608: Unknown result type (might be due to invalid IL or missing references)
		//IL_062c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0636: Unknown result type (might be due to invalid IL or missing references)
		//IL_063b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0646: Unknown result type (might be due to invalid IL or missing references)
		//IL_0670: Unknown result type (might be due to invalid IL or missing references)
		//IL_0676: Unknown result type (might be due to invalid IL or missing references)
		//IL_068c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0696: Unknown result type (might be due to invalid IL or missing references)
		//IL_069b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.life <= 0)
		{
			base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
			base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
			base.NPC.width = 80;
			base.NPC.height = 360;
			base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
			base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
			for (int i = 0; i < 30; i++)
			{
				int rockDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 8, 0f, 0f, 100, default(Color), 2f);
				Dust obj = Main.dust[rockDust];
				obj.velocity *= 3f;
				if (Main.rand.NextBool())
				{
					Main.dust[rockDust].scale = 0.5f;
					Main.dust[rockDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			for (int j = 0; j < 30; j++)
			{
				int rockDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 1, 0f, 0f, 100, default(Color), 3f);
				Main.dust[rockDust2].noGravity = true;
				Dust obj2 = Main.dust[rockDust2];
				obj2.velocity *= 5f;
				rockDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 8, 0f, 0f, 100, default(Color), 2f);
				Dust obj3 = Main.dust[rockDust2];
				obj3.velocity *= 2f;
			}
			if (!Main.dedServ)
			{
				float y = (float)base.NPC.height / 6f;
				float randomVelocityScale = 0.25f;
				for (int k = 0; k < 2; k++)
				{
					Vector2 randomVelocity = base.NPC.velocity * Main.rand.NextFloat() * randomVelocityScale;
					Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity + randomVelocity, base.Mod.Find<ModGore>("RockPillar").Type);
					Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position + Vector2.UnitY * y, base.NPC.velocity + randomVelocity, base.Mod.Find<ModGore>("RockPillar2").Type);
					Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position + Vector2.UnitY * y * 2f, base.NPC.velocity + randomVelocity, base.Mod.Find<ModGore>("RockPillar3").Type);
					Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position + Vector2.UnitY * y * 3f, base.NPC.velocity + randomVelocity, base.Mod.Find<ModGore>("RockPillar4").Type);
					Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position + Vector2.UnitY * y * 4f, base.NPC.velocity + randomVelocity, base.Mod.Find<ModGore>("RockPillar5").Type);
					Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position + Vector2.UnitY * y * 5f, base.NPC.velocity + randomVelocity, base.Mod.Find<ModGore>("RockPillar6").Type);
				}
			}
			return;
		}
		for (int l = 0; l < 2; l++)
		{
			int rockDust3 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 8, 0f, 0f, 100, default(Color), 2f);
			Dust obj4 = Main.dust[rockDust3];
			obj4.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[rockDust3].scale = 0.5f;
				Main.dust[rockDust3].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int m = 0; m < 2; m++)
		{
			int rockDust4 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 1, 0f, 0f, 100, default(Color), 3f);
			Main.dust[rockDust4].noGravity = true;
			Dust obj5 = Main.dust[rockDust4];
			obj5.velocity *= 5f;
			rockDust4 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 8, 0f, 0f, 100, default(Color), 2f);
			Dust obj6 = Main.dust[rockDust4];
			obj6.velocity *= 2f;
		}
	}

	public override void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
	{
		if (item.pick > 0)
		{
			modifiers.FlatBonusDamage += -10000f;
			modifiers.FinalDamage.Flat += item.pick - 1;
			modifiers.SetCrit();
		}
		else
		{
			modifiers.SetMaxDamage(1);
			modifiers.DisableCrit();
			modifiers.HideCombatText();
		}
		base.ModifyHitByItem(player, item, ref modifiers);
	}

	public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
	{
		Item item = Main.player[projectile.owner].HeldItem;
		if (item.pick > 0 && projectile.CountsAsClass<MeleeDamageClass>())
		{
			modifiers.FlatBonusDamage += -10000f;
			modifiers.FinalDamage.Flat += item.pick - 1;
			modifiers.SetCrit();
		}
		else
		{
			modifiers.SetMaxDamage(1);
			modifiers.DisableCrit();
			modifiers.HideCombatText();
		}
		base.ModifyHitByProjectile(projectile, ref modifiers);
	}
}
