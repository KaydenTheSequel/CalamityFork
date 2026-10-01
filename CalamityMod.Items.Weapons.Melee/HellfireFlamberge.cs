using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class HellfireFlamberge : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 58;
		base.Item.height = 60;
		base.Item.damage = 90;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = 20;
		base.Item.useStyle = 1;
		base.Item.useTime = 20;
		base.Item.useTurn = true;
		base.Item.knockBack = 7.75f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.shoot = ModContent.ProjectileType<VolcanicFireball>();
		base.Item.shootSpeed = 20f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item20, player.Center);
		for (int index = 0; index < 3; index++)
		{
			float SpeedX = velocity.X + (float)Main.rand.Next(-40, 41) * 0.05f;
			float SpeedY = velocity.Y + (float)Main.rand.Next(-40, 41) * 0.05f;
			float damageMult = 0.5f;
			switch (index)
			{
			case 0:
			case 1:
				type = ModContent.ProjectileType<VolcanicFireball>();
				break;
			case 2:
				type = ModContent.ProjectileType<VolcanicFireballLarge>();
				damageMult = 0.75f;
				break;
			}
			Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, (int)((float)damage * damageMult), knockback, player.whoAmI);
		}
		return false;
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(3))
		{
			Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, Main.rand.NextBool(3) ? 16 : 174);
		}
		if (Main.rand.NextBool(5) && !Main.dedServ)
		{
			int smoke = Gore.NewGore(player.GetSource_ItemUse(base.Item), new Vector2((float)hitbox.X, (float)hitbox.Y), default(Vector2), Main.rand.Next(375, 378), 0.75f);
			Main.gore[smoke].behindTiles = true;
		}
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(323, 300);
	}

	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		target.AddBuff(323, 300);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ScoriaBar>(15).AddTile(134).Register();
	}
}
