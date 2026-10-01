using System;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.NormalNPCs;

public class WulfrumRover : ModNPC
{
	public const float PlayerTargetingThreshold = 90f;

	public const float PlayerSearchDistance = 500f;

	public const float StuckJumpPromptTime = 90f;

	public const float MaxMovementSpeedX = 6f;

	public const float JumpSpeed = -4f;

	public int mineDelay;

	public float TimeSpentStuck
	{
		get
		{
			return base.NPC.ai[0];
		}
		set
		{
			base.NPC.ai[0] = value;
		}
	}

	public float SuperchargeTimer
	{
		get
		{
			return base.NPC.ai[3];
		}
		set
		{
			base.NPC.ai[3] = value;
		}
	}

	public bool Supercharged => SuperchargeTimer > 0f;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 16;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.SpriteDirection = 1;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.AIType = -1;
		base.NPC.aiStyle = -1;
		base.NPC.damage = 10;
		base.NPC.width = 40;
		base.NPC.height = 40;
		base.NPC.defense = 4;
		base.NPC.lifeMax = 40;
		base.NPC.knockBackResist = (Main.zenithWorld ? 0f : 0.15f);
		base.NPC.value = Item.buyPrice(0, 0, 0, 75);
		base.NPC.HitSound = WulfrumAmplifier.Hit;
		base.NPC.DeathSound = CommonCalamitySounds.WulfrumNPCDeathSound;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<WulfrumRoverBanner>();
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
		if (Main.zenithWorld)
		{
			base.NPC.scale = 2f;
		}
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.WulfrumRover")
		});
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		int frame = (int)(base.NPC.frameCounter / 5.0) % (Main.npcFrameCount[base.Type] / 2);
		if (Supercharged)
		{
			frame += Main.npcFrameCount[base.Type] / 2;
		}
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override void AI()
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.TargetClosest(faceTarget: false);
		if (Supercharged)
		{
			SuperchargeTimer--;
			base.NPC.defense = (Main.getGoodWorld ? 20 : 13);
		}
		else if (!Supercharged)
		{
			base.NPC.defense = (Main.getGoodWorld ? 10 : 4);
		}
		Player player = Main.player[base.NPC.target];
		if (Collision.CanHitLine(player.position, player.width, player.height, base.NPC.position, base.NPC.width, base.NPC.height) && Math.Abs(player.Center.X - base.NPC.Center.X) < 500f && Math.Abs(player.Center.X - base.NPC.Center.X) > 90f)
		{
			int direction = Math.Sign(player.Center.X - base.NPC.Center.X) * ((!base.NPC.confused) ? 1 : (-1));
			if (base.NPC.direction != direction)
			{
				base.NPC.direction = direction;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.collideX)
		{
			base.NPC.direction *= -1;
			base.NPC.netUpdate = true;
		}
		if (base.NPC.oldPosition == base.NPC.position)
		{
			TimeSpentStuck++;
			if (Main.netMode != 1 && TimeSpentStuck > 90f)
			{
				base.NPC.velocity.Y = -4f;
				TimeSpentStuck = 0f;
				base.NPC.netUpdate = true;
			}
		}
		else
		{
			TimeSpentStuck = 0f;
		}
		base.NPC.spriteDirection = -base.NPC.direction;
		base.NPC.velocity.X = MathHelper.Lerp(base.NPC.velocity.X, 6f * (float)base.NPC.direction, 0.0125f);
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.PlayerSafe || spawnInfo.Player.Calamity().ZoneSulphur || (!spawnInfo.Player.ZoneOverworldHeight && !Main.remixWorld) || (!spawnInfo.Player.ZoneNormalCaverns && spawnInfo.Player.ZoneGlowshroom && Main.remixWorld))
		{
			return 0f;
		}
		return (Main.remixWorld ? SpawnCondition.Cavern.Chance : SpawnCondition.OverworldDaySlime.Chance) * (Main.hardMode ? 0.055f : 0.135f) * (NPC.AnyNPCs(ModContent.NPCType<WulfrumAmplifier>()) ? 5.5f : 1f);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ)
		{
			return;
		}
		for (int k = 0; k < 4; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 3, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 3, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("WulfrumRoverGore1").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("WulfrumRoverGore2").Type);
				int randomGoreCount = Main.rand.Next(1, 4);
				for (int j = 0; j < randomGoreCount; j++)
				{
					Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("WulfrumEnemyGore" + Main.rand.Next(1, 11)).Type);
				}
			}
		}
		if (Main.zenithWorld && mineDelay == 0)
		{
			Vector2 roverBase = default(Vector2);
			((Vector2)(ref roverBase))._002Ector(base.NPC.Center.X, base.NPC.Center.Y + 5f);
			int mine = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), roverBase, Vector2.Zero, 135, 50, 0f);
			if (mine.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[mine].friendly = false;
				Main.projectile[mine].hostile = true;
				Main.projectile[mine].timeLeft = 60;
			}
			mineDelay = 3;
		}
		else if (Main.zenithWorld && mineDelay >= 1)
		{
			mineDelay--;
		}
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		if (Supercharged)
		{
			float scale = (Main.zenithWorld ? 0.2f : 0.15f) + 0.03f * (0.5f + 0.5f * (float)Math.Sin(Main.GlobalTimeWrappedHourly * 0.5f + (float)base.NPC.whoAmI * 0.2f));
			float noiseScale = 0.6f;
			Effect shieldEffect = Terraria.Graphics.Effects.Filters.Scene["CalamityMod:RoverDriveShield"].GetShader().Shader;
			shieldEffect.Parameters["time"].SetValue(Main.GlobalTimeWrappedHourly * 0.24f);
			shieldEffect.Parameters["blowUpPower"].SetValue(2.5f);
			shieldEffect.Parameters["blowUpSize"].SetValue(0.5f);
			shieldEffect.Parameters["noiseScale"].SetValue(noiseScale);
			float baseShieldOpacity = 0.9f + 0.1f * (float)Math.Sin(Main.GlobalTimeWrappedHourly * 2f);
			shieldEffect.Parameters["shieldOpacity"].SetValue(baseShieldOpacity);
			shieldEffect.Parameters["shieldEdgeBlendStrenght"].SetValue(4f);
			Color blueTint = default(Color);
			((Color)(ref blueTint))._002Ector(51, 102, 255);
			Color cyanTint = default(Color);
			((Color)(ref cyanTint))._002Ector(71, 202, 255);
			Color wulfGreen = new Color(194, 255, 67) * 0.8f;
			Color edgeColor = CalamityUtils.MulticolorLerp(Main.GlobalTimeWrappedHourly * 0.2f, blueTint, cyanTint, wulfGreen);
			shieldEffect.Parameters["shieldColor"].SetValue(((Color)(ref blueTint)).ToVector3());
			shieldEffect.Parameters["shieldEdgeColor"].SetValue(((Color)(ref edgeColor)).ToVector3());
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, shieldEffect, Main.GameViewMatrix.TransformationMatrix);
			if (RoverDrive.NoiseTex == null)
			{
				RoverDrive.NoiseTex = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/TechyNoise", (AssetRequestMode)2);
			}
			Texture2D tex = RoverDrive.NoiseTex.Value;
			Vector2 pos = base.NPC.Center + base.NPC.gfxOffY * Vector2.UnitY - Main.screenPosition;
			Main.spriteBatch.Draw(tex, pos, (Rectangle?)null, Color.White, 0f, tex.Size() / 2f, scale, (SpriteEffects)0, 0f);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.Transform);
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<WulfrumMetalScrap>(), 1, 1, 2);
		npcLoot.Add(ModContent.ItemType<RoverDrive>(), 10);
		npcLoot.Add(ModContent.ItemType<WulfrumBattery>(), new Fraction(7, 100));
		npcLoot.AddIf((DropAttemptInfo info) => info.npc.ModNPC<WulfrumRover>().Supercharged, ModContent.ItemType<EnergyCore>());
	}
}
