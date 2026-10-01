using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.Materials;
using CalamityMod.Items.PermanentBoosters;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.NPCs.Polterghast;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.TreasureBags;

public class PolterghastBag : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.TreasureBags";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 3;
		ItemID.Sets.BossBag[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 32;
		base.Item.height = 34;
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

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/TreasureBags/PolterghastBagGlow", (AssetRequestMode)2).Value);
	}

	public override void ModifyItemLoot(ItemLoot itemLoot)
	{
		itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<Polterghast>()));
		itemLoot.Add(ModContent.ItemType<RuinousSoul>(), 1, 25, 30);
		itemLoot.Add(ModContent.ItemType<Necroplasm>(), 1, 40, 50);
		itemLoot.Add(DropHelper.CalamityStyle(DropHelper.BagWeaponDropRateFraction, ModContent.ItemType<BansheeHook>(), ModContent.ItemType<TerrorBlade>(), ModContent.ItemType<DaemonsFlame>(), ModContent.ItemType<FatesReveal>(), ModContent.ItemType<GhastlyVisage>(), ModContent.ItemType<EtherealSubjugator>(), ModContent.ItemType<GhoulishGouger>()));
		itemLoot.Add(ModContent.ItemType<Affliction>());
		itemLoot.AddIf((DropAttemptInfo info) => CalamityWorld.revenge && !info.player.Calamity().adrenalineBoostThree, ModContent.ItemType<Ectoheart>());
		itemLoot.AddRevBagAccessories();
		itemLoot.Add(ModContent.ItemType<PolterghastMask>(), 7);
		itemLoot.Add(ModContent.ItemType<ThankYouPainting>(), 100);
	}
}
