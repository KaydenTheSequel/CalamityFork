using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "TyrantYharimsUltisword" })]
public class BlightedCleaver : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 88;
		base.Item.height = 88;
		base.Item.damage = 90;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = (base.Item.useTime = 28);
		base.Item.useStyle = 1;
		base.Item.useTurn = true;
		base.Item.knockBack = 8f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.shootsEveryUse = true;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.shoot = ModContent.ProjectileType<BlazingPhantomBlade>();
		base.Item.shootSpeed = 16f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		float adjustedItemScale = player.GetAdjustedItemScale(base.Item);
		Projectile.NewProjectile(source, player.MountedCenter, velocity, type, (int)((double)damage * 0.75), knockback * 0.5f, player.whoAmI, (float)player.direction * player.gravDir, 32f, adjustedItemScale);
		NetMessage.SendData(13, -1, -1, null, player.whoAmI);
		return false;
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(3))
		{
			int dustType = 171;
			switch (Main.rand.Next(5))
			{
			case 2:
				dustType = 60;
				break;
			case 3:
				dustType = 296;
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
		target.AddBuff(70, 240);
		target.AddBuff(323, 240);
	}

	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		target.AddBuff(70, 240);
		target.AddBuff(323, 240);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<TrueCausticEdge>().AddIngredient(1570).AddIngredient(1006, 15)
			.AddIngredient(1339, 10)
			.AddTile(134)
			.Register();
	}
}
