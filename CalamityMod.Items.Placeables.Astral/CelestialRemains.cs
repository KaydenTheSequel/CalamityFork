using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Tiles.AstralDesert;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Astral;

[LegacyName(new string[] { "AstralFossil" })]
public class CelestialRemains : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 200;
		ItemID.Sets.ExtractinatorMode[base.Type] = base.Type;
		ItemID.Sets.SortingPriorityExtractibles[base.Type] = 3;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.AstralDesert.CelestialRemains>());
	}

	public override void ExtractinatorUse(int extractinatorBlockType, ref int resultType, ref int resultStack)
	{
		bool twoMechsDowned = (NPC.downedMechBoss1 && NPC.downedMechBoss2 && !NPC.downedMechBoss3) || (NPC.downedMechBoss2 && NPC.downedMechBoss3 && !NPC.downedMechBoss1) || (NPC.downedMechBoss3 && NPC.downedMechBoss1 && !NPC.downedMechBoss2) || (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3);
		float val = Main.rand.NextFloat(100f);
		if (val < 20f)
		{
			resultType = 71;
			resultStack = Main.rand.Next(1, 100);
		}
		else if (val < 27f)
		{
			resultType = 72;
			resultStack = Main.rand.Next(1, 100);
		}
		else if (val < 28f)
		{
			resultType = 73;
			resultStack = Main.rand.Next(1, 100);
		}
		else if (val < 28.03f)
		{
			resultType = 74;
			resultStack = Main.rand.Next(1, 11);
		}
		else if (val < 48.03f)
		{
			resultType = ModContent.ItemType<AncientBoneDust>();
			resultStack = Main.rand.Next(1, 11);
		}
		else if (val < 58.03f && !Main.dayTime)
		{
			resultType = 75;
			resultStack = Main.rand.Next(1, 11);
		}
		else if (val < 58.03f && !Main.dayTime)
		{
			resultType = 75;
			resultStack = Main.rand.Next(1, 11);
		}
		else if (val < 68.03f)
		{
			resultType = ModContent.ItemType<StarblightSoot>();
			resultStack = Main.rand.Next(1, 11);
		}
		else if (val < 69.03f)
		{
			resultType = 182;
			resultStack = Main.rand.Next(1, 21);
		}
		else if (val < 70.03f)
		{
			resultType = 178;
			resultStack = Main.rand.Next(1, 21);
		}
		else if (val < 71.03f)
		{
			resultType = 180;
			resultStack = Main.rand.Next(1, 21);
		}
		else if (val < 72.03f)
		{
			resultType = 179;
			resultStack = Main.rand.Next(1, 21);
		}
		else if (val < 73.03f)
		{
			resultType = 177;
			resultStack = Main.rand.Next(1, 21);
		}
		else if (val < 74.03f)
		{
			resultType = 181;
			resultStack = Main.rand.Next(1, 21);
		}
		else if (val < 79.03f)
		{
			resultType = 999;
			resultStack = Main.rand.Next(1, 21);
		}
		else if (val < 81.78f && Main.hardMode)
		{
			resultType = 1104;
			resultStack = Main.rand.Next(1, 17);
		}
		else if (val < 83.03f && Main.hardMode)
		{
			resultType = 364;
			resultStack = Main.rand.Next(1, 17);
		}
		else if (val < 85.03f && Main.hardMode)
		{
			resultType = ((!CalamityServerConfig.Instance.EarlyHardmodeProgressionRework) ? 365 : ((!NPC.downedMechBossAny) ? 364 : 365));
			resultStack = Main.rand.Next(1, 17);
		}
		else if (val < 87.03f && Main.hardMode)
		{
			resultType = ((!CalamityServerConfig.Instance.EarlyHardmodeProgressionRework) ? 1105 : ((!NPC.downedMechBossAny) ? 1104 : 1105));
			resultStack = Main.rand.Next(1, 17);
		}
		else if (val < 88.78f && Main.hardMode)
		{
			resultType = ((!CalamityServerConfig.Instance.EarlyHardmodeProgressionRework) ? 366 : ((!NPC.downedMechBossAny) ? 364 : ((!twoMechsDowned) ? 365 : 366)));
			resultStack = Main.rand.Next(1, 17);
		}
		else if (val < 89.53f && Main.hardMode)
		{
			resultType = ((!CalamityServerConfig.Instance.EarlyHardmodeProgressionRework) ? 1106 : ((!NPC.downedMechBossAny) ? 1104 : ((!twoMechsDowned) ? 1105 : 1106)));
			resultStack = Main.rand.Next(1, 17);
		}
		else if (DownedBossSystem.downedAstrumDeus)
		{
			resultType = ModContent.ItemType<AstralOre>();
			resultStack = Main.rand.Next(1, 2);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CelestialRemainsWall>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
