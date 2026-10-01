using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Demonshade;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
public class DemonshadeHelm : ModItem, IExtendedHat, ILocalizedModType, IModType
{
	public static readonly SoundStyle ActivationSound = new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/DemonshadeEnrage");

	public static int MinionSlotBoost = 2;

	public static float DamageBoost = 0.3f;

	public static int CritBoost = 15;

	public static int SetBonusMinionSlotBoost = 8;

	public static float SetBonusSummonDamageBoost = 1f;

	public static int DevilDamage = 1000;

	public static int EnrageDuration = CalamityUtils.SecondsToFrames(10);

	public static float MultDamageBoost = 0.5f;

	public static double MultDamageTakenBoost = 0.25;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	internal static string ShadowScytheEntitySourceContext => "SetBonus_Calamity_Demonshade";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MinionSlotBoost, DamageBoost.ToPercent(), CritBoost);

	public static int BeamDamage => 300.ScaleWithDifficulty();

	public static int ScytheDamage => 500.ScaleWithDifficulty();

	public string ExtensionTexture => "CalamityMod/Items/Armor/Demonshade/DemonshadeHelm_Extension";

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.defense = 50;
		base.Item.value = CalamityGlobalItem.RarityHotPinkBuyPrice;
		base.Item.rare = ModContent.RarityType<HotPink>();
		base.Item.Calamity().devItem = true;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<DemonshadeBreastplate>())
		{
			return legs.type == ModContent.ItemType<DemonshadeGreaves>();
		}
		return false;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawShadow = true;
		player.armorEffectDrawOutlines = true;
	}

	public override void UpdateArmorSet(Player player)
	{
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusMinionSlotBoost, SetBonusSummonDamageBoost.ToPercent(), CalamityUtils.GetArmorSetBonusKey(), EnrageDuration.FramesToSeconds(), (1f + MultDamageBoost).Round(), (1.0 + MultDamageTakenBoost).Round());
		CalamityPlayer modPlayer = player.Calamity();
		modPlayer.dsSetBonus = true;
		modPlayer.wearingRogueArmor = true;
		modPlayer.WearingPostMLSummonerSet = true;
		if (player.whoAmI == Main.myPlayer && !modPlayer.chibii)
		{
			modPlayer.redDevil = true;
			IEntitySource source = player.GetSource_ItemUse(base.Item);
			if (player.FindBuffIndex(ModContent.BuffType<DemonshadeSetDevilBuff>()) == -1)
			{
				player.AddBuff(ModContent.BuffType<DemonshadeSetDevilBuff>(), 3600);
			}
			if (player.ownedProjectileCounts[ModContent.ProjectileType<DemonshadeRedDevil>()] < 1)
			{
				int damage = (int)player.GetTotalDamage<SummonDamageClass>().ApplyTo(DevilDamage);
				Projectile.NewProjectileDirect(source, player.Center, -Vector2.UnitY, ModContent.ProjectileType<DemonshadeRedDevil>(), damage, 0f, Main.myPlayer, 2f).originalDamage = DevilDamage;
			}
		}
		player.maxMinions += SetBonusMinionSlotBoost;
		player.GetDamage<SummonDamageClass>() += SetBonusSummonDamageBoost;
	}

	public override void UpdateEquip(Player player)
	{
		player.maxMinions += MinionSlotBoost;
		player.GetDamage<GenericDamageClass>() += DamageBoost;
		player.GetCritChance<GenericDamageClass>() += CritBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ShadowspecBar>(12).AddTile<DraedonsForge>().SortBeforeFirstRecipesOf(ModContent.ItemType<DemonshadeBreastplate>())
			.Register();
	}

	public Vector2 ExtensionSpriteOffset(PlayerDrawSet drawInfo)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(0f, -4f);
	}
}
