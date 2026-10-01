using System.Collections.Generic;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class HalibutCannon : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 118;
		base.Item.height = 56;
		base.Item.damage = 50;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 10;
		base.Item.useAnimation = 20;
		base.Item.useStyle = 5;
		base.Item.rare = ModContent.RarityType<HotPink>();
		base.Item.noMelee = true;
		base.Item.knockBack = 1f;
		base.Item.value = CalamityGlobalItem.RarityHotPinkBuyPrice;
		base.Item.UseSound = null;
		base.Item.autoReuse = true;
		base.Item.shoot = 14;
		base.Item.shootSpeed = 12f;
		base.Item.useAmmo = AmmoID.Bullet;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-15f, 0f);
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		list.FindAndReplace("[GFB]", this.GetLocalizedValue(Main.zenithWorld ? "TooltipGFB" : "TooltipNormal"));
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item38, player.Center);
		if (Main.zenithWorld && Main.rand.Next(5) < 4)
		{
			return false;
		}
		int bulletAmt = Main.rand.Next(25, 36);
		for (int index = 0; index < bulletAmt; index++)
		{
			float SpeedX = velocity.X + (float)Main.rand.Next(-10, 11) * 0.05f;
			float SpeedY = velocity.Y + (float)Main.rand.Next(-10, 11) * 0.05f;
			int shot = Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI);
			Main.projectile[shot].timeLeft = 120;
		}
		return false;
	}
}
