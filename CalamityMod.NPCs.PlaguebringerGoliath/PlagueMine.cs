using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Events;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.PlaguebringerGoliath;

public class PlagueMine : ModNPC
{
	public static Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
		Main.npcFrameCount[base.Type] = 4;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/PlaguebringerGoliath/PlagueMineGlow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 80;
		base.NPC.width = 42;
		base.NPC.height = 42;
		base.NPC.defense = 20;
		base.NPC.lifeMax = (BossRushEvent.BossRushActive ? 10000 : 1000);
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0.2f;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = SoundID.NPCHit4;
		base.NPC.DeathSound = SoundID.NPCDeath14;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
	}

	public override void AI()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		Lighting.AddLight(base.NPC.Center, 0.03f, 0.2f, 0f);
		base.NPC.rotation = base.NPC.velocity.X * 0.04f;
		Player player = Main.player[base.NPC.target];
		if (!player.active || player.dead)
		{
			base.NPC.TargetClosest(faceTarget: false);
			player = Main.player[base.NPC.target];
			if (!player.active || player.dead)
			{
				if (base.NPC.timeLeft > 10)
				{
					base.NPC.timeLeft = 10;
				}
				return;
			}
		}
		else if (base.NPC.timeLeft > 600)
		{
			base.NPC.timeLeft = 600;
		}
		Vector2 vector = Main.player[base.NPC.target].Center - base.NPC.Center;
		float distanceRequiredForExplosion = 90f;
		float timeBeforeExplosion = (death ? 740f : (revenge ? 520f : 400f)) + base.NPC.ai[3] * 4f;
		if (((Vector2)(ref vector)).Length() < distanceRequiredForExplosion || base.NPC.ai[0] >= timeBeforeExplosion)
		{
			CheckDead();
			base.NPC.life = 0;
			return;
		}
		base.NPC.ai[0]++;
		if (base.NPC.ai[0] >= timeBeforeExplosion * 0.8f)
		{
			NPC nPC = base.NPC;
			nPC.velocity *= 0.98f;
			return;
		}
		base.NPC.TargetClosest();
		float num = (death ? 12f : (revenge ? 10f : 8f)) + base.NPC.ai[3] * 0.04f;
		Vector2 npcDirection = default(Vector2);
		((Vector2)(ref npcDirection))._002Ector(base.NPC.Center.X + (float)(base.NPC.direction * 20), base.NPC.Center.Y + 6f);
		float targetX = player.position.X + (float)player.width * 0.5f - npcDirection.X;
		float targetY = player.Center.Y - npcDirection.Y;
		float targetDistance = (float)Math.Sqrt(targetX * targetX + targetY * targetY);
		float npcSpeed = num / targetDistance;
		targetX *= npcSpeed;
		targetY *= npcSpeed;
		float inertia = (death ? 40f : (revenge ? 45f : 50f)) - base.NPC.ai[3] * 0.25f;
		base.NPC.velocity.X = (base.NPC.velocity.X * inertia + targetX) / (inertia + 1f);
		base.NPC.velocity.Y = (base.NPC.velocity.Y * inertia + targetY) / (inertia + 1f);
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter % 6.0 == 5.0)
		{
			base.NPC.frame.Y += frameHeight;
		}
		if (base.NPC.frame.Y / frameHeight >= Main.npcFrameCount[base.Type])
		{
			base.NPC.frame.Y = 0;
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture = TextureAssets.Npc[base.Type].Value;
		Vector2 halfSizeTexture = base.NPC.frame.Size() / 2f;
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)texture.Width, (float)(texture.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
		drawLocation += halfSizeTexture * base.NPC.scale + Vector2.UnitY * base.NPC.gfxOffY;
		Color backAfterimageColor = PlaguebringerGoliath.BackglowColor * base.NPC.Opacity;
		for (int i = 0; i < 10; i++)
		{
			Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 10f).ToRotationVector2() * 4f;
			spriteBatch.Draw(texture, drawLocation + drawOffset, (Rectangle?)base.NPC.frame, backAfterimageColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		}
		spriteBatch.Draw(texture, drawLocation, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		texture = GlowTexture.Value;
		Color redLerpColor = Color.Lerp(Color.White, Color.Red, 0.5f);
		spriteBatch.Draw(texture, drawLocation, (Rectangle?)base.NPC.frame, redLerpColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			if (Main.zenithWorld)
			{
				target.AddBuff(ModContent.BuffType<SulphuricPoisoning>(), 300);
				target.AddBuff(20, 300);
				target.AddBuff(70, 300);
			}
			target.AddBuff(ModContent.BuffType<Plague>(), 180);
		}
	}

	public override bool CheckDead()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item14, base.NPC.Center);
		base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
		base.NPC.width = (base.NPC.height = (Main.zenithWorld ? 300 : 216));
		base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
		base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
		for (int i = 0; i < 15; i++)
		{
			int greenPlague = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 89, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[greenPlague];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[greenPlague].scale = 0.5f;
				Main.dust[greenPlague].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
			Main.dust[greenPlague].noGravity = true;
		}
		for (int j = 0; j < 30; j++)
		{
			int greenPlague2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 89, 0f, 0f, 100, default(Color), 3f);
			Main.dust[greenPlague2].noGravity = true;
			Dust obj2 = Main.dust[greenPlague2];
			obj2.velocity *= 5f;
			greenPlague2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 89, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[greenPlague2];
			obj3.velocity *= 2f;
			Main.dust[greenPlague2].noGravity = true;
		}
		return true;
	}
}
