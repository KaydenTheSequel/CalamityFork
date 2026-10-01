using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.Pets;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.NPCs.Perforator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.TreasureBags;

public class PerforatorBag : ModItem, ILocalizedModType, IModType
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
		itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<PerforatorHive>()));
		itemLoot.Add(1257, 1, 15, 20);
		itemLoot.Add(1330, 1, 15, 20);
		itemLoot.AddIf(() => Main.hardMode, 1332, 1, 25, 30);
		itemLoot.Add(2171, 1, 10, 15);
		itemLoot.Add(DropHelper.CalamityStyle(DropHelper.BagWeaponDropRateFraction, new WeightedItemStack[7]
		{
			ModContent.ItemType<Aorta>(),
			ModContent.ItemType<SausageMaker>(),
			ModContent.ItemType<VeinBurster>(),
			ModContent.ItemType<Eviscerator>(),
			ModContent.ItemType<BloodBath>(),
			ModContent.ItemType<FleshOfInfidelity>(),
			ModContent.ItemType<ToothBall>()
		}));
		itemLoot.Add(ModContent.ItemType<BloodstainedGlove>(), DropHelper.BagWeaponDropRateFraction);
		itemLoot.Add(ModContent.ItemType<BloodyWormTooth>());
		itemLoot.AddRevBagAccessories();
		itemLoot.Add(ModContent.ItemType<PerforatorMask>(), 7);
		itemLoot.Add(ModContent.ItemType<BloodyVein>(), 10);
		itemLoot.Add(ModContent.ItemType<ThankYouPainting>(), 100);
	}
}
