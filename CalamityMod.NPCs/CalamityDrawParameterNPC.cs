using System.Collections.Generic;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.NPCs.ExoMechs.Ares;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.NPCs;

public sealed class CalamityDrawParameterNPC : GlobalNPC
{
	private sealed class PostUpdateNPCsHook : ModSystem
	{
		public override void PostUpdateNPCs()
		{
			CalamityDrawParameterNPC.PostUpdateNPCs();
		}
	}

	public override bool InstancePerEntity => false;

	public static bool[] DrawingMiracleBlight { get; private set; }

	public static bool[] DrawingPolarity { get; private set; }

	public static int DoGDeathAnimationTimer { get; private set; }

	public static List<int> MiracleBlightExcludedNPCs => new List<int>
	{
		ModContent.NPCType<AresBody>(),
		ModContent.NPCType<AresGaussNuke>(),
		ModContent.NPCType<AresLaserCannon>(),
		ModContent.NPCType<AresPlasmaFlamethrower>(),
		ModContent.NPCType<AresTeslaCannon>(),
		398,
		397,
		396
	};

	public override void Load()
	{
		DrawingMiracleBlight = new bool[Main.maxNPCs + 1];
		DrawingPolarity = new bool[Main.maxNPCs + 1];
	}

	public override void Unload()
	{
		DrawingMiracleBlight = null;
		DrawingPolarity = null;
	}

	public override void SetDefaults(NPC entity)
	{
		ResetParameters(entity);
	}

	public static void ResetParameters(NPC npc)
	{
		if (npc != null && npc.whoAmI >= 0 && npc.whoAmI < Main.maxNPCs)
		{
			int whoAmI = npc.whoAmI;
			DrawingMiracleBlight[whoAmI] = false;
			DrawingPolarity[whoAmI] = false;
		}
	}

	public override bool PreAI(NPC npc)
	{
		if (npc == null)
		{
			return true;
		}
		if (npc.whoAmI < 0 || npc.whoAmI >= Main.maxNPCs)
		{
			return true;
		}
		int whoAmI = npc.whoAmI;
		DrawingMiracleBlight[whoAmI] = ShouldDrawMiracleBlight(npc);
		DrawingPolarity[whoAmI] = ShouldDrawPolarity(npc);
		return true;
	}

	public static void PostUpdateNPCs()
	{
		DoGDeathAnimationTimer = GetDoGDeathTimer();
	}

	public static bool ShouldDrawMiracleBlight(NPC npc)
	{
		if (npc == null || !npc.active)
		{
			return false;
		}
		if (npc.type <= 0)
		{
			return false;
		}
		if (npc.ModNPC != null && npc.ModNPC.Mod != CalamityMod.Instance && npc.boss)
		{
			return false;
		}
		if (MiracleBlightExcludedNPCs.Contains(npc.type) || npc.IsABestiaryIconDummy)
		{
			return false;
		}
		if (!npc.TryGetGlobalNPC<CalamityGlobalNPC>(out var calNPC) || !npc.TryGetGlobalNPC<CalamityPolarityNPC>(out var polNPC))
		{
			return false;
		}
		if (!calNPC.miracleBlight || polNPC.CurPolarity > 0f)
		{
			return false;
		}
		if (Main.LocalPlayer.Calamity().trippy)
		{
			return false;
		}
		return true;
	}

	public static bool ShouldDrawPolarity(NPC npc)
	{
		if (npc == null || !npc.active)
		{
			return false;
		}
		if (!npc.TryGetGlobalNPC<CalamityGlobalNPC>(out var calNPC) || !npc.TryGetGlobalNPC<CalamityPolarityNPC>(out var polNPC))
		{
			return false;
		}
		if (calNPC.miracleBlight)
		{
			return false;
		}
		if (polNPC.CurPolarity <= 0f)
		{
			return false;
		}
		return true;
	}

	public static bool ShouldDrawDoGDeathAnimation(NPC npc)
	{
		if (npc == null || !npc.active || npc.type <= 0)
		{
			return false;
		}
		if (npc.type != ModContent.NPCType<DevourerofGodsHead>() && npc.type != ModContent.NPCType<DevourerofGodsBody>() && npc.type != ModContent.NPCType<DevourerofGodsTail>())
		{
			return false;
		}
		if (GetDoGDeathTimer() <= 0)
		{
			return false;
		}
		return true;
	}

	public static int GetDoGDeathTimer()
	{
		if (!Main.npc.IndexInRange(CalamityGlobalNPC.DoGHead))
		{
			return 0;
		}
		DevourerofGodsHead head = Main.npc[CalamityGlobalNPC.DoGHead].ModNPC<DevourerofGodsHead>();
		if (head == null)
		{
			return 0;
		}
		if (head.DeathAnimationTimer <= 0)
		{
			return 0;
		}
		return head.DeathAnimationTimer;
	}
}
