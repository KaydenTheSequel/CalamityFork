using System.IO;
using CalamityMod.Events;
using CalamityMod.Projectiles.Boss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Leviathan;

public class AnahitasIceShield : ModNPC
{
	public bool WaitingForLeviathan
	{
		get
		{
			if (Main.npc.IndexInRange(CalamityGlobalNPC.leviathan) && (float)Main.npc[CalamityGlobalNPC.leviathan].life / (float)Main.npc[CalamityGlobalNPC.leviathan].lifeMax >= ((CalamityWorld.death || BossRushEvent.BossRushActive) ? 0.7f : 0.4f))
			{
				return true;
			}
			return CalamityUtils.FindFirstProjectile(ModContent.ProjectileType<LeviathanSpawner>()) != -1;
		}
	}

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 50;
		base.NPC.aiStyle = -1;
		base.AIType = -1;
		base.NPC.noTileCollide = true;
		base.NPC.coldDamage = true;
		base.NPC.width = 100;
		base.NPC.height = 100;
		base.NPC.defense = 10;
		base.NPC.DR_NERD(0.25f);
		base.NPC.lifeMax = (BossRushEvent.BossRushActive ? 2000 : 750);
		base.NPC.alpha = 255;
		base.NPC.HitSound = SoundID.NPCHit5;
		base.NPC.DeathSound = SoundID.NPCDeath7;
		base.NPC.Calamity().VulnerableToHeat = true;
		base.NPC.Calamity().VulnerableToCold = false;
		base.NPC.Calamity().VulnerableToSickness = false;
		if (Main.getGoodWorld)
		{
			base.NPC.scale *= 0.8f;
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.NPC.dontTakeDamage);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.NPC.dontTakeDamage = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		int anahitaID = (int)base.NPC.ai[0];
		if (Main.npc[anahitaID].active && Main.npc[anahitaID].type == ModContent.NPCType<Anahita>())
		{
			if (base.NPC.alpha > 100 && base.NPC.ai[1] == 0f)
			{
				base.NPC.alpha -= 2;
			}
			if (WaitingForLeviathan)
			{
				base.NPC.ai[1] = 1f;
			}
			else
			{
				base.NPC.ai[1] = 0f;
			}
			if (base.NPC.ai[1] == 1f)
			{
				base.NPC.alpha = Main.npc[anahitaID].alpha;
			}
			base.NPC.dontTakeDamage = WaitingForLeviathan;
			base.NPC.rotation = Main.npc[anahitaID].rotation;
			base.NPC.spriteDirection = Main.npc[anahitaID].direction;
			base.NPC.velocity = Vector2.Zero;
			base.NPC.position = Main.npc[anahitaID].Center;
			base.NPC.position.X = base.NPC.position.X - (float)(base.NPC.width / 2) + ((base.NPC.spriteDirection == 1) ? (-20f) : 20f) * base.NPC.scale;
			base.NPC.position.Y = base.NPC.position.Y - (float)(base.NPC.height / 2) - (float)(int)(30f * base.NPC.scale);
			base.NPC.gfxOffY = Main.npc[anahitaID].gfxOffY;
			Lighting.AddLight((int)base.NPC.Center.X / 16, (int)base.NPC.Center.Y / 16, 0f, 0.8f, 1.1f);
		}
		else
		{
			base.NPC.dontTakeDamage = false;
			base.NPC.life = 0;
			base.NPC.HitEffect();
			base.NPC.active = false;
			base.NPC.netUpdate = true;
		}
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		if (base.NPC.ai[1] == 0f)
		{
			return base.NPC.alpha <= 100;
		}
		return false;
	}

	public override Color? GetAlpha(Color drawColor)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		return (base.NPC.ai[1] == 1f) ? Color.Transparent : (new Color(200, 200, 200, (int)((Color)(ref drawColor)).A) * base.NPC.Opacity);
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 67, hit.HitDirection, -1f);
		}
		if (base.NPC.life <= 0)
		{
			for (int i = 0; i < 25; i++)
			{
				Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, 67, hit.HitDirection, -1f);
			}
		}
	}
}
