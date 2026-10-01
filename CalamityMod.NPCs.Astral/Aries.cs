using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Astral;

public class Aries : ModNPC
{
	public static Asset<Texture2D> glowmask;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 8;
		if (!Main.dedServ)
		{
			glowmask = ModContent.Request<Texture2D>("CalamityMod/NPCs/Astral/AriesGlow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 50;
		base.NPC.width = 66;
		base.NPC.height = 64;
		base.NPC.aiStyle = 41;
		base.NPC.defense = 22;
		base.NPC.lifeMax = 300;
		base.NPC.knockBackResist = 0.6f;
		base.NPC.value = Item.buyPrice(0, 0, 3);
		base.NPC.DeathSound = CommonCalamitySounds.AstralNPCDeathSound;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<AriesBanner>();
		if (DownedBossSystem.downedAstrumAureus)
		{
			base.NPC.damage = 85;
			base.NPC.defense = 32;
			base.NPC.knockBackResist = 0.5f;
			base.NPC.lifeMax = 450;
		}
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AstralInfectionBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Aries")
		});
	}

	public override void FindFrame(int frameHeight)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		CalamityGlobalNPC.SpawnDustOnNPC(base.NPC, 66, frameHeight, ModContent.DustType<AstralOrange>(), new Rectangle(44, 18, 12, 12));
		if (base.NPC.velocity.Y == 0f)
		{
			base.NPC.frame.Y = 0;
		}
		else if ((double)base.NPC.velocity.Y < -1.5)
		{
			base.NPC.frame.Y = frameHeight * 7;
		}
		else if ((double)base.NPC.velocity.Y < 0.0)
		{
			base.NPC.frame.Y = frameHeight * 4;
		}
		else if ((double)base.NPC.velocity.Y > 1.5)
		{
			base.NPC.frame.Y = frameHeight * 6;
		}
		else
		{
			base.NPC.frame.Y = frameHeight * 5;
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.soundDelay == 0)
		{
			base.NPC.soundDelay = 15;
			SoundEngine.PlaySound(in CommonCalamitySounds.AstralNPCHitSound, base.NPC.Center);
		}
		CalamityGlobalNPC.DoHitDust(base.NPC, hit.HitDirection, ModContent.DustType<AstralOrange>(), 1f, 4, 24);
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		spriteBatch.Draw(glowmask.Value, base.NPC.Center - screenPos, (Rectangle?)base.NPC.frame, Color.White * 0.6f, base.NPC.rotation, new Vector2(33f, 31f), 1f, (SpriteEffects)(base.NPC.spriteDirection == 1), 0f);
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (CalamityGlobalNPC.AnyEvents(spawnInfo.Player))
		{
			return 0f;
		}
		if (spawnInfo.Player.InAstral(1))
		{
			return 0.15f;
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
		npcLoot.Add(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<StarblightSoot>(), 2, 1, 2, 1, 3));
		npcLoot.AddIf(() => DownedBossSystem.downedAstrumAureus, ModContent.ItemType<StellarKnife>(), 10);
	}
}
