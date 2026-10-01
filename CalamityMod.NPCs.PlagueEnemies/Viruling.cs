using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Sounds;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.PlagueEnemies;

public class Viruling : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 5;
	}

	public override void SetDefaults()
	{
		base.NPC.noGravity = true;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.damage = 60;
		base.NPC.width = 58;
		base.NPC.height = 44;
		base.NPC.defense = 32;
		base.NPC.lifeMax = 900;
		base.NPC.knockBackResist = 0.3f;
		base.NPC.value = Item.buyPrice(0, 0, 10);
		base.NPC.HitSound = SoundID.NPCHit22;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<VirulingBanner>();
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = false;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Viruling")
		});
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += 0.15000000596046448;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override void AI()
	{
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0622: Unknown result type (might be due to invalid IL or missing references)
		//IL_066f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0675: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead)
		{
			base.NPC.TargetClosest();
		}
		float maxSpeed = 6f;
		float acceleration = 0.05f;
		if (CalamityWorld.revenge)
		{
			maxSpeed *= 1.25f;
			acceleration *= 1.25f;
		}
		if (CalamityWorld.death)
		{
			maxSpeed *= 1.25f;
			acceleration *= 1.25f;
		}
		Vector2 vector = default(Vector2);
		((Vector2)(ref vector))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
		float targetXDist = Main.player[base.NPC.target].position.X + (float)(Main.player[base.NPC.target].width / 2);
		float targetYDist = Main.player[base.NPC.target].position.Y + (float)(Main.player[base.NPC.target].height / 2);
		targetXDist = (int)(targetXDist / 8f) * 8;
		targetYDist = (int)(targetYDist / 8f) * 8;
		vector.X = (int)(vector.X / 8f) * 8;
		vector.Y = (int)(vector.Y / 8f) * 8;
		targetXDist -= vector.X;
		targetYDist -= vector.Y;
		float targetDistance = (float)Math.Sqrt(targetXDist * targetXDist + targetYDist * targetYDist);
		if (targetDistance == 0f)
		{
			targetXDist = base.NPC.velocity.X;
			targetYDist = base.NPC.velocity.Y;
		}
		else
		{
			targetDistance = maxSpeed / targetDistance;
			targetXDist *= targetDistance;
			targetYDist *= targetDistance;
		}
		if (Main.player[base.NPC.target].dead)
		{
			targetXDist = (float)base.NPC.direction * maxSpeed / 2f;
			targetYDist = (0f - maxSpeed) / 2f;
		}
		if (base.NPC.velocity.X < targetXDist)
		{
			base.NPC.velocity.X = base.NPC.velocity.X + acceleration;
		}
		else if (base.NPC.velocity.X > targetXDist)
		{
			base.NPC.velocity.X = base.NPC.velocity.X - acceleration;
		}
		if (base.NPC.velocity.Y < targetYDist)
		{
			base.NPC.velocity.Y = base.NPC.velocity.Y + acceleration;
		}
		else if (base.NPC.velocity.Y > targetYDist)
		{
			base.NPC.velocity.Y = base.NPC.velocity.Y - acceleration;
		}
		if (targetXDist > 0f)
		{
			base.NPC.spriteDirection = 1;
			base.NPC.rotation = (float)Math.Atan2(targetYDist, targetXDist);
		}
		else if (targetXDist < 0f)
		{
			base.NPC.spriteDirection = -1;
			base.NPC.rotation = (float)Math.Atan2(targetYDist, targetXDist) + 3.14f;
		}
		float recoilSpeed = 0.7f;
		if (base.NPC.collideX)
		{
			base.NPC.netUpdate = true;
			base.NPC.velocity.X = base.NPC.oldVelocity.X * (0f - recoilSpeed);
			if (base.NPC.direction == -1 && base.NPC.velocity.X > 0f && base.NPC.velocity.X < 2f)
			{
				base.NPC.velocity.X = 2f;
			}
			if (base.NPC.direction == 1 && base.NPC.velocity.X < 0f && base.NPC.velocity.X > -2f)
			{
				base.NPC.velocity.X = -2f;
			}
		}
		if (base.NPC.collideY)
		{
			base.NPC.netUpdate = true;
			base.NPC.velocity.Y = base.NPC.oldVelocity.Y * (0f - recoilSpeed);
			if (base.NPC.velocity.Y > 0f && (double)base.NPC.velocity.Y < 1.5)
			{
				base.NPC.velocity.Y = 2f;
			}
			if (base.NPC.velocity.Y < 0f && (double)base.NPC.velocity.Y > -1.5)
			{
				base.NPC.velocity.Y = -2f;
			}
		}
		if (((base.NPC.velocity.X > 0f && base.NPC.oldVelocity.X < 0f) || (base.NPC.velocity.X < 0f && base.NPC.oldVelocity.X > 0f) || (base.NPC.velocity.Y > 0f && base.NPC.oldVelocity.Y < 0f) || (base.NPC.velocity.Y < 0f && base.NPC.oldVelocity.Y > 0f)) && !base.NPC.justHit)
		{
			base.NPC.netUpdate = true;
		}
		int idleDust = Dust.NewDust(new Vector2(base.NPC.position.X - base.NPC.velocity.X, base.NPC.position.Y - base.NPC.velocity.Y), base.NPC.width, base.NPC.height, 46, base.NPC.velocity.X * 0.2f, base.NPC.velocity.Y * 0.2f, 100, default(Color), 2f);
		Dust obj = Main.dust[idleDust];
		obj.noGravity = true;
		obj.velocity.X *= 0.3f;
		obj.velocity.Y *= 0.3f;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.PlayerSafe || !NPC.downedGolemBoss || spawnInfo.Player.Calamity().ZoneSunkenSea)
		{
			return 0f;
		}
		return SpawnCondition.HardmodeJungle.Chance * 0.09f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Plague>(), 180);
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 46, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			SoundEngine.PlaySound(in CommonCalamitySounds.PlagueBoomSound, base.NPC.Center);
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 46, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Viruling").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Viruling2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Viruling3").Type);
			}
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<PlagueCellCanister>(), 1, 1, 2);
	}
}
