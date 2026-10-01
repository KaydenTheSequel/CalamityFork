using CalamityMod.CalPlayer;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories.Wings;

[AutoloadEquip(new EquipType[] { EquipType.Wings })]
public class SoulofCryogen : BaseWings
{
	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(6, 3));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
		ItemID.Sets.ItemNoGravity[base.Type] = true;
		ArmorIDs.Wing.Sets.Stats[base.Item.wingSlot] = new WingStats(120, 6.25f);
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Item.width = 26;
		base.Item.height = 26;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.expert = true;
	}

	public override void Update(ref float gravity, ref float maxFallSpeed)
	{
		float lightOffset = (float)Main.rand.Next(90, 111) * 0.01f;
		lightOffset *= Main.essScale;
		Lighting.AddLight((int)((base.Item.position.X + (float)(base.Item.width / 2)) / 16f), (int)((base.Item.position.Y + (float)(base.Item.height / 2)) / 16f), 0f * lightOffset, 0.3f * lightOffset, 0.3f * lightOffset);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer modPlayer = player.Calamity();
		modPlayer.cryogenSoul = true;
		if (modPlayer.wingProjectileCooldown <= 0 && player.controlJump && player.jump == 0 && player.velocity.Y != 0f && !player.mount.Active && !player.mount.Cart)
		{
			int p = Projectile.NewProjectile(player.GetSource_Accessory(base.Item), Damage: (int)player.GetBestClassDamage().ApplyTo(32f), X: player.Center.X, Y: player.Center.Y, SpeedX: player.velocity.X * 0f, SpeedY: 2f, Type: ModContent.ProjectileType<FrostShardFriendly>(), KnockBack: 3f, Owner: player.whoAmI, ai0: 1f);
			if (p.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[p].DamageType = DamageClass.Generic;
				Main.projectile[p].frame = Main.rand.Next(5);
			}
			modPlayer.wingProjectileCooldown = 7;
		}
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawInventoryCustomScale(spriteBatch, TextureAssets.Item[base.Type].Value, position, frame, drawColor, itemColor, origin, scale, 1f, new Vector2(0f, 0f), (SpriteEffects)0);
		return false;
	}
}
