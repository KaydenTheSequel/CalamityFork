using CalamityMod.BiomeManagers;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Placeables.Banners;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.Abyss;

public class ChaoticPuffer : ModNPC
{
	public bool puffedUp;

	public bool puffing;

	public bool unpuffing;

	public int puffTimer;

	public int puffingTimer;

	public static Asset<Texture2D> GlowTexture;

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 11;
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.noGravity = true;
		base.NPC.lavaImmune = true;
		base.NPC.width = 78;
		base.NPC.height = 78;
		base.NPC.defense = 50;
		base.NPC.lifeMax = 5600;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0f;
		base.NPC.value = Item.buyPrice(0, 0, 30);
		base.NPC.HitSound = SoundID.NPCHit23;
		base.NPC.DeathSound = SoundID.NPCDeath28;
		base.Banner = base.NPC.type;
		base.BannerItem = ModContent.ItemType<ChaoticPufferBanner>();
		base.NPC.Calamity().VulnerableToHeat = false;
		base.NPC.Calamity().VulnerableToSickness = true;
		base.NPC.Calamity().VulnerableToElectricity = true;
		base.NPC.Calamity().VulnerableToWater = false;
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AbyssLayer3Biome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.ChaoticPuffer")
		});
	}

	public override void AI()
	{
		base.NPC.TargetClosest();
		base.NPC.velocity.X = base.NPC.velocity.X + (float)base.NPC.direction * 0.03f;
		base.NPC.velocity.Y = base.NPC.velocity.Y + (float)base.NPC.directionY * 0.03f;
		base.NPC.damage = (puffedUp ? (Main.expertMode ? 175 : 100) : 0);
		if (!puffing || !unpuffing)
		{
			puffTimer++;
		}
		if (puffTimer >= 300)
		{
			if (!puffedUp)
			{
				puffing = true;
			}
			else
			{
				unpuffing = true;
			}
			puffTimer = 0;
		}
		else if (puffing || unpuffing)
		{
			puffingTimer++;
			if (puffingTimer > 16 && puffing)
			{
				puffing = false;
				puffedUp = true;
				puffingTimer = 0;
			}
			else if (puffingTimer > 16 && unpuffing)
			{
				unpuffing = false;
				puffedUp = false;
				puffingTimer = 0;
			}
		}
		if (base.NPC.velocity.X >= 1f || base.NPC.velocity.X <= -1f)
		{
			base.NPC.velocity.X = base.NPC.velocity.X * 0.97f;
		}
		if (base.NPC.velocity.Y >= 1f || base.NPC.velocity.Y <= -1f)
		{
			base.NPC.velocity.Y = base.NPC.velocity.Y * 0.97f;
		}
		base.NPC.rotation += base.NPC.velocity.X * 0.05f;
	}

	public void Boom()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.NPCDeath14, base.NPC.Center);
		if (Main.netMode != 1 && puffedUp)
		{
			int damageBoom = (Main.masterMode ? 30 : (Main.expertMode ? 35 : 45));
			int projectileType = ModContent.ProjectileType<PufferExplosion>();
			Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center.X, base.NPC.Center.Y, 0f, 0f, projectileType, damageBoom, 0f, Main.myPlayer);
		}
		base.NPC.netUpdate = true;
	}

	public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		if (!base.NPC.IsABestiaryIconDummy)
		{
			SpriteEffects effects = (SpriteEffects)(base.NPC.direction != -1);
			Main.EntitySpriteDraw(GlowTexture.Value, base.NPC.Center - Main.screenPosition + new Vector2(0f, base.NPC.gfxOffY + 4f), base.NPC.frame, Color.White * 0.5f, base.NPC.rotation, base.NPC.frame.Size() / 2f, base.NPC.scale, effects);
		}
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter > 6.0)
		{
			base.NPC.frameCounter = 0.0;
			if (!unpuffing)
			{
				base.NPC.frame.Y = base.NPC.frame.Y + frameHeight;
			}
			else
			{
				base.NPC.frame.Y = base.NPC.frame.Y - frameHeight;
			}
		}
		if (base.NPC.IsABestiaryIconDummy)
		{
			if (base.NPC.frame.Y < frameHeight * 7)
			{
				base.NPC.frame.Y = frameHeight * 7;
			}
			if (base.NPC.frame.Y > frameHeight * 10)
			{
				base.NPC.frame.Y = frameHeight * 7;
			}
		}
		else if (puffing)
		{
			if (base.NPC.frame.Y < frameHeight * 3)
			{
				base.NPC.frame.Y = frameHeight * 3;
			}
			if (base.NPC.frame.Y > frameHeight * 6)
			{
				base.NPC.frame.Y = frameHeight * 3;
			}
		}
		else if (unpuffing)
		{
			if (base.NPC.frame.Y > frameHeight * 6)
			{
				base.NPC.frame.Y = frameHeight * 6;
			}
			if (base.NPC.frame.Y < frameHeight * 3)
			{
				base.NPC.frame.Y = frameHeight * 6;
			}
		}
		else if (!puffedUp)
		{
			if (base.NPC.frame.Y > frameHeight * 3)
			{
				base.NPC.frame.Y = 0;
			}
		}
		else
		{
			if (base.NPC.frame.Y < frameHeight * 7)
			{
				base.NPC.frame.Y = frameHeight * 7;
			}
			if (base.NPC.frame.Y > frameHeight * 10)
			{
				base.NPC.frame.Y = frameHeight * 7;
			}
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.Player.Calamity().ZoneAbyssLayer3 && spawnInfo.Water)
		{
			if (!Main.remixWorld)
			{
				return SpawnCondition.CaveJellyfish.Chance * 0.6f;
			}
			return 5.4f;
		}
		return 0f;
	}

	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.AddIf(() => NPC.downedGolemBoss, ModContent.ItemType<ScoriaOre>(), 1, 10, 26);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		if (hurtInfo.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 180);
		}
		if (puffedUp)
		{
			Boom();
			base.NPC.active = false;
		}
	}

	public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
	{
		base.NPC.velocity.X = projectile.velocity.X;
		base.NPC.velocity.Y = projectile.velocity.Y;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			Boom();
			for (int i = 0; i < 15; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 5, hit.HitDirection, -1f);
			}
		}
	}
}
