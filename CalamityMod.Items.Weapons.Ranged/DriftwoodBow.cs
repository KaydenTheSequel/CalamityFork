using CalamityMod.Items.Placeables.FurnitureDriftwood;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class DriftwoodBow : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.damage = 10;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.width = 22;
		base.Item.height = 42;
		base.Item.useTime = 28;
		base.Item.useAnimation = 28;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 0f;
		base.Item.value = CalamityGlobalItem.RarityWhiteBuyPrice;
		base.Item.rare = 0;
		base.Item.UseSound = SoundID.Item5;
		base.Item.autoReuse = true;
		base.Item.shoot = 1;
		base.Item.shootSpeed = 6.6f;
		base.Item.useAmmo = AmmoID.Arrow;
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().countsAsAnyWet)
		{
			velocity *= 1.3f;
			knockback++;
			for (int i = 0; i <= 18; i++)
			{
				Vector2 position2 = position + velocity * 3f;
				Vector2? velocity2 = velocity.RotatedByRandom(MathHelper.ToRadians(19f)) * Main.rand.NextFloat(0.8f, 3.8f);
				float scale = Main.rand.NextFloat(1.2f, 1.6f);
				Dust.NewDustPerfect(position2, 160, velocity2, 0, default(Color), scale).noGravity = true;
			}
		}
	}

	public override float UseSpeedMultiplier(Player player)
	{
		if (!player.Calamity().countsAsAnyWet)
		{
			return 1f;
		}
		return 1.2f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Driftwood>(10).AddTile(18).Register();
	}
}
