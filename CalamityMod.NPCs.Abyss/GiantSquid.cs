using System;
using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.Abyss;

public class GiantSquid : ModNPC
{
	private bool hasBeenHit;

	public static Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 5;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.noGravity = true;
		base.NPC.damage = 100;
		base.NPC.width = 62;
		base.NPC.height = 226;
		base.NPC.defense = 18;
		base.NPC.lifeMax = 1000;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.value = Item.buyPrice(0, 0, 10);
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<GiantSquidBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AbyssLayer3Biome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.GiantSquid")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(hasBeenHit);
		writer.Write(base.NPC.chaseable);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		hasBeenHit = reader.ReadBoolean();
		base.NPC.chaseable = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.damage = 0;
		if (base.NPC.direction == 0)
		{
			base.NPC.TargetClosest();
		}
		if (!base.NPC.wet)
		{
			if (base.NPC.velocity.Y == 0f)
			{
				base.NPC.velocity.X = base.NPC.velocity.X * 0.98f;
				if ((double)base.NPC.velocity.X > -0.01 && (double)base.NPC.velocity.X < 0.01)
				{
					base.NPC.velocity.X = 0f;
				}
			}
			base.NPC.velocity.Y = base.NPC.velocity.Y + 0.2f;
			if (base.NPC.velocity.Y > 10f)
			{
				base.NPC.velocity.Y = 10f;
			}
			base.NPC.ai[0] = 1f;
			return;
		}
		if (base.NPC.collideX)
		{
			base.NPC.velocity.X = base.NPC.velocity.X * -1f;
			base.NPC.direction *= -1;
		}
		if (base.NPC.collideY)
		{
			if (base.NPC.velocity.Y > 0f)
			{
				base.NPC.velocity.Y = Math.Abs(base.NPC.velocity.Y) * -1f;
				base.NPC.directionY = -1;
				base.NPC.ai[0] = -1f;
			}
			else if (base.NPC.velocity.Y < 0f)
			{
				base.NPC.velocity.Y = Math.Abs(base.NPC.velocity.Y);
				base.NPC.directionY = 1;
				base.NPC.ai[0] = 1f;
			}
		}
		base.NPC.TargetClosest(faceTarget: false);
		if (Main.player[base.NPC.target].wet && !Main.player[base.NPC.target].dead && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
		{
			Vector2 val = Main.player[base.NPC.target].Center - base.NPC.Center;
			if (((Vector2)(ref val)).Length() < Main.player[base.NPC.target].Calamity().GetAbyssAggro(160f))
			{
				goto IL_033f;
			}
		}
		if (base.NPC.justHit)
		{
			goto IL_033f;
		}
		goto IL_0346;
		IL_033f:
		hasBeenHit = true;
		goto IL_0346;
		IL_0346:
		base.NPC.chaseable = hasBeenHit;
		base.NPC.rotation = base.NPC.velocity.X * 0.02f;
		if (hasBeenHit)
		{
			base.NPC.localAI[2] = 1f;
			NPC nPC = base.NPC;
			nPC.velocity *= 0.975f;
			float lungeSpeed = (CalamityWorld.death ? 24f : (CalamityWorld.revenge ? 20f : 16f));
			if (((Vector2)(ref base.NPC.velocity)).Length() > lungeSpeed * 0.4f)
			{
				base.NPC.damage = base.NPC.defDamage;
			}
			float lungeThreshold = 1.6f;
			if (base.NPC.velocity.X > 0f - lungeThreshold && base.NPC.velocity.X < lungeThreshold && base.NPC.velocity.Y > 0f - lungeThreshold && base.NPC.velocity.Y < lungeThreshold)
			{
				base.NPC.TargetClosest();
				Vector2 lungeNPCPos = default(Vector2);
				((Vector2)(ref lungeNPCPos))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
				float lungeTargetX = Main.player[base.NPC.target].position.X + (float)(Main.player[base.NPC.target].width / 2) - lungeNPCPos.X;
				float lungeTargetY = Main.player[base.NPC.target].position.Y + (float)(Main.player[base.NPC.target].height / 2) - lungeNPCPos.Y;
				float lungeTargetDist = (float)Math.Sqrt(lungeTargetX * lungeTargetX + lungeTargetY * lungeTargetY);
				lungeTargetDist = lungeSpeed / lungeTargetDist;
				lungeTargetX *= lungeTargetDist;
				lungeTargetY *= lungeTargetDist;
				base.NPC.velocity.X = lungeTargetX;
				base.NPC.velocity.Y = lungeTargetY;
			}
			return;
		}
		base.NPC.localAI[2] = 0f;
		base.NPC.velocity.X = base.NPC.velocity.X + (float)base.NPC.direction * 0.02f;
		if (base.NPC.velocity.X < -1f || base.NPC.velocity.X > 1f)
		{
			base.NPC.velocity.X = base.NPC.velocity.X * 0.95f;
		}
		if (base.NPC.ai[0] == -1f)
		{
			base.NPC.velocity.Y = base.NPC.velocity.Y - 0.01f;
			if (base.NPC.velocity.Y < -1f)
			{
				base.NPC.ai[0] = 1f;
			}
		}
		else
		{
			base.NPC.velocity.Y = base.NPC.velocity.Y + 0.01f;
			if (base.NPC.velocity.Y > 1f)
			{
				base.NPC.ai[0] = -1f;
			}
		}
		int npcTileX = (int)(base.NPC.position.X + (float)(base.NPC.width / 2)) / 16;
		int npcTileY = (int)(base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16;
		if (Main.tile[npcTileX, npcTileY - 1].LiquidAmount > 128)
		{
			if (Main.tile[npcTileX, npcTileY + 1].HasTile)
			{
				base.NPC.ai[0] = -1f;
			}
			else if (Main.tile[npcTileX, npcTileY + 2].HasTile)
			{
				base.NPC.ai[0] = -1f;
			}
		}
		else
		{
			base.NPC.ai[0] = 1f;
		}
		if ((double)base.NPC.velocity.Y > 1.2 || (double)base.NPC.velocity.Y < -1.2)
		{
			base.NPC.velocity.Y = base.NPC.velocity.Y * 0.99f;
		}
	}

	public override bool? CanBeHitByProjectile(Projectile projectile)
	{
		if (projectile.minion && !projectile.Calamity().overridesMinionDamagePrevention)
		{
			return hasBeenHit;
		}
		return null;
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += (hasBeenHit ? 0.15f : 0.075f);
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		if (!base.NPC.IsABestiaryIconDummy)
		{
			SpriteEffects effects = (SpriteEffects)(base.NPC.direction != -1);
			Main.EntitySpriteDraw(GlowTexture.Value, base.NPC.Center - Main.screenPosition + new Vector2(0f, base.NPC.gfxOffY + 4f), base.NPC.frame, Color.White * 0.5f, base.NPC.rotation, base.NPC.frame.Size() / 2f, base.NPC.scale, effects);
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.Player.Calamity().ZoneAbyssLayer3 && spawnInfo.Water)
		{
			if (!Main.remixWorld)
			{
				return SpawnCondition.CaveJellyfish.Chance * 1.2f;
			}
			return 10.8f;
		}
		return 0f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<CrushDepth>(), 180);
			target.AddBuff(22, 300);
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		LeadingConditionRule mainRule = npcLoot.DefineConditionalDropSet(DropHelper.PostLevi());
		npcLoot.Add(ItemDropRule.NormalvsExpert(888, 100, 50));
		npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<InkBomb>(), 10, 5));
		mainRule.Add(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<DepthCells>(), 2, 2, 4, 3, 6));
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
			for (int i = 0; i < 30; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("GiantSquid").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("GiantSquid2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("GiantSquid3").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("GiantSquid4").Type);
			}
		}
	}
}
