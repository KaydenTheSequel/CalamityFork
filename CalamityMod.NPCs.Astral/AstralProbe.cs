using System;
using CalamityMod.BiomeManagers;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Sounds;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Astral;

public class AstralProbe : ModNPC
{
	public static Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		NPCID.Sets.NeedsExpertScaling[base.Type] = true;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 0;
		base.NPC.width = 30;
		base.NPC.height = 30;
		base.NPC.defense = 20;
		base.NPC.lifeMax = 120;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0.95f;
		base.NPC.value = Item.buyPrice(0, 0, 1);
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.DeathSound = SoundID.NPCDeath14;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<AstralProbeBanner>();
		if (DownedBossSystem.downedAstrumAureus)
		{
			base.NPC.damage = 30;
			base.NPC.defense = 30;
			base.NPC.knockBackResist = 0.85f;
			base.NPC.lifeMax = 180;
		}
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AstralInfectionBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.AstralProbe")
		});
	}

	public override void AI()
	{
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead)
		{
			base.NPC.TargetClosest();
		}
		float probeSpeed = (CalamityWorld.death ? 8f : (CalamityWorld.revenge ? 6.5f : 5f));
		float probeAcceleration = (CalamityWorld.death ? 0.08f : (CalamityWorld.revenge ? 0.065f : 0.05f));
		Vector2 probePosition = default(Vector2);
		((Vector2)(ref probePosition))._002Ector(base.NPC.position.X + (float)base.NPC.width * 0.5f, base.NPC.position.Y + (float)base.NPC.height * 0.5f);
		float targetXDirection = Main.player[base.NPC.target].position.X + (float)(Main.player[base.NPC.target].width / 2);
		float targetYDirection = Main.player[base.NPC.target].position.Y + (float)(Main.player[base.NPC.target].height / 2);
		targetXDirection = (int)(targetXDirection / 8f) * 8;
		targetYDirection = (int)(targetYDirection / 8f) * 8;
		probePosition.X = (int)(probePosition.X / 8f) * 8;
		probePosition.Y = (int)(probePosition.Y / 8f) * 8;
		targetXDirection -= probePosition.X;
		targetYDirection -= probePosition.Y;
		float targetDistance = (float)Math.Sqrt(targetXDirection * targetXDirection + targetYDirection * targetYDirection);
		float num = targetDistance;
		bool tooFar = false;
		if (targetDistance > 600f)
		{
			tooFar = true;
		}
		if (targetDistance == 0f)
		{
			targetXDirection = base.NPC.velocity.X;
			targetYDirection = base.NPC.velocity.Y;
		}
		else
		{
			targetDistance = probeSpeed / targetDistance;
			targetXDirection *= targetDistance;
			targetYDirection *= targetDistance;
		}
		if (num > 100f)
		{
			base.NPC.ai[0]++;
			if (base.NPC.ai[0] > 0f)
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y + 0.023f;
			}
			else
			{
				base.NPC.velocity.Y = base.NPC.velocity.Y - 0.023f;
			}
			if (base.NPC.ai[0] < -100f || base.NPC.ai[0] > 100f)
			{
				base.NPC.velocity.X = base.NPC.velocity.X + 0.023f;
			}
			else
			{
				base.NPC.velocity.X = base.NPC.velocity.X - 0.023f;
			}
			if (base.NPC.ai[0] > 200f)
			{
				base.NPC.ai[0] = -200f;
			}
		}
		if (Main.player[base.NPC.target].dead)
		{
			targetXDirection = (float)base.NPC.direction * probeSpeed / 2f;
			targetYDirection = (0f - probeSpeed) / 2f;
		}
		if (base.NPC.velocity.X < targetXDirection)
		{
			base.NPC.velocity.X = base.NPC.velocity.X + probeAcceleration;
		}
		else if (base.NPC.velocity.X > targetXDirection)
		{
			base.NPC.velocity.X = base.NPC.velocity.X - probeAcceleration;
		}
		if (base.NPC.velocity.Y < targetYDirection)
		{
			base.NPC.velocity.Y = base.NPC.velocity.Y + probeAcceleration;
		}
		else if (base.NPC.velocity.Y > targetYDirection)
		{
			base.NPC.velocity.Y = base.NPC.velocity.Y - probeAcceleration;
		}
		base.NPC.localAI[0]++;
		if (base.NPC.justHit)
		{
			base.NPC.localAI[0] = 0f;
		}
		if (Main.netMode != 1 && base.NPC.localAI[0] >= 200f)
		{
			base.NPC.localAI[0] = 0f;
			if (Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height))
			{
				int projDamage = (Main.expertMode ? 14 : 18);
				if (DownedBossSystem.downedAstrumAureus)
				{
					projDamage += 6;
				}
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), probePosition.X, probePosition.Y, targetXDirection, targetYDirection, 84, projDamage, 0f, Main.myPlayer);
			}
		}
		int num2 = (int)base.NPC.position.X + base.NPC.width / 2;
		int npcTileY = (int)base.NPC.position.Y + base.NPC.height / 2;
		int i = num2 / 16;
		npcTileY /= 16;
		if (!WorldGen.SolidTile(i, npcTileY))
		{
			Lighting.AddLight((int)((base.NPC.position.X + (float)(base.NPC.width / 2)) / 16f), (int)((base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16f), 0.3f, 0f, 0.25f);
		}
		if (targetXDirection > 0f)
		{
			base.NPC.spriteDirection = 1;
			base.NPC.rotation = (float)Math.Atan2(targetYDirection, targetXDirection);
		}
		if (targetXDirection < 0f)
		{
			base.NPC.spriteDirection = -1;
			base.NPC.rotation = (float)Math.Atan2(targetYDirection, targetXDirection) + 3.14f;
		}
		float recoilVelocity = 0.7f;
		if (base.NPC.collideX)
		{
			base.NPC.netUpdate = true;
			base.NPC.velocity.X = base.NPC.oldVelocity.X * (0f - recoilVelocity);
			if (base.NPC.direction == -1 && base.NPC.velocity.X > 0f && base.NPC.velocity.X < 2f)
			{
				base.NPC.velocity.X = 2f;
			}
			if (base.NPC.direction == 1 && base.NPC.velocity.X < 0f && base.NPC.velocity.X > -2f)
			{
				base.NPC.velocity.X = -2f;
			}
		}
		if (base.NPC.collideY)
		{
			base.NPC.netUpdate = true;
			base.NPC.velocity.Y = base.NPC.oldVelocity.Y * (0f - recoilVelocity);
			if (base.NPC.velocity.Y > 0f && (double)base.NPC.velocity.Y < 1.5)
			{
				base.NPC.velocity.Y = 2f;
			}
			if (base.NPC.velocity.Y < 0f && (double)base.NPC.velocity.Y > -1.5)
			{
				base.NPC.velocity.Y = -2f;
			}
		}
		if (tooFar)
		{
			if ((base.NPC.velocity.X > 0f && targetXDirection > 0f) || (base.NPC.velocity.X < 0f && targetXDirection < 0f))
			{
				if (Math.Abs(base.NPC.velocity.X) < 12f)
				{
					base.NPC.velocity.X = base.NPC.velocity.X * 1.05f;
				}
			}
			else
			{
				base.NPC.velocity.X = base.NPC.velocity.X * 0.9f;
			}
		}
		if (((base.NPC.velocity.X > 0f && base.NPC.oldVelocity.X < 0f) || (base.NPC.velocity.X < 0f && base.NPC.oldVelocity.X > 0f) || (base.NPC.velocity.Y > 0f && base.NPC.oldVelocity.Y < 0f) || (base.NPC.velocity.Y < 0f && base.NPC.oldVelocity.Y > 0f)) && !base.NPC.justHit)
		{
			base.NPC.netUpdate = true;
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture2D15 = TextureAssets.Npc[base.Type].Value;
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / 2));
		Vector2 drawPosition = base.NPC.Center - screenPos;
		drawPosition -= new Vector2((float)texture2D15.Width, (float)texture2D15.Height) * base.NPC.scale / 2f;
		drawPosition += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(texture2D15, drawPosition, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		texture2D15 = GlowTexture.Value;
		spriteBatch.Draw(texture2D15, drawPosition, (Rectangle?)base.NPC.frame, Color.White * 0.6f, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.soundDelay == 0)
		{
			base.NPC.soundDelay = 15;
			SoundEngine.PlaySound(in CommonCalamitySounds.AstralNPCHitSound, base.NPC.Center);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		base.NPC.position = base.NPC.Center;
		base.NPC.width = (base.NPC.height = 30);
		base.NPC.Center = base.NPC.position;
		for (int d = 0; d < 5; d++)
		{
			int purple = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[purple];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[purple].scale = 0.5f;
				Main.dust[purple].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int i = 0; i < 10; i++)
		{
			int cosmos = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173, 0f, 0f, 100, default(Color), 3f);
			Main.dust[cosmos].noGravity = true;
			Dust obj2 = Main.dust[cosmos];
			obj2.velocity *= 5f;
			cosmos = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 173, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[cosmos];
			obj3.velocity *= 2f;
		}
		if (Main.dedServ)
		{
			return;
		}
		Vector2 goreSource = base.NPC.Center;
		int goreAmt = 3;
		Vector2 source = default(Vector2);
		((Vector2)(ref source))._002Ector(goreSource.X - 24f, goreSource.Y - 24f);
		for (int goreIndex = 0; goreIndex < goreAmt; goreIndex++)
		{
			float velocityMult = 0.33f;
			if (goreIndex < goreAmt / 3)
			{
				velocityMult = 0.66f;
			}
			if (goreIndex >= 2 * goreAmt / 3)
			{
				velocityMult = 1f;
			}
			ModContent.GetInstance<CalamityMod>();
			int type = Main.rand.Next(61, 64);
			int smoke = Gore.NewGore(base.NPC.GetSource_Death(), source, default(Vector2), type);
			Gore obj4 = Main.gore[smoke];
			obj4.velocity *= velocityMult;
			obj4.velocity.X++;
			obj4.velocity.Y++;
			type = Main.rand.Next(61, 64);
			smoke = Gore.NewGore(base.NPC.GetSource_Death(), source, default(Vector2), type);
			Gore obj5 = Main.gore[smoke];
			obj5.velocity *= velocityMult;
			obj5.velocity.X--;
			obj5.velocity.Y++;
			type = Main.rand.Next(61, 64);
			smoke = Gore.NewGore(base.NPC.GetSource_Death(), source, default(Vector2), type);
			Gore obj6 = Main.gore[smoke];
			obj6.velocity *= velocityMult;
			obj6.velocity.X++;
			obj6.velocity.Y--;
			type = Main.rand.Next(61, 64);
			smoke = Gore.NewGore(base.NPC.GetSource_Death(), source, default(Vector2), type);
			Gore obj7 = Main.gore[smoke];
			obj7.velocity *= velocityMult;
			obj7.velocity.X--;
			obj7.velocity.Y--;
		}
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(DropHelper.NormalVsExpertQuantity(ModContent.ItemType<StarblightSoot>(), 2, 1, 2, 1, 3));
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (CalamityGlobalNPC.AnyEvents(spawnInfo.Player))
		{
			return 0f;
		}
		if (spawnInfo.Player.InAstral())
		{
			return 0.1f;
		}
		return 0f;
	}
}
