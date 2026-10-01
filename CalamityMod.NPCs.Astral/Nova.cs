using System;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Sounds;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Astral;

public class Nova : ModNPC
{
	public static Asset<Texture2D> glowmask;

	private float travelAcceleration = 0.2f;

	private float targetTime = 120f;

	private const float waitBeforeTravel = 20f;

	private const float maxTravelTime = 300f;

	private const float slowdown = 0.84f;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 8;
		if (!Main.dedServ)
		{
			glowmask = ModContent.Request<Texture2D>("CalamityMod/NPCs/Astral/NovaGlow", (AssetRequestMode)2);
		}
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Scale = 0.64f;
		nPCBestiaryDrawModifiers.PortraitPositionXOverride = 10f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 8f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.X += 15f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.width = 78;
		base.NPC.height = 50;
		base.NPC.damage = 45;
		base.NPC.defense = 26;
		base.NPC.lifeMax = 300;
		base.NPC.DeathSound = CommonCalamitySounds.AstralNPCDeathSound;
		base.NPC.noGravity = true;
		base.NPC.knockBackResist = 0.5f;
		base.NPC.value = Item.buyPrice(0, 0, 5);
		base.NPC.aiStyle = -1;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<NovaBanner>();
		if (DownedBossSystem.downedAstrumAureus)
		{
			base.NPC.damage = 75;
			base.NPC.defense = 36;
			base.NPC.knockBackResist = 0.4f;
			base.NPC.lifeMax = 450;
		}
		if (CalamityWorld.revenge)
		{
			travelAcceleration = 0.3f;
			targetTime = 90f;
		}
		if (CalamityWorld.death)
		{
			travelAcceleration = 0.4f;
			targetTime = 60f;
		}
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AstralInfectionBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Nova")
		});
	}

	public override void FindFrame(int frameHeight)
	{
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.frameCounter++;
		if (base.NPC.ai[3] >= 0f)
		{
			if (base.NPC.frameCounter >= 8.0)
			{
				base.NPC.frameCounter = 0.0;
				base.NPC.frame.Y += frameHeight;
				if (base.NPC.frame.Y >= frameHeight * 4)
				{
					base.NPC.frame.Y = 0;
				}
			}
		}
		else if (base.NPC.frameCounter >= 7.0)
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y += frameHeight;
			if (base.NPC.frame.Y >= frameHeight * 8)
			{
				base.NPC.frame.Y = frameHeight * 4;
			}
		}
		Dust d = CalamityGlobalNPC.SpawnDustOnNPC(base.NPC, 114, frameHeight, ModContent.DustType<AstralOrange>(), new Rectangle(78, 34, 36, 18), Vector2.Zero, 0.45f, useSpriteDirection: true);
		if (d != null)
		{
			d.customData = 0.04f;
		}
	}

	public override void AI()
	{
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		Player target = Main.player[base.NPC.target];
		if (base.NPC.ai[3] >= 0f)
		{
			base.NPC.damage = 0;
			CalamityGlobalNPC.DoFlyingAI(base.NPC, CalamityWorld.death ? 8.5f : (CalamityWorld.revenge ? 7f : 5.5f), CalamityWorld.death ? 0.055f : (CalamityWorld.revenge ? 0.045f : 0.035f), 400f, 150f, shouldAttackTarget: false);
			if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, target.position, target.width, target.height))
			{
				base.NPC.ai[3]++;
			}
			else
			{
				base.NPC.ai[3] = 0f;
			}
			Vector2 between = target.Center - base.NPC.Center;
			int random = (CalamityWorld.death ? 90 : (CalamityWorld.revenge ? 135 : 180));
			if (((Vector2)(ref between)).Length() > 150f && base.NPC.ai[3] >= targetTime && Main.rand.NextBool(random))
			{
				base.NPC.ai[3] = -1f;
			}
			return;
		}
		base.NPC.ai[3]--;
		Vector2 between2 = target.Center - base.NPC.Center;
		if (base.NPC.ai[3] < -20f)
		{
			if (base.NPC.collideX || base.NPC.collideY || base.NPC.ai[3] < -300f)
			{
				Explode();
			}
			NPC nPC = base.NPC;
			nPC.velocity += new Vector2(base.NPC.ai[1], base.NPC.ai[2]) * travelAcceleration;
			if (((Vector2)(ref base.NPC.velocity)).Length() > 4f)
			{
				base.NPC.damage = base.NPC.defDamage;
			}
			else
			{
				base.NPC.damage = 0;
			}
			base.NPC.rotation = base.NPC.velocity.ToRotation();
		}
		else if (base.NPC.ai[3] == -20f)
		{
			base.NPC.damage = 0;
			((Vector2)(ref between2)).Normalize();
			base.NPC.ai[1] = between2.X;
			base.NPC.ai[2] = between2.Y;
			base.NPC.rotation = between2.ToRotation();
			base.NPC.velocity = Vector2.Zero;
		}
		else
		{
			base.NPC.damage = 0;
			NPC nPC2 = base.NPC;
			nPC2.velocity *= 0.84f;
			base.NPC.rotation = between2.ToRotation();
		}
		base.NPC.rotation += (float)Math.PI;
	}

	private void Explode()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item14, base.NPC.Center);
		Vector2 center = base.NPC.Center;
		base.NPC.width = 200;
		base.NPC.height = 200;
		base.NPC.Center = center;
		Rectangle myRect = base.NPC.getRect();
		if (Main.netMode != 1)
		{
			ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Player player = enumerator.Current;
				Rectangle rect = player.getRect();
				if (((Rectangle)(ref rect)).Intersects(myRect))
				{
					int direction = ((!(base.NPC.Center.X - player.Center.X < 0f)) ? 1 : (-1));
					player.Hurt(PlayerDeathReason.ByNPC(base.NPC.whoAmI), base.NPC.damage, direction);
				}
			}
		}
		base.NPC.ai[3] = -20000f;
		base.NPC.value = 0f;
		base.NPC.extraValue = 0;
		if (Main.netMode != 1)
		{
			base.NPC.StrikeInstantKill();
		}
		int size = 30;
		Vector2 off = default(Vector2);
		((Vector2)(ref off))._002Ector((float)size / -2f);
		for (int i = 0; i < 45; i++)
		{
			int dust = Dust.NewDust(base.NPC.Center - off, size, size, ModContent.DustType<AstralEnemy>(), Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-3f, 3f), 0, default(Color), Main.rand.NextFloat(1f, 2f));
			Dust obj = Main.dust[dust];
			obj.velocity *= 1.4f;
		}
		for (int j = 0; j < 15; j++)
		{
			int dust2 = Dust.NewDust(base.NPC.Center - off, size, size, 31, 0f, 0f, 100, default(Color), 1.7f);
			Dust obj2 = Main.dust[dust2];
			obj2.velocity *= 1.4f;
		}
		for (int k = 0; k < 27; k++)
		{
			int dust3 = Dust.NewDust(base.NPC.Center - off, size, size, 6, 0f, 0f, 100, default(Color), 2.4f);
			Main.dust[dust3].noGravity = true;
			Dust obj3 = Main.dust[dust3];
			obj3.velocity *= 5f;
			dust3 = Dust.NewDust(base.NPC.Center - off, size, size, 6, 0f, 0f, 100, default(Color), 1.6f);
			Dust obj4 = Main.dust[dust3];
			obj4.velocity *= 3f;
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
		CalamityGlobalNPC.DoHitDust(base.NPC, hit.HitDirection, (Main.rand.Next(0, Math.Max(0, base.NPC.life)) == 0) ? 5 : ModContent.DustType<AstralEnemy>(), 1f, 3, 40);
		if (base.NPC.life <= 0 && !Main.dedServ)
		{
			for (int i = 0; i < 7; i++)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.Center, base.NPC.velocity * 0.3f, base.Mod.Find<ModGore>("NovaGore" + i).Type);
			}
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		LeadingConditionRule leadingConditionRule = npcLoot.DefineConditionalDropSet((DropAttemptInfo info) => info.npc.ai[3] <= -10000f);
		leadingConditionRule.OnFailedConditions(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<StarblightSoot>(), 1, 2, 3, 3, 4));
		leadingConditionRule.OnFailedConditions(ItemDropRule.ByCondition(DropHelper.If(() => DownedBossSystem.downedAstrumAureus), ModContent.ItemType<StellarCannon>(), 10));
		leadingConditionRule.Add(ModContent.ItemType<GloriousEnd>(), 10);
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		if (!base.NPC.IsABestiaryIconDummy)
		{
			Vector2 drawPosition = base.NPC.Center - screenPos - new Vector2(0f, base.NPC.scale * 4f);
			spriteBatch.Draw(glowmask.Value, drawPosition, (Rectangle?)base.NPC.frame, Color.White * 0.75f, base.NPC.rotation, new Vector2(57f, 37f), base.NPC.scale, (SpriteEffects)(base.NPC.spriteDirection == 1), 0f);
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (CalamityGlobalNPC.AnyEvents(spawnInfo.Player))
		{
			return 0f;
		}
		if (spawnInfo.Player.InAstral(1))
		{
			return 0.19f;
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
}
