using CalamityMod.CalPlayer;
using CalamityMod.Items.PermanentBoosters;
using CalamityMod.Items.Potions.Food;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Tools;

public class DisgustingMeat : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Tools";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<DeliciousMeat>();
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(26, 36, 0, 0);
		base.Item.value = 0;
		base.Item.UseSound = SoundID.NPCDeath13;
		base.Item.maxStack = 1;
		base.Item.consumable = false;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool? UseItem(Player player)
	{
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		if (player.ItemTimeIsZero)
		{
			CalamityPlayer calPlayer = player.Calamity();
			if (player.altFunctionUse == 2)
			{
				TryDropBoosterItem(player, ref calPlayer.rageBoostOne, ModContent.ItemType<MushroomPlasmaRoot>());
				TryDropBoosterItem(player, ref calPlayer.rageBoostTwo, ModContent.ItemType<InfernalBlood>());
				TryDropBoosterItem(player, ref calPlayer.rageBoostThree, ModContent.ItemType<RedLightningContainer>());
				TryDropBoosterItem(player, ref calPlayer.adrenalineBoostOne, ModContent.ItemType<ElectrolyteGelPack>());
				TryDropBoosterItem(player, ref calPlayer.adrenalineBoostTwo, ModContent.ItemType<StarlightFuelCell>());
				TryDropBoosterItem(player, ref calPlayer.adrenalineBoostThree, ModContent.ItemType<Ectoheart>());
			}
			else
			{
				if (calPlayer.sTangerine || calPlayer.mFruit || calPlayer.tCloudberry || calPlayer.sStrawberry)
				{
					TryDropBoosterItem(player, ref calPlayer.sTangerine, ModContent.ItemType<SanguineTangerine>());
					TryDropBoosterItem(player, ref calPlayer.mFruit, ModContent.ItemType<MiracleFruit>());
					TryDropBoosterItem(player, ref calPlayer.tCloudberry, ModContent.ItemType<TaintedCloudberry>());
					TryDropBoosterItem(player, ref calPlayer.sStrawberry, ModContent.ItemType<SacredStrawberry>());
				}
				else
				{
					for (int i = 0; i < player.ConsumedLifeFruit; i++)
					{
						int drop = Item.NewItem(player.GetSource_DropAsItem(), player.Hitbox, 1291);
						Main.item[drop].noGrabDelay = 100;
					}
					player.ConsumedLifeFruit = 0;
					for (int j = 0; j < player.ConsumedLifeCrystals; j++)
					{
						int drop2 = Item.NewItem(player.GetSource_DropAsItem(), player.Hitbox, 29);
						Main.item[drop2].noGrabDelay = 100;
					}
					player.ConsumedLifeCrystals = 0;
				}
				if (calPlayer.cShard || calPlayer.eCore || calPlayer.pHeart)
				{
					TryDropBoosterItem(player, ref calPlayer.cShard, ModContent.ItemType<CometShard>());
					TryDropBoosterItem(player, ref calPlayer.eCore, ModContent.ItemType<EtherealCore>());
					TryDropBoosterItem(player, ref calPlayer.pHeart, ModContent.ItemType<PhantomHeart>());
				}
				else
				{
					for (int k = 0; k < player.ConsumedManaCrystals; k++)
					{
						int drop3 = Item.NewItem(player.GetSource_DropAsItem(), player.Hitbox, 109);
						Main.item[drop3].noGrabDelay = 100;
					}
					player.ConsumedManaCrystals = 0;
				}
			}
			return true;
		}
		return null;
	}

	private static void TryDropBoosterItem(Player player, ref bool condition, int itemType)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (condition)
		{
			condition = false;
			int drop = Item.NewItem(player.GetSource_DropAsItem(), player.Hitbox, itemType);
			Main.item[drop].noGrabDelay = 100;
		}
	}
}
