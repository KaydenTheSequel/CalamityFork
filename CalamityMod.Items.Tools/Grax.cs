using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Tools;

public class Grax : ModItem, ILocalizedModType, IModType
{
	private const int HammerPower = 110;

	private const int AxePower = 36;

	public static float DamageBoost = 0.15f;

	public static int DefenseBoost = 30;

	public static float DamageReductionBoost = 0.1f;

	public new string LocalizationCategory => "Items.Tools";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 62;
		base.Item.height = 62;
		base.Item.damage = 472;
		base.Item.knockBack = 8f;
		base.Item.useTime = 4;
		base.Item.useAnimation = 16;
		base.Item.hammer = 110;
		base.Item.axe = 36;
		base.Item.tileBoost += 5;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useStyle = 1;
		base.Item.useTurn = true;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			base.Item.axe = 0;
			base.Item.hammer = 0;
		}
		else
		{
			base.Item.axe = 36;
			base.Item.hammer = 110;
		}
		return base.CanUseItem(player);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<InfernaCutter>().AddRecipeGroup("LunarHamaxe").AddIngredient<UelibloomBar>(5)
			.AddTile(134)
			.Register();
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		player.AddBuff(ModContent.BuffType<GraxBoost>(), 600);
	}
}
