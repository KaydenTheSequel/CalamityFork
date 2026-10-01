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
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Perforator;

[HasPierceResist(false)]
[LongDistanceNetSync(SyncWith = typeof(PerforatorHeadLarge))]
public class PerforatorBodyLarge : ModNPC
{
	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/NPCHit/PerfLargeHit", 3);

	public static readonly SoundStyle DeathSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/PerfLargeDeath");

	public static Asset<Texture2D> AltTexture;

	public static Asset<Texture2D> Texture_Glow;

	public static Asset<Texture2D> AltTexture_Glow;

	public override LocalizedText DisplayName => CalamityUtils.GetText("NPCs.PerforatorHeadLarge.DisplayName");

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
		if (!Main.dedServ)
		{
			AltTexture = ModContent.Request<Texture2D>(Texture + "Alt", (AssetRequestMode)2);
			AltTexture_Glow = ModContent.Request<Texture2D>(Texture + "AltGlow", (AssetRequestMode)2);
			Texture_Glow = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 20;
		base.NPC.width = 60;
		base.NPC.height = 60;
		base.NPC.defense = 8;
		base.NPC.LifeMaxNERB(2000, 2600, 80000);
		if (Main.zenithWorld)
		{
			base.NPC.lifeMax *= 4;
		}
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.alpha = 255;
		base.NPC.behindTiles = true;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = HitSound;
		base.NPC.DeathSound = DeathSound;
		base.NPC.netAlways = true;
		base.NPC.dontCountMe = true;
		if (CalamityWorld.death || BossRushEvent.BossRushActive)
		{
			base.NPC.scale *= 1.2f;
		}
		else if (CalamityWorld.revenge)
		{
			base.NPC.scale *= 1.15f;
		}
		else if (Main.expertMode)
		{
			base.NPC.scale *= 1.1f;
		}
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = true;
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		return false;
	}

	public override void AI()
	{
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.ai[2] > 0f)
		{
			base.NPC.realLife = (int)base.NPC.ai[2];
		}
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		bool shouldDespawn = !NPC.AnyNPCs(ModContent.NPCType<PerforatorHeadLarge>());
		if (!shouldDespawn)
		{
			if (base.NPC.ai[1] <= 0f)
			{
				shouldDespawn = true;
			}
			else if (Main.npc[(int)base.NPC.ai[1]].life <= 0)
			{
				shouldDespawn = true;
			}
		}
		if (shouldDespawn)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.checkDead();
			base.NPC.active = false;
		}
		if (Main.player[base.NPC.target].dead)
		{
			base.NPC.TargetClosest(faceTarget: false);
		}
		Vector2 segmentPosition = base.NPC.Center;
		float targetX = Main.player[base.NPC.target].Center.X;
		float targetY = Main.player[base.NPC.target].Center.Y;
		targetX = (int)(targetX / 16f) * 16;
		targetY = (int)(targetY / 16f) * 16;
		segmentPosition.X = (int)(segmentPosition.X / 16f) * 16;
		segmentPosition.Y = (int)(segmentPosition.Y / 16f) * 16;
		targetX -= segmentPosition.X;
		targetY -= segmentPosition.Y;
		float targetDistance = (float)Math.Sqrt(targetX * targetX + targetY * targetY);
		if (base.NPC.ai[1] > 0f && base.NPC.ai[1] < (float)Main.npc.Length)
		{
			try
			{
				segmentPosition = base.NPC.Center;
				targetX = Main.npc[(int)base.NPC.ai[1]].Center.X - segmentPosition.X;
				targetY = Main.npc[(int)base.NPC.ai[1]].Center.Y - segmentPosition.Y;
			}
			catch
			{
			}
			base.NPC.rotation = (float)Math.Atan2(targetY, targetX) + (float)Math.PI / 2f;
			targetDistance = (float)Math.Sqrt(targetX * targetX + targetY * targetY);
			int npcWidth = base.NPC.width;
			targetDistance = (targetDistance - (float)npcWidth) / targetDistance;
			targetX *= targetDistance;
			targetY *= targetDistance;
			base.NPC.velocity = Vector2.Zero;
			base.NPC.position.X += targetX;
			base.NPC.position.Y += targetY;
			if (targetX < 0f)
			{
				base.NPC.spriteDirection = 1;
			}
			else if (targetX > 0f)
			{
				base.NPC.spriteDirection = -1;
			}
		}
		if (Main.npc[(int)base.NPC.ai[1]].alpha >= 85)
		{
			if (base.NPC.alpha > 0 && base.NPC.life > 0)
			{
				for (int dustIndex = 0; dustIndex < 2; dustIndex++)
				{
					int dust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].noLight = true;
				}
			}
			Vector2 val = base.NPC.position - base.NPC.oldPosition;
			if (((Vector2)(ref val)).Length() > 2f)
			{
				base.NPC.alpha -= 42;
				if (base.NPC.alpha < 0)
				{
					base.NPC.alpha = 0;
				}
			}
		}
		else if (base.NPC.alpha > 0)
		{
			base.NPC.alpha -= 42;
			if (base.NPC.alpha < 0)
			{
				base.NPC.alpha = 0;
			}
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture2D15 = ((base.NPC.localAI[3] == 1f) ? AltTexture.Value : TextureAssets.Npc[base.Type].Value);
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / 2));
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)texture2D15.Width, (float)texture2D15.Height) * base.NPC.scale / 2f;
		drawLocation += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		texture2D15 = ((base.NPC.localAI[3] == 1f) ? AltTexture_Glow.Value : Texture_Glow.Value);
		Color glowmaskColor = Color.Lerp(Color.White, Color.Yellow, 0.5f);
		spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, glowmaskColor, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		return false;
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("LargePerf2").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("LargePerf3").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("LargePerf4").Type, base.NPC.scale);
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<BurningBlood>(), 180);
		}
	}
}
