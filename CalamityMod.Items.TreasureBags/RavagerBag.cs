using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor.Vanity;
using CalamityMod.Items.PermanentBoosters;
using CalamityMod.Items.Placeables.Furniture.Paintings;
using CalamityMod.Items.TreasureBags.MiscGrabBags;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.NPCs.Ravager;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.TreasureBags;

public class RavagerBag : ModItem, ILocalizedModType, IModType
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
		base.Item.expert = true;
		base.Item.rare = 9;
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
		itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<RavagerBody>()));
		itemLoot.AddIf(() => !DownedBossSystem.downedProvidence, ModContent.ItemType<FleshyGeode>());
		itemLoot.AddIf(() => DownedBossSystem.downedProvidence, ModContent.ItemType<NecromanticGeode>());
		itemLoot.Add(DropHelper.CalamityStyle(DropHelper.BagWeaponDropRateFraction, ModContent.ItemType<UltimusCleaver>(), ModContent.ItemType<RealmRavager>(), ModContent.ItemType<Hematemesis>(), ModContent.ItemType<SpikecragStaff>(), ModContent.ItemType<CraniumSmasher>()));
		itemLoot.Add(ModContent.ItemType<Vesuvius>(), 10);
		itemLoot.Add(ModContent.ItemType<CorpusAvertor>(), 20);
		itemLoot.Add(ModContent.ItemType<BloodPact>());
		itemLoot.Add(ModContent.ItemType<FleshTotem>(), 2);
		itemLoot.AddIf(() => DownedBossSystem.downedProvidence, ModContent.ItemType<BloodflareCore>());
		itemLoot.AddRevBagAccessories();
		itemLoot.AddIf((DropAttemptInfo info) => CalamityWorld.revenge && !info.player.Calamity().rageBoostTwo, ModContent.ItemType<InfernalBlood>());
		itemLoot.Add(ModContent.ItemType<RavagerMask>(), 7);
		itemLoot.Add(ModContent.ItemType<ThankYouPainting>(), 100);
	}
}
