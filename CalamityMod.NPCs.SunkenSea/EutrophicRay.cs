using System.IO;
using CalamityMod.BiomeManagers;
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

public class EutrophicRay : ModNPC
{
	public bool hasBeenHit;

	public static Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.NPC.type] = 5;
		NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers();
		value.Position.X += 24f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.damage = (Main.hardMode ? 50 : 20);
		base.NPC.width = 116;
		base.NPC.height = 36;
		base.NPC.defense = (Main.hardMode ? 15 : 5);
		base.NPC.lifeMax = (Main.hardMode ? 500 : 200);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.value = Item.buyPrice(0, 0, 1);
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath55;
		base.NPC.knockBackResist = 0.5f;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<EutrophicRayBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<SunkenSeaBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.EutrophicRay")
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
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.TargetClosest();
		if (base.NPC.velocity.X > 0.25f)
		{
			base.NPC.spriteDirection = 1;
		}
		else if (base.NPC.velocity.X < 0.25f)
		{
			base.NPC.spriteDirection = -1;
		}
		if (base.NPC.justHit && !hasBeenHit)
		{
			hasBeenHit = true;
			base.NPC.damage = base.NPC.defDamage;
			base.NPC.noTileCollide = true;
			base.NPC.noGravity = true;
			if (base.NPC.Center.X < Main.player[base.NPC.target].Center.X)
			{
				base.NPC.ai[0] = 1f;
			}
			else
			{
				base.NPC.ai[0] = 2f;
			}
		}
		base.NPC.chaseable = hasBeenHit;
		if (hasBeenHit)
		{
			float AccelerationY = (Main.hardMode ? 0.4f : 0.2f);
			float MaxSpeedY = (Main.hardMode ? 4f : 2.5f);
			float Rotation = 0f;
			if (base.NPC.Center.Y + 0.4f > Main.player[base.NPC.target].Center.Y)
			{
				base.NPC.velocity.Y -= AccelerationY;
				if (base.NPC.velocity.Y < 0f - MaxSpeedY)
				{
					base.NPC.velocity.Y = 0f - MaxSpeedY;
				}
			}
			else if (base.NPC.Center.Y - 0.4f < Main.player[base.NPC.target].Center.Y)
			{
				base.NPC.velocity.Y += AccelerationY;
				if (base.NPC.velocity.Y > MaxSpeedY)
				{
					base.NPC.velocity.Y = MaxSpeedY;
				}
			}
			float AccelerationX = (Main.hardMode ? 0.4f : 0.25f);
			float MaxSpeedX = (Main.hardMode ? 6f : 4f);
			if (base.NPC.ai[0] == 1f)
			{
				Rotation = -0.05f;
				base.NPC.velocity.X -= AccelerationX;
				if (base.NPC.velocity.X < 0f - MaxSpeedX)
				{
					base.NPC.velocity.X = 0f - MaxSpeedX;
				}
				if (base.NPC.Center.X + 300f < Main.player[base.NPC.target].Center.X)
				{
					base.NPC.ai[0] = 2f;
				}
			}
			else if (base.NPC.ai[0] == 2f)
			{
				Rotation = 0.05f;
				base.NPC.velocity.X += AccelerationX;
				if (base.NPC.velocity.X > MaxSpeedX)
				{
					base.NPC.velocity.X = MaxSpeedX;
				}
				if (base.NPC.Center.X - 300f > Main.player[base.NPC.target].Center.X)
				{
					base.NPC.ai[0] = 1f;
				}
			}
			base.NPC.rotation = base.NPC.velocity.Y * Rotation;
			if (base.NPC.rotation < -0.1f)
			{
				base.NPC.rotation = -0.1f;
			}
			if (base.NPC.rotation > 0.1f)
			{
				base.NPC.rotation = 0.1f;
			}
		}
		else
		{
			base.NPC.damage = 0;
		}
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += ((hasBeenHit || base.NPC.IsABestiaryIconDummy) ? 0.15f : 0f);
		base.NPC.frameCounter %= Main.npcFrameCount[base.NPC.type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
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

	public override bool? CanBeHitByProjectile(Projectile projectile)
	{
		if (projectile.minion && !projectile.Calamity().overridesMinionDamagePrevention)
		{
			return hasBeenHit;
		}
		return null;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.Player.Calamity().ZoneSunkenSea && spawnInfo.Water && !spawnInfo.Player.Calamity().clamity)
		{
			return SpawnCondition.CaveJellyfish.Chance * 0.6f;
		}
		return 0f;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
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
			if (Main.netMode != 2)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("RayGore1").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("RayGore2").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("RayGore3").Type);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("RayGore4").Type);
			}
		}
	}
}
