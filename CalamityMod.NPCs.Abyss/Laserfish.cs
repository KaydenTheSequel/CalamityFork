using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.NPCs.NormalNPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.Abyss;

public class Laserfish : ModNPC
{
	public static Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		NPCID.Sets.NeedsExpertScaling[base.Type] = true;
		Main.npcFrameCount[base.Type] = 6;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.noGravity = true;
		base.NPC.damage = 0;
		base.NPC.width = 58;
		base.NPC.height = 32;
		base.NPC.defense = 20;
		base.NPC.lifeMax = 600;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.value = Item.buyPrice(0, 0, 10);
		base.NPC.HitSound = SoundID.NPCHit51;
		base.NPC.DeathSound = SoundID.NPCDeath26;
		base.NPC.knockBackResist = 0.65f;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<LaserfishBanner>();
		base.NPC.chaseable = false;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[2]
		{
			ModContent.GetInstance<AbyssLayer2Biome>().Type,
			ModContent.GetInstance<AbyssLayer3Biome>().Type
		};
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Laserfish")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.chaseable);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.chaseable = reader.ReadBoolean();
	}

	public override void AI()
	{
		CalamityRegularEnemyAI.PassiveSwimmingAI(base.NPC, base.Mod, 0, Main.player[base.NPC.target].Calamity().GetAbyssAggro(160f), 0.15f, 0.15f, 4f, 4f, 0.1f);
	}

	public override bool? CanBeHitByProjectile(Projectile projectile)
	{
		if (projectile.minion && !projectile.Calamity().overridesMinionDamagePrevention)
		{
			return base.NPC.chaseable;
		}
		return null;
	}

	public override void FindFrame(int frameHeight)
	{
		if (!base.NPC.wet && !base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.frameCounter = 0.0;
			return;
		}
		base.NPC.frameCounter += (base.NPC.chaseable ? 0.15f : 0.075f);
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		if (!base.NPC.IsABestiaryIconDummy)
		{
			SpriteEffects effects = (SpriteEffects)(base.NPC.direction != -1);
			Main.EntitySpriteDraw(GlowTexture.Value, base.NPC.Center - Main.screenPosition + new Vector2(0f, base.NPC.gfxOffY + 4f), base.NPC.frame, Color.White * 0.5f, base.NPC.rotation, base.NPC.frame.Size() / 2f, base.NPC.scale, effects);
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.Player.Calamity().ZoneAbyssLayer2 && spawnInfo.Water)
		{
			return SpawnCondition.CaveJellyfish.Chance * 0.6f;
		}
		if (spawnInfo.Player.Calamity().ZoneAbyssLayer3 && spawnInfo.Water)
		{
			if (!Main.remixWorld)
			{
				return SpawnCondition.CaveJellyfish.Chance * 1.2f;
			}
			return 10.8f;
		}
		return 0f;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		DropHelper.NormalVsExpertQuantity(ModContent.ItemType<MysteriousCircuitry>(), 2, 1, 2, 2, 3);
		npcLoot.DefineConditionalDropSet(DropHelper.PostLevi()).Add(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<DepthCells>(), 2, 1, 2, 2, 3));
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 25; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Laserfish").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Laserfish2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("Laserfish3").Type);
			}
		}
	}
}
