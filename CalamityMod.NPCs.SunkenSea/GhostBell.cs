using System.IO;
using CalamityMod.BiomeManagers;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Placeables.Banners;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.SunkenSea;

public class GhostBell : ModNPC
{
	public bool hasBeenHit;

	public static Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.NPC.type] = 6;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.noGravity = true;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.damage = (Main.hardMode ? 75 : 25);
		base.NPC.width = 54;
		base.NPC.height = 76;
		base.NPC.defense = (Main.hardMode ? 10 : 0);
		base.NPC.lifeMax = (Main.hardMode ? 400 : 120);
		base.NPC.knockBackResist = 0f;
		base.NPC.alpha = 100;
		base.NPC.value = Item.buyPrice(0, 0, 1);
		base.NPC.HitSound = SoundID.NPCHit25;
		base.NPC.DeathSound = SoundID.NPCDeath28;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<GhostBellBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToElectricity = false;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<SunkenSeaBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.GhostBell")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.chaseable);
		writer.Write(hasBeenHit);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.chaseable = reader.ReadBoolean();
		hasBeenHit = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.NPC.Center, 0f, (float)(255 - base.NPC.alpha) * 1.5f / 255f, (float)(255 - base.NPC.alpha) * 1.5f / 255f);
		if (base.NPC.justHit)
		{
			hasBeenHit = true;
		}
		base.NPC.chaseable = hasBeenHit;
		if (base.NPC.localAI[0] == 0f)
		{
			base.NPC.localAI[0] = 1f;
			base.NPC.velocity.Y = -6f;
			base.NPC.netUpdate = true;
		}
		if (base.NPC.wet)
		{
			base.NPC.noGravity = true;
			if (base.NPC.localAI[2] > 0f)
			{
				base.NPC.localAI[2]--;
			}
			if (base.NPC.localAI[2] <= 0f)
			{
				if (base.NPC.velocity.Y == 0f)
				{
					base.NPC.localAI[1]++;
				}
				else
				{
					base.NPC.localAI[1] = 0f;
				}
				base.NPC.velocity.Y += 0.1f;
				if (base.NPC.velocity.Y > 3f || base.NPC.localAI[1] >= 6f)
				{
					base.NPC.velocity.Y = -3f;
				}
			}
		}
		else
		{
			base.NPC.noGravity = false;
			base.NPC.velocity.Y = 2f;
			base.NPC.localAI[2] = 75f;
			base.NPC.netUpdate = true;
		}
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += 0.10000000149011612;
		base.NPC.frameCounter %= Main.npcFrameCount[base.NPC.type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.Player.Calamity().ZoneSunkenSea && spawnInfo.Water && !spawnInfo.Player.Calamity().clamity)
		{
			return SpawnCondition.CaveJellyfish.Chance * 0.9f;
		}
		return 0f;
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Vector2 val = new Vector2(base.NPC.Center.X, base.NPC.Center.Y);
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(TextureAssets.Npc[base.NPC.type].Value.Width / 2), (float)(TextureAssets.Npc[base.NPC.type].Value.Height / Main.npcFrameCount[base.NPC.type] / 2));
		Vector2 vector = val - screenPos;
		vector -= new Vector2((float)GlowTexture.Value.Width, (float)(GlowTexture.Value.Height / Main.npcFrameCount[base.NPC.type])) * 1f / 2f;
		vector += halfSizeTexture * 1f + new Vector2(0f, 4f + base.NPC.gfxOffY);
		Color color = Utils.MultiplyRGBA(new Color(127 - base.NPC.alpha, 127 - base.NPC.alpha, 127 - base.NPC.alpha, 0), Color.LightBlue);
		Main.spriteBatch.Draw(GlowTexture.Value, vector, (Rectangle?)base.NPC.frame, color, base.NPC.rotation, halfSizeTexture, 1f, spriteEffects, 0f);
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
			target.AddBuff(144, 120);
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 68, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 25; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 68, hit.HitDirection, -1f);
			}
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(1303, 30);
		npcLoot.DefineConditionalDropSet(DropHelper.PostDS()).Add(ModContent.ItemType<VoltaicJelly>(), 5);
	}
}
