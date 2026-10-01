using System.Linq;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class CrushsawCrasher : RogueWeapon
{
	private bool HasHoveredOverNameInGFB;

	public override void SetDefaults()
	{
		base.Item.width = 38;
		base.Item.height = 22;
		base.Item.damage = 57;
		base.Item.useAnimation = 18;
		base.Item.useStyle = 1;
		base.Item.useTime = 18;
		base.Item.knockBack = 7f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.shoot = ModContent.ProjectileType<Crushax>();
		base.Item.shootSpeed = 13.5f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int spread = 3;
			for (int i = 0; i < 6; i++)
			{
				Vector2 perturbedspeed = Utils.RotatedBy(new Vector2(velocity.X + (float)Main.rand.Next(-3, 4), velocity.Y + (float)Main.rand.Next(-3, 4)), (double)MathHelper.ToRadians((float)spread), default(Vector2));
				int proj = Projectile.NewProjectile(source, position, perturbedspeed, type, damage, knockback, player.whoAmI);
				if (proj.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[proj].Calamity().stealthStrike = true;
					Main.projectile[proj].penetrate = 1;
				}
				spread -= Main.rand.Next(1, 4);
			}
			return false;
		}
		return true;
	}

	public override void UpdateInventory(Player player)
	{
		if (Main.zenithWorld)
		{
			if (Main.HoverItem.type == base.Item.type)
			{
				if (!HasHoveredOverNameInGFB)
				{
					HasHoveredOverNameInGFB = true;
					string[] firstWords = (from str in this.GetLocalizedValue("GFBFirstWords").Split('\n', '\r')
						select str.Trim()).ToArray();
					string[] lastWords = (from str in this.GetLocalizedValue("GFBLastWords").Split('\n', '\r')
						select str.Trim()).ToArray();
					string firstWord = firstWords[Main.rand.Next(firstWords.Length)];
					string lastWord = lastWords[Main.rand.Next(lastWords.Length)];
					string separator = this.GetLocalizedValue("GFBWordSeparator");
					base.Item.SetNameOverride(firstWord + separator + lastWord);
				}
			}
			else
			{
				HasHoveredOverNameInGFB = false;
			}
		}
		else
		{
			base.Item.ClearNameOverride();
		}
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 300);
	}

	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 300);
	}
}
