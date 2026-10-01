using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.Materials;
using CalamityMod.Items.PermanentBoosters;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.NPCs.SlimeGod;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.TreasureBags;

public class SlimeGodBag : ModItem, ILocalizedModType, IModType
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
		itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<SlimeGodCore>()));
		itemLoot.Add(ModContent.ItemType<PurifiedGel>(), 1, 40, 52);
		itemLoot.Add(DropHelper.CalamityStyle(DropHelper.BagWeaponDropRateFraction, ModContent.ItemType<OverloadedBlaster>(), ModContent.ItemType<AbyssalTome>(), ModContent.ItemType<EldritchTome>(), ModContent.ItemType<CorroslimeStaff>(), ModContent.ItemType<CrimslimeStaff>()));
		itemLoot.Add(ModContent.ItemType<ManaPolarizer>());
		itemLoot.AddRevBagAccessories();
		itemLoot.Add(ItemDropRule.OneFromOptions(7, ModContent.ItemType<SlimeGodMask>(), ModContent.ItemType<SlimeGodMask2>()));
		itemLoot.Add(ModContent.ItemType<ThankYouPainting>(), 100);
		itemLoot.AddIf((DropAttemptInfo info) => CalamityWorld.revenge && !info.player.Calamity().adrenalineBoostOne, ModContent.ItemType<ElectrolyteGelPack>());
	}
}
