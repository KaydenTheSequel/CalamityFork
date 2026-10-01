using System;
using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.AcidRain;

public class GammaSlime : ModNPC
{
	public float DustAngleMultiplier1;

	public float DustAngleMultiplier2;

	public static Asset<Texture2D> GlowTexture;

	public Player Target => Main.player[base.NPC.target];

	public float LaserTelegraphPower => Utils.GetLerpValue(540f, 480f, LaserShootCountdown, clamped: true);

	public float LaserTelegraphLength => MathHelper.Lerp(20f, 550f, LaserTelegraphPower);

	public float LaserTelegraphOpacity => MathHelper.Lerp(0.3f, 0.9f, LaserTelegraphPower);

	public ref float GammaAcidShootTimer => ref base.NPC.ai[0];

	public ref float LaserShootCountdown => ref base.NPC.ai[1];

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 2;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.width = 40;
		base.NPC.height = 44;
		base.NPC.damage = 110;
		base.NPC.lifeMax = 5000;
		base.NPC.defense = 25;
		NPC nPC = base.NPC;
		int aiStyle = (base.AIType = -1);
		nPC.aiStyle = aiStyle;
		base.NPC.knockBackResist = 0f;
		base.AnimationType = 81;
		base.NPC.value = Item.buyPrice(0, 0, 10);
		base.NPC.alpha = 50;
		base.NPC.lavaImmune = false;
		base.NPC.noGravity = false;
		base.NPC.noTileCollide = false;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<GammaSlimeBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = false;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AcidRainBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.GammaSlime")
		});
	}

	public override void AI()
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05df: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_061a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0621: Unknown result type (might be due to invalid IL or missing references)
		//IL_062b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0630: Unknown result type (might be due to invalid IL or missing references)
		//IL_0643: Unknown result type (might be due to invalid IL or missing references)
		//IL_0649: Unknown result type (might be due to invalid IL or missing references)
		//IL_0656: Unknown result type (might be due to invalid IL or missing references)
		//IL_0658: Unknown result type (might be due to invalid IL or missing references)
		//IL_065d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_068e: Unknown result type (might be due to invalid IL or missing references)
		//IL_069e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.damage = ((base.NPC.velocity.Y != 0f && !(((Vector2)(ref base.NPC.velocity)).Length() < 3f)) ? base.NPC.defDamage : 0);
		Lighting.AddLight((int)base.NPC.Center.X / 16, (int)base.NPC.Center.Y / 16, 0.6f, 0.8f, 0.6f);
		base.NPC.TargetClosest(faceTarget: false);
		if (base.NPC.velocity.Y == 0f && LaserShootCountdown <= 0f && !Target.npcTypeNoAggro[base.Type])
		{
			base.NPC.velocity.X *= 0.8f;
			GammaAcidShootTimer++;
			if (GammaAcidShootTimer % 30f == 29f)
			{
				base.NPC.velocity.Y -= MathHelper.Clamp(Math.Abs(Target.Center.Y - base.NPC.Center.Y) / 16f, 5f, 15f);
				base.NPC.velocity.X = base.NPC.SafeDirectionTo(Target.Center).X * 16f;
				if (Main.netMode != 1)
				{
					for (int i = 0; i < 5; i++)
					{
						float angle = (float)Math.PI * 2f / 5f * (float)i;
						if (GammaAcidShootTimer % 60f == 58f)
						{
							angle += (float)Math.PI / 2f;
						}
						Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, angle.ToRotationVector2() * 7f, ModContent.ProjectileType<GammaAcid>(), Main.masterMode ? 30 : (Main.expertMode ? 36 : 45), 3f);
					}
				}
				base.NPC.netUpdate = true;
			}
		}
		else
		{
			base.NPC.velocity.X *= 0.9935f;
		}
		if (LaserShootCountdown > 0f)
		{
			LaserShootCountdown--;
		}
		if (LaserShootCountdown > 240f)
		{
			base.NPC.velocity.X *= 0.95f;
			base.NPC.velocity.Y += 0.2f;
		}
		if (LaserShootCountdown > 480f)
		{
			float scale = ((LaserShootCountdown < 530f) ? 2.25f : 1.65f);
			Vector2 destination = base.NPC.Top + new Vector2(0f, 6f);
			Dust dust = Dust.NewDustPerfect(destination + Main.rand.NextVector2CircularEdge(12f, 12f), 75);
			dust.velocity = Vector2.Normalize(destination - dust.position) * 3f;
			dust.scale = scale;
			dust.noGravity = true;
			if (LaserShootCountdown <= 540f)
			{
				for (float i2 = base.NPC.Top.Y + 4f; i2 >= base.NPC.Top.Y + 4f - LaserTelegraphLength; i2 -= 8f)
				{
					float angle2 = i2 / 24f;
					dust = Dust.NewDustPerfect(new Vector2(base.NPC.Center.X, i2), 75);
					dust.scale = 1.5f;
					dust.velocity = Vector2.UnitX * (float)Math.Cos(angle2) * (1f - LaserTelegraphPower) * 4f;
					dust.noGravity = true;
				}
			}
		}
		if (LaserShootCountdown == 480f)
		{
			DustAngleMultiplier1 = Main.rand.NextFloat(3f);
			DustAngleMultiplier2 = Main.rand.NextFloat(4f);
			if (Main.netMode != 1)
			{
				SoundEngine.PlaySound(in SoundID.Zombie104, base.NPC.Center);
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center, -Vector2.UnitY, ModContent.ProjectileType<GammaBeam>(), Main.masterMode ? 81 : (Main.expertMode ? 96 : 120), 4f, Main.myPlayer, 0f, base.NPC.whoAmI);
			}
			base.NPC.netUpdate = true;
		}
		else if (LaserShootCountdown >= 300f)
		{
			float angle3 = LaserShootCountdown / 30f % ((float)Math.PI * 2f);
			float horizontalSpeed = (float)Math.Sin(angle3 * DustAngleMultiplier1) * (float)Math.Cos(angle3) * 4.5f;
			float verticalSpeed = (float)Math.Cos(angle3 * DustAngleMultiplier1) * (float)Math.Sin(angle3) * 2f;
			Vector2 velocity = default(Vector2);
			((Vector2)(ref velocity))._002Ector(horizontalSpeed, verticalSpeed);
			Dust dust2 = Dust.NewDustPerfect(base.NPC.Center + angle3.ToRotationVector2() * 8f, 75);
			dust2.velocity = velocity;
			dust2.scale = (float)Math.Cos(angle3) + 2f;
			dust2.noGravity = true;
			Dust dust3 = Dust.NewDustPerfect(base.NPC.Center + angle3.ToRotationVector2() * 8f, 75);
			dust3.velocity = -velocity;
			dust3.scale = (float)Math.Cos(angle3) + 2f;
			dust3.noGravity = true;
		}
		if (Main.netMode != 1 && Math.Abs(Target.Center.X - base.NPC.Center.X) < 250f && Target.Center.X < base.NPC.Center.X && LaserShootCountdown == 0f && Main.rand.NextBool(110) && !Target.npcTypeNoAggro[base.Type])
		{
			LaserShootCountdown = 600f;
			base.NPC.netUpdate = true;
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 10; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 75, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 75, hit.HitDirection, -1f);
			}
			if (!Main.dedServ)
			{
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("GammaSlimeGore").Type, base.NPC.scale);
				Gore.NewGore(base.NPC.GetSource_Death(), base.NPC.position, base.NPC.velocity, base.Mod.Find<ModGore>("GammaSlimeGore2").Type, base.NPC.scale);
			}
		}
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		if (LaserShootCountdown >= 480f && LaserShootCountdown <= 540f)
		{
			Vector2 laserBottom = base.NPC.Top + Vector2.UnitY * 4f;
			Vector2 laserTop = laserBottom - Vector2.UnitY * LaserTelegraphLength;
			Utils.DrawLine(spriteBatch, laserBottom, laserTop, Color.Lerp(Color.Lime, Color.Transparent, LaserTelegraphOpacity));
		}
		CalamityGlobalNPC.DrawGlowmask(base.NPC, spriteBatch, GlowTexture.Value);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Irradiated>(), 180);
		}
	}
}
