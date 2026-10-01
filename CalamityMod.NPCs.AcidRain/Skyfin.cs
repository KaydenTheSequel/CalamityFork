using System;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.AcidRain;

public class Skyfin : ModNPC
{
	public ref float AttackState => ref base.NPC.ai[0];

	public ref float AttackTimer => ref base.NPC.ai[1];

	public Player Target => Main.player[base.NPC.target];

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 5;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Rotation = (float)Math.PI;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.width = 46;
		base.NPC.height = 22;
		NPC nPC = base.NPC;
		int aiStyle = (base.AIType = -1);
		nPC.aiStyle = aiStyle;
		base.NPC.damage = 12;
		base.NPC.lifeMax = 50;
		base.NPC.defense = 6;
		base.NPC.knockBackResist = 1f;
		if (DownedBossSystem.downedPolterghast)
		{
			base.NPC.knockBackResist = 0.8f;
			base.NPC.damage = 88;
			base.NPC.lifeMax = 3025;
			base.NPC.defense = 18;
		}
		else if (DownedBossSystem.downedAquaticScourge)
		{
			base.NPC.damage = 38;
			base.NPC.lifeMax = 220;
		}
		base.NPC.value = Item.buyPrice(0, 0, 2);
		base.NPC.lavaImmune = false;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<SkyfinBanner>();
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
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Skyfin")
		});
	}

	public override void AI()
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.TargetClosest(faceTarget: false);
		int idealDirection = (base.NPC.velocity.X > 0f).ToDirectionInt();
		base.NPC.spriteDirection = idealDirection;
		switch ((int)AttackState)
		{
		case 0:
		{
			Vector2 flyDestination = Target.Center + new Vector2((float)(Target.Center.X < base.NPC.Center.X).ToDirectionInt() * 400f, -240f);
			Vector2 idealVelocity = base.NPC.SafeDirectionTo(flyDestination) * 10f;
			base.NPC.velocity = (base.NPC.velocity * 29f + idealVelocity) / 29f;
			base.NPC.velocity = base.NPC.velocity.MoveTowards(idealVelocity, 1.5f);
			base.NPC.rotation = base.NPC.velocity.ToRotation() + (float)(base.NPC.spriteDirection > 0).ToInt() * (float)Math.PI;
			if (base.NPC.WithinRange(flyDestination, 40f) || AttackTimer > 150f)
			{
				AttackState = 1f;
				NPC nPC = base.NPC;
				nPC.velocity *= 0.65f;
				base.NPC.netUpdate = true;
			}
			break;
		}
		case 1:
		{
			base.NPC.spriteDirection = (Target.Center.X > base.NPC.Center.X).ToDirectionInt();
			NPC nPC2 = base.NPC;
			nPC2.velocity *= 0.97f;
			base.NPC.velocity = base.NPC.velocity.MoveTowards(Vector2.Zero, 0.25f);
			base.NPC.rotation = base.NPC.rotation.AngleTowards(base.NPC.AngleTo(Target.Center) + (float)(base.NPC.spriteDirection > 0).ToInt() * (float)Math.PI, 0.2f);
			float chargeSpeed = 11.5f;
			if (DownedBossSystem.downedAquaticScourge)
			{
				chargeSpeed += 4f;
			}
			if (DownedBossSystem.downedPolterghast)
			{
				chargeSpeed += 3.5f;
			}
			if (((Vector2)(ref base.NPC.velocity)).Length() < 1.25f)
			{
				SoundEngine.PlaySound(in SoundID.DD2_WyvernDiveDown, base.NPC.Center);
				for (int i = 0; i < 36; i++)
				{
					Dust dust = Dust.NewDustPerfect(base.NPC.Center, 75);
					dust.velocity = ((float)Math.PI * 2f * (float)i / 36f).ToRotationVector2() * 6f;
					dust.scale = 1.1f;
					dust.noGravity = true;
				}
				AttackState = 2f;
				AttackTimer = 0f;
				base.NPC.velocity = base.NPC.SafeDirectionTo(Target.Center) * chargeSpeed;
				base.NPC.netUpdate = true;
			}
			break;
		}
		case 2:
		{
			float angularTurnSpeed = (float)Math.PI / 300f;
			Vector2 idealVelocity = base.NPC.SafeDirectionTo(Target.Center);
			Vector2 leftVelocity = base.NPC.velocity.RotatedBy(0f - angularTurnSpeed);
			Vector2 rightVelocity = base.NPC.velocity.RotatedBy(angularTurnSpeed);
			if (leftVelocity.AngleBetween(idealVelocity) < rightVelocity.AngleBetween(idealVelocity))
			{
				base.NPC.velocity = leftVelocity;
			}
			else
			{
				base.NPC.velocity = rightVelocity;
			}
			base.NPC.rotation = base.NPC.velocity.ToRotation() + (float)(base.NPC.spriteDirection > 0).ToInt() * (float)Math.PI;
			if (AttackTimer > 50f)
			{
				AttackState = 0f;
				AttackTimer = 0f;
				base.NPC.velocity = Vector2.Lerp(base.NPC.velocity, -Vector2.UnitY * 8f, 0.14f);
				base.NPC.netUpdate = true;
			}
			break;
		}
		}
		AttackTimer++;
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

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<SulphuricScale>(), 2, 1, 3);
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
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("SkyfinGore").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("SkyfinGore2").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("SkyfinGore3").Type, base.NPC.scale);
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
