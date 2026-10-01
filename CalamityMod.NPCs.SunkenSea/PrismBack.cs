using System;
using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.SunkenSea;

public class PrismBack : ModNPC
{
	public static Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.NPC.type] = 5;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.PortraitPositionXOverride = 0f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 15f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.noGravity = true;
		base.NPC.damage = (Main.hardMode ? 40 : 20);
		base.NPC.width = 72;
		base.NPC.height = 58;
		base.NPC.defense = (Main.hardMode ? 25 : 15);
		base.NPC.lifeMax = (Main.hardMode ? 1000 : 500);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.value = Item.buyPrice(0, 0, 2);
		base.NPC.HitSound = SoundID.NPCHit24;
		base.NPC.DeathSound = SoundID.NPCDeath27;
		base.NPC.knockBackResist = 0.15f;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<PrismBackBanner>();
		base.NPC.chaseable = false;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<SunkenSeaBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.PrismBack")
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
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.Center.Y + 10f > Main.player[base.NPC.target].Center.Y)
		{
			if (CalamityWorld.death)
			{
				base.NPC.damage = base.NPC.defDamage * 3;
			}
			else if (CalamityWorld.revenge)
			{
				base.NPC.damage = (int)Math.Round((double)base.NPC.defDamage * 2.75);
			}
			else if (Main.expertMode)
			{
				base.NPC.damage = (int)Math.Round((double)base.NPC.defDamage * 2.5);
			}
			else
			{
				base.NPC.damage = (int)Math.Round((double)base.NPC.defDamage * 1.25);
			}
		}
		else if (CalamityWorld.death)
		{
			base.NPC.damage = (int)Math.Round((double)base.NPC.defDamage * 2.5);
		}
		else if (CalamityWorld.revenge)
		{
			base.NPC.damage = (int)Math.Round((double)base.NPC.defDamage * 2.25);
		}
		else if (Main.expertMode)
		{
			base.NPC.damage = base.NPC.defDamage * 2;
		}
		else
		{
			base.NPC.damage = base.NPC.defDamage;
		}
		Lighting.AddLight(base.NPC.Center, (float)(255 - base.NPC.alpha) * 0f / 255f, (float)(255 - base.NPC.alpha) * 0.75f / 255f, (float)(255 - base.NPC.alpha) * 0.75f / 255f);
		CalamityRegularEnemyAI.PassiveSwimmingAI(base.NPC, base.Mod, 2, 0f, 0f, 0f, 0f, 0f, 0.1f);
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += ((base.NPC.wet || base.NPC.IsABestiaryIconDummy) ? 0.1f : 0f);
		base.NPC.frameCounter %= Main.npcFrameCount[base.NPC.type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Vector2 val = new Vector2(base.NPC.Center.X, base.NPC.Center.Y);
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(TextureAssets.Npc[base.NPC.type].Value.Width / 2), (float)(TextureAssets.Npc[base.NPC.type].Value.Height / Main.npcFrameCount[base.NPC.type] / 2));
		Vector2 vector = val - screenPos;
		vector -= new Vector2((float)GlowTexture.Value.Width, (float)(GlowTexture.Value.Height / Main.npcFrameCount[base.NPC.type])) * 1f / 2f;
		vector += halfSizeTexture * 1f + new Vector2(0f, 4f + base.NPC.gfxOffY);
		Color color = Utils.MultiplyRGBA(new Color(127 - base.NPC.alpha, 127 - base.NPC.alpha, 127 - base.NPC.alpha, 0), Color.Blue);
		Main.spriteBatch.Draw(GlowTexture.Value, vector, (Rectangle?)base.NPC.frame, color, base.NPC.rotation, halfSizeTexture, 1f, spriteEffects, 0f);
	}

	public override bool? CanBeHitByProjectile(Projectile projectile)
	{
		if (projectile.minion && !projectile.Calamity().overridesMinionDamagePrevention)
		{
			return base.NPC.chaseable;
		}
		return null;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.Player.Calamity().ZoneSunkenSea && spawnInfo.Water && !spawnInfo.Player.Calamity().clamity)
		{
			return SpawnCondition.CaveJellyfish.Chance * 0.9f;
		}
		return 0f;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.DefineConditionalDropSet(DropHelper.PostDS()).Add(ModContent.ItemType<PrismShard>(), 1, 1, 3);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 68, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			if (Main.netMode != 2)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("PrismBackGore1").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("PrismBackGore2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("PrismBackGore3").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("PrismBackGore4").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("PrismBackGore5").Type);
			}
			for (int i = 0; i < 25; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 68, hit.HitDirection, -1f);
			}
		}
	}
}
