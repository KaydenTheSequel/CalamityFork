using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class Viscera : ModItem, ILocalizedModType, IModType
{
	public const int BoomLifetime = 40;

	public int Counter;

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 50;
		base.Item.height = 52;
		base.Item.damage = 229;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 18;
		base.Item.useTime = 7;
		base.Item.useAnimation = 22;
		base.Item.reuseDelay = 40;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 5f;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<VisceraBeam>();
		base.Item.shootSpeed = 6f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		Counter++;
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/MagnaCannonShot");
		style.Volume = 0.2f;
		style.Pitch = 0.95f;
		SoundEngine.PlaySound(in style, position);
		position += velocity.RotatedBy(-0.75f * (float)player.direction) * 1.8f;
		Projectile.NewProjectile(source, position, velocity.RotatedByRandom(0.02500000037252903), type, (int)((double)damage * (1.0 + (double)(Counter - 1) * 0.2)), knockback, player.whoAmI, 0f, (Counter == 4) ? 1 : 0);
		if (Counter >= 4)
		{
			Counter = 0;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BloodstoneCore>(4).AddTile(134).Register();
	}
}
