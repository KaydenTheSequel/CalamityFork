using System;
using System.Linq;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Weapons.Magic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.AcidRain;

public class AcidEel : ModNPC
{
	public static Asset<Texture2D> BodyTexture;

	public static Asset<Texture2D> TailTexture;

	public static Asset<Texture2D> BestiaryTexture;

	public Player Target => Main.player[base.NPC.target];

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 6;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		NPCID.Sets.TrailCacheLength[base.Type] = 12;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.PortraitPositionXOverride = 0f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 15f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		if (!Main.dedServ)
		{
			BodyTexture = ModContent.Request<Texture2D>(Texture + "Body", (AssetRequestMode)1);
			TailTexture = ModContent.Request<Texture2D>(Texture + "Tail", (AssetRequestMode)2);
			BestiaryTexture = ModContent.Request<Texture2D>(Texture + "Bestiary", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.width = 20;
		base.NPC.height = 20;
		base.NPC.damage = 20;
		base.NPC.lifeMax = 45;
		base.NPC.defense = 4;
		base.NPC.knockBackResist = 0.9f;
		if (DownedBossSystem.downedPolterghast)
		{
			base.NPC.damage = 100;
			base.NPC.lifeMax = 2000;
			base.NPC.defense = 20;
			base.NPC.knockBackResist = 0.7f;
		}
		else if (DownedBossSystem.downedAquaticScourge)
		{
			base.NPC.damage = 50;
			base.NPC.lifeMax = 180;
		}
		base.NPC.value = Item.buyPrice(0, 0, 2);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.lavaImmune = false;
		base.NPC.noGravity = true;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<AcidEelBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AcidRainBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.AcidEel")
		});
	}

	public override void AI()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.TargetClosest(faceTarget: false);
		if (Main.rand.NextBool(480))
		{
			SoundEngine.PlaySound(in SoundID.Zombie32, base.NPC.Center);
		}
		if (base.NPC.wet)
		{
			SwimTowardsTarget();
			return;
		}
		base.NPC.rotation = base.NPC.rotation.AngleLerp(0f, 0.1f);
		base.NPC.velocity.X *= 0.95f;
		if (base.NPC.velocity.Y < 14f)
		{
			base.NPC.velocity.Y += 0.15f;
		}
		base.NPC.spriteDirection = base.NPC.direction;
	}

	public void SwimTowardsTarget()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		float swimSpeed = 12f;
		if (DownedBossSystem.downedAquaticScourge)
		{
			swimSpeed += 3f;
		}
		if (DownedBossSystem.downedPolterghast)
		{
			swimSpeed += 4f;
		}
		bool airAbove = false;
		for (int dy = -160; dy < 0; dy += 8)
		{
			if (!Collision.WetCollision(base.NPC.position + Vector2.UnitY * (float)dy, base.NPC.width, 16))
			{
				airAbove = true;
				break;
			}
		}
		if (!airAbove)
		{
			base.NPC.velocity.Y = MathHelper.Clamp(base.NPC.velocity.Y - 0.25f, -14f, 14f);
		}
		else
		{
			base.NPC.velocity.Y = MathHelper.Clamp(base.NPC.velocity.Y + 0.4f, -4f, 8f);
		}
		if (base.NPC.direction == 0)
		{
			base.NPC.direction = Main.rand.NextBool().ToDirectionInt();
			base.NPC.netUpdate = true;
		}
		bool nearWorldEdge = base.NPC.Center.X < ((float)Main.offLimitBorderTiles + 2f) * 16f || base.NPC.Center.X > ((float)(Main.maxTilesX - Main.offLimitBorderTiles) - 2f) * 16f;
		if (((CalamityUtils.DistanceToTileCollisionHit(base.NPC.Center, Vector2.UnitX * (float)base.NPC.direction, 20) ?? 20f) < 5f) | nearWorldEdge)
		{
			base.NPC.direction *= -1;
			if (nearWorldEdge)
			{
				base.NPC.position.X += (float)Math.Sign((float)Main.maxTilesX * 8f - base.NPC.position.X) * 12f;
			}
			base.NPC.netUpdate = true;
		}
		base.NPC.velocity.X = (base.NPC.velocity.X * 24f + (float)base.NPC.direction * swimSpeed) / 25f;
	}

	public override bool? CanFallThroughPlatforms()
	{
		return true;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<SulphuricScale>(), 2, 1, 3);
		npcLoot.DefineConditionalDropSet(DropHelper.PostAS()).Add(ModContent.ItemType<SlitheringEels>(), 20);
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter >= 5.0)
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y += frameHeight;
			if (base.NPC.frame.Y >= Main.npcFrameCount[base.Type] * frameHeight)
			{
				base.NPC.frame.Y = 0;
			}
		}
	}

	public float SegmentWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return (float)base.NPC.width * base.NPC.scale * 0.5f;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		Vector2 headDrawPosition = base.NPC.Center - screenPos;
		Rectangle frame;
		if (base.NPC.IsABestiaryIconDummy)
		{
			Texture2D value = BestiaryTexture.Value;
			frame = base.NPC.frame;
			frame.Width = 74;
			Rectangle eelArea = frame;
			Main.EntitySpriteDraw(value, headDrawPosition, eelArea, base.NPC.GetAlpha(Color.White), base.NPC.rotation, eelArea.Size() * 0.5f, base.NPC.scale, (SpriteEffects)0);
			return false;
		}
		Texture2D headTexture = TextureAssets.Npc[base.Type].Value;
		Texture2D tailTexture = TailTexture.Value;
		Vector2[] segmentPositions = (Vector2[])base.NPC.oldPos.Clone();
		Vector2 segmentAreaTopLeft = Vector2.One * 999999f;
		Vector2 segmentAreaTopRight = Vector2.Zero;
		if (base.NPC.IsABestiaryIconDummy)
		{
			for (int i = 0; i < segmentPositions.Length; i++)
			{
				segmentPositions[i] = base.NPC.TopLeft + Vector2.UnitX * (float)i * 5f;
			}
		}
		segmentPositions = segmentPositions.Where(delegate(Vector2 p)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return p != Vector2.Zero;
		}).ToArray();
		for (int i2 = 0; i2 < segmentPositions.Length; i2++)
		{
			ref Vector2 reference = ref segmentPositions[i2];
			reference += base.NPC.Size * 0.5f - base.NPC.rotation.ToRotationVector2() * (float)Math.Sign(base.NPC.velocity.X) * 8f;
			if (segmentAreaTopLeft.X > segmentPositions[i2].X)
			{
				segmentAreaTopLeft.X = segmentPositions[i2].X;
			}
			if (segmentAreaTopLeft.Y > segmentPositions[i2].Y)
			{
				segmentAreaTopLeft.Y = segmentPositions[i2].Y;
			}
			if (segmentAreaTopRight.X < segmentPositions[i2].X)
			{
				segmentAreaTopRight.X = segmentPositions[i2].X;
			}
			if (segmentAreaTopRight.Y < segmentPositions[i2].Y)
			{
				segmentAreaTopRight.Y = segmentPositions[i2].Y;
			}
		}
		float offsetAngle = (base.NPC.position - base.NPC.oldPos[1]).ToRotation();
		Vector2 primitiveArea = (segmentAreaTopRight - segmentAreaTopLeft).RotatedBy(offsetAngle);
		frame = base.NPC.frame;
		frame.Width = 28;
		Rectangle tailArea = frame;
		GameShaders.Misc["CalamityMod:PrimitiveTexture"].SetShaderTexture(BodyTexture);
		GameShaders.Misc["CalamityMod:PrimitiveTexture"].Shader.Parameters["uPrimitiveSize"].SetValue(primitiveArea);
		GameShaders.Misc["CalamityMod:PrimitiveTexture"].Shader.Parameters["flipVertically"].SetValue(base.NPC.velocity.X > 0f);
		SpriteEffects direction = (SpriteEffects)(!(base.NPC.velocity.X < 0f));
		Main.EntitySpriteDraw(headTexture, headDrawPosition, base.NPC.frame, base.NPC.GetAlpha(Color.White), base.NPC.rotation, base.NPC.frame.Size() * 0.5f, base.NPC.scale, direction);
		if (segmentPositions.Length >= 2)
		{
			float tailRotation = (segmentPositions[^2] - segmentPositions[^1]).ToRotation() + (float)Math.PI;
			Vector2 tailDrawPosition = segmentPositions[^1] - tailRotation.ToRotationVector2() * 4f - screenPos;
			SpriteEffects tailDirection = (SpriteEffects)((!(base.NPC.velocity.X < 0f)) ? 2 : 0);
			Main.EntitySpriteDraw(tailTexture, tailDrawPosition, tailArea, base.NPC.GetAlpha(Color.White), tailRotation, tailArea.Size() * new Vector2(0f, 0.5f), base.NPC.scale, tailDirection);
			((Game)Main.instance).GraphicsDevice.BlendState = BlendState.AlphaBlend;
			PrimitiveRenderer.RenderTrail(segmentPositions, new PrimitiveSettings(SegmentWidthFunction, delegate
			{
				//IL_0006: Unknown result type (might be due to invalid IL or missing references)
				//IL_000b: Unknown result type (might be due to invalid IL or missing references)
				return base.NPC.GetAlpha(Color.White);
			}, null, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:PrimitiveTexture"]), 36);
		}
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 8; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 75, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("AcidEelGore").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("AcidEelGore2").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("AcidEelGore3").Type, base.NPC.scale);
			}
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 75, hit.HitDirection, -1f);
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Irradiated>(), 120);
		}
	}
}
