using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.MarniteArchitect;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
public class MarniteArchitectHeadgear : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle LiftSpawnSound = new SoundStyle("CalamityMod/Sounds/Item/MarniteLiftSummon");

	public static readonly SoundStyle LiftGoAwaySound = new SoundStyle("CalamityMod/Sounds/Item/MarniteLiftUnsummon");

	public static readonly SoundStyle LiftHummSound = new SoundStyle("CalamityMod/Sounds/Item/MarniteLiftHumm")
	{
		IsLooped = true
	};

	public static int TileRangeBoost = 5;

	public static float LiftRaiseSpeed = 2f;

	public static float MaxLiftHeight = 138f;

	public static float LiftHeightOffset = 22f;

	public new string LocalizationCategory => "Items.Armor.PreHardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(TileRangeBoost);

	public override void Load()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		On_Player.QuickMount += new hook_QuickMount(ActivateLift);
	}

	public override void SetStaticDefaults()
	{
		if (!Main.dedServ)
		{
			int equipSlot = EquipLoader.GetEquipSlot(base.Mod, Name, EquipType.Head);
			ArmorIDs.Head.Sets.DrawFullHair[equipSlot] = true;
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.defense = 4;
	}

	private void ActivateLift(orig_QuickMount orig, Player self)
	{
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		if (!self.mount.Active && HasArmorSet(self) && self.miscEquips[3].IsAir)
		{
			if (!self.frozen && !self.tongued && !self.webbed && !self.stoned && self.gravDir != -1f && !self.dead && !self.noItems)
			{
				int liftMountType = ModContent.MountType<MarniteLift>();
				if (self.mount.CanMount(liftMountType, self))
				{
					self.mount.SetMount(liftMountType, self);
					SoundEngine.PlaySound(in LiftSpawnSound, self.Center);
				}
			}
		}
		else
		{
			orig.Invoke(self);
		}
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		return body.type == ModContent.ItemType<MarniteArchitectToga>();
	}

	public static bool HasArmorSet(Player player)
	{
		if (player.armor[0].type == ModContent.ItemType<MarniteArchitectHeadgear>())
		{
			return player.armor[1].type == ModContent.ItemType<MarniteArchitectToga>();
		}
		return false;
	}

	public bool IsPartOfSet(Item item)
	{
		if (item.type != ModContent.ItemType<MarniteArchitectHeadgear>())
		{
			return item.type == ModContent.ItemType<MarniteArchitectToga>();
		}
		return true;
	}

	public override void UpdateArmorSet(Player player)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		Color AbilityBriefColor = Color.Lerp(new Color(255, 243, 161), new Color(137, 162, 255), 0.5f + 0.5f * MathF.Sin(Main.GlobalTimeWrappedHourly * 3f));
		player.setBonus = this.GetLocalization("SetBonus").Format(AbilityBriefColor.Hex3(), (MaxLiftHeight + LiftHeightOffset).ToTiles());
		player.GetModPlayer<MarniteArchitectPlayer>().setEquipped = true;
	}

	public override void UpdateEquip(Player player)
	{
		if (Main.myPlayer == player.whoAmI)
		{
			Player.tileRangeX += TileRangeBoost;
			Player.tileRangeY += TileRangeBoost;
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("AnyGoldCrown").AddIngredient(3086, 5).AddIngredient(3081, 5)
			.AddTile(16)
			.Register();
	}
}
