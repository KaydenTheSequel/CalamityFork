using System.IO;
using CalamityMod.NPCs.VanillaNPCAIOverrides.RegularEnemies;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.NormalNPCs;

public class SuperDummyNPC : ModNPC
{
	public int deathCounter;

	public RevengeanceAndDeathAI.MimicAI ZenithSeedMimicAI;

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
		Main.npcFrameCount[base.Type] = 11;
		NPCID.Sets.CantTakeLunchMoney[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.NPC.width = 18;
		base.NPC.height = 48;
		base.NPC.damage = 0;
		base.NPC.defense = 0;
		base.NPC.lifeMax = 9999999;
		base.NPC.HitSound = null;
		base.NPC.DeathSound = SoundID.NPCDeath2;
		base.NPC.knockBackResist = 0f;
		base.NPC.netAlways = true;
		base.NPC.aiStyle = 0;
		ZenithSeedMimicAI = new RevengeanceAndDeathAI.MimicAI();
		ZenithSeedMimicAI.NPC = base.NPC;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(deathCounter);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		deathCounter = reader.ReadInt32();
	}

	public override bool PreAI()
	{
		if (Main.zenithWorld)
		{
			deathCounter++;
			if (deathCounter >= 6000)
			{
				base.NPC.damage = base.NPC.lifeMax;
				ZenithSeedMimicAI.AI(base.Mod);
				return false;
			}
		}
		return true;
	}

	public override void UpdateLifeRegen(ref int damage)
	{
		if (base.NPC.lifeRegen >= 0)
		{
			base.NPC.lifeRegen += 2000000;
		}
	}

	public override bool CanHitPlayer(Player target, ref int cooldownSlot)
	{
		return Main.zenithWorld;
	}

	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.localAI[0] = hit.Damage;
		if (base.NPC.localAI[0] < 20f)
		{
			base.NPC.localAI[0] = 20f;
		}
		if (base.NPC.localAI[0] > 120f)
		{
			base.NPC.localAI[0] = 120f;
		}
		base.NPC.localAI[1] = hit.HitDirection;
		if (deathCounter > 0 && deathCounter < 6000)
		{
			deathCounter = 0;
		}
		SoundStyle toPlay = Main.rand.Next(3) switch
		{
			0 => SoundID.NPCHit15, 
			1 => SoundID.NPCHit16, 
			2 => SoundID.NPCHit17, 
			_ => SoundID.NPCHit15, 
		};
		if (base.NPC.soundDelay <= 0)
		{
			SoundEngine.PlaySound(in toPlay, base.NPC.Center);
		}
	}

	public override void FindFrame(int frameHeight)
	{
		int hitDirection = (int)base.NPC.localAI[1];
		if (base.NPC.direction == 1)
		{
			hitDirection *= -1;
		}
		if (base.NPC.localAI[0] > 24f)
		{
			base.NPC.localAI[0] = 24f;
		}
		if (base.NPC.localAI[0] > 0f)
		{
			base.NPC.localAI[0]--;
		}
		if (base.NPC.localAI[0] < 0f)
		{
			base.NPC.localAI[0] = 0f;
		}
		int animationSpeed = ((hitDirection == -1) ? 4 : 6);
		int currentFrame = (int)base.NPC.localAI[0] / animationSpeed;
		if (base.NPC.localAI[0] % (float)animationSpeed != 0f)
		{
			currentFrame++;
		}
		if (currentFrame != 0 && hitDirection == 1)
		{
			currentFrame += 5;
		}
		base.NPC.frame.Y = currentFrame * frameHeight;
	}

	public override bool CheckDead()
	{
		if (base.NPC.lifeRegen < 0)
		{
			base.NPC.life = base.NPC.lifeMax;
			return false;
		}
		return true;
	}
}
