using System;
using CalamityMod.BiomeManagers.BestiaryCategories;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items.Accessories.Wings;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Sounds;
using CalamityMod.Tiles.AstralDesert;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Astral;

public class Hadarian : ModNPC
{
	public static Asset<Texture2D> glowmask;

	public override void SetStaticDefaults()
	{
		if (!Main.dedServ)
		{
			glowmask = ModContent.Request<Texture2D>("CalamityMod/NPCs/Astral/HadarianGlow", (AssetRequestMode)2);
		}
		NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers();
		value.Position.X += 10f;
		value.Position.Y += 10f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		Main.npcFrameCount[base.Type] = 7;
	}

	public override void SetDefaults()
	{
		base.NPC.width = 50;
		base.NPC.height = 40;
		base.NPC.aiStyle = -1;
		base.NPC.damage = 50;
		base.NPC.defense = 18;
		base.NPC.lifeMax = 360;
		base.NPC.DeathSound = CommonCalamitySounds.AstralNPCDeathSound;
		base.NPC.knockBackResist = 0.75f;
		base.NPC.value = Item.buyPrice(0, 0, 8);
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<HadarianBanner>();
		if (DownedBossSystem.downedAstrumAureus)
		{
			base.NPC.damage = 80;
			base.NPC.defense = 28;
			base.NPC.knockBackResist = 0.65f;
			base.NPC.lifeMax = 540;
		}
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AstralDesert>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Hadarian")
		});
	}

	public override void AI()
	{
		CalamityGlobalNPC.DoVultureAI(base.NPC, CalamityWorld.death ? 0.25f : (CalamityWorld.revenge ? 0.2f : 0.15f), CalamityWorld.death ? 5.5f : (CalamityWorld.revenge ? 4.5f : 3.5f), 32, 50, 150, 150);
		base.NPC.rotation = base.NPC.velocity.X * 0.1f;
	}

	public override void FindFrame(int frameHeight)
	{
		if (base.NPC.velocity.Y == 0f)
		{
			base.NPC.spriteDirection = base.NPC.direction;
		}
		else
		{
			if ((double)base.NPC.velocity.X > 0.5)
			{
				base.NPC.spriteDirection = 1;
			}
			if ((double)base.NPC.velocity.X < -0.5)
			{
				base.NPC.spriteDirection = -1;
			}
		}
		if (base.NPC.velocity.X == 0f && base.NPC.velocity.Y == 0f && !base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.frame.Y = 0;
			base.NPC.frameCounter = 0.0;
		}
		else
		{
			base.NPC.frameCounter++;
			if (base.NPC.frameCounter > 5.0)
			{
				base.NPC.frameCounter = 0.0;
				base.NPC.frame.Y += frameHeight;
			}
			if (base.NPC.frame.Y > frameHeight * 6 || base.NPC.frame.Y == 0)
			{
				base.NPC.frame.Y = frameHeight;
			}
		}
		DoWingDust(frameHeight);
	}

	private void DoWingDust(int frameHeight)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		int frame = base.NPC.frame.Y / frameHeight;
		Dust d = null;
		switch (frame)
		{
		case 1:
			d = CalamityGlobalNPC.SpawnDustOnNPC(base.NPC, 82, frameHeight, ModContent.DustType<AstralOrange>(), new Rectangle(38, 16, 22, 20), Vector2.Zero, 0.35f);
			break;
		case 2:
			d = CalamityGlobalNPC.SpawnDustOnNPC(base.NPC, 82, frameHeight, ModContent.DustType<AstralOrange>(), new Rectangle(38, 24, 30, 14), Vector2.Zero);
			break;
		case 3:
			d = CalamityGlobalNPC.SpawnDustOnNPC(base.NPC, 82, frameHeight, ModContent.DustType<AstralOrange>(), new Rectangle(44, 28, 32, 20), Vector2.Zero);
			break;
		case 4:
			d = CalamityGlobalNPC.SpawnDustOnNPC(base.NPC, 82, frameHeight, ModContent.DustType<AstralOrange>(), new Rectangle(42, 36, 18, 30), Vector2.Zero, 0.3f);
			break;
		}
		if (d != null)
		{
			d.customData = 0.03f;
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.IsABestiaryIconDummy)
		{
			return true;
		}
		if (base.NPC.ai[0] == 0f)
		{
			Vector2 position = base.NPC.Bottom - new Vector2(19f, 42f);
			Rectangle src = default(Rectangle);
			((Rectangle)(ref src))._002Ector(20, 34, 38, 42);
			spriteBatch.Draw(TextureAssets.Npc[base.Type].Value, position - screenPos, (Rectangle?)src, drawColor, base.NPC.rotation, default(Vector2), 1f, (SpriteEffects)(base.NPC.spriteDirection == 1), 0f);
			spriteBatch.Draw(glowmask.Value, position - screenPos, (Rectangle?)src, Color.White * 0.6f, base.NPC.rotation, default(Vector2), 1f, (SpriteEffects)(base.NPC.spriteDirection == 1), 0f);
			return false;
		}
		return true;
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.ai[0] != 0f)
		{
			Vector2 origin = default(Vector2);
			((Vector2)(ref origin))._002Ector(41f, 39f);
			spriteBatch.Draw(glowmask.Value, base.NPC.Center - screenPos - new Vector2(0f, 12f), (Rectangle?)base.NPC.frame, Color.White * 0.6f, base.NPC.rotation, origin, 1f, (SpriteEffects)(base.NPC.spriteDirection == 1), 0f);
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
		CalamityGlobalNPC.DoHitDust(base.NPC, hit.HitDirection, (Main.rand.Next(0, Math.Max(0, base.NPC.life)) == 0) ? 5 : ModContent.DustType<AstralEnemy>(), 1f, 3);
		if (base.NPC.life <= 0 && !Main.dedServ)
		{
			for (int i = 0; i < 5; i++)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.Center, base.NPC.velocity * 0.3f, base.Mod.Find<ModGore>("HadarianGore" + i).Type);
			}
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];
		if (CalamityGlobalNPC.AnyEvents(spawnInfo.Player))
		{
			return 0f;
		}
		if (spawnInfo.Player.InAstral(3) && spawnInfo.SpawnTileType == ModContent.TileType<AstralSand>() && tile.WallType == 0)
		{
			return 0.25f;
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
		npcLoot.Add(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<StarblightSoot>(), 1, 1, 3, 1, 4));
		npcLoot.AddIf(() => DownedBossSystem.downedAstrumAureus, ModContent.ItemType<HadarianWings>(), 10);
	}
}
