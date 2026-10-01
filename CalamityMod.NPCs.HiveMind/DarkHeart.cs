using CalamityMod.Events;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.HiveMind;

public class DarkHeart : ModNPC
{
	public override void SetStaticDefaults()
	{
		NPCID.Sets.NeedsExpertScaling[base.Type] = true;
		Main.npcFrameCount[base.Type] = 4;
		NPCID.Sets.BossBestiaryPriority.Add(base.Type);
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 0;
		base.NPC.width = 32;
		base.NPC.height = 32;
		base.NPC.defense = 2;
		base.NPC.lifeMax = 75;
		if (BossRushEvent.BossRushActive)
		{
			base.NPC.lifeMax = 1800;
		}
		if (Main.getGoodWorld)
		{
			base.NPC.lifeMax *= 3;
		}
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.knockBackResist = 0.4f;
		base.NPC.noGravity = true;
		base.NPC.HitSound = SoundID.NPCHit13;
		base.NPC.DeathSound = SoundID.NPCDeath21;
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToSickness = true;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[3]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCorruption,
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.DarkHeart")
		});
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
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		base.NPC.rotation = base.NPC.velocity.X / 20f;
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.TargetClosest();
		}
		if (CalamityGlobalNPC.hiveMind == -1)
		{
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.checkDead();
			base.NPC.active = false;
		}
		float velocity = (Main.getGoodWorld ? 10f : (death ? 7f : (revenge ? 6f : 4f)));
		float acceleration = (Main.getGoodWorld ? 0.5f : (death ? 0.35f : (revenge ? 0.3f : 0.2f)));
		float deceleration = (Main.getGoodWorld ? 0.9f : (death ? 0.95f : (revenge ? 0.96f : 0.98f)));
		if (base.NPC.position.Y > Main.player[base.NPC.target].position.Y - 400f)
		{
			if (base.NPC.velocity.Y > 0f)
			{
				base.NPC.velocity.Y *= deceleration;
			}
			base.NPC.velocity.Y = MathHelper.Clamp(base.NPC.velocity.Y - acceleration, (0f - velocity) * 1.5f, velocity);
		}
		else if (base.NPC.position.Y < Main.player[base.NPC.target].position.Y - 450f)
		{
			if (base.NPC.velocity.Y < 0f)
			{
				base.NPC.velocity.Y *= deceleration;
			}
			base.NPC.velocity.Y = MathHelper.Clamp(base.NPC.velocity.Y + acceleration, 0f - velocity, velocity * 1.5f);
		}
		bool dropRain = base.NPC.Bottom.Y < Main.player[base.NPC.target].position.Y - 350f && Collision.CanHit(base.NPC.position, base.NPC.width, base.NPC.height, Main.player[base.NPC.target].position, Main.player[base.NPC.target].width, Main.player[base.NPC.target].height);
		float distanceX = (death ? 200f : 400f);
		if (base.NPC.Center.X > Main.player[base.NPC.target].Center.X + distanceX)
		{
			dropRain = false;
			if (base.NPC.velocity.X > 0f)
			{
				base.NPC.velocity.X *= deceleration;
			}
			base.NPC.velocity.X = MathHelper.Clamp(base.NPC.velocity.X - acceleration, (0f - velocity) * 1.5f, velocity);
		}
		if (base.NPC.Center.X < Main.player[base.NPC.target].Center.X - distanceX)
		{
			dropRain = false;
			if (base.NPC.velocity.X < 0f)
			{
				base.NPC.velocity.X *= deceleration;
			}
			base.NPC.velocity.X = MathHelper.Clamp(base.NPC.velocity.X + acceleration, 0f - velocity, velocity * 1.5f);
		}
		if (dropRain && Main.netMode != 1)
		{
			base.NPC.ai[0]++;
			float rainDropRate = (Main.getGoodWorld ? 10f : (death ? 15f : (revenge ? 20f : 30f)));
			if (base.NPC.ai[0] >= rainDropRate)
			{
				base.NPC.ai[0] = 0f;
				int shaderainXPos = (int)(base.NPC.position.X + 10f + (float)Main.rand.Next(base.NPC.width - 20));
				int shaderainYos = (int)(base.NPC.position.Y + (float)base.NPC.height + 4f);
				int type = ModContent.ProjectileType<ShaderainHostile>();
				float randomXVelocity = (Main.getGoodWorld ? (Main.rand.NextFloat() * 5f) : 0f);
				float velocityY = 8f;
				Projectile.NewProjectile(base.NPC.GetSource_FromAI(), shaderainXPos, shaderainYos, randomXVelocity, velocityY, type, HiveMind.ShaderainDamage, 0f, Main.myPlayer);
			}
		}
	}

	public override void OnKill()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		int closestPlayer = Player.FindClosest(base.NPC.Center, 1, 1);
		if (Main.player[closestPlayer].statLife < Main.player[closestPlayer].statLifeMax2)
		{
			Item.NewItem(base.NPC.GetSource_Loot(), (int)base.NPC.position.X, (int)base.NPC.position.Y, base.NPC.width, base.NPC.height, 58);
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 14, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 14, hit.HitDirection, -1f);
			}
		}
	}
}
