using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "TrueTyrantYharimsUltisword" })]
public class DefiledGreatsword : ModItem, ILocalizedModType, IModType
{
	public const int TotalProjectiles = 3;

	public const float ProjectileFullyVisibleDuration = 40f;

	public const float ProjectileFullyVisibleDurationIncreasePerAdditionalProjectile = 8f;

	public const float ShootSpeed = 16f;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 102;
		base.Item.height = 102;
		base.Item.damage = 119;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = (base.Item.useTime = 28);
		base.Item.useStyle = 1;
		base.Item.useTurn = true;
		base.Item.knockBack = 9f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.shootsEveryUse = true;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.shoot = ModContent.ProjectileType<BlazingPhantomBlade>();
		base.Item.shootSpeed = 16f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		float adjustedItemScale = player.GetAdjustedItemScale(base.Item);
		for (int i = 0; i < 3; i++)
		{
			float ai1 = 40f + (float)i * 8f;
			float velocityMultiplier = 1f - (float)i / 3f;
			Projectile.NewProjectile(source, player.MountedCenter, velocity * velocityMultiplier, type, (int)((double)damage * 0.75), knockback * 0.5f, player.whoAmI, (float)player.direction * player.gravDir, ai1, adjustedItemScale);
		}
		NetMessage.SendData(13, -1, -1, null, player.whoAmI);
		return false;
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(3))
		{
			int dustType = 171;
			switch (Main.rand.Next(5))
			{
			case 2:
				dustType = Main.rand.Next(3) switch
				{
					1 => 64, 
					2 => 61, 
					_ => 60, 
				};
				break;
			case 3:
				dustType = Main.rand.Next(3) switch
				{
					1 => 169, 
					2 => 75, 
					_ => 296, 
				};
				break;
			case 4:
				dustType = 74;
				break;
			}
			int dust = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, dustType, 0f, 0f, 100, default(Color), Main.rand.NextFloat(1.8f, 2.4f));
			Main.dust[dust].noGravity = true;
			Dust obj = Main.dust[dust];
			obj.velocity *= 0f;
			if (dustType == 171)
			{
				Main.dust[dust].fadeIn = 1.5f;
			}
		}
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(70, 360);
		target.AddBuff(323, 360);
	}

	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		target.AddBuff(70, 360);
		target.AddBuff(323, 360);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BlightedCleaver>().AddIngredient<CoreofCalamity>().AddIngredient<UelibloomBar>(15)
			.AddTile(134)
			.Register();
	}
}
