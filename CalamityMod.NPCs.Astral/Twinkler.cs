using CalamityMod.BiomeManagers;
using CalamityMod.Dusts;
using CalamityMod.Items.Critters;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace CalamityMod.NPCs.Astral;

public class Twinkler : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 8;
		Main.npcCatchable[base.Type] = true;
		NPCID.Sets.CountsAsCritter[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.NPC.CloneDefaults(358);
		base.NPC.width = 7;
		base.NPC.height = 5;
		base.AIType = 358;
		base.AnimationType = 358;
		base.NPC.catchItem = (short)ModContent.ItemType<TwinklerItem>();
		base.SpawnModBiomes = new int[1] { ModContent.GetInstance<AstralInfectionBiome>().Type };
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Twinkler")
		});
	}

	public override bool? CanBeHitByItem(Player player, Item item)
	{
		return null;
	}

	public override bool? CanBeHitByProjectile(Projectile projectile)
	{
		return null;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.life > 0)
		{
			return;
		}
		for (int i = 0; i < 6; i++)
		{
			int dust = Dust.NewDust(base.NPC.position, base.NPC.width, base.NPC.height, ModContent.DustType<AstralOrange>(), 2 * hit.HitDirection, -2f);
			if (Main.rand.NextBool())
			{
				Main.dust[dust].noGravity = true;
				Main.dust[dust].scale = 1.2f * base.NPC.scale;
			}
			else
			{
				Main.dust[dust].scale = 0.7f * base.NPC.scale;
			}
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (CalamityGlobalNPC.AnyEvents(spawnInfo.Player))
		{
			return 0f;
		}
		if (spawnInfo.Player.InAstral())
		{
			return SpawnCondition.TownCritter.Chance;
		}
		return 0f;
	}
}
