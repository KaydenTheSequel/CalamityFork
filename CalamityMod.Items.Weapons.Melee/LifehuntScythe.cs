using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "LifefruitScythe" })]
public class LifehuntScythe : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 62;
		base.Item.height = 72;
		base.Item.damage = 156;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = 18;
		base.Item.useStyle = 1;
		base.Item.useTime = 18;
		base.Item.useTurn = true;
		base.Item.knockBack = 7.5f;
		base.Item.UseSound = SoundID.Item71;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.shoot = ModContent.ProjectileType<LifeScythe>();
		base.Item.shootSpeed = 12f;
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
		if (Main.rand.NextBool(4))
		{
			Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 75);
		}
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		player.DoLifestealDirect(target, 5, 0.5f);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<UelibloomBar>(15).AddTile(134).Register();
	}
}
