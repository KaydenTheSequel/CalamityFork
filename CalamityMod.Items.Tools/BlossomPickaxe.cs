using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Tools;

public class BlossomPickaxe : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Tools";

	public override void SetDefaults()
	{
		base.Item.width = 50;
		base.Item.height = 52;
		base.Item.damage = 92;
		base.Item.knockBack = 6.5f;
		base.Item.useTime = 4;
		base.Item.useAnimation = 12;
		base.Item.pick = 250;
		base.Item.tileBoost += 5;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.rare = ModContent.RarityType<Turquoise>();
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<UelibloomBar>(7).AddTile(134).Register();
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(5))
		{
			Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 75);
		}
	}
}
