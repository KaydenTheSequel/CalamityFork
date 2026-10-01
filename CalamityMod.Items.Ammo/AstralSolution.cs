using CalamityMod.Projectiles.Typeless;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Ammo;

public class AstralSolution : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Ammo";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 99;
		ItemID.Sets.SortingPriorityTerraforming[base.Type] = 94;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToSolution(ModContent.ProjectileType<AstralSpray>());
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.Solutions;
	}
}
