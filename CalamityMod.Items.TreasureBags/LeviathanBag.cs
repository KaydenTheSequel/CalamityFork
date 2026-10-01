using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.Placeables.Furniture.Monoliths;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.NPCs.Leviathan;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.TreasureBags;

public class LeviathanBag : ModItem, ILocalizedModType, IModType
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
		itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<Leviathan>()));
		itemLoot.Add(DropHelper.CalamityStyle(DropHelper.BagWeaponDropRateFraction, ModContent.ItemType<Greentide>(), ModContent.ItemType<Leviatitan>(), ModContent.ItemType<AnahitasArpeggio>(), ModContent.ItemType<Atlantis>(), ModContent.ItemType<GastricBelcherStaff>(), ModContent.ItemType<Whitewater>(), ModContent.ItemType<LeviathanTeeth>()));
		itemLoot.Add(ModContent.ItemType<LeviathanAmbergris>());
		itemLoot.Add(ModContent.ItemType<PearlofEnthrallment>(), DropHelper.BagWeaponDropRateFraction);
		itemLoot.Add(ModContent.ItemType<TheCommunity>(), 10);
		itemLoot.AddRevBagAccessories();
		itemLoot.Add(ModContent.ItemType<AnahitaMask>(), 7);
		itemLoot.Add(ModContent.ItemType<LeviathanMask>(), 7);
		itemLoot.Add(ModContent.ItemType<DeepSeaAnchor>(), 10);
		itemLoot.Add(ModContent.ItemType<ThankYouPainting>(), 100);
	}
}
