using System;
using CalamityMod.Events;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.NormalNPCs;

public class PlanterasFreeTentacle : ModNPC
{
	public override string Texture => $"Terraria/Images/NPC_{264}";

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 4;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Hide = true;
		NPCID.Sets.NPCBestiaryDrawModifiers bestiaryData = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset.Add(base.Type, bestiaryData);
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 60;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.width = 24;
		base.NPC.height = 24;
		base.NPC.defense = 20;
		base.NPC.lifeMax = 500;
		base.NPC.knockBackResist = 0.4f;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.NPC.Calamity().VulnerableToCold = true;
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToSickness = true;
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter >= 6.0)
		{
			base.NPC.frame.Y += frameHeight;
			base.NPC.frameCounter = 0.0;
		}
		if (base.NPC.frame.Y >= frameHeight * Main.npcFrameCount[base.Type])
		{
			base.NPC.frame.Y = 0;
		}
	}

	public override void AI()
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.target < 0 || base.NPC.target == 255 || Main.player[base.NPC.target].dead || !Main.player[base.NPC.target].active)
		{
			base.NPC.CalamityTargeting(default(CalamityTargetingParameters));
		}
		Lighting.AddLight(base.NPC.Center, 0.2f, 0.4f, 0.1f);
		if (Main.rand.NextBool(10))
		{
			Dust.NewDustDirect(base.NPC.position, base.NPC.width, base.NPC.height, 44, 0f, 0f, 250, default(Color), 0.4f).fadeIn = 0.7f;
		}
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		if (Main.getGoodWorld)
		{
			if (Main.rand.NextBool(5))
			{
				base.NPC.reflectsProjectiles = true;
			}
			else
			{
				base.NPC.reflectsProjectiles = false;
			}
		}
		if (NPC.plantBoss < 0)
		{
			if (Main.netMode != 1)
			{
				base.NPC.StrikeInstantKill();
			}
			return;
		}
		if (base.NPC.ai[0] > 0f)
		{
			base.NPC.knockBackResist = 0f;
			base.NPC.ai[0]--;
			if (((Vector2)(ref base.NPC.velocity)).Length() < base.NPC.ai[1])
			{
				NPC nPC = base.NPC;
				nPC.velocity *= 1.01f;
				if (((Vector2)(ref base.NPC.velocity)).Length() > base.NPC.ai[1])
				{
					((Vector2)(ref base.NPC.velocity)).Normalize();
					NPC nPC2 = base.NPC;
					nPC2.velocity *= base.NPC.ai[1];
				}
			}
			if (base.NPC.velocity.X > 0f)
			{
				base.NPC.spriteDirection = 1;
				base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
			}
			if (base.NPC.velocity.X < 0f)
			{
				base.NPC.spriteDirection = -1;
				base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + (float)Math.PI;
			}
			return;
		}
		base.NPC.knockBackResist = 0.4f;
		Vector2 idealVelocity = base.NPC.SafeDirectionTo(Main.player[base.NPC.target].Center) * (death ? 7f : 5.5f);
		float acceleration = (death ? 0.14f : 0.105f);
		if (Main.getGoodWorld)
		{
			idealVelocity *= 1.2f;
			acceleration *= 1.4f;
		}
		base.NPC.SimpleFlyMovement(idealVelocity, acceleration);
		if (base.NPC.wet)
		{
			if (base.NPC.velocity.Y > 0f)
			{
				base.NPC.velocity.Y *= 0.95f;
			}
			base.NPC.velocity.Y -= 0.5f;
			if (base.NPC.velocity.Y < -4f)
			{
				base.NPC.velocity.Y = -4f;
			}
			base.NPC.TargetClosest();
		}
		float pushVelocity = 0.5f;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (n.whoAmI != base.NPC.whoAmI && n.type == base.NPC.type && Vector2.Distance(base.NPC.Center, n.Center) < 40f * base.NPC.scale)
			{
				if (base.NPC.position.X < n.position.X)
				{
					base.NPC.velocity.X -= pushVelocity;
				}
				else
				{
					base.NPC.velocity.X += pushVelocity;
				}
				if (base.NPC.position.Y < n.position.Y)
				{
					base.NPC.velocity.Y -= pushVelocity;
				}
				else
				{
					base.NPC.velocity.Y += pushVelocity;
				}
			}
		}
		if (base.NPC.velocity.X > 0f)
		{
			base.NPC.spriteDirection = 1;
			base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X);
		}
		if (base.NPC.velocity.X < 0f)
		{
			base.NPC.spriteDirection = -1;
			base.NPC.rotation = (float)Math.Atan2(base.NPC.velocity.Y, base.NPC.velocity.X) + (float)Math.PI;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = (int)((float)base.NPC.lifeMax * balance);
		base.NPC.damage = (int)((float)base.NPC.damage * 1.15f);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.life > 0)
		{
			for (int i = 0; (double)i < (double)hit.Damage / (double)base.NPC.lifeMax * 100.0; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 167, hit.HitDirection, -1f);
			}
			return;
		}
		for (int j = 0; j < 150; j++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 167, 2 * hit.HitDirection, -2f);
		}
		if (!Main.dedServ)
		{
			Gore.NewGore(base.NPC.GetSource_Death(), new Vector2(base.NPC.position.X + (float)Main.rand.Next(base.NPC.width), base.NPC.position.Y + (float)Main.rand.Next(base.NPC.height)), base.NPC.velocity, 388, base.NPC.scale);
			Gore.NewGore(base.NPC.GetSource_Death(), new Vector2(base.NPC.position.X + (float)Main.rand.Next(base.NPC.width), base.NPC.position.Y + (float)Main.rand.Next(base.NPC.height)), base.NPC.velocity, 389, base.NPC.scale);
		}
	}
}
