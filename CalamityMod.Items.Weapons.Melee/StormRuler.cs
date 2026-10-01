using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class StormRuler : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 80;
		base.Item.height = 84;
		base.Item.damage = 80;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = 25;
		base.Item.useTime = 25;
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.knockBack = 6.25f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityRedBuyPrice;
		base.Item.rare = 10;
		base.Item.shoot = ModContent.ProjectileType<StormRulerProj>();
		base.Item.shootSpeed = 20f;
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(5))
		{
			int swingDust = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 187, player.direction * 2, 0f, 150, default(Color), 1.3f);
			Dust obj = Main.dust[swingDust];
			obj.velocity *= 0.2f;
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<StormSaber>().AddIngredient<WindBlade>().AddIngredient(3456, 6)
			.AddTile(412)
			.Register();
	}
}
