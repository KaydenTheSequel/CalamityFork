using System;
using CalamityMod.BiomeManagers.BestiaryCategories;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Astral;

public class StellarCulex : ModNPC
{
	public static Asset<Texture2D> glowmask;

	public override void SetStaticDefaults()
	{
		if (!Main.dedServ)
		{
			glowmask = ModContent.Request<Texture2D>("CalamityMod/NPCs/Astral/StellarCulexGlow", (AssetRequestMode)2);
		}
		Main.npcFrameCount[base.Type] = 4;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.PortraitPositionXOverride = 10f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 30f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.width = 60;
		base.NPC.height = 50;
		base.NPC.aiStyle = 14;
		base.NPC.damage = 55;
		base.NPC.defense = 26;
		base.NPC.knockBackResist = 0.65f;
		base.NPC.lifeMax = 280;
		base.NPC.value = Item.buyPrice(0, 0, 5);
		base.NPC.DeathSound = CommonCalamitySounds.AstralNPCDeathSound;
		base.AnimationType = 152;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<StellarCulexBanner>();
		if (DownedBossSystem.downedAstrumAureus)
		{
			base.NPC.damage = 90;
			base.NPC.defense = 36;
			base.NPC.knockBackResist = 0.55f;
			base.NPC.lifeMax = 420;
		}
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AstralUnderground>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.StellarCulex")
		});
	}

	public override void FindFrame(int frameHeight)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		int frame = base.NPC.frame.Y / frameHeight;
		Dust d = CalamityGlobalNPC.SpawnDustOnNPC(base.NPC, 100, frameHeight, ModContent.DustType<AstralOrange>(), new Rectangle(66, 10, (frame == 0 || frame == 3) ? 32 : 24, 16), Vector2.Zero, 0.45f, useSpriteDirection: true);
		if (d != null)
		{
			d.customData = 0.04f;
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.soundDelay == 0)
		{
			base.NPC.soundDelay = 15;
			SoundEngine.PlaySound(in CommonCalamitySounds.AstralNPCHitSound, base.NPC.Center);
		}
		CalamityGlobalNPC.DoHitDust(base.NPC, hit.HitDirection, (Main.rand.Next(0, Math.Max(0, base.NPC.life)) == 0) ? 5 : ModContent.DustType<AstralEnemy>(), 1f, 4, 22);
		if (base.NPC.life <= 0 && !Main.dedServ)
		{
			for (int i = 0; i < 6; i++)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.Center, base.NPC.velocity * 0.3f, base.Mod.Find<ModGore>("StellarCulexGore" + i).Type);
			}
		}
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector(50f, 30f);
		spriteBatch.Draw(glowmask.Value, base.NPC.Center - screenPos, (Rectangle?)base.NPC.frame, Color.White * 0.6f, base.NPC.rotation, origin, 1f, (SpriteEffects)(base.NPC.spriteDirection == 1), 0f);
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (CalamityGlobalNPC.AnyEvents(spawnInfo.Player))
		{
			return 0f;
		}
		if (spawnInfo.Player.InAstral(2))
		{
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
		npcLoot.AddIf(() => DownedBossSystem.downedAstrumAureus, ModContent.ItemType<StarbusterCore>(), 10);
	}
}
