using System;
using CalamityMod.BiomeManagers.BestiaryCategories;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Sounds;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Astral;

public class Glomerling : ModNPC
{
	public static Asset<Texture2D> glowmask;

	public override void SetStaticDefaults()
	{
		if (!Main.dedServ)
		{
			glowmask = ModContent.Request<Texture2D>("CalamityMod/NPCs/Astral/GlomerlingGlow", (AssetRequestMode)2);
		}
		Main.npcFrameCount[base.Type] = 4;
		NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers();
		value.Position.Y -= 8f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		NPCID.Sets.PositiveNPCTypesExcludedFromDeathTally[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.NPC.width = 50;
		base.NPC.height = 40;
		base.NPC.aiStyle = -1;
		base.NPC.damage = 30;
		base.NPC.defense = 0;
		base.NPC.lifeMax = 160;
		base.NPC.DeathSound = CommonCalamitySounds.AstralNPCDeathSound;
		base.NPC.knockBackResist = 0.5f;
		base.NPC.noGravity = true;
		base.Banner = ModContent.NPCType<Astraglomerate>();
		base.BannerItem = ModContent.ItemType<AstraglomerateBanner>();
		if (DownedBossSystem.downedAstrumAureus)
		{
			base.NPC.damage = 50;
			base.NPC.defense = 8;
			base.NPC.knockBackResist = 0.4f;
			base.NPC.lifeMax = 240;
		}
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AstralUnderground>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Glomerling")
		});
	}

	public override void AI()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.ai[1] == 0f)
		{
			NPC nPC = base.NPC;
			nPC.velocity *= 0.97f;
			base.NPC.TargetClosest(faceTarget: false);
			if (Main.player[base.NPC.target].dead)
			{
				base.NPC.TargetClosest(faceTarget: false);
			}
			Player targ = Main.player[base.NPC.target];
			if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, targ.position, targ.width, targ.height) || Vector2.Distance(base.NPC.Center, targ.MountedCenter) < 320f)
			{
				base.NPC.ai[1] = 1f;
			}
		}
		else
		{
			CalamityGlobalNPC.DoFlyingAI(base.NPC, CalamityWorld.death ? 5f : (CalamityWorld.revenge ? 4f : 3f), CalamityWorld.death ? 0.08f : (CalamityWorld.revenge ? 0.065f : 0.05f), 200f);
			Player myTarget = Main.player[base.NPC.target];
			Vector2 toTarget = myTarget.Center - base.NPC.Center;
			if (!myTarget.dead && myTarget.active)
			{
				base.NPC.spriteDirection = (base.NPC.direction = (toTarget.X > 0f).ToDirectionInt());
			}
			else
			{
				base.NPC.spriteDirection = (base.NPC.direction = (base.NPC.velocity.X > 0f).ToDirectionInt());
			}
			if (base.NPC.spriteDirection == 1)
			{
				base.NPC.rotation += (float)Math.PI;
			}
		}
	}

	public override void FindFrame(int frameHeight)
	{
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.frameCounter += 2.0;
		}
		else
		{
			base.NPC.frameCounter += 0.05f + ((Vector2)(ref base.NPC.velocity)).Length() * 0.667f;
		}
		if (base.NPC.frameCounter >= 8.0)
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y += frameHeight;
			if (base.NPC.frame.Y > base.NPC.height * 2)
			{
				base.NPC.frame.Y = 0;
			}
		}
		Dust d = CalamityGlobalNPC.SpawnDustOnNPC(base.NPC, 30, frameHeight, ModContent.DustType<AstralOrange>(), new Rectangle(16, 8, 6, 6), Vector2.Zero, 0.3f, useSpriteDirection: true);
		if (d != null)
		{
			d.customData = 0.04f;
		}
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		spriteBatch.Draw(glowmask.Value, base.NPC.Center - screenPos + new Vector2(0f, 12f), (Rectangle?)base.NPC.frame, Color.White * 0.6f, base.NPC.rotation, new Vector2(15f, 10f), 1f, (SpriteEffects)(base.NPC.spriteDirection == 1), 0f);
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
		CalamityGlobalNPC.DoHitDust(base.NPC, hit.HitDirection, (Main.rand.Next(0, Math.Max(0, base.NPC.life)) == 0) ? 5 : ModContent.DustType<AstralEnemy>(), 1f, 3);
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		return 0f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 60);
		}
	}
}
