using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class TrueCausticEdge : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 64;
		base.Item.height = 74;
		base.Item.damage = 100;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = 28;
		base.Item.useStyle = 1;
		base.Item.useTime = 28;
		base.Item.useTurn = true;
		base.Item.knockBack = 6f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.shoot = ModContent.ProjectileType<CausticEdgeProjectile>();
		base.Item.shootSpeed = 12f;
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		damage = (int)((double)damage * 0.75);
		knockback *= 0.5f;
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(3))
		{
			int dustType = (Main.rand.NextBool() ? 74 : 171);
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
		target.AddBuff(70, 180);
	}

	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		target.AddBuff(70, 180);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<TaintedBlade>().AddRecipeGroup("CursedFlameIchor", 8).AddIngredient(2607, 8)
			.AddTile(16)
			.Register();
	}
}
