using System;
using System.Collections.Generic;
using CalamityMod.BiomeManagers;
using CalamityMod.Items.Critters;
using CalamityMod.Packets;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.TownNPCs;

public class AndroombaFriendly : ModNPC
{
	public static Asset<Texture2D>[] FaceTextures = new Asset<Texture2D>[9];

	public static int AstralConversionType;

	public static List<(int, string, Action<NPC>)> customConversionTypes = new List<(int, string, Action<NPC>)>();

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 9;
		Main.npcCatchable[base.Type] = true;
		NPCID.Sets.NoTownNPCHappiness[base.Type] = true;
		NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers();
		value.Position.Y += 16f;
		value.PortraitPositionYOverride = 36f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		NPCID.Sets.ShimmerTownTransform[base.Type] = false;
		if (!Main.dedServ)
		{
			FaceTextures[0] = ModContent.Request<Texture2D>(Texture + "_Pure", (AssetRequestMode)2);
			FaceTextures[1] = ModContent.Request<Texture2D>(Texture + "_Corruption", (AssetRequestMode)2);
			FaceTextures[2] = ModContent.Request<Texture2D>(Texture + "_Hallow", (AssetRequestMode)2);
			FaceTextures[3] = ModContent.Request<Texture2D>(Texture + "_Mushroom", (AssetRequestMode)2);
			FaceTextures[4] = ModContent.Request<Texture2D>(Texture + "_Crimson", (AssetRequestMode)2);
			FaceTextures[5] = ModContent.Request<Texture2D>(Texture + "_Desert", (AssetRequestMode)2);
			FaceTextures[6] = ModContent.Request<Texture2D>(Texture + "_Snow", (AssetRequestMode)2);
			FaceTextures[7] = ModContent.Request<Texture2D>(Texture + "_Forest", (AssetRequestMode)2);
			FaceTextures[8] = ModContent.Request<Texture2D>(Texture + "_Astral", (AssetRequestMode)2);
		}
		AstralConversionType = ModContent.GetInstance<AstralConversion>().Type;
	}

	public override void SetDefaults()
	{
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.damage = 0;
		base.NPC.width = 36;
		base.NPC.height = 16;
		base.NPC.lifeMax = 80;
		base.NPC.friendly = true;
		base.NPC.townNPC = true;
		base.NPC.knockBackResist = 0f;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = false;
		base.NPC.chaseable = false;
		base.NPC.HitSound = SoundID.NPCHit4;
		base.NPC.DeathSound = SoundID.NPCDeath44;
		base.NPC.catchItem = (short)ModContent.ItemType<AndroombaItem>();
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<ArsenalLabBiome>().Type };
		base.NPC.dontTakeDamage = true;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
	}

	public override bool CanChat()
	{
		return false;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.AndroombaFriendly")
		});
	}

	public override void AI()
	{
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.velocity.Y = MathHelper.Clamp(base.NPC.velocity.Y + 0.4f, -15f, 15f);
		base.NPC.spriteDirection = (int)base.NPC.ai[2];
		float num = base.NPC.ai[0];
		if (num != 0f)
		{
			if (num != 1f)
			{
				if (num == 2f)
				{
					base.NPC.velocity.X = 0f;
					Convert((int)base.NPC.ai[3]);
				}
			}
			else
			{
				base.NPC.ai[1]++;
				base.NPC.velocity.X = base.NPC.ai[2] * 2f;
				if (!Collision.CanHit(base.NPC.Center - Vector2.UnitX * base.NPC.ai[2] * 8f, 2, 2, base.NPC.Center + Vector2.UnitX * base.NPC.ai[2] * 32f, 8, 8))
				{
					ChangeAIHook(2);
				}
				Convert((int)base.NPC.ai[3]);
			}
		}
		else if (base.NPC.ai[1] == 0f)
		{
			Player closest = Main.player[Player.FindClosest(base.NPC.position, 9999, 9999)];
			base.NPC.ai[2] = ((closest.position.X <= base.NPC.position.X) ? 1 : (-1));
		}
		ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Player player = enumerator.Current;
			Rectangle hitbox = base.NPC.Hitbox;
			if (((Rectangle)(ref hitbox)).Intersects(player.HitboxForBestiaryNearbyCheck))
			{
				Main.BestiaryTracker.Chats.RegisterChatStartWith(base.NPC);
				break;
			}
		}
	}

	public void Convert(int conversionType)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		int x = (int)(base.NPC.Center.X / 16f);
		int y = (int)(base.NPC.Center.Y / 16f);
		if (conversionType <= 7)
		{
			WorldGen.Convert(x, y, conversionType, 2);
		}
		else if (conversionType == 8)
		{
			WorldGen.Convert(x, y, AstralConversionType, 2);
		}
		else
		{
			customConversionTypes[conversionType - 9].Item3(base.NPC);
		}
	}

	public void ChangeAIHook(int phase)
	{
		if (Main.netMode == 0)
		{
			ChangeAI(base.NPC.whoAmI, phase);
		}
		else
		{
			SyncAndroombaAIPacket.Send(this, phase);
		}
	}

	public static void ChangeAI(int index, int phase)
	{
		NPC npc = Main.npc[index];
		if (npc != null && npc.active)
		{
			npc.ai[0] = phase;
			npc.ai[1] = 0f;
			npc.netUpdate = true;
			if (Main.dedServ)
			{
				NetMessage.SendData(23, -1, -1, null, index);
			}
		}
	}

	public static void SwapSolution(int index, int solutionType)
	{
		NPC npc = Main.npc[index];
		if (npc != null && npc.active)
		{
			npc.ai[3] = solutionType;
			npc.netUpdate = true;
			if (Main.dedServ)
			{
				NetMessage.SendData(23, -1, -1, null, index);
			}
		}
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
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter > 6.0)
		{
			base.NPC.frameCounter = 0.0;
			base.NPC.frame.Y += frameHeight;
		}
		if (base.NPC.ai[0] == 1f || base.NPC.IsABestiaryIconDummy)
		{
			if (base.NPC.frame.Y > frameHeight * 4)
			{
				base.NPC.frameCounter = 0.0;
				base.NPC.frame.Y = frameHeight;
			}
		}
		else if (base.NPC.ai[0] == 2f)
		{
			if (base.NPC.frame.Y < frameHeight * 5)
			{
				base.NPC.frameCounter = 0.0;
				base.NPC.frame.Y = frameHeight * 5;
			}
			if (base.NPC.frame.Y > frameHeight * 8)
			{
				base.NPC.frame.Y = frameHeight;
				base.NPC.ai[2] *= -1f;
				ChangeAIHook(1);
			}
		}
		else
		{
			base.NPC.frame.Y = 0;
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
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		Texture2D critterTexture = TextureAssets.Npc[base.Type].Value;
		Texture2D glowmask = FaceTextures[0].Value;
		glowmask = ((!(base.NPC.ai[3] <= 8f)) ? ModContent.Request<Texture2D>(customConversionTypes[(int)base.NPC.ai[3] - 9].Item2, (AssetRequestMode)2).Value : FaceTextures[(int)base.NPC.ai[3]].Value);
		Vector2 drawPosition = base.NPC.Center - screenPos + Vector2.UnitY * base.NPC.gfxOffY;
		drawPosition.Y += base.DrawOffsetY;
		SpriteEffects direction = (SpriteEffects)(base.NPC.spriteDirection == 1);
		spriteBatch.Draw(critterTexture, drawPosition, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, base.NPC.frame.Size() * 0.5f, base.NPC.scale, direction, 0f);
		spriteBatch.Draw(glowmask, drawPosition, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(Color.White), base.NPC.rotation, base.NPC.frame.Size() * 0.5f, base.NPC.scale, direction, 0f);
		return false;
	}
}
