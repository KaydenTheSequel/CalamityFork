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

public class SightseerCollider : ModNPC
{
	public static Asset<Texture2D> glowmask;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 4;
		if (!Main.dedServ)
		{
			glowmask = ModContent.Request<Texture2D>("CalamityMod/NPCs/Astral/SightseerColliderGlow", (AssetRequestMode)2);
		}
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.PortraitPositionXOverride = 0f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 15f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.width = 48;
		base.NPC.height = 40;
		base.NPC.damage = 38;
		base.NPC.defense = 28;
		base.NPC.lifeMax = 320;
		base.NPC.DeathSound = CommonCalamitySounds.AstralNPCDeathSound;
		base.NPC.noGravity = true;
		base.NPC.knockBackResist = 0.58f;
		base.NPC.value = Item.buyPrice(0, 0, 5);
		base.NPC.aiStyle = -1;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<SightseerColliderBanner>();
		if (DownedBossSystem.downedAstrumAureus)
		{
			base.NPC.damage = 58;
			base.NPC.defense = 38;
			base.NPC.knockBackResist = 0.48f;
			base.NPC.lifeMax = 480;
		}
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AstralInfectionBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.SightseerCollider")
		});
	}

	public override void FindFrame(int frameHeight)
	{
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.frameCounter += 2.0;
		}
		else
		{
			base.NPC.frameCounter += 0.05f + ((Vector2)(ref base.NPC.velocity)).Length() * 0.667f;
		}
		if (base.NPC.frameCounter >= 8.0)
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y += frameHeight;
			if (base.NPC.frame.Y > base.NPC.height * 2)
			{
				base.NPC.frame.Y = 0;
			}
		}
		Dust d = CalamityGlobalNPC.SpawnDustOnNPC(base.NPC, 80, frameHeight, ModContent.DustType<AstralOrange>(), new Rectangle(16, 8, 6, 6), Vector2.Zero, 0.45f, useSpriteDirection: true);
		if (d != null)
		{
			d.customData = 0.04f;
		}
	}

	public override void AI()
	{
		CalamityGlobalNPC.DoFlyingAI(base.NPC, CalamityWorld.death ? 9.8f : (CalamityWorld.revenge ? 7.8f : 5.8f), CalamityWorld.death ? 0.05f : (CalamityWorld.revenge ? 0.04f : 0.03f), 350f);
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
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position + new Vector2(Main.rand.NextFloat(0f, base.NPC.width), Main.rand.NextFloat(0f, base.NPC.height)), base.NPC.velocity * rand, base.Mod.Find<ModGore>("SightseerColliderGore" + i).Type);
			}
		}
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		spriteBatch.Draw(glowmask.Value, base.NPC.Center - screenPos + new Vector2(0f, 4f), (Rectangle?)new Rectangle(0, base.NPC.frame.Y, 80, base.NPC.frame.Height), Color.White * 0.75f, base.NPC.rotation, new Vector2(40f, 20f), base.NPC.scale, (SpriteEffects)(base.NPC.spriteDirection == 1), 0f);
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
				return 0.2f;
			}
			return 0.16f;
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
		npcLoot.Add(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<StarblightSoot>(), 1, 1, 2, 1, 3));
	}
}
