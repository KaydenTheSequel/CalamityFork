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

namespace CalamityMod.NPCs.Cryogen;

[LongDistanceNetSync(SyncWith = typeof(Cryogen))]
public class CryogenShield : ModNPC
{
	public static readonly SoundStyle BreakSound = new SoundStyle("CalamityMod/Sounds/NPCKilled/CryogenShieldBreak");

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
	}

	public override void SetDefaults()
	{
		base.NPC.Calamity().canBreakPlayerDefense = true;
		base.NPC.damage = 60;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.noTileCollide = true;
		base.NPC.coldDamage = true;
		base.NPC.width = 216;
		base.NPC.height = 216;
		base.NPC.scale *= ((CalamityWorld.death || BossRushEvent.BossRushActive || Main.getGoodWorld) ? 0.8f : 1f);
		base.NPC.DR_NERD(0.4f);
		base.NPC.LifeMaxNERB(2800, 3360, 33600);
		base.NPC.Opacity = 0f;
		base.NPC.HitSound = Cryogen.HitSound;
		base.NPC.DeathSound = BreakSound;
		if (Main.zenithWorld)
		{
			base.NPC.Calamity().VulnerableToHeat = false;
			base.NPC.Calamity().VulnerableToCold = true;
			base.NPC.Calamity().VulnerableToWater = true;
		}
		else
		{
			base.NPC.Calamity().VulnerableToHeat = true;
			base.NPC.Calamity().VulnerableToCold = false;
			base.NPC.Calamity().VulnerableToSickness = false;
		}
	}

	public override void AI()
	{
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.HitSound = (Main.zenithWorld ? SoundID.NPCHit41 : Cryogen.HitSound);
		base.NPC.DeathSound = (Main.zenithWorld ? SoundID.NPCDeath14 : BreakSound);
		base.NPC.Opacity += 0.01f;
		if (base.NPC.Opacity >= 1f)
		{
			base.NPC.damage = base.NPC.defDamage;
			base.NPC.Opacity = 1f;
		}
		else
		{
			base.NPC.damage = 0;
		}
		base.NPC.rotation += 0.15f;
		if (base.NPC.type == ModContent.NPCType<CryogenShield>())
		{
			int mainCryogen = (int)base.NPC.ai[0];
			if (Main.npc[mainCryogen].active && Main.npc[mainCryogen].type == ModContent.NPCType<Cryogen>())
			{
				base.NPC.velocity = Vector2.Zero;
				base.NPC.position = Main.npc[mainCryogen].Center;
				base.NPC.ai[1] = Main.npc[mainCryogen].velocity.X;
				base.NPC.ai[2] = Main.npc[mainCryogen].velocity.Y;
				base.NPC.ai[3] = Main.npc[mainCryogen].target;
				base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
				base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
			}
			else
			{
				base.NPC.life = 0;
				base.NPC.HitEffect();
				base.NPC.active = false;
				base.NPC.netUpdate = true;
			}
		}
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		Rectangle targetHitbox = target.Hitbox;
		float num = Vector2.Distance(base.NPC.Center, targetHitbox.TopLeft());
		float hitboxTopRight = Vector2.Distance(base.NPC.Center, targetHitbox.TopRight());
		float hitboxBotLeft = Vector2.Distance(base.NPC.Center, targetHitbox.BottomLeft());
		float hitboxBotRight = Vector2.Distance(base.NPC.Center, targetHitbox.BottomRight());
		float minDist = num;
		if (hitboxTopRight < minDist)
		{
			minDist = hitboxTopRight;
		}
		if (hitboxBotLeft < minDist)
		{
			minDist = hitboxBotLeft;
		}
		if (hitboxBotRight < minDist)
		{
			minDist = hitboxBotRight;
		}
		if (minDist <= 100f * base.NPC.scale)
		{
			return base.NPC.Opacity >= 1f;
		}
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			if (Main.zenithWorld)
			{
				target.AddBuff(ModContent.BuffType<VulnerabilityHex>(), 180);
			}
			else
			{
				target.AddBuff(46, 120);
			}
		}
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/NPCs/Cryogen/CryogenShield", (AssetRequestMode)2).Value;
		base.NPC.DrawBackglow(Main.zenithWorld ? Color.Red : Cryogen.BackglowColor, 4f, (SpriteEffects)0, base.NPC.frame, screenPos);
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / Main.npcFrameCount[base.Type] / 2));
		Vector2 drawPos = base.NPC.Center - screenPos;
		drawPos -= new Vector2((float)texture.Width, (float)(texture.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
		drawPos += origin * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		Color overlay = (Main.zenithWorld ? Color.Red : drawColor);
		spriteBatch.Draw(texture, drawPos, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(overlay), base.NPC.rotation, origin, base.NPC.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * 0.5f * balance);
	}

	public override void ModifyTypeName(ref string typeName)
	{
		if (Main.zenithWorld)
		{
			typeName = CalamityUtils.GetTextValue("NPCs.PyrogenShield");
		}
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		int dusttype = (Main.zenithWorld ? 235 : 67);
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, dusttype, hit.HitDirection, -1f);
		}
		if (base.NPC.life > 0)
		{
			return;
		}
		for (int i = 0; i < 25; i++)
		{
			int icyDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, dusttype, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[icyDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[icyDust].scale = 0.5f;
				Main.dust[icyDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 50; j++)
		{
			int icyDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, dusttype, 0f, 0f, 100, default(Color), 3f);
			Main.dust[icyDust2].noGravity = true;
			Dust obj2 = Main.dust[icyDust2];
			obj2.velocity *= 5f;
			icyDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, dusttype, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[icyDust2];
			obj3.velocity *= 2f;
		}
		if (Main.dedServ || Main.zenithWorld)
		{
			return;
		}
		int totalGores = 16;
		double radians = (float)Math.PI * 2f / (float)totalGores;
		Vector2 spinningPoint = default(Vector2);
		((Vector2)(ref spinningPoint))._002Ector(0f, -1f);
		for (int l = 0; l < totalGores; l++)
		{
			Vector2 goreRotation = spinningPoint.RotatedBy(radians * (double)l);
			for (int x = 1; x <= 4; x++)
			{
				float randomSpread = (float)Main.rand.Next(-200, 201) / 100f;
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.Center + Vector2.Normalize(goreRotation) * 80f, goreRotation * new Vector2(base.NPC.ai[1], base.NPC.ai[2]) * randomSpread, base.Mod.Find<ModGore>("CryoShieldGore" + x).Type, base.NPC.scale);
			}
		}
	}
}
