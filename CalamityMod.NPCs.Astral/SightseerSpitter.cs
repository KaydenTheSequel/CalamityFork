using System;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Sounds;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Astral;

public class SightseerSpitter : ModNPC
{
	public static Asset<Texture2D> glowmask;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 4;
		if (!Main.dedServ)
		{
			glowmask = ModContent.Request<Texture2D>("CalamityMod/NPCs/Astral/SightseerSpitterGlow", (AssetRequestMode)2);
		}
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.7f;
		nPCBestiaryDrawModifiers.Velocity = 2f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 0f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 15f;
		value.Position.Y -= 10f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.width = 64;
		base.NPC.height = 56;
		base.NPC.damage = 50;
		base.NPC.defense = 35;
		base.NPC.lifeMax = 500;
		base.NPC.DeathSound = CommonCalamitySounds.AstralNPCDeathSound;
		base.NPC.noGravity = true;
		base.NPC.knockBackResist = 0.8f;
		base.NPC.value = Item.buyPrice(0, 0, 8);
		base.NPC.aiStyle = -1;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<SightseerSpitterBanner>();
		if (DownedBossSystem.downedAstrumAureus)
		{
			base.NPC.damage = 85;
			base.NPC.defense = 45;
			base.NPC.knockBackResist = 0.7f;
			base.NPC.lifeMax = 750;
		}
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AstralInfectionBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.SightseerSpitter")
		});
	}

	public override void FindFrame(int frameHeight)
	{
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.frameCounter += 0.05f + ((Vector2)(ref base.NPC.velocity)).Length() * 0.667f;
		if (base.NPC.frameCounter >= 8.0)
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y += frameHeight;
			if (base.NPC.frame.Y > base.NPC.height * 3)
			{
				base.NPC.frame.Y = 0;
			}
		}
		Dust d = CalamityGlobalNPC.SpawnDustOnNPC(base.NPC, 118, frameHeight, ModContent.DustType<AstralOrange>(), new Rectangle(70, 18, 48, 18), Vector2.Zero, 0.45f, useSpriteDirection: true);
		if (d != null)
		{
			d.customData = 0.04f;
		}
	}

	public override void AI()
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC.DoFlyingAI(base.NPC, 4f, 0.025f, 300f);
		base.NPC.ai[1]++;
		Player target = Main.player[base.NPC.target];
		if (base.NPC.justHit || target.dead)
		{
			base.NPC.ai[1] = 0f;
		}
		if (!Collision.CanHit(target.position, target.width, target.height, base.NPC.position, base.NPC.width, base.NPC.height))
		{
			return;
		}
		Vector2 vector = target.Center - base.NPC.Center;
		((Vector2)(ref vector)).Normalize();
		Vector2 spawnPoint = base.NPC.Center + vector * 42f;
		if (base.NPC.ai[1] >= (CalamityWorld.death ? 80f : (CalamityWorld.revenge ? 120f : 160f)))
		{
			base.NPC.ai[1] = 0f;
			if (Main.netMode != 1)
			{
				int n = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)spawnPoint.X, (int)spawnPoint.Y, ModContent.NPCType<AstralSeekerSpit>());
				Main.npc[n].Center = spawnPoint;
				Main.npc[n].velocity = vector * (CalamityWorld.death ? 12f : (CalamityWorld.revenge ? 11f : 10f));
			}
		}
		else if (base.NPC.ai[1] >= 140f)
		{
			int dustType = (Main.rand.NextBool() ? ModContent.DustType<AstralOrange>() : ModContent.DustType<AstralBlue>());
			int d = Dust.NewDust(spawnPoint - new Vector2(5f), 10, 10, dustType);
			Main.dust[d].velocity = base.NPC.velocity * 0.3f;
			Main.dust[d].customData = true;
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.soundDelay == 0)
		{
			base.NPC.soundDelay = 15;
			SoundEngine.PlaySound(in CommonCalamitySounds.AstralNPCHitSound, base.NPC.Center);
		}
		CalamityGlobalNPC.DoHitDust(base.NPC, hit.HitDirection, (Main.rand.Next(0, Math.Max(0, base.NPC.life)) == 0) ? 5 : ModContent.DustType<AstralEnemy>(), 1f, 4, 22);
		if (base.NPC.life <= 0 && !Main.dedServ)
		{
			for (int i = 0; i < 5; i++)
			{
				float rand = Main.rand.NextFloat(-0.18f, 0.18f);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position + new Vector2(Main.rand.NextFloat(0f, base.NPC.width), Main.rand.NextFloat(0f, base.NPC.height)), base.NPC.velocity * rand, base.Mod.Find<ModGore>("SightseerSpitterGore" + i).Type);
			}
		}
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		if (!base.NPC.IsABestiaryIconDummy)
		{
			spriteBatch.Draw(glowmask.Value, base.NPC.Center - screenPos + new Vector2(0f, 4f), (Rectangle?)base.NPC.frame, Color.White * 0.75f, base.NPC.rotation, new Vector2(59f, 28f), base.NPC.scale, (SpriteEffects)(base.NPC.spriteDirection == 1), 0f);
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (CalamityGlobalNPC.AnyEvents(spawnInfo.Player))
		{
			return 0f;
		}
		if (spawnInfo.Player.InAstral(1))
		{
			if (!spawnInfo.Player.ZoneDesert)
			{
				return 0.17f;
			}
			return 0.14f;
		}
		return 0f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180);
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<StarblightSoot>(), 1, 2, 3, 3, 4));
	}
}
