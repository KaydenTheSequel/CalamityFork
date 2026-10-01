using System;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Abyss;

[LongDistanceNetSync]
public class BobbitWormHead : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 4;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.CustomTexturePath = "CalamityMod/ExtraTextures/Bestiary/BobbitWorm_Bestiary";
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.Y += 40f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.lavaImmune = true;
		base.NPC.aiStyle = -1;
		base.NPC.damage = 150;
		base.NPC.width = 80;
		base.NPC.height = 40;
		base.NPC.defense = 50;
		base.NPC.lifeMax = 6000;
		base.NPC.knockBackResist = 0f;
		base.AIType = -1;
		base.NPC.noGravity = true;
		base.NPC.value = Item.buyPrice(0, 0, 50);
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<BobbitWormBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AbyssLayer4Biome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.BobbitWorm")
		});
	}

	public override void AI()
	{
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_070e: Unknown result type (might be due to invalid IL or missing references)
		//IL_072e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0738: Unknown result type (might be due to invalid IL or missing references)
		//IL_0753: Unknown result type (might be due to invalid IL or missing references)
		//IL_075d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0598: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0608: Unknown result type (might be due to invalid IL or missing references)
		//IL_0618: Unknown result type (might be due to invalid IL or missing references)
		//IL_063b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0645: Unknown result type (might be due to invalid IL or missing references)
		//IL_0661: Unknown result type (might be due to invalid IL or missing references)
		//IL_066b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_0540: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.npc[(int)base.NPC.ai[2]].active || (int)base.NPC.ai[2] < 0)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.active = false;
			base.NPC.netUpdate = true;
		}
		else if (base.NPC.ai[0] == 0f)
		{
			base.NPC.noTileCollide = true;
			float launchSpeed = 14f;
			Vector2 bobbitCenter = default(Vector2);
			((Vector2)(ref bobbitCenter))._002Ector(base.NPC.Center.X, base.NPC.Center.Y);
			float segmentXDist = Main.npc[(int)base.NPC.ai[2]].Center.X - bobbitCenter.X;
			float segmentYDist = Main.npc[(int)base.NPC.ai[2]].Center.Y - bobbitCenter.Y;
			float segmentDistance = (float)Math.Sqrt(segmentXDist * segmentXDist + segmentYDist * segmentYDist);
			if (segmentDistance < 11f + launchSpeed)
			{
				base.NPC.rotation = 0f;
				base.NPC.velocity.X = segmentXDist;
				base.NPC.velocity.Y = segmentYDist;
				base.NPC.ai[1]++;
				if (!(base.NPC.ai[1] >= 60f))
				{
					return;
				}
				base.NPC.TargetClosest();
				if (base.NPC.Center.Y > Main.player[base.NPC.target].Center.Y)
				{
					Vector2 val = Main.player[base.NPC.target].Center - base.NPC.Center;
					if (((Vector2)(ref val)).Length() < Main.player[base.NPC.target].Calamity().GetAbyssAggro(480f) && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
					{
						base.NPC.ai[1] = 0f;
						base.NPC.ai[0] = 1f;
						return;
					}
				}
				base.NPC.ai[1] = 0f;
			}
			else
			{
				segmentDistance = launchSpeed / segmentDistance;
				base.NPC.velocity.X = segmentXDist * segmentDistance;
				base.NPC.velocity.Y = segmentYDist * segmentDistance;
				base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) - 1.57f;
			}
		}
		else if (base.NPC.ai[0] == 1f)
		{
			base.NPC.noTileCollide = true;
			base.NPC.collideX = false;
			base.NPC.collideY = false;
			Vector2 bobbitCenterReturn = default(Vector2);
			((Vector2)(ref bobbitCenterReturn))._002Ector(base.NPC.Center.X, base.NPC.Center.Y);
			float segmentReturnXDist = Main.player[base.NPC.target].Center.X - bobbitCenterReturn.X;
			float segmentReturnYDist = Main.player[base.NPC.target].Center.Y - bobbitCenterReturn.Y;
			float segmentReturnDistance = (float)Math.Sqrt(segmentReturnXDist * segmentReturnXDist + segmentReturnYDist * segmentReturnYDist);
			segmentReturnDistance = 16f / segmentReturnDistance;
			base.NPC.velocity.X = segmentReturnXDist * segmentReturnDistance;
			base.NPC.velocity.Y = segmentReturnYDist * segmentReturnDistance;
			base.NPC.ai[0] = 2f;
			base.NPC.rotation = (float)Math.Atan2(0.0 - (double)base.NPC.velocity.Y, 0.0 - (double)base.NPC.velocity.X) - 1.57f;
		}
		else if (base.NPC.ai[0] == 2f)
		{
			if (Math.Abs(base.NPC.velocity.X) > Math.Abs(base.NPC.velocity.Y))
			{
				if (base.NPC.velocity.X > 0f && base.NPC.Center.X > Main.player[base.NPC.target].Center.X)
				{
					base.NPC.noTileCollide = false;
				}
				if (base.NPC.velocity.X < 0f && base.NPC.Center.X < Main.player[base.NPC.target].Center.X)
				{
					base.NPC.noTileCollide = false;
				}
			}
			else
			{
				if (base.NPC.velocity.Y > 0f && base.NPC.Center.Y > Main.player[base.NPC.target].Center.Y)
				{
					base.NPC.noTileCollide = false;
				}
				if (base.NPC.velocity.Y < 0f && base.NPC.Center.Y < Main.player[base.NPC.target].Center.Y)
				{
					base.NPC.noTileCollide = false;
				}
			}
			Vector2 bobbitCenterReturning = default(Vector2);
			((Vector2)(ref bobbitCenterReturning))._002Ector(base.NPC.Center.X, base.NPC.Center.Y);
			float num = Main.npc[(int)base.NPC.ai[2]].Center.X - bobbitCenterReturning.X;
			float segmentReturningYDist = Main.npc[(int)base.NPC.ai[2]].Center.Y - bobbitCenterReturning.Y;
			if ((float)Math.Sqrt(num * num + segmentReturningYDist * segmentReturningYDist) > 700f || base.NPC.collideX || base.NPC.collideY)
			{
				base.NPC.noTileCollide = true;
				base.NPC.ai[0] = 0f;
			}
		}
		else
		{
			if (base.NPC.ai[0] != 3f)
			{
				return;
			}
			base.NPC.noTileCollide = true;
			float unusedAcceleration = 0.25f;
			Vector2 unusedBobbitCenter = default(Vector2);
			((Vector2)(ref unusedBobbitCenter))._002Ector(base.NPC.Center.X, base.NPC.Center.Y);
			float unusedTargetXDist = Main.player[base.NPC.target].Center.X - unusedBobbitCenter.X;
			float unusedTargetYDist = Main.player[base.NPC.target].Center.Y - unusedBobbitCenter.Y;
			float unusedTargetDistance = (float)Math.Sqrt(unusedTargetXDist * unusedTargetXDist + unusedTargetYDist * unusedTargetYDist);
			unusedTargetDistance = 16f / unusedTargetDistance;
			unusedTargetXDist *= unusedTargetDistance;
			unusedTargetYDist *= unusedTargetDistance;
			if (base.NPC.velocity.X < unusedTargetXDist)
			{
				base.NPC.velocity.X = base.NPC.velocity.X + unusedAcceleration;
				if (base.NPC.velocity.X < 0f && unusedTargetXDist > 0f)
				{
					base.NPC.velocity.X = base.NPC.velocity.X + unusedAcceleration * 2f;
				}
			}
			else if (base.NPC.velocity.X > unusedTargetXDist)
			{
				base.NPC.velocity.X = base.NPC.velocity.X - unusedAcceleration;
				if (base.NPC.velocity.X > 0f && unusedTargetXDist < 0f)
				{
					base.NPC.velocity.X = base.NPC.velocity.X - unusedAcceleration * 2f;
				}
			}
			if (base.NPC.velocity.Y < unusedTargetYDist)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y + unusedAcceleration;
				if (base.NPC.velocity.Y < 0f && unusedTargetYDist > 0f)
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y + unusedAcceleration * 2f;
				}
			}
			else if (base.NPC.velocity.Y > unusedTargetYDist)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y - unusedAcceleration;
				if (base.NPC.velocity.Y > 0f && unusedTargetYDist < 0f)
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y - unusedAcceleration * 2f;
				}
			}
			base.NPC.rotation = (float)Math.Atan2(0.0 - (double)base.NPC.velocity.Y, 0.0 - (double)base.NPC.velocity.X);
		}
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += 0.10000000149011612;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = default(Vector2);
		((Vector2)(ref center))._002Ector(base.NPC.Center.X, base.NPC.Center.Y);
		float drawPositionX = Main.npc[(int)base.NPC.ai[2]].Center.X - center.X;
		float drawPositionY = Main.npc[(int)base.NPC.ai[2]].Center.Y - center.Y;
		float rotation = (float)Math.Atan2(drawPositionY, drawPositionX) - 1.57f;
		bool draw = !base.NPC.IsABestiaryIconDummy;
		while (draw)
		{
			float totalDrawDistance = (float)Math.Sqrt(drawPositionX * drawPositionX + drawPositionY * drawPositionY);
			if (totalDrawDistance < 16f)
			{
				draw = false;
				continue;
			}
			totalDrawDistance = 16f / totalDrawDistance;
			drawPositionX *= totalDrawDistance;
			drawPositionY *= totalDrawDistance;
			center.X += drawPositionX;
			center.Y += drawPositionY;
			drawPositionX = Main.npc[(int)base.NPC.ai[2]].Center.X - center.X;
			drawPositionY = Main.npc[(int)base.NPC.ai[2]].Center.Y - center.Y;
			drawPositionY += 4f;
			Color color = Lighting.GetColor((int)center.X / 16, (int)(center.Y / 16f));
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityMod/NPCs/Abyss/BobbitWormSegment", (AssetRequestMode)2).Value, new Vector2(center.X - screenPos.X, center.Y - screenPos.Y), (Rectangle?)new Rectangle(0, 0, ModContent.Request<Texture2D>("CalamityMod/NPCs/Abyss/BobbitWormSegment", (AssetRequestMode)2).Value.Width, ModContent.Request<Texture2D>("CalamityMod/NPCs/Abyss/BobbitWormSegment", (AssetRequestMode)2).Value.Height), color, rotation, new Vector2((float)ModContent.Request<Texture2D>("CalamityMod/NPCs/Abyss/BobbitWormSegment", (AssetRequestMode)2).Value.Width * 0.5f, (float)ModContent.Request<Texture2D>("CalamityMod/NPCs/Abyss/BobbitWormSegment", (AssetRequestMode)2).Value.Height * 0.5f), 1f, (SpriteEffects)0, 0f);
		}
		return true;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<HadopelagicPressure>(), 300);
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.DefineConditionalDropSet(DropHelper.PostLevi()).Add(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<DepthCells>(), 2, 5, 7, 7, 10));
		npcLoot.AddIf(DropHelper.PostPolter(), ModContent.ItemType<BobbitHook>(), 3);
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
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("BobbitWorm").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("BobbitWorm2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("BobbitWorm3").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("BobbitWorm4").Type);
			}
		}
	}
}
