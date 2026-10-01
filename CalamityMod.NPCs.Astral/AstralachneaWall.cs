using System;
using CalamityMod.BiomeManagers.BestiaryCategories;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Sounds;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Astral;

public class AstralachneaWall : ModNPC
{
	public static Asset<Texture2D> glowmask;

	public override LocalizedText DisplayName => CalamityUtils.GetText("NPCs.AstralachneaGround.DisplayName");

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 4;
		if (!Main.dedServ)
		{
			glowmask = ModContent.Request<Texture2D>("CalamityMod/NPCs/Astral/AstralachneaWallGlow", (AssetRequestMode)2);
		}
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Rotation = -(float)Math.PI / 2f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.width = 60;
		base.NPC.height = 60;
		base.NPC.aiStyle = -1;
		base.NPC.damage = 55;
		base.NPC.defense = 30;
		base.NPC.lifeMax = 500;
		base.NPC.DeathSound = CommonCalamitySounds.AstralNPCDeathSound;
		base.NPC.knockBackResist = 0f;
		base.NPC.noGravity = true;
		base.NPC.value = Item.buyPrice(0, 0, 8);
		base.NPC.timeLeft = NPC.activeTime * 2;
		base.AnimationType = 238;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<AstralachneaBanner>();
		if (DownedBossSystem.downedAstrumAureus)
		{
			base.NPC.damage = 90;
			base.NPC.defense = 40;
			base.NPC.lifeMax = 750;
		}
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AstralUnderground>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Astralachnea")
		});
	}

	public override void AI()
	{
		CalamityGlobalNPC.DoSpiderWallAI(base.NPC, ModContent.NPCType<AstralachneaGround>(), CalamityWorld.death ? 3.6f : (CalamityWorld.revenge ? 3f : 2.4f), CalamityWorld.death ? 0.15f : (CalamityWorld.revenge ? 0.125f : 0.1f));
	}

	public override void FindFrame(int frameHeight)
	{
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.frameCounter += 0.10000000149011612;
			base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
			base.NPC.frame.Y = (int)base.NPC.frameCounter * frameHeight;
			return;
		}
		int frame = base.NPC.frame.Y / frameHeight;
		Rectangle rect = default(Rectangle);
		((Rectangle)(ref rect))._002Ector(12, 24, 18, 10);
		Rectangle rect2 = default(Rectangle);
		((Rectangle)(ref rect2))._002Ector(12, 44, 18, 10);
		switch (frame)
		{
		case 1:
			((Rectangle)(ref rect))._002Ector(6, 26, 28, 8);
			((Rectangle)(ref rect2))._002Ector(6, 44, 28, 8);
			break;
		case 2:
			((Rectangle)(ref rect))._002Ector(12, 26, 18, 8);
			((Rectangle)(ref rect2))._002Ector(12, 44, 18, 8);
			break;
		case 3:
			((Rectangle)(ref rect))._002Ector(16, 24, 16, 10);
			((Rectangle)(ref rect2))._002Ector(16, 44, 16, 10);
			break;
		}
		Dust d = CalamityGlobalNPC.SpawnDustOnNPC(base.NPC, 80, frameHeight, ModContent.DustType<AstralOrange>(), rect, Vector2.Zero, 0.225f, useSpriteDirection: true);
		Dust d2 = CalamityGlobalNPC.SpawnDustOnNPC(base.NPC, 80, frameHeight, ModContent.DustType<AstralOrange>(), rect2, Vector2.Zero, 0.225f, useSpriteDirection: true);
		if (d != null)
		{
			d.customData = 0.04f;
		}
		if (d2 != null)
		{
			d2.customData = 0.04f;
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
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.Center, base.NPC.velocity * 0.3f, base.Mod.Find<ModGore>("AstralachneaGore" + i).Type);
			}
		}
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector(40f, 40f);
		spriteBatch.Draw(glowmask.Value, base.NPC.Center - screenPos - new Vector2(0f, 8f), (Rectangle?)base.NPC.frame, Color.White * 0.6f, base.NPC.rotation, origin, 1f, (SpriteEffects)0, 0f);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180);
		}
	}

	public static void ModifyAstralachneaLoot(NPCLoot npcLoot)
	{
		npcLoot.AddIf(() => !Main.expertMode, ModContent.ItemType<StarblightSoot>(), 2, 2, 3);
		npcLoot.AddIf(() => Main.expertMode, ModContent.ItemType<StarblightSoot>(), 1, 1, 4);
		npcLoot.AddIf(() => DownedBossSystem.downedAstrumAureus, ModContent.ItemType<AstralachneaStaff>(), 10);
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		ModifyAstralachneaLoot(npcLoot);
	}
}
