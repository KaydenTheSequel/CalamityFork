using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class TacticiansTrumpCard : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 74;
		base.Item.height = 70;
		base.Item.damage = 248;
		base.Item.knockBack = 7f;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.useAnimation = (base.Item.useTime = 12);
		base.Item.mana = 20;
		base.Item.useTurn = true;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 13.5f;
		base.Item.shoot = ModContent.ProjectileType<TacticiansTrumpCardProj>();
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item1;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.Calamity().donorItem = true;
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(3))
		{
			int dustType = (Main.rand.NextBool() ? 132 : 264);
			int dust = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, dustType);
			Main.dust[dust].noGravity = true;
		}
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(144, 300);
	}

	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		target.AddBuff(144, 300);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3507).AddIngredient<Apathanull>().AddIngredient<FlareBolt>()
			.AddIngredient<Tradewinds>()
			.AddIngredient<NuclearFury>()
			.AddIngredient<UelibloomBar>(5)
			.AddIngredient<DarkPlasma>(3)
			.AddTile(134)
			.Register();
		CreateRecipe().AddIngredient(3501).AddIngredient<Apathanull>().AddIngredient<FlareBolt>()
			.AddIngredient<Tradewinds>()
			.AddIngredient<NuclearFury>()
			.AddIngredient<UelibloomBar>(5)
			.AddIngredient<DarkPlasma>(3)
			.AddTile(134)
			.Register();
	}
}
