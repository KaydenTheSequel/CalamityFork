using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Placeables.Banners;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.AcidRain;

public class IrradiatedSlime : ModNPC
{
	public bool Falling = true;

	public static Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 2;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.width = 40;
		base.NPC.height = 30;
		base.NPC.damage = 42;
		base.NPC.lifeMax = 220;
		base.NPC.defense = 5;
		base.NPC.knockBackResist = 0.5f;
		base.AnimationType = 81;
		base.AIType = 141;
		base.NPC.value = Item.buyPrice(0, 0, 2);
		base.NPC.alpha = 50;
		base.NPC.lavaImmune = false;
		base.NPC.noGravity = false;
		base.NPC.noTileCollide = false;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<IrradiatedSlimeBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AcidRainBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		int associatedNPCType = ModContent.NPCType<GammaSlime>();
		bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[associatedNPCType], quickUnlock: true);
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.IrradiatedSlime")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(Falling);
		writer.Write(base.NPC.aiStyle);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		Falling = reader.ReadBoolean();
		base.NPC.aiStyle = reader.ReadInt32();
	}

	public override void AI()
	{
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.damage = ((base.NPC.velocity.Y != 0f && !(((Vector2)(ref base.NPC.velocity)).Length() < 3f)) ? base.NPC.defDamage : 0);
		Lighting.AddLight((int)((base.NPC.position.X + (float)(base.NPC.width / 2)) / 16f), (int)((base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16f), 0.6f, 0.8f, 0.6f);
		if (Falling)
		{
			base.NPC.TargetClosest(faceTarget: false);
			Player obj = Main.player[base.NPC.target];
			NPC nPC = base.NPC;
			int aiStyle = (base.AIType = -1);
			nPC.aiStyle = aiStyle;
			base.NPC.noTileCollide = (base.NPC.noGravity = true);
			if (obj.Top.Y < base.NPC.Bottom.Y)
			{
				base.NPC.noTileCollide = (base.NPC.noGravity = false);
				Falling = false;
				base.NPC.netUpdate = true;
			}
			else
			{
				base.NPC.velocity = Vector2.UnitY * 6f;
			}
		}
		else
		{
			base.NPC.aiStyle = 1;
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 75, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 75, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("IrradiatedSlime").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("IrradiatedSlime2").Type);
			}
		}
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC.DrawGlowmask(base.NPC, spriteBatch, GlowTexture.Value);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Irradiated>(), 180);
		}
	}
}
