using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class FeralthornClaymore : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 68;
		base.Item.height = 66;
		base.Item.damage = 109;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = 13;
		base.Item.useStyle = 1;
		base.Item.useTime = 13;
		base.Item.useTurn = true;
		base.Item.knockBack = 7.25f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
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
			Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 44);
		}
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(70, 300);
		Projectile.NewProjectile(player.GetSource_ItemUse(base.Item), target.Center.X, target.Center.Y, Main.rand.NextFloat(-18f, 18f), Main.rand.NextFloat(-18f, 18f), ModContent.ProjectileType<ThornBase>(), (int)((double)base.Item.damage * 0.5), 0f, Main.myPlayer);
	}

	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(70, 300);
		Projectile.NewProjectile(player.GetSource_ItemUse(base.Item), target.Center.X, target.Center.Y, Main.rand.NextFloat(-18f, 18f), Main.rand.NextFloat(-18f, 18f), ModContent.ProjectileType<ThornBase>(), (int)((double)base.Item.damage * 0.5), 0f, Main.myPlayer);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PerennialBar>(12).AddTile(134).Register();
	}
}
