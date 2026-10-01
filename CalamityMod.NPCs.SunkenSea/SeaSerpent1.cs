using System;
using CalamityMod.BiomeManagers;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Weapons.Magic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.SunkenSea;

public class SeaSerpent1 : ModNPC
{
	public const int maxLength = 9;

	public float speed = 3f;

	public float turnSpeed = 0.0625f;

	private bool TailSpawned;

	public override void SetStaticDefaults()
	{
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.PortraitPositionXOverride = 40f;
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 20f;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.Y += 20f;
		value.Position.X += 40f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 50;
		base.NPC.width = 50;
		base.NPC.height = 24;
		base.NPC.defense = 10;
		base.NPC.lifeMax = 3000;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.value = Item.buyPrice(0, 0, 20);
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.NPC.netAlways = true;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<SeaSerpentBanner>();
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
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.SeaSerpent")
		});
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_0534: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_056d: Unknown result type (might be due to invalid IL or missing references)
		//IL_068a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0808: Unknown result type (might be due to invalid IL or missing references)
		//IL_0813: Unknown result type (might be due to invalid IL or missing references)
		//IL_0709: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0777: Unknown result type (might be due to invalid IL or missing references)
		//IL_077e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0783: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b3: Unknown result type (might be due to invalid IL or missing references)
		if (Framing.GetTileSafely(base.NPC.Center.ToTileCoordinates()).HasUnactuatedTile && base.NPC.Distance(Main.player[base.NPC.target].Center) < 800f && Main.rand.NextBool())
		{
			Dust dust = Dust.NewDustDirect(base.NPC.position, base.NPC.width, base.NPC.height, 204, 0f, 0f, 150, default(Color), 0.3f);
			dust.fadeIn = 0.75f;
			dust.velocity *= 0.1f;
			dust.noLight = true;
		}
		Lighting.AddLight(base.NPC.Center, 0f, (float)(255 - base.NPC.alpha) * 0.3f / 255f, (float)(255 - base.NPC.alpha) * 0.3f / 255f);
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[2];
		}
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead)
		{
			base.NPC.TargetClosest();
		}
		if (Main.netMode != 1 && !TailSpawned && base.NPC.ai[0] == 0f)
		{
			int Previous = base.NPC.whoAmI;
			for (int segment = 0; segment < 9; segment++)
			{
				int lol = 0;
				switch (segment)
				{
				case 0:
				case 1:
				case 4:
				case 5:
					lol = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + base.NPC.width / 2, (int)base.NPC.position.Y + base.NPC.height / 2, ModContent.NPCType<SeaSerpent2>(), base.NPC.whoAmI);
					break;
				case 2:
				case 3:
				case 6:
					lol = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + base.NPC.width / 2, (int)base.NPC.position.Y + base.NPC.height / 2, ModContent.NPCType<SeaSerpent3>(), base.NPC.whoAmI);
					break;
				case 7:
					lol = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + base.NPC.width / 2, (int)base.NPC.position.Y + base.NPC.height / 2, ModContent.NPCType<SeaSerpent4>(), base.NPC.whoAmI);
					break;
				case 8:
					lol = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.position.X + base.NPC.width / 2, (int)base.NPC.position.Y + base.NPC.height / 2, ModContent.NPCType<SeaSerpent5>(), base.NPC.whoAmI);
					break;
				}
				Main.npc[lol].realLife = base.NPC.whoAmI;
				Main.npc[lol].ai[2] = base.NPC.whoAmI;
				Main.npc[lol].ai[1] = Previous;
				Main.npc[Previous].ai[0] = lol;
				NetMessage.SendData(23, -1, -1, null, lol);
				Previous = lol;
			}
			TailSpawned = true;
		}
		if (base.NPC.velocity.X < 0f)
		{
			base.NPC.spriteDirection = -1;
		}
		else if (base.NPC.velocity.X > 0f)
		{
			base.NPC.spriteDirection = 1;
		}
		if (Main.player[base.NPC.target].dead)
		{
			base.NPC.TargetClosest(faceTarget: false);
		}
		base.NPC.alpha -= 42;
		if (base.NPC.alpha < 0)
		{
			base.NPC.alpha = 0;
		}
		if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 5600f)
		{
			base.NPC.TargetClosest(faceTarget: false);
			if (Vector2.Distance(Main.player[base.NPC.target].Center, base.NPC.Center) > 5600f)
			{
				base.NPC.active = false;
			}
		}
		float currentSpeed = speed;
		float currentTurnSpeed = turnSpeed;
		Vector2 segmentPosition = default(Vector2);
		((Vector2)(ref segmentPosition))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
		float targetXDist = Main.player[base.NPC.target].position.X + (float)(Main.player[base.NPC.target].width / 2);
		float targetYDist = Main.player[base.NPC.target].position.Y + (float)(Main.player[base.NPC.target].height / 2);
		if ((double)base.NPC.life > (double)base.NPC.lifeMax * 0.99)
		{
			targetYDist += 300f;
			if (Math.Abs(base.NPC.Center.X - Main.player[base.NPC.target].Center.X) < 250f)
			{
				targetXDist = ((!(base.NPC.velocity.X > 0f)) ? (Main.player[base.NPC.target].Center.X - 300f) : (Main.player[base.NPC.target].Center.X + 300f));
			}
		}
		else
		{
			currentSpeed *= 1.5f;
			currentTurnSpeed *= 1.5f;
		}
		float maxCurrentSpeed = currentSpeed * 1.3f;
		float minCurrentSpeed = currentSpeed * 0.7f;
		float speedCompare = ((Vector2)(ref base.NPC.velocity)).Length();
		if (speedCompare > 0f)
		{
			if (speedCompare > maxCurrentSpeed)
			{
				((Vector2)(ref base.NPC.velocity)).Normalize();
				NPC nPC = base.NPC;
				nPC.velocity *= maxCurrentSpeed;
			}
			else if (speedCompare < minCurrentSpeed)
			{
				((Vector2)(ref base.NPC.velocity)).Normalize();
				NPC nPC2 = base.NPC;
				nPC2.velocity *= minCurrentSpeed;
			}
		}
		targetXDist = (int)(targetXDist / 16f) * 16;
		targetYDist = (int)(targetYDist / 16f) * 16;
		segmentPosition.X = (int)(segmentPosition.X / 16f) * 16;
		segmentPosition.Y = (int)(segmentPosition.Y / 16f) * 16;
		targetXDist -= segmentPosition.X;
		targetYDist -= segmentPosition.Y;
		float targetDistance = (float)Math.Sqrt(targetXDist * targetXDist + targetYDist * targetYDist);
		float absoluteTargetX = Math.Abs(targetXDist);
		float absoluteTargetY = Math.Abs(targetYDist);
		float timeToReachTarget = currentSpeed / targetDistance;
		targetXDist *= timeToReachTarget;
		targetYDist *= timeToReachTarget;
		if ((base.NPC.velocity.X > 0f && targetXDist > 0f) || (base.NPC.velocity.X < 0f && targetXDist < 0f) || (base.NPC.velocity.Y > 0f && targetYDist > 0f) || (base.NPC.velocity.Y < 0f && targetYDist < 0f))
		{
			if (base.NPC.velocity.X < targetXDist)
			{
				base.NPC.velocity.X = base.NPC.velocity.X + currentTurnSpeed;
			}
			else if (base.NPC.velocity.X > targetXDist)
			{
				base.NPC.velocity.X = base.NPC.velocity.X - currentTurnSpeed;
			}
			if (base.NPC.velocity.Y < targetYDist)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y + currentTurnSpeed;
			}
			else if (base.NPC.velocity.Y > targetYDist)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y - currentTurnSpeed;
			}
			if ((double)Math.Abs(targetYDist) < (double)currentSpeed * 0.2 && ((base.NPC.velocity.X > 0f && targetXDist < 0f) || (base.NPC.velocity.X < 0f && targetXDist > 0f)))
			{
				if (base.NPC.velocity.Y > 0f)
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y + currentTurnSpeed * 2f;
				}
				else
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y - currentTurnSpeed * 2f;
				}
			}
			if ((double)Math.Abs(targetXDist) < (double)currentSpeed * 0.2 && ((base.NPC.velocity.Y > 0f && targetYDist < 0f) || (base.NPC.velocity.Y < 0f && targetYDist > 0f)))
			{
				if (base.NPC.velocity.X > 0f)
				{
					base.NPC.velocity.X = base.NPC.velocity.X + currentTurnSpeed * 2f;
				}
				else
				{
					base.NPC.velocity.X = base.NPC.velocity.X - currentTurnSpeed * 2f;
				}
			}
		}
		else if (absoluteTargetX > absoluteTargetY)
		{
			if (base.NPC.velocity.X < targetXDist)
			{
				base.NPC.velocity.X = base.NPC.velocity.X + currentTurnSpeed * 1.1f;
			}
			else if (base.NPC.velocity.X > targetXDist)
			{
				base.NPC.velocity.X = base.NPC.velocity.X - currentTurnSpeed * 1.1f;
			}
			if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)currentSpeed * 0.5)
			{
				if (base.NPC.velocity.Y > 0f)
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y + currentTurnSpeed;
				}
				else
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y - currentTurnSpeed;
				}
			}
		}
		else
		{
			if (base.NPC.velocity.Y < targetYDist)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y + currentTurnSpeed * 1.1f;
			}
			else if (base.NPC.velocity.Y > targetYDist)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y - currentTurnSpeed * 1.1f;
			}
			if ((double)(Math.Abs(base.NPC.velocity.X) + Math.Abs(base.NPC.velocity.Y)) < (double)currentSpeed * 0.5)
			{
				if (base.NPC.velocity.X > 0f)
				{
					base.NPC.velocity.X = base.NPC.velocity.X + currentTurnSpeed;
				}
				else
				{
					base.NPC.velocity.X = base.NPC.velocity.X - currentTurnSpeed;
				}
			}
		}
		base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + 1.57f;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (Main.hardMode && spawnInfo.Player.Calamity().ZoneSunkenSea && spawnInfo.Water && !NPC.AnyNPCs(ModContent.NPCType<SeaSerpent1>()) && !spawnInfo.Player.Calamity().clamity && !spawnInfo.PlayerSafe)
		{
			return SpawnCondition.CaveJellyfish.Chance * 0.3f;
		}
		return 0f;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ModContent.ItemType<Serpentine>(), 4);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 37, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 10; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 37, hit.HitDirection, -1f);
			}
			if (Main.netMode != 2)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("SeaSerpentGore1").Type);
			}
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.IsABestiaryIconDummy)
		{
			Texture2D texture = TextureAssets.Npc[base.Type].Value;
			Texture2D[] bodyTextures = (Texture2D[])(object)new Texture2D[4]
			{
				TextureAssets.Npc[ModContent.NPCType<SeaSerpent2>()].Value,
				TextureAssets.Npc[ModContent.NPCType<SeaSerpent3>()].Value,
				TextureAssets.Npc[ModContent.NPCType<SeaSerpent4>()].Value,
				TextureAssets.Npc[ModContent.NPCType<SeaSerpent5>()].Value
			};
			base.NPC.frame = texture.Frame();
			float offset = -0.2f;
			float rotationStrength = 0.3f;
			int segmentSpacing = 22;
			int animationSpeed = 2;
			float range = 10f;
			float wormTimer = base.NPC.Calamity().bestiaryWormTimer;
			for (int i = 6; i > 0; i--)
			{
				float bodyOffset = ((i == 1) ? ((float)(i * segmentSpacing) * 0.4f) : ((float)(i * segmentSpacing) - (float)segmentSpacing * 0.5f));
				Texture2D toUse = ((i == 2 || i == 3) ? bodyTextures[1] : bodyTextures[0]);
				spriteBatch.Draw(toUse, base.NPC.position + new Vector2(bodyOffset, MathF.Sin((wormTimer + offset * (float)i) * (float)animationSpeed) * range), (Rectangle?)toUse.Frame(), base.NPC.GetAlpha(drawColor), base.NPC.rotation - (float)Math.PI / 2f - MathF.Cos((wormTimer + offset * (float)i) * (float)animationSpeed) * ((float)Math.PI / 4f) * rotationStrength, toUse.Size() / 2f, base.NPC.scale, (SpriteEffects)0, 0f);
			}
			spriteBatch.Draw(texture, base.NPC.position + new Vector2(0f, MathF.Sin(wormTimer * (float)animationSpeed) * range), (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation - (float)Math.PI / 2f - MathF.Cos(wormTimer * (float)animationSpeed) * ((float)Math.PI / 4f) * rotationStrength, new Vector2((float)texture.Width * 0.5f, (float)texture.Height), base.NPC.scale, (SpriteEffects)0, 0f);
			return false;
		}
		return true;
	}
}
