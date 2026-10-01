using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.Pets;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.NPCs.HiveMind;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.TreasureBags;

public class HiveMindBag : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.TreasureBags";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 3;
		ItemID.Sets.BossBag[base.Type] = true;
		ItemID.Sets.PreHardmodeLikeBossBag[base.Type] = true;
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
		base.Item.TreasureBagLightAndDust();
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		return CalamityUtils.DrawTreasureBagInWorld(base.Item, spriteBatch, ref rotation, ref scale, whoAmI);
	}

	public override void ModifyItemLoot(ItemLoot itemLoot)
	{
		itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<HiveMind>()));
		itemLoot.Add(57, 1, 15, 20);
		itemLoot.Add(68, 1, 15, 20);
		itemLoot.AddIf(() => Main.hardMode, 522, 1, 25, 30);
		itemLoot.Add(59, 1, 10, 15);
		itemLoot.Add(DropHelper.CalamityStyle(DropHelper.BagWeaponDropRateFraction, new WeightedItemStack[5]
		{
			ModContent.ItemType<PerfectDark>(),
			ModContent.ItemType<Shadethrower>(),
			ModContent.ItemType<ShaderainStaff>(),
			ModContent.ItemType<DankStaff>(),
			ModContent.ItemType<RotBall>()
		}));
		itemLoot.Add(ModContent.ItemType<FilthyGlove>(), DropHelper.BagWeaponDropRateFraction);
		itemLoot.Add(ModContent.ItemType<RottenBrain>());
		itemLoot.AddRevBagAccessories();
		itemLoot.Add(ModContent.ItemType<HiveMindMask>(), 7);
		itemLoot.Add(ModContent.ItemType<RottingEyeball>(), 10);
		itemLoot.Add(ModContent.ItemType<ThankYouPainting>(), 100);
	}
}
