using System.IO;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.Leviathan;

public class LeviathanStart : ModNPC
{
	public static Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 6;
		NPCID.Sets.CantTakeLunchMoney[base.Type] = true;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = -6f;
		nPCBestiaryDrawModifiers.Scale = 0.65f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.75f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.damage = 0;
		base.NPC.width = 100;
		base.NPC.height = 100;
		base.NPC.defense = 0;
		base.NPC.lifeMax = 1000;
		base.NPC.knockBackResist = 0f;
		base.NPC.Opacity = 0f;
		base.NPC.noGravity = true;
		base.NPC.dontTakeDamage = true;
		base.NPC.chaseable = false;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = null;
		base.NPC.rarity = 2;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.NPC.Calamity().ProvidesProximityRage = false;
		if (Main.getGoodWorld)
		{
			base.NPC.scale *= 0.8f;
		}
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[2]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.LeviathanStart")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.dontTakeDamage);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.dontTakeDamage = reader.ReadBoolean();
	}

	public override void FindFrame(int frameHeight)
	{
		if (base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.Opacity = 1f;
		}
		base.NPC.frameCounter += 0.10000000149011612;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override void AI()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.TargetClosest();
		float playerLocation = base.NPC.Center.X - Main.player[base.NPC.target].Center.X;
		base.NPC.direction = ((playerLocation < 0f) ? 1 : (-1));
		base.NPC.spriteDirection = base.NPC.direction;
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) < 560f)
		{
			if (base.NPC.ai[0] < 90f)
			{
				base.NPC.ai[0]++;
			}
		}
		else if (base.NPC.ai[0] > 0f)
		{
			base.NPC.ai[0]--;
		}
		base.NPC.dontTakeDamage = base.NPC.ai[0] != 90f;
		base.NPC.Opacity = MathHelper.Clamp(base.NPC.ai[0] / 90f, 0f, 1f);
		Lighting.AddLight((int)base.NPC.Center.X / 16, (int)base.NPC.Center.Y / 16, 0f, 0f, 0.8f * base.NPC.Opacity);
		if (CalamityPlayer.areThereAnyDamnBosses)
		{
			base.NPC.active = false;
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D drawTex = (Main.zenithWorld ? TextureAssets.Npc[ModContent.NPCType<Leviathan>()].Value : TextureAssets.Npc[base.Type].Value);
		Rectangle frame = (Main.zenithWorld ? drawTex.Frame(2, 3) : base.NPC.frame);
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)(drawTex.Width / 2), (float)(drawTex.Height / 2));
		Vector2 drawPos = base.NPC.Center - screenPos;
		if (Main.zenithWorld)
		{
			drawPos += new Vector2(200f, 200f);
		}
		else
		{
			drawPos -= new Vector2((float)drawTex.Width, (float)(drawTex.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
			drawPos += origin * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		}
		Color color = (Main.zenithWorld ? Color.Purple : drawColor);
		float size = (Main.zenithWorld ? 0.39f : base.NPC.scale);
		spriteBatch.Draw(drawTex, drawPos, (Rectangle?)frame, base.NPC.GetAlpha(color), base.NPC.rotation, origin, size, spriteEffects, 0f);
		if (!Main.zenithWorld)
		{
			drawTex = GlowTexture.Value;
			spriteBatch.Draw(drawTex, drawPos, (Rectangle?)frame, Color.White, base.NPC.rotation, origin, base.NPC.scale, spriteEffects, 0f);
		}
		return false;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.Player.Calamity().disableAnahitaSpawns)
		{
			return 0f;
		}
		if (spawnInfo.PlayerSafe || !spawnInfo.Player.ZoneBeach || spawnInfo.Player.Calamity().ZoneSulphur || spawnInfo.Player.PillarZone())
		{
			return 0f;
		}
		if (NPC.AnyNPCs(base.NPC.type))
		{
			return 0f;
		}
		if (NPC.AnyNPCs(370))
		{
			return 0f;
		}
		if (NPC.AnyNPCs(ModContent.NPCType<Anahita>()))
		{
			return 0f;
		}
		if (NPC.AnyNPCs(ModContent.NPCType<Leviathan>()))
		{
			return 0f;
		}
		if (!Main.hardMode)
		{
			return SpawnCondition.OceanMonster.Chance * 0.025f;
		}
		if (!NPC.downedPlantBoss && !DownedBossSystem.downedCalamitasClone)
		{
			return SpawnCondition.OceanMonster.Chance * 0.1f;
		}
		return SpawnCondition.OceanMonster.Chance * 0.4f;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<AquaticHeart>(), 4);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.life > 0)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
		}
		else if (Main.netMode != 1)
		{
			int NPCType = (Main.zenithWorld ? ModContent.NPCType<Leviathan>() : ModContent.NPCType<Anahita>());
			CalamityUtils.BossAwakenMessage(NPC.NewNPC(base.NPC.GetSource_Death(), (int)base.NPC.Center.X, (int)base.NPC.position.Y + base.NPC.height, NPCType, base.NPC.whoAmI));
		}
	}
}
