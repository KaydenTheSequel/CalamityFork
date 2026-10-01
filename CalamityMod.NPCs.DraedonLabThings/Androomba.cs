using CalamityMod.BiomeManagers;
using CalamityMod.Items.Critters;
using CalamityMod.Items.Placeables.Banners;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.DraedonLabThings;

public class Androomba : ModNPC
{
	public static Asset<Texture2D> GlowTexture;

	public SoundStyle HurrySound = new SoundStyle("CalamityMod/Sounds/Custom/WulfrumDroidHurry", 2)
	{
		PitchVariance = 0.3f
	};

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 22;
		NPCID.Sets.CountsAsCritter[base.Type] = true;
		Main.npcCatchable[base.Type] = true;
		NPCID.Sets.NormalGoldCritterBestiaryPriority.Add(base.Type);
		NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers();
		value.Position.Y += 18f;
		value.PortraitPositionYOverride = 38f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "_Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.damage = 0;
		base.NPC.width = 40;
		base.NPC.height = 16;
		base.NPC.lifeMax = 80;
		base.NPC.knockBackResist = 0f;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = false;
		base.NPC.chaseable = false;
		base.NPC.HitSound = SoundID.NPCHit4;
		base.NPC.DeathSound = SoundID.NPCDeath44;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<AndroombaBanner>();
		base.NPC.rarity = 2;
		base.NPC.catchItem = (short)ModContent.ItemType<AndroombaItem>();
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<ArsenalLabBiome>().Type };
		base.DrawOffsetY = -17f;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Androomba")
		});
	}

	public override void AI()
	{
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.velocity.Y = MathHelper.Clamp(base.NPC.velocity.Y + 0.4f, -15f, 15f);
		base.NPC.spriteDirection = (int)base.NPC.ai[2];
		float num = base.NPC.ai[0];
		if (num != 0f)
		{
			if (num != 1f && num != 3f)
			{
				if (num != 2f)
				{
					if (num == 4f)
					{
						base.NPC.velocity = Vector2.Zero;
						base.NPC.ai[1]++;
						if (base.NPC.ai[1] > 1200f)
						{
							base.NPC.active = false;
						}
						base.NPC.damage = 10;
					}
				}
				else
				{
					base.NPC.ai[1]++;
					base.NPC.velocity.X = base.NPC.ai[2] * 4f;
					if (base.NPC.collideX)
					{
						ChangeAI(3);
					}
					else if (base.NPC.ai[1] > 480f)
					{
						for (int i = 0; i < 255; i++)
						{
							if (!Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[i].position, Main.player[i].width, Main.player[i].height))
							{
								ChangeAI(0);
							}
						}
					}
				}
			}
			else
			{
				base.NPC.velocity.X = 0f;
				if (base.NPC.ai[1] == 0f)
				{
					if (base.NPC.ai[0] == 1f)
					{
						SoundEngine.PlaySound(in HurrySound, base.NPC.Center);
					}
					if (base.NPC.ai[0] == 3f)
					{
						base.NPC.catchItem = 0;
						SoundEngine.PlaySound(base.NPC.DeathSound, (Vector2?)base.NPC.Center);
					}
				}
				base.NPC.ai[1]++;
			}
		}
		else
		{
			if (base.NPC.ai[1] == 0f)
			{
				base.NPC.ai[2] = ((!Main.rand.NextBool()) ? 1 : (-1));
			}
			base.NPC.ai[1]++;
			base.NPC.velocity.X = base.NPC.ai[2] * 2f;
			if (!Collision.CanHit(base.NPC.Center - Vector2.UnitX * base.NPC.ai[2] * 8f, 2, 2, base.NPC.Center + Vector2.UnitX * base.NPC.ai[2] * 32f, 8, 8))
			{
				base.NPC.ai[2] *= -1f;
			}
			ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Player player = enumerator.Current;
				if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, player.position, player.width, player.height))
				{
					ChangeAI(1);
				}
			}
		}
		if (Main.netMode != 1 && Main.BestiaryTracker.Kills.GetKillCount(base.NPC) <= 0)
		{
			Main.BestiaryTracker.Kills.RegisterKill(base.NPC);
		}
	}

	public void ChangeAI(int phase)
	{
		base.NPC.ai[0] = phase;
		base.NPC.ai[1] = 0f;
		base.NPC.netUpdate = true;
	}

	public override bool? CanBeHitByItem(Player player, Item item)
	{
		return null;
	}

	public override bool? CanBeHitByProjectile(Projectile projectile)
	{
		return null;
	}

	public override void FindFrame(int frameHeight)
	{
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter > 6.0)
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y += frameHeight;
		}
		if (base.NPC.ai[0] == 0f || base.NPC.IsABestiaryIconDummy)
		{
			if (base.NPC.frame.Y > frameHeight * 4)
			{
				base.NPC.frameCounter = 0.0;
				base.NPC.frame.Y = 1;
			}
		}
		else if (base.NPC.ai[0] == 1f)
		{
			if (base.NPC.frame.Y < frameHeight * 5)
			{
				base.NPC.frameCounter = 0.0;
				base.NPC.frame.Y = frameHeight * 5;
			}
			if (base.NPC.frame.Y > frameHeight * 6)
			{
				ChangeAI(2);
			}
		}
		else if (base.NPC.ai[0] == 2f)
		{
			if (base.NPC.frame.Y < frameHeight * 7 || base.NPC.frame.Y > frameHeight * 12)
			{
				base.NPC.frameCounter = 0.0;
				base.NPC.frame.Y = frameHeight * 7;
			}
		}
		else if (base.NPC.ai[0] == 3f)
		{
			if (base.NPC.frame.Y < frameHeight * 13)
			{
				base.NPC.frameCounter = 0.0;
				base.NPC.frame.Y = frameHeight * 13;
			}
			if (base.NPC.frame.Y > frameHeight * 17)
			{
				ChangeAI(4);
			}
		}
		else if (base.NPC.frame.Y < frameHeight * 18 || base.NPC.frame.Y > frameHeight * 21)
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y = frameHeight * 18;
		}
		if (base.NPC.frame.Y >= frameHeight * 15 && base.NPC.active)
		{
			Lighting.AddLight(base.NPC.Center, 0.8f, 0.03f, 0.1f);
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 6; i++)
		{
			Dust.NewDustDirect(base.NPC.position, base.NPC.width, base.NPC.height, 226);
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		Texture2D critterTexture = TextureAssets.Npc[base.Type].Value;
		Texture2D glowmask = GlowTexture.Value;
		Vector2 drawPosition = base.NPC.Center - screenPos + Vector2.UnitY * base.NPC.gfxOffY;
		drawPosition.Y += base.DrawOffsetY;
		SpriteEffects direction = (SpriteEffects)(base.NPC.spriteDirection == 1);
		spriteBatch.Draw(critterTexture, drawPosition, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, base.NPC.frame.Size() * 0.5f, base.NPC.scale, direction, 0f);
		spriteBatch.Draw(glowmask, drawPosition, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(Color.White), base.NPC.rotation, base.NPC.frame.Size() * 0.5f, base.NPC.scale, direction, 0f);
		return false;
	}
}
