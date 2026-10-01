using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Pets;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.NPCs.SupremeCalamitas;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.TreasureBags;

[LegacyName(new string[] { "SCalBag", "SupremeCalamitasCoffer" })]
public class CalamitasCoffer : ModItem, ILocalizedModType, IModType
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
		base.Item.rare = 11;
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
		base.Item.TreasureBagLightAndDust();
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		return CalamityUtils.DrawTreasureBagInWorld(base.Item, spriteBatch, ref rotation, ref scale, whoAmI);
	}

	public override void ModifyItemLoot(ItemLoot itemLoot)
	{
		itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<SupremeCalamitas>()));
		itemLoot.Add(ModContent.ItemType<AshesofAnnihilation>(), 1, 30, 40);
		itemLoot.Add(DropHelper.CalamityStyle(DropHelper.BagWeaponDropRateFraction, ModContent.ItemType<Violence>(), ModContent.ItemType<Condemnation>(), ModContent.ItemType<Heresy>(), ModContent.ItemType<Vehemence>(), ModContent.ItemType<Perdition>(), ModContent.ItemType<Vigilance>(), ModContent.ItemType<Sacrifice>()));
		itemLoot.Add(ModContent.ItemType<Calamity>());
		itemLoot.AddRevBagAccessories();
		IItemDropRule scalVanitySet = ItemDropRule.Common(ModContent.ItemType<SCalMask>(), 7);
		scalVanitySet.OnSuccess(ItemDropRule.Common(ModContent.ItemType<AshenHorns>()));
		scalVanitySet.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SCalRobes>()));
		scalVanitySet.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SCalBoots>()));
		itemLoot.Add(scalVanitySet);
		itemLoot.Add(ModContent.ItemType<BrimstoneJewel>());
		itemLoot.Add(ModContent.ItemType<ThankYouPainting>(), 100);
	}
}
