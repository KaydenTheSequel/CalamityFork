using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class ClothiersWrath : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 30;
		base.Item.damage = 36;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 15;
		base.Item.useTime = 22;
		base.Item.useAnimation = 22;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 3f;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.UseSound = SoundID.Item8;
		base.Item.autoReuse = true;
		base.Item.shoot = 585;
		base.Item.shootSpeed = 9f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		int numProj = 3;
		float rotation = MathHelper.ToRadians(2f);
		for (int i = 0; i < numProj; i++)
		{
			Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)i / (float)(numProj - 1)));
			int proj = Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
			if (proj.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[proj].DamageType = DamageClass.Magic;
				Main.projectile[proj].penetrate = 3;
				Main.projectile[proj].usesLocalNPCImmunity = true;
				Main.projectile[proj].localNPCHitCooldown = -1;
			}
		}
		return false;
	}
}
