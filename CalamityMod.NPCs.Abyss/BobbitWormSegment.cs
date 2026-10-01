using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.Abyss;

[LongDistanceNetSync(SyncWith = typeof(BobbitWormHead))]
public class BobbitWormSegment : ModNPC
{
	public override LocalizedText DisplayName => CalamityUtils.GetText("NPCs.BobbitWormHead.DisplayName");

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
	}

	public override void SetDefaults()
	{
		base.NPC.lavaImmune = true;
		base.NPC.aiStyle = -1;
		base.NPC.damage = 0;
		base.NPC.alpha = 255;
		base.NPC.width = 26;
		base.NPC.height = 26;
		base.NPC.defense = 0;
		base.NPC.lifeMax = 100;
		base.NPC.knockBackResist = 0f;
		base.AIType = -1;
		base.NPC.dontTakeDamage = true;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
	}

	public override void AI()
	{
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.ai[0] == 0f)
		{
			for (int i = 0; i < CalamityGlobalNPC.bobbitWormBottom.Length; i++)
			{
				if (CalamityGlobalNPC.bobbitWormBottom[i] == -1)
				{
					CalamityGlobalNPC.bobbitWormBottom[i] = base.NPC.whoAmI;
					base.NPC.ai[0] = i;
					break;
				}
			}
		}
		if (base.NPC.ai[1] == 0f)
		{
			base.NPC.ai[1] = 1f;
			if (Main.netMode != 1)
			{
				int spawnedNPC = NPC.NewNPC(base.NPC.GetSource_FromAI(), (int)base.NPC.Center.X, (int)base.NPC.Center.Y, ModContent.NPCType<BobbitWormHead>(), base.NPC.whoAmI);
				Main.npc[spawnedNPC].ai[2] = CalamityGlobalNPC.bobbitWormBottom[(int)base.NPC.ai[0]];
				base.NPC.ai[2] = spawnedNPC;
			}
		}
		if (!Main.npc[(int)base.NPC.ai[2]].active || Main.npc[(int)base.NPC.ai[2]].life <= 0)
		{
			base.NPC.active = false;
			base.NPC.netUpdate = true;
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.Player.Calamity().ZoneAbyssLayer4 && spawnInfo.Water && CalamityGlobalNPC.bobbitWormBottom.Contains(-1))
		{
			if (!Main.remixWorld)
			{
				return SpawnCondition.CaveJellyfish.Chance * 0.85f;
			}
			return 8.25f;
		}
		return 0f;
	}

	public override bool CheckActive()
	{
		return false;
	}
}
