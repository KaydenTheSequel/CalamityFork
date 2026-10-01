using CalamityMod.Items.Placeables.FurnitureDriftwood;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Tools;

public class DriftwoodHammer : ModItem, ILocalizedModType, IModType
{
	public static int NormalUseTime = 11;

	public static int FasterUseTime = 8;

	public new string LocalizationCategory => "Items.Tools";

	public override void SetDefaults()
	{
		base.Item.damage = 13;
		base.Item.knockBack = 4f;
		base.Item.useTime = NormalUseTime;
		base.Item.useAnimation = 31;
		base.Item.hammer = 25;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.width = 40;
		base.Item.height = 42;
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.value = CalamityGlobalItem.RarityWhiteBuyPrice;
		base.Item.rare = 0;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
	}

	public override float UseTimeMultiplier(Player player)
	{
		if (player.Calamity().countsAsAnyWet)
		{
			return (float)FasterUseTime / (float)NormalUseTime;
		}
		return base.UseTimeMultiplier(player);
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().countsAsAnyWet)
		{
			Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 160);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Driftwood>(8).AddTile(18).Register();
	}
}
