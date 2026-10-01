using System;
using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.AcidRain;

public class SulphurousSkater : ModNPC
{
	public bool Flying;

	public const int JumpDelay = 64;

	public static Asset<Texture2D> GlowTexture;

	public Player Target => Main.player[base.NPC.target];

	public ref float JumpTimer => ref base.NPC.ai[0];

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 5;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		NPCID.Sets.TrailCacheLength[base.Type] = 6;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.width = 48;
		base.NPC.height = 48;
		base.NPC.damage = 48;
		base.NPC.lifeMax = 280;
		base.NPC.defense = 3;
		if (DownedBossSystem.downedPolterghast)
		{
			base.NPC.damage = 85;
			base.NPC.lifeMax = 3850;
			base.NPC.defense = 15;
		}
		base.NPC.knockBackResist = 0.8f;
		base.NPC.value = Item.buyPrice(0, 0, 4);
		base.NPC.lavaImmune = false;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = false;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<SulphurousSkaterBanner>();
		NPC nPC = base.NPC;
		int aiStyle = (base.AIType = -1);
		nPC.aiStyle = aiStyle;
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AcidRainBiome>().Type };
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(Flying);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		Flying = reader.ReadBoolean();
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.SulphurousSkater")
		});
	}

	public override void AI()
	{
		base.NPC.TargetClosest(faceTarget: false);
		if (!Flying)
		{
			JumpToDestination();
		}
		else
		{
			DoFlyMovement();
		}
	}

	public void JumpToDestination()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.knockBackResist = 0.8f;
		base.NPC.noGravity = false;
		Projectile closestBubble = SearchForNearestBubble(out var distanceToBubbele);
		Vector2 destination = Target.Center;
		if (closestBubble != null)
		{
			destination = closestBubble.Center;
		}
		if (base.NPC.wet && base.NPC.velocity.Y >= 0f)
		{
			base.NPC.velocity.Y = -3f;
		}
		if (closestBubble != null && distanceToBubbele < 200f)
		{
			base.NPC.velocity.Y += 0.2f;
			Rectangle hitbox = closestBubble.Hitbox;
			if (((Rectangle)(ref hitbox)).Intersects(base.NPC.Hitbox))
			{
				Flying = true;
				base.NPC.ForceNetUpdate();
				closestBubble.Kill();
			}
		}
		if (base.NPC.velocity.Y == 0f || base.NPC.wet)
		{
			base.NPC.TargetClosest(faceTarget: false);
			base.NPC.velocity.X *= 0.85f;
			JumpTimer++;
			float lungeForwardSpeed = 12f;
			float jumpSpeed = 4f;
			if (Collision.CanHit(base.NPC.Center, 1, 1, Target.Center, 1, 1))
			{
				lungeForwardSpeed *= 1.2f;
			}
			if (JumpTimer >= 64f)
			{
				JumpTimer = 0f;
				base.NPC.velocity.Y -= jumpSpeed;
				base.NPC.velocity.X = lungeForwardSpeed * (float)(base.NPC.Center.X - destination.X < 0f).ToDirectionInt();
				base.NPC.spriteDirection = (base.NPC.Center.X - destination.X > 0f).ToDirectionInt();
				base.NPC.ForceNetUpdate();
			}
		}
		else
		{
			base.NPC.knockBackResist = 0f;
		}
	}

	public void DoFlyMovement()
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.knockBackResist = 0.5f;
		float flySpeed = (DownedBossSystem.downedPolterghast ? 17f : 14f);
		float flyInertia = (DownedBossSystem.downedPolterghast ? 20f : 24.5f);
		if (base.NPC.WithinRange(Target.Center, 200f))
		{
			flyInertia *= 0.667f;
		}
		base.NPC.velocity = (base.NPC.velocity * flyInertia + base.NPC.SafeDirectionTo(Target.Center, Vector2.UnitY) * flySpeed) / (flyInertia + 1f);
		base.NPC.spriteDirection = (base.NPC.velocity.X < 0f).ToDirectionInt();
		NPC nPC = base.NPC;
		Vector2 center = Target.Center;
		Vector2 size = Target.Size;
		if (nPC.WithinRange(center, ((Vector2)(ref size)).Length()))
		{
			Flying = false;
			base.NPC.ForceNetUpdate();
		}
	}

	public Projectile SearchForNearestBubble(out float distanceToBubble)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		int bubbleType = ModContent.ProjectileType<SulphuricAcidBubble>();
		float minimumDistance = 2400f;
		Projectile closestBubble = null;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.type == bubbleType && !(Math.Abs(base.NPC.Center.X - p.Center.X) >= minimumDistance) && !(p.Center.Y <= base.NPC.Bottom.Y) && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, p.position, p.width, p.height))
			{
				minimumDistance = base.NPC.Distance(p.Center);
				closestBubble = p;
			}
		}
		distanceToBubble = minimumDistance;
		return closestBubble;
	}

	public override void FindFrame(int frameHeight)
	{
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.SpriteDirection = 1;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		if (!Flying && !base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.frame.Y = 0;
			return;
		}
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter >= 4.0)
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y += frameHeight;
			if (base.NPC.frame.Y >= Main.npcFrameCount[base.Type] * frameHeight)
			{
				base.NPC.frame.Y = frameHeight;
			}
		}
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		if (!base.NPC.IsABestiaryIconDummy)
		{
			CalamityGlobalNPC.DrawAfterimage(base.NPC, spriteBatch, drawColor, Color.Transparent, null, null, directioning: true, invertedDirection: true);
			CalamityGlobalNPC.DrawGlowmask(base.NPC, spriteBatch, GlowTexture.Value, invertedDirection: true);
		}
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
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 75, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0 && !Main.dedServ)
		{
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("SulphurousSkaterGore").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("SulphurousSkaterGore2").Type, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("SulphurousSkaterGore3").Type, base.NPC.scale);
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<SulphurousGrabber>(), 20);
		LeadingConditionRule mainRule = npcLoot.DefineConditionalDropSet(() => DownedBossSystem.downedPolterghast);
		mainRule.Add(ModContent.ItemType<CorrodedFossil>(), 15, 1, 3, !DownedBossSystem.downedPolterghast);
		mainRule.AddFail(ModContent.ItemType<CorrodedFossil>(), 3, 1, 3, DownedBossSystem.downedPolterghast);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Irradiated>(), 120);
		}
	}
}
