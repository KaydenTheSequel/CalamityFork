using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Mounts;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.NPCs.ExoMechs.Apollo;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.TreasureBags;

[LegacyName(new string[] { "DraedonTreasureBag" })]
public class DraedonBag : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.TreasureBags";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 3;
		ItemID.Sets.BossBag[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 24;
		base.Item.height = 24;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.consumable = true;
		base.Item.rare = 9;
		base.Item.expert = true;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossBags;
	}

	public override bool CanRightClick()
	{
		return true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp(lightColor, Color.White, 0.4f);
	}

	public override void PostUpdate()
	{
		CalamityUtils.ForceItemIntoWorld(base.Item);
		base.Item.TreasureBagLightAndDust();
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		return CalamityUtils.DrawTreasureBagInWorld(base.Item, spriteBatch, ref rotation, ref scale, whoAmI);
	}

	public override void ModifyItemLoot(ItemLoot itemLoot)
	{
		itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<Apollo>()));
		Fraction maskFraction = new Fraction(2, 7);
		itemLoot.Add(ModContent.ItemType<ExoPrism>(), 1, 30, 40);
		LeadingConditionRule mainRule = itemLoot.DefineConditionalDropSet(() => DownedBossSystem.downedAres);
		mainRule.Add(ModContent.ItemType<PhotonRipper>());
		mainRule.Add(ModContent.ItemType<TheJailor>());
		mainRule.Add(ModContent.ItemType<AresExoskeleton>());
		mainRule.Add(ModContent.ItemType<AresMask>(), maskFraction);
		LeadingConditionRule mainRule2 = itemLoot.DefineConditionalDropSet(() => DownedBossSystem.downedThanatos);
		mainRule2.Add(ModContent.ItemType<SpineOfThanatos>());
		mainRule2.Add(ModContent.ItemType<RefractionRotor>());
		mainRule2.Add(ModContent.ItemType<AtlasMunitionsBeacon>());
		mainRule2.Add(ModContent.ItemType<ThanatosMask>(), maskFraction);
		LeadingConditionRule mainRule3 = itemLoot.DefineConditionalDropSet(() => DownedBossSystem.downedArtemisAndApollo);
		mainRule3.Add(ModContent.ItemType<SurgeDriver>());
		mainRule3.Add(ModContent.ItemType<TheAtomSplitter>());
		mainRule3.Add(ModContent.ItemType<ArtemisMask>(), maskFraction);
		mainRule3.Add(ModContent.ItemType<ApolloMask>(), maskFraction);
		itemLoot.Add(ModContent.ItemType<DraedonsHeart>());
		itemLoot.Add(ModContent.ItemType<ExoThrone>());
		itemLoot.AddRevBagAccessories();
		itemLoot.Add(ModContent.ItemType<DraedonMask>(), maskFraction);
		itemLoot.Add(ModContent.ItemType<ThankYouPainting>(), 100);
	}
}
