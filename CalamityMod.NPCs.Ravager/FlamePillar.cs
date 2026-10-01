using CalamityMod.Events;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Ravager;

public class FlamePillar : ModNPC
{
	public static readonly SoundStyle HitSound = new SoundStyle("CalamityMod/Sounds/NPCHit/RavagerRockPillarHit", 3);

	public static Asset<Texture2D> GlowTexture;

	public static int FlameDamage = 30;

	public static int PostProviFlameBuff = 20;

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
		NPCID.Sets.ImmuneToAllBuffs[base.Type] = true;
		Main.npcFrameCount[base.Type] = 4;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 75;
		base.NPC.width = 40;
		base.NPC.height = 150;
		base.NPC.chaseable = false;
		base.NPC.lifeMax = 1250;
		base.NPC.alpha = 255;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.HitSound = HitSound;
		base.NPC.DeathSound = SoundID.NPCDeath14;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToWater = true;
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter += 0.15000000596046448;
		base.NPC.frameCounter %= Main.npcFrameCount[base.Type];
		int frame = (int)base.NPC.frameCounter;
		base.NPC.frame.Y = frame * frameHeight;
	}

	public override void AI()
	{
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.lifeMax > 1250)
		{
			base.NPC.lifeMax = 1250;
		}
		if (base.NPC.life > base.NPC.lifeMax)
		{
			base.NPC.life = base.NPC.lifeMax;
		}
		base.NPC.damage = 0;
		bool provy = DownedBossSystem.downedProvidence;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		if (CalamityGlobalNPC.scavenger < 0 || !Main.npc[CalamityGlobalNPC.scavenger].active)
		{
			if (Main.netMode != 1)
			{
				base.NPC.StrikeInstantKill();
			}
			return;
		}
		if (base.NPC.timeLeft < 1800)
		{
			base.NPC.timeLeft = 1800;
		}
		Lighting.AddLight((int)((base.NPC.position.X + (float)(base.NPC.width / 2)) / 16f), (int)((base.NPC.position.Y + (float)(base.NPC.height / 2)) / 16f), 0f, 0.5f, 0.5f);
		if (base.NPC.alpha > 0)
		{
			base.NPC.alpha -= 5;
			if (base.NPC.alpha < 0)
			{
				base.NPC.alpha = 0;
			}
		}
		if (base.NPC.ai[0] == 0f)
		{
			base.NPC.ai[1]++;
			if (base.NPC.ai[1] >= 60f)
			{
				base.NPC.ai[0] = 1f;
				base.NPC.ai[1] = 180f;
			}
		}
		else
		{
			if (base.NPC.ai[0] != 1f)
			{
				return;
			}
			if (base.NPC.ai[1] >= 0f)
			{
				base.NPC.ai[1]--;
				base.NPC.localAI[0]++;
				if (base.NPC.localAI[0] % (death ? 45f : 60f) != 0f)
				{
					return;
				}
				if (Main.netMode != 1)
				{
					float speedY = -12f;
					float speedX = 0f;
					switch ((int)base.NPC.ai[2])
					{
					case 1:
						speedX = 2f;
						break;
					case 2:
						speedX = -2f;
						break;
					}
					Vector2 velocity = default(Vector2);
					((Vector2)(ref velocity))._002Ector(speedX, speedY);
					int type = ModContent.ProjectileType<RavagerFlame>();
					base.NPC.SimpleStrikeNPC(base.NPC.lifeMax / 4, 0, crit: false, 0f, null, damageVariation: false, 0f, noPlayerInteraction: true);
					Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, velocity, type, FlameDamage + (provy ? PostProviFlameBuff : 0), 0f, Main.myPlayer);
				}
				base.NPC.ai[2]++;
				base.NPC.localAI[0] = 0f;
				base.NPC.ForceNetUpdate(ignoreCurrentNetSpam: false);
			}
			else if (Main.netMode != 1)
			{
				base.NPC.StrikeInstantKill();
			}
		}
	}

	public override bool? CanFallThroughPlatforms()
	{
		return base.NPC.target >= 0 && Main.player[base.NPC.target].position.Y > base.NPC.position.Y + (float)base.NPC.height;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.NPC.spriteDirection == 1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture2D15 = TextureAssets.Npc[base.Type].Value;
		Vector2 halfSizeTexture = default(Vector2);
		((Vector2)(ref halfSizeTexture))._002Ector((float)(TextureAssets.Npc[base.Type].Value.Width / 2), (float)(TextureAssets.Npc[base.Type].Value.Height / 2));
		Vector2 drawLocation = base.NPC.Center - screenPos;
		drawLocation -= new Vector2((float)texture2D15.Width, (float)(texture2D15.Height / Main.npcFrameCount[base.Type])) * base.NPC.scale / 2f;
		drawLocation += halfSizeTexture * base.NPC.scale + new Vector2(0f, base.NPC.gfxOffY);
		spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, base.NPC.GetAlpha(drawColor), base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		texture2D15 = GlowTexture.Value;
		Color flameBlue = Color.Lerp(Color.White, Color.Cyan, 0.5f);
		spriteBatch.Draw(texture2D15, drawLocation, (Rectangle?)base.NPC.frame, flameBlue, base.NPC.rotation, halfSizeTexture, base.NPC.scale, spriteEffects, 0f);
		return false;
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.life <= 0)
		{
			base.NPC.position.X = base.NPC.position.X + (float)(base.NPC.width / 2);
			base.NPC.position.Y = base.NPC.position.Y + (float)(base.NPC.height / 2);
			base.NPC.width = 50;
			base.NPC.height = 180;
			base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2);
			base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2);
			for (int i = 0; i < 30; i++)
			{
				int iceFlame = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 135, 0f, 0f, 100, default(Color), 2f);
				Dust obj = Main.dust[iceFlame];
				obj.velocity *= 3f;
				if (Main.rand.NextBool())
				{
					Main.dust[iceFlame].scale = 0.5f;
					Main.dust[iceFlame].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			for (int j = 0; j < 30; j++)
			{
				int rockDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 1, 0f, 0f, 100, default(Color), 3f);
				Main.dust[rockDust].noGravity = true;
				Dust obj2 = Main.dust[rockDust];
				obj2.velocity *= 5f;
				rockDust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 8, 0f, 0f, 100, default(Color), 2f);
				Dust obj3 = Main.dust[rockDust];
				obj3.velocity *= 2f;
			}
			return;
		}
		for (int k = 0; k < 2; k++)
		{
			int iceFlame2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 8, 0f, 0f, 100, default(Color), 2f);
			Dust obj4 = Main.dust[iceFlame2];
			obj4.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[iceFlame2].scale = 0.5f;
				Main.dust[iceFlame2].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int l = 0; l < 2; l++)
		{
			int rockDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 1, 0f, 0f, 100, default(Color), 3f);
			Main.dust[rockDust2].noGravity = true;
			Dust obj5 = Main.dust[rockDust2];
			obj5.velocity *= 5f;
			rockDust2 = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 8, 0f, 0f, 100, default(Color), 2f);
			Dust obj6 = Main.dust[rockDust2];
			obj6.velocity *= 2f;
		}
	}

	public override void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
	{
		if (item.pick > 0)
		{
			modifiers.FlatBonusDamage += -10000f;
			modifiers.FinalDamage.Flat += item.pick - 1;
			modifiers.SetCrit();
		}
		else
		{
			modifiers.SetMaxDamage(1);
			modifiers.DisableCrit();
			modifiers.HideCombatText();
		}
		base.ModifyHitByItem(player, item, ref modifiers);
	}

	public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
	{
		Item item = Main.player[projectile.owner].HeldItem;
		if (item.pick > 0 && projectile.CountsAsClass<MeleeDamageClass>())
		{
			modifiers.FlatBonusDamage += -10000f;
			modifiers.FinalDamage.Flat += item.pick - 1;
			modifiers.SetCrit();
		}
		else
		{
			modifiers.SetMaxDamage(1);
			modifiers.DisableCrit();
			modifiers.HideCombatText();
		}
		base.ModifyHitByProjectile(projectile, ref modifiers);
	}
}
