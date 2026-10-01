using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[AutoloadEquip(new EquipType[] { EquipType.Shield })]
public class StygianShield : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle DashChargeSound = new SoundStyle("CalamityMod/Sounds/Item/StygianDashCharge");

	public static readonly SoundStyle DashSound = new SoundStyle("CalamityMod/Sounds/Item/StygianDash");

	public static readonly SoundStyle DashHitSound = new SoundStyle("CalamityMod/Sounds/Item/StygianBonk", 3);

	public static readonly SoundStyle ShieldThrowSound = new SoundStyle("CalamityMod/Sounds/Item/StygianThrow");

	public static readonly SoundStyle ThrowLoopSound = new SoundStyle("CalamityMod/Sounds/Item/StygianThrowLoop");

	public static readonly SoundStyle ShieldThrowHitSound = CommonCalamitySounds.ExoHitSound;

	public static readonly SoundStyle ShieldCatchSound = new SoundStyle("CalamityMod/Sounds/Item/StygianCatch");

	public const int HeldDefense = 16;

	public const int DisableDashDuration = 90;

	public int ThrownShieldID = ModContent.ProjectileType<StygianShieldThrown>();

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(16, 1.5.ToString("N1"));

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 74;
		base.Item.height = 78;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.damage = 180;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.useAnimation = (base.Item.useTime = 40);
		base.Item.shoot = ModContent.ProjectileType<StygianShieldAttack>();
		base.Item.shootSpeed = 10f;
		base.Item.knockBack = 6f;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.Calamity().donorItem = true;
		base.Item.UseSound = null;
	}

	public override bool AltFunctionUse(Player player)
	{
		return player.ownedProjectileCounts[ThrownShieldID] <= 1;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override float UseSpeedMultiplier(Player player)
	{
		return 2.5f;
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		if (player.altFunctionUse == 2)
		{
			type = ThrownShieldID;
			knockback *= 0f;
		}
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<OrnateShield>().AddIngredient<ScoriaBar>(6).AddIngredient(175, 6)
			.AddIngredient<LivingShard>(6)
			.AddTile(134)
			.Register();
	}
}
