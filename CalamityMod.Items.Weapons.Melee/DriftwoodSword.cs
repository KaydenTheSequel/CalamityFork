using CalamityMod.Items.Placeables.FurnitureDriftwood;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class DriftwoodSword : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.damage = 14;
		base.Item.width = 42;
		base.Item.height = 46;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = (base.Item.useTime = 20);
		base.Item.useStyle = 1;
		base.Item.knockBack = 4f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.useTurn = true;
		base.Item.value = CalamityGlobalItem.RarityWhiteBuyPrice;
		base.Item.rare = 0;
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

	public override float UseSpeedMultiplier(Player player)
	{
		if (!player.Calamity().countsAsAnyWet)
		{
			return 1f;
		}
		return 1.66f;
	}

	public override void ModifyWeaponKnockback(Player player, ref StatModifier knockback)
	{
		knockback.Base += (player.Calamity().countsAsAnyWet ? 1.5f : 0f);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Driftwood>(7).AddTile(18).Register();
	}
}
