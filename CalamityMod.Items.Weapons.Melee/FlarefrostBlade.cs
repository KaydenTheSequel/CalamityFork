using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class FlarefrostBlade : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 64;
		base.Item.height = 66;
		base.Item.damage = 125;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = (base.Item.useTime = 29);
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.knockBack = 6.25f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.shoot = ModContent.ProjectileType<Flarefrost>();
		base.Item.shootSpeed = 11f;
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		damage = (int)((double)damage * 0.6);
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		int dustChoice = ((Main.rand.Next(2) != 0) ? 6 : 67);
		if (Main.rand.NextBool(3))
		{
			Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, dustChoice);
		}
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(323, 180);
		target.AddBuff(324, 180);
	}

	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		target.AddBuff(323, 180);
		target.AddBuff(324, 180);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CryonicBar>(8).AddIngredient(175, 8).AddIngredient(520, 3)
			.AddTile(134)
			.Register();
	}
}
