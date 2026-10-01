using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class DarklightGreatsword : ModItem, ILocalizedModType, IModType
{
	internal const float ShootSpeed = 2f;

	internal const float ProjectileDamageMultiplier = 0.8f;

	internal const float TrueMeleeSlashProjectileDamageMultiplier = 0.8f;

	internal const float SlashProjectileDamageMultiplier = 0.4125f;

	internal const int SlashProjectileLimit = 4;

	internal const int SlashCreationRate = 18;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 92;
		base.Item.height = 100;
		base.Item.damage = 75;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = (base.Item.useTime = 30);
		base.Item.useStyle = 1;
		base.Item.useTurn = true;
		base.Item.knockBack = 6f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.shoot = ModContent.ProjectileType<DarkBeam>();
		base.Item.shootSpeed = 2f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		Vector2 offset = default(Vector2);
		((Vector2)(ref offset))._002Ector((float)base.Item.width * 0.25f * (float)(-player.direction), (float)base.Item.height * 0.25f);
		position = ((player.gravDir != 1f) ? (position + offset) : (position - offset));
		velocity = (Main.MouseWorld - position).SafeNormalize(Vector2.UnitY) * 2f;
		type = (Main.rand.NextBool() ? type : ModContent.ProjectileType<LightBeam>());
		Projectile.NewProjectile(source, position, velocity, type, (int)((float)damage * 0.8f), knockback * 0.8f, player.whoAmI);
		return false;
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(Main.rand.NextBool() ? 324 : 153, 240);
	}

	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		target.AddBuff(Main.rand.NextBool() ? 324 : 153, 240);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CryonicBar>(12).AddIngredient(520).AddIngredient(521)
			.AddTile(134)
			.Register();
	}
}
