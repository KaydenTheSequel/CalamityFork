using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Events;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Sounds;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.AstrumAureus;

public class AureusSpawn : ModNPC
{
	public static Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
		Main.npcFrameCount[base.Type] = 4;
		NPCID.Sets.TrailingMode[base.Type] = 1;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 60;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.width = 90;
		base.NPC.height = 60;
		base.NPC.Opacity = 0f;
		base.NPC.defense = 10;
		base.NPC.lifeMax = 5000;
		base.NPC.knockBackResist = 0f;
		base.NPC.dontTakeDamage = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToSickness = false;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.dontTakeDamage);
		for (int i = 0; i < 4; i++)
		{
			writer.Write(base.NPC.Calamity().newAI[i]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.dontTakeDamage = reader.ReadBoolean();
		for (int i = 0; i < 4; i++)
		{
			base.NPC.Calamity().newAI[i] = reader.ReadSingle();
		}
	}

	public override void AI()
	{
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0896: Unknown result type (might be due to invalid IL or missing references)
		//IL_089f: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0881: Unknown result type (might be due to invalid IL or missing references)
		//IL_0884: Unknown result type (might be due to invalid IL or missing references)
		//IL_0889: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_076e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0773: Unknown result type (might be due to invalid IL or missing references)
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_053c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0553: Unknown result type (might be due to invalid IL or missing references)
		//IL_055d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0604: Unknown result type (might be due to invalid IL or missing references)
		//IL_060a: Unknown result type (might be due to invalid IL or missing references)
		//IL_062f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0639: Unknown result type (might be due to invalid IL or missing references)
		//IL_063e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0649: Unknown result type (might be due to invalid IL or missing references)
		//IL_0677: Unknown result type (might be due to invalid IL or missing references)
		//IL_067d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0694: Unknown result type (might be due to invalid IL or missing references)
		//IL_069e: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a3: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityGlobalNPC.astrumAureus < 0 || !Main.npc[CalamityGlobalNPC.astrumAureus].active)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.checkDead();
			base.NPC.active = false;
			base.NPC.netUpdate = true;
			return;
		}
		Lighting.AddLight((int)((base.NPC.position.X + (float)(base.NPC.width / 2)) / 16f), (int)((base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16f), 0.6f * base.NPC.Opacity * base.NPC.scale, 0.25f * base.NPC.Opacity * base.NPC.scale, 0f);
		base.NPC.rotation = Math.Abs(base.NPC.velocity.X) * (float)base.NPC.direction * 0.04f;
		base.NPC.spriteDirection = base.NPC.direction;
		if (base.NPC.Opacity < 1f)
		{
			base.NPC.Opacity += 0.01f;
			if (base.NPC.Opacity > 0.33f)
			{
				NPC nPC = base.NPC;
				nPC.velocity *= 0.95f;
			}
			if (base.NPC.Opacity >= 1f)
			{
				base.NPC.Opacity = 1f;
				base.NPC.dontTakeDamage = false;
			}
			for (int i = 0; i < 8; i++)
			{
				int dust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, ModContent.DustType<AstralOrange>(), base.NPC.velocity.X, base.NPC.velocity.Y, 255);
				Main.dust[dust].noGravity = true;
				Dust obj = Main.dust[dust];
				obj.velocity *= 0.5f;
			}
			return;
		}
		base.NPC.TargetClosest();
		float pushVelocity = 0.5f;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (n.whoAmI != base.NPC.whoAmI && n.type == base.NPC.type && Vector2.Distance(base.NPC.Center, n.Center) < 160f + 30f * (base.NPC.scale - 1f))
			{
				if (base.NPC.position.X < n.position.X)
				{
					base.NPC.velocity.X = base.NPC.velocity.X - pushVelocity;
				}
				else
				{
					base.NPC.velocity.X = base.NPC.velocity.X + pushVelocity;
				}
				if (base.NPC.position.Y < n.position.Y)
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y - pushVelocity;
				}
				else
				{
					base.NPC.velocity.Y = base.NPC.velocity.Y + pushVelocity;
				}
			}
		}
		bool num = (float)base.NPC.life / (float)base.NPC.lifeMax <= 0.5f || Main.IsItDay();
		int inertia = 30;
		Vector2 vector = Main.player[base.NPC.target].Center - base.NPC.Center;
		Vector2 vector2 = Main.npc[CalamityGlobalNPC.astrumAureus].Center - base.NPC.Center;
		float distanceFromAureus = ((Vector2)(ref vector2)).Length();
		float distanceFromTarget = ((Vector2)(ref vector)).Length();
		float chargeVelocity = 8f;
		if (num)
		{
			inertia = 50;
			chargeVelocity = 16f;
			float engagePhase2GateValue = 60f;
			float enlargeGateValue = engagePhase2GateValue + 300f;
			base.NPC.ai[0]++;
			if (base.NPC.ai[0] < engagePhase2GateValue)
			{
				NPC nPC2 = base.NPC;
				nPC2.velocity *= 0.95f;
				return;
			}
			if (base.NPC.ai[0] == engagePhase2GateValue)
			{
				SoundEngine.PlaySound(in CommonCalamitySounds.ExoPlasmaExplosionSound, base.NPC.Center);
				for (int j = 0; j < 10; j++)
				{
					int dust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100);
					Dust obj2 = Main.dust[dust2];
					obj2.velocity *= 1.66f;
					if (Main.rand.NextBool())
					{
						Main.dust[dust2].scale = 0.5f;
						Main.dust[dust2].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
					}
					Main.dust[dust2].noGravity = true;
				}
				for (int k = 0; k < 20; k++)
				{
					int dust3 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dust3].noGravity = true;
					Dust obj3 = Main.dust[dust3];
					obj3.velocity *= 2f;
					dust3 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100);
					Dust obj4 = Main.dust[dust3];
					obj4.velocity *= 1.33f;
					Main.dust[dust3].noGravity = true;
				}
			}
			float enlargeDuration = 180f;
			if (base.NPC.ai[0] >= enlargeGateValue)
			{
				base.NPC.Calamity().newAI[0]++;
				base.NPC.scale = MathHelper.Lerp(1f, 2f, base.NPC.Calamity().newAI[0] / enlargeDuration);
				base.NPC.width = (int)(90f * base.NPC.scale);
				base.NPC.height = (int)(60f * base.NPC.scale);
			}
			Tile tileSafely = Framing.GetTileSafely(base.NPC.Center.ToTileCoordinates());
			bool explodeOnCollision = tileSafely.HasUnactuatedTile && Main.tileSolid[tileSafely.TileType] && !Main.tileSolidTop[tileSafely.TileType] && !TileID.Sets.Platforms[tileSafely.TileType];
			bool explodeOnAureus = distanceFromAureus < 180f + 30f * (base.NPC.scale - 1f) && !Main.IsItDay();
			if (((((Vector2)(ref vector)).Length() < 60f + 30f * (base.NPC.scale - 1f)) | explodeOnCollision | explodeOnAureus) || base.NPC.Calamity().newAI[0] >= enlargeDuration)
			{
				base.NPC.life = 0;
				base.NPC.HitEffect();
				base.NPC.checkDead();
				return;
			}
		}
		chargeVelocity += distanceFromTarget / 200f;
		if (distanceFromTarget > chargeVelocity)
		{
			((Vector2)(ref vector)).Normalize();
			vector *= chargeVelocity;
		}
		base.NPC.velocity = (base.NPC.velocity * (float)(inertia - 1) + vector) / (float)inertia;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.5f * balance);
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.Calamity().newAI[0] >= 180f)
		{
			return false;
		}
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		if (base.NPC.ai[0] >= 60f)
		{
			NPC nPC = base.NPC;
			Color backglowColor = Color.Lerp(Color.Cyan, Color.Orange, (float)Math.Sin(Main.GlobalTimeWrappedHourly) / 2f + 0.5f);
			((Color)(ref backglowColor)).A = 0;
			nPC.DrawBackglow(backglowColor, 2f + 8f * ((float)Math.Sin(Main.GlobalTimeWrappedHourly * ((float)Math.PI * 2f)) + 1f), spriteEffects, base.NPC.frame, screenPos);
		}
		Texture2D texture2D15 = TextureAssets.Npc[base.Type].Value;
		Vector2 originalDrawSize = default(Vector2);
		((Vector2)(ref originalDrawSize))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / Main.npcFrameCount[base.Type] / 2));
		Color whiteColor = Color.White;
		int afterimageAmt = 10;
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int i = 1; i < afterimageAmt; i += 2)
			{
				Color afterimageColor = drawColor;
				afterimageColor = Color.Lerp(afterimageColor, whiteColor, 0.5f);
				afterimageColor = base.NPC.GetAlpha(afterimageColor);
				afterimageColor *= (float)(afterimageAmt - i) / 15f;
				Vector2 afterimagePos = base.NPC.oldPos[i] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				afterimagePos -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
				afterimagePos += originalDrawSize * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture2D15, afterimagePos, (Rectangle?)base.NPC.frame, afterimageColor, base.NPC.rotation, originalDrawSize, base.NPC.scale, spriteEffects, 0f);
			}
		}
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
		drawLocation += originalDrawSize * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, originalDrawSize, base.NPC.scale, spriteEffects, 0f);
		texture2D15 = GlowTexture.Value;
		Color afterimageColorLerp = Color.Lerp(Color.White, Color.Orange, 0.5f) * base.NPC.Opacity;
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int j = 1; j < afterimageAmt; j++)
			{
				Color secondAfterimageColor = afterimageColorLerp;
				secondAfterimageColor = Color.Lerp(secondAfterimageColor, whiteColor, 0.5f);
				secondAfterimageColor *= (float)(afterimageAmt - j) / 15f;
				Vector2 secondAfterimagePos = base.NPC.oldPos[j] + new Vector2((float)base.NPC.width, (float)base.NPC.height) / 2f - screenPos;
				secondAfterimagePos -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
				secondAfterimagePos += originalDrawSize * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
				spriteBatch.Draw(texture2D15, secondAfterimagePos, (Rectangle?)base.NPC.frame, secondAfterimageColor, base.NPC.rotation, originalDrawSize, base.NPC.scale, spriteEffects, 0f);
			}
		}
		spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, afterimageColorLerp, base.NPC.rotation, originalDrawSize, base.NPC.scale, spriteEffects, 0f);
		return false;
	}

	public override void FindFrame(int frameHeight)
	{
		if (base.NPC.IsABestiaryIconDummy)
		{
			base.NPC.Opacity = 1f;
		}
		base.NPC.frameCounter += 0.15000000596046448;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override void OnKill()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		int closestPlayer = Player.FindClosest(base.NPC.Center, 1, 1);
		if (Main.rand.NextBool(8) && Main.player[closestPlayer].statLife < Main.player[closestPlayer].statLifeMax2)
		{
			Item.NewItem(base.NPC.GetSource_Loot(), (int)base.NPC.position.X, (int)base.NPC.position.Y, base.NPC.width, base.NPC.height, 58);
		}
		Vector2 center;
		if ((CalamityWorld.death || BossRushEvent.BossRushActive) && Main.netMode != 1)
		{
			int totalProjectiles = 3 + (int)((base.NPC.scale - 1f) * 3f);
			double radians = (float)Math.PI * 2f / (float)totalProjectiles;
			int type = ModContent.ProjectileType<AstralLaser>();
			float velocity = 6f;
			double angleA = radians * 0.5;
			double angleB = (double)MathHelper.ToRadians(90f) - angleA;
			float velocityX = (float)((double)velocity * Math.Sin(angleA) / Math.Sin(angleB));
			Vector2 spinningPoint = (Main.rand.NextBool() ? new Vector2(0f, 0f - velocity) : new Vector2(0f - velocityX, 0f - velocity));
			for (int k = 0; k < totalProjectiles; k++)
			{
				double radians2 = radians * (double)k;
				center = default(Vector2);
				Vector2 vector255 = spinningPoint.RotatedBy(radians2, center);
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, vector255, type, AstrumAureus.LaserDamage, 0f, Main.myPlayer);
			}
		}
		if (CalamityGlobalNPC.astrumAureus >= 0 && Main.npc[CalamityGlobalNPC.astrumAureus].active && Main.netMode != 1)
		{
			center = Main.npc[CalamityGlobalNPC.astrumAureus].Center - base.NPC.Center;
			if (((Vector2)(ref center)).Length() < 200f + 30f * (base.NPC.scale - 1f) && !Main.IsItDay())
			{
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), Main.npc[CalamityGlobalNPC.astrumAureus].Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), (int)((float)(Main.npc[CalamityGlobalNPC.astrumAureus].lifeMax / 200) * base.NPC.scale), 0f, Main.myPlayer, Main.npc[CalamityGlobalNPC.astrumAureus].whoAmI);
			}
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173, hit.HitDirection, -1f);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		SoundEngine.PlaySound(in SoundID.Item14, base.NPC.Center);
		base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
		base.NPC.damage = (int)Math.Round((double)base.NPC.defDamage * (double)base.NPC.scale);
		base.NPC.width = (base.NPC.height = (int)(216f * base.NPC.scale));
		base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
		for (int r = 0; r < 30; r++)
		{
			int astralDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100);
			Dust obj = Main.dust[astralDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[astralDust].scale = 0.5f;
				Main.dust[astralDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
			Main.dust[astralDust].noGravity = true;
		}
		for (int s = 0; s < 60; s++)
		{
			int astralDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
			Main.dust[astralDust2].noGravity = true;
			Dust obj2 = Main.dust[astralDust2];
			obj2.velocity *= 5f;
			astralDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100);
			Dust obj3 = Main.dust[astralDust2];
			obj3.velocity *= 2f;
			Main.dust[astralDust2].noGravity = true;
		}
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		return base.NPC.Opacity == 1f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		int debuffType = (Main.zenithWorld ? ModContent.BuffType<GodSlayerInferno>() : ModContent.BuffType<AstralInfectionDebuff>());
		target.AddBuff(debuffType, (int)(120f * base.NPC.scale));
	}
}
