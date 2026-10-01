using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Weapons.Magic;
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

public class MirageJelly : ModNPC
{
	private bool teleporting;

	private bool rephasing;

	private bool hasBeenHit;

	public static Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 7;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 20f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.Y += 30f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.damage = 100;
		base.NPC.width = 78;
		base.NPC.height = 170;
		base.NPC.defense = 30;
		base.NPC.lifeMax = 6000;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.value = Item.buyPrice(0, 0, 25);
		base.NPC.HitSound = SoundID.NPCHit25;
		base.NPC.DeathSound = SoundID.NPCDeath28;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<MirageJellyBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = false;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AbyssLayer3Biome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.MirageJelly")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(hasBeenHit);
		writer.Write(teleporting);
		writer.Write(rephasing);
		writer.Write(base.NPC.chaseable);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		hasBeenHit = reader.ReadBoolean();
		teleporting = reader.ReadBoolean();
		rephasing = reader.ReadBoolean();
		base.NPC.chaseable = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.TargetClosest();
		Player player = Main.player[base.NPC.target];
		NPC nPC = base.NPC;
		nPC.velocity *= 0.985f;
		if (base.NPC.velocity.Y > -0.3f)
		{
			base.NPC.velocity.Y = -3f;
		}
		if (base.NPC.justHit)
		{
			if (Main.rand.NextBool(10))
			{
				teleporting = true;
			}
			hasBeenHit = true;
		}
		if (base.NPC.ai[0] == 0f)
		{
			base.NPC.chaseable = true;
			if (Main.netMode == 1 || !teleporting)
			{
				return;
			}
			teleporting = false;
			base.NPC.TargetClosest();
			int teleportTries = 0;
			int teleportTileX;
			int teleportTileY;
			while (true)
			{
				teleportTries++;
				teleportTileX = (int)player.Center.X / 16;
				teleportTileY = (int)player.Center.Y / 16;
				int min = 6;
				int max = 9;
				teleportTileX = ((!Main.rand.NextBool()) ? (teleportTileX - Main.rand.Next(min, max)) : (teleportTileX + Main.rand.Next(min, max)));
				min = 11;
				max = 26;
				teleportTileY += Main.rand.Next(min, max);
				if (!WorldGen.SolidTile(teleportTileX, teleportTileY) && Collision.CanHit(new Vector2((float)(teleportTileX * 16), (float)(teleportTileY * 16)), 1, 1, player.position, player.width, player.height) && Main.tile[teleportTileX, teleportTileY].LiquidAmount > 204)
				{
					break;
				}
				if (teleportTries > 100)
				{
					return;
				}
			}
			base.NPC.ai[0] = 1f;
			base.NPC.ai[1] = teleportTileX;
			base.NPC.ai[2] = teleportTileY;
			base.NPC.netUpdate = true;
		}
		else if (base.NPC.ai[0] == 1f)
		{
			base.NPC.damage = 0;
			base.NPC.chaseable = false;
			base.NPC.alpha += 5;
			if (base.NPC.alpha >= 255)
			{
				base.NPC.alpha = 255;
				base.NPC.position.X = base.NPC.ai[1] * 16f - (float)(base.NPC.width / 2);
				base.NPC.position.Y = base.NPC.ai[2] * 16f - (float)(base.NPC.height / 2);
				base.NPC.ai[0] = 2f;
				base.NPC.netUpdate = true;
			}
		}
		else if (base.NPC.ai[0] == 2f)
		{
			base.NPC.alpha -= 5;
			if (base.NPC.alpha <= 0)
			{
				base.NPC.damage = (Main.expertMode ? 200 : 100);
				base.NPC.chaseable = true;
				base.NPC.alpha = 0;
				base.NPC.ai[0] = 0f;
				base.NPC.netUpdate = true;
			}
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

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += (hasBeenHit ? 0.15f : 0.1f);
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.Player.Calamity().ZoneAbyssLayer3 && spawnInfo.Water)
		{
			if (!Main.remixWorld)
			{
				return SpawnCondition.CaveJellyfish.Chance * 0.6f;
			}
			return 5.4f;
		}
		return 0f;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.AddIf(() => NPC.downedBoss3, ModContent.ItemType<AbyssShocker>(), 10);
		npcLoot.DefineConditionalDropSet(DropHelper.PostLevi()).Add(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<DepthCells>(), 2, 5, 7, 10, 14));
		npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<LifeJelly>(), 7, 5));
		npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<CleansingJelly>(), 7, 5));
		npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<VitalJelly>(), 7, 5));
		npcLoot.Add(1303, 10);
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		Vector2 npcCenter = base.NPC.Center;
		Rectangle tentacleHitbox = default(Rectangle);
		((Rectangle)(ref tentacleHitbox))._002Ector((int)(npcCenter.X - (float)base.NPC.width / 4f), (int)npcCenter.Y, base.NPC.width / 2, base.NPC.height / 2);
		Rectangle targetHitbox = target.Hitbox;
		return ((Rectangle)(ref targetHitbox)).Intersects(tentacleHitbox);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(70, 240);
			target.AddBuff(144, 120);
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 15; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
		}
	}
}
