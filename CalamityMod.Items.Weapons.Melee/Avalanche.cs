using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class Avalanche : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 64;
		base.Item.height = 64;
		base.Item.damage = 75;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = 35;
		base.Item.useTime = 35;
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.knockBack = 7.25f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		int type = ModContent.ProjectileType<IceBombFriendly>();
		if (player.ownedProjectileCounts[type] < 16)
		{
			IEntitySource source = player.GetSource_ItemUse(base.Item);
			int bombDamage = player.CalcIntDamage<MeleeDamageClass>(base.Item.damage);
			for (int k = 0; k < 4; k++)
			{
				Projectile.NewProjectile(source, target.Center, Vector2.Zero, type, bombDamage, hit.Knockback * 0.5f, Main.myPlayer, 0f, target.whoAmI, k);
			}
		}
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(3))
		{
			int iceDust = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 67, player.direction * 2, 0f, 150, default(Color), 1.5f);
			Dust obj = Main.dust[iceDust];
			obj.velocity *= 0.2f;
		}
	}
}
