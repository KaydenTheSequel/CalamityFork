using System;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class HellwingStaff : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 70;
		base.Item.height = 60;
		base.Item.damage = 21;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 18;
		base.Item.useTime = 30;
		base.Item.useAnimation = 30;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 5f;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.UseSound = SoundID.Item43;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<HellwingBat>();
		base.Item.shootSpeed = 9f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		Vector2 realPlayerPos = default(Vector2);
		Vector2 mouseDist = default(Vector2);
		for (int i = 0; i < 4; i++)
		{
			((Vector2)(ref realPlayerPos))._002Ector((player.MountedCenter.X + (float)Main.mouseX + Main.screenPosition.X - player.position.X + player.Center.X) / 2f, player.MountedCenter.Y - 100f * (float)i);
			((Vector2)(ref mouseDist))._002Ector((float)Main.mouseX + Main.screenPosition.X - realPlayerPos.X, Math.Abs((float)Main.mouseY + Main.screenPosition.Y - realPlayerPos.Y));
			if (mouseDist.Y < 20f)
			{
				mouseDist.Y = 20f;
			}
			mouseDist = mouseDist.SafeNormalize(Vector2.UnitX) * ((Vector2)(ref velocity)).Length();
			mouseDist.X += Main.rand.NextFloat(-0.4f, 0.4f);
			mouseDist.Y += Main.rand.NextFloat(-0.4f, 0.4f);
			Projectile.NewProjectile(source, realPlayerPos, mouseDist, type, damage, knockback, player.whoAmI);
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(175, 10).AddIngredient(5215, 10).AddTile(16)
			.Register();
	}
}
